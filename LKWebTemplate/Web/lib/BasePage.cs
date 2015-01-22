using System;
using System.Linq;
using System.Web;
using log4net;
using System.Reflection;
using LKWebTemplate.Model.Basic;
using LKWebTemplate.Model.Basic.Table.Record;
namespace LKWebTemplate
{
    public class BasePage : System.Web.UI.Page
    {
        public LKWebTemplate.Util.Session.Store ss = new LKWebTemplate.Util.Session.Store();
        public static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public BasePage() {
            Load += BasePage_Load;
        }
        

        protected void BasePage_Load(object sender, EventArgs e)
        {
            if (Parameter.Config.ParemterConfigs.GetConfig().AuthenticationType.ToUpper().IndexOf("AD", StringComparison.Ordinal)>=0)
            {
                //是否已經登入系統了
                if (ss.getObject("USER") == null)
                {
                    var modBasic = new BasicModel();

                    //未登入系統，程式將自動登入
                    var userAction = new UserAction();
                    string[] userInfo = HttpContext.Current.User.Identity.Name.Split('\\');
                    string companyName = userInfo[0];
                    string account = userInfo[1];

                    var dtAttendant = modBasic.getAttendantV_By_Company_Account_Password(companyName, account);
                    AttendantV_Record drAttendantV = null;
                    if (dtAttendant.Count == 1)
                    {
                        drAttendantV = dtAttendant.First();
                    }
                    if (drAttendantV != null)
                    {
                        userAction.logon(drAttendantV.COMPANY_ID, drAttendantV.ACCOUNT, drAttendantV.PASSWORD, null);
                        //系統預設頁面
                        Response.Redirect(Page.ResolveUrl(Parameter.Config.ParemterConfigs.GetConfig().DefaultPage));
                    }
                    else
                    {
                        //系統登入頁面
                        Response.Redirect(Page.ResolveUrl(Parameter.Config.ParemterConfigs.GetConfig().LogonPage));
                    }
                }
            }
            else
            {
                if (ss.getObject("USER") == null)
                {
                    string defaultPage = Parameter.Config.ParemterConfigs.GetConfig().DefaultPage;
                    Response.Redirect(Page.ResolveUrl(defaultPage));
                }
            }
        }

        /// <summary>
        /// 取得登入者人員資料
        /// </summary>
        /// <returns></returns>
        public LKWebTemplate.Model.Basic.Table.Record.AttendantV_Record getUser()
        {
            if (ss.ExistKey("USER"))
            {
                return (AttendantV_Record)ss.getObject("USER");
            }
            return null;
        }

        public string showSync()
        {
            string ret = "true";
            if (LK.Config.Cloud.CloudConfigs.GetConfig().Role.ToLower() == "member")
            {
                ret = "false";
            }
            else
            {
                ret = "true";
                return ret;
            }

            if (LK.Config.Cloud.CloudConfigs.GetConfig().IsAuthCenter == false)
            {
                ret = "false";
            }
            else
            {
                ret = "true";
                return ret;
            }

            if (LK.Config.Cloud.CloudConfigs.GetConfig().AuthMaster.Trim().Length > 0)
            {
                ret = "false";
            }
            else
            {
                ret = "true";
                return ret;
            }
            return ret;
        }
    }
  
}
