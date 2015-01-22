using System;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Web;
using LK.Config;
using log4net;
using System.Reflection;


namespace LK.ExceptionParser
{
    /// <summary>
    /// �򥻳]�m�T���޲z��
    /// </summary>
    public class Parser 
    {
        public static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static string _focusTag = "ExceptionParser";
        private static string focusTag = "ExceptionParser";
                /// <summary>�t�m���Ҧb���|</summary>
        public static string filename;
        #region Parser()
        /// <summary>
        /// ��l�Ƥ��ק�ɶ��M�ﹳ���
        /// </summary>
        static Parser()
        {
        }
        public static System.Xml.XmlDocument changeLanguage(string lan)
        {
            var a = new object();
            lock (a)
            {
                focusTag = getLanguageFocusTag(lan);
                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                doc.Load(ConfigFilePath2(lan));
                return doc;   
            }
        }
        public static string getLanguageFocusTag(string lan)
        {
            try
            {
                string ret = "";
                ret = lan + _focusTag;
                return ret;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        #endregion
        #region ConfigFilePath
        private static string ConfigFilePath2(string lan)
        {
            try
            {
                HttpContext context = HttpContext.Current;
                var nv = new NameValueCollection();
                var a = new object();
                lock (a)
                {
                    focusTag = getLanguageFocusTag(lan);
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
                    throw new Exception("�o�Ϳ��~: �S�����T��" + focusTag + "���");
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