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
[DirectService("ProxyAction")]
public class ProxyAction : BaseAction
{
    [DirectMethod("loadProxy", DirectAction.Store, MethodVisibility.Visible)]
    public JObject loadProxy(string pApplicationHeadUuid, string pKeyWord, string pageNo, string limitNo, string sort, string dir, Request request)
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
            }/*權限檢查*/
            if (!checkProxy(new StackTrace().GetFrame(0)))
            {
                throw new Exception("Permission Denied!");
            };
            /*是Store操作一下就可能含有分頁資訊。*/
            orderLimit = ExtDirect.Direct.Helper.Order.getOrderLimit(pageNo, limitNo, sort, dir);
            /*取得總資料數*/            
            var totalCount = modBasic.getProxy_By_KeyWord_Count(pApplicationHeadUuid,pKeyWord);
            /*取得資料*/
            var data = modBasic.getProxy_By_KeyWord (pApplicationHeadUuid, pKeyWord, orderLimit);
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
     
    [DirectMethod("submitProxy", DirectAction.FormSubmission, MethodVisibility.Visible)]
    public JObject submitProxy(string uuid,
string proxy_action,
string proxy_method,
string description,
string proxy_type,
string need_redirect,
string redirect_proxy_action,
string redirect_proxy_method,
string application_head_uuid,
        string redirect_src,
                                        HttpRequest request)
    {
        #region Declare
        var action = SubmitAction.None;
        BasicModel modBasic = new BasicModel();
        Proxy_Record drProxy = new Proxy_Record();
        #endregion
        try
        {  /*Cloud身份檢查*/
            checkUser(request);
            if (this.getUser() == null)
            {
                throw new Exception("Identity authentication failed.");
            }/*權限檢查*/
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
                drProxy = modBasic.getProxy_By_Uuid(uuid).AllRecord().First();
            }
            else
            {
                action = SubmitAction.Create;
                drProxy.UUID = LK.Util.UID.Instance.GetUniqueID();                
            }
            /*固定要更新的欄位*/
            //drProxy.UPDATE_DATE = DateTime.Now;

            /*非固定更新的欄位*/
            
            drProxy.DESCRIPTION = description;
            drProxy.PROXY_ACTION = proxy_action;
            drProxy.PROXY_METHOD = proxy_method;
            drProxy.NEED_REDIRECT = need_redirect;
            drProxy.PROXY_TYPE = proxy_type;
            drProxy.REDIRECT_PROXY_ACTION = redirect_proxy_action;
            drProxy.REDIRECT_PROXY_METHOD = redirect_proxy_method;
            drProxy.APPLICATION_HEAD_UUID = application_head_uuid;
            drProxy.REDIRECT_SRC = redirect_src;
            //drAppPage.WEB_SITE = web_site;
            
            if (action == SubmitAction.Edit)
            {
                drProxy.gotoTable().Update_Empty2Null(drProxy);
            }
            else if (action == SubmitAction.Create)
            {
                drProxy.gotoTable().Insert_Empty2Null(drProxy);
            }

            System.Collections.Hashtable otherParam = new System.Collections.Hashtable();
            otherParam.Add("UUID", drProxy.UUID);
            return ExtDirect.Direct.Helper.Message.Success.OutputJObject(otherParam);
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    [DirectMethod("infoProxy", DirectAction.Store, MethodVisibility.Visible)]
    public JObject infoProxy(string pUuid, Request request)
    {
        #region Declare
        BasicModel modBasic = new BasicModel();       
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
            var dtAppPage = modBasic.getProxy_By_Uuid(pUuid);
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

    [DirectMethod("destroyProxyByUuid", DirectAction.Store, MethodVisibility.Visible)]
    public JObject destroyProxyByUuid(string pUuid, Request request)
    {
        #region Declare
        BasicModel modBasic = new BasicModel();
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
            var dtAppPage = modBasic.getProxy_By_Uuid(pUuid);
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


    [DirectMethod("loadVAppmenuProxyMap", DirectAction.Store, MethodVisibility.Visible)]
    public JObject loadVAppmenuProxyMap(string pApplicationHeadUuid, string pAppmenuUuid, string pKeyWord, string pageNo, string limitNo, string sort, string dir, Request request)
    {
        #region Declare
        List<JObject> jobject = new List<JObject>();
        BasicModel modBasic = new BasicModel();
        
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
            orderLimit = ExtDirect.Direct.Helper.Order.getOrderLimit(pageNo, limitNo, sort, dir);
            /*取得總資料數*/
            var totalCount = modBasic.getVAppmenuProxyMap_Count(pApplicationHeadUuid, pAppmenuUuid, pKeyWord);            
            /*取得資料*/
            var data = modBasic.getVAppmenuProxyMap(pApplicationHeadUuid, pAppmenuUuid, pKeyWord, orderLimit);
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

    [DirectMethod("loadAppmenuProxyMapUnSelected", DirectAction.Store, MethodVisibility.Visible)]
    public JObject loadAppmenuProxyMapUnSelected(string pApplicationHeadUuid, string pAppmenuUuid, string pKeyWord, string pageNo, string limitNo, string sort, string dir, Request request)
    {
        #region Declare
        List<JObject> jobject = new List<JObject>();
        BasicModel modBasic = new BasicModel();

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
            orderLimit = ExtDirect.Direct.Helper.Order.getOrderLimit(pageNo, limitNo, sort, dir);
            /*取得總資料數*/
            //var dataAll  = modBasic(pApplicationHeadUuid, pKeyWord);
            
            /*取得資料*/
            var data = modBasic.getAppmenuProxyMap_By_AppMenuUuid(pAppmenuUuid, null);
            var dataAll = modBasic.getProxy_By_KeyWord(pApplicationHeadUuid, pKeyWord, orderLimit);

            var retData = dataAll.Where(c => !data.Any(d => d.PROXY_UUID == c.UUID)).ToList();
            
            if (retData.Count > 0)
            {
                /*將List<RecordBase>變成JSON字符串*/
                jobject = JsonHelper.RecordBaseListJObject(retData);
            }
            /*使用Store Std out 『Sotre物件標準輸出格式』*/
            return ExtDirect.Direct.Helper.Store.OutputJObject(jobject, retData.Count);
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
            /*將Exception轉成EXT Exception JSON格式*/
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    [DirectMethod("addAppmenuProxyMap", DirectAction.Store, MethodVisibility.Visible)]
    public JObject addAppmenuProxyMap(string pApplicationHeadUuid, string pAppmenuUuid, string pProxyUuid, Request request)
    {
        #region Declare
        List<JObject> jobject = new List<JObject>();
        BasicModel modBasic = new BasicModel();
        AppmenuProxyMap_Record recore = new AppmenuProxyMap_Record();        
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
            //orderLimit = ExtDirect.Direct.Helper.Order.getOrderLimit(pageNo, limitNo, sort, dir);
            /*取得總資料數*/
            //var dataAll  = modBasic(pApplicationHeadUuid, pKeyWord);

            /*取得資料*/
            var nowData = modBasic.getAppmenuProxyMap_By_AppMenuUuid(pAppmenuUuid,null);
            var checkDataExist = nowData.Where(c => c.PROXY_UUID.Equals(pProxyUuid)).Count();

            if (checkDataExist == 0)
            {
                recore.UUID = LK.Util.UID.Instance.GetUniqueID();
                recore.PROXY_UUID = pProxyUuid;
                recore.APPMENU_UUID = pAppmenuUuid;
                recore.gotoTable().Insert_Empty2Null(recore);
            }
            else {
                throw new Exception("資料已存在!");
            }            
            /*使用Store Std out 『Sotre物件標準輸出格式』*/
            return ExtDirect.Direct.Helper.Message.Success.OutputJObject();
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
            /*將Exception轉成EXT Exception JSON格式*/
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    [DirectMethod("removeAppmenuProxyMap", DirectAction.Store, MethodVisibility.Visible)]
    public JObject removeAppmenuProxyMap( string pAppmenuUuid, string pProxyUuid, Request request)
    {
        #region Declare
        List<JObject> jobject = new List<JObject>();
        BasicModel modBasic = new BasicModel();        
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
            /*取得資料*/
            var dt = modBasic.getAppmenuProxyMap_By_AppMenuUuid_ProxyUuid(pAppmenuUuid, pProxyUuid);

            if (dt.Count >0 )
            {
                foreach (var item in dt) {
                    item.gotoTable().Delete(item);
                }
            }            
            /*使用Store Std out 『Sotre物件標準輸出格式』*/
            return ExtDirect.Direct.Helper.Message.Success.OutputJObject();
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
            /*將Exception轉成EXT Exception JSON格式*/
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

}

