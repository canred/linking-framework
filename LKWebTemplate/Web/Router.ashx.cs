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
namespace LKWebTemplate
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    public class Router : IHttpHandler, IRequiresSessionState
    {
        private string getContentText(HttpContext context, out string action, out string method)
        {
            string _action = "", _method = "";

            byte[] requestDataInByte = context.Request.BinaryRead(context.Request.TotalBytes);
            var enc = new ASCIIEncoding();

            string requestData = enc.GetString(requestDataInByte);

            if (context.Request.Files.Count > 0)
            {
                action = context.Request["extAction"];
                method = context.Request["extMethod"];
                return "";
            }
            //var jobj = JObject.Parse(requestData);
            var paramsC = requestData.Split('&');
            try
            {
                _action = paramsC.Where(c => c.StartsWith("extAction=")).First().Split('=')[1].ToString();
                _method = paramsC.Where(c => c.StartsWith("extMethod=")).First().Split('=')[1].ToString();
            }
            catch
            {
                try
                {
                    var jobj = JObject.Parse(requestData);
                    if (jobj["action"] != null)
                    {
                        _action = jobj["action"].Value<string>();
                    }
                    if (jobj["method"] != null)
                    {
                        _method = jobj["method"].Value<string>();
                    }
                }
                catch
                {
                    var jobj = JObject.Parse("{ma:" + requestData + "}");

                    if (jobj["ma"].Children().First()["action"] != null)
                    {
                        _action = jobj["ma"].Children().First()["action"].Value<string>();
                    }
                    if (jobj["ma"].Children().First()["method"] != null)
                    {
                        _method = jobj["ma"].Children().First()["method"].Value<string>();
                    }
                }
            }
            action = _action;
            method = _method;
            return requestData;
        }

        private JObject getServiceClassList()
        {
            StringBuilder resStr = new StringBuilder();
            AppDomain MyDomain = AppDomain.CurrentDomain;
            System.Reflection.Assembly[] AssembliesLoaded = MyDomain.GetAssemblies();
            StringBuilder sb = new StringBuilder();
            LKWebTemplate.Util.Session.Store ss = new LKWebTemplate.Util.Session.Store();

            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("ServiceClass");
            foreach (var allAssembly in AssembliesLoaded)
            {
                foreach (var theType in allAssembly.GetTypes())
                {
                    object[] allCustomAttribute = theType.GetCustomAttributes(false);
                    foreach (var _theType in allCustomAttribute)
                    {
                        Type directType = _theType.GetType();
                        if (typeof(DirectServiceAttribute) == directType)
                        {
                            string className = theType.Name;
                            var tmpDt = DirectProxyGenerator.generateDirectApiServiceClass(className);
                            foreach (System.Data.DataRow tmpRow in tmpDt.Rows)
                            {
                                var newRow = dt.NewRow();
                                newRow["ServiceClass"] = tmpRow["ServiceClass"];
                                dt.Rows.Add(newRow);
                            }
                        }
                    }
                }
            }
            return LK.Util.JsonHelper.DataTable2JObject(dt, 0, 9999);
        }

        public void ProcessRequest(HttpContext context)
        {
            var rpc = new ExtDirect.Direct.ExtRPC();
            string action, method;
            LKWebTemplate.Model.Basic.BasicModel mod = new LKWebTemplate.Model.Basic.BasicModel();
            string INIT = "";
            IList<LKWebTemplate.Model.Basic.Table.Record.Proxy_Record> allProxy = new List<LKWebTemplate.Model.Basic.Table.Record.Proxy_Record>();
            if (context.Request.QueryString["init"] != null)
            {
                allProxy = mod.getProxy_By_KeyWord(LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().InitAppUuid, "", null);
            }




            #region 跨網POST支援
            if (LK.Config.DirectAuth.DirectAuthConfigs.GetConfig().AllowCrossPost)
            {
                JObject jo = new JObject();
                var json = "{" + LK.Config.DirectAuth.DirectAuthConfigs.GetConfig().Rule + "}";
                jo = JObject.Parse(json);

                var accessAll = jo["access_all"].Value<string>().ToLower() == "true" ? true : false;
                if (accessAll)
                {
                    context.Response.AddHeader("Access-Control-Allow-Origin", "*");
                    if (context.Request.RequestType == "OPTIONS")
                    {

                        context.Response.AddHeader("Access-Control-Allow-Methods", "GET,PUT,POST,DELETE,OPTIONS");
                        context.Response.AddHeader("Access-Control-Allow-Headers", "x-user-session,origin, content-type, accept,Origin,cache-control,man,messagetype,soapaction,x-requested-with");
                        return;
                    }
                }
                else
                {
                    var beark = false;
                    foreach (var item in jo["directUrl"])
                    {
                        if (beark)
                            break;
                        foreach (var subItem in item["dsource"])
                        {
                            if (subItem["IP"] != null)
                            {
                                string _a = subItem["IP"].Value<string>();
                                if (context.Request.UserHostAddress.StartsWith(_a))
                                {
                                    context.Response.AddHeader("Access-Control-Allow-Origin", "*");
                                    if (context.Request.RequestType == "OPTIONS")
                                    {
                                        context.Response.AddHeader("Access-Control-Allow-Methods", "GET,PUT,POST,DELETE,OPTIONS");
                                        context.Response.AddHeader("Access-Control-Allow-Headers", "x-user-session,origin, content-type, accept,Origin,cache-control,man,messagetype,soapaction,x-requested-with");
                                        return;
                                    }
                                    beark = true;
                                    break;
                                }
                            }
                        }
                    }
                }

            }
            else
            {
                if (context.Request.RequestType == "OPTIONS")
                {
                    context.Response.AddHeader("Access-Control-Allow-Methods", "GET");
                    context.Response.AddHeader("Access-Control-Allow-Headers", "x-user-session,origin, content-type, accept,Origin,cache-control,man,messagetype,soapaction,x-requested-with");
                    return;
                }
            }
            #endregion

            #region

            if (context.Request.Params["ServiceClass"] != null)
            {
                var service = getServiceClassList();
                var outputString = "{";
                outputString += "method:\"ServiceClass\",";
                //outputString += "tid:\"" + drProxy.PROXY_METHOD + "\",";
                outputString += "action:\"ClassList\",";
                outputString += "result:" + service.ToString();
                outputString += "}";
                context.Response.Write(outputString);
                return;
            }
            var bodyContent = getContentText(context, out action, out method);
            IList<LKWebTemplate.Model.Basic.Table.Record.Proxy_Record> drsProxy = new List<LKWebTemplate.Model.Basic.Table.Record.Proxy_Record>();
            if (context.Request.QueryString["init"] != null)
            {
                drsProxy = mod.getProxy_By_KeyWord(LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().InitAppUuid, "", null);
            }

            var countNeedRedirect = drsProxy.Where(c => c.PROXY_ACTION.ToUpper().Equals(action.ToUpper()) && c.PROXY_METHOD.ToUpper().Equals(method.ToUpper()) && c.NEED_REDIRECT.Equals("Y"))
                .Count();

            //檢查是否執行的是跨服器功能
            if (countNeedRedirect > 0)
            {
                var drProxy = drsProxy.Where(c => c.PROXY_ACTION.ToUpper().Equals(action.ToUpper()) && c.PROXY_METHOD.ToUpper().Equals(method.ToUpper()) && c.NEED_REDIRECT.Equals("Y"))
                .First();

                LK.Cloud cloud = new LK.Cloud();
                StoreSession ss = new StoreSession();
                //var dataCount = JObject.Parse(bodyContent)["data"].Count();
                List<string> sParam = new List<string>();
                foreach (var item in JObject.Parse(bodyContent)["data"])
                {
                    sParam.Add(item.Value<string>());
                }

                var cloudId = ss.getCloudId();
                if (cloudId == "")
                {
                    cloudId = context.Request.Headers["CLOUD_ID"].ToString();
                }

                /**參數**/
                var retCloud = cloud.CallDirect(drProxy.REDIRECT_SRC, drProxy.REDIRECT_PROXY_ACTION + "." + drProxy.REDIRECT_PROXY_METHOD.Split(',')[0], sParam.ToArray(), cloudId);
                //context.Response.Write(retCloud.ToString());
                var outputString = "{";
                outputString += "method:\"" + drProxy.PROXY_METHOD + "\",";
                //outputString += "tid:\"" + drProxy.PROXY_METHOD + "\",";
                outputString += "action:\"" + drProxy.PROXY_ACTION + "\",";
                outputString += "result:" + retCloud.ToString();
                outputString += "}";
                context.Response.Write(outputString);
                return;
                /*
                {
                  "type": "rpc",
                  "method": "Encode",
                  "tid": 999,
                  "action": "EncryptAction",
                  "result": {
                    "success": false,
                    "message": "Connection has expired!"
                  }
                }
                */

            }
            /*
            
            return;
            */
            #endregion
            var ret = rpc.ExecuteRPCJObject(context.Request, bodyContent, context);
            if (ret["MUTIL_ACTION"] != null)
            {
                context.Response.Write(ret["MUTIL_ACTION"].ToString());
                return;
            }
            if (ret["EXTDIRECTCALLBACK"] != null)
            {
                string js = ret["EXTDIRECTCALLBACK"].ToString();
                js += "(" + ret["EXTDIRECTCALLBACK_VALUE"].ToString() + ")";
                context.Response.Write(js);
                return;
            }
            else
            {
                //ret = ret.Replace("\"[", "[").Replace("\"]", "]");
                context.Response.Write(ret.ToString());
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
