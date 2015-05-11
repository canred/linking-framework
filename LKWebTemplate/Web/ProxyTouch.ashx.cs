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

namespace LKWebTemplate
{
    /// <summary>
    /// $codebehindclassname$ 的摘要描述
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class ProxyTouch : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/JavaScript";
            //context.Response.AddHeader("Access-Control-Allow-Origin", "*");
            //context.Response.AddHeader("X-Ms-Origin", "*");            
            StringBuilder resStr = new StringBuilder();
            AppDomain MyDomain = AppDomain.CurrentDomain;
            Assembly[] AssembliesLoaded = MyDomain.GetAssemblies();
            StringBuilder sb = new StringBuilder();
            LKWebTemplate.Util.Session.Store ss = new LKWebTemplate.Util.Session.Store();
            var INIT = "";
            if (context.Request.QueryString["init"] != null)
            {
                INIT = context.Request.QueryString["init"].ToUpper();
            }
            //if (ss.ExistKey("PROXY-TOUCH") && Parameter.Config.ParemterConfigs.GetConfig().IsProductionServer == true)
            if (ss.ExistKey("PROXY-TOUCH"))
            {
                context.Response.Write(ss.getObject("PROXY-TOUCH").ToString());
                return;
            }
            var isFrist = true;
            foreach (var allAssembly in AssembliesLoaded)
            {
                if (!allAssembly.GetName().Name.StartsWith(this.GetType().Namespace))
                {
                    continue;
                };
                foreach (var theType in allAssembly.GetTypes())
                {
                    object[] allCustomAttribute = theType.GetCustomAttributes(false);
                    foreach (var _theType in allCustomAttribute)
                    {
                        Type directType = _theType.GetType();
                        if (typeof(DirectServiceAttribute) == directType)
                        {
                            string className = theType.Name;
                            string rem = "{";
                            var arrUrl = context.Request.Url.OriginalString.Split('/');
                            var newUrl = "";
                            for (int i = 0; i < 4; i++)
                            {
                                if (arrUrl[i].ToUpper().IndexOf("PROXY.ASHX") == -1)
                                {

                                    newUrl += arrUrl[i] + "/";
                                }
                            }
                            //newUrl = newUrl.Replace("localhost","10.11.80.145");
                            newUrl += "Router.ashx";
                            rem += "url: \"" + newUrl + "\",";
                            rem += "type:\"remoting\",";
                            rem += "timeout:" + LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().DirectTimeOut.ToString() + ",";
                            string json = DirectProxyGenerator.generateDirectApi(allAssembly.GetName().Name,className, true);
                            rem += json;
                            rem += "};";
                            rem =  className + " =" + rem;
                            if (isFrist == true)
                            {
                                rem = " Ext.ns('" + LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().DirectApplicationName + "');" + rem;
                                //rem ="(function(){" + rem + "})();";
                                isFrist = false;
                            }
                            else
                            {
                                rem = "(function(){" + rem + "})();";
                            }
                            sb.Append(rem);
                        }
                    }
                }
            }


            LKWebTemplate.Model.Basic.BasicModel mod = new LKWebTemplate.Model.Basic.BasicModel();
            IList<LKWebTemplate.Model.Basic.Table.Record.Proxy_Record> drsProxy = new List<LKWebTemplate.Model.Basic.Table.Record.Proxy_Record>();
            if (INIT.Length == 0)
            {
                drsProxy = mod.getProxy_By_KeyWord(LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().InitAppUuid, "", null);
            }
            foreach (var proxy in drsProxy)
            {
                if (proxy.NEED_REDIRECT == "Y")
                {
                    //string className = theType.Name;
                    string rem = "{";
                    var arrUrl = context.Request.Url.OriginalString.Split('/');
                    var newUrl = "";
                    for (int i = 0; i < 4; i++)
                    {
                        if (arrUrl[i].ToUpper().IndexOf("PROXY.ASHX") == -1)
                        {

                            newUrl += arrUrl[i] + "/";
                        }
                    }
                    //newUrl = newUrl.Replace("localhost","10.11.80.145");
                    newUrl += "Router.ashx";
                    rem += "url: \"" + newUrl + "\",";
                    rem += "type:\"remoting\",";
                    rem += "timeout:" + LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().DirectTimeOut.ToString() + ",";

                    rem += "\"actions\":{";
                    rem += "\"" +  proxy.PROXY_ACTION + "\":[";
                    rem += "{";
                    rem += "name:\"" + proxy.REDIRECT_PROXY_METHOD.Split(',')[0] + "\",";
                    rem += "len:" + proxy.REDIRECT_PROXY_METHOD.Split(',')[1] + "";
                    rem += "}";
                    rem += "]";
                    rem += "}";
                    rem += "};";
                    rem =  proxy.PROXY_ACTION + " =" + rem;
                    rem = "(function(){" + rem + "})();";
                    sb.Append(rem);
                }
            }
            ss.setObject("PROXY-TOUCH", sb.ToString());
            context.Response.Write(sb.ToString());
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
