using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace LK.Attribute
{
    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class ModelName : System.Attribute
    {
        private string modelName = "";
        public ModelName(string name)
        {
            this.modelName = name;
        }
    }
}
