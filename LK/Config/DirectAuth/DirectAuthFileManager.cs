using System;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Web;
using LK.Config;
namespace LK.Config.DirectAuth
{
    internal class DirectAuthConfigFileManager : DefaultConfigFileManager
    {
        private static string focusTag = "DirectAuthFilePath";
        private static DirectAuthConfigInfo m_configinfo;
        private static DateTime m_fileoldchange;
        public static string filename;
        static DirectAuthConfigFileManager()
        {
            m_fileoldchange = File.GetLastWriteTime(ConfigFilePath);
            m_configinfo = (DirectAuthConfigInfo)DeserializeInfo(ConfigFilePath, typeof(DirectAuthConfigInfo));
        }        
        public new static IConfigInfo ConfigInfo
        {
            get { return m_configinfo; }
            set { m_configinfo = (DirectAuthConfigInfo)value; }
        }        
        public new static string ConfigFilePath
        {
            get
            {
                HttpContext context = HttpContext.Current;
                var nv = new NameValueCollection();
                if (filename == null)
                {
                    nv = (NameValueCollection) ConfigurationManager.GetSection(DefaultSectionTag);
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
                }
                return filename;
            }
        }
        public static DirectAuthConfigInfo LoadConfig()
        {
            ConfigInfo = LoadConfig(ref m_fileoldchange, ConfigFilePath, ConfigInfo);
            return ConfigInfo as DirectAuthConfigInfo;
        }
        public override bool SaveConfig()
        {
            return base.SaveConfig(ConfigFilePath, ConfigInfo);
        }
    }
}