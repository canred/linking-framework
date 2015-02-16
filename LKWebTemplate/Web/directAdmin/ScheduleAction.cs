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
using System.Globalization;

#endregion
[DirectService("ScheduleAction")]
public class ScheduleAction : BaseAction
{


    [DirectMethod("loadSchedule", DirectAction.Store, MethodVisibility.Visible)]
    public JObject loadSchedule(string keyword, string pageNo, string limitNo, string sort, string dir, Request request)
    {
        #region Declare
        List<JObject> jobject = new List<JObject>();
        BasicModel model = new BasicModel();
        AttendantV_Record table = new AttendantV_Record();
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
            /*取得總資料數*/

            var totalCount = model.getSchedule_By_Keyword_Count(keyword);
            /*取得資料*/
            var data = model.getSchedule_By_Keyword(keyword, orderLimit);
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

    [DirectMethod("infoSchedule", DirectAction.Load, MethodVisibility.Visible)]
    public JObject infoSchedule(string pUuid, Request request)
    {
        #region Declare
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
            var data = model.getSchedule_By_Uuid(pUuid);

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

    [DirectMethod("deleteSchedule", DirectAction.Load, MethodVisibility.Visible)]
    public JObject deleteSchedule(string pUuid, Request request)
    {
        #region Declare
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
            var drSchedule = model.getSchedule_By_Uuid(pUuid);
            var drScheduleTime = drSchedule.Link_ScheduleTime_By_ScheduleUuid();

            foreach (var delItem in drScheduleTime)
            {
                delItem.gotoTable().Delete(delItem);
            }

            foreach (var delItem in drSchedule.AllRecord())
            {
                delItem.gotoTable().Delete(delItem);
            }

            return ExtDirect.Direct.Helper.Message.Success.OutputJObject();

        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    [DirectMethod("submitSchedule", DirectAction.FormSubmission, MethodVisibility.Visible)]
    public JObject submitSchedule(string uuid,
string schedule_name,
string schedule_end_date,
string last_run_time,
string last_run_status,
string is_cycle,
string single_date,
string hour,
string minute,
string cycle_type,
string c_minute,
string c_hour,
string c_day,
string c_week,
string c_day_of_week,
string c_month,
string c_day_of_month,
string c_week_of_month,
string c_year,
string c_week_of_year,
string run_url,
string run_url_parameter,
string run_attendant_uuid,
string is_active,
        string run_security,
        string start_date,
        HttpRequest request)
    {


        #region Declare
        var action = SubmitAction.None;
        BasicModel model = new BasicModel();
        Schedule_Record record = new Schedule_Record();
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
                action = SubmitAction.Edit;
                record = model.getSchedule_By_Uuid(uuid).AllRecord().First();

            }
            else
            {
                action = SubmitAction.Create;
                record.UUID = LK.Util.UID.Instance.GetUniqueID();
            }


            record.SCHEDULE_NAME = schedule_name;
            record.SCHEDULE_END_DATE = Convert.ToDateTime(schedule_end_date);
            //record.LAST_RUN_TIME = last_run_time;
            //record.LAST_RUN_STATUS = last_run_status;
            record.IS_CYCLE = is_cycle;
            if (is_cycle == "N")
            {
                record.SINGLE_DATE = Convert.ToDateTime(single_date + " " + request["cmbSingleHour"].ToString() + ":" + request["cmbSingleMinute"].ToString());
            }
            else
            {
                record.SINGLE_DATE = null;
            }

            record.START_DATE = Convert.ToDateTime(start_date);
            record.RUN_SECURITY = run_security;
            record.HOUR = hour;
            record.MINUTE = minute;
            record.CYCLE_TYPE = cycle_type;
            if (c_minute.Length > 0)
            {
                record.C_MINUTE = Convert.ToInt16(c_minute);
            }
            else
            {
                record.C_MINUTE = null;
            }

            if (c_hour.Length > 0)
            {
                record.C_HOUR = Convert.ToInt16(c_hour);
            }
            else
            {
                record.C_HOUR = null;
            }
            if (c_day.Length > 0)
            {
                record.C_DAY = Convert.ToInt16(c_day);
            }
            else
            {
                record.C_DAY = null;
            }
            if (c_week.Length > 0)
            {
                record.C_WEEK = Convert.ToInt16(c_week);
            }
            else
            {
                record.C_WEEK = null;
            }
            if (c_day_of_week.Length > 0)
            {
                record.C_DAY_OF_WEEK = c_day_of_week;
            }
            else
            {
                record.C_DAY_OF_WEEK = null;
            }
            if (c_month.Length > 0)
            {
                record.C_MONTH = Convert.ToInt16(c_month);
            }
            else
            {
                record.C_MONTH = null;
            }

            if (c_day_of_month.Length > 0)
            {
                record.C_DAY_OF_MONTH = c_day_of_month;
            }
            else
            {
                record.C_DAY_OF_MONTH = null;
            }
            if (c_week_of_month.Length > 0)
            {
                record.C_WEEK_OF_MONTH = c_week_of_month;
            }
            else
            {
                record.C_WEEK_OF_MONTH = null;
            }
            if (c_year != null && c_year.Length > 0)
            {
                record.C_YEAR = Convert.ToInt16(c_year);
            }
            else
            {
                record.C_YEAR = null;
            }
            record.C_WEEK_OF_YEAR = c_week_of_year;
            record.RUN_URL = run_url;
            record.RUN_URL_PARAMETER = run_url_parameter;
            record.RUN_ATTENDANT_UUID = run_attendant_uuid;
            record.IS_ACTIVE = is_active;

            if (action == SubmitAction.Edit)
            {
                record.gotoTable().Update_Empty2Null(record);
            }
            else if (action == SubmitAction.Create)
            {
                record.gotoTable().Insert(record);
                uuid = record.UUID;
            }



            System.Collections.Hashtable otherParam = new System.Collections.Hashtable();
            otherParam.Add("UUID", record.UUID);

            expentToScheduleTime(record.UUID, false);

            return ExtDirect.Direct.Helper.Message.Success.OutputJObject(otherParam);
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    public void expentToScheduleTime(string scheduleUuid, bool isContinue)
    {
        BasicModel model = new BasicModel();
        Schedule_Record record = new Schedule_Record();
        try
        {
            var drSchedule = model.getSchedule_By_Uuid(scheduleUuid).AllRecord().First();
            DateTime? startDate = drSchedule.START_DATE;
            DateTime? endDate = drSchedule.SCHEDULE_END_DATE;
            var cMinute = drSchedule.C_MINUTE;
            var cHour = drSchedule.C_HOUR;
            var cDay = drSchedule.C_DAY;
            var cWeek = drSchedule.C_WEEK;
            var cMonth = drSchedule.C_MONTH;
            var cDayOfWeek = drSchedule.C_DAY_OF_WEEK;
            var cDayOfMonth = drSchedule.C_DAY_OF_MONTH;
            var cWeekOfMonth = drSchedule.C_WEEK_OF_MONTH;
            ScheduleTime_Record st = new ScheduleTime_Record();
            if (drSchedule.IS_CYCLE == "Y")
            {
                var oldData = model.getScheduleTime_By_ScheduleUuid(drSchedule.UUID);
                /*把原來有的排程作一個刪除標記*/
                foreach (var old in oldData)
                {
                    if (old.START_TIME > DateTime.Now)
                    {
                        old.STATUS = "DEL";
                        old.gotoTable().Update_Empty2Null(old);
                    }
                }

                /*週期任務*/
                if (drSchedule.CYCLE_TYPE == "每分重複")
                {
                    #region 每分重複

                    if (isContinue == true)
                    {
                        startDate = drSchedule.CONTIUNE_DATETIME.Value.AddMinutes(Convert.ToInt16(cMinute));
                    }
                    else
                    {
                        startDate = startDate.Value.AddMinutes(Convert.ToInt16(cMinute));
                    }

                    ScheduleTime stTable = new ScheduleTime();

                    DateTime? genData = null;
                    while (endDate > startDate)
                    {
                        if (startDate < DateTime.Now)
                        {
                            startDate = startDate.Value.AddMinutes(Convert.ToInt16(cMinute));
                            continue;
                        }
                        if (genData == null)
                        {
                            genData = new DateTime(startDate.Value.Year, startDate.Value.Month, startDate.Value.Day);
                        }
                        st = new ScheduleTime_Record();
                        st.UUID = LK.Util.UID.Instance.GetUniqueID();
                        st.SCHEDULE_UUID = drSchedule.UUID;
                        st.STATUS = "READY";
                        st.START_TIME = startDate;
                        //st.gotoTable().Insert_Empty2Null(st);
                        stTable.AllRecord().Add(st.Clone());
                        startDate = startDate.Value.AddMinutes(Convert.ToInt16(cMinute));

                        if (genData.Value.Day != startDate.Value.Day)
                        {
                            drSchedule.EXPEND_ALL = "N";
                            drSchedule.CONTIUNE_DATETIME = startDate;
                            drSchedule.gotoTable().Update_Empty2Null(drSchedule);

                            st = new ScheduleTime_Record();
                            st.UUID = LK.Util.UID.Instance.GetUniqueID();
                            st.SCHEDULE_UUID = drSchedule.UUID;
                            st.STATUS = "READY";
                            st.START_TIME = startDate;
                            st.gotoTable().Insert_Empty2Null(st);
                            break;
                        }


                    }
                    stTable.InsertAllRecord();
                    #endregion
                }

                if (drSchedule.CYCLE_TYPE == "每天重複")
                {
                    #region 每天重複
                    if (isContinue == true)
                    {
                        startDate = drSchedule.CONTIUNE_DATETIME.Value.AddHours(Convert.ToInt16(drSchedule.HOUR)).AddMinutes(Convert.ToInt16(drSchedule.MINUTE));
                    }
                    else
                    {
                        startDate = startDate.Value.AddHours(Convert.ToInt16(drSchedule.HOUR)).AddMinutes(Convert.ToInt16(drSchedule.MINUTE));
                    }

                    DateTime? genData = null;
                    while (endDate > startDate)
                    {
                        if (startDate < DateTime.Now)
                        {
                            startDate = startDate.Value.AddDays(Convert.ToInt16(cDay));
                            continue;
                        }
                        if (genData == null)
                        {
                            genData = new DateTime(startDate.Value.Year, startDate.Value.Month, startDate.Value.Day);
                        }

                        st.UUID = LK.Util.UID.Instance.GetUniqueID();
                        st.SCHEDULE_UUID = drSchedule.UUID;
                        st.STATUS = "READY";
                        st.START_TIME = startDate;
                        st.gotoTable().Insert_Empty2Null(st);
                        startDate = startDate.Value.AddDays(Convert.ToInt16(drSchedule.C_DAY));

                        if (genData.Value.Month != startDate.Value.Month)
                        {
                            drSchedule.EXPEND_ALL = "N";
                            drSchedule.CONTIUNE_DATETIME = startDate;
                            drSchedule.gotoTable().Update_Empty2Null(drSchedule);

                            st = new ScheduleTime_Record();
                            st.UUID = LK.Util.UID.Instance.GetUniqueID();
                            st.SCHEDULE_UUID = drSchedule.UUID;
                            st.STATUS = "READY";
                            st.START_TIME = startDate;
                            st.gotoTable().Insert_Empty2Null(st);
                            break;
                        }

                    }
                    #endregion
                }

                if (drSchedule.CYCLE_TYPE == "每月重複")
                {
                    #region 每月重複

                    if (drSchedule.C_DAY_OF_MONTH.Trim().Length > 0)
                    {
                        #region 每月重複by日

                        var dayOfMonth = cDayOfMonth.Split(',');
                        var oldStart = drSchedule.START_DATE;
                        while (endDate > drSchedule.START_DATE)
                        {
                            foreach (var day in dayOfMonth)
                            {
                                var strStartDate = drSchedule.START_DATE.Value.Year.ToString() + "/" + drSchedule.START_DATE.Value.Month.ToString() + "/1";

                                startDate = Convert.ToDateTime(strStartDate).AddDays(Convert.ToInt32(day) - 1).AddHours(Convert.ToInt32(drSchedule.HOUR)).AddMinutes(Convert.ToInt32(drSchedule.MINUTE));

                                st.UUID = LK.Util.UID.Instance.GetUniqueID();
                                st.SCHEDULE_UUID = drSchedule.UUID;
                                st.STATUS = "READY";
                                st.START_TIME = startDate;
                                st.gotoTable().Insert_Empty2Null(st);
                            }
                            drSchedule.START_DATE = drSchedule.START_DATE.Value.AddMonths(Convert.ToInt32(cMonth));
                        }
                        drSchedule.START_DATE = oldStart;

                        #endregion
                    }
                    else if (drSchedule.C_WEEK_OF_MONTH.Length > 0)
                    {
                        #region 每月重複by週
                        var weekOfMonth = cWeekOfMonth.Split(',');
                        var dayOfWeek = cDayOfWeek.Split(',');
                        var oldStart = drSchedule.START_DATE;

                        //getFirstDayInMonth(oldStart.Value.Year, oldStart.Value.Month, 1);

                        while (endDate > drSchedule.START_DATE)
                        {
                            foreach (var week in weekOfMonth)
                            {
                                foreach (var day in dayOfWeek)
                                {
                                    var dayInWeek = getFirstDayInMonth(oldStart.Value.Year, oldStart.Value.Month, Convert.ToInt16(week));
                                    st = new ScheduleTime_Record();
                                    st.UUID = LK.Util.UID.Instance.GetUniqueID();
                                    st.SCHEDULE_UUID = drSchedule.UUID;
                                    st.STATUS = "READY";

                                    if (dayInWeek == null)
                                    {
                                        continue;
                                    }

                                    st.START_TIME = dayInWeek[Convert.ToInt16(day)].AddHours(Convert.ToInt16(drSchedule.HOUR)).AddMinutes(Convert.ToInt16(drSchedule.MINUTE));
                                    st.gotoTable().Insert_Empty2Null(st);
                                }
                            }
                            oldStart = oldStart.Value.AddMonths(Convert.ToInt32(cMonth));
                            drSchedule.START_DATE = drSchedule.START_DATE.Value.AddMonths(Convert.ToInt32(cMonth));
                        }
                        drSchedule.START_DATE = oldStart;
                        #endregion
                    }

                    drSchedule.EXPEND_ALL = "Y";
                    drSchedule.gotoTable().Update_Empty2Null(drSchedule);
                    #endregion
                }

                if (drSchedule.CYCLE_TYPE == "每時重複")
                {
                    #region 每天重複

                    if (isContinue == true)
                    {
                        startDate = drSchedule.CONTIUNE_DATETIME;
                    }
                    else
                    {
                        startDate = startDate.Value.AddHours(Convert.ToInt16(cHour)).AddMinutes(Convert.ToInt16(drSchedule.MINUTE));
                    }

                    DateTime? genData = null;
                    while (endDate > startDate)
                    {
                        if (startDate < DateTime.Now)
                        {
                            startDate = startDate.Value.AddHours(Convert.ToInt16(cHour));
                            continue;
                        }
                        if (genData == null)
                        {
                            genData = new DateTime(startDate.Value.Year, startDate.Value.Month, startDate.Value.Day);
                        }

                        st.UUID = LK.Util.UID.Instance.GetUniqueID();
                        st.SCHEDULE_UUID = drSchedule.UUID;
                        st.STATUS = "READY";
                        st.START_TIME = startDate;
                        st.gotoTable().Insert_Empty2Null(st);
                        startDate = startDate.Value.AddHours(Convert.ToInt16(cHour));

                        if (genData.Value.Day != startDate.Value.Day)
                        {
                            drSchedule.EXPEND_ALL = "N";
                            drSchedule.CONTIUNE_DATETIME = startDate;
                            drSchedule.gotoTable().Update_Empty2Null(drSchedule);

                            st.UUID = LK.Util.UID.Instance.GetUniqueID();
                            st.SCHEDULE_UUID = drSchedule.UUID;
                            st.STATUS = "READY";
                            st.START_TIME = startDate;
                            st.gotoTable().Insert_Empty2Null(st);
                            startDate = startDate.Value.AddHours(Convert.ToInt16(cHour));

                            break;
                        }

                    }
                    #endregion
                }

                if (drSchedule.CYCLE_TYPE == "每週重複")
                {
                    #region 每週重複
                    startDate = startDate.Value.AddHours(Convert.ToInt16(drSchedule.HOUR)).AddMinutes(Convert.ToInt16(drSchedule.MINUTE));
                    var dayOfWeek = cDayOfWeek.Split(',');

                    while (endDate > startDate)
                    {
                        foreach (var day in dayOfWeek)
                        {
                            st.UUID = LK.Util.UID.Instance.GetUniqueID();
                            st.SCHEDULE_UUID = drSchedule.UUID;
                            st.STATUS = "READY";
                            st.START_TIME = startDate.Value.AddDays(Convert.ToInt16(day));
                            st.gotoTable().Insert_Empty2Null(st);
                            startDate = startDate.Value.AddDays(Convert.ToInt16(drSchedule.C_WEEK * 7));
                        }

                    }

                    drSchedule.EXPEND_ALL = "Y";
                    drSchedule.gotoTable().Update_Empty2Null(drSchedule);

                    #endregion
                }



            }
            else
            {
                /*單次任務*/
                if (drSchedule.CYCLE_TYPE == "單次任務")
                {
                    st.UUID = LK.Util.UID.Instance.GetUniqueID();
                    st.SCHEDULE_UUID = drSchedule.UUID;
                    st.STATUS = "READY";
                    st.START_TIME = drSchedule.SINGLE_DATE;
                    st.gotoTable().Insert_Empty2Null(st);

                    drSchedule.EXPEND_ALL = "Y";
                    drSchedule.gotoTable().Update_Empty2Null(drSchedule);
                }

            }
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
        }
    }


    private DateTime[] getFirstDayInMonth(int year, int month, int week)
    {
        DateTime jan1 = new DateTime(year, month, 1);

        var cal = CultureInfo.CurrentCulture.Calendar;
        int firstWeek = cal.GetWeekOfYear(jan1, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday);

        firstWeek = firstWeek + (week - 1);

        var ret = WeekDays(year, month, week);

        return (DateTime[])ret[month.ToString() + "." + week.ToString()];



    }

    private static DateTime GetIso8601FirstWeekOfYear(int year)
    {
        // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
        // be the same week# as whatever Thursday, Friday or Saturday are,
        // and we always get those right
        var cal = CultureInfo.CurrentCulture.Calendar;
        var time = new DateTime(year, 1, 1);
        DayOfWeek day = cal.GetDayOfWeek(time);

        if (day == DayOfWeek.Sunday || day == DayOfWeek.Monday || day == DayOfWeek.Thursday || day == DayOfWeek.Wednesday)
        {
            while (cal.GetDayOfWeek(time) != DayOfWeek.Sunday)
            {
                time = time.AddDays(-1);
            }
            return time;
        }
        else
        {
            while (cal.GetDayOfWeek(time) != DayOfWeek.Sunday)
            {
                time = time.AddDays(1);
            }
            return time;
        }
    }

    private static System.Collections.Hashtable WeekDays(int Year, int month, int week)
    {
        //System.Collections.Hashtable ht = new System.Collections.Hashtable();
        IDictionary<string, DateTime[]> ht = new Dictionary<string, DateTime[]>();


        //DateTime start = new DateTime(Year, 1, 1).AddDays(7 * WeekNumber);

        DateTime start = GetIso8601FirstWeekOfYear(Year);
        //start = start.AddDays(7 * WeekNumber);
        start = start.AddDays(-((int)start.DayOfWeek));
        int weekOfYear = 0;

        while ((Year + 1) > start.Year)
        {

            weekOfYear++;
            var value = Enumerable.Range(0, 7).Select(num => start.AddDays(num)).ToArray();
            string key = "";
            var count = value.Count(c => c.Month.Equals(start.Month));
            if (start.Month == 6)
            {
                //var debug = true;
            }
            if (count >= 4)
            {
                key = start.Month.ToString() + "." + weekOfYear.ToString();
            }
            else
            {

                key = (start.AddDays(7).Month).ToString() + "." + weekOfYear.ToString();
            }

            ht.Add(key, value);
            start = start.AddDays(7);

        }

        System.Collections.Hashtable returnData = new System.Collections.Hashtable();
        string nowMonth = "";
        int seq = 1;



        try
        {
            foreach (var key in ht)
            {
                if (nowMonth == "")
                {
                    nowMonth = key.Key.ToString().Split('.')[0];
                }

                if (nowMonth != key.Key.ToString().Split('.')[0])
                {
                    seq = 1;
                    nowMonth = key.Key.ToString().Split('.')[0];
                }
                else
                {
                    seq++;
                }

                returnData.Add(nowMonth + "." + seq.ToString(), ht[key.Key]);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }



        return returnData;
    }
}

