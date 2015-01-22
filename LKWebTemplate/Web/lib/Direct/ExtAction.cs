using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using Newtonsoft.Json.Linq;
namespace ExtDirect.Direct
{
    public class ExtAction : IAction
    {
        public Response ExecuteForm(HttpRequest httpRequest)
        {
            int i = 0;
            var responseForm = new Request
            {
                action = httpRequest["extAction"],
                method = httpRequest["extMethod"],
                tid = Convert.ToInt32(httpRequest["extTID"])
            };

            if (responseForm.action.IndexOf(".") > 0)
            {
                responseForm.action = responseForm.action.Split('.')[1];
            }
            var action = Assembly.GetExecutingAssembly().CreateInstance(responseForm.action);
           
            Type thisType = action.GetType();
            MethodInfo theMethod = thisType.GetMethod(responseForm.method);
            ParameterInfo[] parmInfo = theMethod.GetParameters();
            var parmList = new object[parmInfo.Length];

            foreach (var parm in parmInfo)
            {
                if (parm.ParameterType.ToString() == "System.Web.HttpRequest")
                {

                }
                else
                {
                    parmList[i] = httpRequest[parm.Name];
                    i++;
                }               
            }
            parmList[parmList.Count()-1] = httpRequest;

            var requestData = (string)theMethod.Invoke(action, parmList);

            var resNew = new Response
            {
                type = "rpc",
                method = responseForm.method,
                tid = responseForm.tid,
                result = requestData,
                action = responseForm.action
            };
            return resNew;
        }

        public JObject ExecuteFormJObject(HttpRequest httpRequest)
        {
            int i = 0;
            var responseForm = new Request
            {
                action = httpRequest["extAction"],
                method = httpRequest["extMethod"],
                tid = Convert.ToInt32(httpRequest["extTID"])
            };
            if (responseForm.action.IndexOf(".") > 0)
            {
                responseForm.action = responseForm.action.Split('.')[1];
            }
            var action = Assembly.GetExecutingAssembly().CreateInstance(responseForm.action);
            Type thisType = action.GetType();
            MethodInfo theMethod = thisType.GetMethod(responseForm.method);
            ParameterInfo[] parmInfo = theMethod.GetParameters();
            var parmList = new object[parmInfo.Length];

            foreach (var parm in parmInfo)
            {
                if (parm.ParameterType.ToString() == "System.Web.HttpRequest")
                {

                }
                else
                {
                    parmList[i] = httpRequest[parm.Name];
                    i++;
                }
            }
            parmList[parmList.Count() - 1] = httpRequest;

            var requestData = (JObject)theMethod.Invoke(action, parmList);

            var ret = JObject.Parse("{type:\"rpc\",method:\"\",tid:"+responseForm.tid+",result:\"\",actioin:\""+responseForm.action+"\"}");

            ret["result"] = requestData;
            
            return ret;
        }
        public Response ExecuteLoad(Request request)
        {
            
            var action = Assembly.GetExecutingAssembly().CreateInstance(request.action);
            Type thisType = action.GetType();
            MethodInfo theMethod = thisType.GetMethod(request.method);
            var parmList = new List<object>();
            request.data = parmList;
            var param = new object[request.data.Count];
           
            for (int i = 0; i < request.data.Count; i++) {
                param[i] = request.data[i];
            }

            var resNew = new Response
            {
                type = "rpc",
                method = request.method,
                tid = request.tid,
                result = (string)theMethod.Invoke(action, param),
                action = request.action
            };
            return resNew;
        }
        public Response ExecuteCreate(Request request)
        {
            var res = new Response();
            return res;
        }
        public Response ExecuteSave(Request request)
        {
            var res = new Response();
            return res;
        }
        public Response ExecuteUpdate(Request request, List<Dictionary<string, string>> dataList)
        {
            var action = Assembly.GetExecutingAssembly().CreateInstance(request.action);

            Type thisType = action.GetType();
            MethodInfo theMethod = thisType.GetMethod(request.method);
            ParameterInfo[] parmInfo = theMethod.GetParameters();
            var funcParList = new object[parmInfo.Length];
            string res = "";
            foreach (var dat in dataList)
            {
                int i = 0;
                foreach (var parm in parmInfo)
                {
                    try
                    {
                        funcParList[i] = dat[parm.Name];
                    }
                    catch
                    {
                        funcParList[i] = "";
                    }
                    i++;
                }
               res+= (string)theMethod.Invoke(action, funcParList);
               res += ",";
            }
            if (dataList.Count > 0)
            {
                res = res.Remove(res.Length - 1, 1);
                if (dataList.Count > 1)
                {
                    res = "[" + res + "]";
                }
            }
            else
            {
                res = "{}";
            }
            
            var resNew = new Response
            {
                type = "rpc",
                method = request.method,
                tid = request.tid,
                result = res,
                action = request.action

            };
            return resNew;
        }
        public Response ExecuteDelete(Request request)
        {
            var res = new Response();
            return res;
        }
        public Response ExecuteNormalAction(Request request)
        {
            return new Response();
        }

        public string ExecuteNormalAction2(Request request)
        {            
            var obj = Assembly.GetExecutingAssembly().CreateInstance(request.action);
            Type thisType = obj.GetType();
            /*取得要執行的方法*/
            MethodInfo theMethod = thisType.GetMethod(request.method);            
            DirectAction action = DirectAction.Null;
            /*取得此方法的attribute屬性*/
            var attrs = theMethod.GetCustomAttributes(typeof(DirectMethodAttribute), false);                
            if (attrs.Length == 1){action = ((DirectMethodAttribute)(attrs[0])).Action;}
            request.data.Add(request);
            /*取得 前端傳入的資料*/
            var param = new object[request.data.Count];
            for (int i = 0; i < request.data.Count; i++) {
                param[i] = request.data[i];
            }
            /*ActionLog*/
            LK.ActionLog.ActionLog.SetActionLog(param, theMethod);
            /*反射呼叫指定的方法*/
            try
            {
                //字串的處理方式
                var requestData = (string)theMethod.Invoke(obj, param);
                string ret = "{";
                ret += string.Format("\"type\":\"{0}\",", "rpc");
                ret += string.Format("\"method\":\"{0}\",", request.method);
                ret += string.Format("\"tid\":{0},", request.tid);
                ret += string.Format("\"action\":\"{0}\",", request.action);
                if (action == DirectAction.TreeStore)
                {
                    ret += string.Format("\"result\":[{0}]", requestData);
                }
                else
                {
                    try
                    {
                        JObject.Parse(requestData);
                        ret += string.Format("\"result\":{0}", requestData);
                    }
                    catch
                    {
                        ret += string.Format("\"result\":\"{0}\"", requestData);
                    }
                }
                ret += "}";
                return ret;
            }
            catch {
                //JObject 的處理方式
                var jobject = (JObject)theMethod.Invoke(obj, param);
                var ret = JObject.Parse("{type:\"rpc\",method:\"" + request.method + "\",tid:" + request.tid + ",action:\"" + request.action + "\",result:[]}");
                ret["result"] = jobject;

                return ret.ToString();

            }
            
        }

        public JObject ExecuteNormalAction2JObject(Request request)
        {
            var obj = Assembly.GetExecutingAssembly().CreateInstance(request.action);
            Type thisType = obj.GetType();
            /*取得要執行的方法*/
            MethodInfo theMethod = thisType.GetMethod(request.method);
            DirectAction action = DirectAction.Null;
            /*取得此方法的attribute屬性*/
            var attrs = theMethod.GetCustomAttributes(typeof(DirectMethodAttribute), false);
            if (attrs.Length == 1) { action = ((DirectMethodAttribute)(attrs[0])).Action; }
            request.data.Add(request);
            /*取得 前端傳入的資料*/
            var param = new object[request.data.Count];
            for (int i = 0; i < request.data.Count; i++)
            {
                param[i] = request.data[i];
            }
            /*ActionLog*/
            LK.ActionLog.ActionLog.SetActionLog(param, theMethod);
            /*反射呼叫指定的方法*/
            try
            {
                //字串的處理方式
                JObject requestData = new JObject();
                var ret = JObject.Parse("{type:\"rpc\",method:\"" + request.method + "\",tid:" + request.tid + ",action:\"" + request.action + "\",result:{}}");
                try
                {
                    requestData = (JObject) theMethod.Invoke(obj, param);
                }catch
                {
                    LK.MyException.MyException.ErrorNoThrowExceptionForStaticClass(new Exception(request.action + "." + request.method + "發現呼叫action但無法執行，可能是參數錯誤!"));
                }
                if (action == DirectAction.TreeStore)
                {
                    ret["result"] = requestData["TREE"];                    
                }
                else
                {
                    if (requestData["STRING_ONLY"] != null) {
                        ret["result"] = requestData["STRING_ONLY"];
                    }
                    else
                    {
                        ret["result"] = requestData;
                    }
                }                
                return ret;
            
            }
            catch
            {
                //JObject 的處理方式
                //var jobject = (JObject)theMethod.Invoke(obj, param);
                var ret = JObject.Parse("{type:\"rpc\",method:\"" + request.method + "\",tid:" + request.tid + ",action:\"" + request.action + "\",result:{}}");
                ret["result"] = null;

                return ret;

            }

        }

        public string ExecuteUpdateAction2(Request request)
        {           
            var obj = Assembly.GetExecutingAssembly().CreateInstance(request.action);
            Type thisType = obj.GetType();
            MethodInfo theMethod = thisType.GetMethod(request.method);
            request.data.Add(request);
            var param = new object[request.data.Count];
            for (int i = 0; i < request.data.Count; i++)
            {
                param[i] = request.data[i];
            }
            theMethod.Invoke(obj, param);
            param[0]=JObject.Parse("{updates:"+param[0]+"}");
            string ret = request.action+"_"+request.method+ "_callback({\"success\":true,\"function_name\":\"" + request.action + "\"})";
            return ret;
        }

        public JObject ExecuteUpdateActionJObject(Request request)
        {
            var obj = Assembly.GetExecutingAssembly().CreateInstance(request.action);
            Type thisType = obj.GetType();
            MethodInfo theMethod = thisType.GetMethod(request.method);
            request.data.Add(request);
            var param = new object[request.data.Count];
            for (int i = 0; i < request.data.Count; i++)
            {
                param[i] = request.data[i];
            }
            var updates = JObject.Parse("{updates:"+param[0]+"}");
            param[0] = updates;
            //param[0] = JObject.Parse(param[0].ToString());


            theMethod.Invoke(obj, param);

            

            return Helper.JObjectHelper.CallBack(request.action, request.method);
        }

        public Response ExecuteCRUD(Request request, List<Dictionary<string,string>> recordList)
        {
            string json = "";
            var action = Assembly.GetExecutingAssembly().CreateInstance(request.action);
            Type thisType = action.GetType();
            MethodInfo theMethod = thisType.GetMethod(request.method);
            ParameterInfo[] parmInfo = theMethod.GetParameters();
            
            foreach (var t in recordList)
            {
                var funcParList = new object[parmInfo.Length];
                int i = 0;
                foreach (var parm in parmInfo)
                {
                    funcParList[i] = t[parm.Name];
                    i++;
                }
                json += (string)theMethod.Invoke(action, funcParList) + ",";

            }
            if (recordList.Count > 0)
            {
                json = json.Remove(json.Length - 1, 1);
            }
            if (recordList.Count > 1)
            {
                json = "[" + json + "]";
            }
            var resNew = new Response
            {
                type = "rpc",
                method = request.method,
                tid = request.tid,
                result = json,
                action = request.action
            };
            return resNew;            
        }

        public JObject ExecuteCRUDJObject(Request request, List<Dictionary<string, string>> recordList)
        {
            var json = new List<JObject>();
            var action = Assembly.GetExecutingAssembly().CreateInstance(request.action);
            Type thisType = action.GetType();
            MethodInfo theMethod = thisType.GetMethod(request.method);
            ParameterInfo[] parmInfo = theMethod.GetParameters();

            foreach (var t in recordList)
            {
                var funcParList = new object[parmInfo.Length];
                int i = 0;
                foreach (var parm in parmInfo)
                {
                    funcParList[i] = t[parm.Name];
                    i++;
                }
                json.Add((JObject)theMethod.Invoke(action, funcParList));

            }
            
            
            JObject ret = JObject.Parse("{type:\"rpc\",method:\""+request.method+"\",tid:"+request.tid+",action:\""+request.action+"\",result:{}}");

            if (recordList.Count > 1)
            {
                var jarray = new JArray();
                foreach (var item in json) {
                    jarray.Add(item);
                }
                ret["result"] = jarray;
            }
            else
            {
                ret["result"] = json.First();
            }
            
            return ret;
        }
    }
}
