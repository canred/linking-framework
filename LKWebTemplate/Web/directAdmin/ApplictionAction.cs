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

[DirectService("ApplicationAction")]
public class ApplicationAction : BaseAction
{
    [DirectMethod("loadApplication", DirectAction.Store, MethodVisibility.Visible)]
    public JObject loadApplication(string pKeyWord, string pageNo, string limitNo, string sort, string dir, Request request)
    {
        #region Declare
         List<JObject> jobject = new List<JObject>();
        BasicModel modBasic = new BasicModel();
        ApplicationHead tblApplication = new ApplicationHead();
        OrderLimit orderLimit = null;
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
            /*是Store操作一下就可能含有分頁資訊。*/
            orderLimit = ExtDirect.Direct.Helper.Order.getOrderLimit(pageNo, limitNo, sort, dir);
            /*取得總資料數*/
            var totalCount = modBasic.getApplicationHead_By_KeyWord_Count(pKeyWord);
            /*取得資料*/
            var data = modBasic.getApplicationHead_By_KeyWord(pKeyWord, orderLimit);
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
            log.Error(ex);
            LK.MyException.MyException.Error(this, ex);
            /*將Exception轉成EXT Exception JSON格式*/
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    [DirectMethod("getApplication", DirectAction.Store, MethodVisibility.Visible)]
    public JObject getApplication(string pUuid, Request request)
    {
        #region Declare
        JObject jobject = null;
        BasicModel modBasic = new BasicModel();
        ApplicationHead tblApplication = new ApplicationHead();
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
            /*取得資料*/
            
            var data = modBasic.getApplicationHead_By_Uuid(pUuid);
            if (data!=null)
            {
                /*將List<RecordBase>變成JSON字符串*/                
                jobject = JsonHelper.RecordBaseJObject(data.AllRecord().First());
            }
            /*使用Store Std out 『Sotre物件標準輸出格式』*/
            return ExtDirect.Direct.Helper.Store.OutputJObject(jobject, 1);
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
            /*將Exception轉成EXT Exception JSON格式*/
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }
     
    [DirectMethod("submit", DirectAction.FormSubmission, MethodVisibility.Visible)]
    public JObject submit(  string uuid,
                                        string create_date,
                                        string update_date,
                                        string is_active,
                                        string name,
                                        string description,
                                        string id,
                                        string create_user,
                                        string update_user,
                                        string web_site,
                                        HttpRequest request)
    {
        #region Declare
        var action = SubmitAction.None;
        BasicModel modBasic = new BasicModel();
        ApplicationHead_Record drApplicationHead = new ApplicationHead_Record();
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
                drApplicationHead = modBasic.getApplicationHead_By_Uuid(uuid).AllRecord().First();
            }
            else
            {
                action = SubmitAction.Create;
                drApplicationHead.UUID = LK.Util.UID.Instance.GetUniqueID();
                drApplicationHead.CREATE_DATE = DateTime.Now;
                drApplicationHead.CREATE_USER = getUser().UUID;
                drApplicationHead.UPDATE_USER = getUser().UUID;
                drApplicationHead.CREATE_DATE = DateTime.Now;              
            }
            /*固定要更新的欄位*/
            drApplicationHead.UPDATE_DATE = DateTime.Now;

            /*非固定更新的欄位*/
            drApplicationHead.IS_ACTIVE = is_active;
            drApplicationHead.DESCRIPTION = description;
            drApplicationHead.ID = id;
            drApplicationHead.NAME = name;
            drApplicationHead.WEB_SITE = web_site;
            
            if (action == SubmitAction.Edit)
            {
                drApplicationHead.gotoTable().Update(drApplicationHead);
            }
            else if (action == SubmitAction.Create)
            {
                drApplicationHead.gotoTable().Insert(drApplicationHead);
            }

            System.Collections.Hashtable otherParam = new System.Collections.Hashtable();
            otherParam.Add("UUID", drApplicationHead.UUID);
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
            var dtApplicationHead  = modBasic.getApplicationHead_By_Uuid(pUuid);
            if (dtApplicationHead.AllRecord().Count > 0)
            {
                /*將List<RecordBase>變成JSON字符串*/
                return ExtDirect.Direct.Helper.Form.OutputJObject(JsonHelper.RecordBaseJObject(dtApplicationHead.AllRecord().First()));   
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

