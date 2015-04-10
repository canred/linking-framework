using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
namespace LK.Util
{
    public class JsonHelper
    {
        /// <summary>
        /// JSON序列化
        /// </summary>
        public static string JsonSerializer<T>(T t)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, t);
            string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            return jsonString;
        }
        public static string RecordBaseSerializer<T>(T t)
            where T : LK.DB.RecordBase
        {
            string jsonString = "{";
            foreach (var col in t.getAllColumn())
            {
                jsonString += String.Format("\"{0}\":", col.Name);
                object value = t.getPropValue(t, col.Name);
                if (value != null)
                {
                    if (value.GetType().Name == "DateTime")
                    {
                        DateTime dateValue = System.Convert.ToDateTime(value);
                        jsonString += "\"" + dateValue.ToString("yyyy/MM/dd HH:mm:ss") + "\"";
                    }
                    else
                    {
                        jsonString += Enquote(value.ToString());
                    }
                }
                else
                {
                    jsonString += "\"\"";
                }
                jsonString += ",";
            }
            if (jsonString.EndsWith(","))
            {
                jsonString = jsonString.Substring(0, jsonString.Length - 1);
            }
            jsonString += "}";
            return jsonString;
        }

        public static JObject DataTable2JObject(DataTable result, int start, int limit)
        {
            try
            {
                JObject ret = new JObject();
                System.Collections.Hashtable ht = new System.Collections.Hashtable();
                foreach (System.Data.DataColumn dc in result.Columns)
                {
                    if (!ht.ContainsKey(dc.ColumnName))
                    {
                        ht.Add(dc.ColumnName, dc.DataType.Name);
                    }
                };
                ret = JObject.Parse(DataTable2Json(ht,result, start, limit));
                return ret;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static JObject RecordBaseJObject<T>(T t)
           where T : LK.DB.RecordBase
        {
            try
            {
                //here 2 
                string jsonString = "{";
                foreach (var col in t.getAllColumn())
                {
                    jsonString += String.Format("\"{0}\":", col.Name);
                    object value = t.getPropValue(t, col.Name);
                    if (value != null)
                    {
                        if (value.GetType().Name == "DateTime")
                        {
                            DateTime dateValue = System.Convert.ToDateTime(value);
                            jsonString += "\"" + dateValue.ToString("yyyy/MM/dd HH:mm:ss") + "\"";                            
                        }
                        else
                        {
                            jsonString += Enquote(value.ToString());
                        }
                    }
                    else
                    {
                        jsonString += "\"\"";
                    }
                    jsonString += ",";
                }
                if (jsonString.EndsWith(","))
                {
                    jsonString = jsonString.Substring(0, jsonString.Length - 1);
                }
                jsonString += "}";
                JObject obj = JObject.Parse(jsonString);
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static JObject RecordBaseJObject<T>(T t, System.Collections.Hashtable htColumn)
           where T : LK.DB.RecordBase
        {
            try
            {
                StringBuilder jsonString = new StringBuilder();
                jsonString.Append("{");
                var lastIsFlag = false;
                foreach (System.Collections.DictionaryEntry col in htColumn)
                {
                    jsonString.Append(String.Format("\"{0}\":", col.Key.ToString()));
                    object value = t.getPropValue(t, col.Key.ToString());
                    if (value != null)
                    {
                        if (col.Value.ToString() == "DateTime")
                        {
                            DateTime dateValue = System.Convert.ToDateTime(value);
                            jsonString.Append("\"" + dateValue.ToString("yyyy/MM/dd HH:mm:ss") + "\"");
                        }
                        else
                        {
                            jsonString.Append(Enquote(value.ToString()));
                        }
                    }
                    else
                    {
                        jsonString.Append("\"\"");
                    }
                    jsonString.Append(",");
                    lastIsFlag = true;
                }  
                if (lastIsFlag)
                {
                    jsonString.Remove(jsonString.Length - 1, 1);
                }
                jsonString.Append("}");
                JObject obj = JObject.Parse(jsonString.ToString());
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string Enquote(string s)
        {
            if (s == null || s.Length == 0)
            {
                return "\"\"";
            }
            char c;
            int i;
            int len = s.Length;
            StringBuilder sb = new StringBuilder(len + 4);
            string t;
            sb.Append('"');
            for (i = 0; i < len; i += 1)
            {
                c = s[i];
                if ((c == '\\') || (c == '"'))
                {
                    sb.Append('\\');
                    sb.Append(c);
                }
                else if (c == '\b')
                    sb.Append("\b");
                else if (c == '\t')
                    sb.Append("\t");
                else if (c == '\n')
                    sb.Append("\n");
                else if (c == '\f')
                    sb.Append("\f");
                else if (c == '\r')
                    sb.Append("\r");
                else
                {
                    if (c < ' ')
                    {
                        t = new string(c, 1);
                        t = "000" + int.Parse(t, System.Globalization.NumberStyles.HexNumber);
                        sb.Append("\\u" + t.Substring(t.Length - 4));
                    }
                    else
                    {
                        sb.Append(c);
                    }
                }
            }
            sb.Append('"');
            return sb.ToString();
        }
        public static string RecordBaseListSerializer<T>(IList<T> t)
            where T : LK.DB.RecordBase
        {
            string jdata = "";
            foreach (var item in t)
            {
                jdata += LK.Util.JsonHelper.RecordBaseSerializer(item) + ",";
            }

            if (jdata.EndsWith(","))
            {
                jdata = jdata.Substring(0, jdata.Length - 1);
            }
            RecordBaseListJObject(t);
            return jdata;
        }
        public static List<JObject> RecordBaseListJObject<T>(IList<T> t)
            where T : LK.DB.RecordBase
        {
            //here 1 
            List<JObject> obj = new List<JObject>();
            System.Collections.Hashtable htCol = new System.Collections.Hashtable();
            htCol = getTableColumnToHt<T>(t);
            foreach (var item in t)
            {   
                var jobject = LK.Util.JsonHelper.RecordBaseJObject(item,htCol);
                obj.Add(jobject);
            }
            return obj;
        }

        private static System.Collections.Hashtable getTableColumnToHt( LK.DB.RecordBase c) {
            System.Collections.Hashtable ht = new System.Collections.Hashtable();
           
            foreach (var col in c.getAllColumn()) {
                ht.Add(col.Name,col.GetType().Name);
            }
            return ht;
        }

        private static System.Collections.Hashtable getTableColumnToHt<T>(IList<T> t) 
            where T : LK.DB.RecordBase
        {
            System.Collections.Hashtable ht = new System.Collections.Hashtable();
            if (t.Count == 0)
            {
                return ht;
            };
            foreach (var col in t.First().getAllColumn())
            {
                ht.Add(col.Name, col.GetType().Name);
            }
            return ht;
        }


        public static string DataTableSerializer(System.Data.DataTable t)
        {
            string jdata = "";
            foreach (System.Data.DataRow item in t.Rows)
            {
                jdata += LK.Util.JsonHelper.DataRowSerializer(item) + ",";
            }
            if (jdata.EndsWith(","))
            {
                jdata = jdata.Substring(0, jdata.Length - 1);
            }
            return jdata;
        }
        public static JArray DataTableSerializerJArray(System.Data.DataTable t)
        {
            JArray jarray = new JArray();
            foreach (System.Data.DataRow item in t.Rows)
            {
                jarray.Add(LK.Util.JsonHelper.DataRowSerializerJObject(item));
            }
            return jarray;
        }
        public static string DataRowSerializer(System.Data.DataRow t)
        {
            string jsonString = "{";
            foreach (System.Data.DataColumn col in t.Table.Columns)
            {
                jsonString += String.Format("\"{0}\":", col.ColumnName);
                if (t[col.ColumnName].GetType() == typeof(bool))
                {
                    #region 當欄位是boolean時特別處理，不然tree長不出checkbox
                    object value = t[col.ColumnName].ToString();
                    if (value != null)
                    {
                        value = value.ToString().Replace(Environment.NewLine.ToString(), "\\r\\n");
                        jsonString += "" + value.ToString().ToLower() + "";
                    }
                    else
                    {
                        jsonString += "\"\"";
                    }
                    jsonString += ",";
                    #endregion
                }
                else
                {
                    #region 一般
                    object value = t[col.ColumnName].ToString();
                    if (value != null)
                    {
                        value = value.ToString().Replace(Environment.NewLine.ToString(), "\\r\\n");
                        value = value.ToString().Replace("\\", "\\\\");
                        jsonString += "\"" + value + "\"";
                    }
                    else
                    {
                        jsonString += "\"\"";
                    }
                    jsonString += ",";
                    #endregion
                }
            }
            if (jsonString.EndsWith(","))
            {
                jsonString = jsonString.Substring(0, jsonString.Length - 1);
            }
            jsonString += "}";
            return jsonString;
        }

        public static JObject DataRowSerializerJObject(System.Data.DataRow t)
        {
            return JObject.Parse(DataRowSerializer(t));
        }
        /// <summary>
        /// JSON反序列化
        /// </summary>
        public static T JsonDeserialize<T>(string jsonString)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T obj = (T)ser.ReadObject(ms);
            return obj;
        }
        public static string List2Json<T>(System.Collections.Generic.IList<T> result, int start, int limit)
        where T : System.Data.DataRow
        {
            string ret = "";
            ret = "{";
            ret += "'TotalResults':" + result.Count + ",";
            ret += "'TotalPages':" + ((result.Count / 10) + 1) + ",'Item':[";
            int i = 0;
            foreach (var item in result)
            {
                if (i >= start && i < start + limit)
                {
                    ret += "{";
                    #region 在欄位中找Mappint
                    foreach (System.Data.DataColumn dc in item.Table.Columns)
                    {
                        if (item[dc.ColumnName].ToString().Trim().Length == 0)
                        {
                            ret += "'" + dc.ColumnName.ToUpper() + "'" + ":'',";
                        }
                        else
                        {
                            var colType = item.Table.Columns[dc.ColumnName].DataType.Name;
                            switch (colType)
                            {
                                case "String":
                                    ret += "'" + dc.ColumnName.ToUpper() + "'" + ":'" + System.Security.SecurityElement.Escape(item[dc.ColumnName].ToString()).Replace("\r\n", "\\r\\n") + "',";
                                    break;
                                case "DateTime":
                                    var _dataTime = DateTime.MinValue;
                                    var isDateTime = DateTime.TryParse(item[dc.ColumnName].ToString(), out _dataTime);
                                    if (isDateTime)
                                    {
                                        _dataTime = Convert.ToDateTime(item[dc.ColumnName].ToString());
                                        ret += "'" + dc.ColumnName.ToUpper() + "'" + ":'" + System.Security.SecurityElement.Escape(_dataTime.ToString("yyyy/MM/dd HH:mm:ss")) + "',";
                                    }
                                    else
                                    {
                                        ret += "'" + dc.ColumnName.ToUpper() + "'" + ":'" + System.Security.SecurityElement.Escape(item[dc.ColumnName].ToString()) + "',";
                                    }
                                    break;
                                case "Decimal":
                                    ret += "'" + dc.ColumnName.ToUpper() + "'" + ":" + System.Security.SecurityElement.Escape(item[dc.ColumnName].ToString()) + ",";
                                    break;
                                default:
                                    ret += "'" + dc.ColumnName.ToUpper() + "'" + ":'" + System.Security.SecurityElement.Escape(item[dc.ColumnName].ToString()) + "',";
                                    break;
                            }
                            if (result[0].Table.Constraints.Count > 0)
                            {
                                #region 加入PK資訊
                                foreach (var ct in ((System.Data.UniqueConstraint)(result[0].Table.Constraints[0])).Columns)
                                {
                                    if (ct.ToString().ToLower() == dc.ColumnName.ToUpper())
                                    {
                                        switch (colType)
                                        {
                                            case "String":
                                                ret += "'PK_" + dc.ColumnName.ToUpper() + "':'" + System.Security.SecurityElement.Escape(item[dc.ColumnName].ToString()) + "',";
                                                break;
                                            case "DateTime":
                                                var _dataTime = DateTime.MinValue;
                                                var isDateTime = DateTime.TryParse(item[dc.ColumnName].ToString(), out _dataTime);
                                                if (isDateTime)
                                                {
                                                    _dataTime = Convert.ToDateTime(item[dc.ColumnName].ToString());
                                                    ret += "'PK_" + dc.ColumnName.ToUpper() + "':'" + System.Security.SecurityElement.Escape(_dataTime.ToString("yyyy/MM/dd HH:mm:ss")) + "',";
                                                }
                                                else
                                                {
                                                    ret += "'PK_" + dc.ColumnName.ToUpper() + "':'" + System.Security.SecurityElement.Escape(item[dc.ColumnName].ToString()) + "',";
                                                }
                                                break;
                                            case "Decimal":
                                                ret += "'PK_" + dc.ColumnName.ToUpper() + "':" + System.Security.SecurityElement.Escape(item[dc.ColumnName].ToString()) + ",";
                                                break;
                                            default:
                                                ret += "'PK_" + dc.ColumnName.ToUpper() + "':'" + System.Security.SecurityElement.Escape(item[dc.ColumnName].ToString()) + "',";
                                                break;
                                        }
                                        break;
                                    }
                                }
                                #endregion
                            }
                        }
                    }
                    #endregion  在欄位中找Mappint
                    if (ret.EndsWith(","))
                        ret = ret.Remove(ret.Length - 1, 1);
                    ret += "},";
                }
                i++;
            }
            if (ret.EndsWith(","))
                ret = ret.Remove(ret.Length - 1, 1);
            ret += "]}";
            ret = ret.Replace("\n", "\\n");
            ret = ret.Replace("\t", "\\t");
            ret = ret.Replace("\r", "\\r");
            ret = ret.Replace("\r\n", "\\r\\n");
            ret = ret.Replace(Environment.NewLine.ToString(), "");
            ret = cleanString(ret);
            return ret;
        }
        public static string DataTable2Json(System.Collections.Hashtable htCol,DataTable result, int start, int limit)
        {
            string ret = "";
            ret = "{";
            ret += "'TotalResults':" + result.Rows.Count + ",";
            ret += "'TotalPages':" + ((result.Rows.Count / limit) + 1) + ",'Item':[";
            int i = 0;
           
            foreach (DataRow item in result.Rows)
            {
                if (i >= start && i < start + limit)
                {
                    ret += "{";
                    #region 在欄位中找Mappint
                    foreach (System.Collections.DictionaryEntry col in htCol) {
                        #region

                        if ( item[col.Key.ToString()].ToString().Trim().Length == 0)
                        {
                            ret += "'" + col.Key.ToString().ToUpper() + "'" + ":'',";
                        }
                        else
                        {
                            var colType = col.Value.ToString();// result.Columns[dc.ColumnName].DataType.Name;
                            switch (colType)
                            {
                                case "String":
                                    ret += "'" + col.Key.ToString().ToUpper() + "'" + ":'" + System.Security.SecurityElement.Escape(item[col.Key.ToString()].ToString()).Replace("\r\n", "\\r\\n") + "',";
                                    break;
                                case "DateTime":
                                    var _dataTime = DateTime.MinValue;
                                    var isDateTime = DateTime.TryParse(item[col.Key.ToString()].ToString(), out _dataTime);
                                    if (isDateTime)
                                    {
                                        _dataTime = Convert.ToDateTime(item[col.Key.ToString()].ToString());
                                        ret += "'" + col.Key.ToString().ToUpper() + "'" + ":'" + System.Security.SecurityElement.Escape(_dataTime.ToString("yyyy/MM/dd HH:mm:ss")) + "',";
                                    }
                                    else
                                    {
                                        ret += "'" + col.Key.ToString().ToUpper() + "'" + ":'" + System.Security.SecurityElement.Escape(item[col.Key.ToString()].ToString()) + "',";
                                    }
                                    break;
                                case "Decimal":
                                    ret += "'" + col.Key.ToString().ToUpper() + "'" + ":" + System.Security.SecurityElement.Escape(item[col.Key.ToString()].ToString()) + ",";
                                    break;
                                default:
                                    ret += "'" + col.Key.ToString().ToUpper() + "'" + ":'" + System.Security.SecurityElement.Escape(item[col.Key.ToString()].ToString()) + "',";
                                    break;
                            }
                            if (result.Constraints.Count > 0)
                            {
                                #region 加入PK資訊
                                foreach (var ct in ((System.Data.UniqueConstraint)(result.Constraints[0])).Columns)
                                {
                                    if (ct.ToString().ToLower() == col.Key.ToString().ToUpper())
                                    {
                                        switch (colType)
                                        {
                                            case "String":
                                                ret += "'PK_" + col.Key.ToString().ToUpper() + "':'" + System.Security.SecurityElement.Escape(item[col.Key.ToString()].ToString()) + "',";

                                                break;
                                            case "DateTime":
                                                var _dataTime = DateTime.MinValue;
                                                var isDateTime = DateTime.TryParse(item[col.Key.ToString()].ToString(), out _dataTime);
                                                if (isDateTime)
                                                {
                                                    _dataTime = Convert.ToDateTime(item[col.Key.ToString()].ToString());
                                                    ret += "'PK_" + col.Key.ToString().ToUpper() + "':'" + System.Security.SecurityElement.Escape(_dataTime.ToString("yyyy/MM/dd HH:mm:ss")) + "',";
                                                }
                                                else
                                                {
                                                    ret += "'PK_" + col.Key.ToString().ToUpper() + "':'" + System.Security.SecurityElement.Escape(item[col.Key.ToString()].ToString()) + "',";

                                                }
                                                break;
                                            case "Decimal":
                                                ret += "'PK_" + col.Key.ToString().ToUpper() + "':" + System.Security.SecurityElement.Escape(item[col.Key.ToString()].ToString()) + ",";
                                                break;
                                            default:
                                                ret += "'PK_" + col.Key.ToString().ToUpper() + "':'" + System.Security.SecurityElement.Escape(item[col.Key.ToString()].ToString()) + "',";
                                                break;
                                        }
                                        break;
                                    }
                                }
                                #endregion
                            }
                        }
                        #endregion
                    }
                    //foreach (System.Data.DataColumn dc in result.Columns)
                    //{
                       
                    //}
                    #endregion  在欄位中找Mappint
                    if (ret.EndsWith(","))
                        ret = ret.Remove(ret.Length - 1, 1);
                    ret += "},";
                }
                i++;
            }
            if (ret.EndsWith(","))
                ret = ret.Remove(ret.Length - 1, 1);
            ret += "]}";
            ret = ret.Replace("\n", "\\n");
            ret = ret.Replace("\t", "\\t");
            ret = ret.Replace("\r", "\\r");
            ret = ret.Replace("\r\n", "\\r\\n");
            ret = ret.Replace(Environment.NewLine.ToString(), "");
            ret = cleanString(ret);
            return ret;
        }

        public static string Row2Json<T>(T result, int start, int limit)
        where T : System.Data.DataRow
        {
            string ret = "";
            ret = "{";
            ret += "'TotalResults':1,";
            ret += "'TotalPages':1,'Item':[";
            var item = result;
            int i = 0;
            ret += "{";
            #region 在欄位中找Mappint
            foreach (System.Data.DataColumn dc in item.Table.Columns)
            {
                if (item[dc.ColumnName].ToString().Trim().Length == 0)
                {
                    ret += "'" + dc.ColumnName.ToUpper() + "'" + ":'',";
                }
                else
                {
                    var colType = item.Table.Columns[dc.ColumnName].DataType.Name;
                    switch (colType)
                    {
                        case "String":
                            ret += "'" + dc.ColumnName.ToUpper() + "'" + ":'" + System.Security.SecurityElement.Escape(item[dc.ColumnName].ToString()).Replace("\r\n", "\\r\\n") + "',";
                            break;
                        case "DateTime":
                            var _dataTime = DateTime.MinValue;
                            var isDateTime = DateTime.TryParse(item[dc.ColumnName].ToString(), out _dataTime);
                            if (isDateTime)
                            {
                                _dataTime = Convert.ToDateTime(item[dc.ColumnName].ToString());
                                ret += "'" + dc.ColumnName.ToUpper() + "'" + ":'" + System.Security.SecurityElement.Escape(_dataTime.ToString("yyyy/MM/dd HH:mm:ss")) + "',";
                            }
                            else
                            {
                                ret += "'" + dc.ColumnName.ToUpper() + "'" + ":'" + System.Security.SecurityElement.Escape(item[dc.ColumnName].ToString()) + "',";
                            }
                            break;
                        case "Decimal":
                            ret += "'" + dc.ColumnName.ToUpper() + "'" + ":" + System.Security.SecurityElement.Escape(item[dc.ColumnName].ToString()) + ",";
                            break;
                        default:
                            ret += "'" + dc.ColumnName.ToUpper() + "'" + ":'" + System.Security.SecurityElement.Escape(item[dc.ColumnName].ToString()) + "',";
                            break;
                    }
                    if (result.Table.Constraints.Count > 0)
                    {
                        #region 加入PK資訊
                        foreach (var ct in ((System.Data.UniqueConstraint)(result.Table.Constraints[0])).Columns)
                        {
                            if (ct.ToString().ToLower() == dc.ColumnName.ToUpper())
                            {
                                switch (colType)
                                {
                                    case "String":
                                        ret += "'PK_" + dc.ColumnName.ToUpper() + "':'" + System.Security.SecurityElement.Escape(item[dc.ColumnName].ToString()) + "',";
                                        break;
                                    case "DateTime":
                                        var _dataTime = DateTime.MinValue;
                                        var isDateTime = DateTime.TryParse(item[dc.ColumnName].ToString(), out _dataTime);
                                        if (isDateTime)
                                        {
                                            _dataTime = Convert.ToDateTime(item[dc.ColumnName].ToString());
                                            ret += "'PK_" + dc.ColumnName.ToUpper() + "':'" + System.Security.SecurityElement.Escape(_dataTime.ToString("yyyy/MM/dd HH:mm:ss")) + "',";
                                        }
                                        else
                                        {
                                            ret += "'PK_" + dc.ColumnName.ToUpper() + "':'" + System.Security.SecurityElement.Escape(item[dc.ColumnName].ToString()) + "',";
                                        }
                                        break;
                                    case "Decimal":
                                        ret += "'PK_" + dc.ColumnName.ToUpper() + "':" + System.Security.SecurityElement.Escape(item[dc.ColumnName].ToString()) + ",";
                                        break;
                                    default:
                                        ret += "'PK_" + dc.ColumnName.ToUpper() + "':'" + System.Security.SecurityElement.Escape(item[dc.ColumnName].ToString()) + "',";
                                        break;
                                }
                                break;
                            }
                        }
                        #endregion
                    }
                }
            }
            #endregion  在欄位中找Mappint
            if (ret.EndsWith(",")){
                ret = ret.Remove(ret.Length - 1, 1);
            }
            ret += "},";
            i++;
            if (ret.EndsWith(",")){
                ret = ret.Remove(ret.Length - 1, 1);
            }
            ret += "]}";
            ret = ret.Replace("\n", "\\n");
            ret = ret.Replace("\t", "\\t");
            ret = ret.Replace("\r", "\\r");
            ret = ret.Replace("\r\n", "\\r\\n");
            ret = ret.Replace(Environment.NewLine.ToString(), "");
            ret = cleanString(ret);
            return ret;
        }

        public static string List2Json<T>(System.Collections.Generic.IList<T> result)
       where T : System.Data.DataRow
        {
            string ret = "";
            ret = "{";
            ret += "'TotalResults':" + result.Count + ",";
            ret += "'TotalPages':" + ((result.Count / 10) + 1) + ",'Item':[";
            int i = 0;
            foreach (var item in result)
            {
                ret += "{";
                #region 在欄位中找Mappint
                foreach (System.Data.DataColumn dc in item.Table.Columns)
                {
                    if (item[dc.ColumnName].ToString().Trim().Length == 0)
                    {
                        ret += "'" + dc.ColumnName.ToUpper() + "'" + ":'',";
                    }
                    else
                    {
                        var colType = item.Table.Columns[dc.ColumnName].DataType.Name;
                        switch (colType)
                        {
                            case "String":
                                ret += "'" + dc.ColumnName.ToUpper() + "'" + ":'" + System.Security.SecurityElement.Escape(item[dc.ColumnName].ToString()).Replace("\r\n", "\\r\\n") + "',";
                                break;
                            case "DateTime":
                                var _dataTime = DateTime.MinValue;
                                var isDateTime = DateTime.TryParse(item[dc.ColumnName].ToString(), out _dataTime);
                                if (isDateTime)
                                {
                                    _dataTime = Convert.ToDateTime(item[dc.ColumnName].ToString());
                                    ret += "'" + dc.ColumnName.ToUpper() + "'" + ":'" + System.Security.SecurityElement.Escape(_dataTime.ToString("yyyy/MM/dd HH:mm:ss")) + "',";
                                }
                                else
                                {
                                    ret += "'" + dc.ColumnName.ToUpper() + "'" + ":'" + System.Security.SecurityElement.Escape(item[dc.ColumnName].ToString()) + "',";
                                }
                                break;
                            case "Decimal":
                                ret += "'" + dc.ColumnName.ToUpper() + "'" + ":" + System.Security.SecurityElement.Escape(item[dc.ColumnName].ToString()) + ",";
                                break;
                            default:
                                ret += "'" + dc.ColumnName.ToUpper() + "'" + ":'" + System.Security.SecurityElement.Escape(item[dc.ColumnName].ToString()) + "',";
                                break;
                        }
                        if (result[0].Table.Constraints.Count > 0)
                        {
                            #region 加入PK資訊
                            foreach (var ct in ((System.Data.UniqueConstraint)(result[0].Table.Constraints[0])).Columns)
                            {
                                if (ct.ToString().ToLower() == dc.ColumnName.ToUpper())
                                {
                                    switch (colType)
                                    {
                                        case "String":
                                            ret += "'PK_" + dc.ColumnName.ToUpper() + "':'" + System.Security.SecurityElement.Escape(item[dc.ColumnName].ToString()) + "',";
                                            break;
                                        case "DateTime":
                                            var _dataTime = DateTime.MinValue;
                                            var isDateTime = DateTime.TryParse(item[dc.ColumnName].ToString(), out _dataTime);
                                            if (isDateTime)
                                            {
                                                _dataTime = Convert.ToDateTime(item[dc.ColumnName].ToString());
                                                ret += "'PK_" + dc.ColumnName.ToUpper() + "':'" + System.Security.SecurityElement.Escape(_dataTime.ToString("yyyy/MM/dd HH:mm:ss")) + "',";
                                            }
                                            else
                                            {
                                                ret += "'PK_" + dc.ColumnName.ToUpper() + "':'" + System.Security.SecurityElement.Escape(item[dc.ColumnName].ToString()) + "',";
                                            }
                                            break;
                                        case "Decimal":
                                            ret += "'PK_" + dc.ColumnName.ToUpper() + "':" + System.Security.SecurityElement.Escape(item[dc.ColumnName].ToString()) + ",";
                                            break;
                                        default:
                                            ret += "'PK_" + dc.ColumnName.ToUpper() + "':'" + System.Security.SecurityElement.Escape(item[dc.ColumnName].ToString()) + "',";
                                            break;
                                    }
                                    break;
                                }
                            }
                            #endregion
                        }
                    }
                }
                #endregion  在欄位中找Mappint
                if (ret.EndsWith(",")){
                    ret = ret.Remove(ret.Length - 1, 1);
                }
                ret += "},";
                i++;
            }
            if (ret.EndsWith(",")){
                ret = ret.Remove(ret.Length - 1, 1);
            }
            ret += "]}";
            ret = ret.Replace("\n", "\\n");
            ret = ret.Replace("\t", "\\t");
            ret = ret.Replace("\r", "\\r");
            ret = ret.Replace("\r\n", "\\r\\n");
            ret = ret.Replace(Environment.NewLine.ToString(), "");
            ret = cleanString(ret);
            return ret;
        }
        private static string cleanString(string newStr)
        {
            string tempStr = newStr.Replace((char)13, (char)0);
            return tempStr.Replace((char)10, (char)0);
        }
        public static string ReportSpecialFlag(string ret)
        {
            ret = ret.Replace("\n", "\\n");
            ret = ret.Replace("\t", "\\t");
            ret = ret.Replace("\r", "\\r");
            ret = ret.Replace("\r\n", "\\r\\n");
            ret = ret.Replace(Environment.NewLine.ToString(), "");
            return ret;
        }
    }
}
