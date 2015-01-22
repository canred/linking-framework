using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using ExtDirect;
using ExtDirect.Direct;
using System.Web.SessionState;
using System.Text;
using System.Reflection;
namespace LKWebTemplate.js
{
    /// <summary>
    /// $codebehindclassname$ 的摘要描述
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class System_Config : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/JavaScript";
            var systemConfig = HttpContext.Current.Application.Get("system-config");
            if (systemConfig == null || LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().IsProductionServer==false)
            {
                var srJs = System.IO.File.OpenText(HttpContext.Current.Request.PhysicalApplicationPath.ToString() + "js/system-config.js");
                var jsContext = srJs.ReadToEnd();
                srJs.Close();
                jsContext += "SYSTEM_APPLICATION='" + LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().DirectApplicationName + "';";
                HttpContext.Current.Application.Set("system-config", jsContext);
                systemConfig = HttpContext.Current.Application.Get("system-config");
            }            
            context.Response.Write(systemConfig as string);                            
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
