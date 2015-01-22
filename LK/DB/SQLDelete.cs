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
    public class SQLDelete : IDisposable
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
                    {
                        //if (_dbconnection_ != null)
                        //{
                        //    try
                        //    {
                        //        _dataBaseConfigInfo = null;
                        //        _dbconnection_.Close();
                        //        _dbconnection_.Dispose();
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        throw ex;
                        //    }
                        //}
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
        /// <summary>
        /// SQLCondition的建構子之一
        /// </summary>
        /// <param name="modelObj"></param>
        public SQLDelete(object modelObj)
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

                //ret.GetDBType(
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
        


        public virtual SQLDelete Where(SQLCondition condition)
        {
            try
            {

                if (CSQL.EndsWith(",")) {
                    CSQL = CSQL.Substring(0, CSQL.Length - 1);
                }

                if (condition.PARAMETER().Count() > 0 && CSQL.Trim().StartsWith("WHERE") == false)
                    {
                        CSQL += " WHERE ";
                    }
                    this.CSQL += condition.SQL();

                    //_parameter.Add(condition.PARAMETER());
                    _parameter.AddRange(condition.PARAMETER());
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
        public virtual SQLDelete CheckSQL()
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
