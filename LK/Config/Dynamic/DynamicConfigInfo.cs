using System.Timers;
using LK.Config;
using System.Collections.Generic;
using System.Linq;
using log4net;
using System.Reflection;
namespace LK.Config.Dynamic
{
    public class DynamicConfigInfo
    {
        private static readonly Timer baseConfigTimer = new Timer(ConfigBase.RefreshTime);
        public static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static System.Xml.XmlDocument m_configinfo;
        private static System.Data.DataTable m_dt;
        static DynamicConfigInfo()
        {
            try
            {
                m_dt = new System.Data.DataTable();
            }
            catch (System.Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
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
            try
            {
                return m_configinfo;
            }
            catch (System.Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public static System.Data.DataTable GetBaseConfig_DataTable()
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
        public static string GetMessage(string actionTag, bool alwaysReLoad)
        {
            string ret_message = "";
            m_configinfo = DynamicConfig.Init();
            try
            {                
                if (m_dt.Rows.Count == 0 || alwaysReLoad)
                {
                    GetBaseConfig_DataTable();
                }
                foreach (System.Data.DataRow dr in m_dt.Rows)
                {
                    if (dr["key"].ToString().ToLower() == actionTag.ToLower())
                    {
                        ret_message = dr["value"].ToString();
                        break;
                    }
                }
                return ret_message;
            }
            catch
            {
                return "";
            }
        }
        public static string GetMessage(string ActionTag)
        {
            try
            {
                return GetMessage(ActionTag, true);
            }
            catch (System.Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
    }
}