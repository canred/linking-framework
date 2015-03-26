using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using ExtDirect;
using ExtDirect.Direct;
using System.Web.SessionState;
using System.Security.Principal;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Net;
namespace LKWebTemplate.admin.jobs
{
    /// <summary>
    /// $codebehindclassname$ 的摘要描述
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class RunSchedule : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            LKWebTemplate.Model.Basic.BasicModel mod = new LKWebTemplate.Model.Basic.BasicModel();                        
            var currentTime = DateTime.Now;            
            /*再這一分鐘有需要執行的程式嗎?*/
            var runSchedule = mod.getVScheduleTime_By_StartDate(currentTime);
            if (runSchedule.Count > 0) {
                foreach (var vitem in runSchedule) {
                    var drSchedule = vitem.Link_Schedule_By_Uuid().First();
                    try
                    {   
                        if (drSchedule.RUN_SECURITY.Trim() == "localhost")
                        {
                            /*僅可以本地執行的*/
                            if (context.Request.Url.Host.ToLower().Equals("localhost"))
                            {
                                
                            }
                            else {
                                continue;
                            }                            
                        }                        
                        CallJobs(drSchedule.RUN_URL + "?" + drSchedule.RUN_URL_PARAMETER);
                        var drScheduletime = vitem.Link_ScheduleTime_By_Uuid().First();
                        drScheduletime.STATUS = "FINISH";
                        drSchedule.LAST_RUN_TIME = DateTime.Now;
                        drSchedule.LAST_RUN_STATUS= "OK";
                        drScheduletime.gotoTable().Update_Empty2Null(drScheduletime);
                        drSchedule.gotoTable().Update_Empty2Null(drSchedule);
                        context.Response.Write("OK");                        
                    }
                    catch  {
                        var drScheduletime = vitem.Link_ScheduleTime_By_Uuid().First();
                        drScheduletime.STATUS = "ERR";
                        drScheduletime.gotoTable().Update_Empty2Null(drScheduletime);

                        drSchedule.LAST_RUN_TIME = DateTime.Now;
                        drSchedule.LAST_RUN_STATUS = "ERR";
                        drSchedule.gotoTable().Update_Empty2Null(drSchedule);
                        context.Response.Write("ERR");
                        
                    }
                }    
            }
            context.Response.End();
            /*先找出來未完全展開的項目*/
            var drsSchedule = mod.getSchedule_By_ExpendAll("N");
            ScheduleAction scheduleAction = new ScheduleAction();
            foreach (var scheduleItem in drsSchedule)
            {
                if (scheduleItem.SCHEDULE_END_DATE > scheduleItem.CONTIUNE_DATETIME)
                {
                    if (DateTime.Now > scheduleItem.CONTIUNE_DATETIME)
                    {
                        scheduleAction.expentToScheduleTime(scheduleItem.UUID, true);
                    }
                }
            }
        }

        public void CallJobs(string url)
        {
            try
            {
                Uri target = new Uri(url);
                WebRequest request = WebRequest.Create(target);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                }
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
