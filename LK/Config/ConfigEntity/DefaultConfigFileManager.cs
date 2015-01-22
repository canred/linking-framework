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
        /// <summary>
        /// ���Ҧb���|�ܶq
        /// </summary>
        private static string _mConfigfilepath;
        /// <summary>
        /// �{�ɰt�m�ﹳ�ܶq
        /// </summary>
        private static IConfigInfo _mConfiginfo;
        /// <summary>
        /// ��ﹳ
        /// </summary>
        private static readonly object MLockHelper = new object();
        /// <summary>
        /// ���Ҧb���|
        /// </summary>
        public static string ConfigFilePath
        {
            get { return _mConfigfilepath; }
            set { _mConfigfilepath = value; }
        }
        /// <summary>
        /// �{�ɰt�m�ﹳ
        /// </summary>
        public static IConfigInfo ConfigInfo
        {
            get { return _mConfiginfo; }
            set { _mConfiginfo = value; }
        }
        /// <summary>
        /// �[��(�ϧǦC��)���w�ﹳ�������t�m�ﹳ
        /// </summary>
        /// <param name="fileoldchange">���[���ɶ�</param>
        /// <param name="configFilePath">�t�m���Ҧb���|</param>
        /// <param name="configinfo">�������ܶq �`:�ӰѼƥD�n�Ω�]�mm_configinfo�ܶq �M �������.GetType()</param>
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
                log.Info("================�o�ͤ@�Ӥ��z�|�����~ Start ================");
                log.Error(ex);
                log.Info("================�o�ͤ@�Ӥ��z�|�����~ End   ================");
                return null;
            }
        }
        /// <summary>
        /// �[��(�ϧǦC��)���w�ﹳ�������t�m�ﹳ
        /// </summary>
        /// <param name="fileoldchange">���[���ɶ�</param>
        /// <param name="configFilePath">�t�m���Ҧb���|(�]�A���W)</param>
        /// <param name="configinfo">�������ܶq �`:�ӰѼƥD�n�Ω�]�mm_configinfo�ܶq �M �������.GetType()</param>
        /// <param name="checkTime">�O�_�ˬd�ç�s�ǻ��i�Ӫ�"���[���ɶ�"�ܶq</param>
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
                    //��{�ǹB�椤config���o���ܤƮɫh��config���s���
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
        /// <summary>
        /// �ϧǦC�ƫ��w����
        /// </summary>
        /// <param name="configfilepath">config ��󪺸��|</param>
        /// <param name="configtype">����������</param>
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
        /// �O�s�t�m���(���k���~��)
        /// </summary>
        /// <returns></returns>
        public virtual bool SaveConfig()
        {
            return true;
        }
        /// <summary>
        /// �O�s(�ǦC��)���w���|�U���t�m���
        /// </summary>
        /// <param name="configFilePath">���w���t�m���Ҧb�����|(�]�A���W)</param>
        /// <param name="configinfo">�Q�O�s(�ǦC��)����H</param>
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