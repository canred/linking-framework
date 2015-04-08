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

[DirectService("AuthorityAction")]
public partial class AuthorityAction : BaseAction
{
    [DirectMethod("loadAppmenuTree", DirectAction.TreeStore)]
    public JObject loadAppmenuTree(string parentUuid, string pGroupHeadUuid, Request request)
    {
        #region Declare
        List<JObject> jobject = new List<JObject>();
        BasicModel model = new BasicModel();
        AppmenuApppageV tblAppmenu = new AppmenuApppageV();
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
            OrderLimit orderlimit = new OrderLimit("ORD", OrderLimit.OrderMethod.ASC);
            orderlimit.Start = 1;
            orderlimit.Limit = 99999;
            /*取得資料*/
            var genTable = new LKWebTemplate.Model.Basic.Table.AppmenuApppageV();
            var dataTable = model.getAppmenuApppageV_By_ParentUuid_DataTable(parentUuid, orderlimit);
            dataTable.Columns.Add("leaf");            
            dataTable.Columns.Add("checked", typeof(Boolean));            
            dataTable.Columns.Add("DEFAULT_PAGE_CHECKED");
            foreach (DataRow dr in dataTable.Rows)
            {
                var children = model.getAppmenuApppageV_By_ParentUuid_DataTable(dr[tblAppmenu.UUID].ToString(), orderlimit);
                if (children.Rows.Count == 0)
                {
                    dr["leaf"] = "true";
                }
                else
                {
                    dr["leaf"] = "false";
                }
                dr["id"] = dr[tblAppmenu.UUID].ToString();
                //是否"可以"設為預設頁面 
                //dr["IS_DEFAULT_PAGE"]
                //是否"有"被設為預設頁面
                string defaultPageChecked = "N";
                IList<GroupAppmenu_Record> gm_ut = model.getGroupAppmenuV_By_GroupHeadUuid(pGroupHeadUuid);
                var groupMenu = gm_ut.Where(col => col.APPMENU_UUID.Equals(dr[tblAppmenu.UUID]));
                if (groupMenu.Count() > 0)
                {
                    dr["checked"] = true;
                    if (groupMenu.First().IS_DEFAULT_PAGE == "Y")
                        defaultPageChecked = "Y";
                }
                else
                    dr["checked"] = false;

                dr["DEFAULT_PAGE_CHECKED"] = defaultPageChecked;

            }
            JArray jarray = new JArray();
            jarray = JsonHelper.DataTableSerializerJArray(dataTable);
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

    [DirectMethod("loadAppmenuTree2", DirectAction.TreeStore)]
    public JObject loadAppmenuTree2(string parentUuid, string pGroupHeadUuid, Request request)
    {
        #region Declare
        List<JObject> jobject = new List<JObject>();
        BasicModel model = new BasicModel();
        AppmenuApppageV tblAppmenuApppageV = new AppmenuApppageV();
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
            OrderLimit orderlimit = new OrderLimit("ORD", OrderLimit.OrderMethod.ASC);
            orderlimit.Start = 1;
            orderlimit.Limit = 99999;
            /*取得資料*/
            var genTable = new AppmenuApppageV();
            var drsAppmenuApppageV = model.getAppmenuApppageV_By_ParentUuid(parentUuid);
            var drAppmenuApppageV = drsAppmenuApppageV.First();
            drsAppmenuApppageV = model.getAppmenuApppageV_By_ApplicationHeadUuid(drAppmenuApppageV.APPLICATION_HEAD_UUID,orderlimit);
            var dataTable = model.getAppmenuApppageV_By_ParentUuid_DataTable(parentUuid, orderlimit);
            IList<GroupAppmenu_Record> gm_ut = model.getGroupAppmenuV_By_GroupHeadUuid(pGroupHeadUuid);
            dataTable.Columns.Add("leaf", System.Type.GetType("System.Boolean"));            
            dataTable.Columns.Add("name");            
            dataTable.Columns.Add("checked", typeof(Boolean));            
            dataTable.Columns.Add("DEFAULT_PAGE_CHECKED");
            dataTable.Columns.Add("expanded", System.Type.GetType("System.Boolean"));
            foreach (DataRow dr in dataTable.Rows)
            {
                var children = model.getAppmenuApppageV_By_ParentUuid_DataTable(dr[tblAppmenuApppageV.UUID].ToString(), orderlimit);
                if (children.Rows.Count == 0)
                {
                    dr["leaf"] = true;
                }
                else
                {
                    dr["leaf"] = false;
                }
                dr["name"] = dr[tblAppmenuApppageV.NAME_ZH_TW].ToString();
                dr["expanded"] = true;
                string defaultPageChecked = "N";
                var groupMenu = gm_ut.Where(col => col.APPMENU_UUID.Equals(dr[tblAppmenuApppageV.UUID]));
                if (groupMenu.Count() > 0)
                {
                    dr["checked"] = true;
                    if (groupMenu.First().IS_DEFAULT_PAGE == "Y"){
                        defaultPageChecked = "Y";
                    }
                }
                else{
                    dr["checked"] = false;
                }
                dr["DEFAULT_PAGE_CHECKED"] = defaultPageChecked;
            }
            var jarray = JsonHelper.DataTableSerializerJArray(dataTable);
            foreach (var item in jarray)
            {
                var thisUuid = item["UUID"].ToString();
                var thisLeaf = item["leaf"].ToString();
                if (thisLeaf.ToLower() == "false")
                {
                    item["children"] = _loadAppmenuTree2(thisUuid, pGroupHeadUuid, ref drsAppmenuApppageV,ref gm_ut);
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

    public JArray _loadAppmenuTree2(string parentUuid, string pGroupHeadUuid, ref IList<AppmenuApppageV_Record> drsAppmenuApppageV,ref IList<GroupAppmenu_Record> gm_ut)
    {
        #region Declare
        List<JObject> jobject = new List<JObject>();
        BasicModel model = new BasicModel();        
        #endregion
        try
        {
            /*取得資料*/
            var dataTable = new System.Data.DataTable();
            AppmenuApppageV tbl = new AppmenuApppageV();
            dataTable.Columns.Add(tbl.ACTION_MODE);
            dataTable.Columns.Add(tbl.APPLICATION_HEAD_UUID);
            dataTable.Columns.Add(tbl.APPMENU_UUID);
            dataTable.Columns.Add(tbl.CREATE_DATE);
            dataTable.Columns.Add(tbl.CREATE_USER);
            dataTable.Columns.Add(tbl.DESCRIPTION);
            dataTable.Columns.Add(tbl.FUNC_NAME);
            dataTable.Columns.Add(tbl.FUNC_PARAMETER_CLASS);
            dataTable.Columns.Add(tbl.HASCHILD);
            dataTable.Columns.Add(tbl.ID);
            dataTable.Columns.Add(tbl.IMAGE);
            dataTable.Columns.Add(tbl.IS_ACTIVE);
            dataTable.Columns.Add(tbl.IS_ADMIN);
            dataTable.Columns.Add(tbl.IS_DEFAULT_PAGE);
            dataTable.Columns.Add(tbl.NAME_EN_US);
            dataTable.Columns.Add(tbl.NAME_ZH_CN);
            dataTable.Columns.Add(tbl.NAME_ZH_TW);
            dataTable.Columns.Add(tbl.ORD);
            dataTable.Columns.Add(tbl.P_MODE);
            dataTable.Columns.Add(tbl.PARAMETER_CLASS);
            dataTable.Columns.Add(tbl.SITEMAP_UUID);
            dataTable.Columns.Add(tbl.UPDATE_DATE);
            dataTable.Columns.Add(tbl.UPDATE_USER);
            dataTable.Columns.Add(tbl.URL);
            dataTable.Columns.Add(tbl.UUID);
            dataTable.Columns.Add("leaf", System.Type.GetType("System.Boolean"));
            dataTable.Columns.Add("name");
            dataTable.Columns.Add("checked", typeof(Boolean));
            dataTable.Columns.Add("DEFAULT_PAGE_CHECKED");
            dataTable.Columns.Add("expanded", System.Type.GetType("System.Boolean"));
            var _AppmenuApppageV = drsAppmenuApppageV.Where(c => c.APPMENU_UUID.Equals(parentUuid));
            foreach (var item in _AppmenuApppageV)
            {
                var dr = dataTable.NewRow();
                dr[tbl.ACTION_MODE]=item.ACTION_MODE;
                dr[tbl.APPLICATION_HEAD_UUID] = item.APPLICATION_HEAD_UUID;
                dr[tbl.APPMENU_UUID] = item.APPMENU_UUID;
                dr[tbl.CREATE_DATE] = item.CREATE_DATE;
                dr[tbl.CREATE_USER] = item.CREATE_USER;
                dr[tbl.DESCRIPTION] = item.DESCRIPTION;
                dr[tbl.FUNC_NAME] = item.FUNC_NAME;
                dr[tbl.FUNC_PARAMETER_CLASS] = item.FUNC_PARAMETER_CLASS;
                dr[tbl.HASCHILD] = item.HASCHILD;
                dr[tbl.ID] = item.ID;
                dr[tbl.IMAGE] = item.IMAGE;
                dr[tbl.IS_ACTIVE] = item.IS_ACTIVE;
                dr[tbl.IS_ADMIN] = item.IS_ADMIN;
                dr[tbl.IS_DEFAULT_PAGE] = item.IS_DEFAULT_PAGE;
                dr[tbl.NAME_EN_US] = item.NAME_EN_US;
                dr[tbl.NAME_ZH_CN] = item.NAME_ZH_CN;
                dr[tbl.NAME_ZH_TW] = item.NAME_ZH_TW;
                dr[tbl.ORD] = item.ORD;
                dr[tbl.P_MODE] = item.P_MODE;
                dr[tbl.PARAMETER_CLASS] = item.PARAMETER_CLASS;
                dr[tbl.SITEMAP_UUID] = item.SITEMAP_UUID;
                dr[tbl.UPDATE_DATE] = item.UPDATE_DATE;
                dr[tbl.UPDATE_USER] = item.UPDATE_USER;
                dr[tbl.URL] = item.URL;
                dr[tbl.UUID] = item.UUID;
                dr["expanded"] = true;
                var childrenCount = drsAppmenuApppageV.Where(c => c.APPMENU_UUID.Equals(dr[tbl.UUID].ToString())).Count();                
                if (childrenCount == 0)
                {
                    dr["leaf"] = true;                    
                }
                else
                {
                    dr["leaf"] = false;
                }
                dr["name"] = dr[tbl.NAME_ZH_TW].ToString();
                string defaultPageChecked = "N";
                var groupMenu = gm_ut.Where(col => col.APPMENU_UUID.Equals(dr[tbl.UUID]));
                if (groupMenu.Count() > 0)
                {
                    dr["checked"] = true;
                    if (groupMenu.First().IS_DEFAULT_PAGE == "Y")
                        defaultPageChecked = "Y";
                }
                else
                {
                    dr["checked"] = false;
                }
                dr["DEFAULT_PAGE_CHECKED"] = defaultPageChecked;
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
                    item["children"] = _loadAppmenuTree2(thisUuid,  pGroupHeadUuid,ref drsAppmenuApppageV,ref gm_ut);
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
    public JObject loadTreeRoot(string parentUuid, Request request)
    {
        #region Declare
        List<JObject> jobject = new List<JObject>();
        BasicModel model = new BasicModel();
        Appmenu table = new Appmenu();
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
            var data = model.getAppmenuV_Root_By_ApplicationHead(parentUuid);
            if (data.Count == 1)
            {
                return JsonHelper.RecordBaseJObject(data.First());
            }
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(new Exception("Data Not Found!"));
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    [DirectMethod("setGroupAppmenuIsDefaultPage", DirectAction.Store)]
    public JObject setGroupAppmenuIsDefaultPage(string pAppmenuUuid, string pGroupHeadUuid, string pIsDefaultPage, Request request)
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
            System.Collections.Hashtable otherParam = new System.Collections.Hashtable();
            var list = modBasic.getGroupAppmenuV_By_Param(pAppmenuUuid, pGroupHeadUuid);
            string _pIsDefaultPage = "Y";
            if (pIsDefaultPage == "1" || pIsDefaultPage.ToUpper() == "TRUE" || pIsDefaultPage.ToUpper() == "Y"){
                _pIsDefaultPage = "Y";
            }
            else{
                _pIsDefaultPage = "N";
            }
            if (list.Count > 0)
            {
                /*將List<RecordBase>變成JSON字符串*/
                var dr = list.First();
                dr.IS_DEFAULT_PAGE = _pIsDefaultPage;
                dr.UPDATE_DATE = DateTime.Now;
                dr.UPDATE_USER = getUser().UUID;
                dr.gotoTable().Update(dr);
                otherParam.Add("UUID", dr.UUID);
                otherParam.Add("APPMENU_UUID", dr.APPMENU_UUID);
            }
            else//沒有這個Appmenu & GroupHead的對應，因此要新增一筆
            {
                var action = SubmitAction.Create;
                BasicModel model = new BasicModel();
                GroupAppmenu_Record record = new GroupAppmenu_Record();
                record.UUID = LK.Util.UID.Instance.GetUniqueID();
                record.CREATE_DATE = DateTime.Now;
                record.CREATE_USER = getUser().UUID;
                record.CREATE_USER = getUser().UUID;
                record.IS_ACTIVE = "Y";
                record.UPDATE_DATE = DateTime.Now;
                record.APPMENU_UUID = pAppmenuUuid;
                record.GROUP_HEAD_UUID = pGroupHeadUuid;
                record.IS_DEFAULT_PAGE = _pIsDefaultPage;
                if (action == SubmitAction.Create)
                {
                    record.gotoTable().Insert(record);
                }
                otherParam.Add("UUID", record.UUID);
                otherParam.Add("APPMENU_UUID", record.APPMENU_UUID);
            }
            return ExtDirect.Direct.Helper.Message.Success.OutputJObject(otherParam);
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    [DirectMethod("setGroupAppmenu", DirectAction.Store)]
    public JObject setGroupAppmenu(string pAppmenuUuid, string pGroupHeadUuid, string is_checked, Request request)
    {
        #region Declare
        BasicModel modBasic = new BasicModel();
        System.Collections.Hashtable otherParam = new System.Collections.Hashtable();
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
            var dt = modBasic.getGroupAppmenuV_By_Param(pAppmenuUuid, pGroupHeadUuid);
            if (is_checked == "Y")
            {
                //需新增
                if (dt.Count == 0)
                {
                    addGroupAppMenu(dt, pAppmenuUuid, pGroupHeadUuid);
                }
            }
            else
            {
                if (dt.Count > 0)
                {
                    //需刪除
                    List<string> deleteAppMenuUuid = new List<string>();
                    deleteAppMenuUuid = deleteGroupAppMenu(pAppmenuUuid, deleteAppMenuUuid);
                    deleteAppMenuUuid.Add(pAppmenuUuid);//自己也必須被刪除

                    IList<GroupAppmenu_Record> delet_items =
                    modBasic.getGroupAppmenuV_By_AppMenuUuid(pGroupHeadUuid, deleteAppMenuUuid);
                    foreach (GroupAppmenu_Record data in delet_items)
                    {
                        data.gotoTable().Delete(data);
                    }
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

    public void addGroupAppMenu(IList<GroupAppmenu_Record> dt_GroupMenu, string pAppmenuUuid, string pGroupHeadUuid)
    {
        try
        {
            #region Declare
            BasicModel modBasic = new BasicModel();
            #endregion
            if (dt_GroupMenu.Count == 0)
            {
                BasicModel model = new BasicModel();
                GroupAppmenu_Record record = new GroupAppmenu_Record();
                record.UUID = LK.Util.UID.Instance.GetUniqueID();
                record.CREATE_DATE = DateTime.Now;
                record.CREATE_USER = getUser().UUID;
                record.CREATE_USER = getUser().UUID;
                record.IS_ACTIVE = "Y";
                record.UPDATE_DATE = DateTime.Now;
                record.APPMENU_UUID = pAppmenuUuid;
                record.GROUP_HEAD_UUID = pGroupHeadUuid;
                record.IS_DEFAULT_PAGE = "N";
                record.gotoTable().Insert(record);
            }
            var dt = modBasic.getAppmenu_By_Uuid(pAppmenuUuid);
            if (dt.AllRecord().Count > 0)
            {
                string pParentAppMenuUuid = dt.AllRecord().First().APPMENU_UUID;
                if (!string.IsNullOrEmpty(pParentAppMenuUuid))
                {
                    var dtGroupAppmenu =
                       modBasic.getGroupAppmenuV_By_Param(pParentAppMenuUuid, pGroupHeadUuid);
                    addGroupAppMenu(dtGroupAppmenu, pParentAppMenuUuid, pGroupHeadUuid);
                }
            }
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
        }
    }

    public List<string> deleteGroupAppMenu(string pAppmenuUuid, List<string> deleteAppMenuUuid)
    {
        BasicModel modBasic = new BasicModel();
        try
        {
            IList<Appmenu_Record> dt = modBasic.getAppmenu_By_ParentUuid(pAppmenuUuid);
            if (dt.Count > 0)
            {
                foreach (Appmenu_Record r in dt)
                {
                    deleteAppMenuUuid.Add(r.UUID);
                    deleteGroupAppMenu(r.UUID, deleteAppMenuUuid);
                }
            }
            return deleteAppMenuUuid;
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
            throw ex;
        }
    }
}

