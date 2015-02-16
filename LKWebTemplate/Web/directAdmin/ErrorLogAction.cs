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
public class ErrorLogAction : BaseAction
{
    [DirectMethod("load", DirectAction.Load, MethodVisibility.Visible)]
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
            /*取得資料*/
            var data = basicModel.getErrorLog_ByIsRead("N", orderLimit);
            if (data.Count > 0)
            {
                /*將List<RecordBase>變成JSON字符串*/
                //jsonStr.Append(JsonHelper.RecordBaseListSerializer(data));
                //JsonHelper.RecordBaseListJObject(data);
            }
            /*使用Store Std out 『Sotre物件標準輸出格式』*/
            return ExtDirect.Direct.Helper.Store.OutputJObject(JsonHelper.RecordBaseListJObject(data), totalCount);
        }
        catch (Exception ex)
        {
            //log.Error(ex); 
            //LK.MyException.MyException.ErrorNoThrowException(this, ex);
            /*將Exception轉成EXT Exception JSON格式*/
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    [DirectMethod("UpdateAllRead", DirectAction.Store, MethodVisibility.Visible)]
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
            /*
             * 所有Form的動作最終是使用Submit的方式將資料傳出；
             * 必須有一個特徵來判斷使用者，執行的動作；
             */
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

    [DirectMethod("UpdateRead", DirectAction.Load, MethodVisibility.Visible)]
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

