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

    [DirectService("SyncClientAction")]
public class SyncClientAction : BaseAction
    {
        /// <summary>
        /// 由主伺服器同步company的資料到自身資料庫中
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [DirectMethod("SyncCompany", DirectAction.Store, MethodVisibility.Visible)]
        public JObject SyncCompany(Request request)
        {
            #region Declare
            List<JObject> jobject = new List<JObject>();
            BasicModel basicModel = new BasicModel();
            CompanyAction table = new CompanyAction();
            LK.Cloud cloud = new LK.Cloud();
            #endregion
            try
            {
                /*Cloud身份檢查*/
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
                string masterUrl = String.Empty;
                /*取得主伺服器的服務位置*/
                if (LK.Config.Cloud.CloudConfigs.GetConfig().AuthMaster.EndsWith("//"))
                {
                    masterUrl = LK.Config.Cloud.CloudConfigs.GetConfig().AuthMaster + "Router.ashx";
                }
                else {
                    masterUrl = LK.Config.Cloud.CloudConfigs.GetConfig().AuthMaster + "//Router.ashx";
                }
                /*要到主伺服器取得所有的資料,呼叫SyncServerAction.loadCompany取得所有主伺服器中的資料*/
                var cloudId = cloud.getCloudId(
                                                            masterUrl, 
                                                            LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().AppName, 
                                                            this.getUser().COMPANY_ID, 
                                                            this.getUser().ACCOUNT,
                                                            this.getUser().PASSWORD
                                                        );
                cloud.Cloud_Id = cloudId;
                var resultJson = cloud.CallDirect(masterUrl, "SyncServerAction.loadCompany", null, cloudId);
                var resultDt = cloud.ConvertToDataTable(resultJson["data"]);

                
                var selfData = basicModel.getCompany();                
                /*先新增資料到本身資料庫中*/                
                foreach (DataRow dr in resultDt.Rows) { 
                    int count = selfData.Count(c=>c.ID.ToLower().Equals(dr["id"].ToString().ToLower()));
                    #region 新增公司的部份
                    if (count==0){
                        Company_Record newRd = new Company_Record();
                        newRd.UUID = LK.Util.UID.Instance.GetUniqueID();
                        newRd.AD_LDAP = dr["AD_LDAP"].ToString();
                        newRd.AD_LDAP_USER = dr["AD_LDAP_USER"].ToString();
                        newRd.AD_LDAP_USER_PASSWORD = dr["AD_LDAP_USER_PASSWORD"].ToString();
                        newRd.C_NAME = dr["C_NAME"].ToString();
                        newRd.CLASS = dr["CLASS"].ToString();
                        newRd.CONCURRENT_USER = dr["CONCURRENT_USER"].ToString();
                        newRd.CREATE_DATE = DateTime.Now;
                        newRd.E_NAME = dr["E_NAME"].ToString();
                        if (dr["EXPIRED_DATE"].ToString() != "")
                        {
                            newRd.EXPIRED_DATE = Convert.ToDateTime(dr["EXPIRED_DATE"].ToString());
                        }
                        newRd.ID = dr["ID"].ToString();
                        newRd.IS_ACTIVE = dr["IS_ACTIVE"].ToString();
                        newRd.IS_SYNC_AD_USER = dr["IS_SYNC_AD_USER"].ToString();
                        newRd.NAME_ZH_CN = dr["NAME_ZH_CN"].ToString();
                        newRd.OU_SYNC_TYPE = dr["OU_SYNC_TYPE"].ToString();
                        newRd.SALES_ATTENDANT_UUID = dr["SALES_ATTENDANT_UUID"].ToString();
                        newRd.UPDATE_DATE = DateTime.Now;
                        newRd.VOUCHER_POINT_UUID = dr["VOUCHER_POINT_UUID"].ToString();
                        newRd.WEEK_SHIFT = Convert.ToDecimal( dr["WEEK_SHIFT"].ToString());    
                        newRd.gotoTable().Insert_Empty2Null(newRd);
                    }
                    #endregion
                }

                /*設定自身的company的is_active*/                
                if (resultDt.Rows.Count > 0)
                {
                    foreach (var item in selfData)
                    {                        
                        var tmpDr = resultDt.Select("ID='" + item.ID + "'");
                        if (tmpDr[0]["IS_ACTIVE"].ToString().ToUpper() == item.IS_ACTIVE.ToUpper())
                        {}
                        else {
                            if (tmpDr.Count() == 0){                            
                                item.IS_ACTIVE = "N";
                                item.gotoTable().Update_Empty2Null(item);
                                continue;
                            }
                        }
                        #region 按key值抓出資料，並以主伺服器資料為主更新自身的資料內容
                        
                        item.AD_LDAP = tmpDr[0]["AD_LDAP"].ToString();
                        item.AD_LDAP_USER = tmpDr[0]["AD_LDAP_USER"].ToString();
                        item.AD_LDAP_USER_PASSWORD = tmpDr[0]["AD_LDAP_USER_PASSWORD"].ToString();
                        item.C_NAME = tmpDr[0]["C_NAME"].ToString();
                        item.CLASS = tmpDr[0]["CLASS"].ToString();
                        item.CONCURRENT_USER = tmpDr[0]["CONCURRENT_USER"].ToString();
                        item.CREATE_DATE = DateTime.Now;
                        item.E_NAME = tmpDr[0]["E_NAME"].ToString();
                        if (tmpDr[0]["EXPIRED_DATE"].ToString() != "")
                        {
                            item.EXPIRED_DATE = Convert.ToDateTime(tmpDr[0]["EXPIRED_DATE"].ToString());
                        }
                        item.ID = tmpDr[0]["ID"].ToString();
                        item.IS_ACTIVE = tmpDr[0]["IS_ACTIVE"].ToString();
                        item.IS_SYNC_AD_USER = tmpDr[0]["IS_SYNC_AD_USER"].ToString();
                        item.NAME_ZH_CN = tmpDr[0]["NAME_ZH_CN"].ToString();
                        item.OU_SYNC_TYPE = tmpDr[0]["OU_SYNC_TYPE"].ToString();
                        item.SALES_ATTENDANT_UUID = tmpDr[0]["SALES_ATTENDANT_UUID"].ToString();
                        item.UPDATE_DATE = DateTime.Now;
                        item.VOUCHER_POINT_UUID = tmpDr[0]["VOUCHER_POINT_UUID"].ToString();
                        item.WEEK_SHIFT = Convert.ToDecimal(tmpDr[0]["WEEK_SHIFT"].ToString());    
                        item.gotoTable().Update_Empty2Null(item);
                        #endregion
                    }
                }




                

                return ExtDirect.Direct.Helper.Message.Success.OutputJObject();
            }
            catch (Exception ex)
            {
                log.Error(ex); LK.MyException.MyException.Error(this, ex);
                /*將Exception轉成EXT Exception JSON格式*/
                return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
            }
        }

        /// <summary>
        /// 由主伺服器同步attendant的資料到自身資料庫中
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [DirectMethod("SyncAttendant", DirectAction.Store, MethodVisibility.Visible)]
        public JObject SyncAttendant(Request request)
        {
            #region Declare
            List<JObject> jobject = new List<JObject>();
            BasicModel basicModel = new BasicModel();
            CompanyAction table = new CompanyAction();
            LK.Cloud cloud = new LK.Cloud();
            #endregion
            try
            {
                /*Cloud身份檢查*/
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
                string masterUrl = String.Empty;
                /*取得主伺服器的服務位置*/
                if (LK.Config.Cloud.CloudConfigs.GetConfig().AuthMaster.EndsWith("//"))
                {
                    masterUrl =  LK.Config.Cloud.CloudConfigs.GetConfig().AuthMaster + "Router.ashx";
                }
                else
                {
                    masterUrl =  LK.Config.Cloud.CloudConfigs.GetConfig().AuthMaster + "//Router.ashx";
                }
                /*要到主伺服器取得所有的資料,呼叫SyncServerAction.loadCompany取得所有主伺服器中的資料*/
                var cloudId = cloud.getCloudId(
                                                            masterUrl,
                                                            LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().AppName,
                                                            this.getUser().COMPANY_ID,
                                                            this.getUser().ACCOUNT,
                                                            this.getUser().PASSWORD
                                                        );
                cloud.Cloud_Id = cloudId;
                var resultCompanyJson = cloud.CallDirect(masterUrl, "SyncServerAction.loadCompany", null, cloudId);
                var resultAttendantJson = cloud.CallDirect(masterUrl, "SyncServerAction.loadAttendant", null, cloudId);

                var resultCompanyDt = cloud.ConvertToDataTable(resultCompanyJson["data"]);
                var resultAttendantDt = cloud.ConvertToDataTable(resultAttendantJson["data"]);


                var selfCompanyData = basicModel.getCompany();
                
                foreach (DataRow drCp in resultCompanyDt.Rows) {
                    
                    if( selfCompanyData.Count(c => c.ID.ToUpper().Equals(drCp["id"].ToString().ToUpper())) == 0)
                        continue;

                    string comapnyUuid = selfCompanyData.Where(c => c.ID.ToUpper().Equals(drCp["id"].ToString().ToUpper())).First().UUID;

                    var selfAttendantData = basicModel.getAttendant_By_CompanyUuid(comapnyUuid);

                    /*先新增資料到本身資料庫中*/
                    foreach (DataRow dr in resultAttendantDt.Select("COMPANY_UUID='"+drCp["UUID"].ToString()+"'"))
                    {
                        int count = selfAttendantData.Count(c => c.ACCOUNT.ToLower().Equals(dr["account"].ToString().ToLower()) && c.COMPANY_UUID.ToLower().Equals(comapnyUuid.ToLower()));
                        #region 新增人員的部份
                        if (count == 0)
                        {
                            Attendant_Record newRd = new Attendant_Record();
                            newRd.UUID = LK.Util.UID.Instance.GetUniqueID();

                            newRd.ACCOUNT = dr["ACCOUNT"].ToString();
                            if (dr["BIRTHDAY"].ToString().Length > 0)
                            {
                                newRd.BIRTHDAY = Convert.ToDateTime( dr["BIRTHDAY"].ToString());
                            }
                            newRd.C_NAME = dr["C_NAME"].ToString();
                            newRd.CODE_PAGE = dr["CODE_PAGE"].ToString();
                            newRd.COMPANY_UUID = comapnyUuid;
                            if (dr["CREATE_DATE"].ToString().Length > 0)
                            {
                                newRd.CREATE_DATE = Convert.ToDateTime( dr["CREATE_DATE"].ToString());
                            }
                            newRd.DEPARTMENT_UUID = dr["DEPARTMENT_UUID"].ToString();
                            newRd.E_NAME = dr["E_NAME"].ToString();
                            newRd.EMAIL = dr["EMAIL"].ToString();
                            newRd.GENDER = dr["GENDER"].ToString();
                            newRd.GRADE = dr["GRADE"].ToString();
                            if (dr["HIRE_DATE"].ToString().Length > 0)
                            {
                                newRd.HIRE_DATE = Convert.ToDateTime( dr["HIRE_DATE"].ToString());
                            }
                            newRd.ID = dr["ID"].ToString();
                            newRd.IS_ACTIVE = dr["IS_ACTIVE"].ToString();
                            newRd.IS_ADMIN = dr["IS_ADMIN"].ToString();
                            newRd.IS_DEFAULT_PASS = dr["IS_DEFAULT_PASS"].ToString();
                            newRd.IS_DIRECT = dr["IS_DIRECT"].ToString();
                            newRd.IS_MANAGER = dr["IS_MANAGER"].ToString();
                            newRd.IS_SUPPER = dr["IS_SUPPER"].ToString();
                            newRd.PASSWORD = dr["PASSWORD"].ToString();
                            newRd.PHONE = dr["PHONE"].ToString();
                            if (dr["QUIT_DATE"].ToString().Length > 0)
                            {
                                newRd.QUIT_DATE = Convert.ToDateTime( dr["QUIT_DATE"].ToString());
                            }
                            newRd.SITE_UUID = dr["SITE_UUID"].ToString();
                            newRd.SRC_UUID = dr["SRC_UUID"].ToString();
                            if (dr["UPDATE_DATE"].ToString().Length > 0)
                            {
                                newRd.UPDATE_DATE = Convert.ToDateTime( dr["UPDATE_DATE"].ToString());
                            }
                            newRd.gotoTable().Insert_Empty2Null(newRd);
                        }
                        #endregion
                    }

                    /*設定自身的company的is_active*/
                    if (resultAttendantDt.Rows.Count > 0)
                    {
                        foreach (var item in selfAttendantData)
                        {
                            var tmpDr = resultAttendantDt.Select("ACCOUNT='" + item.ACCOUNT + "' AND COMPANY_UUID='" + drCp["UUID"].ToString()+ "'");
                            if (tmpDr[0]["IS_ACTIVE"].ToString().ToUpper() == item.IS_ACTIVE.ToUpper())
                            { }
                            else
                            {
                                if (tmpDr.Count() == 0)
                                {
                                    item.IS_ACTIVE = "N";
                                    item.gotoTable().Update_Empty2Null(item);
                                    continue;
                                }
                            }
                            #region 按key值抓出資料，並以主伺服器資料為主更新自身的資料內容

                            //item.AD_LDAP = tmpDr[0]["AD_LDAP"].ToString();

                            item.ACCOUNT = tmpDr[0]["ACCOUNT"].ToString();
                            if (tmpDr[0]["BIRTHDAY"].ToString().Length > 0)
                            {
                                item.BIRTHDAY = Convert.ToDateTime(tmpDr[0]["BIRTHDAY"].ToString());
                            }
                            item.C_NAME = tmpDr[0]["C_NAME"].ToString();
                            item.CODE_PAGE = tmpDr[0]["CODE_PAGE"].ToString();
                            item.COMPANY_UUID = comapnyUuid;
                            if (tmpDr[0]["CREATE_DATE"].ToString().Length > 0)
                            {
                                item.CREATE_DATE = Convert.ToDateTime(tmpDr[0]["CREATE_DATE"].ToString());
                            }
                            item.DEPARTMENT_UUID = tmpDr[0]["DEPARTMENT_UUID"].ToString();
                            item.E_NAME = tmpDr[0]["E_NAME"].ToString();
                            item.EMAIL = tmpDr[0]["EMAIL"].ToString();
                            item.GENDER = tmpDr[0]["GENDER"].ToString();
                            item.GRADE = tmpDr[0]["GRADE"].ToString();
                            if (tmpDr[0]["HIRE_DATE"].ToString().Length > 0)
                            {
                                item.HIRE_DATE = Convert.ToDateTime(tmpDr[0]["HIRE_DATE"].ToString());
                            }
                            item.ID = tmpDr[0]["ID"].ToString();
                            item.IS_ACTIVE = tmpDr[0]["IS_ACTIVE"].ToString();
                            item.IS_ADMIN = tmpDr[0]["IS_ADMIN"].ToString();
                            item.IS_DEFAULT_PASS = tmpDr[0]["IS_DEFAULT_PASS"].ToString();
                            item.IS_DIRECT = tmpDr[0]["IS_DIRECT"].ToString();
                            item.IS_MANAGER = tmpDr[0]["IS_MANAGER"].ToString();
                            item.IS_SUPPER = tmpDr[0]["IS_SUPPER"].ToString();
                            item.PASSWORD = tmpDr[0]["PASSWORD"].ToString();
                            item.PHONE = tmpDr[0]["PHONE"].ToString();
                            if (tmpDr[0]["QUIT_DATE"].ToString().Length > 0)
                            {
                                item.QUIT_DATE = Convert.ToDateTime(tmpDr[0]["QUIT_DATE"].ToString());
                            }
                            item.SITE_UUID = tmpDr[0]["SITE_UUID"].ToString();
                            item.SRC_UUID = tmpDr[0]["SRC_UUID"].ToString();
                            if (tmpDr[0]["UPDATE_DATE"].ToString().Length > 0)
                            {
                                item.UPDATE_DATE = Convert.ToDateTime(tmpDr[0]["UPDATE_DATE"].ToString());
                            }

                            item.gotoTable().Update_Empty2Null(item);
                            #endregion
                        }
        
                    
                }
               
                }






                return ExtDirect.Direct.Helper.Message.Success.OutputJObject();
            }
            catch (Exception ex)
            {
                log.Error(ex); LK.MyException.MyException.Error(this, ex);
                /*將Exception轉成EXT Exception JSON格式*/
                return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
            }
        }

        /// <summary>
        /// 由主伺服器同步dept的資料到自身資料庫中
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [DirectMethod("SyncDept", DirectAction.Store, MethodVisibility.Visible)]
        public JObject SyncDept(Request request)
        {
            #region Declare
            List<JObject> jobject = new List<JObject>();
            BasicModel basicModel = new BasicModel();
            CompanyAction table = new CompanyAction();
            LK.Cloud cloud = new LK.Cloud();
            #endregion
            try
            {
                /*Cloud身份檢查*/
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
                string masterUrl = String.Empty;
                /*取得主伺服器的服務位置*/
                if (LK.Config.Cloud.CloudConfigs.GetConfig().AuthMaster.EndsWith("//"))
                {
                    masterUrl = LK.Config.Cloud.CloudConfigs.GetConfig().AuthMaster + "Router.ashx";
                }
                else
                {
                    masterUrl =  LK.Config.Cloud.CloudConfigs.GetConfig().AuthMaster + "//Router.ashx";
                }
                /*要到主伺服器取得所有的資料,呼叫SyncServerAction.loadCompany取得所有主伺服器中的資料*/
                var cloudId = cloud.getCloudId(
                                                            masterUrl,
                                                            LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().AppName,
                                                            this.getUser().COMPANY_ID,
                                                            this.getUser().ACCOUNT,
                                                            this.getUser().PASSWORD
                                                        );
                cloud.Cloud_Id = cloudId;
                var resultCompanyJson = cloud.CallDirect(masterUrl, "SyncServerAction.loadCompany", null, cloudId);
                var resultAttendantJson = cloud.CallDirect(masterUrl, "SyncServerAction.loadDept", null, cloudId);

                var resultCompanyDt = cloud.ConvertToDataTable(resultCompanyJson["data"]);
                var resultAttendantDt = cloud.ConvertToDataTable(resultAttendantJson["data"]);


                var selfCompanyData = basicModel.getCompany();

                foreach (DataRow drCp in resultCompanyDt.Rows)
                {

                    if (selfCompanyData.Count(c => c.ID.ToUpper().Equals(drCp["id"].ToString().ToUpper())) == 0)
                        continue;

                    string comapnyUuid = selfCompanyData.Where(c => c.ID.ToUpper().Equals(drCp["id"].ToString().ToUpper())).First().UUID;

                    var selfDepartmentData = basicModel.getDepartment_By_CompanyUuid(comapnyUuid);

                    /*先新增資料到本身資料庫中*/
                    foreach (DataRow dr in resultAttendantDt.Select("COMPANY_UUID='" + drCp["UUID"].ToString() + "'"))
                    {
                        int count = selfDepartmentData.Count(c => c.ID.ToLower().Equals(dr["id"].ToString().ToLower()) && c.COMPANY_UUID.ToLower().Equals(comapnyUuid.ToLower()));
                        #region 新增部門的部份
                        if (count == 0)
                        {
                            Department_Record newRd = new Department_Record();
                            newRd.UUID = LK.Util.UID.Instance.GetUniqueID();

                            newRd.C_NAME = dr["C_NAME"].ToString();
                            newRd.COMPANY_UUID = comapnyUuid;
                            newRd.COST_CENTER = dr["COST_CENTER"].ToString();
                            if (dr["CREATE_DATE"].ToString().Length > 0)
                            {
                                newRd.CREATE_DATE =Convert.ToDateTime( dr["CREATE_DATE"].ToString());
                            }
                            newRd.E_NAME = dr["E_NAME"].ToString();
                            newRd.FULL_DEPARTMENT_NAME = dr["FULL_DEPARTMENT_NAME"].ToString();
                            newRd.ID = dr["ID"].ToString();
                            newRd.IS_ACTIVE = dr["IS_ACTIVE"].ToString();
                            newRd.MANAGER_ID = dr["MANAGER_ID"].ToString();
                            newRd.MANAGER_UUID = dr["MANAGER_UUID"].ToString();
                            newRd.PARENT_DEPARTMENT_ID = dr["PARENT_DEPARTMENT_ID"].ToString();
                            newRd.PARENT_DEPARTMENT_UUID = dr["PARENT_DEPARTMENT_UUID"].ToString();
                            newRd.PARENT_DEPARTMENT_UUID_LIST = dr["PARENT_DEPARTMENT_UUID_LIST"].ToString();
                            newRd.S_NAME = dr["S_NAME"].ToString();
                            newRd.SRC_UUID = dr["SRC_UUID"].ToString();
                            if (dr["UPDATE_DATE"].ToString().Length > 0)
                            {
                                newRd.UPDATE_DATE =Convert.ToDateTime( dr["UPDATE_DATE"].ToString());
                            }
                            newRd.gotoTable().Insert_Empty2Null(newRd);
                        }
                        #endregion
                    }

                    /*設定自身的company的is_active*/
                    if (resultAttendantDt.Rows.Count > 0)
                    {
                        foreach (var item in selfDepartmentData)
                        {
                            var tmpDr = resultAttendantDt.Select("ID='" + item.ID + "' AND COMPANY_UUID='" + drCp["UUID"].ToString() + "'");
                            if (tmpDr[0]["IS_ACTIVE"].ToString().ToUpper() == item.IS_ACTIVE.ToUpper())
                            { }
                            else
                            {
                                if (tmpDr.Count() == 0)
                                {
                                    item.IS_ACTIVE = "N";
                                    item.gotoTable().Update_Empty2Null(item);
                                    continue;
                                }
                            }
                            #region 按key值抓出資料，並以主伺服器資料為主更新自身的資料內容

                            //item.AD_LDAP = tmpDr[0]["AD_LDAP"].ToString();


                            item.C_NAME = tmpDr[0]["C_NAME"].ToString();
                            item.COMPANY_UUID = comapnyUuid;
                            item.COST_CENTER = tmpDr[0]["COST_CENTER"].ToString();
                            if (tmpDr[0]["CREATE_DATE"].ToString().Length > 0)
                            {
                                item.CREATE_DATE = Convert.ToDateTime(tmpDr[0]["CREATE_DATE"].ToString());
                            }
                            item.E_NAME = tmpDr[0]["E_NAME"].ToString();
                            item.FULL_DEPARTMENT_NAME = tmpDr[0]["FULL_DEPARTMENT_NAME"].ToString();
                            item.ID = tmpDr[0]["ID"].ToString();
                            item.IS_ACTIVE = tmpDr[0]["IS_ACTIVE"].ToString();
                            item.MANAGER_ID = tmpDr[0]["MANAGER_ID"].ToString();
                            item.MANAGER_UUID = tmpDr[0]["MANAGER_UUID"].ToString();
                            item.PARENT_DEPARTMENT_ID = tmpDr[0]["PARENT_DEPARTMENT_ID"].ToString();
                            item.PARENT_DEPARTMENT_UUID = tmpDr[0]["PARENT_DEPARTMENT_UUID"].ToString();
                            item.PARENT_DEPARTMENT_UUID_LIST = tmpDr[0]["PARENT_DEPARTMENT_UUID_LIST"].ToString();
                            item.S_NAME = tmpDr[0]["S_NAME"].ToString();
                            item.SRC_UUID = tmpDr[0]["SRC_UUID"].ToString();
                            if (tmpDr[0]["UPDATE_DATE"].ToString().Length > 0)
                            {
                                item.UPDATE_DATE = Convert.ToDateTime(tmpDr[0]["UPDATE_DATE"].ToString());
                            }

                            item.gotoTable().Update_Empty2Null(item);
                            #endregion
                        }


                    }

                }






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

    
  
