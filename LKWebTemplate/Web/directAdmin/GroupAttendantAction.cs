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

[DirectService("GroupAttendantAction")]
public partial class GroupAttendantAction : BaseAction
{
    [DirectMethod("loadAttendantStoreInGroup", DirectAction.Store)]
    public JObject loadAttendantStoreInGroup(string pCompanyUuid, string group_head_uuid,
        string pKeyword, string pIsActive,
        string pageNo, string limitNo, string sort, string dir, Request request)
    {
        #region Declare
        List<JObject> jobject = new List<JObject>();
        BasicModel basicModel = new BasicModel();
        OrderLimit orderLimit = null;
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
            var data = basicModel.getAttendantInGroupAttendant(group_head_uuid,
                pKeyword, pIsActive, orderLimit);//basicModel.getGroupHead_By(group_head_uuid, orderLimit);
            if (data.Count > 0)
            {
                /*將List<RecordBase>變成JSON字符串*/
                jobject = JsonHelper.RecordBaseListJObject(data);
            }
            /*使用Store Std out 『Sotre物件標準輸出格式』*/
            return ExtDirect.Direct.Helper.Store.OutputJObject(jobject, data.Count);
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
            /*將Exception轉成EXT Exception JSON格式*/
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    [DirectMethod("loadAttendantStoreNotInGroup", DirectAction.Store)]
    public JObject loadAttendantStoreNotInGroup(string pCompanyUuid, string pGroupHeadUuid,
        string pKeyword, string pIsActive,
        string pageNo, string limitNo, string sort, string dir, Request request)
    {
        #region Declare
        List<JObject> jobject = new List<JObject>();
        BasicModel modBasic = new BasicModel();
        OrderLimit orderLimit = null;
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
            orderLimit = ExtDirect.Direct.Helper.Order.getOrderLimit(pageNo, limitNo, sort, dir);
            var data = modBasic.getAttendantNotInGroupAttendant(pCompanyUuid, pGroupHeadUuid, pKeyword, pIsActive, orderLimit);
            if (data.Count > 0)
            {
                jobject = JsonHelper.RecordBaseListJObject(data);
            }
            /*使用Store Std out 『Sotre物件標準輸出格式』*/
            return ExtDirect.Direct.Helper.Store.OutputJObject(jobject, data.Count);
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    [DirectMethod("addAttendnatGroupHead", DirectAction.Store)]
    public JObject addAttendnatGroupHead(string pGroupHeadUuid, string pAttedantUuid, Request request)
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
            var dtGroupAtt = modBasic.getGroupAttendantByGroupHeadUuidxAttendantUuid
                (pGroupHeadUuid, pAttedantUuid);
            if (dtGroupAtt.Count > 0)
            {
                /*將List<RecordBase>變成JSON字符串*/
                var drGroupAtt = dtGroupAtt.First();
                drGroupAtt.UPDATE_DATE = DateTime.Now;
                //drGroupAtt.UPDATE_USER = getUser().UUID;
                drGroupAtt.gotoTable().Update(drGroupAtt);
                return ExtDirect.Direct.Helper.Message.Success.OutputJObject();
            }
            else
            {
                GroupAttendant_Record drGroupAttNew = new GroupAttendant_Record();
                drGroupAttNew.UUID = LK.Util.UID.Instance.GetUniqueID();
                drGroupAttNew.CREATE_DATE = DateTime.Now;
                drGroupAttNew.CREATE_USER = getUser().UUID;
                drGroupAttNew.UPDATE_USER = getUser().UUID;
                drGroupAttNew.CREATE_DATE = DateTime.Now;
                drGroupAttNew.IS_ACTIVE = "Y";
                drGroupAttNew.GROUP_HEAD_UUID = pGroupHeadUuid;
                drGroupAttNew.ATTENDANT_UUID = pAttedantUuid;
                drGroupAttNew.gotoTable().Insert(drGroupAttNew);
            }
            return ExtDirect.Direct.Helper.Message.Success.OutputJObject();
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    [DirectMethod("destroyBy", DirectAction.Store)]
    public JObject destroyBy(string pGroupHeadUuid, string pAttedantUuid, Request request)
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
            var dtGroupAtt = modBasic.getGroupAttendantByGroupHeadUuidxAttendantUuid
                (pGroupHeadUuid, pAttedantUuid);
            if (dtGroupAtt.Count > 0)
            {
                var uuid = dtGroupAtt.First().UUID;
                var drDelete = modBasic.getGroupAttendant_By_Uuid(uuid);
                /*將List<RecordBase>變成JSON字符串*/
                drDelete.Delete(drDelete.AllRecord().First());
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
}

