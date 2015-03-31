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
[DirectService("DeptAction")]
public class DeptAction : BaseAction
{
    [DirectMethod("loadTree", DirectAction.TreeStore)]
    public JObject loadTree(string parentUuid, Request request)
    {
        #region Declare
        JArray jobject = null;
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
            /*是Store操作一下就可能含有分頁資訊。*/
            /*取得資料*/
            var dtDepartment = new Department();
            var dataTable = model.getDepartment_By_ParentUuid(parentUuid);
            /*將List<RecordBase>變成JSON字符串*/
            dataTable.Columns.Add("leaf");
            //dataTable.Columns.Add("id");
            foreach (DataRow dr in dataTable.Rows)
            {
                var children = model.getDepartment_By_ParentUuid("UUID");
                if (children.Rows.Count == 0)
                {
                    dr["leaf"] = "true";
                }
                else
                {
                    dr["leaf"] = "false";
                }
                dr["id"] = dr["UUID"].ToString();
            }
            jobject = JsonHelper.DataTableSerializerJArray(dataTable);
            /*使用Store Std out 『Sotre物件標準輸出格式』*/
            return ExtDirect.Direct.Helper.Tree.Output(jobject, 9999);
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
            /*將Exception轉成EXT Exception JSON格式*/
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    [DirectMethod("loadTreeRoot", DirectAction.Store)]
    public JObject loadTreeRoot(string pCompanyUuid, Request request)
    {
        #region Declare
        List<JObject> jobject = new List<JObject>();
        BasicModel model = new BasicModel();
        Department table = new Department();
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
            var data = model.getDepartment_Root_By_CompanyUuid(pCompanyUuid);
            if (data.Count == 1)
            {
                jobject = JsonHelper.RecordBaseListJObject(data);
            }
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(new Exception("Data Not Found!"));
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
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
            var data = model.getDepartment_By_Uuid(pUuid);
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
    public JObject submit(
        string uuid,
        string is_active,
        string company_uuid,
        string id
       , string c_name,
        string e_name
       , string parent_department_uuid,
        string manager_id,
        string parent_department_uuid_list,
        string s_name,
        string cost_center,
        string src_uuid,
        string manager_uuid,
        string full_department_name, Request request)
    {
        #region Declare
        var action = SubmitAction.None;
        BasicModel model = new BasicModel();
        Department_Record record = new Department_Record();
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
                record = model.getDepartment_By_Uuid(uuid).AllRecord().First();
            }
            else
            {
                action = SubmitAction.Create;
                record.UUID = LK.Util.UID.Instance.GetUniqueID();
                record.CREATE_DATE = DateTime.Now;
            }
            record.IS_ACTIVE = is_active;
            record.UPDATE_DATE = DateTime.Now;
            record.ID = id;
            record.C_NAME = c_name;
            record.E_NAME = e_name;
            record.S_NAME = s_name;
            record.COST_CENTER = cost_center;
            record.SRC_UUID = src_uuid;
            record.COMPANY_UUID = company_uuid;
            record.MANAGER_ID = manager_id;
            record.MANAGER_UUID = manager_uuid;
            record.PARENT_DEPARTMENT_UUID = parent_department_uuid;
            if (action == SubmitAction.Edit)
            {
                record.gotoTable().Update(record);
            }
            else if (action == SubmitAction.Create)
            {
                record.gotoTable().Insert(record);
            }
            return ExtDirect.Direct.Helper.Message.Success.OutputJObject();
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    [DirectMethod("destroy", DirectAction.FormSubmission)]
    public JObject destroy(string uuid, Request request)
    {
        BasicModel modBasic = new BasicModel();
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
                var table = modBasic.getDepartment_By_Uuid(uuid);
                if (table.AllRecord().Count == 1)
                {
                    var data = table.AllRecord().First();
                    data.gotoTable().Delete(data);
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
}

