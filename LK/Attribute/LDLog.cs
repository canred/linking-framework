using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
namespace LK.Attribute
{
    [System.AttributeUsage(System.AttributeTargets.Method)]
    public class LkLog : System.Attribute
    {
        public LkLog()
        {
            System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace();
            System.Diagnostics.StackFrame sf = st.GetFrame(0);
            ParameterInfo[] pis = sf.GetMethod().GetParameters();
            //取得命名空間名稱
            string sDllName = MethodInfo.GetCurrentMethod().Module.Name;
            //取得類別名稱
            string sClassName = MethodInfo.GetCurrentMethod().ReflectedType.Name;
            //取得方法名稱
            string sMethodName = MethodInfo.GetCurrentMethod().Name;
            var callingMethod = new System.Diagnostics.StackTrace(1, false).GetFrame(0).GetMethod();     
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
        }
    }
}