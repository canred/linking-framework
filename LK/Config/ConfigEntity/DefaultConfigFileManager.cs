using System;
using System.IO;
using log4net;
using System.Reflection;
namespace LK.Config
{
    /// <summary>
    /// ���t�m�޲z����
    /// </summary>
    public class DefaultConfigFileManager
    {
        public static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static string DefaultSectionTag = "APConfigFiles";       
        private static string _mConfigfilepath;       
        private static IConfigInfo _mConfiginfo;       
        private static readonly object MLockHelper = new object();      
        public static string ConfigFilePath
        {
            get { return _mConfigfilepath; }
            set { _mConfigfilepath = value; }
        }      
        public static IConfigInfo ConfigInfo
        {
            get { return _mConfiginfo; }
            set { _mConfiginfo = value; }
        }     
        protected static IConfigInfo LoadConfig(ref DateTime fileoldchange, string configFilePath,
                                                IConfigInfo configinfo)
        {
            try
            {
                return LoadConfig(ref fileoldchange, configFilePath, configinfo, true);
            }
            catch(Exception ex)
            {
                log.Info("================�o�ͤ@�Ӥ��z�|�����~ Start ================");
                log.Error(ex);
                log.Info("================�o�ͤ@�Ӥ��z�|�����~ End   ================");
                return null;
            }
        }        
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
                    //��{�ǹB�椤config���o���ܤƮɫh��config���s���
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
        public virtual bool SaveConfig()
        {
            return true;
        }
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