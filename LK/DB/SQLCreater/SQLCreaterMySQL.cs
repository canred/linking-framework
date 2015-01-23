using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using System.Reflection;
using System.Data;
using MySql.Data;
namespace LK.DB.SQLCreater
{
    public class SQLCreaterMySQL:LK.DB.SQLCreater.ASQLCreater
    {
        public new static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public override System.Data.IDataParameter justGenParameter(string name, object value,  System.Data.ParameterDirection parameterDirect)
        {
            try
            {

                System.Data.IDataParameter para = new MySql.Data.MySqlClient.MySqlParameter(name, value);
                para.Direction = parameterDirect;
                return para;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        /// <summary>
        /// 產生一個Parameter，並直到放入物件中。
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public override void genParameter(string name, object value)
        {
            try
            {

                System.Data.IDataParameter para = new MySql.Data.MySqlClient.MySqlParameter(name, value);
                this.addParameter(para);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

       



      
        

        public override string __getParameterFlag()
        {
            return "?";
        }
        
        /// <summary>
        /// 產生有關限制筆數的SQL語法
        /// </summary>
        /// <param name="SQL"></param>
        /// <returns></returns>
        public override string LimitSQL(string SQL)
        {
            
            try
            {
                if (this.getStartCount() == null || this.getFetchCount() == null)
                {
                    return SQL;
                }
                else
                {               
                    string orderByString = " ORDER BY ";
                    var orderLimit =  getSplitOrderLimit();
                  
                    if (orderLimit.getOrderColumn().Count == 0) { 
                        throw new Exception("MYSQL 在執行類Lmiti的行為必須要到Order,請設定你的OrderLimit物件，利用AddOrder方法。");
                    }
                    bool jumpOrder = false;
                    for (int i = 0; i < orderLimit.getOrderColumn().Count; i++) {
                        if (orderLimit.getOrderColumn()[i] == "")
                            jumpOrder = true;
                        else
                            orderByString += " " + orderLimit.getOrderColumn()[i] + " " + orderLimit.getOrderMethod()[i] + ",";
                    }

                    
                    if (jumpOrder==false && orderByString.EndsWith(",")) {
                        orderByString = orderByString.Substring(0, orderByString.Length - 1);
                    }
         
                   
                    string newSQL = "";                    
                    newSQL += SQL;
                    if (jumpOrder == false)
                    {
                        newSQL += orderByString;
                    }
                    newSQL += " LIMIT " + (this.getStartCount()-1).ToString() + "," + this.getFetchCount().ToString();
              
                    return newSQL;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        public override IDbDataParameter createDbParameter(System.Data.IDbDataParameter param, ParameterDirection pdirection)
        {
            try
            {
                MySql.Data.MySqlClient.MySqlParameter t = (MySql.Data.MySqlClient.MySqlParameter)param;                
                return t;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        public override string __ExecuteProcedureAndReturnSQL(string procedureName)
        {
            try
            {
                return procedureName;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        /// <summary>
        /// 【注意】底層程式使用；請不要直接使用
        /// </summary>
        /// <param name="dbc"></param>
        /// <param name="procedureName"></param>
        /// <param name="cmd"></param>
        /// <param name="yourParams"></param>
        public override void __setExecuteProcedureAndReturnParameter(ADataBaseConnection dbc, string procedureName, System.Data.IDbCommand cmd, System.Data.IDataParameter[] yourParams)
        {
            try
            {           
                foreach (var item in yourParams)
                {
                    cmd.Parameters.Add(item);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        public override ASQLCreater Insert(LK.DB.RecordBase rc)
        {
            try
            {
             
                this._SQL_ = "SET @@sql_mode = '';" +"INSERT INTO " + rc.getTableName() + " ";
                Int16 no = 1;
                this._SQL_ += " ( ";
                foreach (LK.Attribute.ColumnName col in rc.getAllColumn())
                {
                    this._SQL_ += col.Name + ",";
                }
                if (this._SQL_.EndsWith(","))
                {
                    this._SQL_ = this._SQL_.Substring(0, this._SQL_.Length - 1);
                }
                this._SQL_ += " ) ";
                this._SQL_ += " VALUES (";
                foreach (LK.Attribute.ColumnName col in rc.getAllColumn())
                {
                    this._SQL_ += ":######,";
                    try
                    {
                        object value = rc.getPropValue(rc, col.Name);
                        _SelfPARAMETER_.Add(value);
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

        public override ASQLCreater Delete(LK.DB.RecordBase rc)
        {
            try
            {
                this._SQL_ = "SET @@sql_mode = '';" + "DELETE FROM " + rc.getTableName();
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
		
		public override string DeleteSQL()
        {
            try
            {
                this._SQL_ = " DELETE FROM " + this.getTableName() + " " + _DELETESQL_;
                return this.adjuestSql(this._SQL_);
                
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
    }
}
