using System;
using System.Collections.Generic;

namespace ExtDirect.Direct
{
    public class Request
    {
        public string action
        {
            get;
            set;
        }
        public string method
        {
            get;
            set;
        }
        public List<object> data
        {
            get;
            set;
        }
        public int tid
        {
            get;
            set;
        }
        public int page
        {
            get;
            set;
        }
        public Int32 start
        {
            get;
            set;
        }
        public Int32 limit
        {
            get;
            set;
        }
        public string dir
        {
            get;
            set;
        }

        public System.Web.HttpRequest HttpRequest
        {
            get;
            set;
        }
        
    }
}
