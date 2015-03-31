using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace LK.Attribute
{
    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class LkDataBase : System.Attribute
    {
        private string database = "";
        public LkDataBase(string name)
        {
            this.database = name;          
        }
        public string getDataBase() {
            return this.database;
        }
    }
}
