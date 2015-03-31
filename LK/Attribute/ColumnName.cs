using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LK.Attribute
{
    [System.AttributeUsage(System.AttributeTargets.Field | System.AttributeTargets.Property)]
    public class ColumnName : System.Attribute
    {
        private string columnName = "";
        private bool databasePK = false;
        private Type columnType = null;
        private int? columnLength = null; 
        public ColumnName(string name, bool isPK, Type type, int length) {
            this.columnName = name;
            this.databasePK = isPK;
            this.columnType = type;
            this.columnLength = length;
        }
        public ColumnName(string name, bool isPK, Type type)
        {  
                this.columnName = name;
                this.databasePK = isPK;
                this.columnType = type;          
        }
        public bool IsPK {
            get {
                return this.databasePK;
            }
        }
        public string Name {
            get
            {
                return this.columnName;
            }
        }
        public Type ColumnType {
            get {
                return this.columnType;
            }
        }
    }
}