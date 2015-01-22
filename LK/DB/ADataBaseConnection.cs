using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LK.Config.DataBase;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.Net;
using Microsoft.Win32;
using log4net;
using System.Reflection;
namespace LK.DB
{
    public abstract class ADataBaseConnection :IDisposable
    {
        public static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private IDataBaseConfigInfo _dataBaseConfigInfo = null;
        private IDbConnection _dbconnection_ = null;
        DbProviderFactory _factory = null;
        private string _DataBaseName_ = null;
        /// <summary>
        /// ADataBAseConnection初始化
        /// </summary>
        /// <param name="batabaseConfigInfo">資料庫設定檔物件</param>
        public ADataBaseConnection(IDataBaseConfigInfo batabaseConfigInfo) {
            _dataBaseConfigInfo = batabaseConfigInfo;
        }

        public string getDataBaseName(){
            return _DataBaseName_;
        }
        /// <summary>
        /// 設定/載入資料庫設定檔物件
        /// </summary>
        /// <param name="batabaseConfigInfo">資料庫設定檔物件</param>
        public void setDataBaseConfigInfo(IDataBaseConfigInfo batabaseConfigInfo)
        {
            _dataBaseConfigInfo = batabaseConfigInfo;
        }
        public IDataBaseConfigInfo getDataBaseConfigInfo() {
            return _dataBaseConfigInfo;
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
                    {
                        if (_dbconnection_ != null)
                        {
                            try
                            {
                                _dataBaseConfigInfo = null;
                                _dbconnection_.Close();
                                _dbconnection_.Dispose();
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }
                    }
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

        #region setDataBase
        /// <summary>
        /// 初始化資料庫的連線物件
        /// </summary>
        /// <param name="dbName"></param>
        public void setDataBase(string dbName) {
            try
            {
                IDbConnection ret = null;
                _DataBaseName_ = dbName;
                if (_dataBaseConfigInfo == null)
                {
                    throw new Exception("沒有設置正確的DataBaseConfig物件!");
                }

                if (_dataBaseConfigInfo.GetDBType(dbName).ToUpper().IndexOf("ORACLE") >= 0)
                {
                    /*是oracle資料庫*/
                    ret = getOracleConnection(_dataBaseConfigInfo.GetDB(dbName));
                }
                else if (_dataBaseConfigInfo.GetDBType(dbName).ToUpper().IndexOf("MSSQL") >= 0)
                {
                    /*是mssql資料庫*/
                    ret = getMsSQLConnection(_dataBaseConfigInfo.GetDB(dbName));
                }
                else if (_dataBaseConfigInfo.GetDBType(dbName).ToUpper().IndexOf("MYSQL") >= 0)
                {
                    /*是mysql資料庫*/                    
                    ret = getMySQLConnection(_dataBaseConfigInfo.GetDB(dbName));
                }
                else if (_dataBaseConfigInfo.GetDBType(dbName).ToUpper().IndexOf("ACCESS") >= 0)
                {
                    /*是access資料庫*/
                    throw new NotImplementedException("未實作ACCESS的DataBaseConnection");
                    //ret = getAccessConnection(_dataBaseConfigInfo.GetDB(dbName));
                }
                else if (_dataBaseConfigInfo.GetDBType(dbName).ToUpper().IndexOf("LITESQL") >= 0)
                {
                    /*是litesql資料庫*/
                    throw new NotImplementedException("未實作LiteSQL的DataBaseConnection");
                    //ret = getLiteSQLConnection(_dataBaseConfigInfo.GetDB(dbName));
                }

                string whereType = _dataBaseConfigInfo.GetWhere(dbName);
                string connectionString = "";
                /*按whereType的字段來決定如何產生"連線字符串"*/
                if (whereType.ToUpper() == "WS")
                {
                    string url = _dataBaseConfigInfo.GetDB(dbName);
                    WebClient client = new WebClient();
                    connectionString = client.DownloadString(url);
                    connectionString = LK.Util.Encrypt.pwdDecode(connectionString);
                    client = null;
                }
                else if (whereType.ToUpper() == "REGISTRY")
                {
                    string url = _dataBaseConfigInfo.GetDB(dbName);
                    connectionString = (string)Registry.GetValue(url, null, null);
                    connectionString = LK.Util.Encrypt.pwdDecode(connectionString);
                }
                else if (whereType.ToUpper() == "FILE")
                {
                    System.IO.StreamReader sr = new System.IO.StreamReader(_dataBaseConfigInfo.GetDB(dbName));
                    connectionString = sr.ReadToEnd().Trim();
                    connectionString = LK.Util.Encrypt.pwdDecode(connectionString);
                    sr = null;
                }
                else
                {
                    connectionString = _dataBaseConfigInfo.GetDB(dbName);
                    connectionString = LK.Util.Encrypt.pwdDecode(connectionString);
                }
                ret.ConnectionString = connectionString.Trim();
                _dbconnection_ = ret;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        #endregion
        public enum DataBaseType { 
            MsSQL,Oracle,MySql,Access,LiteSql
        }
        public DataBaseType getDataBaseType(string dbName)
        {
            try
            {
                IDbConnection ret = null;
                _DataBaseName_ = dbName;
                if (_dataBaseConfigInfo == null)
                {
                    throw new Exception("沒有設置正確的DataBaseConfig物件!");
                }

                if (_dataBaseConfigInfo.GetDBType(dbName).ToUpper().IndexOf("ORACLE") >= 0)
                {
                    /*是oracle資料庫*/
                    return DataBaseType.Oracle;
                }
                else if (_dataBaseConfigInfo.GetDBType(dbName).ToUpper().IndexOf("MSSQL") >= 0)
                {
                    /*是mssql資料庫*/
                    return DataBaseType.MsSQL;
                }
                else if (_dataBaseConfigInfo.GetDBType(dbName).ToUpper().IndexOf("MYSQL") >= 0)
                {
                    /*是mysql資料庫*/
                    return DataBaseType.MySql;
                }
                else if (_dataBaseConfigInfo.GetDBType(dbName).ToUpper().IndexOf("ACCESS") >= 0)
                {
                    /*是access資料庫*/
                    return DataBaseType.Access;
                    //ret = getAccessConnection(_dataBaseConfigInfo.GetDB(dbName));
                }
                else if (_dataBaseConfigInfo.GetDBType(dbName).ToUpper().IndexOf("LITESQL") >= 0)
                {
                    /*是litesql資料庫*/
                    return DataBaseType.LiteSql;
                    //ret = getLiteSQLConnection(_dataBaseConfigInfo.GetDB(dbName));
                }

                throw new Exception("getDataBaseType出現未知的錯誤!");
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        public string getConnectionUser(string dbName)
        {
            try
            {
                IDbConnection ret = null;
                _DataBaseName_ = dbName;
                if (_dataBaseConfigInfo == null)
                {
                    throw new Exception("沒有設置正確的DataBaseConfig物件!");
                }

                if (_dataBaseConfigInfo.GetDBType(dbName).ToUpper().IndexOf("ORACLE") >= 0)
                {
                    /*是oracle資料庫*/
                    ret = getOracleConnection(_dataBaseConfigInfo.GetDB(dbName));
                }
                else if (_dataBaseConfigInfo.GetDBType(dbName).ToUpper().IndexOf("MSSQL") >= 0)
                {
                    /*是mssql資料庫*/
                    ret = getMsSQLConnection(_dataBaseConfigInfo.GetDB(dbName));
                }
                else if (_dataBaseConfigInfo.GetDBType(dbName).ToUpper().IndexOf("MYSQL") >= 0)
                {
                    /*是mysql資料庫*/
                    ret = getMySQLConnection(_dataBaseConfigInfo.GetDB(dbName));
                }
                else if (_dataBaseConfigInfo.GetDBType(dbName).ToUpper().IndexOf("ACCESS") >= 0)
                {
                    /*是access資料庫*/
                    throw new NotImplementedException("未實作ACCESS的DataBaseConnection");
                    //ret = getAccessConnection(_dataBaseConfigInfo.GetDB(dbName));
                }
                else if (_dataBaseConfigInfo.GetDBType(dbName).ToUpper().IndexOf("LITESQL") >= 0)
                {
                    /*是litesql資料庫*/
                    throw new NotImplementedException("未實作LiteSQL的DataBaseConnection");
                    //ret = getLiteSQLConnection(_dataBaseConfigInfo.GetDB(dbName));
                }

                string whereType = _dataBaseConfigInfo.GetWhere(dbName);
                string connectionString = "";
                /*按whereType的字段來決定如何產生"連線字符串"*/
                if (whereType.ToUpper() == "WS")
                {
                    string url = _dataBaseConfigInfo.GetDB(dbName);
                    WebClient client = new WebClient();
                    connectionString = client.DownloadString(url);
                    client = null;
                }
                else if (whereType.ToUpper() == "REGISTRY")
                {
                    string url = _dataBaseConfigInfo.GetDB(dbName);
                    connectionString = (string)Registry.GetValue(url, null, null);
                }
                else if (whereType.ToUpper() == "FILE")
                {
                    System.IO.StreamReader sr = new System.IO.StreamReader(_dataBaseConfigInfo.GetDB(dbName));
                    connectionString = sr.ReadToEnd().Trim();
                    sr = null;
                }
                else
                {
                    connectionString = _dataBaseConfigInfo.GetDB(dbName);
                }

                
                if (getDataBaseType(dbName) == DataBaseType.MySql) {
                    var arrCon = connectionString.Split(';');
                    for (var i = 0; i < arrCon.Length; i++) {
                        if (arrCon[i].ToLower().Trim().StartsWith("uid"))
                        {
                            return arrCon[i].Split('=')[1];
                        }
                    }
                    
                }
                else if (getDataBaseType(dbName) == DataBaseType.Oracle)
                {
                    var arrCon = connectionString.Split(';');
                    for (var i = 0; i < arrCon.Length; i++)
                    {
                        if (arrCon[i].ToLower().Trim().StartsWith("user id"))
                        {
                            return arrCon[i].Split('=')[1];
                        }
                    }
                }
                else if (getDataBaseType(dbName) == DataBaseType.MsSQL)
                {
                    var arrCon = connectionString.Split(';');
                    for (var i = 0; i < arrCon.Length; i++)
                    {
                        if (arrCon[i].ToLower().Trim().StartsWith("user id"))
                        {
                            return arrCon[i].Split('=')[1];
                        }
                    }
                }
                
                    throw new Exception("getConnectionUser 未實作");
                
                
                //ret.ConnectionString = connectionString.Trim();
                //_dbconnection_ = ret;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        #region getXXXConnection
        /// <summary>
        /// 取得Oracle的DataConnection物件
        /// </summary>
        /// <param name="dbName"></param>
        /// <returns></returns>
        private IDbConnection getOracleConnection(string dbName) {
            try
            {                
                string connectionstring = "";
                if (_dataBaseConfigInfo != null)
                {
                    connectionstring = _dataBaseConfigInfo.GetDB(dbName);
                  
                    _factory = DbProviderFactories.GetFactory("System.Data.OracleClient");
                }
                else
                {
                    throw new Exception("沒有設置正確的DataBaseConfig物件!");
                }
                if (_factory == null)
                {
                    throw new Exception("無法利用DbProviderFactories建新DbProviderFactory物件!");
                }
                return _factory.CreateConnection();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        /// <summary>
        /// 取得MSSQL的DataConnection物件
        /// </summary>
        /// <param name="dbName"></param>
        /// <returns></returns>
        private IDbConnection getMsSQLConnection(string dbName)
        {
            try
            {                
                string connectionstring = "";
                if (_dataBaseConfigInfo != null)
                {
                    connectionstring = _dataBaseConfigInfo.GetDB(dbName);
                  
                    _factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
                }
                else
                {
                    throw new Exception("沒有設置正確的DataBaseConfig物件!");
                }
                if (_factory == null)
                {
                    throw new Exception("無法利用DbProviderFactories建新DbProviderFactory物件!");
                }
                return _factory.CreateConnection();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        /// <summary>
        /// 取得Access的DataConnection物件
        /// </summary>
        /// <param name="dbName"></param>
        /// <returns></returns>
        private IDbConnection getAccessConnection(string dbName)
        {
            try
            {
                throw new NotImplementedException("未實作ACCESS的getAccessConnection方法");
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        /// <summary>
        /// 取得MYSQL的DataConnection物件
        /// </summary>
        /// <param name="dbName"></param>
        /// <returns></returns>
        private IDbConnection getMySQLConnection(string dbName)
        {
            try
            {
                string connectionstring = "";
                if (_dataBaseConfigInfo != null)
                {
                    connectionstring = _dataBaseConfigInfo.GetDB(dbName);
                    
                    _factory = DbProviderFactories.GetFactory("MySql.Data.MySqlClient");
                }
                else
                {
                    throw new Exception("沒有設置正確的DataBaseConfig物件!");
                }
                if (_factory == null)
                {
                    throw new Exception("無法利用DbProviderFactories建新DbProviderFactory物件!");
                }
                return _factory.CreateConnection();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        /// <summary>
        /// 取得LITESQL的DataConnection物件
        /// </summary>
        /// <param name="dbName"></param>
        /// <returns></returns>
        private IDbConnection getLiteSQLConnection(string dbName)
        {
            try
            {
                throw new NotImplementedException("未實作LiteSQL的getLiteSQLConnection方法");
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        #endregion
        /// <summary>
        /// 取得DataConnection物件
        /// </summary>
        /// <returns></returns>
        public IDbConnection getConnection() {
            return _dbconnection_;            
        }
        public IDbDataParameter getDataParameter(LK.DB.SQLCreater.ISQLCreater pSqlCreater, string pName,object pValue,ParameterDirection pdirection) {

            try
            {
                
                var param = _factory.CreateParameter();
                param.ParameterName = pName;
                param.Value = pValue;
                param.Direction = pdirection;                
                return pSqlCreater.createDbParameter(param, pdirection);                
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
       

    }
}
