using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LK.Config.DataBase;
using LK.Attribute;
using log4net;
using System.Reflection;
namespace LK.DB
{
    /*  注意 *************************************************
        這隻程式的Like(),SLike,ELike(),BLike四個方法要被抽像化
        因為Oracle , MsSQL 的Like寫法不同的關系
     * *******************************************************
     */
    public class SQLCondition : IDisposable
    {
        public static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        /*操作那一個Table*/
        string _TABLE_ = "";
        /*組語法使用的*/
        string CSQL = "";
        /*內含所有的Parameter*/
        List<object> _parameter = new List<object>();
        /*要從attribute中取得是操作用一個資料庫*/
        //LK.Config.DataBase.IDataBaseConfigInfo _databaseConfigInfo = null;
        /*原資料庫是有大小寫敏感的設定*/
        bool isCase_Sensitive = false;
        string dbType = "";
        /*DataParamert的站位符*/
        private string parameterFlag = ":######";
        /// <summary>
        /// 取得要執行的SQL語句
        /// </summary>
        /// <returns></returns>
        public string SQL()
        {
            return CSQL;
        }
        /// <summary>
        /// 取得使用者傳入的Parameter
        /// </summary>
        /// <returns></returns>
        public List<object> PARAMETER()
        {
            return _parameter;
        }
        #region Dispose
        void IDisposable.Dispose()
        {
            this.Dispose(true);
            System.GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    try
                    {}
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        #endregion
        /// <summary>
        /// SQLCondition的建構子之一
        /// </summary>
        /// <param name="modelObj"></param>
        public SQLCondition(object modelObj)
        {
            try
            {
                DataBaseConfigInfo ret = new DataBaseConfigInfo();
                if (ret.GetCaseSensitive().ToUpper() == "TRUE")
                {
                    isCase_Sensitive = true;
                }
                else
                {
                    isCase_Sensitive = false;
                }
                var attrs = modelObj.GetType().GetCustomAttributes(typeof(LK.Attribute.LkDataBase), false);
                string dbName = null;
                if (attrs.Length == 1)
                {
                    dbName = ((LK.Attribute.LkDataBase)(attrs[0])).getDataBase();
                }
                else
                {
                    throw new Exception("你的物件內並沒有DataBase關閉的Attribute!");
                }
                dbType = ret.GetDBType(dbName);
                var attrs2 = modelObj.GetType().GetCustomAttributes(typeof(TableView), false);
                if (attrs2.Length == 1)
                {
                    _TABLE_ = ((TableView)(attrs2[0])).getName();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        /// <summary>
        /// 取得SQLCondition要操作的Table名稱
        /// </summary>
        /// <returns></returns>
        public string TABLE()
        {
            return _TABLE_;
        }
        /// <summary>
        /// 取定Where語句中的等於
        /// </summary>
        /// <param name="columnName">欄位名稱</param>
        /// <param name="value">值</param>
        /// <param name="caseSensitive">是否大小寫區分；false為不區分</param>
        /// <returns></returns>
        public virtual SQLCondition Equal(string columnName, object value, bool caseSensitive)
        {
            try
            {
                if (caseSensitive == true)
                {
                    CSQL += " " + columnName + "=" + parameterFlag + " ";
                }
                else
                {
                    CSQL += " upper(" + columnName + ")=upper(" + parameterFlag + ") ";
                }
                _parameter.Add(value);
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        /// <summary>
        /// 取定Where語句中的等於
        /// </summary>
        /// <param name="columnName">欄位名稱</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public virtual SQLCondition Equal(string columnName, object value)
        {
            try
            {
                if (isCase_Sensitive == true)
                {
                    CSQL += " " + columnName + "=" + parameterFlag + " ";
                }
                else
                {
                    CSQL += " upper(" + columnName + ")=upper(" + parameterFlag + ") ";
                }
                _parameter.Add(value);
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        /// <summary>
        /// 取定Where語句中的等於(一律乎略大小寫)
        /// </summary>
        /// <param name="columnName">欄位名稱</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public virtual SQLCondition iEqual(string columnName, object value)
        {
            try
            {
                CSQL += " upper(" + columnName + ")=upper(" + parameterFlag + ") ";
                _parameter.Add(value);
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        /// <summary>
        /// 取定Where語句中的 AND 
        /// </summary>
        /// <returns></returns>
        public virtual SQLCondition And()
        {
            try
            {
                CSQL += " AND ";
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        /// <summary>
        /// 取定Where語句中的不等於
        /// </summary>
        /// <param name="columnName">欄位名稱</param>
        /// <param name="value">值</param>
        /// <param name="caseSensitive">是否大小寫區分；false為不區分</param>
        /// <returns></returns>
        public virtual SQLCondition NotEqual(string columnName, object value, bool caseSensitive)
        {
            try
            {
                if (caseSensitive == true)
                {
                    CSQL += " " + columnName + "<>" + parameterFlag + " ";
                }
                else
                {
                    CSQL += " upper(" + columnName + ")<>upper(" + parameterFlag + ") ";
                }
                _parameter.Add(value);
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        /// <summary>
        /// 取定Where語句中的不等於
        /// </summary>
        /// <param name="columnName">欄位名稱</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public virtual SQLCondition NotEqual(string columnName, object value)
        {
            try
            {
                if (isCase_Sensitive == true)
                {
                    CSQL += " " + columnName + "<>" + parameterFlag + " ";
                }
                else
                {
                    CSQL += " upper(" + columnName + ")<>upper(" + parameterFlag + ") ";
                }
                _parameter.Add(value);
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        /// <summary>
        /// 取定Where語句中的不等於(忽略大小寫)
        /// </summary>
        /// <param name="columnName">欄位名稱</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public virtual SQLCondition iNotEqual(string columnName, object value)
        {
            try
            {
                CSQL += " upper(" + columnName + ")<>upper(" + parameterFlag + ") ";
                _parameter.Add(value);
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        /// <summary>
        /// 取定Where語句中的 (  
        /// </summary>
        /// <returns></returns>
        public virtual SQLCondition L()
        {
            try
            {
                CSQL += " ( ";
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        /// <summary>
        /// 取定Where語句中的 )  
        /// </summary>
        /// <returns></returns>
        public virtual SQLCondition R()
        {
            try
            {
                CSQL += " ) ";
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        /// <summary>
        /// 取定Where語句中的 OR   
        /// </summary>
        /// <returns></returns>
        public virtual SQLCondition Or()
        {
            try
            {
                CSQL += " OR ";
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        /// <summary>
        /// 取定Where語句中的 IS NULL  
        /// </summary>
        /// <returns></returns>
        public virtual SQLCondition IsNull(string columnName)
        {
            try
            {
                CSQL += " " + columnName + " IS NULL ";
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        /// <summary>
        /// 取定Where語句中的 IS NOT NULL  
        /// </summary>
        /// <returns></returns>
        public virtual SQLCondition IsNotNull(string columnName)
        {
            try
            {
                CSQL += " " + columnName + " IS NOT NULL ";
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        /// <summary>
        /// 取定Where語句中的大於等於
        /// </summary>
        /// <param name="columnName">欄位名稱</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public virtual SQLCondition OverEqual(string columnName, object value)
        {
            try
            {
                if (isCase_Sensitive == true)
                {
                    CSQL += " " + columnName + ">=" + parameterFlag + " ";
                }
                else
                {
                    CSQL += " upper(" + columnName + ")>=upper(" + parameterFlag + ") ";
                }
                _parameter.Add(value);
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        /// <summary>
        /// 取定Where語句中的大於等於(忽略大小寫)
        /// </summary>
        /// <param name="columnName">欄位名稱</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public virtual SQLCondition iOverEqual(string columnName, object value)
        {
            try
            {
                CSQL += " upper(" + columnName + ")>=upper(" + parameterFlag + ") ";
                _parameter.Add(value);
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        /// <summary>
        /// 取定Where語句中的大於
        /// </summary>
        /// <param name="columnName">欄位名稱</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public virtual SQLCondition OverThen(string columnName, object value)
        {
            try
            {
                if (isCase_Sensitive == true)
                {
                    CSQL += " " + columnName + ">" + parameterFlag + " ";
                }
                else
                {
                    CSQL += " upper(" + columnName + ")>upper(" + parameterFlag + ") ";
                }
                _parameter.Add(value);
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        /// <summary>
        /// 取定Where語句中的大於(忽略大小寫)
        /// </summary>
        /// <param name="columnName">欄位名稱</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public virtual SQLCondition iOverThen(string columnName, object value)
        {
            try
            {
                CSQL += " upper(" + columnName + ")>upper(" + parameterFlag + ") ";
                _parameter.Add(value);
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        /// <summary>
        /// 取定Where語句中的小於等於
        /// </summary>
        /// <param name="columnName">欄位名稱</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public virtual SQLCondition LessEqual(string columnName, object value)
        {
            try
            {
                if (isCase_Sensitive == true)
                {
                    CSQL += " " + columnName + "<=" + parameterFlag + " ";
                }
                else
                {
                    CSQL += " upper(" + columnName + ")<=upper(" + parameterFlag + ") ";
                }
                _parameter.Add(value);
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        /// <summary>
        /// 取定Where語句中的小於等於(忽略大小寫)
        /// </summary>
        /// <param name="columnName">欄位名稱</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public virtual SQLCondition iLessEqual(string columnName, object value)
        {
            try
            {

                CSQL += " upper(" + columnName + ")<=upper(" + parameterFlag + ") ";
                _parameter.Add(value);
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        /// <summary>
        /// 取定Where語句中的小於
        /// </summary>
        /// <param name="columnName">欄位名稱</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public virtual SQLCondition LessThen(string columnName, object value)
        {
            try
            {
                if (isCase_Sensitive == true)
                {
                    CSQL += " " + columnName + "<" + parameterFlag + " ";
                }
                else
                {
                    CSQL += " upper(" + columnName + ")<upper(" + parameterFlag + ") ";
                }
                _parameter.Add(value);
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        /// <summary>
        /// 取定Where語句中的小於(忽略大小寫)
        /// </summary>
        /// <param name="columnName">欄位名稱</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public virtual SQLCondition iLessThen(string columnName, object value)
        {
            try
            {
                CSQL += " upper(" + columnName + ")<upper(" + parameterFlag + ") ";
                _parameter.Add(value);
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        /// <summary>
        /// 取定Where語句中的like
        /// </summary>
        /// <param name="columnName">欄位名稱</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public virtual SQLCondition Like(string columnName, object value)
        {
            try
            {
                if (isCase_Sensitive == true)
                {
                    if (dbType.ToUpper() == "ORACLE")
                    {
                        CSQL += " " + columnName + " LIKE '%' || " + parameterFlag + " || '%' ";
                    }
                    else if (dbType.ToUpper() == "MSSQL")
                    {
                        CSQL += " " + columnName + " LIKE '%' + " + parameterFlag + " + '%' ";
                    }
                    else if (dbType.ToUpper() == "MYSQL")
                    {
                        CSQL += " " + columnName + " LIKE  " + parameterFlag + "  ";
                    }
                }
                else
                {
                    if (dbType.ToUpper() == "ORACLE")
                    {
                        CSQL += " upper(" + columnName + ") LIKE upper('%' || " + parameterFlag + " || '%' ) ";
                    }
                    else if (dbType.ToUpper() == "MSSQL")
                    {
                        CSQL += " upper(" + columnName + ") LIKE upper('%' + " + parameterFlag + " + '%' ) ";
                    }
                    else if (dbType.ToUpper() == "MYSQL")
                    {
                        CSQL += " upper(" + columnName + ") LIKE upper( " + parameterFlag + "  ) ";
                    }
                }
                if (dbType.ToUpper() == "MYSQL")
                {
                    _parameter.Add("%" + value + "%");
                }
                else
                {
                    _parameter.Add(value);
                }
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        /// <summary>
        /// 取定Where語句中的like(忽略大小寫)
        /// </summary>
        /// <param name="columnName">欄位名稱</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public virtual SQLCondition iLike(string columnName, object value)
        {
            try
            {
                if (dbType.ToUpper() == "ORACLE")
                {
                    CSQL += " upper(" + columnName + ") LIKE upper('%' || " + parameterFlag + " || '%' ) ";
                }
                else if (dbType.ToUpper() == "MSSQL")
                {
                    CSQL += " upper(" + columnName + ") LIKE upper('%' + " + parameterFlag + " + '%' ) ";
                }
                else if (dbType.ToUpper() == "MYSQL")
                {
                    CSQL += " upper(" + columnName + ") LIKE upper( " + parameterFlag + "  ) ";
                }

                if (dbType.ToUpper() == "MYSQL")
                {
                    _parameter.Add("%" + value + "%");
                }
                else
                {
                    _parameter.Add(value);
                }
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        /// <summary>
        /// 取定Where語句中的start like
        /// </summary>
        /// <param name="columnName">欄位名稱</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public virtual SQLCondition SLike(string columnName, object value)
        {
            try
            {
                if (isCase_Sensitive == true)
                {
                    if (dbType.ToUpper() == "ORACLE")
                    {
                        CSQL += " " + columnName + " LIKE '%' || " + parameterFlag + " ";
                    }
                    else if (dbType.ToUpper() == "MSSQL")
                    {
                        CSQL += " " + columnName + " LIKE '%' + " + parameterFlag + " ";
                    }
                    else if (dbType.ToUpper() == "MYSQL")
                    {
                        CSQL += " " + columnName + " LIKE  " + parameterFlag + "  ";
                    }
                }
                else
                {
                    if (dbType.ToUpper() == "ORACLE")
                    {
                        CSQL += " upper(" + columnName + ") LIKE upper('%' || " + parameterFlag + ") ";
                    }
                    else if (dbType.ToUpper() == "MSSQL")
                    {
                        CSQL += " upper(" + columnName + ") LIKE upper('%' + " + parameterFlag + ") ";
                    }
                    else if (dbType.ToUpper() == "MYSQL")
                    {
                        CSQL += " upper(" + columnName + ") LIKE upper( " + parameterFlag + "  ) ";
                    }
                }
                if (dbType.ToUpper() == "MYSQL")
                {
                    _parameter.Add("%" + value);
                }
                else
                {
                    _parameter.Add(value);
                }
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        /// <summary>
        /// 取定Where語句中的start like(忽略大小寫)
        /// </summary>
        /// <param name="columnName">欄位名稱</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public virtual SQLCondition iSLike(string columnName, object value)
        {
            try
            {
                if (dbType.ToUpper() == "ORACLE")
                {
                    CSQL += " upper(" + columnName + ") LIKE upper('%' || " + parameterFlag + ") ";
                }
                else if (dbType.ToUpper() == "MSSQL")
                {
                    CSQL += " upper(" + columnName + ") LIKE upper('%' + " + parameterFlag + ") ";
                }
                else if (dbType.ToUpper() == "MYSQL")
                {
                    CSQL += " upper(" + columnName + ") LIKE upper( " + parameterFlag + "  ) ";
                }

                if (dbType.ToUpper() == "MYSQL")
                {
                    _parameter.Add("%" + value);
                }
                else
                {
                    _parameter.Add(value);
                }
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        /// <summary>
        /// 取定Where語句中的end like
        /// </summary>
        /// <param name="columnName">欄位名稱</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public virtual SQLCondition ELike(string columnName, object value)
        {
            try
            {
                if (isCase_Sensitive == true)
                {
                    if (dbType.ToUpper() == "ORACLE")
                    {
                        CSQL += " " + columnName + " LIKE " + parameterFlag + " || '%' ";
                    }
                    else if (dbType.ToUpper() == "MSSQL")
                    {
                        CSQL += " " + columnName + " LIKE " + parameterFlag + "+'%' ";
                    }
                    else if (dbType.ToUpper() == "MYSQL")
                    {
                        CSQL += " " + columnName + " LIKE  " + parameterFlag + "  ";
                    }
                }
                else
                {
                    if (dbType.ToUpper() == "ORACLE")
                    {
                        CSQL += " upper(" + columnName + ") LIKE upper(" + parameterFlag + " || '%') ";
                    }
                    else if (dbType.ToUpper() == "MSSQL")
                    {
                        CSQL += " upper(" + columnName + ") LIKE upper(" + parameterFlag + "+'%') ";
                    }
                    else if (dbType.ToUpper() == "MYSQL")
                    {
                        CSQL += " upper(" + columnName + ") LIKE upper( " + parameterFlag + "  ) ";
                    }
                }
                if (dbType.ToUpper() == "MYSQL")
                {
                    _parameter.Add(value + "%");
                }
                else
                {
                    _parameter.Add(value);
                }
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        /// <summary>
        /// 取定Where語句中的end like(忽略大小寫)
        /// </summary>
        /// <param name="columnName">欄位名稱</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public virtual SQLCondition iELike(string columnName, object value)
        {
            try
            {
                if (dbType.ToUpper() == "ORACLE")
                {
                    CSQL += " upper(" + columnName + ") LIKE upper(" + parameterFlag + " || '%') ";
                }
                else if (dbType.ToUpper() == "MSSQL")
                {
                    CSQL += " upper(" + columnName + ") LIKE upper(" + parameterFlag + "+'%') ";
                }
                else if (dbType.ToUpper() == "MYSQL")
                {
                    CSQL += " upper(" + columnName + ") LIKE upper( " + parameterFlag + "  ) ";
                }

                if (dbType.ToUpper() == "MYSQL")
                {
                    _parameter.Add(value + "%");
                }
                else
                {
                    _parameter.Add(value);
                }
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        /// <summary>
        /// 取定Where語句中的between like
        /// </summary>
        /// <param name="columnName">欄位名稱</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public virtual SQLCondition BLike(string columnName, object value)
        {
            try
            {
                return this.Like(columnName, value);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        /// <summary>
        /// 取定Where語句中的between like(忽略大小寫)
        /// </summary>
        /// <param name="columnName">欄位名稱</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public virtual SQLCondition iBLike(string columnName, object value)
        {
            try
            {
                return this.iLike(columnName, value);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        /// <summary>
        /// 取定Where語句中的IN
        /// </summary>
        /// <param name="columnName">欄位名稱</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public virtual SQLCondition In(string columnName, List<object> values)
        {
            try
            {
                CSQL += " ";
                if (isCase_Sensitive == true)
                {
                    CSQL += " " + columnName + " IN (";
                }
                else
                {
                    CSQL += " upper( " + columnName + " ) IN (";
                }
                foreach (string tmp in values)
                {
                    if (isCase_Sensitive == true)
                    {
                        CSQL += " :######, ";
                    }
                    else
                    {
                        CSQL += " :######, ";
                    }
                    _parameter.Add(tmp.ToUpper());
                }
                if (CSQL.EndsWith(", "))
                {
                    CSQL = CSQL.Substring(0, CSQL.Length - ", ".Length);
                }
                CSQL += " ) ";
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        /// <summary>
        /// 取定Where語句中的IN(忽略大小寫)
        /// </summary>
        /// <param name="columnName">欄位名稱</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public virtual SQLCondition iIn(string columnName, List<object> values)
        {
            try
            {
                CSQL += " ";
                CSQL += " upper( " + columnName + " ) IN (";
                foreach (string tmp in values)
                {
                    CSQL += " :######, ";
                    _parameter.Add(tmp.ToUpper());
                }
                if (CSQL.EndsWith(", "))
                {
                    CSQL = CSQL.Substring(0, CSQL.Length - ", ".Length);
                }
                CSQL += " ) ";
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public virtual SQLCondition NotIn(string columnName, List<object> values)
        {
            try
            {
                CSQL += " ";
                CSQL += " " + columnName + "  NOT IN (";
                foreach (string tmp in values)
                {
                    CSQL += " :######, ";
                    _parameter.Add(tmp.ToUpper());
                }
                if (CSQL.EndsWith(", "))
                {
                    CSQL = CSQL.Substring(0, CSQL.Length - ", ".Length);
                }
                CSQL += " ) ";
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        /// <summary>
        /// 檢查語法最後是不是有無用的語法如 AND , OR , 
        /// </summary>
        /// <returns></returns>
        public virtual SQLCondition CheckSQL()
        {
            try
            {
                if (CSQL.EndsWith(" AND "))
                {
                    CSQL = CSQL.Substring(0, CSQL.Length - " AND ".Length);
                }
                if (CSQL.EndsWith(" OR "))
                {
                    CSQL = CSQL.Substring(0, CSQL.Length - " OR ".Length);
                }
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
    }
}
