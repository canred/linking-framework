using System;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;
using LK.DB.SQLCreater;
using Newtonsoft.Json.Linq;
namespace ExtDirect.Direct
{
    public class Helper
    {
        public class Store {
            public static string Output(string jsonStr, double totalCount) {
                var str = new System.Text.StringBuilder();
                str.Append("\"data\":[");
                str.Append(jsonStr);
                str.Append("]");
                string total = "\"success\": true,\"total\":" + totalCount.ToString(CultureInfo.InvariantCulture) + ",";
                return "{" + total + str + "}";
            }

            public static JObject OutputJObject(List<JObject> jobject, double totalCount)
            {
                try
                {
                    var ret = JObject.Parse("{data:[],success:true,total:" + totalCount.ToString(CultureInfo.InvariantCulture) + "}");                    
                    var jarray = new JArray();
                    foreach (var item in jobject)
                    {
                        jarray.Add(item);
                    }
                    ret["data"] = jarray;                  
                    return ret;
                }
                catch (Exception ex)
                {
                    LK.MyException.MyException.ErrorStaticClass(ex);
                    //MyException.ErrorNoThrowExceptionForStaticClass(ex);
                    return null;
                }
            }

            public static JObject OutputJObject(List<JObject> jobject, double totalCount, System.Collections.Hashtable pOtherParam)
            {
                try
                {
                    var ret = JObject.Parse("{data:[],success:true,total:" + totalCount.ToString(CultureInfo.InvariantCulture) + "}");
                    var jarray = new JArray();
                    foreach (var item in jobject)
                    {
                        jarray.Add(item);
                    }
                    ret["data"] = jarray;



                    if (pOtherParam.Count > 0)
                    {
                        foreach (var item in pOtherParam.Keys)
                        {
                            ret[item] = new JValue(pOtherParam[item]);
                        }
                    }

                    return ret;
                }
                catch (Exception ex)
                {
                    LK.MyException.MyException.ErrorStaticClass(ex);
                    //MyException.ErrorNoThrowExceptionForStaticClass(ex);
                    return null;
                }
            }

            public static JObject OutputJObject(System.Data.DataTable source,int start, int limit)
            {
                try
                {

                    var ret = JObject.Parse("{data:[],success:true,total:" + source.Rows.Count.ToString(CultureInfo.InvariantCulture) + "}");
                    var jarray = new JArray();
                    ret["data"] = LK.Util.JsonHelper.DataTable2JObject(source, start, limit);
                    return ret;
                }
                catch (Exception ex)
                {
                    LK.MyException.MyException.ErrorStaticClass(ex);
                    //MyException.ErrorNoThrowExceptionForStaticClass(ex);
                    return null;
                }
            }

            public static JObject OutputJObject(JObject jobject, double totalCount)
            {
                try
                {
                    var ret = JObject.Parse("{data:[],success:true,total:" + totalCount.ToString(CultureInfo.InvariantCulture) + "}");
                    var jarray = new JArray();
                    if (jobject != null)
                    {
                        foreach (var item in jobject)
                        {
                            jarray.Add(item);
                        }
                        ret["data"] = jarray;
                        return ret;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    LK.MyException.MyException.ErrorNoThrowExceptionForStaticClass(ex);
                    return null;
                }
            }

            public static JObject OutputJObject(JObject jobject)
            {
                try
                {
                    var ret = JObject.Parse("{data:[],success:true,total:1" + "}");
                    var jarray = new JArray();
                    if (jobject != null)
                    {

                        jarray.Add(jobject);
                        ret["data"] = jarray;
                        return ret;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    LK.MyException.MyException.ErrorNoThrowExceptionForStaticClass(ex);
                    return null;
                }
            }
        }

        public class Form {
            /// <summary>
            /// Form load 使用的正式輸出
            /// </summary>
            /// <param name="jsonStr"></param>
            /// <returns></returns>
            public static string Output(string jsonStr)
            {
                var str = new System.Text.StringBuilder();
                str.Append("{");
                str.Append("\"success\":true,");
                str.Append("\"data\":");
                str.Append(jsonStr);
                str.Append("}");
                return str.ToString();
            }


            public static JObject OutputJObject(JObject json)
            {
                var str = new System.Text.StringBuilder();
                str.Append("{");
                str.Append("\"success\":true,");
                str.Append("\"data\":{}");                
                str.Append("}");
                var ret = JObject.Parse(str.ToString());
                ret["data"]=json;
                return ret;
            }
        }
        public class Tree {
            public static string Output(string jsonStr, double totalCount)
            {
                var str = new System.Text.StringBuilder();                
                str.Append(jsonStr);
                return str.ToString();
            }

            public static JObject Output(JArray jsonArray, double totalCount)
            {                
                JObject ret =JObject.Parse("{TREE:{}}");
                ret["TREE"] = jsonArray;
                return ret;
            }
        }

        public class JObjectHelper {
            public static JObject StringOnly(string returnValue) {
                return JObject.Parse("{STRING_ONLY:\"" + returnValue + "\"}");
            }
            public static JObject CallBack(string action,string method)
            {
                //string ret = action + "_" + method + "_callback({\"success\":true,\"function_name\":\"" + action + "\"})";
                
                string actionMethod = action + "_" + method + "_callback";
                string actionMethodValue = "{\"success\":true,\"function_name\":\"" + action + "\"}";
                var jobjectActionMethodValue = JObject.Parse(actionMethodValue);
                JObject jobject = JObject.Parse("{EXTDIRECTCALLBACK:{},EXTDIRECTCALLBACK_VALUE:{}}");
                jobject["EXTDIRECTCALLBACK"] = actionMethod;
                jobject["EXTDIRECTCALLBACK_VALUE"] = jobjectActionMethodValue;
                return jobject;

            }
        }

        public class Message {
            public class Fail {
                public static string Output(Exception ex)
                {
                    var err = new System.Text.StringBuilder();
                    err.Append("{");
                    err.Append("\"success\":false,");
                    err.Append("\"message\":\"" + ex.Message.Replace("\"", "").Replace("'", "") + "\"");
                    err.Append("}");
                    return err.ToString();
                }

                public static JObject OutputJObject(Exception ex)
                {
                    var err = new System.Text.StringBuilder();
                    err.Append("{");
                    err.Append("\"success\":false,");
                    err.Append("\"message\":\"" + ex.Message.Replace("\"", "").Replace("'", "") + "\"");
                    err.Append("}");
                    var ret = JObject.Parse(err.ToString());
                    return ret;
                }
            }

            public class Success {
                public static string Output() {
                    var err = new System.Text.StringBuilder();
                    err.Append("{");
                    err.Append("\"success\":true");
                    err.Append("}");
                    return err.ToString();
                }

                public static JObject OutputJObject()
                {
                    var err = new System.Text.StringBuilder();
                    err.Append("{");
                    err.Append("\"success\":true");
                    err.Append("}");
                    return JObject.Parse(err.ToString());
                }

                public static string Output(System.Collections.Hashtable pOtherParam)
                {
                    var err = new System.Text.StringBuilder();
                    err.Append("{");
                    err.Append("\"success\":true");
                    if (pOtherParam.Count > 0)
                    {
                        err.Append(",");
                        foreach (var item in pOtherParam.Keys)
                        {
                            err.Append("\"" + item + "\":");
                            err.Append("\"" + pOtherParam[item] + "\",");
                        }
                        string ret = err.ToString().Substring(0, err.ToString().Length - 1);
                        err.Remove(0, err.ToString().Length);
                        err.Append(ret);
                    }
                    err.Append("}");
                    return err.ToString();
                }

                public static JObject OutputJObject(System.Collections.Hashtable pOtherParam)
                {

                    var err = new System.Text.StringBuilder();
                    err.Append("{");
                    err.Append("\"success\":true");
                    if (pOtherParam.Count > 0)
                    {
                        err.Append(",");
                        foreach (var item in pOtherParam.Keys)
                        {
                            err.Append("\"" + item + "\":");
                            err.Append("\"" + pOtherParam[item] + "\",");
                        }
                        string ret = err.ToString().Substring(0, err.ToString().Length - 1);
                        err.Remove(0, err.ToString().Length);
                        err.Append(ret);
                    }
                    err.Append("}");
                    return JObject.Parse(err.ToString());
                }
            }

        }


        public class Order {
            public static OrderLimit getOrderLimit(Request request, string orderColumnName, OrderLimit.OrderMethod ASCD_ESC)
            {
                try
                {
                    int start = ((request.limit * request.page) + 1) - (request.limit);
                    var orderLimit = new OrderLimit(orderColumnName, ASCD_ESC) {Start = start, Limit = 999};
                    return orderLimit;
                }
                catch
                {
                    return null;
                }
            }

            public static OrderLimit getOrderLimit(string page, string limit, string sort, string dir)
            {
                try
                {
                    int startNo = (Convert.ToInt32(limit) * Convert.ToInt32(page) + 1) - Convert.ToInt32(limit);
                    int limiNo = Convert.ToInt32(limit);
                    OrderLimit.OrderMethod ASC_DESC;
                    if (dir.ToUpper() == "ASC")
                    {
                        ASC_DESC = OrderLimit.OrderMethod.ASC;
                    }
                    else
                    {
                        ASC_DESC = OrderLimit.OrderMethod.DESC;
                    }
                    var orderLimit = new OrderLimit(sort, ASC_DESC) {Start = startNo, Limit = limiNo};

                    return orderLimit;
                }
                catch 
                {
                    return null;
                }
            }
        }

        public class Json {
            public static List<T> json2Record<T>(JObject jobject) where T : LK.DB.RecordBase, new()
            {
                var t = new List<T>();
                var schema = new T();
                var dataItem = new T();
                //var pk = schema.getPK();
                var col = schema.getAllColumn();
                JToken jData = null;
                //先找到資料的維度是
                var dataArray = 0;
                try
                {
                    if (jobject["updates"][0][0].GetType().ToString() == "Newtonsoft.Json.Linq.JObject")
                    {
                        jData = jobject["updates"][0];
                    }
                }
                catch
                {
                    try
                    {
                        if (jobject["updates"][0].GetType().ToString() == "Newtonsoft.Json.Linq.JObject")
                        {
                            jData = jobject["updates"];
                        }
                    }
                    catch
                    {
                        if (jobject["updates"]!=null)
                        {
                            if (jobject["updates"].GetType().ToString() == "Newtonsoft.Json.Linq.JObject")
                            {
                                jData = jobject["updates"];
                                dataArray = 1;
                            }
                        }
                        else
                        {
                            jData = jobject;
                            dataArray = 1; 
                        }
                    }
                }

                if (dataArray != 1)
                {
                    for (var i = 0; i < jData.Count(); i++)
                    {
                        dataItem = new T();
                        foreach (var ucol in col)
                        {
                            #region Convert

                            if (jData != null && jData[i][ucol.Name] != null)
                            {
                                if (dataItem.GetType().GetProperty(ucol.Name) != null)
                                {
                                    Type dcType = dataItem.GetType().GetProperty(ucol.Name.ToUpper()).PropertyType;
                                    if (dcType == typeof(string))
                                    {
                                        #region string
                                        dataItem.GetType().GetProperty(ucol.Name.ToUpper()).SetValue(dataItem, jData[i][ucol.Name].ToString(), null);
                                        #endregion
                                    }
                                    else if (dcType == typeof(DateTime?))
                                    {
                                        #region DateTime?
                                        if (jData[i][ucol.Name].ToString() == "")
                                        {
                                            dataItem.GetType().GetProperty(ucol.Name.ToUpper()).SetValue(dataItem, Convert.ToDateTime(jData[i][ucol.Name].ToString()), null);
                                        }
                                        else
                                        {
                                            dataItem.GetType().GetProperty(ucol.Name.ToUpper()).SetValue(dataItem, null, null);
                                        }
                                        #endregion
                                    }
                                    else if (dcType == typeof(Double?))
                                    {
                                        #region DateTime?
                                        if (jData[i][ucol.Name].ToString() == "")
                                        {
                                            dataItem.GetType().GetProperty(ucol.Name.ToUpper()).SetValue(dataItem, null, null);
                                        }
                                        else
                                        {
                                            dataItem.GetType().GetProperty(ucol.Name.ToUpper()).SetValue(dataItem, Convert.ToDouble(jData[i][ucol.Name].ToString()), null);


                                        }
                                        #endregion
                                    }
                                    else if (dcType == typeof(Decimal?))
                                    {
                                        #region Decimal
                                        try
                                        {
                                            if (jData[i][ucol.Name].ToString() == "")
                                            {
                                                dataItem.GetType().GetProperty(ucol.Name.ToUpper()).SetValue(dataItem, Convert.ToDecimal(jData[i][ucol.Name].ToString()), null);
                                            }
                                            else
                                            {
                                                dataItem.GetType().GetProperty(ucol.Name.ToUpper()).SetValue(dataItem, null, null);
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            LK.MyException.MyException.ErrorStaticClass(ex);
                                        }
                                        #endregion
                                    }
                                    else if (dcType == typeof(DateTime?))
                                    {
                                        #region DateTime
                                        try
                                        {
                                            if (jData[i][ucol.Name].ToString() == "")
                                            {
                                                dataItem.GetType().GetProperty(ucol.Name.ToUpper()).SetValue(dataItem, Convert.ToDateTime(jData[i][ucol.Name].ToString()), null);
                                            }
                                            else
                                            {
                                                dataItem.GetType().GetProperty(ucol.Name.ToUpper()).SetValue(dataItem, null, null);
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            LK.MyException.MyException.ErrorStaticClass(ex);
                                        }
                                        #endregion
                                    }
                                    else if (dcType == typeof(int?) || dcType == typeof(int))
                                    {
                                        #region DateTime
                                        try
                                        {
                                            if (jData[i][ucol.Name].ToString() != "")
                                            {
                                                dataItem.GetType().GetProperty(ucol.Name.ToUpper()).SetValue(dataItem, Convert.ToInt32(jData[i][ucol.Name].ToString()), null);
                                            }
                                            else
                                            {
                                                dataItem.GetType().GetProperty(ucol.Name.ToUpper()).SetValue(dataItem, null, null);
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            LK.MyException.MyException.ErrorStaticClass(ex);
                                        }
                                        #endregion
                                    }
                                    else if (dcType == typeof(float?))
                                    {
                                        #region DateTime
                                        try
                                        {
                                            if (jData[i][ucol.Name].ToString() == "")
                                            {
                                                dataItem.GetType().GetProperty(ucol.Name.ToUpper()).SetValue(dataItem, float.Parse(jData[i][ucol.Name].ToString()), null);
                                            }
                                            else
                                            {
                                                dataItem.GetType().GetProperty(ucol.Name.ToUpper()).SetValue(dataItem, null, null);
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            LK.MyException.MyException.ErrorStaticClass(ex);
                                        }
                                        #endregion
                                    }
                                    else if (dcType == typeof(short?))
                                    {
                                        #region DateTime
                                        try
                                        {
                                            if (jData[i][ucol.Name].ToString() == "")
                                            {
                                                dataItem.GetType().GetProperty(ucol.Name.ToUpper()).SetValue(dataItem, short.Parse(jData[i][ucol.Name].ToString()), null);
                                            }
                                            else
                                            {
                                                dataItem.GetType().GetProperty(ucol.Name.ToUpper()).SetValue(dataItem, null, null);
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            LK.MyException.MyException.ErrorStaticClass(ex);
                                        }
                                        #endregion
                                    }
                                    else if (dcType == typeof(byte?))
                                    {
                                        #region DateTime
                                        try
                                        {
                                            if (jData[i][ucol.Name].ToString() == "")
                                            {
                                                dataItem.GetType().GetProperty(ucol.Name.ToUpper()).SetValue(dataItem, Convert.ToByte(jData[i][ucol.Name].ToString()), null);
                                            }
                                            else
                                            {
                                                dataItem.GetType().GetProperty(ucol.Name.ToUpper()).SetValue(dataItem, null, null);
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            LK.MyException.MyException.ErrorStaticClass(ex);
                                        }
                                        #endregion
                                    }
                                    else if (dcType == typeof(ushort?))
                                    {
                                        #region DateTime
                                        try
                                        {
                                            if (jData[i][ucol.Name].ToString() == "")
                                            {
                                                dataItem.GetType().GetProperty(ucol.Name.ToUpper()).SetValue(dataItem, ushort.Parse(jData[i][ucol.Name].ToString()), null);
                                            }
                                            else
                                            {
                                                dataItem.GetType().GetProperty(ucol.Name.ToUpper()).SetValue(dataItem, null, null);
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            LK.MyException.MyException.ErrorStaticClass(ex);
                                        }
                                        #endregion
                                    }
                                    else if (dcType == typeof(uint?))
                                    {
                                        #region DateTime
                                        try
                                        {
                                            if (jData[i][ucol.Name].ToString() == "")
                                            {
                                                dataItem.GetType().GetProperty(ucol.Name.ToUpper()).SetValue(dataItem, Convert.ToUInt32(jData[i][ucol.Name].ToString()), null);
                                            }
                                            else
                                            {
                                                dataItem.GetType().GetProperty(ucol.Name.ToUpper()).SetValue(dataItem, null, null);
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            LK.MyException.MyException.ErrorStaticClass(ex);
                                        }
                                        #endregion
                                    }
                                    else if (dcType == typeof(bool?))
                                    {
                                        #region DateTime
                                        try
                                        {
                                            if (jData[i][ucol.Name].ToString() == "")
                                            {
                                                dataItem.GetType().GetProperty(ucol.Name.ToUpper()).SetValue(dataItem, Convert.ToBoolean(jData[i][ucol.Name].ToString()), null);
                                            }
                                            else
                                            {
                                                dataItem.GetType().GetProperty(ucol.Name.ToUpper()).SetValue(dataItem, null, null);
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            LK.MyException.MyException.ErrorStaticClass(ex);
                                        }
                                        #endregion
                                    }
                                    else if (dcType == typeof(TimeSpan?))
                                    {
                                        #region DateTime
                                        try
                                        {
                                            if (jData[i][ucol.Name].ToString() == "")
                                            {
                                                dataItem.GetType().GetProperty(ucol.Name.ToUpper()).SetValue(dataItem, TimeSpan.Parse(jData[i][ucol.Name].ToString()), null);
                                            }
                                            else
                                            {
                                                dataItem.GetType().GetProperty(ucol.Name.ToUpper()).SetValue(dataItem, null, null);
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            LK.MyException.MyException.ErrorStaticClass(ex);
                                        }
                                        #endregion
                                    }

                                }
                            }
                            #endregion
                        }
                        t.Add(dataItem);
                    }
                }
                else if (dataArray == 1)
                {
                    //第一個維度就是資料時
                    dataItem = new T();
                    foreach (var ucol in col)
                    {
                        #region Convert
                        if (jData != null && jData[ucol.Name] != null)
                        {
                            if (dataItem.GetType().GetProperty(ucol.Name) != null)
                            {
                                Type dcType = dataItem.GetType().GetProperty(ucol.Name.ToUpper()).PropertyType;
                                if (dcType == typeof(string))
                                {
                                    #region string
                                    dataItem.GetType().GetProperty(ucol.Name.ToUpper()).SetValue(dataItem, jData[ucol.Name].ToString(), null);
                                    #endregion
                                }
                                else if (dcType == typeof(DateTime?))
                                {
                                    #region DateTime?
                                    if (jData[ucol.Name].ToString() == "")
                                    {
                                        dataItem.GetType().GetProperty(ucol.Name.ToUpper()).SetValue(dataItem, Convert.ToDateTime(jData[ucol.Name].ToString()), null);
                                    }
                                    else
                                    {
                                        dataItem.GetType().GetProperty(ucol.Name.ToUpper()).SetValue(dataItem, null, null);
                                    }
                                    #endregion
                                }
                                else if (dcType == typeof(Double?))
                                {
                                    #region Double?
                                    if (jData[ucol.Name].ToString() == "")
                                    {
                                        dataItem.GetType().GetProperty(ucol.Name.ToUpper()).SetValue(dataItem, null, null);
                                    }
                                    else
                                    {
                                        dataItem.GetType().GetProperty(ucol.Name.ToUpper()).SetValue(dataItem, Convert.ToDouble(jData[ucol.Name].ToString()), null);


                                    }
                                    #endregion
                                }
                                else if (dcType == typeof(Decimal?))
                                {
                                    #region Decimal
                                    try
                                    {
                                        if (jData[ucol.Name].ToString() == "")
                                        {
                                            dataItem.GetType().GetProperty(ucol.Name.ToUpper()).SetValue(dataItem, Convert.ToDecimal(jData[ucol.Name].ToString()), null);
                                        }
                                        else
                                        {
                                            dataItem.GetType().GetProperty(ucol.Name.ToUpper()).SetValue(dataItem, null, null);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        LK.MyException.MyException.ErrorStaticClass(ex);
                                    }
                                    #endregion
                                }
                                else if (dcType == typeof(DateTime?))
                                {
                                    #region DateTime
                                    try
                                    {
                                        if (jData[ucol.Name].ToString() == "")
                                        {
                                            dataItem.GetType().GetProperty(ucol.Name.ToUpper()).SetValue(dataItem, Convert.ToDateTime(jData[ucol.Name].ToString()), null);
                                        }
                                        else
                                        {
                                            dataItem.GetType().GetProperty(ucol.Name.ToUpper()).SetValue(dataItem, null, null);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        LK.MyException.MyException.ErrorStaticClass(ex);
                                    }
                                    #endregion
                                }
                                else if (dcType == typeof(int?) || dcType == typeof(int))
                                {
                                    #region DateTime
                                    try
                                    {
                                        if (jData[ucol.Name].ToString() != "")
                                        {
                                            dataItem.GetType().GetProperty(ucol.Name.ToUpper()).SetValue(dataItem, Convert.ToInt32(jData[ucol.Name].ToString()), null);
                                        }
                                        else
                                        {
                                            dataItem.GetType().GetProperty(ucol.Name.ToUpper()).SetValue(dataItem, null, null);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        LK.MyException.MyException.ErrorStaticClass(ex);
                                    }
                                    #endregion
                                }
                                else if (dcType == typeof(float?))
                                {
                                    #region DateTime
                                    try
                                    {
                                        if (jData[ucol.Name].ToString() == "")
                                        {
                                            dataItem.GetType().GetProperty(ucol.Name.ToUpper()).SetValue(dataItem, float.Parse(jData[ucol.Name].ToString()), null);
                                        }
                                        else
                                        {
                                            dataItem.GetType().GetProperty(ucol.Name.ToUpper()).SetValue(dataItem, null, null);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        LK.MyException.MyException.ErrorStaticClass(ex);
                                    }
                                    #endregion
                                }
                                else if (dcType == typeof(short?))
                                {
                                    #region DateTime
                                    try
                                    {
                                        if (jData[ucol.Name].ToString() == "")
                                        {
                                            dataItem.GetType().GetProperty(ucol.Name.ToUpper()).SetValue(dataItem, short.Parse(jData[ucol.Name].ToString()), null);
                                        }
                                        else
                                        {
                                            dataItem.GetType().GetProperty(ucol.Name.ToUpper()).SetValue(dataItem, null, null);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        LK.MyException.MyException.ErrorStaticClass(ex);
                                    }
                                    #endregion
                                }
                                else if (dcType == typeof(byte?))
                                {
                                    #region DateTime
                                    try
                                    {
                                        if (jData[ucol.Name].ToString() == "")
                                        {
                                            dataItem.GetType().GetProperty(ucol.Name.ToUpper()).SetValue(dataItem, Convert.ToByte(jData[ucol.Name].ToString()), null);
                                        }
                                        else
                                        {
                                            dataItem.GetType().GetProperty(ucol.Name.ToUpper()).SetValue(dataItem, null, null);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        LK.MyException.MyException.ErrorStaticClass(ex);
                                    }
                                    #endregion
                                }
                                else if (dcType == typeof(ushort?))
                                {
                                    #region DateTime
                                    try
                                    {
                                        if (jData[ucol.Name].ToString() == "")
                                        {
                                            dataItem.GetType().GetProperty(ucol.Name.ToUpper()).SetValue(dataItem, ushort.Parse(jData[ucol.Name].ToString()), null);
                                        }
                                        else
                                        {
                                            dataItem.GetType().GetProperty(ucol.Name.ToUpper()).SetValue(dataItem, null, null);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        LK.MyException.MyException.ErrorStaticClass(ex);
                                    }
                                    #endregion
                                }
                                else if (dcType == typeof(uint?))
                                {
                                    #region DateTime
                                    try
                                    {
                                        if (jData[ucol.Name].ToString() == "")
                                        {
                                            dataItem.GetType().GetProperty(ucol.Name.ToUpper()).SetValue(dataItem, Convert.ToUInt32(jData[ucol.Name].ToString()), null);
                                        }
                                        else
                                        {
                                            dataItem.GetType().GetProperty(ucol.Name.ToUpper()).SetValue(dataItem, null, null);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        LK.MyException.MyException.ErrorStaticClass(ex);
                                    }
                                    #endregion
                                }
                                
                                else if (dcType == typeof(bool?))
                                {
                                    #region DateTime
                                    try
                                    {
                                        if (jData[ucol.Name].ToString() == "")
                                        {
                                            dataItem.GetType().GetProperty(ucol.Name.ToUpper()).SetValue(dataItem, Convert.ToBoolean(jData[ucol.Name].ToString()), null);
                                        }
                                        else
                                        {
                                            dataItem.GetType().GetProperty(ucol.Name.ToUpper()).SetValue(dataItem, null, null);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        LK.MyException.MyException.ErrorStaticClass(ex);
                                    }
                                    #endregion
                                }
                                else if (dcType == typeof(TimeSpan?))
                                {
                                    #region DateTime
                                    try
                                    {
                                        if (jData[ucol.Name].ToString() == "")
                                        {
                                            dataItem.GetType().GetProperty(ucol.Name.ToUpper()).SetValue(dataItem, TimeSpan.Parse(jData[ucol.Name].ToString()), null);
                                        }
                                        else
                                        {
                                            dataItem.GetType().GetProperty(ucol.Name.ToUpper()).SetValue(dataItem, null, null);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        LK.MyException.MyException.ErrorStaticClass(ex);
                                    }
                                    #endregion
                                }

                            }
                        }
                        #endregion
                    }
                    t.Add(dataItem);
                }
                return t;
            }

        }
       

        
       
    }

}
