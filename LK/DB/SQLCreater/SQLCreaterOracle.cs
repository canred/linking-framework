using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OracleClient;
using log4net;
using System.Reflection;
using System.Data;
namespace LK.DB.SQLCreater
{
    /// <summary>
    /// SQL產生 for Oracle
    /// </summary>
    public class SQLCreaterOracle:LK.DB.SQLCreater.ASQLCreater
    {
        public new static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        
        public override System.Data.IDataParameter justGenParameter(string name, object value, System.Data.ParameterDirection parameterDirect)
        {
            try
            {
                System.Data.IDataParameter para = new System.Data.OracleClient.OracleParameter(name, value);
                para.Direction = parameterDirect;
                return para;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        public override string __getParameterFlag()
        {
            return ":";
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
                System.Data.IDataParameter para = new System.Data.OracleClient.OracleParameter(name, value);
                this.addParameter(para);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        /// <summary>
        /// 產生有關限制筆數的SQL語法
        /// </summary>
        /// <param name="SQL"></param>
        /// <returns></returns>
        public override  string LimitSQL(string SQL)
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
                    var orderLimit = getSplitOrderLimit();

                    if (orderLimit.getOrderColumn().Count == 0)
                    {
                        throw new Exception("Oracle 在執行類Lmiti的行為必須要到Order,請設定你的OrderLimit物件，利用AddOrder方法。");
                    }

                    for (int i = 0; i < orderLimit.getOrderColumn().Count; i++)
                    {
                        orderByString += " " + orderLimit.getOrderColumn()[i] + " " + orderLimit.getOrderMethod()[i] + ",";
                    }

                    if (orderByString.EndsWith(","))
                    {
                        orderByString = orderByString.Substring(0, orderByString.Length - 1);
                    }

                    string newSQL = "SELECT  * FROM (SELECT limitTableA.*,ROWNUM RowNumNo FROM (";
                    newSQL += SQL ;
                    newSQL += " "+orderByString +" ";
                    newSQL += ") limitTableA  ) limitTableB WHERE RowNumNo >= ";
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
        /// <summary>
        /// 利用Connection及Procedure Name，推出這一個Procedure要使用的所有參數
        /// </summary>
        /// <param name="dbc"></param>
        /// <param name="procedureName"></param>
        /// <returns></returns>
        public override System.Data.IDataParameter[] DeriveParameter(ADataBaseConnection dbc, string procedureName)
        {
            try
            {
                var command = dbc.getConnection().CreateCommand();
                command.CommandText = procedureName;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                OracleCommandBuilder.DeriveParameters((OracleCommand)command);
                var discoveredParameters = new System.Data.IDataParameter[command.Parameters.Count];
                command.Parameters.CopyTo(discoveredParameters, 0);
                command.Parameters.Clear();
                command = null;
                return discoveredParameters;
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
                var allParameter = this.DeriveParameter(dbc, procedureName);
                if (allParameter.Length < 1)
                {
                    throw new Exception("反向解析參數錯誤!");
                }
                cmd.Parameters.Add(allParameter[0]);
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

        public override IDbDataParameter createDbParameter(System.Data.IDbDataParameter param, ParameterDirection pdirection)
        {
            try
            {                
                System.Data.OracleClient.OracleParameter t = (OracleParameter)param;
                if (param.Direction == ParameterDirection.Output)
                {
                    t.OracleType = OracleType.Cursor;
                }
                return t;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
    }
}
