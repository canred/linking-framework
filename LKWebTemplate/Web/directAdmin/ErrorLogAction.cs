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

[DirectService("ErrorLogAction")]
public partial class ErrorLogAction : BaseAction
{
    [DirectMethod("load", DirectAction.Load)]
    public JObject load(string is_read, string pageNo, string limitNo, string sort, string dir, Request request)
    {
        #region Declare
        List<JObject> jobject = new List<JObject>();
        BasicModel basicModel = new BasicModel();
        ErrorLog table = new ErrorLog();
        OrderLimit orderLimit = null;
        #endregion
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
            /*是Store操作一下就可能含有分頁資訊。*/
            orderLimit = ExtDirect.Direct.Helper.Order.getOrderLimit(pageNo, limitNo, sort, dir);
            /*取得總資料數*/
            var totalCount = basicModel.getErrorLog_ByIsRead_Count("N");
            if (totalCount == 0)
            {
                List<ErrorLogV_Record> data = new List<ErrorLogV_Record>();
                return ExtDirect.Direct.Helper.Store.OutputJObject(JsonHelper.RecordBaseListJObject(data), totalCount);
            }
            else {                
                var data = basicModel.getErrorLog_ByIsRead("N", orderLimit);
                return ExtDirect.Direct.Helper.Store.OutputJObject(JsonHelper.RecordBaseListJObject(data), totalCount);
            }           
        }
        catch (Exception ex)
        {         
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    [DirectMethod("UpdateAllRead", DirectAction.Store)]
    public JObject UpdateAllRead(Request request)
    {
        #region Declare
        BasicModel basicModel = new BasicModel();
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
            var data = basicModel.getAllErrorLog_ByIsRead("N");
            if (data.Count > 0)
            {
                foreach (ErrorLog_Record dr in data)
                {
                    dr.IS_READ = "Y";
                    dr.gotoTable().Update(dr);
                }
            }
            return ExtDirect.Direct.Helper.Message.Success.OutputJObject();
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    [DirectMethod("UpdateRead", DirectAction.Load)]
    public JObject UpdateRead(string uuid, Request request)
    {
        #region Declare
        BasicModel basicModel = new BasicModel();
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
            var data = basicModel.getErrorLog_By_Uuid(uuid);
            if (data.AllRecord().Count > 0)
            {
                var dr = data.AllRecord().First();
                dr.IS_READ = "Y";
                dr.gotoTable().Update(dr);
            }
            return ExtDirect.Direct.Helper.Message.Success.OutputJObject();
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }
}

