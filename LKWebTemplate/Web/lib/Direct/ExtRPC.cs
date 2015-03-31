using System.Linq;
using System.Text;
using System.Web;
using System.Reflection;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using LK.MyException;
using Newtonsoft.Json.Linq;
using System;
namespace ExtDirect.Direct
{
    public class ExtRPC
    {
        public string ExecuteRPC(HttpRequest request)
        {
            bool flag = true;
            var rpc = new ExtAction();
            var checkFormPost = request["extAction"];
            var jSri = new JavaScriptSerializer();
            try
            {
                if (checkFormPost.Length > 0)
                {
                    flag = false;
                }
            }
            catch (Exception ex)
            {
                MyException.ErrorNoThrowException(this, ex);
            }
            if (flag)
            {
                var tempDataList = new List<Dictionary<string, string>>();
                byte[] requestDataInByte = request.BinaryRead(request.TotalBytes);
                var enc = new ASCIIEncoding();
                string requestData = enc.GetString(requestDataInByte);
                var data = new Request();
                JObject jobject;
                Boolean isMutilAction;
                try
                {
                    jobject = JObject.Parse(requestData);
                    isMutilAction = false;
                }
                catch
                {
                    jobject = JObject.Parse("{MUTIL_ACTION:" + requestData + "}");
                    isMutilAction = true;
                }
                if (isMutilAction)
                {
                    string ret = jobject["MUTIL_ACTION"].Children().Select(item => JObject.Parse(item.ToString())).Aggregate("", (current, objItem) => current + (runAction(objItem, data, requestData, tempDataList) + ","));

                    /*
                     foreach (var item in jobject["MUTIL_ACTION"].Children())
                    {
                        var objItem  = JObject.Parse(item.ToString());
                       ret += runAction(objItem, data, requestData, tempDataList) + ",";
                    }
                     */
                    ret = ret.Substring(0, ret.Length - 1);
                    return "[" + ret + "]";
                }
                return runAction(jobject, data, requestData, tempDataList);
            }
            Response res = rpc.ExecuteForm(request);
            return jSri.Serialize(res);
        }

        public JObject ExecuteRPCJObject(Request request, string bodyContext,HttpContext hc)
        {
            bool flag = true;
            var rpc = new ExtAction();
            var checkFormPost = request.HttpRequest["extAction"];
            /*Canred*/
            try
            {
                if (!string.IsNullOrEmpty(checkFormPost) && checkFormPost.Length > 0)
                {
                    flag = false;

                }
            }
            catch (Exception ex)
            {
                MyException.ErrorNoThrowException(this, ex);
            }

            var data = new Request();
            data.HttpRequest = request.HttpRequest;
            data.HttpContext = hc;
            JObject jobject;
            Boolean isMutilAction;

            if (flag)
            {
                var tempDataList = new List<Dictionary<string, string>>();
                //byte[] requestDataInByte = request.BinaryRead(request.TotalBytes);
                //var enc = new ASCIIEncoding();
                //string requestData = enc.GetString(requestDataInByte);
                string requestData = bodyContext;
                try
                {
                    jobject = JObject.Parse(requestData);
                    isMutilAction = false;
                }
                catch
                {
                    jobject = JObject.Parse("{MUTIL_ACTION:" + requestData + "}");
                    isMutilAction = true;
                }
                if (isMutilAction)
                {
                    var jarray = new JArray();
                    foreach (var item in jobject["MUTIL_ACTION"].Children())
                    {
                        var objItem = JObject.Parse(item.ToString());
                        jarray.Add(runActionJObject(objItem, data, requestData, tempDataList));
                    }
                    var jobject2 = JObject.Parse("{MUTIL_ACTION:{}}");
                    jobject2["MUTIL_ACTION"] = jarray;
                    return jobject2;
                }
                return runActionJObject(jobject, data, requestData, tempDataList);
            }
            return rpc.ExecuteFormJObject(data);
        }

        private string runAction(JObject jobject, Request data, string requestData, List<Dictionary<string, string>> _tempDataList)
        {
            #region action
            var jSri = new JavaScriptSerializer();
            var rpc = new ExtAction();
            var newData = new Request();

            newData.action = jobject["action"].ToString();
            newData.method = jobject["method"].ToString();
            newData.tid = Convert.ToInt32(jobject["tid"].ToString());

            var obj = Assembly.GetExecutingAssembly().CreateInstance(newData.action);
            Type thisType = obj.GetType();
            MethodInfo theMethod = thisType.GetMethod(newData.method);


            object[] mAtt = theMethod.GetCustomAttributes(false);
            DirectAction runAction = DirectAction.Null;
            foreach (var att in mAtt)
            {
                runAction = ((DirectMethodAttribute)(att)).Action;
            }

            var d = new List<string>();
            try
            {
                if (jobject["data"].First["page"] != null)
                {
                    newData.page = Convert.ToInt32(jobject["data"].First["page"].ToString());
                }
            }
            catch (InvalidOperationException ex)
            {
                MyException.ErrorNoThrowException(this, ex);
            }
            catch (Exception ex2)
            {
                MyException.Error(this, ex2);
            }
            try
            {
                if (jobject["data"].First["start"] != null)
                {
                    newData.start = Convert.ToInt32(jobject["data"].First["start"].ToString());
                }
            }
            catch (InvalidOperationException ex)
            {
                MyException.ErrorNoThrowException(this, ex);
            }
            catch (Exception ex2)
            {
                MyException.Error(this, ex2);
            }
            try
            {
                if (jobject["data"].First["limit"] != null)
                {
                    newData.limit = Convert.ToInt32(jobject["data"].First["limit"].ToString());
                }
            }
            catch (InvalidOperationException ex)
            {
                MyException.ErrorNoThrowException(this, ex);
            }
            catch (Exception ex2)
            {
                MyException.Error(this, ex2);
            }

            try
            {
                if (jobject["data"].First["dir"] != null)
                {
                    newData.dir = jobject["data"].First["dir"].ToString();
                }
            }
            catch (InvalidOperationException ex)
            {
                MyException.ErrorNoThrowException(this, ex);
            }
            catch (Exception ex2)
            {
                MyException.Error(this, ex2);
            }
            try
            {
                //if (runAction == DirectAction.Update)
                //{
                //    d.Add(jobject["data"].First["updatedata"].ToString());
                //}
                //else
                //{
                    d.AddRange(from object tmp in (jobject["data"].First).AsJEnumerable() select tmp.ToString().Split(':') into kv where kv[0] != "\"page\"" && kv[0] != "\"start\"" && kv[0] != "\"limit\"" && kv[0] != "\"sort\"" && kv[0] != "\"dir\"" select kv[1].Replace("\"", "").Trim());                   
                //}
            }
            catch
            {
                //if (runAction == DirectAction.Update)
                //{
                //    d.Add(jobject["data"].ToString());
                //}
                //else
                //{
                    if (jobject["data"] != null)
                    {
                        try
                        {
                            d.AddRange(from object tmp in (jobject["data"]).AsJEnumerable() where (((JValue)(tmp))).Type != JTokenType.Null select tmp.ToString());
                            /*
                            原來的程式
                             foreach (object tmp in (jobject["data"]).AsJEnumerable())
                            {
                                if ((((Newtonsoft.Json.Linq.JValue)(tmp))).Type != Newtonsoft.Json.Linq.JTokenType.Null)
                                {
                                    var kv = tmp.ToString();
                                    d.Add(kv);
                                }
                            }
                             */
                        }
                        catch (InvalidOperationException ex2)
                        {
                            MyException.ErrorNoThrowException(this, ex2);
                        }
                    }
                //}
            }

            var dd = d.Cast<object>().ToList();
            /*
            原來的程式
             for (int i = 0; i < d.Count; i++)
            {
                dd.Add(d[i]);
            }
             */

            newData.data = dd;
            data = newData;
            var isNull = data.IsNullData();
            if (data.IsSpecialField())
            {
                //if (runAction == DirectAction.Update)
                //{
                //    return rpc.ExecuteUpdateAction2(data);
                //}
                if (isNull)
                {
                    var parmList = new List<object>();
                    data.data = parmList;
                    requestData = jSri.Serialize(rpc.ExecuteNormalAction(data));
                    return requestData;
                }
                _tempDataList = ExtractData(data.data[0].ToString());
                return jSri.Serialize(rpc.ExecuteCRUD(data, _tempDataList));
            }
            requestData = rpc.ExecuteNormalAction2(data);
            return requestData;

            #endregion
        }


        private JObject runActionJObject(JObject jobject, Request data, string requestData, List<Dictionary<string, string>> _tempDataList)
        {
            #region action
            var jSri = new JavaScriptSerializer();
            var rpc = new ExtAction();
            var newData = new Request();
            newData.HttpRequest = data.HttpRequest;
            newData.HttpContext = data.HttpContext;
            newData.action = jobject["action"].ToString();
            newData.method = jobject["method"].ToString();
            newData.tid = Convert.ToInt32(jobject["tid"].ToString());

            if (newData.action.IndexOf(".") > 0)
            {
                newData.action = newData.action.Split('.')[1];
            }

            var obj = Assembly.GetExecutingAssembly().CreateInstance(newData.action);

            Type thisType = obj.GetType();
            MethodInfo theMethod = thisType.GetMethod(newData.method);


            object[] mAtt = theMethod.GetCustomAttributes(false);
            DirectAction runAction = DirectAction.Null;
            foreach (var att in mAtt)
            {
                runAction = ((DirectMethodAttribute)(att)).Action;
            }

            var d = new List<string>();
            try
            {
                if (jobject["data"].ToString() != "")
                {
                    for (var i = 0; i < jobject["data"].Count() - 1; i++)
                    {
                        switch (jobject["data"][i].ToString())
                        {
                            case "page":
                                newData.page = Convert.ToInt32(jobject["data"].First["page"].ToString());
                                break;
                        }
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                MyException.ErrorNoThrowException(this, ex);
            }
            catch (Exception ex2)
            {
                MyException.Error(this, ex2);
            }
            try
            {
                if (jobject["data"].ToString() != "")
                {
                    for (var i = 0; i < jobject["data"].Count() - 1; i++)
                    {
                        switch (jobject["data"][i].ToString())
                        {
                            case "start":
                                newData.start = Convert.ToInt32(jobject["data"].First["start"].ToString());
                                break;
                        }
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                MyException.ErrorNoThrowException(this, ex);
            }
            catch (Exception ex2)
            {
                MyException.Error(this, ex2);
            }
            try
            {
                if (jobject["data"].ToString() != "")
                {
                    for (var i = 0; i < jobject["data"].Count() - 1; i++)
                    {
                        switch (jobject["data"][i].ToString())
                        {
                            case "limit":
                                newData.limit = Convert.ToInt32(jobject["data"].First["limit"].ToString());
                                break;
                        }
                    }
                }

            }
            catch (InvalidOperationException ex)
            {
                MyException.ErrorNoThrowException(this, ex);
            }
            catch (Exception ex2)
            {
                MyException.Error(this, ex2);
            }

            try
            {
                if (jobject["data"] != null)
                {
                    if (jobject["data"].ToString() != "")
                    {
                        for (var i = 0; i < jobject["data"].Count() - 1; i++)
                        {
                            switch (jobject["data"][i].ToString())
                            {
                                case "dir":
                                    newData.dir = jobject["data"].First["dir"].ToString();
                                    break;
                            }
                        }
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                MyException.ErrorNoThrowException(this, ex);
            }
            catch (Exception ex2)
            {
                MyException.Error(this, ex2);
            }
            try
            {
                //if (runAction == DirectAction.Update)
                //{
                //    d.Add(jobject["data"].First["updatedata"].ToString());
                //}
                //else
                //{
                    d.AddRange(from object tmp in (jobject["data"].First).AsJEnumerable() select tmp.ToString().Split(':') into kv where kv[0] != "\"page\"" && kv[0] != "\"start\"" && kv[0] != "\"limit\"" && kv[0] != "\"sort\"" && kv[0] != "\"dir\"" select kv[1].Replace("\"", "").Trim());
                    
                //}
            }
            catch
            {
                //if (runAction == DirectAction.Update)
                //{
                //    d.Add(jobject["data"].ToString());
                //}
                //else
                //{
                    if (jobject["data"] != null)
                    {
                        try
                        {
                            if (jobject["data"].ToString() != "")
                            {
                                foreach (object tmp in (jobject["data"]).AsJEnumerable())
                                {
                                    if ((((JValue)(tmp))).Type != JTokenType.Null)
                                    {
                                        var kv = tmp.ToString();
                                        d.Add(kv);
                                    }
                                }
                            }
                        }
                        catch (InvalidOperationException ex2)
                        {
                            MyException.ErrorNoThrowException(this, ex2);
                        }
                    }
                //}
            }

            var dd = d.Cast<object>().ToList();
            /*
             原來的程式
             * for (int i = 0; i < d.Count; i++)
            {
                dd.Add(d[i]);
            }
             */
            newData.data = dd;
            data = newData;
            data.HttpRequest = newData.HttpRequest;
            data.HttpContext = newData.HttpContext;
            var isNull = data.IsNullData();
            if (data.IsSpecialField())
            {
                //if (runAction == DirectAction.Update)
                //{
                //    return rpc.ExecuteUpdateActionJObject(data);
                //}
                if (isNull == false)
                {
                    _tempDataList = ExtractData(data.data[0].ToString());
                    return rpc.ExecuteCRUDJObject(data, _tempDataList);
                }
            }
            else
            {
                return rpc.ExecuteNormalAction2JObject(data);
            }
            return null;
            #endregion
        }

        internal string StringFy(string str)
        {
            return str.Replace("\"", "");
        }
        internal Dictionary<string, string> ExtractSingleRecord(string str)
        {
            str = str.Replace("{", "");
            str = str.Replace("}", "");
            string[] splitStr = str.Split(',');
            return splitStr.Select(s => s.Split(':')).ToDictionary(_tS => _tS[0], _tS => _tS[1]);
            /*
             原來的程式
             foreach (var s in splitStr)
            {
                var _tS = s.Split(':');
                d.Add(_tS[0], _tS[1]);
            }
            return d;
             */

        }
        internal List<Dictionary<string, string>> ExtractMultipleRecord(string str)
        {
            var dataL = new List<Dictionary<string, string>>();
            str = str.Replace("[", "");
            str = str.Replace("]", "");
            var oldStr = str;
            while (true)
            {
                int idx = oldStr.IndexOf(",{", StringComparison.Ordinal);
                string tempStr;
                if (idx != -1)
                {
                    tempStr = oldStr.Substring(0, idx);
                    dataL.Add(ExtractSingleRecord(tempStr));
                    oldStr = oldStr.Replace(tempStr + ",", "");
                }
                else
                {
                    tempStr = oldStr;
                    if ((tempStr.Length > 1) && (tempStr.IndexOf("{", StringComparison.Ordinal) != -1) && (tempStr.IndexOf("}", StringComparison.Ordinal) != -1))
                    {
                        dataL.Add(ExtractSingleRecord(tempStr));
                        break;
                    }
                }
            }

            return dataL;
        }
        internal List<Dictionary<string, string>> ExtractData(string str)
        {
            var temp = new List<Dictionary<string, string>>();
            str = str.Replace("\"", "");
            var intt = str.IndexOf("[", StringComparison.Ordinal);
            if (intt != -1)
            {
                temp = ExtractMultipleRecord(str);
            }
            else
            {
                temp.Add(ExtractSingleRecord(str));
            }
            return temp;
        }
    }
    public class ExtPool
    {
        internal class PoolResult
        {
            public string type
            {
                get;
                set;
            }
            public string name
            {
                get;
                set;
            }
            public string data
            {
                get;
                set;
            }

        }
        public string BindPool(string poolType, string name, string data)
        {
            var jSri = new JavaScriptSerializer();
            var tempRes = new PoolResult
            {
                type = poolType,
                name = name,
                data = data
            };
            var responseData = jSri.Serialize(tempRes);
            return responseData;
        }
    }
}