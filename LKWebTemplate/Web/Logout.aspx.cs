using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LKWebTemplate.Model.Basic.Table;
using LKWebTemplate.Model.Basic.Table.Record;
namespace LKWebTemplate
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LKWebTemplate.Util.Session.Store ss = new LKWebTemplate.Util.Session.Store();            
            ss.ClearCookieInSession();
            if (LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().EnableGuestLogin == true)
            {
                LKWebTemplate.Model.Basic.BasicModel modBasic = new Model.Basic.BasicModel();
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
        }
    }
}
