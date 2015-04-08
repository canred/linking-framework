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
using LKWebTemplate.Model.Basic.Table.Record;
namespace Limew.js
{
    /// <summary>
    /// $codebehindclassname$ 的摘要描述
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class System_Config : IHttpHandler, IRequiresSessionState    
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/JavaScript";
            LKWebTemplate.Util.Session.Store ss = new LKWebTemplate.Util.Session.Store();
            AttendantV_Record attendantv = null;
            var code_page = "";
            if (ss.ExistKey("USER"))
            {
                attendantv = (AttendantV_Record)ss.getObject("USER");
                code_page = attendantv.CODE_PAGE;
            }
            var systemConfig = HttpContext.Current.Application.Get("system-config");

            var srJs = System.IO.File.OpenText(HttpContext.Current.Request.PhysicalApplicationPath.ToString() + "js/system-config.js");
            var jsContext = srJs.ReadToEnd();
            srJs.Close();
            jsContext += "SYSTEM_APPLICATION='" + LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().DirectApplicationName + "';";
            if (code_page.Length > 0)
            {
                if (System.IO.File.Exists(HttpContext.Current.Request.PhysicalApplicationPath.ToString() + "js/" + code_page + "-Message.js"))
                {
                    var languageJs = System.IO.File.OpenText(HttpContext.Current.Request.PhysicalApplicationPath.ToString() + "js/" + code_page + "-Message.js");
                    var languageJsContext = languageJs.ReadToEnd();
                    languageJs.Close();
                    jsContext += languageJsContext;
                }
            }
            HttpContext.Current.Application.Set("system-config", jsContext);
            systemConfig = HttpContext.Current.Application.Get("system-config");

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
