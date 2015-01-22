using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.admin.basic
{
    public partial class attendant : LKWebTemplate.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        

        public string showAD()
        {
            string ret = "true";
            //this.getUser().COMPANY_UUID
            LKWebTemplate.Model.Basic.BasicModel mod = new LKWebTemplate.Model.Basic.BasicModel();
            var dtCompany = mod.getCompany_By_Uuid(this.getUser().COMPANY_UUID);
            if (dtCompany.AllRecord().Count > 0)
            {
                var drCompany = dtCompany.AllRecord().First();
                if (drCompany.IS_SYNC_AD_USER.ToUpper().Equals("Y"))
                {
                    ret = "false";
                }
                else {
                    ret = "true";
                    return ret;
                }

                if (drCompany.AD_LDAP.Trim().Length > 0) {
                    ret = "false";
                }
                else
                {
                    ret = "true";
                    return ret;
                }

                if (drCompany.AD_LDAP_USER.Trim().Length > 0)
                {
                    ret = "false";
                }
                else
                {
                    ret = "true";
                    return ret;
                }

                if (drCompany.AD_LDAP_USER_PASSWORD.Trim().Length > 0)
                {
                    ret = "false";
                }
                else
                {
                    ret = "true";
                    return ret;
                }
            }
            else {
                ret = "true";
            }
            return ret;
        }
    }
}
