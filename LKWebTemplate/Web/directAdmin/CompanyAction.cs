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

[DirectService("CompanyAction")]
public class CompanyAction : BaseAction
{
    [DirectMethod("getCompany", DirectAction.Store, MethodVisibility.Visible)]
    public JObject getCompany(string basic_company_uuid,  Request request)
    {
        #region Declare
         List<JObject> jobject = new List<JObject>();        
        LKWebTemplate.Model.Basic.BasicModel model = new LKWebTemplate.Model.Basic.BasicModel();
        #endregion
        try
        {     /*Cloud身份檢查*/
            checkUser(request.HttpRequest);
            if (this.getUser() == null)
            {
                throw new Exception("Identity authentication failed.");
            }/*權限檢查*/
            if (!checkProxy(new StackTrace().GetFrame(0)))
            {
                throw new Exception("Permission Denied!");
            };       
            /*取得總資料數*/          
            var totalCount = 1;
            /*取得資料*/
            var data = model.getCompany_By_Uuid(basic_company_uuid);
            //var data = model.getGhgProjectCompany(basic_company_uuid, is_active, orderLimit);
            if (data.AllRecord().Count > 0)
            {
                /*將List<RecordBase>變成JSON字符串*/
                jobject = JsonHelper.RecordBaseListJObject(data.AllRecord().ToList());
            }
            else {
                totalCount = 0;
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
}

