using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
namespace LK.UserInfo
{
    public class User
    {
        public static string Uuid()
        {
            try
            {
                UserInfoConfigInfo config = new UserInfoConfigInfo();
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
                return null;
            }
        }
        public static string CompanyUuid()
        {
            try
            {
                UserInfoConfigInfo config = new UserInfoConfigInfo();
                string asbLoadName = config.GetTag("User_Assembly_Load", false);
                string asbLoadType = config.GetTag("User_Assembly_Type", false);
                string asbMethod = config.GetTag("User_Assembly_Method", false);
                string asbProperty = config.GetTag("User_Assembly_Property_CompanyUuid", false);
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
                return null;
            }
        }

        public static string CompanyId()
        {
            try
            {
                UserInfoConfigInfo config = new UserInfoConfigInfo();
                string asbLoadName = config.GetTag("User_Assembly_Load", false);
                string asbLoadType = config.GetTag("User_Assembly_Type", false);
                string asbMethod = config.GetTag("User_Assembly_Method", false);
                string asbProperty = config.GetTag("User_Assembly_Property_COMPANY_ID", false);
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
                return null;
            }
        }
        public static string email()
        {
            try
            {
                UserInfoConfigInfo config = new UserInfoConfigInfo();
                string asbLoadName = config.GetTag("User_Assembly_Load", false);
                string asbLoadType = config.GetTag("User_Assembly_Type", false);
                string asbMethod = config.GetTag("User_Assembly_Method", false);
                string asbProperty = config.GetTag("User_Assembly_Property_EMAIL", false);
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
                return null;
            }
        }
        public static string isAdmin()
        {
            try
            {
                UserInfoConfigInfo config = new UserInfoConfigInfo();
                string asbLoadName = config.GetTag("User_Assembly_Load", false);
                string asbLoadType = config.GetTag("User_Assembly_Type", false);
                string asbMethod = config.GetTag("User_Assembly_Method", false);
                string asbProperty = config.GetTag("User_Assembly_Property_IS_ADMIN", false);
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
                return null;
            }
        }

         public static string account()
        {
            try
            {
                UserInfoConfigInfo config = new UserInfoConfigInfo();
                string asbLoadName = config.GetTag("User_Assembly_Load", false);
                string asbLoadType = config.GetTag("User_Assembly_Type", false);
                string asbMethod = config.GetTag("User_Assembly_Method", false);
                string asbProperty = config.GetTag("User_Assembly_Property_ACCOUNT", false);
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
                return null;
            }
        }

    }
}
