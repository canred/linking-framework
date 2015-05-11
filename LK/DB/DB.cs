using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LK.Config.DataBase;
using System.Data;
using log4net;
using System.Reflection;
namespace LK.DB
{
    public class DB :IDisposable
    {
        public static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        #region Dispose
        void IDisposable.Dispose()
        {
            this.Dispose(true);
            System.GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
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
        #endregion
        IDbTransaction __transaction__ = null;
        ADataBaseConnection _connection = null;
        System.Data.IDbCommand _command_ = null;
        SQLCreater.ASQLCreater _SQLCreater_ = null;
        List<System.Data.IDataParameter> _commandParameter = new List<IDataParameter>();
        int _timeOut = 300;
        string _commandText_ = null;
        public enum TestFlag
        { 
            Success,Faild,Undefined
        }
        public TestFlag TestResult = TestFlag.Undefined;
        private void initSQLCreater(IDataBaseConfigInfo config) {
            try
            {
                if (config.GetDBType(_connection.getDataBaseName()).ToUpper() == "ORACLE")
                {
                    _SQLCreater_ = new LK.DB.SQLCreater.SQLCreaterOracle();
                }
                else if (config.GetDBType(_connection.getDataBaseName()).ToUpper() == "MSSQL")
                {
                    _SQLCreater_ = new LK.DB.SQLCreater.SQLCreaterMSSQL();
                }
                else if (config.GetDBType(_connection.getDataBaseName()).ToUpper() == "MYSQL")
                {
                    _SQLCreater_ = new LK.DB.SQLCreater.SQLCreaterMySQL();
                }
                else
                {
                    throw new NotImplementedException("未實作" + config.GetDBType().ToString() + "的SQLCreater");
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        } 
        public DB(object modelObj) {
            try
            {
                var attrs = modelObj.GetType().GetCustomAttributes(typeof(LK.Attribute.LkDataBase), false);
                string dbName = null;
                if (attrs.Length == 1)
                {
                    dbName = ((LK.Attribute.LkDataBase)(attrs[0])).getDataBase();
                    LK.Config.DataBase.IDataBaseConfigInfo a = LK.Config.DataBase.Factory.getInfo();
                    LK.DB.DataBaseConnection obj = new DataBaseConnection(a);                    
                    obj.setDataBase(dbName);
                    _connection = obj;
                }
                else
                {
                    throw new Exception("你的物件內並沒有DataBase關閉的Attribute!");
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        /// <summary>
        /// 使用注入的方式
        /// </summary>
        /// <param name="databaseConfigInfo"></param>
        /// <param name="adatabaseconnection"></param>
        /// <param name="modelObj"></param>
        public DB(IDataBaseConfigInfo databaseConfigInfo, ADataBaseConnection adatabaseconnection, object modelObj)
        {
            try
            {
                var attrs = modelObj.GetType().GetCustomAttributes(typeof(LK.Attribute.LkDataBase), false);
                string dbName = null;
                if (attrs.Length == 1)
                {
                    dbName = ((LK.Attribute.LkDataBase)(attrs[0])).getDataBase();
                    LK.Config.DataBase.IDataBaseConfigInfo a = databaseConfigInfo;
                    adatabaseconnection.setDataBaseConfigInfo(a);
                    adatabaseconnection.setDataBase(dbName);
                    _connection = adatabaseconnection;
                    initSQLCreater(adatabaseconnection.getDataBaseConfigInfo());
                }
                else
                {
                    throw new Exception("你的物件內並沒有DataBase關閉的Attribute!");
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        /// <summary>
        /// 使用注入的方式
        /// </summary>
        /// <param name="databaseConfigInfo"></param>
        /// <param name="adatabaseconnection"></param>
        /// <param name="modelObj"></param>
        public DB(ADataBaseConnection adatabaseconnection, object modelObj)
        {
            try
            {
                var attrs = modelObj.GetType().GetCustomAttributes(typeof(LK.Attribute.LkDataBase), false);
                string dbName = null;
                if (attrs.Length == 1)
                {
                    dbName = ((LK.Attribute.LkDataBase)(attrs[0])).getDataBase();
                    adatabaseconnection.setDataBase(dbName);
                    _connection = adatabaseconnection;
                    initSQLCreater(adatabaseconnection.getDataBaseConfigInfo());
                }
                else
                {
                    throw new Exception("你的物件內並沒有DataBase關閉的Attribute!");
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public DB(ADataBaseConnection adatabaseconnection, string dbName)
        {
            try
            {
                adatabaseconnection.setDataBase(dbName);
                _connection = adatabaseconnection;
                initSQLCreater(adatabaseconnection.getDataBaseConfigInfo());
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        private string _GetConnectionString(string dataBaseFlag) {  
            return "";   
        }
        public bool Update(RecordBase record)
        {
            try
            {
                this.openConnection();
                _command_ = _connection.getConnection().CreateCommand();
                return true;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public bool Insert(RecordBase record) {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public bool Delete(RecordBase record) {
            return true;
        }
        public void ExecuteProcedure(string procedureName) {
            this.openConnection();
            try
            {
                _command_ = _connection.getConnection().CreateCommand();
                _command_.CommandText = procedureName;
                _command_.CommandType = CommandType.StoredProcedure;
                _command_.Parameters.Add(new object());
                _command_.Parameters.Add(new object());
                var s = DateTime.Now;
                _command_.ExecuteNonQuery();
                var e = DateTime.Now;

                
                logSQL(ref _command_, true);
                monitorSql(ref _command_, s, e);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                logSQL(ref _command_, false);
                throw ex;
            }
            finally {
                this.closeConnection();
            }
        }        
        public void ExecuteProcedure_NoParameter(string procedureName)
        {
            this.openConnection();
            try
            {

                _command_ = _connection.getConnection().CreateCommand();
                _command_.CommandText = _SQLCreater_.__ExecuteProcedureAndReturnSQL(procedureName);
                _command_.CommandType = CommandType.StoredProcedure;
                var s = DateTime.Now;
                _command_.ExecuteNonQuery();
                var e = DateTime.Now;

                logSQL(ref _command_, true);
                monitorSql(ref _command_, s, e);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                logSQL(ref _command_, false);
                throw ex;
            }
            finally {
                this.closeConnection();
            }
        }
        public void ExecuteProcedure(string procedureName, IDataParameter[] parameters)
        {
            this.openConnection();
            try
            {

                _command_ = _connection.getConnection().CreateCommand();
                _command_.CommandText = _SQLCreater_.__ExecuteProcedureAndReturnSQL(procedureName);
                _command_.CommandType = CommandType.StoredProcedure;
                if (parameters != null)
                {
                    foreach (var item in parameters)
                    {
                        _command_.Parameters.Add(item);
                    }
                }
                var s = DateTime.Now;
                _command_.ExecuteNonQuery();
                var e = DateTime.Now;
                
                logSQL(ref _command_, true);
                monitorSql(ref _command_, s, e);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                logSQL(ref _command_, false);
                throw ex;
            }
            finally {
                this.closeConnection();
            }
        }
        public void ExecuteProcedureAndReturn(string procedureName, IDataParameter[] parameters)
        {
            this.openConnection();
            try
            {

                _command_ = _connection.getConnection().CreateCommand();
                _command_.CommandText = _SQLCreater_.__ExecuteProcedureAndReturnSQL(procedureName);
                _command_.CommandType = CommandType.StoredProcedure;
                _SQLCreater_.__setExecuteProcedureAndReturnParameter(_connection, procedureName, _command_, parameters);
                var s = DateTime.Now;
                var r = _command_.ExecuteReader();
                var e = DateTime.Now;
                DataTable tt = new DataTable();
                tt.Load(r);

                logSQL(ref _command_, true);
                monitorSql(ref _command_, s, e);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                logSQL(ref _command_, false);
                throw ex;
            }
            finally {
                this.closeConnection();
            }
        }
        public DataTable ExecuteProcedureAndReturnTable(string procedureName, IDataParameter[] parameters)
        {
            this.openConnection();
            try
            {

                _command_ = _connection.getConnection().CreateCommand();
                this.initSQLCreater(_connection.getDataBaseConfigInfo());
                _command_.CommandText = _SQLCreater_.__ExecuteProcedureAndReturnSQL(procedureName);
                _command_.CommandType = CommandType.StoredProcedure;
                _SQLCreater_.__setExecuteProcedureAndReturnParameter(_connection, procedureName, _command_, parameters);
                var s = DateTime.Now;
                var r = _command_.ExecuteReader();
                var e = DateTime.Now;
                DataTable tt = new DataTable();
                tt.Load(r);
            
                logSQL(ref _command_, true);
                monitorSql(ref _command_, s, e);
                return tt;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                logSQL(ref _command_, false);
                throw ex;
            }
            finally {
                this.closeConnection();
            }
        }
        public DataTable[] ExecuteProcedureAndReturnTables(string procedureName, IDataParameter[] parameters)
        {
            this.openConnection();
            try
            {

                _command_ = _connection.getConnection().CreateCommand();
                this.initSQLCreater(_connection.getDataBaseConfigInfo());
                _command_.CommandText = _SQLCreater_.__ExecuteProcedureAndReturnSQL(procedureName);
                _command_.CommandType = CommandType.StoredProcedure;
                _SQLCreater_.__setExecuteProcedureAndReturnParameter(_connection, procedureName, _command_, parameters);
                List<DataTable> tbls = new List<DataTable>();
                var s = DateTime.Now;
                var r = _command_.ExecuteReader();
                var e = DateTime.Now;
                DataTable tt = new DataTable();
                tt.Load(r);
                tbls.Add(tt);
                while (r.IsClosed == false && r.Read())
                {
                    DataTable tmp = new DataTable();
                    tmp.Load(r);
                    tbls.Add(tmp);
                }
            
                logSQL(ref _command_, true);
                monitorSql(ref _command_, s, e);
                return tbls.ToArray();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                logSQL(ref _command_, false);
                throw ex;
            }
            finally {
                this.closeConnection();
            }
        }
        public DB BeginTransaction()
        {
            try
            {
                if (_connection.getConnection().State == ConnectionState.Closed)
                    _connection.getConnection().Open();
                __transaction__ = _connection.getConnection().BeginTransaction( IsolationLevel.ReadCommitted);
                log.Info("【Transaction】Start");
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public bool TestConnection() {
            try
            {
                this.getConnection().Open();
                return true;
            }
            catch (Exception ex)
            {                
                log.Error(ex);
                return false;
                //throw ex;
            }
        }
        public void Commit()
        {
            try
            {
                try
                {
                    if (__transaction__ != null)
                    {
                        __transaction__.Commit();
                        log.Info("【Transaction】 End");
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    closeConnection();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public void Rollback() {
            try
            {
                try
                {
                    if (__transaction__ != null)
                    {
                        __transaction__.Rollback();
                        log.Info("【Transaction】 End And Rollback");
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    closeConnection();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }        
        public string CommandText {
            get {
                return _commandText_;
            }
            set {
                _commandText_ = value;
            }
        }
        public void addParameter() {         
        }
        public void addParameter(List<System.Data.IDataParameter> parms)
        {
            try
            {
                _commandParameter = parms;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public void addParameter(System.Data.IDataParameter parms)
        {
            try
            {
                _commandParameter.Add(parms);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public void removeAllParameter()
        {
            try
            {
                _commandParameter = new List<IDataParameter>();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }            
        }
        public IDbDataParameter getParameter(string pName, object value, ParameterDirection direction)
        {
            try
            {
                this.initSQLCreater(this._connection.getDataBaseConfigInfo());
                return this._connection.getDataParameter(_SQLCreater_, pName, value, direction);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public IDbDataParameter getParameter(string pName, object value,System.Data.DbType dbtype, ParameterDirection direction)
        {
            try
            {
                this.initSQLCreater(this._connection.getDataBaseConfigInfo());
                return this._connection.getDataParameter(_SQLCreater_, pName, value,dbtype, direction);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public IDbConnection getConnection()
        {
            return _connection.getConnection();
        }       
        public void ExecuteNonQuery() {
            this.openConnection();
            try
            {                
                _command_ = _connection.getConnection().CreateCommand();
                _command_.CommandText = this.CommandText;
                _command_.CommandTimeout = _timeOut;
                _command_.Parameters.Clear();                
                foreach (var item in this._commandParameter)
                {
                    _command_.Parameters.Add(item);
                }
                var s = DateTime.Now;
                _command_.ExecuteNonQuery();
                var e = DateTime.Now;
                
                logSQL(ref _command_, true);
                monitorSql(ref _command_, s, e);
            }
            catch (Exception ex)
            {                
                log.Error(ex);               
                logSQL(ref _command_, false);
                throw ex;
            }finally{
                this.closeConnection();
            }            
        }
        public void ExecuteNonQuery(DB db)
        {
            this.openConnection();
            try
            {

                _command_ = _connection.getConnection().CreateCommand();
                _command_.CommandText = this.CommandText;
                _command_.CommandTimeout = _timeOut;
                _command_.Parameters.Clear();
                if (__transaction__ != null)
                {
                    _command_.Transaction = __transaction__;
                }
                foreach (var item in this._commandParameter)
                {
                    _command_.Parameters.Add(item);
                }
                var s = DateTime.Now;
                _command_.ExecuteNonQuery();
                var e = DateTime.Now;
                logSQL(ref _command_, true);
                monitorSql(ref _command_, s, e);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                logSQL(ref _command_, false);
                throw ex;
            }
            
        }
        private void logSQL(ref IDbCommand cmd ,bool isSuccess) {
            var logSQL = LK.Config.DataBase.Factory.getInfo().GetTag("logSQL").ToLower();
            try
            {
                if (logSQL == "false")
                {
                    return;
                }
                if (isSuccess)
                {
                    log.Info("---Run SQL【Success】----------------------------------------------------------------------");
                    log.Info(cmd.CommandText);
                }
                else {
                    log.Error("---Run SQL【 Failed 】----------------------------------------------------------------------");
                    log.Error(cmd.CommandText);
                }                
                foreach (System.Data.IDataParameter para in cmd.Parameters)
                {
                    if (isSuccess)
                    {
                        log.Info(para.ParameterName + "=>" + para.Value);
                    }
                    else
                    {
                        log.Error(para.ParameterName + "=>" + para.Value);
                    }                   
                }                
            }
            catch (Exception ex)
            {
            }
            finally {
                if (logSQL == "false")
                {
                }
                else
                {
                    if (isSuccess)
                    {
                        log.Info("-------------------------------------------------------------------------------------------");
                    }
                    else
                    {
                        log.Error("-------------------------------------------------------------------------------------------");
                    }
                }                
            }
        }

        private void monitorSql(ref IDbCommand cmb, DateTime rT, DateTime eT) {
            var sqlRunTimeMaximum = LK.Config.DataBase.Factory.getInfo().GetTag("SqlRunTimeMaximum").ToLower();
            double monitorTime = 0.0;
            if (sqlRunTimeMaximum.Trim().Length > 0) {
                if (LK.Util.Math.IsNumeric(sqlRunTimeMaximum)) {
                    monitorTime = Convert.ToDouble(sqlRunTimeMaximum);
                    TimeSpan bT = new TimeSpan();
                    bT = eT - rT;
                    if (bT.TotalMilliseconds > monitorTime)
                    {
                        log.Warn("******************************************************************");
                        log.Warn("執行時間:" + bT.TotalMilliseconds + "毫秒");
                        log.Warn("執行語句:" + cmb.CommandText);
                        log.Warn("執行參數:");
                        foreach (System.Data.IDataParameter para in cmb.Parameters)
                        {                            
                            log.Warn(para.ParameterName + "=>" + para.Value);                           
                        }
                        log.Warn("******************************************************************");
                    }
                }
            }
        }

        private DataTable __FillDataTable(string tableName)
        {
            System.Data.DataSet ds = new DataSet();
            /*針對MySQLClient設置的項目EnforceConstraints一定要為false不然會掛掉*/
            ds.EnforceConstraints = false;
            this.openConnection();
            try
            {
                System.Data.DataTable ret = new System.Data.DataTable(tableName);
                
                _command_ = _connection.getConnection().CreateCommand();
                _command_.CommandText = this.CommandText;
                _command_.CommandTimeout = _timeOut;
                _command_.Parameters.Clear();
                foreach (var item in this._commandParameter)
                {
                    try
                    {
                        _command_.Parameters.Add(item);
                    }
                    catch(Exception ex) {
                        throw ex;
                    }
                }
                if (this.__transaction__ != null)
                {
                    _command_.Transaction = this.__transaction__;
                }
                var s = DateTime.Now;
                var reader = _command_.ExecuteReader();
                var e = DateTime.Now;
                ds.Tables.Add(ret);
                ret.Load(reader);
                logSQL(ref _command_, true);
                monitorSql(ref _command_, s, e);
                
                return ret;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                logSQL(ref _command_, false);
                throw ex;
            }
            finally {
                if (this.__transaction__ == null)
                {
                    this.closeConnection();
                }
                ds.Dispose();
            }
        }
        public DataTable FillDataTable(string tableName) {
            try
            {
                return __FillDataTable(tableName);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        } 
        private void openConnection() {
            try
            {
                if (_connection == null)
                {
                    throw new Exception("沒有初始資料連線!");
                }
                if (_connection.getConnection().State == System.Data.ConnectionState.Closed)
                {
                    _connection.getConnection().Open();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }           
        }
        private void closeConnection() {
            try
            {
                if (_connection != null)
                {
                    _connection.getConnection().Close();
                }
                //else
                //{
                //    throw new Exception("沒有初始資料連線!");
                //}
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public void setTimeout(int second) {
            _timeOut = second;
        }
        public int getTimeout()
        {
            return _timeOut;
        }
        public void ReleaseTransaction() {            
        }
    }
}