using System;
using System.Linq;
using System.Web;
using log4net;
using System.Reflection;
using LKWebTemplate.Model.Basic;
using LKWebTemplate.Model.Basic.Table.Record;
using System.Collections.Generic;
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
            var modBasic = new BasicModel();


            if (Parameter.Config.ParemterConfigs.GetConfig().AuthenticationType.ToUpper().IndexOf("AD", StringComparison.Ordinal) >= 0)
            {
                #region AD
                //是否已經登入系統了
                if (ss.getObject("USER") == null)
                {


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
                #endregion
            }
            else
            {
                #region PWD
                if (ss.getObject("USER") == null)
                {
                    if (Parameter.Config.ParemterConfigs.GetConfig().EnableGuestLogin == true)
                    {
                        var company = Parameter.Config.ParemterConfigs.GetConfig().GuestCompany;
                        var account = Parameter.Config.ParemterConfigs.GetConfig().GuestAccount;
                        var drAttendantV = modBasic.getAttendantV_By_Company_Account_ForGuest(company, account);
                        ss.setObject("USER", drAttendantV);
                        #region 將此人員的郡組紀錄到系統暫存區中
                        var userGroup = modBasic.getGroupAttendantVByAttendantUuid(drAttendantV.UUID);
                        List<GroupAttendantV_Record> saveData = new List<GroupAttendantV_Record>();
                        foreach (var item in userGroup)
                        {
                            GroupAttendantV_Record newR = new GroupAttendantV_Record();
                            newR.ATTENDANT_UUID = item.ATTENDANT_UUID;
                            newR.CREATE_DATE = item.CREATE_DATE;
                            newR.ACCOUNT = item.ACCOUNT;
                            newR.APPLICATION_HEAD_UUID = item.APPLICATION_HEAD_UUID;
                            newR.ATTENDANT_C_NAME = item.ATTENDANT_C_NAME;
                            newR.ATTENDANT_E_NAME = item.ATTENDANT_E_NAME;
                            newR.ATTENDANT_UUID = item.ATTENDANT_UUID;
                            newR.COMPANY_C_NAME = item.COMPANY_C_NAME;
                            newR.COMPANY_E_NAME = item.COMPANY_E_NAME;
                            newR.COMPANY_ID = item.COMPANY_ID;
                            newR.COMPANY_UUID = item.COMPANY_UUID;
                            newR.DEPARTMENT_UUID = item.DEPARTMENT_UUID;
                            newR.EMAIL = item.EMAIL;
                            newR.GROUP_HEAD_UUID = item.GROUP_HEAD_UUID;
                            newR.GROUP_ID = item.GROUP_ID;
                            newR.GROUP_NAME_EN_US = item.GROUP_NAME_EN_US;
                            newR.GROUP_NAME_ZH_CN = item.GROUP_NAME_ZH_CN;
                            newR.GROUP_NAME_ZH_TW = item.GROUP_NAME_ZH_TW;
                            newR.IS_ACTIVE = item.IS_ACTIVE;
                            newR.IS_ATTENDANT_ACTIVE = item.IS_ATTENDANT_ACTIVE;
                            newR.IS_GROUP_ACTIVE = item.IS_GROUP_ACTIVE;
                            newR.UPDATE_DATE = item.UPDATE_DATE;
                            newR.UUID = item.UUID;
                            saveData.Add(newR);
                        }
                        if (saveData.Count > 0)
                        {
                            ss.setObject("USER_GROUP", saveData);
                        }
                        else
                        {
                            ss.setObject("USER_GROUP", null);
                        }
                        #endregion
                    }
                    else
                    {
                        string defaultPage = Parameter.Config.ParemterConfigs.GetConfig().LogonPage;
                        Response.Redirect(Page.ResolveUrl(defaultPage));
                    }
                }
                #endregion
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
