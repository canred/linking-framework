using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Xml;
using LK.UserInfo;
using log4net;
using System.Reflection;
namespace LK.UserInfo
{
    public class UserInfoConfigInfo : IUserInfoConfigInfo
    {
        public static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly Timer baseConfigTimer = new Timer(15000);
        private static System.Xml.XmlDocument m_configinfo;
        private static System.Data.DataTable m_dt;
        /// <summary>
        /// 靜態構造函數初始化相應實例和定時器
        /// </summary>
        static UserInfoConfigInfo()
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
                    m_dt = new System.Data.DataTable();
                    m_dt.Columns.Add("key");
                    m_dt.Columns.Add("value");  
                    m_dt.AcceptChanges();
                    foreach (XmlNode item in m_configinfo.FirstChild.NextSibling.ChildNodes)
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
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        public  string GetTag(string actionTag, bool alwaysReLoad)
        {
            string ret_message = "";
            m_configinfo = UserInfoConfig.Init();//.changeLanguage(lan);//.LoadConfig(lan);
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
    }
}
