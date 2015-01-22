using System;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Web;
using LK.Config;

using log4net;
using System.Reflection;


namespace LK.Config.Dynamic
{
    /// <summary>
    /// 基本設置訊息管理類
    /// </summary>
    public class DynamicConfig 
    {
        public static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static string _focusTag = "DynamicConfig";
        private static string focusTag = "DynamicConfig";
        /// <summary>配置文件所在路徑</summary>
        public static string filename;

        #region DynamicConfigFileManager()

        /// <summary>
        /// 初始化文件修改時間和對像實例
        /// </summary>
        static DynamicConfig()
        {
        }




        #endregion

        public static System.Xml.XmlDocument Init()
        {
            try
            {
                var a = new object();
                lock (a)
                {
                    focusTag = _focusTag;
                    System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                    doc.Load(ConfigFilePath());
                    return doc;

                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }


        #region ConfigFilePath

        private static string ConfigFilePath()
        {
            try
            {
                HttpContext context = HttpContext.Current;
                var nv = new NameValueCollection();
                var a = new object();
                lock (a)
                {
                    focusTag = _focusTag;
                }
                nv = (NameValueCollection)ConfigurationManager.GetSection("APConfigFiles");
                for (int i = 0; i < nv.AllKeys.Length; i++)
                {
                    if (nv.AllKeys[i].ToUpper() == focusTag.ToUpper())
                    {
                        filename = nv[i];
                        break;
                    }
                }
                if (context != null)
                {
                    if (filename.IndexOf("~") > -1)
                    {
                        filename = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + filename.Replace("~", "");
                            
                    }
                    else
                    {
                        filename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename);
                    }
                }
                else
                {
                    filename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename);
                }
                if (!File.Exists(filename))
                {
                    throw new Exception("發生錯誤: 沒有正確的" + focusTag + "文件");
                }
                return filename;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        #endregion
    }
}