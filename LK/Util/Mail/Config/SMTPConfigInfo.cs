using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Xml;
using log4net;
using System.Reflection;
namespace LK.Util.Mail
{
    public class SMTPConfigInfo : ISMTPConfig
    {
        public static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly Timer baseConfigTimer = new Timer(15000);
        private static System.Xml.XmlDocument m_configinfo;
        private static System.Data.DataTable m_dt;
        /// <summary>
        /// 靜態構造函數初始化相應實例和定時器
        /// </summary>
        static SMTPConfigInfo()
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

        public System.Xml.XmlDocument GetBaseConfig()
        {
            return m_configinfo;
        }

        public System.Data.DataTable GetBaseConfig_DataTable()
        {
            try
            {
                if (m_configinfo.FirstChild.NextSibling != null)
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
                    m_dt.AcceptChanges();
                    foreach (XmlNode item in m_configinfo.FirstChild.NextSibling.ChildNodes)
                    {
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

        public string GetTag(string actionTag, bool alwaysReLoad)
        {
            string ret_message = "";
            m_configinfo = SMTPConfig.Init();//.changeLanguage(lan);//.LoadConfig(lan);
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

        public string GetTag(string ActionTag)
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

        public string FromEmail {
            get {
                return GetTag("FromEmail");
            }
        }

        public string IsSendMail {
            get
            {
                return GetTag("IsSendMail");
            }
        }

        public string IsSendAdminMail
        {
            get
            {
                return GetTag("IsSendAdminMail");
            }
        }

        public string AdministratorEmail
        {
            get
            {
                return GetTag("AdministratorEmail");
            }
        }

        public string IsSendDebugMail
        {
            get
            {
                return GetTag("IsSendDebugMail");
            }
        }


        public string DebugEmail
        {
            get
            {
                return GetTag("DebugEmail");
            }
        }

        public string SMTPServerHost
        {
            get
            {
                return GetTag("SMTPServerHost");
            }
        }


        public string SMTPServerPort
        {
            get
            {
                return GetTag("SMTPServerPort");
            }
        }


        public string IsSend
        {
            get
            {
                return GetTag("IsSend");
            }
        }

        public string CredentialsAccount
        {
            get
            {
                return GetTag("CredentialsAccount");
            }
        }

        public string CredentialsPassword
        {
            get
            {
                return GetTag("CredentialsPassword");
            }
        }
       
    }
}
