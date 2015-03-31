using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace LK.Attribute
{
    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class LkRecord : System.Attribute
    {
        public LkRecord()
        {
        }
    }
}
