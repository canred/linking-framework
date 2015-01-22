using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using System.Reflection;
namespace LK.DBAction
{
    public enum DataBaseType { 
        Oracle,MsSQL
    }
    public static class DBActionFactory
    {
        public static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static LK.DB.IDBAction Factory() {
            try
            {
                OracleDBAction action = new OracleDBAction();
                return action;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
    }
}
