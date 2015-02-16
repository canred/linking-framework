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
[DirectService("MenuAction")]
public class MenuAction : BaseAction
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="parentUuid"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [DirectMethod("loadMenuTree", DirectAction.TreeStore, MethodVisibility.Visible)]
    public JObject loadMenuTree(string parentUuid, Request request)
    {
        #region Declare
        List<JObject> jobject = new List<JObject>();
        BasicModel model = new BasicModel();
        Appmenu tblAppMenu = new Appmenu();
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
            /*取得資料*/
            var genTable = new LKWebTemplate.Model.Basic.Table.Appmenu();
            var dataTable = model.getAppmenu_By_RootUuid_DataTable(parentUuid);
            dataTable.Columns.Add("leaf");            
            dataTable.Columns.Add("name");
            dataTable.Columns.Add("checked", typeof(Boolean));

            foreach (DataRow dr in dataTable.Rows)
            {

                var children = model.getAppmenu_By_RootUuid_DataTable(dr[tblAppMenu.UUID].ToString());
                if (children.Rows.Count == 0)
                {
                    dr["leaf"] = "true";
                }
                else
                {
                    dr["leaf"] = "false";
                }
                //dr["id"] = dr[tblAppMenu.UUID].ToString();
                dr["name"] = dr[tblAppMenu.NAME_ZH_TW].ToString();
                dr["checked"] = dr["IS_ACTIVE"].ToString().ToLower() == "y" ? true : false;

            }
            //jsonStr.Append(JsonHelper.DataTableSerializer(dataTable));
            var jarray = JsonHelper.DataTableSerializerJArray(dataTable);
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

    [DirectMethod("loadMenuTree2", DirectAction.TreeStore, MethodVisibility.Visible)]
    public JObject loadMenuTree2(string parentUuid, Request request)
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
            /*取得資料*/
            var genTable = new Appmenu();
            var drsAppmenu = model.getAppmenu_By_Uuid(parentUuid).AllRecord();
            var drAppmenu = drsAppmenu.First();
            drsAppmenu = model.getAppmenu_By_ApplicationHead(drAppmenu.APPLICATION_HEAD_UUID);
            var dataTable = model.getAppmenu_By_RootUuid_DataTable(parentUuid);            
            dataTable.Columns.Add("leaf", System.Type.GetType("System.Boolean"));
            dataTable.Columns.Add("name");                        
            dataTable.Columns.Add("expanded", System.Type.GetType("System.Boolean"));            
            foreach (DataRow dr in dataTable.Rows)
            {

                var children = model.getAppmenu_By_RootUuid_DataTable(dr[genTable.UUID].ToString());
                if (children.Rows.Count == 0)
                {
                    dr["leaf"] = true;
                }
                else
                {
                    dr["leaf"] = false;
                }
                dr["name"] = dr[genTable.NAME_ZH_TW].ToString();
                dr["expanded"] = true;
            }
            var jarray = JsonHelper.DataTableSerializerJArray(dataTable);

            foreach (var item in jarray)
            {
                var thisUuid = item["UUID"].ToString();
                var thisLeaf = item["leaf"].ToString();
                if (thisLeaf.ToLower() == "false")
                {
                    item["children"] = _loadMenuTree2(thisUuid, ref drsAppmenu);
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

    [DirectMethod("_loadMenuTree2", DirectAction.TreeStore, MethodVisibility.Visible)]
    public JArray _loadMenuTree2(string parentUuid,  ref IList<Appmenu_Record> drsAppmenu)
    {
        #region Declare
        List<JObject> jobject = new List<JObject>();
        BasicModel model = new BasicModel();
        #endregion
        try
        {
            /*取得資料*/

            var dataTable = new System.Data.DataTable();
            Appmenu tbl = new Appmenu();
            dataTable.Columns.Add(tbl.ACTION_MODE);
            dataTable.Columns.Add(tbl.APPLICATION_HEAD_UUID);
            dataTable.Columns.Add(tbl.APPMENU_UUID);
            dataTable.Columns.Add(tbl.CREATE_DATE);
            dataTable.Columns.Add(tbl.CREATE_USER);
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
            dataTable.Columns.Add(tbl.PARAMETER_CLASS);
            dataTable.Columns.Add(tbl.SITEMAP_UUID);
            dataTable.Columns.Add(tbl.UPDATE_DATE);
            dataTable.Columns.Add(tbl.UPDATE_USER);
            dataTable.Columns.Add(tbl.UUID);                        
            dataTable.Columns.Add("leaf", System.Type.GetType("System.Boolean"));
            dataTable.Columns.Add("name");                        
            dataTable.Columns.Add("expanded", System.Type.GetType("System.Boolean"));
            var _drsAppmenu = drsAppmenu.Where(c => c.APPMENU_UUID.Equals(parentUuid));
            foreach (var item in _drsAppmenu)
            {
                var dr = dataTable.NewRow();
                dr[tbl.ACTION_MODE] = item.ACTION_MODE;
                dr[tbl.APPLICATION_HEAD_UUID] = item.APPLICATION_HEAD_UUID;
                dr[tbl.APPMENU_UUID] = item.APPMENU_UUID;
                dr[tbl.CREATE_DATE] = item.CREATE_DATE;
                dr[tbl.CREATE_USER] = item.CREATE_USER;                
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
                dr[tbl.PARAMETER_CLASS] = item.PARAMETER_CLASS;
                dr[tbl.SITEMAP_UUID] = item.SITEMAP_UUID;
                dr[tbl.UPDATE_DATE] = item.UPDATE_DATE;
                dr[tbl.UPDATE_USER] = item.UPDATE_USER;                
                dr[tbl.UUID] = item.UUID;
                dr["expanded"] = true;
                var childrenCount = drsAppmenu.Where(c => c.APPMENU_UUID.Equals(dr[tbl.UUID].ToString())).Count();
                if (childrenCount == 0)
                {
                    dr["leaf"] = true;
                }
                else
                {
                    dr["leaf"] = false;
                }
                dr["name"] = dr[tbl.NAME_ZH_TW].ToString();
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
                    item["children"] = _loadMenuTree2(thisUuid, ref drsAppmenu);
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="parentUuid"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [DirectMethod("loadTreeRoot", DirectAction.Store, MethodVisibility.Visible)]
    public JObject loadTreeRoot(string pApplicationHeadUuid, Request request)
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
            var data = model.getAppMenu_By_ApplicationHead(pApplicationHeadUuid, null);
            foreach (var dr in data)
            {
                if (dr.APPMENU_UUID.Trim() == "")
                {
                    return JsonHelper.RecordBaseJObject(dr);
                }
            }
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(new Exception("Data Not Found!"));
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
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
                            string name_zh_tw,
                            string name_zh_cn,
                            string name_en_us,
                            string id,
                            string appmenu_uuid,
                            string haschild,
                            string application_head_uuid,
                            string ord,
                            string parameter_class,
                            string image,
                            string sitemap_uuid,
                            string action_mode,
                            string is_default_page,
                            string is_admin,
                            HttpRequest request)
    {
        #region Declare
        var action = SubmitAction.None;
        BasicModel modBasic = new BasicModel();
        Appmenu_Record drAppMenu = new Appmenu_Record();
        string errorMsg = "update SiteMap record fail.";
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

                drAppMenu = modBasic.getAppmenu_By_Uuid(uuid).AllRecord().First();
                //判斷有沒有子項，有的話不可修改
                /*
                var canBeChange = getChildByMenuUuid(uuid, drAppMenu.APPMENU_UUID);
                if (!canBeChange)
                {
                    errorMsg = "尚有子項，無法異動節點";
                }
                else
                 */
                action = SubmitAction.Edit;
            }
            else
            {
                action = SubmitAction.Create;
                drAppMenu.UUID = LK.Util.UID.Instance.GetUniqueID();
                drAppMenu.CREATE_DATE = DateTime.Now;
                drAppMenu.CREATE_USER = getUser().UUID;
                drAppMenu.UPDATE_USER = getUser().UUID;
                drAppMenu.CREATE_DATE = DateTime.Now;
                drAppMenu.APPLICATION_HEAD_UUID = application_head_uuid;
                drAppMenu.ORD = modBasic.getAppMenu_MaxOrd(application_head_uuid) + 1;
                //新增一定沒有子項
                drAppMenu.HASCHILD = "N";
            }
            /*固定要更新的欄位*/
            drAppMenu.UPDATE_DATE = DateTime.Now;
            drAppMenu.UPDATE_USER = getUser().UUID;

            /*非固定更新的欄位*/
            drAppMenu.IS_ACTIVE = is_active;
            drAppMenu.NAME_EN_US = name_en_us;
            drAppMenu.NAME_ZH_CN = name_zh_cn;
            drAppMenu.NAME_ZH_TW = name_zh_tw;
            drAppMenu.ID = id;
            drAppMenu.APPMENU_UUID = appmenu_uuid;
            drAppMenu.PARAMETER_CLASS = parameter_class;
            drAppMenu.IMAGE = image;
            drAppMenu.SITEMAP_UUID = sitemap_uuid;
            drAppMenu.ACTION_MODE = action_mode;
            drAppMenu.IS_DEFAULT_PAGE = is_default_page;
            drAppMenu.IS_ADMIN = is_admin;
            drAppMenu.ORD = System.Convert.ToInt32(ord);

            if (action == SubmitAction.Edit)
            {
                drAppMenu.gotoTable().Update_Empty2Null(drAppMenu);
            }
            else if (action == SubmitAction.Create)
            {
                drAppMenu.gotoTable().Insert_Empty2Null(drAppMenu);
            }

            if (action == SubmitAction.Edit || action == SubmitAction.Create)
            {
                //更新父項的HASCHIIST = "Y"
                updateParentMenu(appmenu_uuid, "Y");
                System.Collections.Hashtable otherParam = new System.Collections.Hashtable();
                otherParam.Add("UUID", drAppMenu.UUID);
                return ExtDirect.Direct.Helper.Message.Success.OutputJObject(otherParam);
            }
            else
                return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(new Exception(errorMsg));
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    #region  updateParentMenu
    private bool updateParentMenu(string parent_appmenu_uuid, string haschild)
    {
        bool success = true;
        BasicModel modBasic = new BasicModel();
        Appmenu dtAppMenu = new Appmenu();
        Appmenu_Record drAppMenu = new Appmenu_Record();
        dtAppMenu = modBasic.getAppmenu_By_Uuid(parent_appmenu_uuid);
        if (dtAppMenu.AllRecord().Count > 0)
        {
            drAppMenu = dtAppMenu.AllRecord().First();
            drAppMenu.HASCHILD = haschild;
            drAppMenu.gotoTable().Update_Empty2Null(drAppMenu);
        }
        else
            success = false;
        return success;
    }
    #endregion

    #region getChildByMenuUuid
    private bool getChildByMenuUuid(string parent_appmenu_uuid, string appmenu_uuid)
    {
        bool canBeChange = true;
        bool hasChild = true;
        BasicModel modBasic = new BasicModel();
        var child = modBasic.getAppmenu_By_ParentUuid(parent_appmenu_uuid);
        var self = modBasic.getAppmenu_By_Uuid(appmenu_uuid).AllRecord().First();
        if (child.Count == 0)
            hasChild = false;

        if (self.APPMENU_UUID != appmenu_uuid && hasChild)
            canBeChange = false;

        return canBeChange;
    }
    #endregion

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pUuid"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [DirectMethod("info", DirectAction.Store, MethodVisibility.Visible)]
    public JObject info(string pUuid, Request request)
    {
        #region Declare
        BasicModel modBasic = new BasicModel();
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
            var dtAppPage = modBasic.getAppmenu_By_Uuid(pUuid);
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pUuid"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [DirectMethod("destroyByUuid", DirectAction.Store, MethodVisibility.Visible)]
    public JObject destroyByUuid(string pUuid, Request request)
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
            var dtAppPage = modBasic.getAppmenu_By_Uuid(pUuid);
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

    /// <summary>
    /// 未使用
    /// </summary>
    /// <param name="pUuid"></param>
    /// <param name="is_active"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [DirectMethod("setAppMenuIsActive", DirectAction.Store, MethodVisibility.Visible)]
    public JObject setAppMenuIsActive(string pUuid, string is_active, Request request)
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
            var dtSitemap = modBasic.getAppmenu_By_Uuid(pUuid);
            if (dtSitemap.AllRecord().Count > 0)
            {
                /*將List<RecordBase>變成JSON字符串*/
                var drSitemap = dtSitemap.AllRecord().First();
                if (is_active == "1" || is_active.ToUpper() == "TRUE" || is_active.ToUpper() == "Y")
                {
                    drSitemap.IS_ACTIVE = "Y";
                }
                else
                {
                    drSitemap.IS_ACTIVE = "N";
                }
                drSitemap.UPDATE_DATE = DateTime.Now;
                drSitemap.UPDATE_USER = getUser().UUID;

                drSitemap.gotoTable().Update(drSitemap);
                return ExtDirect.Direct.Helper.Message.Success.OutputJObject();
            }
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(new Exception("Setting Sitemap record fail."));
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pUuid"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [DirectMethod("deleteAppMenu", DirectAction.Store, MethodVisibility.Visible)]
    public JObject deleteAppMenu(string pUuid, Request request)
    {
        #region Declare
        BasicModel modBasic = new BasicModel();
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
            var dtAppmenu = modBasic.getAppmenu_By_Uuid(pUuid);
            if (dtAppmenu.AllRecord().Count > 0)
            {
                /*將List<RecordBase>變成JSON字符串*/
                var drAppmenu = dtAppmenu.AllRecord().First();
                var drsGroupAppmenu = modBasic.getGroupAppmenu_By_AppMenuUuid(drAppmenu.UUID);
                if (drsGroupAppmenu.Count > 0) {
                    foreach (var item in drsGroupAppmenu) {
                        item.gotoTable().Delete(item);
                    }
                }
                drAppmenu.gotoTable().Delete(drAppmenu);
                return ExtDirect.Direct.Helper.Message.Success.OutputJObject();
            }
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(new Exception("delete SiteMap record fail."));
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    [DirectMethod("load", DirectAction.Store, MethodVisibility.Visible)]
    public JObject load(string pApplicationHeadUuid, string pageNo, string limitNo, string sort, string dir, Request request)
    {
        #region Declare
        List<JObject> jobject = new List<JObject>();
        BasicModel modBasic = new BasicModel();
        OrderLimit orderLimit = null;
        Appmenu tblAppmenu = new Appmenu();
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
            var totalCount = modBasic.getAppMenu_By_ApplicationHead_Count(pApplicationHeadUuid);
            /*取得資料*/
            var data = modBasic.getAppMenu_By_ApplicationHead(pApplicationHeadUuid, orderLimit);
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pApplicationHeadUuid">無用的參數</param>
    /// <param name="pageNo"></param>
    /// <param name="limitNo"></param>
    /// <param name="sort"></param>
    /// <param name="dir"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [DirectMethod("loadThisApplicationMenu", DirectAction.Store, MethodVisibility.Visible)]
    public JObject loadThisApplicationMenu(string pApplicationHeadUuid, string pageNo, string limitNo, string sort, string dir, Request request)
    {
        #region Declare
        List<JObject> jobject = new List<JObject>();
        BasicModel modBasic = new BasicModel();
        OrderLimit orderLimit = null;
        Appmenu tblAppmenu = new Appmenu();
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
            string appName = LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().AppName;

            ApplicationHead tb = new ApplicationHead(LK.Config.DataBase.Factory.getInfo());
            var drs = tb.Where(new LK.DB.SQLCondition(tb).Equal(tb.NAME, appName))
                .FetchAll<ApplicationHead_Record>();
            pApplicationHeadUuid = "";
            if (drs.Count > 0)
            {
                pApplicationHeadUuid = drs.First().UUID;
            }
            /*取得總資料數*/
            var totalCount = modBasic.getAppMenu_By_ApplicationHead_Count(pApplicationHeadUuid);
            /*取得資料*/
            var data = modBasic.getAppMenu_By_ApplicationHead(pApplicationHeadUuid, orderLimit);
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
}

