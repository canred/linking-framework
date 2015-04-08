#region USING
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using ExtDirect;
using ExtDirect.Direct;
using LK.DB.SQLCreater;
using LK.Util;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using LKWebTemplate.Model.Basic;
using LKWebTemplate.Model.Basic.Table;
using LKWebTemplate.Model.Basic.Table.Record;
using LKWebTemplate;
using System.Text;
using LKWebTemplate.Util;
using System.Data;
using System.Diagnostics;
#endregion

[DirectService("AdminCompanyAction")]
public partial class AdminCompanyAction : BaseAction
{
    [DirectMethod("loadCompany", DirectAction.Store)]
    public JObject loadCompany(string pKeyword, string pIsActive, string pageNo, string limitNo, string sort, string dir, Request request)
    {
        #region Declare
        List<JObject> jobject = new List<JObject>();
        BasicModel basicModel = new BasicModel();
        CompanyAction table = new CompanyAction();
        OrderLimit orderLimit = null;
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
            /*是Store操作一下就可能含有分頁資訊。*/
            orderLimit = ExtDirect.Direct.Helper.Order.getOrderLimit(pageNo, limitNo, sort, dir);
            /*取得總資料數*/
            var totalCount = basicModel.getCompany_By_KeyWord_IsActive_Count(pKeyword, pIsActive);
            /*取得資料*/
            var data = basicModel.getCompany_By_KeyWord_IsActive(pKeyword, pIsActive, orderLimit);
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
            }
            /*權限檢查*/
            if (!checkProxy(new StackTrace().GetFrame(0)))
            {
                throw new Exception("Permission Denied!");
            };
            var data = model.getCompany_By_Uuid(pUuid);

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
    public JObject submit(string uuid, string c_name, string e_name, string id, string is_active, string week_shift, string name_zh_cn,
        string is_sync_ad_user,
        string ad_ldap,
        string ad_ldap_user,
        string ad_ldap_user_password,
        Request request)
    {
        #region Declare
        var action = SubmitAction.None;
        BasicModel model = new BasicModel();
        Company_Record record = new Company_Record();
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
            bool isSuccess = true;           
            if (uuid.Trim().Length > 0)
            {
                action = SubmitAction.Edit;
                record = model.getCompany_By_Uuid(uuid).AllRecord().First();
            }
            else
            {
                action = SubmitAction.Create;
                record.UUID = LK.Util.UID.Instance.GetUniqueID();
                record.CREATE_DATE = DateTime.Now;
            }
            record.IS_ACTIVE = is_active;
            record.ID = id;
            record.WEEK_SHIFT = Convert.ToInt32(week_shift);
            record.C_NAME = c_name;
            record.E_NAME = e_name;
            record.NAME_ZH_CN = name_zh_cn;
            record.UPDATE_DATE = DateTime.Now;
            record.IS_SYNC_AD_USER = is_sync_ad_user;
            record.AD_LDAP = ad_ldap;
            record.AD_LDAP_USER = ad_ldap_user;
            record.AD_LDAP_USER_PASSWORD = ad_ldap_user_password;
            if (action == SubmitAction.Edit)
            {
                record.gotoTable().Update(record);
            }
            else if (action == SubmitAction.Create)
            {
                /*新增的公司是沒有class欄位資訊的，所以新設定class的值為『I』*/
                record.CLASS = "I";
                //check id是否重複，重複就不新增，丟錯誤訊息
                var dbc = LK.Config.DataBase.Factory.getInfo();
                Company company = new Company(dbc);
                int repeatCount = company.Where(new LK.DB.SQLCondition(company)
                    .Equal(company.ID, id)
                    ).FetchCount();

                if (repeatCount > 0)
                {
                    isSuccess = false;
                }
                else
                {
                    record.gotoTable().Insert(record);
                    uuid = record.UUID;
                }
            }
            if (isSuccess)
            {
                System.Collections.Hashtable otherParam = new System.Collections.Hashtable();
                otherParam.Add("UUID", record.UUID);
                return ExtDirect.Direct.Helper.Message.Success.OutputJObject(otherParam);
            }
            else
            {
                return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(new Exception("識別碼重複，請重新輸入!"));
            }
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }
}

