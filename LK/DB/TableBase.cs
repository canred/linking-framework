using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LK.DB;
using LK.Config.DataBase;
using LK.Attribute;
using LK.DB.SQLCreater;
using log4net;
using System.Reflection;
namespace LK.DB
{
    #region TableBase
    public class TableBase : iTable
    {
        public iTable _table = null;
        ADataBaseConnection _connection = null;
        private ASQLCreater _sqlCreater_ = null;
        private IDataBaseConfigInfo thisDataBaseConfigInfo = null;
        public static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        List<object> _parameter_ = new List<object>();
        SQLCondition _sqlCondition_ = null;       
        private object ParserVale(object value, Type convertsionType) {
            if (convertsionType.IsGenericType && convertsionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>))) {
                if (value == null || value.ToString().Length == 0) {
                    return null;
                }
                System.ComponentModel.NullableConverter nullableConverter = new System.ComponentModel.NullableConverter(convertsionType);
                convertsionType = nullableConverter.UnderlyingType;
            }
            return Convert.ChangeType(value, convertsionType);
        }
        public void setDataBaseConfigInfo(IDataBaseConfigInfo config){
            try
            {
                var classType = this.GetType().GetCustomAttributes(typeof(LkDataBase), true);
                var dataBaseName = ((LK.Attribute.LkDataBase)(classType[0])).getDataBase().ToUpper();
                if (config.GetDBType(dataBaseName).ToUpper() == "ORACLE")
                {
                    _sqlCreater_ = new LK.DB.SQLCreater.SQLCreaterOracle();
                }
                else if (config.GetDBType(dataBaseName).ToUpper() == "MSSQL")
                {
                    _sqlCreater_ = new LK.DB.SQLCreater.SQLCreaterMSSQL();
                }
                else
                {
                    _sqlCreater_ = new LK.DB.SQLCreater.SQLCreaterMySQL();
                }
                this.thisDataBaseConfigInfo = config;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public TableBase(IDataBaseConfigInfo config,string db) {
            try
            {
                var classType = this.GetType().GetCustomAttributes(typeof(LkDataBase), true);
                var dbType = ((LK.Attribute.LkDataBase)(classType[0])).getDataBase().ToUpper();
                if (dbType == "ORACLE")
                {
                    _sqlCreater_ = new LK.DB.SQLCreater.SQLCreaterOracle();
                }
                else if (dbType == "MSSQL")
                {
                    _sqlCreater_ = new LK.DB.SQLCreater.SQLCreaterMSSQL();
                }
                else if (dbType == "MYSQL")
                {
                    _sqlCreater_ = new LK.DB.SQLCreater.SQLCreaterMySQL();
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
        public System.Data.IDataParameter justGenParameter(string name, object value, System.Data.ParameterDirection parameterDirect )
        {
            try
            {
                var classType = this.GetType().GetCustomAttributes(typeof(LkDataBase), true);
                var dataBaseName = ((LK.Attribute.LkDataBase)(classType[0])).getDataBase().ToUpper();
                if (thisDataBaseConfigInfo == null) {
                    throw new NotImplementedException("未實作DataBaseConfigInfo為空");
                }
                if (thisDataBaseConfigInfo.GetDBType(dataBaseName).ToUpper() == "ORACLE")
                {
                    _sqlCreater_ = new LK.DB.SQLCreater.SQLCreaterOracle();
                }
                else if (thisDataBaseConfigInfo.GetDBType(dataBaseName).ToUpper() == "MSSQL")
                {
                    _sqlCreater_ = new LK.DB.SQLCreater.SQLCreaterMSSQL();
                }
                else if (thisDataBaseConfigInfo.GetDBType(dataBaseName).ToUpper() == "MYSQL")
                {
                    _sqlCreater_ = new LK.DB.SQLCreater.SQLCreaterMySQL();
                }
                else
                {
                    throw new NotImplementedException("未實作" + thisDataBaseConfigInfo.GetDBType().ToString() + "的SQLCreater");
                }
                return _sqlCreater_.justGenParameter(name, value,parameterDirect);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public TableBase(IDataBaseConfigInfo config)
        {
            try
            {
                var classType = this.GetType().GetCustomAttributes(typeof(LkDataBase), true);
                var dataBaseName = ((LK.Attribute.LkDataBase)(classType[0])).getDataBase().ToUpper();
                if (config.GetDBType(dataBaseName).ToUpper() == "ORACLE")
                {
                    _sqlCreater_ = new LK.DB.SQLCreater.SQLCreaterOracle();
                }
                else if (config.GetDBType(dataBaseName).ToUpper() == "MSSQL")
                {
                    _sqlCreater_ = new LK.DB.SQLCreater.SQLCreaterMSSQL();
                }
                else if (config.GetDBType(dataBaseName).ToUpper() == "MYSQL")
                {
                    _sqlCreater_ = new LK.DB.SQLCreater.SQLCreaterMySQL();
                }
                else
                {
                    throw new NotImplementedException("未實作" + config.GetDBType().ToString() + "的SQLCreater");
                }
                this.thisDataBaseConfigInfo = config;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }        
        public void setSQLCreater(ASQLCreater sc) {
            try
            {
                this._sqlCreater_ = sc;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public TableBase(object modelObj)
        {
            try
            {
                setInit(modelObj);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        private void setInit(object modelObj) {
            try
            {
                var attrs = modelObj.GetType().GetCustomAttributes(typeof(LkDataBase), false);
                string dbName = null;
                if (attrs.Length == 1)
                {
                    dbName = ((LkDataBase)(attrs[0])).getDataBase();
                    IDataBaseConfigInfo a = Factory.getInfo();
                    this.thisDataBaseConfigInfo = a;
                    DataBaseConnection obj = new DataBaseConnection(a);
                    obj.setDataBase(dbName);
                    _connection = obj;
                    _sqlCondition_ = new SQLCondition(this);
                    if (_sqlCreater_ == null)
                    {
                        var config = LK.Config.DataBase.Factory.getInfo();
                        if (config.GetDBType(dbName).ToUpper() == "ORACLE")
                        {
                            _sqlCreater_ = new LK.DB.SQLCreater.SQLCreaterOracle();
                        }
                        else if (config.GetDBType(dbName).ToUpper() == "MSSQL")
                        {
                            _sqlCreater_ = new LK.DB.SQLCreater.SQLCreaterMSSQL();
                        }
                        else if (config.GetDBType(dbName).ToUpper() == "MYSQL")
                        {
                            _sqlCreater_ = new LK.DB.SQLCreater.SQLCreaterMySQL();
                        }
                        else
                        {
                            throw new NotImplementedException("未實作" + config.GetDBType().ToString() + "的SQLCreater");
                        }                        
                    }
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
        public TableBase() {         
        }
        public System.Data.DataTable RunSpReturnTable(string spName, System.Data.IDataParameter[] pParams)
        {
            try
            {
                DB db = new DB(this);
                System.Data.DataTable dt = db.ExecuteProcedureAndReturnTable(spName, pParams);
                return dt;
            }
            catch (Exception ex)
            {
                log.Error(ex); LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        public System.Data.DataTable[] RunSpReturnTables(string spName, System.Data.IDataParameter[] pParams)
        {
            try
            {
                DB db = new DB(this);
                return  db.ExecuteProcedureAndReturnTables(spName, pParams);
                
            }
            catch (Exception ex)
            {
                log.Error(ex); LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        public System.Data.IDataParameter getParameter(string pName, object value, System.Data.ParameterDirection pType)
        {
            DB db = new DB(this);
            return db.getParameter(pName, value, pType);
        }		
		public System.Data.IDataParameter getParameter(string pName, object value,System.Data.DbType dbtype, System.Data.ParameterDirection pType)
        {
            DB db = new DB(this);
            return db.getParameter(pName, value,dbtype, pType);
        }		
        public TableBase From<T>(iTable a)
        where T : iTable, new()
        {
            try
            {
                _table = new T();
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public TableBase Select(string col)
        {

            try
            {
                _sqlCreater_.setSelectColumn(col);
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public TableBase Count(string col)
        {
            try
            {                
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public TableBase SelectAll()
        {
            try
            {
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public TableBase Where(string whereStr)
        {
            try
            {
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public TableBase Where(SQLCondition condition)
        {
            try
            {
                _sqlCreater_.Where(condition);
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public TableBase SetUpdate(SQLUpdate sqlupdate) 
        {
            try
            {
                if (_sqlCreater_ == null) {
                    setInit(this);
                }
                _sqlCreater_.SetUpdate(sqlupdate);
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public TableBase SetDelete(SQLDelete sqldelete)
        {
            try
            {
                _sqlCreater_.SetDelete(sqldelete);
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }      
        public TableBase On(SQLCondition condition)
        {
            try
            {
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public TableBase In(string col, TableBase condition)
        {
            try
            {
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public TableBase Order(string strOrder)
        {
            try
            {
                _sqlCreater_.Order(strOrder);
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public TableBase Order(OrderLimit limit)
        {
            try
            {
                if (limit == null) {
                    return this;
                }
                if (limit.getOrderColumn().Count > 0 && limit.getOrderMethod().Count()>0) {
                    int i = 0;
                    for (i = 0; i < limit.getOrderMethod().Count(); i++) {
                        
                        if (LK.DB.SQLCreater.OrderLimit.OrderMethod.ASC == limit.getOrderMethod()[i]) {
                            _sqlCreater_.OrderASC(limit.getOrderColumn()[i]);
                        }
                        else if (LK.DB.SQLCreater.OrderLimit.OrderMethod.DESC == limit.getOrderMethod()[i])
                        {
                            _sqlCreater_.OrderDESC(limit.getOrderColumn()[i]);
                        }                        
                    }                   
                }                
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public TableBase OrderASC(string strOrder)
        {
            try
            {
                _sqlCreater_.OrderASC(strOrder);
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public TableBase OrderDESC(string strOrder)
        {
            try
            {
                _sqlCreater_.OrderDESC(strOrder);
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        public TableBase GroupBy(string groupString)
        {
            try
            {
                _sqlCreater_.GroupBy(groupString);
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }       
        public TableBase Limit(OrderLimit limit)
        {
            try
            {
                if (limit != null)
                {
                    if (limit.Start != null && limit.Limit != null)
                    {
                        _sqlCreater_.Limit(limit);
                    }                   
                }
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public IList<T> DataTable2Record<T>(System.Data.DataTable data)
        where T : RecordBase,new()
        {
            try
            {
                IList<T> ret = new List<T>();
                foreach (System.Data.DataRow dr in data.Rows)
                {
                    T dataItem = new T();
                    foreach (System.Data.DataColumn dc in data.Columns)
                    {
                        if (dataItem.GetType().GetProperty(dc.ColumnName.ToUpper()) != null)
                        {
                            Type dcType = dataItem.GetType().GetProperty(dc.ColumnName.ToUpper()).PropertyType;
                            if (dcType == typeof(string))
                            {
                                #region string
                                dataItem.GetType().GetProperty(dc.ColumnName.ToUpper()).SetValue(dataItem, dr[dc].ToString(), null);
                                #endregion
                            }
                            else if (dcType == typeof(DateTime?))
                            {
                                #region DateTime?
                                if (dr[dc] != System.DBNull.Value)
                                {
                                    dataItem.GetType().GetProperty(dc.ColumnName.ToUpper()).SetValue(dataItem, System.Convert.ToDateTime(dr[dc]), null);
                                }
                                else
                                {
                                    dataItem.GetType().GetProperty(dc.ColumnName.ToUpper()).SetValue(dataItem, null, null);
                                }
                                #endregion
                            }
                            else if (dcType == typeof(Decimal?))
                            {
                                #region Decimal
                                try
                                {
                                    if (dr[dc] != System.DBNull.Value)
                                    {
                                        dataItem.GetType().GetProperty(dc.ColumnName.ToUpper()).SetValue(dataItem, System.Convert.ToDecimal(dr[dc]), null);
                                    }
                                    else
                                    {
                                        dataItem.GetType().GetProperty(dc.ColumnName.ToUpper()).SetValue(dataItem, null, null);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    throw ex;
                                }
                                #endregion
                            }
                            else if (dcType == typeof(Double?))
                            {
                                #region Double
                                try
                                {
                                    if (dr[dc] != System.DBNull.Value)
                                    {
                                        dataItem.GetType().GetProperty(dc.ColumnName.ToUpper()).SetValue(dataItem, System.Convert.ToDouble(dr[dc]), null);
                                    }
                                    else
                                    {
                                        dataItem.GetType().GetProperty(dc.ColumnName.ToUpper()).SetValue(dataItem, null, null);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    throw ex;
                                }
                                #endregion
                            }
                            else if (dcType == typeof(DateTime?))
                            {
                                #region DateTime
                                try
                                {
                                    if (dr[dc] != System.DBNull.Value)
                                    {
                                        dataItem.GetType().GetProperty(dc.ColumnName.ToUpper()).SetValue(dataItem, System.Convert.ToDateTime(dr[dc]), null);
                                    }
                                    else
                                    {
                                        dataItem.GetType().GetProperty(dc.ColumnName.ToUpper()).SetValue(dataItem, null, null);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    throw ex;
                                }
                                #endregion
                            }
                            else if (dcType == typeof(int?))
                            {
                                #region int
                                try
                                {
                                    if (dr[dc] != System.DBNull.Value)
                                    {
                                        dataItem.GetType().GetProperty(dc.ColumnName.ToUpper()).SetValue(dataItem, System.Convert.ToInt32(dr[dc]), null);
                                    }
                                    else
                                    {
                                        dataItem.GetType().GetProperty(dc.ColumnName.ToUpper()).SetValue(dataItem, null, null);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    throw ex;
                                }
                                #endregion
                            }
                            else if (dcType == typeof(float?))
                            {
                                #region float
                                try
                                {
                                    if (dr[dc] != System.DBNull.Value)
                                    {
                                        dataItem.GetType().GetProperty(dc.ColumnName.ToUpper()).SetValue(dataItem, float.Parse(dr[dc].ToString()), null);
                                    }
                                    else
                                    {
                                        dataItem.GetType().GetProperty(dc.ColumnName.ToUpper()).SetValue(dataItem, null, null);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    throw ex;
                                }
                                #endregion
                            }
                            else if (dcType == typeof(short?))
                            {
                                #region short
                                try
                                {
                                    if (dr[dc] != System.DBNull.Value)
                                    {
                                        dataItem.GetType().GetProperty(dc.ColumnName.ToUpper()).SetValue(dataItem, short.Parse(dr[dc].ToString()), null);
                                    }
                                    else
                                    {
                                        dataItem.GetType().GetProperty(dc.ColumnName.ToUpper()).SetValue(dataItem, null, null);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    throw ex;
                                }
                                #endregion
                            }
                            else if (dcType == typeof(byte?))
                            {
                                #region byte
                                try
                                {
                                    if (dr[dc] != System.DBNull.Value)
                                    {
                                        dataItem.GetType().GetProperty(dc.ColumnName.ToUpper()).SetValue(dataItem,System.Convert.ToByte(dr[dc]), null);
                                    }
                                    else
                                    {
                                        dataItem.GetType().GetProperty(dc.ColumnName.ToUpper()).SetValue(dataItem, null, null);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    throw ex;
                                }
                                #endregion
                            }
                            else if (dcType == typeof(ushort?))
                            {
                                #region ushort
                                try
                                {
                                    if (dr[dc] != System.DBNull.Value)
                                    {
                                        dataItem.GetType().GetProperty(dc.ColumnName.ToUpper()).SetValue(dataItem, ushort.Parse(dr[dc].ToString()), null);
                                    }
                                    else
                                    {
                                        dataItem.GetType().GetProperty(dc.ColumnName.ToUpper()).SetValue(dataItem, null, null);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    throw ex;
                                }
                                #endregion
                            }
                            else if (dcType == typeof(uint?))
                            {
                                #region uint
                                try
                                {
                                    if (dr[dc] != System.DBNull.Value)
                                    {
                                        dataItem.GetType().GetProperty(dc.ColumnName.ToUpper()).SetValue(dataItem, System.Convert.ToUInt32(dr[dc]), null);
                                    }
                                    else
                                    {
                                        dataItem.GetType().GetProperty(dc.ColumnName.ToUpper()).SetValue(dataItem, null, null);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    throw ex;
                                }
                                #endregion
                            }
                            else if (dcType == typeof(byte[]))
                            {
                                #region byte
                                try
                                {
                                    if (dr[dc] != System.DBNull.Value)
                                    {
                                        dataItem.GetType().GetProperty(dc.ColumnName.ToUpper()).SetValue(dataItem, (byte[])(dr[dc]), null);
                                    }
                                    else
                                    {
                                        dataItem.GetType().GetProperty(dc.ColumnName.ToUpper()).SetValue(dataItem, null, null);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    throw ex;
                                }
                                #endregion
                            }
                            else if (dcType == typeof(bool?))
                            {
                                #region bool
                                try
                                {
                                    if (dr[dc] != System.DBNull.Value)
                                    {
                                        dataItem.GetType().GetProperty(dc.ColumnName.ToUpper()).SetValue(dataItem, System.Convert.ToBoolean(dr[dc]), null);
                                    }
                                    else
                                    {
                                        dataItem.GetType().GetProperty(dc.ColumnName.ToUpper()).SetValue(dataItem, null, null);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    throw ex;
                                }
                                #endregion
                            }
                            else if (dcType == typeof(TimeSpan?))
                            {
                                #region TimeSpan
                                try
                                {
                                    if (dr[dc] != System.DBNull.Value)
                                    {
                                        dataItem.GetType().GetProperty(dc.ColumnName.ToUpper()).SetValue(dataItem,TimeSpan.Parse(dr[dc].ToString()), null);
                                    }
                                    else
                                    {
                                        dataItem.GetType().GetProperty(dc.ColumnName.ToUpper()).SetValue(dataItem, null, null);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    throw ex;
                                }
                                #endregion
                            }                         
                        }
                    }
                    ret.Add(dataItem);
                }
                return ret;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public T FetchOne<T>()
             where T : RecordBase, new() {
                 try
                 {
                     return FetchAll<T>().First();
                 }
                 catch {
                     return null;
                 }
        }
        public T FetchOne<T>(DB db)
             where T : RecordBase, new()
        {
            try
            {
                return FetchAll<T>(db).First();
            }
            catch
            {
                return null;
            }
        }
        public IList<T> FetchAll<T>()
            where T: RecordBase , new()
        {
            try
            {
                DB db = new DB(this);
                try
                {                  
                    db.CommandText = _sqlCreater_.FetchAllSQL();
                    db.addParameter(_sqlCreater_.PARAMETER());
                    var data = db.FillDataTable("Data");
                    return DataTable2Record<T>(data);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    db = null;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }     
        public IList<T> FetchAll<T>(DB db)
            where T : RecordBase, new()
        {
            try
            {                
                try
                {
                    db.CommandText = _sqlCreater_.FetchAllSQL();
                    db.addParameter(_sqlCreater_.PARAMETER());
                    var data = db.FillDataTable("Data");
                    return DataTable2Record<T>(data);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    db = null;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public System.Data.DataTable FetchAll()    
        {
            try
            {
                DB db = new DB(this);
                try
                {
                    db.CommandText = _sqlCreater_.FetchAllSQL();
                    db.addParameter(_sqlCreater_.PARAMETER());
                    var data = db.FillDataTable("Data");
                    foreach (System.Data.DataColumn c in data.Columns) {
                        c.ColumnName = c.ColumnName.ToUpper();
                    }
                    return data;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    db = null;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public int FetchCount()
        {
            try
            {
                DB db = new DB(this);
                try
                {
                    db.CommandText = _sqlCreater_.FetchCountSQL();
                    db.addParameter(_sqlCreater_.PARAMETER());
                    var data = db.FillDataTable("Data");
                    return System.Convert.ToInt32(data.Rows[0][0].ToString());                    
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    db = null;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public int FetchCount(DB db)
        {
            try
            {                
                try
                {
                    db.CommandText = _sqlCreater_.FetchCountSQL();
                    db.addParameter(_sqlCreater_.PARAMETER());
                    var data = db.FillDataTable("Data");
                    return System.Convert.ToInt32(data.Rows[0][0].ToString());
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
        private object getPropValue(RecordBase rb,string name) {
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
        /// 執行UPDATE(完全更新)
        /// </summary>
        /// <param name="oneData"></param>
        public void Update(RecordBase oneData) {
            try
            {
                /*找出我的PK*/
                var pks = oneData.getPK();
                /*依PK的欄位產生Condition*/
                SQLCondition condition = new SQLCondition(oneData);
                foreach (var pk in pks)
                {
                    /*在物件抓出PK的值*/
                    object value = getPropValue(oneData, pk.Name);                    
                    condition.Equal(
                                    pk.Name,
                                    ParserVale(value, pk.ColumnType)
                                    ).And();
                }
                /*檢查SQL語句*/
                condition.CheckSQL();                
                if (_sqlCreater_ == null)
                {
                    setInit(this);
                }
                DB db = new DB(this);
                db.removeAllParameter();
                _sqlCreater_.removeSelfParameter();
                _sqlCreater_.isComplete = ASQLCreater.SQLComplete.Complete;
                db.CommandText = _sqlCreater_.Update(oneData).Where(condition).SQL();
                db.addParameter(_sqlCreater_.PARAMETER());
                db.ExecuteNonQuery();
                db = null;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }    
        }
        public void Update(RecordBase oneData,DB db)
        {
            try
            {
                /*找出我的PK*/
                var pks = oneData.getPK();
                /*依PK的欄位產生Condition*/
                SQLCondition condition = new SQLCondition(oneData);
                foreach (var pk in pks)
                {
                    /*在物件抓出PK的值*/
                    object value = getPropValue(oneData, pk.Name);
                    condition.Equal(
                                    pk.Name,
                                    ParserVale(value, pk.ColumnType)
                                    ).And();
                }
                /*檢查SQL語句*/
                condition.CheckSQL();
                if (_sqlCreater_ == null)
                {
                    setInit(this);
                }                
                db.removeAllParameter();
                _sqlCreater_.removeSelfParameter();
                _sqlCreater_.isComplete = ASQLCreater.SQLComplete.Complete;
                db.CommandText = _sqlCreater_.Update(oneData).Where(condition).SQL();
                db.addParameter(_sqlCreater_.PARAMETER());
                db.ExecuteNonQuery(db);                
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        /// <summary>
        /// 在執行UPDATE時，會先處理字串變成NULL值
        /// </summary>
        /// <param name="oneData"></param>
        public void Update_Empty2Null(RecordBase oneData)
        {
            try
            {
                /*找出我的PK*/
                var pks = oneData.getPK();
                /*依PK的欄位產生Condition*/
                SQLCondition condition = new SQLCondition(oneData);
                foreach (var pk in pks)
                {
                    /*在物件抓出PK的值*/
                    object value = getPropValue(oneData, pk.Name);
                    condition.Equal(
                                    pk.Name,
                                    ParserVale(value, pk.ColumnType)
                                    ).And();
                }
                /*檢查SQL語句*/
                condition.CheckSQL();
                if (_sqlCreater_ == null)
                {
                    setInit(this);
                }
                DB db = new DB(this);
                db.removeAllParameter();
                _sqlCreater_.removeSelfParameter();
                _sqlCreater_.isComplete = ASQLCreater.SQLComplete.Complete;
                db.CommandText = _sqlCreater_.Update_Empty2Null(oneData).Where(condition).SQL();
                db.addParameter(_sqlCreater_.PARAMETER());
                db.ExecuteNonQuery();
                db = null;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public void Update_Empty2Null(RecordBase oneData, LK.DB.DB db)
        {
            try
            {
                /*找出我的PK*/
                var pks = oneData.getPK();
                /*依PK的欄位產生Condition*/
                SQLCondition condition = new SQLCondition(oneData);
                foreach (var pk in pks)
                {
                    /*在物件抓出PK的值*/
                    object value = getPropValue(oneData, pk.Name);
                    condition.Equal(
                                    pk.Name,
                                    System.Convert.ChangeType(value, pk.ColumnType)
                                    ).And();
                }
                /*檢查SQL語句*/
                condition.CheckSQL();
                if (_sqlCreater_ == null)
                {
                    setInit(this);
                }
                db.removeAllParameter();
                _sqlCreater_.removeSelfParameter();
                db.CommandText = _sqlCreater_.Update_Empty2Null(oneData).Where(condition).SQL();
                db.addParameter(_sqlCreater_.PARAMETER());
                db.ExecuteNonQuery(db);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public void PartialUpdate(RecordBase oneData)
        {
            try
            {
                /*找出我的PK*/
                var pks = oneData.getPK();
                /*依PK的欄位產生Condition*/
                SQLCondition condition = new SQLCondition(oneData);
                foreach (var pk in pks)
                {
                    /*在物件抓出PK的值*/
                    object value = getPropValue(oneData, pk.Name);
                    condition.Equal(
                                    pk.Name,
                                    ParserVale(value, pk.ColumnType)
                                    ).And();
                }
                /*檢查SQL語句*/
                condition.CheckSQL();
                if (_sqlCreater_ == null)
                {
                    setInit(this);
                }
                DB db = new DB(this);
                db.removeAllParameter();
                _sqlCreater_.removeSelfParameter();
                _sqlCreater_.isComplete = ASQLCreater.SQLComplete.Complete;
                db.CommandText = _sqlCreater_.PartialUpdate(oneData).Where(condition).SQL();
                db.addParameter(_sqlCreater_.PARAMETER());
                db.ExecuteNonQuery();
                db = null;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }        
        public void UpdateAllRecord<T>(IList<T> pAllRecord)
        where T : RecordBase{
            try
            {
                foreach (RecordBase item in pAllRecord)
                {
                    this.Update(item);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        public void UpdateAllRecord<T>(IList<T> pAllRecord,DB db)
where T : RecordBase
        {
            try
            {
                foreach (RecordBase item in pAllRecord)
                {
                    this.Update(item, db);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public void Insert(RecordBase oneData) {
            try
            {
                if (_sqlCreater_ == null)
                {
                    setInit(this);
                }
                DB db = new DB(this);
                db.removeAllParameter();
                _sqlCreater_.removeSelfParameter();
                _sqlCreater_.isComplete = ASQLCreater.SQLComplete.NULL;
                db.CommandText = _sqlCreater_.Insert(oneData).SQL();
                db.addParameter(_sqlCreater_.PARAMETER());
                db.ExecuteNonQuery();
                db = null;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public void Insert_Empty2Null(RecordBase oneData)
        {
            try
            {
                if (_sqlCreater_ == null)
                {
                    setInit(this);
                }
                DB db = new DB(this);
                db.removeAllParameter();
                _sqlCreater_.removeSelfParameter();
                _sqlCreater_.isComplete = ASQLCreater.SQLComplete.NULL;
                db.CommandText = _sqlCreater_.Insert_Empty2Null(oneData).SQL();
                db.addParameter(_sqlCreater_.PARAMETER());
                db.ExecuteNonQuery();
                db = null;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public void Insert_Empty2Null(RecordBase oneData, DB db)
        {
            try
            {
                if (_sqlCreater_ == null)
                {
                    setInit(this);
                }                
                db.removeAllParameter();
                _sqlCreater_.removeSelfParameter();
                _sqlCreater_.isComplete = ASQLCreater.SQLComplete.NULL;
                db.CommandText = _sqlCreater_.Insert_Empty2Null(oneData).SQL();
                db.addParameter(_sqlCreater_.PARAMETER());
                db.ExecuteNonQuery(db);                
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }        
        public void Insert(RecordBase oneData,DB db)
        {
            try
            {
                if (_sqlCreater_ == null)
                {
                    setInit(this);
                }
                db.removeAllParameter();
                _sqlCreater_.removeSelfParameter();
                db.CommandText = _sqlCreater_.Insert(oneData).SQL();
                db.addParameter(_sqlCreater_.PARAMETER());
                db.ExecuteNonQuery(db);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }           
        }
        public void InsertAllRecord<T>(IList<T> pAllRecord)
where T : RecordBase
        {
            try
            {
                foreach (RecordBase item in pAllRecord)
                {
                    this.Insert(item);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public void InsertAllRecord<T>(IList<T> pAllRecord,DB db)
where T : RecordBase
        {
            try
            {
                foreach (RecordBase item in pAllRecord)
                {
                    this.Insert(item, db);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public void Delete(RecordBase oneData)
        {
            try
            {
                /*找出我的PK*/
                var pks = oneData.getPK();
                /*依PK的欄位產生Condition*/
                SQLCondition condition = new SQLCondition(oneData);
                foreach (var pk in pks)
                {
                    /*在物件抓出PK的值*/
                    object value = getPropValue(oneData, pk.Name);
                    condition.Equal(
                                    pk.Name,
                                    ParserVale(value,pk.ColumnType)                                 
                                    ).And();
                }
                /*檢查SQL語句*/
                condition.CheckSQL();
                if (_sqlCreater_ == null)
                {
                    setInit(this);
                }
                DB db = new DB(this);
                db.removeAllParameter();
                _sqlCreater_.removeSelfParameter();
                _sqlCreater_.isComplete = ASQLCreater.SQLComplete.UNComplete;
                db.CommandText = _sqlCreater_.Delete(oneData).Where(condition).SQL();
                db.addParameter(_sqlCreater_.PARAMETER());
                _sqlCreater_.isComplete = ASQLCreater.SQLComplete.Complete;
                db.ExecuteNonQuery();
                db = null;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public enum TruncateConfirm { 
            OK,Null
        }
       /// <summary>
       /// Truncate Table 
       /// </summary>
        public void TruncateTable(TruncateConfirm pTruncateConfirm)
        {
            try
            {
                if (pTruncateConfirm == TruncateConfirm.OK)
                {
                    DB db = new DB(this);
                    SQLCondition condition = new SQLCondition(this);
                    if (_sqlCreater_ == null)
                    {
                        setInit(this);
                    }
                    _sqlCreater_.setTableName(this.getTableName());
                    db.removeAllParameter();
                    _sqlCreater_.removeSelfParameter();
                    db.CommandText = _sqlCreater_.TruncateTable().SQL();
                    db.ExecuteNonQuery(db);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public string getTableName()
        {
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
        public void Delete(RecordBase oneData,DB db)
        {
            try
            {
                /*找出我的PK*/
                var pks = oneData.getPK();
                /*依PK的欄位產生Condition*/
                SQLCondition condition = new SQLCondition(oneData);
                foreach (var pk in pks)
                {
                    /*在物件抓出PK的值*/
                    object value = getPropValue(oneData, pk.Name);
                    condition.Equal(
                                    pk.Name,
                                    System.Convert.ChangeType(value, pk.ColumnType)
                                    ).And();
                }
                /*檢查SQL語句*/
                condition.CheckSQL();
                if (_sqlCreater_ == null)
                {
                    setInit(this);
                }
                db.removeAllParameter();
                _sqlCreater_.removeSelfParameter();
                db.CommandText = _sqlCreater_.Delete(oneData).Where(condition).SQL();
                db.addParameter(_sqlCreater_.PARAMETER());
                db.ExecuteNonQuery(db);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public void DeleteAllRecord<T>(IList<T> pAllRecord)
where T : RecordBase
        {
            try
            {
                foreach (RecordBase item in pAllRecord)
                {
                    this.Delete(item);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        public void DeleteAllRecord<T>(IList<T> pAllRecord,DB db)
where T : RecordBase
        {
            try
            {
                foreach (RecordBase item in pAllRecord)
                {
                    this.Delete(item, db);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        protected void InsertByRow<TableSchema>()
            where TableSchema : System.Data.DataTable, new()
        {          
        }
        public string SQL() {
            try
            {
                return _sqlCreater_.SQL();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public TableBase InPackage()
        {
            try
            {
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public TableBase Bind(Array array)
        {
            try
            {
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }        
        public void ExecuteUpdate()
        {
            try
            {
                DB db = new DB(this);
                try
                {
                    db.CommandText = _sqlCreater_.UpdateSQL();
                    db.addParameter(_sqlCreater_.PARAMETER());
                    db.ExecuteNonQuery();                    
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    db = null;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public void ExecuteUpdate(DB Db)
        {
            try
            {
                DB db = new DB(this);
                try
                {
                    db.CommandText = _sqlCreater_.FetchAllSQL();
                    db.addParameter(_sqlCreater_.PARAMETER());
                    db.ExecuteNonQuery(Db);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    db = null;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public void ExecuteDelete()
        {
            try
            {
                DB db = new DB(this);
                try
                {
                    db.CommandText = _sqlCreater_.DeleteSQL();
                    db.addParameter(_sqlCreater_.PARAMETER());
                    db.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    db = null;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public void ExecuteDelete(DB Db)
        {
            try
            {
                DB db = new DB(this);
                try
                {
                    db.CommandText = _sqlCreater_.FetchAllSQL();
                    db.addParameter(_sqlCreater_.PARAMETER());
                    db.ExecuteNonQuery(Db);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    db = null;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public void ExecuteInsert()
        {
            try
            {
                DB db = new DB(this);
                try
                {
                    db.CommandText = _sqlCreater_.FetchAllSQL();
                    db.addParameter(_sqlCreater_.PARAMETER());
                    db.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    db = null;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public void ExecuteInsert(DB Db)
        {
            try
            {
                DB db = new DB(this);
                try
                {
                    db.CommandText = _sqlCreater_.FetchAllSQL();
                    db.addParameter(_sqlCreater_.PARAMETER());
                    db.ExecuteNonQuery(Db);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    db = null;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
    }
    #endregion
}
