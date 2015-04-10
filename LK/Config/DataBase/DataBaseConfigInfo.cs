using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Xml;
using log4net;
using System.Reflection;
namespace LK.Config.DataBase
{
    public class DataBaseConfigInfo : IDataBaseConfigInfo
    {
        public static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly Timer baseConfigTimer = new Timer(15000);
        private static System.Xml.XmlDocument m_configinfo;
        private static System.Data.DataTable m_dt;
        private static Object thisLock = new object();
        /// <summary>
        /// 靜態構造函數初始化相應實例和定時器
        /// </summary>
        static DataBaseConfigInfo()
        {
            m_dt = new System.Data.DataTable();
        }
        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                ResetConfig();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        /// <summary>
        /// 重設配置類實例
        /// </summary>
        public static void ResetConfig()
        {            
        }
        public  System.Xml.XmlDocument GetBaseConfig()
        {
            return m_configinfo;
        }
        public  System.Data.DataTable GetBaseConfig_DataTable()
        {
            try
            {
                if (m_configinfo.FirstChild.NextSibling != null)
                {
                    lock (thisLock)
                    {
                        m_dt = new System.Data.DataTable();
                        m_dt.Columns.Add("key");
                        m_dt.Columns.Add("value");
                        m_dt.Columns.Add("Type");
                        m_dt.Columns.Add("DataSchema");
                        m_dt.Columns.Add("Case_Sensitive");
                        m_dt.Columns.Add("where");
                        m_dt.Columns.Add("count");
                        m_dt.Columns.Add("hit");
                        m_dt.Columns.Add("rate");
                        m_dt.Columns.Add("logSQL");
                        m_dt.AcceptChanges();
                        foreach (XmlNode item in m_configinfo.FirstChild.NextSibling.ChildNodes)
                        {
                            #region
                            var newRow = m_dt.NewRow();
                            if (item.Name == "DB")
                            {
                                newRow["key"] = item.Name + "->" + item.Attributes["Name"].Value;
                                if (item.Attributes["Type"] != null)
                                {
                                    newRow["Type"] = item.Attributes["Type"].Value;
                                }
                                if (item.Attributes["DataSchema"] != null)
                                {
                                    newRow["DataSchema"] = item.Attributes["DataSchema"].Value;
                                }
                                if (item.Attributes["Case_Sensitive"] != null)
                                {
                                    newRow["Case_Sensitive"] = item.Attributes["Case_Sensitive"].Value;
                                }
                                if (item.Attributes["Where"] != null)
                                {
                                    newRow["Where"] = item.Attributes["Where"].Value;
                                }
                            }
                            else
                            {
                                newRow["key"] = item.Name;
                            }
                            newRow["value"] = (item).InnerText;
                            m_dt.Rows.Add(newRow);
                            #endregion
                        }
                        m_dt.AcceptChanges();
                    }
                }
                return m_dt;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public  string GetTag(string actionTag, bool alwaysReLoad)
        {
            string ret_message = "";
            m_configinfo = DataBaseConfig.Init();
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
        public  string GetTag(string ActionTag)
        {
            try
            {
                return GetTag(ActionTag, true);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public  string GetDB(string dataBaseName) {
            try
            {
                return GetTag("DB->" + dataBaseName);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public string GetDBType()
        {
            try
            {
                return GetTag("Type");
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public string GetDBType(string dbName)
        {
            try
            {
                m_configinfo = DataBaseConfig.Init();
                if (m_dt.Rows.Count == 0)
                {
                    GetBaseConfig_DataTable();
                }
                var rows = m_dt.Select("key = 'DB->" + dbName + "' ").First();
                if (rows["Type"].ToString() != "")
                {
                    return rows["Type"].ToString();
                }
                else
                {
                    return GetTag("Type");
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            } 
        }
        public string GetWhere(string dbName) {
            try
            {
                m_configinfo = DataBaseConfig.Init();
                if (m_dt.Rows.Count == 0)
                {
                    GetBaseConfig_DataTable();
                }
                var rows = m_dt.Select("key = 'DB->" + dbName + "' ").First();
                if (rows["where"].ToString() != "")
                {
                    return rows["where"].ToString();
                }
                else
                {
                    return GetTag("where");
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public string GetCaseSensitive() {
            try
            {
                return GetTag("Case_Sensitive");
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public string GetSchema(string dbName) {
            try
            {
                m_configinfo = DataBaseConfig.Init();
                if (m_dt.Rows.Count == 0)
                {
                    GetBaseConfig_DataTable();
                }
                var rows = m_dt.Select("key = 'DB->" + dbName + "' ").First();
                if (rows["DataSchema"].ToString() != "")
                {
                    return rows["DataSchema"].ToString();
                }
                else
                {
                    return GetTag("DataSchema");
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public string GetCaseSensitive(string dbName) {
            try
            {
                m_configinfo = DataBaseConfig.Init();
                if (m_dt.Rows.Count == 0)
                {
                    GetBaseConfig_DataTable();
                }
                var rows = m_dt.Select("key = 'DB->" + dbName + "' ").First();
                if (rows["Case_Sensitive"].ToString() != "")
                {
                    return rows["Case_Sensitive"].ToString();
                }
                else
                {
                    return GetTag("Case_Sensitive");
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
    }
}