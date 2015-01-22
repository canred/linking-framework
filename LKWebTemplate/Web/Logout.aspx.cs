using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LKWebTemplate
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LKWebTemplate.Util.Session.Store ss = new LKWebTemplate.Util.Session.Store();            
            ss.ClearCookieInSession();
        }
    }
}
