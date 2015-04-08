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

#endregion

[DirectService("UserAction")]
public partial class UserAction : BaseAction
{

    [DirectMethod("ValidateCode", DirectAction.Load)]
    public JObject ValidateCode(string code, Request request)
    {
        System.Collections.Hashtable hash = new System.Collections.Hashtable();
        try
        {
            /*權限檢查
            if (!checkProxy(new StackTrace().GetFrame(0)))
            {
                throw new Exception("Permission Denied!");
            };
            */
            if (LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().GraphicsCertification == false)
            {
                hash.Add("validation", "ok");
                return ExtDirect.Direct.Helper.Message.Success.OutputJObject(hash);
            }
            string session = ss.getObject("CheckCode").ToString();
            if (session == code)
            {
                hash.Add("validation", "ok");
            }
            else
                hash.Add("validation", "fail");
            return ExtDirect.Direct.Helper.Message.Success.OutputJObject(hash);
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    [DirectMethod("logon", DirectAction.FormSubmission)]
    public JObject logon(string company, string account, string password, Request request)
    {
        try
        { /*權限檢查*/
            if (!checkProxy(new StackTrace().GetFrame(0)))
            {
                throw new Exception("Permission Denied!");
            };
            var drAttendantV = new List<AttendantV_Record>();
            LKWebTemplate.Model.Basic.BasicModel mBasic = new LKWebTemplate.Model.Basic.BasicModel();
            if (
               LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().AuthenticationType.ToUpper().IndexOf("AD") >= 0
               )
            {
                drAttendantV = mBasic.getAttendantV_By_Company_Account_Password_FormDomain(company, account, password, company).ToList();
            }
            else
            {
                drAttendantV = mBasic.getAttendantV_By_Company_Account_Password(company, account, password).ToList();
            }
            System.Collections.Hashtable hash = new System.Collections.Hashtable();
            if (drAttendantV.Count == 0)
            {
                hash.Add("validation", "CANCEL");
                return ExtDirect.Direct.Helper.Message.Success.OutputJObject(hash);
            }
            var drMenu = mBasic.getAuthorityMenuVByAttendantUuid(drAttendantV.First().UUID, LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().InitAppUuid);
            if (drAttendantV.Count == 0)
            {
                hash.Add("validation", "CANCEL");
                LK.MyException.MyException.ErrorNoThrowException(this, new Exception("UserAction->logon沒有此人員帳號存在!"));
            }
            if (drMenu.Count == 0)
            {
                hash.Add("validation", "CANCEL");
                LK.MyException.MyException.ErrorNoThrowException(this, new Exception("UserAction->logon沒有此人員帳號合適的選單存在!"));
            }
            if (drAttendantV.Count > 0 && drMenu.Count > 0)
            {
                hash.Add("validation", "OK");
                ss.setObject("CLOUD_ID", "");
                ss.setObject("USER", drAttendantV.First());
                #region 將此人員的郡組紀錄到系統暫存區中
                var userGroup = mBasic.getGroupAttendantVByAttendantUuid(drAttendantV.First().UUID);
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
                if (!hash.ContainsKey("validation"))
                {
                    hash.Add("validation", "CANCEL");
                }
            }
            if (LK.Config.Cloud.CloudConfigs.GetConfig().IsAuthCenter)
            {
                /*本身是身份認證中心*/
                LKWebTemplate.Controller.Model.Cloud.CloudModel cMod = new LKWebTemplate.Controller.Model.Cloud.CloudModel();
                LKWebTemplate.Model.Basic.Table.Record.ActiveConnection_Record newAc = new LKWebTemplate.Model.Basic.Table.Record.ActiveConnection_Record();
                newAc.UUID = LK.Util.UID.Instance.GetUniqueID();
                newAc.ACCOUNT = account;
                newAc.APPLICATION = "TEST";
                newAc.COMPANY_UUID = drAttendantV.First().COMPANY_UUID;
                newAc.STARTTIME = System.DateTime.Now;
                newAc.EXPIRESTIME = System.DateTime.Now.AddHours(8);
                newAc.IP = request.HttpRequest.UserHostAddress;
                newAc.STATUS = "ONLINE";
                newAc.gotoTable().Insert_Empty2Null(newAc);
                ss.setObject("CLOUD_ID", newAc.UUID);
                ss.setObject("USER", drAttendantV.First());
                hash.Add("CLOUD_ID", newAc.UUID);
            }
            return ExtDirect.Direct.Helper.Message.Success.OutputJObject(hash);
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }
    [DirectMethod("cloudLogon", DirectAction.FormSubmission)]
    public JObject cloudLogon(string applicationName, string company, string account, string password, Request request)
    {
        try
        { /*權限檢查*/
            if (!checkProxy(new StackTrace().GetFrame(0)))
            {
                throw new Exception("Permission Denied!");
            };
            LKWebTemplate.Model.Basic.BasicModel mBasic = new LKWebTemplate.Model.Basic.BasicModel();
            var drAttendantV = mBasic.getAttendantV_By_Company_Account_Password(company, account, password);
            System.Collections.Hashtable hash = new System.Collections.Hashtable();

            if (drAttendantV.Count == 0)
            {
                hash.Add("validation", "CANCEL");
                return ExtDirect.Direct.Helper.Message.Success.OutputJObject(hash);
            }
            var drMenu = mBasic.getAuthorityMenuVByAttendantUuid(drAttendantV.First().UUID, LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().InitAppUuid);
            if (drAttendantV.Count == 0)
            {
                hash.Add("validation", "CANCEL");
                LK.MyException.MyException.ErrorNoThrowException(this, new Exception("UserAction->logon沒有此人員帳號存在!"));
            }
            if (drMenu.Count == 0)
            {
                hash.Add("validation", "CANCEL");
                LK.MyException.MyException.ErrorNoThrowException(this, new Exception("UserAction->logon沒有此人員帳號合適的選單存在!"));
            }
            if (drAttendantV.Count > 0 && drMenu.Count > 0)
            {
                hash.Add("validation", "OK");
                ss.setObject("USER", drAttendantV.First());
            }
            else
            {
                if (!hash.ContainsKey("validation"))
                {
                    hash.Add("validation", "CANCEL");
                }
            }
            if (LK.Config.Cloud.CloudConfigs.GetConfig().IsAuthCenter)
            {
                /*本身是身份認證中心*/
                LKWebTemplate.Controller.Model.Cloud.CloudModel cMod = new LKWebTemplate.Controller.Model.Cloud.CloudModel();
                LKWebTemplate.Model.Basic.Table.Record.ActiveConnection_Record newAc = new LKWebTemplate.Model.Basic.Table.Record.ActiveConnection_Record();
                newAc.UUID = LK.Util.UID.Instance.GetUniqueID();
                newAc.ACCOUNT = account;
                newAc.APPLICATION = applicationName;
                newAc.COMPANY_UUID = drAttendantV.First().COMPANY_UUID;
                newAc.STARTTIME = System.DateTime.Now;
                newAc.EXPIRESTIME = System.DateTime.Now.AddDays(1);
                newAc.IP = request.HttpRequest.UserHostAddress;
                newAc.STATUS = "ONLINE";
                newAc.gotoTable().Insert_Empty2Null(newAc);
                hash.Add("CLOUD_ID", newAc.UUID);
            }
            return ExtDirect.Direct.Helper.Message.Success.OutputJObject(hash);
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    [DirectMethod("cloudLogout", DirectAction.FormSubmission)]
    public JObject cloudLogout(string pApplication, string pCompany, string pAccount, Request request)
    {
        try
        { /*權限檢查*/
            if (!checkProxy(new StackTrace().GetFrame(0)))
            {
                throw new Exception("Permission Denied!");
            };
            LKWebTemplate.Controller.Model.Cloud.CloudModel mod = new LKWebTemplate.Controller.Model.Cloud.CloudModel();
            string cloudId = request.HttpRequest.Headers["CLOUD_ID"];
            var drs = mod.getActiveConnection_By_Uuid(cloudId).AllRecord();
            if (drs.Count > 0)
            {
                foreach (var dr in drs)
                {
                    dr.STATUS = "OFFLINE";
                    dr.gotoTable().Update_Empty2Null(dr);
                }
            }
            System.Collections.Hashtable hash = new System.Collections.Hashtable();
            hash.Add("validation", "OK");
            return ExtDirect.Direct.Helper.Message.Success.OutputJObject(hash);
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    [DirectMethod("forgetPassword", DirectAction.FormSubmission)]
    public JObject forgetPassword(string company, string account, Request request)
    {
        try
        {
            /*權限檢查*/
            if (!checkProxy(new StackTrace().GetFrame(0)))
            {
                throw new Exception("Permission Denied!");
            };
            LKWebTemplate.Model.Basic.BasicModel mBasic = new LKWebTemplate.Model.Basic.BasicModel();
            var drAttendantV = mBasic.getAttendantV_By_Company_Account_Password(company, account);
            System.Collections.Hashtable hash = new System.Collections.Hashtable();
            if (drAttendantV.Count == 1)
            {
                hash.Add("status", "OK");
                hash.Add("email", drAttendantV.First().EMAIL);
                LK.Util.Mail.SmtpMailObj mail = new LK.Util.Mail.SmtpMailObj();
                mail.To = drAttendantV.First().EMAIL;
                mail.Contents = "GHG 14064 System<BR>";
                mail.Contents += "Your Password is " + drAttendantV.First().PASSWORD;
                mail.Subject = "Forget Password from 14064";
                LK.Util.Mail.SmtpMailer.Send(mail);
            }
            else
            {
                hash.Add("status", "FAIL");
                hash.Add("email", "");
            }
            return ExtDirect.Direct.Helper.Message.Success.OutputJObject(hash);
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    [DirectMethod("keepSession", DirectAction.Load)]
    public JObject keepSession(Request request)
    {
        System.Collections.Hashtable hash = new System.Collections.Hashtable();
        try
        {
            /*權限檢查*/
            if (!checkProxy(new StackTrace().GetFrame(0)))
            {
                throw new Exception("Permission Denied!");
            };
            var existSession = ss.getObject("USER");
            if (existSession == null)
            {
                hash.Add("validation", "fail");
            }
            else
            {
                hash.Add("validation", "ok");
            }
            return ExtDirect.Direct.Helper.Message.Success.OutputJObject(hash);
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    [DirectMethod("getUserInfo", DirectAction.Load)]
    public JObject getUserInfo(Request request)
    {
        List<JObject> jobject = new List<JObject>();
        try
        {
            /*Cloud身份檢查*/
            checkUser(request.HttpRequest);
            if (this.getUser() == null)
            {
                throw new Exception("Identity authentication failed.");
            }/*權限檢查*/
            if (!checkProxy(new StackTrace().GetFrame(0)))
            {
                throw new Exception("Permission Denied!");
            };
            getUser().PASSWORD = "*************";
            return (JsonHelper.RecordBaseJObject(getUser()));
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    [DirectMethod("changeAccount", DirectAction.Load)]
    public JObject changeAccount(string company, string account, Request request)
    {
        try
        { /*權限檢查*/
            if (!checkProxy(new StackTrace().GetFrame(0)))
            {
                throw new Exception("Permission Denied!");
            };
            var drAttendantV = new List<AttendantV_Record>();
            LKWebTemplate.Model.Basic.BasicModel mBasic = new LKWebTemplate.Model.Basic.BasicModel();
            var drAttendant = mBasic.getAttendant_By_CompanyUuid_Account(company, account);
            System.Collections.Hashtable hash = new System.Collections.Hashtable();
            if (drAttendant != null)
            {
                hash.Add("validation", "OK");
                ss.setObject("CLOUD_ID", "");
                ss.setObject("USER", mBasic.getAttendantV_By_Uuid(drAttendant.UUID).AllRecord().First());
            }
            else
            {
                if (!hash.ContainsKey("validation"))
                {
                    hash.Add("validation", "CANCEL");
                }
            }
            return ExtDirect.Direct.Helper.Message.Success.OutputJObject(hash);
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }
}