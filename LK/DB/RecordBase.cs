using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using log4net;

namespace LK.DB
{
    [Serializable]
    public abstract class RecordBase 
    {
        public static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public List<LK.Attribute.ColumnName> getPK()
        {
            try
            {
                List<LK.Attribute.ColumnName> list = new List<LK.Attribute.ColumnName>();
                PropertyInfo[] porps = this.GetType().GetProperties();

                foreach (PropertyInfo prop in porps)
                {
                    object[] attrs = prop.GetCustomAttributes(typeof(LK.Attribute.ColumnName), false);
                    if (attrs.Length == 1)
                    {
                        var isPK = ((LK.Attribute.ColumnName)attrs[0]).IsPK;
                        if (isPK)
                        {
                            list.Add(((LK.Attribute.ColumnName)attrs[0]));
                        }
                    }
                }
                list.Sort(CompareRecord);
                return list;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        public List<LK.Attribute.ColumnName> getColumnWithOutPK()
        {
            try
            {
                List<LK.Attribute.ColumnName> list = new List<LK.Attribute.ColumnName>();
                PropertyInfo[] porps = this.GetType().GetProperties();

                foreach (PropertyInfo prop in porps)
                {
                    object[] attrs = prop.GetCustomAttributes(typeof(LK.Attribute.ColumnName), false);
                    if (attrs.Length == 1)
                    {
                        var isPK = ((LK.Attribute.ColumnName)attrs[0]).IsPK;
                        if (isPK == false)
                        {
                            list.Add(((LK.Attribute.ColumnName)attrs[0]));
                        }
                    }
                }
                list.Sort(CompareRecord);
                return list;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        /// <summary>
        /// 由Rcord物件中取出指定欄位的值
        /// 找不到指定欄位時回傳null
        /// 找到指定欄位時，但值為null還是回傳null
        /// </summary>
        /// <param name="columnName">欄位名稱</param>
        /// <returns>string</returns>
        public string getFieldStringValue(string columnName) {
            string ret = "";
            try
            {
                List<LK.Attribute.ColumnName> list = new List<LK.Attribute.ColumnName>();
                PropertyInfo[] porps = this.GetType().GetProperties();
                foreach (PropertyInfo prop in porps)
                {
                    object[] attrs = prop.GetCustomAttributes(typeof(LK.Attribute.ColumnName), false);
                    if (attrs.Length == 1)
                    {
                        if (((LK.Attribute.ColumnName)attrs[0]).Name.ToUpper() == columnName.ToUpper()) {
                            /*判斷取出來的值是null時，回傳空字串。因為是getFieldStringValue*/
                            if (prop.GetValue(this, null) == null)
                            {
                                return "";
                            }
                            else
                            {
                                ret = prop.GetValue(this, null).ToString();
                            }
                            break;
                        }                       
                    }
                }               
                return ret;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        /// <summary>
        /// 由Rcord物件中取出指定欄位的值
        /// 找不到指定欄位時回傳null
        /// 找到指定欄位時，但值為null還是回傳null 
        /// </summary>
        /// <typeparam name="T">預轉換的Type。已可nullable物件為主DateTime?,String?...</typeparam>
        /// <param name="columnName">欄位名稱</param>
        /// <returns>指定的物件類型</returns>
        public T getFieldValue<T>(string columnName) where T: new()
        {
            T ret = new T();           
            try
            {
                List<LK.Attribute.ColumnName> list = new List<LK.Attribute.ColumnName>();
                PropertyInfo[] porps = this.GetType().GetProperties();
                foreach (PropertyInfo prop in porps)
                {
                    object[] attrs = prop.GetCustomAttributes(typeof(LK.Attribute.ColumnName), false);
                    if (attrs.Length == 1)
                    {
                        if (((LK.Attribute.ColumnName)attrs[0]).Name.ToUpper() == columnName.ToUpper())
                        {
                            ret = (T)prop.GetValue(this, null);
                            break;
                        }
                    }
                }
                return ret;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        public double getFieldDoubleValue(string columnName) 
        {
            double ret = 0.0;
            try
            {
                List<LK.Attribute.ColumnName> list = new List<LK.Attribute.ColumnName>();
                PropertyInfo[] porps = this.GetType().GetProperties();
                foreach (PropertyInfo prop in porps)
                {
                    object[] attrs = prop.GetCustomAttributes(typeof(LK.Attribute.ColumnName), false);
                    if (attrs.Length == 1)
                    {
                        if (((LK.Attribute.ColumnName)attrs[0]).Name.ToUpper() == columnName.ToUpper())
                        {
                            ret = System.Convert.ToDouble(prop.GetValue(this, null));
                            break;
                        }
                    }
                }
                return ret;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }


        public List<LK.Attribute.ColumnName> getAllColumn()
        {
            try
            {
                List<LK.Attribute.ColumnName> list = new List<LK.Attribute.ColumnName>();
                PropertyInfo[] porps = this.GetType().GetProperties();

                foreach (PropertyInfo prop in porps)
                {
                    object[] attrs = prop.GetCustomAttributes(typeof(LK.Attribute.ColumnName), false);
                    if (attrs.Length == 1)
                    {
                        list.Add(((LK.Attribute.ColumnName)attrs[0]));
                    }
                }
                list.Sort(CompareRecord);
                return list;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        private static int CompareRecord(LK.Attribute.ColumnName x, LK.Attribute.ColumnName y)
        {
            if (x == null)
            {
                if (y == null)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            else
            {                
                if (y == null)                
                {
                    return 1;
                }
                else
                {                   
                    int retval = x.Name.CompareTo(y.Name);

                    if (retval != 0)
                    {                   
                        return retval;
                    }
                    else
                    {                       
                        return x.Name.CompareTo(y.Name);
                    }
                }
            }
        }

        public string getTableName() {
            try
            {
                var attrs = this.GetType().GetCustomAttributes(typeof(LK.Attribute.TableView), false);
                if (attrs.Length == 1)
                {
                    return ((LK.Attribute.TableView)attrs[0]).getName();
                }
                return null;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        public object getPropValue(RecordBase rb, string name)
        {
            try
            {
                object ret = new object();
                System.Reflection.PropertyInfo info = rb.GetType().GetProperty(name);
                if (info == null)
                {
                    return null;
                }
                ret = info.GetValue(rb, null);
                return ret;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        /// <summary>
        /// 深度的Clone
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public T Clone<T>(T source)
        {
            try
            {
                if (!typeof(T).IsSerializable)
                {
                    throw new ArgumentException("The type must be serializable.", "source");
                }

                if (Object.ReferenceEquals(source, null))
                {
                    return default(T);
                }
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new MemoryStream();
                using (stream)
                {
                    formatter.Serialize(stream, source);
                    stream.Seek(0, SeekOrigin.Begin);
                    return (T)formatter.Deserialize(stream);
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
