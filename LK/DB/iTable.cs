using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace LK.DB
{
    public abstract class iTable
    {
        public enum Status
        {
            Defined, Table
        }
        public enum CurrentDataStats { 
            Defined,
            FromTable,
            New,
            Update
        }
        public CurrentDataStats DataStatus = CurrentDataStats.Defined;
    }
}
