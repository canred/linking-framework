using System.Timers;
using System.Collections.Generic;
using System.Linq;
using LK.Config;
using log4net;
using System.Reflection;
namespace LK.ExceptionHandler.Parser
{    
    public class ParserInfo
    {
        public static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly Timer baseConfigTimer = new Timer(ConfigBase.RefreshTime);
        private static System.Xml.XmlDocument m_configinfo;
        private static System.Data.DataTable m_dt;      
        static ParserInfo()
        {
            m_dt = new System.Data.DataTable();
        }
        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                ResetConfig();
            }
            catch (System.Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }      
        public static void ResetConfig()
        {
        }
        public static System.Xml.XmlDocument GetBaseConfig()
        {           
            return m_configinfo;
        }
        public static System.Data.DataTable GetBaseConfig_DataTable(string lan)
        {
            try
            {
                if (m_configinfo.FirstChild.NextSibling != null)
                {
                    m_dt = new System.Data.DataTable();
                    m_dt.Columns.Add("key");
                    m_dt.Columns.Add("value");
                    m_dt.Columns.Add("count");
                    m_dt.Columns.Add("hit");
                    m_dt.Columns.Add("rate");
                    m_dt.AcceptChanges();
                    foreach (System.Xml.XmlNode item in m_configinfo.FirstChild.NextSibling.ChildNodes)
                    {
                        var newRow = m_dt.NewRow();
                        newRow["key"] = item.Name;
                        newRow["value"] = (item).InnerText;
                        m_dt.Rows.Add(newRow);
                    }
                    m_dt.AcceptChanges();
                }
                return m_dt;
            }
            catch (System.Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public static System.Exception ParserMessage(string language , System.Exception ex,string pageName,string actionTag,bool alwaysReLoad) {
            string ret_message = "";
            ret_message = ex.Message;
            var lan = language.ToUpper();
            m_configinfo = ExceptionParser.Parser.changeLanguage(lan);
            try
            {
                if (m_dt.Rows.Count == 0 || alwaysReLoad)
                {
                    GetBaseConfig_DataTable(lan);
                }
                foreach (System.Data.DataRow dr in m_dt.Rows)
                {
                    List<string> allKey = new List<string>();
                    string parserKey = dr["key"].ToString();
                    while (parserKey.IndexOf("___") > 0)
                    {
                        allKey.Add(parserKey.Substring(0, parserKey.IndexOf("___")));
                        parserKey = parserKey.Substring(parserKey.IndexOf("___") + "___".Length);
                    }
                    if (parserKey.Length > 0)
                    {
                        allKey.Add(parserKey);
                    }
                    if (actionTag.Trim().Length > 0)
                    {
                        allKey.Add(actionTag);
                    }
                    dr["count"] = allKey.Count;
                    foreach (var item in allKey)
                    {
                        if (ex.Message.ToLower().IndexOf(item.ToLower()) >= 0)
                        {
                            try
                            {
                                dr["hit"] = System.Convert.ToInt32(dr["hit"].ToString()) + 1;
                            }
                            catch
                            {
                                dr["hit"] = 1;
                            }
                        }
                    }
                }
                foreach (System.Data.DataRow item in m_dt.Rows)
                {
                    if (item["count"] != System.DBNull.Value && item["hit"] != System.DBNull.Value)
                    {
                        item["rate"] = System.Convert.ToInt32(item["hit"]) / System.Convert.ToInt32(item["count"]);
                    }
                }
                m_dt.DefaultView.Sort = "rate,count";
                System.Data.DataTable ret_dt = m_dt.DefaultView.ToTable();
                if (ret_dt.Rows[ret_dt.Rows.Count - 1]["rate"] != System.DBNull.Value && 0 < System.Convert.ToInt32(ret_dt.Rows[ret_dt.Rows.Count - 1]["rate"]))
                    ret_message = ret_dt.Rows[ret_dt.Rows.Count - 1]["value"].ToString();
                return new System.Exception(ret_message, ex);
            }
            catch (System.Exception ex2)
            {
                log.Error(ex2);
                throw ex;
            }            
        }
        public static System.Exception ParserMessage(string lan,string sourcePath, System.Exception ex)
        {
            try
            {
                return ParserMessage(lan, ex, sourcePath, "", true);
            }
            catch (System.Exception ex2)
            {
                log.Error(ex2);
                throw ex;
            }
        }
        public static System.Exception ParserMessage(string lan, string sourcePath, string ActionTag, System.Exception ex)
        {
            try
            {
                return ParserMessage(lan, ex, sourcePath, ActionTag, true);
            }
            catch (System.Exception ex2)
            {
                log.Error(ex2);
                throw ex;
            }
        }
    }
}