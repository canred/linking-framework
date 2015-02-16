using System;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Web;
using LKWebTemplate.Parameter.Config;
using LK.Config;


namespace LKWebTemplate.Parameter.Config
{
    internal class ParemterConfigFileManager : DefaultConfigFileManager
    {
        private static string focusTag = "ParemterFilePath";

        private static ParemterConfigInfo m_configinfo;

        private static DateTime m_fileoldchange;

        public static string filename;

        static ParemterConfigFileManager()
        {
            m_fileoldchange = File.GetLastWriteTime(ConfigFilePath);
            m_configinfo = (ParemterConfigInfo)DeserializeInfo(ConfigFilePath, typeof(ParemterConfigInfo));
        }

        public new static IConfigInfo ConfigInfo
        {
            get { return m_configinfo; }
            set { m_configinfo = (ParemterConfigInfo)value; }
        }

        public new static string ConfigFilePath
        {
            get
            {
                HttpContext context = HttpContext.Current;
                var nv = new NameValueCollection();

                if (filename == null)
                {
                    nv = (NameValueCollection)ConfigurationManager.GetSection(DefaultSectionTag);
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
                            filename = context.Server.MapPath(filename);
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

        public static ParemterConfigInfo LoadConfig()
        {
            ConfigInfo = LoadConfig(ref m_fileoldchange, ConfigFilePath, ConfigInfo);
            return ConfigInfo as ParemterConfigInfo;
        }

        public override bool SaveConfig()
        {
            return base.SaveConfig(ConfigFilePath, ConfigInfo);
        }
    }
}