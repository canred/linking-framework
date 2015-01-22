using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using System.Reflection;
namespace ISTFrameWork.Pool
{
    static class DataBaseConfigPool
    {
        public static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static String XMLString = "";
        public static void setXMLString(string xml){
            XMLString = xml;
        }
    }
}
