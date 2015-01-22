using System;
using System.IO;
using log4net;
using System.Reflection;
namespace LK.Config
{
    /// <summary>
    /// 文件配置管理基類
    /// </summary>
    public class DefaultConfigFileManager
    {
        public static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static string DefaultSectionTag = "APConfigFiles";
        /// <summary>
        /// 文件所在路徑變量
        /// </summary>
        private static string _mConfigfilepath;
        /// <summary>
        /// 臨時配置對像變量
        /// </summary>
        private static IConfigInfo _mConfiginfo;
        /// <summary>
        /// 鎖對像
        /// </summary>
        private static readonly object MLockHelper = new object();
        /// <summary>
        /// 文件所在路徑
        /// </summary>
        public static string ConfigFilePath
        {
            get { return _mConfigfilepath; }
            set { _mConfigfilepath = value; }
        }
        /// <summary>
        /// 臨時配置對像
        /// </summary>
        public static IConfigInfo ConfigInfo
        {
            get { return _mConfiginfo; }
            set { _mConfiginfo = value; }
        }
        /// <summary>
        /// 加載(反序列化)指定對像類型的配置對像
        /// </summary>
        /// <param name="fileoldchange">文件加載時間</param>
        /// <param name="configFilePath">配置文件所在路徑</param>
        /// <param name="configinfo">相應的變量 注:該參數主要用於設置m_configinfo變量 和 獲取類型.GetType()</param>
        /// <returns></returns>
        protected static IConfigInfo LoadConfig(ref DateTime fileoldchange, string configFilePath,
                                                IConfigInfo configinfo)
        {
            try
            {
                return LoadConfig(ref fileoldchange, configFilePath, configinfo, true);
            }
            catch(Exception ex)
            {
                log.Info("================發生一個不理會的錯誤 Start ================");
                log.Error(ex);
                log.Info("================發生一個不理會的錯誤 End   ================");
                return null;
            }
        }
        /// <summary>
        /// 加載(反序列化)指定對像類型的配置對像
        /// </summary>
        /// <param name="fileoldchange">文件加載時間</param>
        /// <param name="configFilePath">配置文件所在路徑(包括文件名)</param>
        /// <param name="configinfo">相應的變量 注:該參數主要用於設置m_configinfo變量 和 獲取類型.GetType()</param>
        /// <param name="checkTime">是否檢查並更新傳遞進來的"文件加載時間"變量</param>
        /// <returns></returns>
        protected static IConfigInfo LoadConfig(ref DateTime fileoldchange, string configFilePath,
                                                IConfigInfo configinfo, bool checkTime)
        {
            try
            {
                _mConfigfilepath = configFilePath;
                _mConfiginfo = configinfo;
                if (checkTime)
                {
                    DateTime mFilenewchange = File.GetLastWriteTime(configFilePath);
                    //當程序運行中config文件發生變化時則對config重新賦值
                    if (fileoldchange != mFilenewchange)
                    {
                        fileoldchange = mFilenewchange;
                        lock (MLockHelper)
                        {
                            _mConfiginfo = DeserializeInfo(configFilePath, configinfo.GetType());
                        }
                    }
                }
                else
                {
                    lock (MLockHelper)
                    {
                        _mConfiginfo = DeserializeInfo(configFilePath, configinfo.GetType());
                    }
                }
                return _mConfiginfo;
            }
            catch (Exception ex) {
                log.Error(ex);
                throw ex;
            }
        }
        protected static System.Xml.XmlDocument LoadConfigXML(ref DateTime fileoldchange, string configFilePath, bool checkTime)
        {
            try
            {
                _mConfigfilepath = configFilePath;
                if (checkTime)
                {
                    DateTime mFilenewchange = File.GetLastWriteTime(configFilePath);
                    //當程序運行中config文件發生變化時則對config重新賦值
                    if (fileoldchange != mFilenewchange)
                    {
                        fileoldchange = mFilenewchange;
                        lock (MLockHelper)
                        {
                            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                            doc.Load(configFilePath);
                            return doc;
                        }
                    }
                }
                else
                {
                    lock (MLockHelper)
                    {
                        System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                        doc.Load(configFilePath);
                        return doc;
                    }
                }
                return new System.Xml.XmlDocument();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        /// <summary>
        /// 反序列化指定的類
        /// </summary>
        /// <param name="configfilepath">config 文件的路徑</param>
        /// <param name="configtype">相應的類型</param>
        /// <returns></returns>
        public static IConfigInfo DeserializeInfo(string configfilepath, Type configtype)
        {
            try
            {
                return (IConfigInfo)SerializationHelper.Load(configtype, configfilepath);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        /// <summary>
        /// 保存配置實例(虛方法需繼承)
        /// </summary>
        /// <returns></returns>
        public virtual bool SaveConfig()
        {
            return true;
        }
        /// <summary>
        /// 保存(序列化)指定路徑下的配置文件
        /// </summary>
        /// <param name="configFilePath">指定的配置文件所在的路徑(包括文件名)</param>
        /// <param name="configinfo">被保存(序列化)的對象</param>
        /// <returns></returns>
        public bool SaveConfig(string configFilePath, IConfigInfo configinfo)
        {
            try
            {
                return SerializationHelper.Save(configinfo, configFilePath);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
    }
}