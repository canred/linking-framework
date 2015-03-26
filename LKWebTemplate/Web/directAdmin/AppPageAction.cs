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
[DirectService("AppPageAction")]
public class AppPageAction : BaseAction
{
    [DirectMethod("loadAppPage", DirectAction.Store, MethodVisibility.Visible)]
    public JObject loadAppPage(string pApplicationHeadUuid, string pKeyWord, string pageNo, string limitNo, string sort, string dir, Request request)
    {
        #region Declare
        List<JObject> jobject = new List<JObject>();
        BasicModel modBasic = new BasicModel();
        Apppage tblApppage = new Apppage();
        OrderLimit orderLimit = null;
        #endregion
        try
        {  /*Cloud身份檢查*/
            checkUser(request.HttpRequest);
            if (this.getUser() == null)
            {
                throw new Exception("Identity authentication failed.");
            }
            /*權限檢查*/
            if (!checkProxy(new StackTrace().GetFrame(0)))
            {
                throw new Exception("Permission Denied!");
            };
            /*是Store操作一下就可能含有分頁資訊。*/
            orderLimit = ExtDirect.Direct.Helper.Order.getOrderLimit(pageNo, limitNo, sort, dir);
            /*取得總資料數*/
            var totalCount = modBasic.getAppPage_By_KeyWord_Count(pApplicationHeadUuid, pKeyWord);
            /*取得資料*/
            var data = modBasic.getAppPage_By_KeyWord(pApplicationHeadUuid, pKeyWord, orderLimit);
            if (data.Count > 0)
            {
                /*將List<RecordBase>變成JSON字符串*/
                jobject = JsonHelper.RecordBaseListJObject(data);
            }
            /*使用Store Std out 『Sotre物件標準輸出格式』*/
            return ExtDirect.Direct.Helper.Store.OutputJObject(jobject, totalCount);
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
            /*將Exception轉成EXT Exception JSON格式*/
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    [DirectMethod("submit", DirectAction.FormSubmission, MethodVisibility.Visible)]
    public JObject submit(string uuid,
                                        string is_active,
                                        string create_date,
                                        string create_user,
                                        string update_date,
                                        string update_user,
                                        string id,
                                        string name,
                                        string description,
                                        string url,
                                        string parameter_class,
                                        string application_head_uuid,
                                        string p_mode,
                                        string runjsfunction,
                                        HttpRequest request)
    {
        #region Declare
        var action = SubmitAction.None;
        BasicModel modBasic = new BasicModel();
        Apppage_Record drAppPage = new Apppage_Record();
        #endregion
        try
        { /*Cloud身份檢查*/
            checkUser(request);
            if (this.getUser() == null)
            {
                throw new Exception("Identity authentication failed.");
            }
            /*權限檢查*/
            if (!checkProxy(new StackTrace().GetFrame(0)))
            {
                throw new Exception("Permission Denied!");
            };
            /*
             * 所有Form的動作最終是使用Submit的方式將資料傳出；
             * 必須有一個特徵來判斷使用者，執行的動作；
             */
            if (uuid.Trim().Length > 0)
            {
                action = SubmitAction.Edit;
                drAppPage = modBasic.getApppage_By_Uuid(uuid).AllRecord().First();
            }
            else
            {
                action = SubmitAction.Create;
                drAppPage.UUID = LK.Util.UID.Instance.GetUniqueID();
                drAppPage.CREATE_DATE = DateTime.Now;
                drAppPage.CREATE_USER = getUser().UUID;
                drAppPage.UPDATE_USER = getUser().UUID;
                drAppPage.CREATE_DATE = DateTime.Now;
            }
            /*固定要更新的欄位*/
            drAppPage.UPDATE_DATE = DateTime.Now;

            /*非固定更新的欄位*/
            drAppPage.IS_ACTIVE = is_active;
            drAppPage.DESCRIPTION = description;
            drAppPage.ID = id;
            drAppPage.NAME = name;
            drAppPage.URL = url;
            drAppPage.PARAMETER_CLASS = parameter_class;
            drAppPage.APPLICATION_HEAD_UUID = application_head_uuid;
            drAppPage.P_MODE = p_mode;
            drAppPage.RUNJSFUNCTION = runjsfunction;
            //drAppPage.WEB_SITE = web_site;

            if (action == SubmitAction.Edit)
            {
                drAppPage.gotoTable().Update(drAppPage);
            }
            else if (action == SubmitAction.Create)
            {
                drAppPage.gotoTable().Insert(drAppPage);
            }

            System.Collections.Hashtable otherParam = new System.Collections.Hashtable();
            otherParam.Add("UUID", drAppPage.UUID);
            return ExtDirect.Direct.Helper.Message.Success.OutputJObject(otherParam);
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    [DirectMethod("info", DirectAction.Store, MethodVisibility.Visible)]
    public JObject info(string pUuid, Request request)
    {
        #region Declare
        BasicModel modBasic = new BasicModel();
        #endregion
        try
        { /*Cloud身份檢查*/
            checkUser(request.HttpRequest);
            if (this.getUser() == null)
            {
                throw new Exception("Identity authentication failed.");
            }
            /*權限檢查*/
            if (!checkProxy(new StackTrace().GetFrame(0)))
            {
                throw new Exception("Permission Denied!");
            };
            var dtAppPage = modBasic.getApppage_By_Uuid(pUuid);
            if (dtAppPage.AllRecord().Count > 0)
            {
                /*將List<RecordBase>變成JSON字符串*/
                return ExtDirect.Direct.Helper.Form.OutputJObject(JsonHelper.RecordBaseJObject(dtAppPage.AllRecord().First()));
            }
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(new Exception("Data Not Found!"));
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    [DirectMethod("destroyByUuid", DirectAction.Store, MethodVisibility.Visible)]
    public JObject destroyByUuid(string pUuid, Request request)
    {
        #region Declare
        BasicModel modBasic = new BasicModel();
        #endregion
        try
        { /*Cloud身份檢查*/
            checkUser(request.HttpRequest);
            if (this.getUser() == null)
            {
                throw new Exception("Identity authentication failed.");
            }
            /*權限檢查*/
            if (!checkProxy(new StackTrace().GetFrame(0)))
            {
                throw new Exception("Permission Denied!");
            };
            var dtAppPage = modBasic.getApppage_By_Uuid(pUuid);
            if (dtAppPage.AllRecord().Count > 0)
            {
                /*將List<RecordBase>變成JSON字符串*/
                dtAppPage.DeleteAllRecord();
                return ExtDirect.Direct.Helper.Message.Success.OutputJObject();
            }
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(new Exception("Data Not Found!"));
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }
}

