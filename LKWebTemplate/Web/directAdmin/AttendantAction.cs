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
[DirectService("AttendantAction")]
public partial class AttendantAction : BaseAction
{
    [DirectMethod("getUserName", DirectAction.Load)]
    public JObject getUserName(string pUuid, Request request)
    {
        #region Declare
        BasicModel model = new BasicModel();
        #endregion
        try
        {  /*Cloud身份檢查*/
            checkUser(request.HttpRequest);
            if (this.getUser() == null)
            {
                throw new Exception("Identity authentication failed.");
            }/*權限檢查*/
            if (!checkProxy(new StackTrace().GetFrame(0)))
            {
                throw new Exception("Permission Denied!");
            };
            var data = model.getAttendant_By_Uuid(pUuid);
            if (data.AllRecord().Count == 1)
            {
                return ExtDirect.Direct.Helper.JObjectHelper.StringOnly(data.AllRecord().First().C_NAME);
            }

            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(new Exception("Data Not Found!"));
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    [DirectMethod("load", DirectAction.Store)]
    public JObject load(string company_uuid, string keyword, string pageNo, string limitNo, string sort, string dir, Request request)
    {
        #region Declare
        List<JObject> jobject = new List<JObject>();
        BasicModel model = new BasicModel();
        AttendantV_Record table = new AttendantV_Record();
        OrderLimit orderLimit = null;
        #endregion
        try
        {  /*Cloud身份檢查*/
            checkUser(request.HttpRequest);
            if (this.getUser() == null)
            {
                throw new Exception("Identity authentication failed.");
            }/*權限檢查*/
            if (!checkProxy(new StackTrace().GetFrame(0)))
            {
                throw new Exception("Permission Denied!");
            };            
            /*是Store操作一下就可能含有分頁資訊。*/
            var t1 = DateTime.Now;
            orderLimit = ExtDirect.Direct.Helper.Order.getOrderLimit(pageNo, limitNo, sort, dir);
            var t2 = DateTime.Now;
            TimeSpan t = t2 - t1;
            /*取得總資料數*/
            var p1 = DateTime.Now;
            var totalCount = model.getAttendantV_By_CompanyUuid_KeyWord_Count(company_uuid, keyword);
            var p2 = DateTime.Now;
            TimeSpan p = p2 - p1;
            /*取得資料*/
            var q1 = DateTime.Now;
            var data = model.getAttendantV_By_CompanyUuid_KeyWord(company_uuid, keyword, orderLimit);
            var q2 = DateTime.Now;
            TimeSpan q = q2 - q1;

            TimeSpan d = new TimeSpan();

            if (data.Count > 0)
            {
                /*將List<RecordBase>變成JSON字符串*/
                var d1 = DateTime.Now;
                jobject = JsonHelper.RecordBaseListJObject(data);
                var d2 = DateTime.Now;
                d = d2 - d1;                
            }
            /*使用Store Std out 『Sotre物件標準輸出格式』*/
            var z1 = DateTime.Now;
            var ret = ExtDirect.Direct.Helper.Store.OutputJObject(jobject, totalCount);
            var z2 = DateTime.Now;
            TimeSpan z = z2 - z1;

            return ret;
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
            /*將Exception轉成EXT Exception JSON格式*/
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }


    [DirectMethod("loadAnyWhere", DirectAction.Store)]
    public JObject loadAnyWhere(string company_uuid, string keyword, string pageNo, string limitNo, string sort, string dir, Request request)
    {
        #region Declare
        List<JObject> jobject = new List<JObject>();
        BasicModel model = new BasicModel();
        AttendantV_Record table = new AttendantV_Record();
        OrderLimit orderLimit = null;
        #endregion
        try
        {
            /*是Store操作一下就可能含有分頁資訊。*/
            if (LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().WhereAnyChangeAccount)
            {
                orderLimit = ExtDirect.Direct.Helper.Order.getOrderLimit(pageNo, limitNo, sort, dir);
                /*取得總資料數*/
                var totalCount = model.getAttendantV_By_CompanyUuid_KeyWord_Count(company_uuid, keyword);
                /*取得資料*/
                var data = model.getAttendantV_By_CompanyUuid_KeyWord(company_uuid, keyword, orderLimit);
                if (data.Count > 0)
                {
                    /*將List<RecordBase>變成JSON字符串*/
                    jobject = JsonHelper.RecordBaseListJObject(data);
                }
                /*使用Store Std out 『Sotre物件標準輸出格式』*/
                return ExtDirect.Direct.Helper.Store.OutputJObject(jobject, totalCount);
            }
            else
            {
                return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(new Exception("動作不允許"));
            }
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
            /*將Exception轉成EXT Exception JSON格式*/
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }
    [DirectMethod("info", DirectAction.Load)]
    public JObject info(string pUuid, Request request)
    {
        #region Declare
        BasicModel model = new BasicModel();
        #endregion
        try
        {  /*Cloud身份檢查*/
            checkUser(request.HttpRequest);
            if (this.getUser() == null)
            {
                throw new Exception("Identity authentication failed.");
            }/*權限檢查*/
            if (!checkProxy(new StackTrace().GetFrame(0)))
            {
                throw new Exception("Permission Denied!");
            };
            var data = model.getAttendant_By_Uuid(pUuid);

            if (data.AllRecord().Count == 1)
            {
                return ExtDirect.Direct.Helper.Form.OutputJObject(JsonHelper.RecordBaseJObject(data.AllRecord().First()));
            }
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(new Exception("Data Not Found!"));
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    [DirectMethod("submit", DirectAction.FormSubmission)]
    public JObject submit(string uuid,
string create_date,
string update_date,
string is_active,
string company_uuid,
string account,
string c_name,
string e_name,
string email,
string password,
string is_supper,
string is_admin,
string code_page,
string department_uuid,
string phone,
string site_uuid,
string gender,
string birthday,
string hire_date,
string quit_date,
string is_manager,
string is_direct,
string grade,
string id,
string src_uuid,
string is_default_pass, Request request)
    {
        #region Declare
        var action = SubmitAction.None;
        BasicModel model = new BasicModel();
        Attendant_Record record = new Attendant_Record();
        #endregion
        try
        {  /*Cloud身份檢查*/
            checkUser(request.HttpRequest);
            if (this.getUser() == null)
            {
                throw new Exception("Identity authentication failed.");
            }/*權限檢查*/
            if (!checkProxy(new StackTrace().GetFrame(0)))
            {
                throw new Exception("Permission Denied!");
            };
            if (uuid.Trim().Length > 0)
            {
                action = SubmitAction.Edit;
                record = model.getAttendant_By_Uuid(uuid).AllRecord().First();
            }
            else
            {
                action = SubmitAction.Create;
                record.UUID = LK.Util.UID.Instance.GetUniqueID();
                record.CREATE_DATE = DateTime.Now;
            }
            record.ACCOUNT = account;
            record.BIRTHDAY = null;
            record.C_NAME = c_name;
            var changeCodePage = false;
            if (action == SubmitAction.Edit)
            {
                if (record.CODE_PAGE != code_page)
                {
                    changeCodePage = true;
                }
            }
            else
            {
                changeCodePage = true;
            }
            record.CODE_PAGE = code_page;
            record.COMPANY_UUID = getUser().COMPANY_UUID;
            record.DEPARTMENT_UUID = department_uuid;
            record.E_NAME = e_name;
            record.EMAIL = email;
            record.GENDER = gender;
            record.GRADE = grade;
            record.ID = id;
            record.IS_ACTIVE = is_active;
            record.IS_ADMIN = is_admin;
            record.IS_DIRECT = is_direct;
            record.IS_MANAGER = is_manager;
            record.IS_SUPPER = is_supper;
            record.PASSWORD = password;
            record.PHONE = phone;
            record.SITE_UUID = null;
            record.IS_ACTIVE = is_active;
            record.UPDATE_DATE = DateTime.Now;

            #region 附件處理
            if (request.HttpRequest.Files.Count > 0)
            {
                if (request.HttpRequest.Files[0].FileName != "")
                {
                    HttpServerUtility server = System.Web.HttpContext.Current.Server;
                    var uploadFolder = server.MapPath(LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().UploadFolder);
                    System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(uploadFolder);
                    if (di.Exists == false)
                    {
                        di.Create();
                    }
                    //公司用的目錄
                    uploadFolder = uploadFolder + getUser().COMPANY_ID + "//";
                    di = new System.IO.DirectoryInfo(uploadFolder);
                    if (di.Exists == false)
                    {
                        di.Create();
                    }
                    //頭像的folder
                    uploadFolder = uploadFolder + "user//";
                    di = new System.IO.DirectoryInfo(uploadFolder);
                    if (di.Exists == false)
                    {
                        di.Create();
                    }
                    string extName = "";
                    if (request.HttpRequest.Files[0].FileName.Split('.').Length > 1)
                    {
                        extName = request.HttpRequest.Files[0].FileName.Split('.').Last();
                    }
                    var fileUuid = LK.Util.UID.Instance.GetUniqueID();
                    string saveFilePath = "";
                    if (extName.Trim().Length > 0)
                    {
                        saveFilePath = uploadFolder + fileUuid + "." + extName;
                    }
                    else
                    {
                        saveFilePath = uploadFolder + fileUuid;
                    }
                    request.HttpRequest.Files[0].SaveAs(saveFilePath);
                    record.PICTURE_URL = LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().UploadFolder + this.getUser().COMPANY_ID + "//user//" + fileUuid + "." + extName;
                }
            }
            #endregion

            if (action == SubmitAction.Edit)
            {
                record.gotoTable().Update_Empty2Null(record);
            }
            else if (action == SubmitAction.Create)
            {
                record.gotoTable().Insert(record);
                uuid = record.UUID;
            }
            System.Collections.Hashtable otherParam = new System.Collections.Hashtable();
            otherParam.Add("UUID", record.UUID);

            if (changeCodePage)
            {
                LKWebTemplate.Util.Session.Store ss = new LKWebTemplate.Util.Session.Store();
                var drsAttendantV = model.getAttendantV_By_Uuid(record.UUID).AllRecord();
                if (drsAttendantV.Count == 1)
                {
                    ss.setObject("USER", drsAttendantV.First());
                    otherParam.Add("ChangeLanguage", "YES");
                }
            }
            return ExtDirect.Direct.Helper.Message.Success.OutputJObject(otherParam);
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }
}

