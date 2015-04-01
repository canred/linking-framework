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

    [DirectMethod("loadDepartment", DirectAction.Store)]
    public JObject loadDepartment(string pCompanyUuid, string pageNo, string limitNo, string sort, string dir, Request request)
    {
        #region Declare
        List<JObject> jobject = new List<JObject>();
        BasicModel modBasic = new BasicModel();
        OrderLimit orderLimit = null;
        Department department = new Department();
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
            //var totalCount = modBasic.getDepartment_By_CompanyUuid(pCompanyUuid, orderLimit);
            /*取得資料*/
            var data = modBasic.getDepartment_By_CompanyUuid(pCompanyUuid, orderLimit);
            var totalCount = data.Count;
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



    [DirectMethod("loadTree", DirectAction.TreeStore)]
    public JObject loadTree(string parentUuid, Request request)
    {
        #region Declare
        List<JObject> jobject = new List<JObject>();
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
            OrderLimit orderlimit = new OrderLimit("C_NAME", OrderLimit.OrderMethod.ASC);
            orderlimit.Start = 1;
            orderlimit.Limit = 99999;
            /*取得資料*/
            var genTable = new Department();
            var drsDept = model.getDepartment_By_Uuid(parentUuid).AllRecord();
            var drDept = drsDept.First();            
            drsDept = model.getDepartment_By_CompanyUuid(drDept.COMPANY_UUID, orderlimit);
            var dataTable = model.getDepartment_By_RootUuid_DataTable(parentUuid, orderlimit);
            dataTable.Columns.Add("leaf", System.Type.GetType("System.Boolean"));
            dataTable.Columns.Add("name");
            dataTable.Columns.Add("expanded", System.Type.GetType("System.Boolean"));
            foreach (DataRow dr in dataTable.Rows)
            {
                var children = model.getDepartment_By_RootUuid_DataTable(dr[genTable.UUID].ToString(), orderlimit);
                if (children.Rows.Count == 0)
                {
                    dr["leaf"] = true;
                }
                else
                {
                    dr["leaf"] = false;
                }
                dr["name"] = dr[genTable.C_NAME].ToString();
                dr["expanded"] = true;
            }
            var jarray = JsonHelper.DataTableSerializerJArray(dataTable);
            foreach (var item in jarray)
            {
                var thisUuid = item["UUID"].ToString();
                var thisLeaf = item["leaf"].ToString();
                if (thisLeaf.ToLower() == "false")
                {
                    item["children"] = _loadTree(thisUuid, ref drsDept);
                }
            }
            /*使用Store Std out 『Sotre物件標準輸出格式』*/
            return ExtDirect.Direct.Helper.Tree.Output(jarray, 9999);
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
            /*將Exception轉成EXT Exception JSON格式*/
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    [DirectMethod("_loadTree", DirectAction.TreeStore)]
    public JArray _loadTree(string parentUuid, ref IList<Department_Record> drsDept)
    {
        #region Declare
        List<JObject> jobject = new List<JObject>();
        BasicModel model = new BasicModel();
        #endregion
        try
        {
            /*取得資料*/
            var dataTable = new System.Data.DataTable();
            Department tbl = new Department();
            dataTable.Columns.Add(tbl.COMPANY_UUID);
            dataTable.Columns.Add(tbl.C_NAME);
            dataTable.Columns.Add(tbl.COST_CENTER);
            dataTable.Columns.Add(tbl.CREATE_DATE);
            dataTable.Columns.Add(tbl.E_NAME);
            dataTable.Columns.Add(tbl.FULL_DEPARTMENT_NAME);
            dataTable.Columns.Add(tbl.ID);
            dataTable.Columns.Add(tbl.IS_ACTIVE);
            dataTable.Columns.Add(tbl.MANAGER_ID);
            dataTable.Columns.Add(tbl.MANAGER_UUID);
            dataTable.Columns.Add(tbl.PARENT_DEPARTMENT_ID);
            dataTable.Columns.Add(tbl.PARENT_DEPARTMENT_UUID);
            dataTable.Columns.Add(tbl.PARENT_DEPARTMENT_UUID_LIST);
            dataTable.Columns.Add(tbl.S_NAME);
            dataTable.Columns.Add(tbl.SRC_UUID);
            dataTable.Columns.Add(tbl.UPDATE_DATE);
            dataTable.Columns.Add(tbl.UUID);            
            dataTable.Columns.Add("leaf", System.Type.GetType("System.Boolean"));
            dataTable.Columns.Add("name");
            dataTable.Columns.Add("expanded", System.Type.GetType("System.Boolean"));
            var _drsDept = drsDept.Where(c => c.PARENT_DEPARTMENT_UUID.Equals(parentUuid));
            foreach (var item in _drsDept)
            {
                var dr = dataTable.NewRow();
                dr[tbl.C_NAME] = item.C_NAME;
                dr[tbl.COMPANY_UUID] = item.COMPANY_UUID;
                dr[tbl.COST_CENTER] = item.COST_CENTER;
                dr[tbl.CREATE_DATE] = item.CREATE_DATE;
                dr[tbl.E_NAME] = item.E_NAME;
                dr[tbl.FULL_DEPARTMENT_NAME] = item.FULL_DEPARTMENT_NAME;
                dr[tbl.ID] = item.ID;
                dr[tbl.IS_ACTIVE] = item.IS_ACTIVE;
                dr[tbl.MANAGER_ID] = item.MANAGER_ID;
                dr[tbl.MANAGER_UUID] = item.MANAGER_UUID;
                dr[tbl.PARENT_DEPARTMENT_ID] = item.PARENT_DEPARTMENT_ID;
                dr[tbl.PARENT_DEPARTMENT_UUID] = item.PARENT_DEPARTMENT_UUID;
                dr[tbl.PARENT_DEPARTMENT_UUID_LIST] = item.PARENT_DEPARTMENT_UUID_LIST;
                dr[tbl.S_NAME] = item.S_NAME;
                dr[tbl.SRC_UUID] = item.SRC_UUID;
                dr[tbl.UPDATE_DATE] = item.UPDATE_DATE;
                dr[tbl.UUID] = item.UUID;                
                dr["expanded"] = true;
                var childrenCount = drsDept.Where(c => c.PARENT_DEPARTMENT_UUID.Equals(dr[tbl.UUID].ToString())).Count();
                if (childrenCount == 0)
                {
                    dr["leaf"] = true;
                }
                else
                {
                    dr["leaf"] = false;
                }
                dr["name"] = dr[tbl.C_NAME].ToString();
                dataTable.Rows.Add(dr);
                dataTable.AcceptChanges();
            }
            var jarray = JsonHelper.DataTableSerializerJArray(dataTable);
            foreach (var item in jarray)
            {
                var thisUuid = item["UUID"].ToString();
                var thisLeaf = item["leaf"].ToString();
                if (thisLeaf.ToLower() == "false")
                {
                    item["children"] = _loadTree(thisUuid, ref drsDept);
                }
            }
            return jarray;
        }
        catch (Exception ex)
        {
            log.Error(ex);
            LK.MyException.MyException.Error(this, ex);
            throw ex;
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
            if (data.Count == 0) {
                var drsCompany = model.getCompany_By_Uuid(pCompanyUuid).AllRecord();
                if(drsCompany.Count!=1){                   
                    return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(new Exception("Company cannot find."));
                }
                var drCompany = drsCompany.First();
                Department_Record newDr = new Department_Record();
                newDr.UUID = LK.Util.UID.Instance.GetUniqueID();
                newDr.COMPANY_UUID = pCompanyUuid;
                newDr.CREATE_DATE = DateTime.Now;
                newDr.E_NAME = drCompany.E_NAME;
                newDr.FULL_DEPARTMENT_NAME = drCompany.C_NAME;
                newDr.ID = drCompany.ID;
                newDr.IS_ACTIVE = "Y";
                newDr.S_NAME = drCompany.C_NAME;
                newDr.C_NAME = drCompany.C_NAME;
                newDr.gotoTable().Insert_Empty2Null(newDr);
                data = model.getDepartment_Root_By_CompanyUuid(pCompanyUuid);
            }
            if (data.Count == 1)
            {
                jobject = JsonHelper.RecordBaseListJObject(data);
                return ExtDirect.Direct.Helper.Store.OutputJObject(jobject,1);
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
            System.Collections.Hashtable ht = new System.Collections.Hashtable();
            ht.Add("UUID", record.UUID);
            return ExtDirect.Direct.Helper.Message.Success.OutputJObject(ht);
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

