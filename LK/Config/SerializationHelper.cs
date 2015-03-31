using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using log4net;
using System.Reflection;
namespace LK.Config
{
    public class SerializationHelper
    {
        public static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly Dictionary<int, XmlSerializer> SerializerDict = new Dictionary<int, XmlSerializer>();
        private SerializationHelper()
        {
        }
        public static XmlSerializer GetSerializer(Type t)
        {
            try
            {
                int typeHash = t.GetHashCode();
                if (!SerializerDict.ContainsKey(typeHash))
                {
                    SerializerDict.Add(typeHash, new XmlSerializer(t));
                }
                return SerializerDict[typeHash];
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public static object Load(Type type, string filename)
        {
            try
            {
                FileStream fs = null;
                try
                {
                    fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    var serializer = new XmlSerializer(type);
                    return serializer.Deserialize(fs);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (fs != null)
                        fs.Close();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public static bool Save(object obj, string filename)
        {
            try
            {
                bool success = false;
                FileStream fs = null;
                try
                {
                    fs = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                    var serializer = new XmlSerializer(obj.GetType());
                    serializer.Serialize(fs, obj);
                    success = true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (fs != null)
                        fs.Close();
                }
                return success;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public static string Serialize(object obj)
        {
            try
            {
                string returnStr;
                XmlSerializer serializer = GetSerializer(obj.GetType());
                var ms = new MemoryStream();
                XmlTextWriter xtw = null;
                StreamReader sr = null;
                try
                {
                    xtw = new XmlTextWriter(ms, Encoding.UTF8);
                    xtw.Formatting = Formatting.Indented;
                    serializer.Serialize(xtw, obj);
                    ms.Seek(0, SeekOrigin.Begin);
                    sr = new StreamReader(ms);
                    returnStr = sr.ReadToEnd();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (xtw != null)
                        xtw.Close();
                    if (sr != null)
                        sr.Close();
                    ms.Close();
                }
                return returnStr;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public static object DeSerialize(Type type, string s)
        {
            try
            {
                byte[] b = Encoding.UTF8.GetBytes(s);
                try
                {
                    XmlSerializer serializer = GetSerializer(type);
                    return serializer.Deserialize(new MemoryStream(b));
                }
                catch (Exception ex)
                {
                    throw ex;
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