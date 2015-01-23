using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using System.Reflection;
using System.Data;
namespace LK.DB.SQLCreater
{
    /// <summary>
    /// 產生SQL語句的抽像類別
    /// </summary>
    public abstract class ASQLCreater:IDisposable,LK.DB.SQLCreater.ISQLCreater
    {
        public string _SQL_ = "";
        public string _UPDATESQL_ = "";
        public string _DELETESQL_ = "";
        string TableName = "";
        string Select_Column = "";
        string Update_Column = "";

        decimal? startCount = null;
        decimal? fetchCount = null;
        OrderLimit splitOrderLimit = null;
        string strOrder = null;
        string strGroupBy = null;
		public string getTableName() {
            return this.TableName;
        }
        public enum SQLComplete { 
            NULL,UNComplete,Complete
        }
        public SQLComplete isComplete = SQLComplete.NULL;
        public void setSelectColumn(string selectColumn) {
            this.Select_Column = selectColumn ;
        }

        public void setUpdateColumn(string updateColumn)
        {
            this.Select_Column = updateColumn;
        }
        public static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        /*_SelfPARAMETER是一個給SQL Creater用的…一盤情況下不要操作它*/
        public List<object> _SelfPARAMETER_ = new List<object>();
        /*_PARAMETER_是一個給使用者餵入，一般情況下是使用者自已操作DataCommand物件*/
        List<System.Data.IDataParameter> _PARAMETER_ = new List<System.Data.IDataParameter>();


        public OrderLimit getSplitOrderLimit() {
            return splitOrderLimit;
        }

        public void setSplitOrderLimit(OrderLimit limit)
        {
            splitOrderLimit = limit;
        }

        /// <summary>
        /// 使用限制筆數功能時，取出"從第幾筆開始"
        /// </summary>
        /// <returns></returns>
        public virtual decimal? getStartCount() {
            return startCount;
        }
        /// <summary>
        /// 使用限制筆數功能時，取出"一共取多少筆資料出來"
        /// </summary>
        /// <returns></returns>
        public virtual decimal? getFetchCount() {
            
            return fetchCount;
        }
        public ASQLCreater() { 
        
        }

        /*取得要執行的SQL語句*/
        /// <summary>
        /// 取得要執行的SQL語句
        /// </summary>
        /// <returns></returns>
        public virtual string SQL() {
            try
            {
                _SQL_ = adjuestSql(_SQL_);
                return _SQL_;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        /*取得要執行的Select ALL 的 SQL語句*/
        /// <summary>
        /// 取得要執行的Select ALL 的 SQL語句
        /// </summary>
        /// <returns></returns>
        public virtual string FetchAllSQL() {
            try
            {

                if (Select_Column.Length == 0)
                {
                    _SQL_ = " SELECT * FROM " + TableName + "   " + _SQL_;
                }
                else {
                    _SQL_ = " SELECT "+Select_Column+" FROM " + TableName + "   " + _SQL_;
                }
               
                _SQL_ = this.GroupBySQL(_SQL_);  
                _SQL_ = this.OrderSQL(_SQL_);
                _SQL_ = this.LimitSQL(_SQL_);
                return adjuestSql(_SQL_);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        public virtual string FetchCountSQL()
        {
            try
            {
                if (Select_Column.Length == 0)
                {
                    _SQL_ = " SELECT COUNT(*) FROM " + TableName + "   " + _SQL_;
                }
                _SQL_ = this.LimitSQL(_SQL_);
                _SQL_ = this.OrderSQL(_SQL_);
                return adjuestSql(_SQL_);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }


        

        /*在RecordBase的物件上，取得它的值*/
        /// <summary>
        /// 在RecordBase的物件上，取得它的值
        /// </summary>
        /// <param name="rb"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private object getPropValue(RecordBase rb, string name)
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


        /*
         取得要執行的Update 的 的部份SQL語句，產出的語句不含WHERE的語句，
         使用的時侯要搭配where方法使用，由where餵入SQLCondition.
         */
        /// <summary>
        /// 取得要執行的Update 的 的部份SQL語句，產出的語句不含WHERE的語句，
        /// 使用的時侯要搭配where方法使用，由where餵入SQLCondition.
        /// </summary>
        /// <param name="rc"></param>
        /// <returns></returns>
        public ASQLCreater Update(LK.DB.RecordBase rc) {
            try
            {
                isComplete = SQLComplete.UNComplete;
                this._SQL_ = "UPDATE " + rc.getTableName();
                this._SQL_ += " SET ";
                Int16 no = 1;
                foreach (LK.Attribute.ColumnName col in rc.getColumnWithOutPK())
                {
                    this._SQL_ += col.Name + " = :######,";
                    try
                    {
                        object value = rc.getPropValue(rc, col.Name);
                        if (value == null)
                        {
                            _SelfPARAMETER_.Add(System.DBNull.Value);
                        }
                        else
                        {
                            _SelfPARAMETER_.Add(value);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    no++;
                }
                if (this._SQL_.EndsWith(","))
                {
                    this._SQL_ = this._SQL_.Substring(0, this._SQL_.Length - 1);
                }
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        public ASQLCreater Update_Empty2Null(LK.DB.RecordBase rc)
        {
            try
            {
                isComplete = SQLComplete.UNComplete;
                this._SQL_ = "UPDATE " + rc.getTableName();
                this._SQL_ += " SET ";
                Int16 no = 1;
                foreach (LK.Attribute.ColumnName col in rc.getColumnWithOutPK())
                {
                    this._SQL_ += col.Name + " = :######,";
                    try
                    {
                        object value = rc.getPropValue(rc, col.Name);
                        if (value == null)
                        {
                            _SelfPARAMETER_.Add(System.DBNull.Value);
                        }
                        else if(value.ToString()==""){
                            _SelfPARAMETER_.Add(System.DBNull.Value);
                        }
                        else
                        {
                            _SelfPARAMETER_.Add(value);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    no++;
                }
                if (this._SQL_.EndsWith(","))
                {
                    this._SQL_ = this._SQL_.Substring(0, this._SQL_.Length - 1);
                }
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public ASQLCreater PartialUpdate(LK.DB.RecordBase rc)
        {
            try
            {
                isComplete = SQLComplete.UNComplete;
                this._SQL_ = "UPDATE " + rc.getTableName();
                this._SQL_ += " SET ";
                Int16 no = 1;
                foreach (LK.Attribute.ColumnName col in rc.getColumnWithOutPK())
                {                   
                    try
                    {
                        object value = rc.getPropValue(rc, col.Name);
                        if (value != null)
                        {
                            this._SQL_ += col.Name + " = :######,";
                            _SelfPARAMETER_.Add(value);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    no++;
                }
                if (this._SQL_.EndsWith(","))
                {
                    this._SQL_ = this._SQL_.Substring(0, this._SQL_.Length - 1);
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
        /// 取得要執行的Delete 的 的部份SQL語句，產出的語句不含WHERE的語句，
        /// 使用的時侯要搭配where方法使用，由where餵入SQLCondition. 
        /// </summary>
        /// <param name="rc"></param>
        /// <returns></returns>
        public virtual ASQLCreater Delete(LK.DB.RecordBase rc)
        {
            try
            {
                isComplete = SQLComplete.UNComplete;
                this._SQL_ = "Delete " + rc.getTableName();
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        /*取得要執行的INSERT INTO 的 SQL語句*/
        /// <summary>
        /// 取得要執行的INSERT INTO 的 SQL語句
        /// </summary>
        /// <param name="rc"></param>
        /// <returns></returns>
        public virtual ASQLCreater Insert(LK.DB.RecordBase rc)
        {
            try
            {
                isComplete = SQLComplete.UNComplete;
                this._SQL_ = "INSERT INTO " + rc.getTableName() + " ";
                Int16 no = 1;
                this._SQL_ += " ( ";
                foreach (LK.Attribute.ColumnName col in rc.getAllColumn())
                {
                    if (rc.getPropValue(rc, col.Name) != null)
                    {
                        this._SQL_ += col.Name + ",";
                    }
                }
                if (this._SQL_.EndsWith(","))
                {
                    this._SQL_ = this._SQL_.Substring(0, this._SQL_.Length - 1);
                }
                this._SQL_ += " ) ";
                this._SQL_ += " VALUES (";
                foreach (LK.Attribute.ColumnName col in rc.getAllColumn())
                {
                    
                    try
                    {
                        object value = rc.getPropValue(rc, col.Name);
                        if (value != null)
                        {
                            this._SQL_ += ":######,";
                            _SelfPARAMETER_.Add(value);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    no++;
                }
                if (this._SQL_.EndsWith(","))
                {
                    this._SQL_ = this._SQL_.Substring(0, this._SQL_.Length - 1);
                }
                this._SQL_ += " ) ";
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        public virtual ASQLCreater Insert_Empty2Null(LK.DB.RecordBase rc)
        {
            try
            {
                isComplete = SQLComplete.UNComplete;
                this._SQL_ = "INSERT INTO " + rc.getTableName() + " ";
                Int16 no = 1;
                this._SQL_ += " ( ";
                foreach (LK.Attribute.ColumnName col in rc.getAllColumn())
                {
                    if (rc.getPropValue(rc, col.Name) != null)
                    {
                        this._SQL_ += col.Name + ",";
                    }
                }
                if (this._SQL_.EndsWith(","))
                {
                    this._SQL_ = this._SQL_.Substring(0, this._SQL_.Length - 1);
                }
                this._SQL_ += " ) ";
                this._SQL_ += " VALUES (";
                foreach (LK.Attribute.ColumnName col in rc.getAllColumn())
                {

                    try
                    {
                        object value = rc.getPropValue(rc, col.Name);
                        if (value != null)
                        {
                            this._SQL_ += ":######,";
                            if (value == null)
                            {
                                _SelfPARAMETER_.Add(System.DBNull.Value);
                            }
                            else if (value.ToString() == "")
                            {
                                _SelfPARAMETER_.Add(System.DBNull.Value);
                            }
                            else
                            {
                                _SelfPARAMETER_.Add(value);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    no++;
                }
                if (this._SQL_.EndsWith(","))
                {
                    this._SQL_ = this._SQL_.Substring(0, this._SQL_.Length - 1);
                }
                this._SQL_ += " ) ";
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        /// <summary>
        /// 調整要輸出的SQL語句，若有一個parameter，將有一個:######的標記
        /// 符號，此功能在調整:######變成一個正常的Parameter Name.
        /// </summary>
        /// <param name="_sql"></param>
        /// <returns></returns>
        protected string adjuestSql(string _sql) {
            try
            {
                string sql = _sql;
                string[] arrSql = null;
                while (sql.IndexOf(":######") >= 0)
                {
                    sql = sql.Replace("######", "");
                    arrSql = sql.Split(':');
                }
                sql = "";
                Int16 no = 1;

                if (arrSql == null)
                    return _sql;

                foreach (string tmp in arrSql)
                {
                    if (tmp.Trim().Length == 0)
                        break;
                    if (no <= _SelfPARAMETER_.Count)
                    {

                        if (this.ToString() == "LK.DB.SQLCreater.SQLCreaterMySQL")
                        {
                            sql += tmp + this.__getParameterFlag()/*:*/ + "P" + no.ToString();
                            genParameter("P" + no.ToString(), _SelfPARAMETER_[no - 1]);
                        }
                        else
                        {
                            sql += tmp + this.__getParameterFlag()/*:*/ + "P" + no.ToString();
                            genParameter("P" + no.ToString(), _SelfPARAMETER_[no - 1]);
                        }
                        
                    }
                    else
                    {
                        sql += tmp;
                    }
                    no++;
                }
                return sql;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        /*取得不同類型的IDataParameter物件*/
        /// <summary>
        /// 取得不同類型的IDataParameter
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public virtual void genParameter(string name, object value) {
            try
            {
                throw new System.NotImplementedException("無有實作genParameter方法");
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        public virtual System.Data.IDataParameter justGenParameter(string name, object value,System.Data.ParameterDirection pParameterDirection)
        {
            try
            {
                throw new System.NotImplementedException("無有實作genParameter方法");
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        

        public virtual IDbDataParameter createDbParameter(System.Data.IDbDataParameter param,ParameterDirection pdirection)
        {
            try
            {
                throw new System.NotImplementedException("無有實作genParameter方法");
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        /*加上一個DataParameter到"使用者"的DataParameter中*/
        /// <summary>
        /// 加上一個DataParameter到"使用者"的DataParameter中
        /// </summary>
        /// <param name="param"></param>
        public void addParameter(System.Data.IDataParameter param) {
            try
            {
                this._PARAMETER_.Add(param);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        public List<System.Data.IDataParameter> getAllParameter()
        {
            try
            {
                return this._PARAMETER_;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        /// <summary>
        /// 移除物件上的所有的Parameter的物件
        /// </summary>
        public void removeSelfParameter()
        {
            try
            {
                this._SelfPARAMETER_.Clear();
                this._PARAMETER_.Clear();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        /*加上一個DataParameter到"使用者"的DataParameter中*/
        /// <summary>
        /// 取得"使用者"的DataParameter
        /// </summary>
        /// <returns></returns>
        public List<System.Data.IDataParameter> PARAMETER() {
            try
            {
                return _PARAMETER_;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        /*依SQLCondition組合WHERE 的SQL語句，此處僅組合WHERE的部份*/
        /// <summary>
        /// 依SQLCondition組合WHERE 的SQL語句，此處僅組合WHERE的部份
        /// </summary>
        /// <param name="condition">SQLCondition物件</param>
        /// <returns></returns>
        public ASQLCreater Where(SQLCondition condition) {
            try
            {
                if (isComplete == SQLComplete.UNComplete)
                {

                }
                else
                {
                    _SQL_ = "";
                }
                TableName = condition.TABLE();
                
                if (condition.PARAMETER().Count() > 0 && _SQL_.Trim().StartsWith("WHERE") == false)
                {
                    _SQL_ += " WHERE ";
                }else if (condition.SQL().Trim().Length>0)
                {
                    _SQL_ += " WHERE ";
                }
                this._SQL_ += condition.SQL();

                if (isComplete != SQLComplete.UNComplete && this._SelfPARAMETER_.Count > 0) {
                    this._SelfPARAMETER_.Clear();
                }

                this._SelfPARAMETER_.AddRange(condition.PARAMETER());
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        /// <summary>
        /// 取得Order的SQL語句
        /// </summary>
        /// <returns></returns>
        public string getOrder() {
            return this.strOrder;
        }
        /// <summary>
        /// 設定Order的SQL語句
        /// </summary>
        /// <param name="pStrOrder"></param>
        /// <returns></returns>
        public ASQLCreater Order(string pStrOrder) {
            this.strOrder = pStrOrder;
            return this;
        }

        public ASQLCreater GroupBy(string pStrGroupby)
        {
            this.strGroupBy = pStrGroupby;
            return this;
        }
        /// <summary>
        /// 依傳入的欄位名稱，產生出 由小到大 的 Order的SQL語句
        /// </summary>
        /// <param name="pStrOrder"></param>
        /// <returns></returns>
        public ASQLCreater OrderASC(string pStrOrder)
        {
            try
            {
                this.strOrder = pStrOrder + " ASC, ";
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        /// <summary>
        /// 依傳入的欄位名稱，產生出 由大到小 的 Order的SQL語句
        /// </summary>
        /// <param name="pStrOrder"></param>
        /// <returns></returns>
        public ASQLCreater OrderDESC(string pStrOrder)
        {
            try
            {
                this.strOrder = pStrOrder + " DESC, ";
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        /// <summary>
        /// 設定限制筆數
        /// </summary>
        /// <param name="pStartCount">始第幾筆開始取資料</param>
        /// <param name="pFetchCount">一共要取得多少筆資料</param>
        /// <returns></returns>
        //public ASQLCreater Limit(decimal? pStartCount, decimal? pFetchCount)
        //{
        //    try
        //    {
        //        startCount = pStartCount;
        //        fetchCount = pFetchCount;
        //        return this;
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error(ex);
        //        throw ex;
        //    }
        //} 
        public ASQLCreater Limit(OrderLimit limit)
        {
            try
            {
                startCount = limit.Start;
                fetchCount = limit.Limit;
                setSplitOrderLimit(limit);
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        } 
        /// <summary>
        /// 設定 指定的 筆數限制的SQL 語法
        /// </summary>
        /// <param name="SQL"></param>
        /// <returns></returns>
        public virtual string LimitSQL(string SQL){
            try
            {
                throw new NotImplementedException("未實作LimitSQL");
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        /// <summary>
        /// Free Style的方式來取得Order的SQL語句
        /// 此方法會自動產出 ORDER BY 字眼。ex ColumnA ASC,ColumnB DESC
        /// </summary>
        /// <param name="SQL">Order語句</param>
        /// <returns></returns>
        public virtual string OrderSQL(string SQL)
        {
            try
            {
                if (this.strOrder == null)
                {
                    return _SQL_;
                }

                if (strOrder.Trim().EndsWith(","))
                {
                    strOrder = strOrder.Trim().Substring(0, strOrder.Trim().Length - 1);
                }

                _SQL_ += " ORDER BY " + this.strOrder;
                return _SQL_;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        public virtual string GroupBySQL(string SQL)
        {
            try
            {
                if (this.strGroupBy == null)
                {
                    return _SQL_;
                }

                if (strGroupBy.Trim().EndsWith(","))
                {
                    strGroupBy = strGroupBy.Trim().Substring(0, strGroupBy.Trim().Length - 1);
                }

                _SQL_ += " GROUP BY " + this.strGroupBy;
                return _SQL_;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        /// <summary>
        /// 【注意】此功能為系統呼叫使用，主要是產生出 
        /// 執行一個 帶有資料回傳 Procedure的語法，每一個資料庫產出的語法不同，
        /// 所以每一個繼承此類的物件要自行實作他。
        /// </summary>
        /// <param name="procedureName"></param>
        /// <returns></returns>
        public virtual string __ExecuteProcedureAndReturnSQL(string procedureName) {
            try
            {
                throw new NotImplementedException("沒有實作__ExecuteProcedureAndReturnSQL方法。");
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        /// <summary>
        /// 【注意】此功能為系統呼叫使用，主要功能在取得指定Procedure，所必要的參數(DataParameter)
        /// </summary>
        /// <param name="dbc">DataBaseConfig物件</param>
        /// <param name="procedureName"></param>
        /// <returns></returns>
        public virtual System.Data.IDataParameter[] DeriveParameter(ADataBaseConnection dbc, string procedureName)
        {
            try
            {
                throw new NotImplementedException("沒有實作DeriveParameter方法。");
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        public virtual string __getParameterFlag() {
            try
            {
                throw new System.NotImplementedException("沒有實作__getParameterFlag");
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        /// <summary>
        /// 【注意】此功能為系統呼叫使用，主要在設定你傳入的Command的Parameter資訊
        /// 每一個資料庫產出的語法不同，所以每一個繼承此類的物件要自行實作他。
        /// </summary>
        /// <param name="dbc"></param>
        /// <param name="procedureName"></param>
        /// <param name="cmd"></param>
        /// <param name="yourParams"></param>
        public virtual void __setExecuteProcedureAndReturnParameter(ADataBaseConnection dbc, string procedureName,System.Data.IDbCommand cmd, System.Data.IDataParameter[] yourParams) {
            try
            {
                throw new System.NotImplementedException("沒有實作__setExecuteProcedureAndReturnParameter");
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual string UpdateSQL()
        {
            try
            {

                //if (Select_Column.Length == 0)
                //{
                //    _SQL_ = " UPDATE FROM " + TableName + "   " + _SQL_;
                //}
                //else
                //{
                    _SQL_ = " UPDATE " + TableName + _UPDATESQL_ ;
                //}

                //_SQL_ = this.GroupBySQL(_SQL_);
                //_SQL_ = this.OrderSQL(_SQL_);
                //_SQL_ = this.LimitSQL(_SQL_);
                return adjuestSql(_SQL_);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        public virtual string DeleteSQL()
        {
            try
            {

                //if (Select_Column.Length == 0)
                //{
                _SQL_ = " DELETE " + TableName + " " + _DELETESQL_;
                //}
                //else
                //{
                //    _SQL_ = " DELETE " + Select_Column + " FROM " + TableName + "   " + _SQL_;
                //}

                //_SQL_ = this.GroupBySQL(_SQL_);
                //_SQL_ = this.OrderSQL(_SQL_);
                //_SQL_ = this.LimitSQL(_SQL_);
                return adjuestSql(_SQL_);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
       

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual string InsertSQL()
        {
            try
            {

                if (Select_Column.Length == 0)
                {
                    _SQL_ = " SELECT * FROM " + TableName + "   " + _SQL_;
                }
                else
                {
                    _SQL_ = " SELECT " + Select_Column + " FROM " + TableName + "   " + _SQL_;
                }

                _SQL_ = this.GroupBySQL(_SQL_);
                _SQL_ = this.OrderSQL(_SQL_);
                _SQL_ = this.LimitSQL(_SQL_);
                return adjuestSql(_SQL_);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }


        public ASQLCreater SetUpdate(SQLUpdate sqlupdate)
        {
            try
            {
                if (isComplete == SQLComplete.UNComplete)
                {

                }
                else
                {
                    _UPDATESQL_ = "";
                }
                TableName = sqlupdate.TABLE();

                if (sqlupdate.PARAMETER().Count() > 0 && _UPDATESQL_.Trim().StartsWith("SET") == false)
                {
                    _UPDATESQL_ += " SET ";
                }
                this._UPDATESQL_ += sqlupdate.SQL();

                if (isComplete != SQLComplete.UNComplete && this._SelfPARAMETER_.Count > 0)
                {
                    this._SelfPARAMETER_.Clear();
                }

                this._SelfPARAMETER_.AddRange(sqlupdate.PARAMETER());
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public ASQLCreater SetDelete(SQLDelete sqldelete)
        {
            try
            {
                if (isComplete == SQLComplete.UNComplete)
                {

                }
                else
                {
                    _DELETESQL_ = "";
                }
                TableName = sqldelete.TABLE();

                //if (sqldelete.PARAMETER().Count() > 0 && _UPDATESQL_.Trim().StartsWith("SET") == false)
                //{
                //    _UPDATESQL_ += " SET ";
                //}
                this._DELETESQL_ += sqldelete.SQL();

                if (isComplete != SQLComplete.UNComplete && this._SelfPARAMETER_.Count > 0)
                {
                    this._SelfPARAMETER_.Clear();
                }

                this._SelfPARAMETER_.AddRange(sqldelete.PARAMETER());
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }


        #region Dispose
        void IDisposable.Dispose()
        {
            try
            {
                this.Dispose(true);
                System.GC.SuppressFinalize(this);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        private void Dispose(bool disposing)
        {
            try
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
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        #endregion
    }
}
