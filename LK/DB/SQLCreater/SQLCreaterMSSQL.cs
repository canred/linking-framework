using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using System.Reflection;
using System.Data;
namespace LK.DB.SQLCreater
{
    public class SQLCreaterMSSQL:LK.DB.SQLCreater.ASQLCreater
    {
        public new static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public override System.Data.IDataParameter justGenParameter(string name, object value,System.Data.ParameterDirection parameterDirect)
        {
            try
            {
                System.Data.IDataParameter para = new System.Data.SqlClient.SqlParameter(name, value);
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
                System.Data.IDataParameter para = new System.Data.SqlClient.SqlParameter(name, value);
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
            return "@";
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
                        throw new Exception("MSSQL 在執行類Lmiti的行為必須要到Order,請設定你的OrderLimit物件，利用AddOrder方法。");
                    }

                    for (int i = 0; i < orderLimit.getOrderColumn().Count; i++) {
                        orderByString += " " + orderLimit.getOrderColumn()[i] + " " + orderLimit.getOrderMethod()[i] + ",";
                    }
                    
                    if (orderByString.EndsWith(",")) {
                        orderByString = orderByString.Substring(0, orderByString.Length - 1);
                    }
         
                    var insertIndex = SQL.IndexOf(" FROM ");
                    SQL = SQL.Insert(insertIndex, ",ROW_NUMBER() OVER ( " + orderByString + " ) RowNumNo ");

                    string newSQL = "SELECT  * FROM (";                    
                    newSQL += SQL;
                    newSQL += ") limitTableA WHERE RowNumNo >= ";
                    newSQL += this.getStartCount().ToString();
                    newSQL += " AND RowNumNo <= ";
                    newSQL += (this.getFetchCount() + this.getStartCount() - 1).ToString();
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
                System.Data.SqlClient.SqlParameter t = (System.Data.SqlClient.SqlParameter)param;              
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
    }
}
