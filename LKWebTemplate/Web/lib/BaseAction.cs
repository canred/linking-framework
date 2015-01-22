using log4net;
using System.Reflection;
using System.Diagnostics;
using LKWebTemplate.Model.Basic;
using LKWebTemplate.Model.Basic.Table;
using LKWebTemplate.Model.Basic.Table.Record;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
namespace LKWebTemplate
{

    public class BaseAction
    {
        public Util.Session.Store ss = new LKWebTemplate.Util.Session.Store();
        public static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
       
        public enum SubmitAction { 
            None,Edit,Create,Update,Delete
        }

        /// <summary>
        /// 取得登入者人員資料
        /// </summary>
        /// <returns></returns>
        public LKWebTemplate.Model.Basic.Table.Record.AttendantV_Record getUser()
        {
            if (ss.ExistKey("USER"))
            {
                
                return (Model.Basic.Table.Record.AttendantV_Record)ss.getObject("USER");
            }
            return null;
        }

        public enum CheckUserStatus { 
            NULL,PASS,EXPIRES,Illegal
        }

        public CheckUserStatus checkUser(System.Web.HttpRequest httpRequest)
        {
            if (this.getUser() == null) {
                if (httpRequest.Headers["CLOUD_ID"] != null) {
                    LK.Cloud cloud = new LK.Cloud();
                    var cloudid = httpRequest.Headers["CLOUD_ID"].ToString();
                    LKWebTemplate.Controller.Model.Cloud.CloudModel mod = new LKWebTemplate.Controller.Model.Cloud.CloudModel();
                    LKWebTemplate.Model.Basic.BasicModel bmod = new BasicModel();
                    IList<LKWebTemplate.Model.Basic.Table.Record.ActiveConnection_Record> drsAc = new List<LKWebTemplate.Model.Basic.Table.Record.ActiveConnection_Record>();
                    if (LK.Config.Cloud.CloudConfigs.GetConfig().IsAuthCenter)
                    {
                        drsAc = mod.getActiveConnection_By_Uuid(cloudid).AllRecord();
                    }
                    else {
                        var authMaster = "http://"+ LK.Config.Cloud.CloudConfigs.GetConfig().AuthMaster+"//router.ashx";

                        var jsonObj  = cloud.CallDirect(authMaster, "CloudAction.loadActiveConnection", new string[1] { cloudid }, "");
                        var jitem = jsonObj["result"]["data"];
                        foreach (var item in jitem) {
                            LKWebTemplate.Model.Basic.Table.Record.ActiveConnection_Record newData = new LKWebTemplate.Model.Basic.Table.Record.ActiveConnection_Record();
                            newData.ACCOUNT = item["ACCOUNT"].Value<string>();
                            newData.UUID = item["UUID"].Value<string>();
                            newData.STATUS = item["STATUS"].Value<string>();
                            newData.STARTTIME = item["STARTTIME"].Value<System.DateTime>();
                            newData.IP = item["IP"].Value<string>();
                            newData.EXPIRESTIME = item["EXPIRESTIME"].Value<System.DateTime>();
                            newData.COMPANY_UUID = item["COMPANY_UUID"].Value<string>();
                            newData.APPLICATION = item["APPLICATION"].Value<string>();
                            newData.ACCOUNT = item["ACCOUNT"].Value<string>();
                            drsAc.Add(newData);
                        }
                    }

                    

                    
                    if (drsAc.Count > 0) {
                        if (System.DateTime.Now > drsAc.First().EXPIRESTIME) {
                            throw new System.Exception("Connection has expired!");                            
                        }
                        var drAc = drsAc.First();
                        if (drAc.STATUS=="OFFLINE")
                        {
                            throw new System.Exception("Connection has closed!");
                        }
                        var drAttendant = bmod.getAttendant_By_CompanyUuid_Account(drAc.COMPANY_UUID, drAc.ACCOUNT);
                        var drCompany = bmod.getCompany_By_Uuid(drAc.COMPANY_UUID).AllRecord().First();
                        UserAction u = new UserAction();
                        u.logon(drCompany.ID, drAttendant.ACCOUNT, drAttendant.PASSWORD, httpRequest);
                        if (drAc.IP != httpRequest.UserHostAddress) {
                            throw new System.Exception("Illegal connections");
                        }
                        return CheckUserStatus.PASS;
                    }
                }
            }
            return CheckUserStatus.PASS;
        }

        public bool checkProxy(StackFrame sf) {
           
            if (LK.Config.DirectAuth.DirectAuthConfigs.GetConfig().ProxyPermission == false) {
                return true;
            }
            BasicModel model = new BasicModel();            
            var methodName = sf.GetMethod().Name;
            var className = this.GetType().Name;

            System.Collections.Hashtable htNoPermission = new System.Collections.Hashtable();
            
            var json = "{" + LK.Config.DirectAuth.DirectAuthConfigs.GetConfig().NoPermissionAction + "}";
            var jo = JObject.Parse(json);

            //var list = jo["list"].Value<string>().ToUpper();

            foreach (var subItem in jo["list"])
            {
                if (subItem["name"] != null)
                {
                    string _a = subItem["name"].Value<string>();
                    htNoPermission.Add(_a.ToUpper(), "");
                }
            }


            if (htNoPermission.ContainsKey(className.ToUpper() + "." + methodName.ToUpper()))
            {
                return true;
            }
            int hasPer = 0;
            if (this.getUser() != null)
            {
                hasPer = model.getVAuthProxy_By_AttendantUuid_ProxyAction_ProxyMethod(this.getUser().UUID, className, methodName);
            }
            if (hasPer == 1)
            {
                return true;
            }
            else {
                return false;
            }
           // model = null;
        }


    }
}
