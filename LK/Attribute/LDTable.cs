using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace LK.Attribute
{
    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class TableView : System.Attribute
    {
        private string tableName = "";
        private bool isTable = true;
        public TableView(string name,bool isTable)
        {
            this.tableName = name;
            this.isTable = isTable;
        }
        public string getName() {
            return tableName;
        }
        public bool getIsTable() {
            return isTable;
        }
    }
}
