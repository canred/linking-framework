using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
namespace ExtDirect.Direct
{
    public static class DirectProxyGenerator
    {
        public static string generateDirectApi(string dllName,string pClassName,bool isTouch)
        {
            var resStr = new StringBuilder();
            var myDomain = AppDomain.CurrentDomain;
            var assembliesLoaded = myDomain.GetAssemblies();
            resStr.Append("\"actions\":");
         
            bool flag = false;            
            foreach (var allAssembly in assembliesLoaded)           
            {
                /*只解析LKWebTemplate.DLL*/
                if (allAssembly.ManifestModule.Name.ToUpper() != (dllName + ".DLL").ToUpper())
                {
                    continue;
                }
                foreach (var theType in allAssembly.GetTypes())
                {
                    object[] allCustomAttribute = theType.GetCustomAttributes(false);
                    foreach (var customAttrItem in allCustomAttribute)
                    {
                        Type directType = customAttrItem.GetType();
                        if (typeof(DirectServiceAttribute) == directType)
                        {
                            string className = theType.Name;
                            if (pClassName.ToUpper() != className.ToUpper()) {
                                continue;
                            }

                            if (className.ToLower().IndexOf("sitemap")>0) {
                                //var debug = "";
                            }
                            if (isTouch) {
                                resStr.Append("{\"" + className + "\":[");                            
                            }
                            else {
                                resStr.Append("{\"" + LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().DirectApplicationName + "." + className + "\":[");                            
                            }
                            
                            MethodInfo[] methodInfo = theType.GetMethods();

                            var methodList = new List<string>();

                            foreach (var m in methodInfo)
                            {
                                object[] mAtt = m.GetCustomAttributes(false);
                                foreach (var xyz in mAtt)
                                {
                                    Type methodType = xyz.GetType();

                                    if (typeof(DirectMethodAttribute) == methodType)
                                    {
                                        var methodDesc = (DirectMethodAttribute)xyz;
                                        string methodName = m.Name;
                                        DirectAction gMethod = methodDesc.Action;
                                        string tempMey = "{\"name\":";
                                        tempMey += "\"" + methodName + "\",";
                                        if (DirectAction.Null == gMethod)
                                        {
                                            ParameterInfo[] p = m.GetParameters();
                                            int paramCount = p.Count(item => item.ToString().Split(' ')[0] != "ExtDirect.Direct.Request");
                                            tempMey += "\"len\":" + paramCount + "}";
                                        }
                                       
                                        else if (DirectAction.FormSubmission == gMethod)
                                        {
                                            var p = m.GetParameters();
                                            int paramCount = p.Count(item => item.ToString().Split(' ')[0] != "ExtDirect.Direct.Request");
                                            tempMey += "\"formHandler\": true,\"len\": " + paramCount + "}";
                                        }
                                        else if (DirectAction.Load == gMethod)
                                        {
                                            ParameterInfo[] p = m.GetParameters();
                                            int paramCount = p.Count(item => item.ToString().Split(' ')[0] != "ExtDirect.Direct.Request");
                                            tempMey += "\"len\":" + paramCount + "}";                                            
                                        }
                                        //else if (DirectAction.Update == gMethod)
                                        //{
                                        //    tempMey += "\"len\": 1}";
                                        //}
                                        else
                                        {
                                            ParameterInfo[] p = m.GetParameters();
                                            int paramCount = p.Count(item => item.ToString().Split(' ')[0] != "ExtDirect.Direct.Request");
                                            tempMey += "\"len\":" + paramCount + "}";
                                        }
                                        resStr.Append(tempMey + ",");
                                        methodList.Add(tempMey);
                                    }

                                }
                            }
                            resStr.Remove(resStr.Length - 1, 1);
                            resStr.Append("]},");
                            flag = true;
                        }
                    }

                }
                if (flag)
                {
                    resStr.Remove(resStr.Length - 1, 1);
                    flag = false;
                }
            }
            
            return resStr.ToString();
            
            
        }

        public static List<string> generateDirectApiNameList(string pClassName,string prexString)
        {
            List<string> ret = new List<string>();
            var myDomain = AppDomain.CurrentDomain;
            var assembliesLoaded = myDomain.GetAssemblies();
            foreach (var allAssembly in assembliesLoaded)
            {
                /*只解析LKWebTemplate.DLL*/
                if (allAssembly.ManifestModule.Name.ToUpper() != "LKWebTemplate.DLL".ToUpper())
                {
                    continue;
                }
                foreach (var theType in allAssembly.GetTypes())
                {
                    object[] allCustomAttribute = theType.GetCustomAttributes(false);
                    foreach (var customAttrItem in allCustomAttribute)
                    {
                        Type directType = customAttrItem.GetType();
                        if (typeof(DirectServiceAttribute) == directType)
                        {
                            string className = theType.Name;
                            if (pClassName.ToUpper() != className.ToUpper())
                            {
                                continue;
                            }

                            //LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().DirectApplicationName + "." + className;
                            MethodInfo[] methodInfo = theType.GetMethods();
                            var methodList = new List<string>();

                            foreach (var m in methodInfo)
                            {
                                object[] mAtt = m.GetCustomAttributes(false);
                                foreach (var xyz in mAtt)
                                {
                                    Type methodType = xyz.GetType();

                                    if (typeof(DirectMethodAttribute) == methodType)
                                    {
                                        var methodDesc = (DirectMethodAttribute)xyz;
                                        string methodName = m.Name;
                                        ret.Add(prexString + "." + className + "." + methodName);
                                    }
                                }
                            }

                        }
                    }
                }
            }
            return ret;
        }
        public static System.Data.DataTable generateDirectApiServiceClass(string pClassName)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("ServiceClass");
            
            var myDomain = AppDomain.CurrentDomain;
            var assembliesLoaded = myDomain.GetAssemblies();
            

            foreach (var allAssembly in assembliesLoaded)
            {
                /*只解析LKWebTemplate.DLL*/
                if (allAssembly.ManifestModule.Name.ToUpper() != "LKWebTemplate.DLL".ToUpper())
                {
                    continue;
                }
                foreach (var theType in allAssembly.GetTypes())
                {
                    object[] allCustomAttribute = theType.GetCustomAttributes(false);
                    foreach (var customAttrItem in allCustomAttribute)
                    {
                        Type directType = customAttrItem.GetType();
                        if (typeof(DirectServiceAttribute) == directType)
                        {
                            string className = theType.Name;
                            if (pClassName.ToUpper() != className.ToUpper())
                            {
                                continue;
                            }
                            var newRow = dt.NewRow();
                            newRow["ServiceClass"] = className;
                            dt.Rows.Add(newRow);
                        
                        }
                    }
                }                
            }

            return dt;


        }
    }
}
