using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LK;
using LK.Attribute;
using System.Reflection;
using LK.DB;
using LK.UserInfo;
namespace LK.ActionLog
{
    public static class ActionLog
    {
        private static LK.Config.DataBase.IDataBaseConfigInfo dbc = null;
        #region initDB
        private static void initDB()
        {
            if (dbc == null)
            {
                dbc = dbc = LK.Config.DataBase.Factory.getInfo();
            }
        }
        #endregion
        #region  getUserUuid
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
                return result.GetType().GetProperty(asbProperty).GetValue(result, null).ToString();
            }
            catch
            {
                return "ERROR!!";
            }
        }
        #endregion
        #region GetCallStack
        public static string GetCallStack()
        {
            System.Diagnostics.StackTrace callStack = new System.Diagnostics.StackTrace();
            string s = "";
            int index = 0;
            while (true)
            {
                System.Diagnostics.StackFrame frame = callStack.GetFrame(index);
                if (frame == null) break;
                System.Reflection.MethodBase method = frame.GetMethod();
                if (index == 0) s = " --" + s;
                s = method.DeclaringType.Name + "." + method.Name + "()" + s;
                index++;
            }
            return (s);
        }

        #endregion

        #region SetActionLog
        public static void SetActionLog(Object[] paramters, MethodInfo theMothod)
        {           
            string className = theMothod.DeclaringType.FullName;
            string funcName = theMothod.Name;
            ParameterInfo[] pis = theMothod.GetParameters();
            string param = "{";
            foreach (ParameterInfo pi in pis)
            {
                int index = pi.Position;
                MemberInfo mi = pi.Member;
                if (pis.Length - 1 >= index)
                {
                    try
                    {
                        param += string.Format("'{0}':'{1}',", pi.Name, paramters[index]);
                    }
                    catch
                    {
                        param += string.Format("'{0}':'{1}',", pi.Name, "");
                    }
                }
                else
                {
                    param += string.Format("'{0}':'{1}',", pi.Name, "參數發生錯誤");
                }
            }
            if (param.Length > 1)
                param = param.Substring(0, param.Length - 1);
            param += "}";
            log(className, getUserUuid(), funcName, param);
        }
        #endregion
        #region log
        private static void log(string className, string attendant_uuid, string funcName, string param)
        {
            try
            {
                initDB();
                var LdLog = new LK.ActionLog.Table.ActionLog(dbc);
                var dtLog = LdLog.CreateNew();
                dtLog.UUID = LK.Util.UID.Instance.GetUniqueID();
                dtLog.CREATE_DATE = DateTime.Now;
                dtLog.UPDATE_DATE = DateTime.Now;
                dtLog.IS_ACTIVE = "Y";
                dtLog.ATTENDANT_UUID = attendant_uuid;
                dtLog.CLASS_NAME = className;
                dtLog.FUNCTION_NAME = funcName;
                dtLog.PARAMETER = param;
                dtLog.CREATE_USER = attendant_uuid;
                dtLog.UPDATE_USER = attendant_uuid;
                dtLog.gotoTable().Insert(dtLog);
            }
            catch (Exception innerEx)
            {
                MyException.MyException.ErrorNoThrowExceptionForStaticClass(innerEx);
                /*失敗是不影響其他程式*/
            }
        }
        #endregion log
    }
}