#region USING
using System;
using System.IO;
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
[DirectService("UtilAction")]
public class UtilAction : BaseAction
{
    [DirectMethod("getUid", DirectAction.Load)]
    public JObject getUid(Request request)
    {
        #region Declare

        #endregion
        try
        {   /*Cloud身份檢查*/
            //checkUser(request.HttpRequest);
            //if (this.getUser() == null)
            //{
            //    throw new Exception("Identity authentication failed.");
            //}/*權限檢查*/
            //if (!checkProxy(new StackTrace().GetFrame(0)))
            //{
            //    throw new Exception("Permission Denied!");
            //};
            var uid = LK.Util.UID.Instance.GetUniqueID();
            System.Data.DataTable dt = new DataTable();
            dt.Columns.Add("uid");
            var nr = dt.NewRow();
            nr["uid"] = uid;
            dt.Rows.Add(nr);
            return ExtDirect.Direct.Helper.Store.OutputJObject(JsonHelper.DataRowSerializerJObject(dt.Rows[0]));
        }
        catch (Exception ex)
        {
            log.Error(ex);
            LK.MyException.MyException.Error(this, ex);
            /*將Exception轉成EXT Exception JSON格式*/
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }
}