#region USING
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using ExtDirect;
using ExtDirect.Direct;
using LK.DB.SQLCreater;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using LKWebTemplate.Model.Basic;
using LKWebTemplate.Model.Basic.Table;
using LKWebTemplate.Model.Basic.Table.Record;
using LKWebTemplate;
using System.Text;
using LK.Util;
using System.Data;
using System.Diagnostics;
using System.DirectoryServices;
#endregion
[DirectService("ADAction")]
public partial class ADAction : BaseAction
{
    [DirectMethod("loadUser", DirectAction.Load)]
    public JObject loadUser(Request request)
    {
        #region Declare
        List<JObject> jobject = new List<JObject>();
        BasicModel modBasic = new BasicModel();
        ApplicationHead tblApplication = new ApplicationHead();
        #endregion
        try
        {   
            /*Cloud身份檢查
            checkUser(request.HttpRequest);
            if (this.getUser() == null)
            {
                throw new Exception("Identity authentication failed.");
            }
             **/
            /*權限檢查
            if (!checkProxy(new StackTrace().GetFrame(0)))
            {
                throw new Exception("Permission Denied!");
            };
             * */
            /*是Store操作一下就可能含有分頁資訊。*/
            var drCompany = modBasic.getCompany_By_Uuid(this.getUser().COMPANY_UUID).AllRecord().First();
            if (drCompany.IS_SYNC_AD_USER.ToUpper() != "Y")
            {
                throw new Exception("AD人員同步功能未啟動!");
            }
            var allUser = syncUser(drCompany.AD_LDAP, drCompany.AD_LDAP_USER, drCompany.AD_LDAP_USER_PASSWORD);
            var drsUser = modBasic.getAttendant_By_CompanyUuid(this.getUser().COMPANY_UUID);
            /*先設定所有人員的有啟用為 「否」，但除了admin外*/
            foreach (var user in drsUser)
            {
                if (user.IS_ADMIN != "Y")
                {
                    user.IS_ACTIVE = "N";
                    user.gotoTable().Update_Empty2Null(user);
                }
            }
            foreach (System.Collections.DictionaryEntry user in allUser)
            {
                var isExistCount = drsUser.Where(c => c.ACCOUNT.ToUpper().Equals(user.Key.ToString().ToUpper())).Count();
                if (isExistCount == 0)
                {
                    //create new 
                    Attendant_Record newAt = new Attendant_Record();
                    newAt.COMPANY_UUID = this.getUser().COMPANY_UUID;
                    newAt.CREATE_DATE = System.DateTime.Now;
                    newAt.ACCOUNT = user.Key.ToString();
                    try
                    {
                        newAt.C_NAME = user.Value.ToString().Split('(')[1].Replace(")", "").Trim();
                    }
                    catch
                    {
                        continue;
                    }
                    newAt.E_NAME = newAt.ACCOUNT;
                    newAt.E_NAME = newAt.ACCOUNT;
                    newAt.ID = newAt.ACCOUNT;
                    newAt.IS_ACTIVE = "Y";
                    newAt.IS_ADMIN = "N";
                    newAt.IS_DEFAULT_PASS = "N";
                    newAt.IS_DIRECT = "N";
                    newAt.IS_MANAGER = "N";
                    newAt.IS_SUPPER = "N";
                    newAt.PASSWORD = newAt.ACCOUNT;
                    newAt.UPDATE_DATE = System.DateTime.Now;
                    newAt.UUID = LK.Util.UID.Instance.GetUniqueID();
                    newAt.EMAIL = newAt.ACCOUNT;
                    newAt.gotoTable().Insert_Empty2Null(newAt);
                }
                else
                {
                    var drUser = drsUser.Where(c => c.ACCOUNT.ToUpper().Equals(user.Key.ToString().ToUpper())).First();
                    drUser.IS_ACTIVE = "Y";
                    drUser.gotoTable().Update_Empty2Null(drUser);
                }
            }
            /*使用Store Std out 『Sotre物件標準輸出格式』*/
            return ExtDirect.Direct.Helper.Message.Success.OutputJObject();
        }
        catch (Exception ex)
        {
            log.Error(ex);
            LK.MyException.MyException.Error(this, ex);
            /*將Exception轉成EXT Exception JSON格式*/
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    [DirectMethod("testLDAP", DirectAction.Load)]
    public JObject testLDAP(string ADString, string account, string password, Request request)
    {
        #region Declare
        List<JObject> jobject = new List<JObject>();
        BasicModel modBasic = new BasicModel();
        ApplicationHead tblApplication = new ApplicationHead();
        #endregion
        try
        {
            var ht = syncUser(ADString, account, password);
            if (ht.Count > 0)
            {
                /*使用Store Std out 『Sotre物件標準輸出格式』*/
                return ExtDirect.Direct.Helper.Message.Success.OutputJObject();
            }
            else
            {
                return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(new Exception("Test AD Fail!"));
            }
        }
        catch (Exception ex)
        {
            log.Error(ex);
            LK.MyException.MyException.Error(this, ex);
            /*將Exception轉成EXT Exception JSON格式*/
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    private System.Collections.Hashtable syncUser(string ADString, string account, string password)
    {
        System.Collections.Hashtable ht = new System.Collections.Hashtable();
        string sFromWhere = ADString;
        DirectoryEntry deBase = new DirectoryEntry(sFromWhere, account, password);
        DirectorySearcher dsLookFor = new DirectorySearcher(deBase);
        dsLookFor.SearchScope = SearchScope.Subtree;
        dsLookFor.PropertiesToLoad.Add("cn");
        SearchResultCollection srcUsers = dsLookFor.FindAll();
        foreach (SearchResult srcUser in srcUsers)
        {
            var a = srcUser.GetDirectoryEntry();
            if (srcUser.GetDirectoryEntry().SchemaClassName == "user")
            {
                var obj = srcUser.GetDirectoryEntry();
                if (obj.Name.Split('=').Length > 1)
                {
                    var _account = obj.Name.Split('=')[1].Split(new char[] { ' ', '(' })[0].Trim();
                    if (_account.Length > 0)
                    {
                        ht.Add(_account, obj.Name);
                    }
                }
            }
        }
        return ht;
    }
}

