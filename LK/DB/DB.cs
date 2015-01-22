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
                {
                    /*
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
                    }*/
                }
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
        //public DB(string ConnectionInfo)
        //{
        //    try
        //    {
        //        throw new NotImplementedException("在DB class中為實作public DB(string ConnectionInfo)");
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error(ex);
        //        throw ex;
        //    }
        //}
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
            try
            {
                this.openConnection();
                _command_ = _connection.getConnection().CreateCommand();
                _command_.CommandText = procedureName;
                _command_.CommandType = CommandType.StoredProcedure;
                _command_.Parameters.Add(new object());
                _command_.Parameters.Add(new object());
                _command_.ExecuteNonQuery();

                this.closeConnection();
                logSQL(ref _command_, true);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                logSQL(ref _command_, false);
                throw ex;
            }    
        }
        
        public void ExecuteProcedure_NoParameter(string procedureName)
        {
            try
            {
                this.openConnection();
                _command_ = _connection.getConnection().CreateCommand();
                _command_.CommandText = _SQLCreater_.__ExecuteProcedureAndReturnSQL(procedureName);
                _command_.CommandType = CommandType.StoredProcedure;
                _command_.ExecuteNonQuery();
                this.closeConnection();
                logSQL(ref _command_, true);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                logSQL(ref _command_, false);
                throw ex;
            }
        }

        public void ExecuteProcedure(string procedureName, IDataParameter[] parameters)
        {
            try
            {
                this.openConnection();
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
                _command_.ExecuteNonQuery();
                this.closeConnection();
                logSQL(ref _command_, true);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                logSQL(ref _command_, false);
                throw ex;
            }  
        }

        public void ExecuteProcedureAndReturn(string procedureName, IDataParameter[] parameters)
        {
            try
            {
                this.openConnection();
                _command_ = _connection.getConnection().CreateCommand();
                _command_.CommandText = _SQLCreater_.__ExecuteProcedureAndReturnSQL(procedureName);
                _command_.CommandType = CommandType.StoredProcedure;
                _SQLCreater_.__setExecuteProcedureAndReturnParameter(_connection, procedureName, _command_, parameters);
                var r = _command_.ExecuteReader();
                DataTable tt = new DataTable();
                tt.Load(r);
                this.closeConnection();
                logSQL(ref _command_, true);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                logSQL(ref _command_, false);
                throw ex;
            }
        }

        public DataTable ExecuteProcedureAndReturnTable(string procedureName, IDataParameter[] parameters)
        {
            try
            {
                this.openConnection();
                _command_ = _connection.getConnection().CreateCommand();
                this.initSQLCreater(_connection.getDataBaseConfigInfo());
                _command_.CommandText = _SQLCreater_.__ExecuteProcedureAndReturnSQL(procedureName);
                _command_.CommandType = CommandType.StoredProcedure;
                _SQLCreater_.__setExecuteProcedureAndReturnParameter(_connection, procedureName, _command_, parameters);
                var r = _command_.ExecuteReader();
                DataTable tt = new DataTable();
                tt.Load(r);
                this.closeConnection();                
                logSQL(ref _command_, true);
                return tt;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                logSQL(ref _command_, false);
                throw ex;
            }
        }

        public DataTable[] ExecuteProcedureAndReturnTables(string procedureName, IDataParameter[] parameters)
        {
            try
            {
                this.openConnection();
                _command_ = _connection.getConnection().CreateCommand();
                this.initSQLCreater(_connection.getDataBaseConfigInfo());
                _command_.CommandText = _SQLCreater_.__ExecuteProcedureAndReturnSQL(procedureName);
                _command_.CommandType = CommandType.StoredProcedure;
                _SQLCreater_.__setExecuteProcedureAndReturnParameter(_connection, procedureName, _command_, parameters);
                List<DataTable> tbls = new List<DataTable>();

                var r = _command_.ExecuteReader();                
                DataTable tt = new DataTable();
                tt.Load(r);
                tbls.Add(tt);

                while (r.IsClosed == false && r.Read()) {
                    DataTable tmp = new DataTable();
                    tmp.Load(r);
                    tbls.Add(tmp);
                }

                this.closeConnection();

                logSQL(ref _command_, true);
                return tbls.ToArray();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                logSQL(ref _command_, false);
                throw ex;
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

        //public DB BeginTransactionSelect()
        //{
        //    try
        //    {
        //        if (_connection.getConnection().State == ConnectionState.Closed)
        //            _connection.getConnection().Open();
        //        __transaction__ = _connection.getConnection().BeginTransaction(IsolationLevel.ReadUncommitted);
        //        return this;
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error(ex);
        //        throw ex;
        //    }
        //}

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

        

        /*
        public void Connection() {
        
        }
         */

        public IDbConnection getConnection()
        {
            return _connection.getConnection();
        }

       
        public void ExecuteNonQuery() {
            try
            {
                this.openConnection();
                _command_ = _connection.getConnection().CreateCommand();
                _command_.CommandText = this.CommandText;
                _command_.CommandTimeout = _timeOut;
                _command_.Parameters.Clear();
                
                foreach (var item in this._commandParameter)
                {
                    _command_.Parameters.Add(item);
                }
                
                _command_.ExecuteNonQuery();
                
                this.closeConnection();
                logSQL(ref _command_, true);
            }
            catch (Exception ex)
            {                
                log.Error(ex);
                /*
                log.Error("------------------------------------------------------------------------------");
                log.Error(_command_.CommandText);
                foreach (System.Data.IDataParameter para in _command_.Parameters) {
                    log.Error(para.ParameterName + "=>" + para.Value);
                }
                log.Error("------------------------------------------------------------------------------");
                 * */
                logSQL(ref _command_, false);
                throw ex;
            }            
        }

        public void ExecuteNonQuery(DB db)
        {
            try
            {
                this.openConnection();
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
                _command_.ExecuteNonQuery();
                logSQL(ref _command_,true);
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
        private DataTable __FillDataTable(string tableName)
        {
            System.Data.DataSet ds = new DataSet();
            /*針對MySQLClient設置的項目EnforceConstraints一定要為false不然會掛掉*/
            ds.EnforceConstraints = false;
            try
            {
                System.Data.DataTable ret = new System.Data.DataTable(tableName);
                this.openConnection();
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
                var reader = _command_.ExecuteReader();
                ds.Tables.Add(ret);
                ret.Load(reader);
                logSQL(ref _command_, true);
                if (this.__transaction__ == null)
                {
                    this.closeConnection();
                }
                return ret;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                logSQL(ref _command_, false);
                throw ex;
            }
            finally {
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
                else
                {
                    throw new Exception("沒有初始資料連線!");
                }
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
