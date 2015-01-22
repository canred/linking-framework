using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LK.Attribute;
using System.Reflection;
using LK.UserInfo;

namespace LK.MyException
{

    [LkDataBase("ERROR")]
    public static class MyException
    {
        private static LK.Config.DataBase.IDataBaseConfigInfo dbc = null;

        private static void initDB()
        {
            if (dbc == null)
            {
                dbc = dbc = LK.Config.DataBase.Factory.getInfo();
            }
        }
        #region ERROR
        public static void Error(object classObj, System.Exception ex)
        {
            log("500", classObj.GetType().ToString(), getUserUuid(), "ERROR", ex, true);
        }

        public static void ErrorNoThrowException(object classObj, System.Exception ex)
        {
            log("500", classObj.GetType().ToString(), getUserUuid(), "ERROR", ex, false);
        }

        public static void ErrorNoThrowExceptionForStaticClass(System.Exception ex)
        {
            log("500", "--Static Class--", getUserUuid(), "ERROR", ex, false);
        }

        public static void ErrorStaticClass(System.Exception ex)
        {
            log("500", "--Static Class--", getUserUuid(), "ERROR", ex, true);
        }

        public static void Error(string application_name, System.Exception ex)
        {
            log("500", application_name, getUserUuid(), "ERROR", ex, true);
        }


        #endregion
        public static string getUserUuid()
        {
            try
            {
                var config = new UserInfoConfigInfo();
                string asbLoadName = config.GetTag("User_Assembly_Load", false);
                string asbLoadType = config.GetTag("User_Assembly_Type", false);
                string asbMethod = config.GetTag("User_Assembly_Method", false);
                string asbProperty = config.GetTag("User_Assembly_Property_Uuid", false);

                var assembly = Assembly.Load(asbLoadName);
                var assemblyType = assembly.GetType(asbLoadType);
                var instance = Activator.CreateInstance(assemblyType);
                object[] para = new object[] { };
                var methodInfo = assemblyType.GetMethod(asbMethod, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                var result = methodInfo.Invoke(instance, para);
                if(result==null ) 
                    return "";
                else
                    return result.GetType().GetProperty(asbProperty).GetValue(result, null).ToString();
            }
            catch
            {
                return "ERROR!!";
            }
        }
        #region INFO
        public static void Info(object classObj, string message)
        {
            log("000", classObj.GetType().ToString(), getUserUuid(), "INFO", message);
        }
        public static void Info(string application_name, string message)
        {
            log("000", application_name, getUserUuid(), "INFO", message);
        }

        #endregion

        #region DEBUG

        public static void Debug(object classObj, string message)
        {
            log("100", classObj.GetType().ToString(), getUserUuid(), "DEBUG", message);
        }
        public static void Debug(string application_name, string message)
        {
            log("100", application_name, getUserUuid(), "DEBUG", message);
        }

        #endregion

        #region WARNING

        public static void Warning(object classObj, string message)
        {
            log("200", classObj.GetType().ToString(), getUserUuid(), "WARNING", message);
        }
        public static void Warning(string application_name, string message)
        {
            log("200", application_name, getUserUuid(), "WARNING", message);
        }
        #endregion

        private static void log(string error_code, string application_name, string attendant_uuid, string errorType, System.Exception ex, bool throwException)
        {
            try
            {
                initDB();
                var modError = new LK.MyException.Table.ErrorLog(dbc);
                var drError = modError.CreateNew();
                drError.UUID = LK.Util.UID.Instance.GetUniqueID();
                drError.CREATE_DATE = DateTime.Now;
                drError.UPDATE_DATE = DateTime.Now;
                drError.IS_ACTIVE = "Y";
                drError.ERROR_CODE = error_code;
                drError.ERROR_TIME = "1";
                drError.ERROR_MESSAGE = ex.Message;
                drError.ERROR_MESSAGE += "\r\n" + ex.Source;
                drError.ERROR_MESSAGE += "\r\n" + ex.StackTrace;
                drError.APPLICATION_NAME = application_name.Split('.').First();
                drError.ATTENDANT_UUID = attendant_uuid;
                drError.ERROR_TYPE = errorType;
                drError.gotoTable().Insert(drError);
            }
            catch (Exception innerEx)
            {
                if (throwException)
                {
                    throw innerEx;
                }
            }
        }

        private static void log(string error_code, string application_name, string attendant_uuid, string errorType, string message)
        {
            try
            {
                initDB();
                var modError = new LK.MyException.Table.ErrorLog(dbc);
                var drError = modError.CreateNew();
                drError.UUID = LK.Util.UID.Instance.GetUniqueID();
                drError.CREATE_DATE = DateTime.Now;
                drError.UPDATE_DATE = DateTime.Now;
                drError.IS_ACTIVE = "Y";
                drError.ERROR_CODE = error_code;
                drError.ERROR_TIME = "1";
                drError.ERROR_MESSAGE = message;
                drError.APPLICATION_NAME = application_name.Split('.').First();
                drError.ATTENDANT_UUID = attendant_uuid;
                drError.ERROR_TYPE = errorType;
                drError.gotoTable().Insert(drError);
            }
            catch (Exception innerEx)
            {
                throw innerEx;
            }
        }

        private static IList<LK.MyException.Table.Record.ErrorLog_Record> getError(string applicationName, string attendantUuid, string keyWord)
        {
            initDB();
            var dtError = new LK.MyException.Table.ErrorLog(dbc);

            var condition = new LK.DB.SQLCondition(dtError);
            condition.Equal(dtError.APPLICATION_NAME, applicationName);
            if (attendantUuid.Trim().Length > 0)
            {
                condition.And().Equal(dtError.ATTENDANT_UUID, attendantUuid);
            }

            if (keyWord.Trim().Length > 0)
            {
                condition.And().BLike(dtError.ERROR_MESSAGE, keyWord);
            }

            var result = dtError.Where(condition)
            .FetchAll<LK.MyException.Table.Record.ErrorLog_Record>();

            return result;
        }

        private static IList<LK.MyException.Table.Record.ErrorLog_Record> getError(string applicationName)
        {
            return getError(applicationName, "", "");
        }

    }
}
