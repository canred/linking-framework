using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LK.Config.DataBase;
using log4net;
using System.Reflection;
namespace LK.DB
{
    public class DataBaseConnection:LK.DB.ADataBaseConnection
    {
        public new static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public DataBaseConnection(IDataBaseConfigInfo batabaseConfigInfo)
            : base(batabaseConfigInfo)
        { 
        
        }
    }
}
