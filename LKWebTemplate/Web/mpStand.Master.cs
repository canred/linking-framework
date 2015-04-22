using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LKWebTemplate.Model.Basic;
using LKWebTemplate.Model.Basic.Table;
using LKWebTemplate.Model.Basic.Table.Record;

namespace LKWebTemplate
{
    public partial class mpStand : System.Web.UI.MasterPage
    {
        public LKWebTemplate.Util.Session.Store ss = new LKWebTemplate.Util.Session.Store();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                BasicModel model = new BasicModel();
                LKWebTemplate.Model.Basic.Table.Record.AttendantV_Record user = getUser();
                if (user != null)
                {
                    string attendant_uuid = user.UUID;
                    //var menuList = model.getAuthorityMenuVByAttendantUuid(attendant_uuid, "13111517364100129");
                    //setMenu(attendant_uuid, menuList);
                    var menuList = model.getAuthorityMenuVByAttendantUuid(attendant_uuid, LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().InitAppUuid);
                    menuList=menuList.OrderBy(c => c.ORD).ToList();
                    ss.setObject("USERMENU", menuList);
                    setMenu(attendant_uuid, menuList);
                }
            }
        }

        /// <summary>
        /// 取得登入者人員資料
        /// </summary>
        /// <returns></returns>
        public LKWebTemplate.Model.Basic.Table.Record.AttendantV_Record getUser()
        {
            if (ss.ExistKey("USER"))
            {
                return (LKWebTemplate.Model.Basic.Table.Record.AttendantV_Record)ss.getObject("USER");
            }
            else
            {
                return null;
            }
        }

        #region setMenu
        private void setMenu(string attendant_uuid,IList<AuthorityMenuV_Record> menuList)
        {
            IEnumerable<AuthorityMenuV_Record> menuQuery =
           from _menu in menuList
           where _menu.APPMENU_UUID ==null || _menu.APPMENU_UUID ==string.Empty 
           select _menu;

            if (menuQuery.Count() > 0)
            {
                string root_appmenu_uuid = menuQuery.ToList()[0].UUID;
                string html = string.Empty;

                IEnumerable<AuthorityMenuV_Record> new_menuQuery =
                  from new_menu in menuList
                  where new_menu.APPMENU_UUID == root_appmenu_uuid
                  select new_menu;

                string items = getMenuHTML(menuList, new_menuQuery);
                html += "<ul class='sf-menu'>" + items + "</ul>";
                menu.InnerHtml = html;
            }
        }

        
        #endregion setMenu

        #region getMenuHTML
        private string getMenuHTML(IList<AuthorityMenuV_Record> menuList,
           IEnumerable<AuthorityMenuV_Record> menuQuery)
        {
            string ret = string.Empty;
            string defaultLanguage = "US";
            foreach (AuthorityMenuV_Record r in menuQuery)
            {                
                if (r.IS_ACTIVE == "Y")
                {
                    string hasChild = r.HASCHILD;
                    string url = r.URL;
                    string image = r.IMAGE;
                    string parameter = r.FUNC_PARAMETER_CLASS;
                    string name = r.NAME_EN_US;
                    if (getUser() != null) {
                        var lan = getUser().CODE_PAGE;
                        if (lan.ToUpper().Equals("CHS")) {
                            name = r.NAME_ZH_CN;
                        }
                        else if (lan.ToUpper().Equals("CHT")) {
                            name = r.NAME_ZH_TW;
                        }
                        else if (lan.ToUpper().Equals("JPN"))
                        {
                            name = r.NAME_JPN;
                        }
                        else {
                            name = r.NAME_EN_US;
                        }                        
                    };
                    string action_mode = r.ACTION_MODE;
                    string _appmenu_uuid = r.APPMENU_UUID;
                    string _uuid = r.UUID;
                    string _runjsfunction = r.RUNJSFUNCTION;

                    //url加上參數
                    if (url.Trim().Length > 0 && parameter.Trim().Length>0)
                        url = url + "?" + parameter;

                    if (url.Trim().Length > 0 && action_mode.Trim().Length > 0)
                    {
                        if (url.IndexOf('?') > 0)
                            url = url + "&ACTION=" + action_mode;
                        else
                            url = url + "?ACTION=" + action_mode;
                    }

                    ret += "<li><a";

                    //是否有URL & 圖片
                    if (url.Trim().Length > 0)
                    {
                        if (_runjsfunction.Trim().Length == 0)
                        {
                            if (image.Trim().Length > 0)
                                ret += " href='" + ResolveUrl(url) + "'><img style='height:14px;padding-right:5px;' src='" + ResolveUrl(image) + "' />" + name + "</a>";
                            else
                                ret += " href='" + ResolveUrl(url) + "'>" + name + "</a>";
                        }
                        else
                        {
                            if (image.Trim().Length > 0)
                                ret += " href='#' onclick='" + _runjsfunction.Replace("'", "\"") + ";void(0);'><img style='height:14px;padding-right:5px;' src='" + ResolveUrl(image) + "' />" + name + "</a>";
                            else
                                ret += " href='#' onclick='" + _runjsfunction.Replace("'", "\"") + ";void(0);'>" + name + "</a>";
                        }
                    }
                    else
                    {
                        if (_runjsfunction.Trim().Length == 0)
                        {
                            if (image.Trim().Length > 0)
                                ret += "><img style='height:14px;padding-right:5px;' src='" + ResolveUrl(image) + "' />" + name + "</a>";
                            else
                                ret += ">" + name + "</a>";
                        }
                        else
                        {
                            if (image.Trim().Length > 0)
                                ret += " href='#' onclick='" + _runjsfunction.Replace("'", "\"") + ";void(0);'><img style='height:14px;padding-right:5px;' src='" + ResolveUrl(image) + "' />" + name + "</a>";
                            else
                                ret += " href='#' onclick='" + _runjsfunction.Replace("'", "\"") + ";void(0);'>" + name + "</a>";
                        }
                    }


                    if (hasChild == "Y")
                    {
                        IEnumerable<AuthorityMenuV_Record> _menuQuery =
                       from _menu in menuList
                       where _menu.APPMENU_UUID == _uuid
                       orderby _menu.ORD
                       select _menu;

                        if (_menuQuery.Count() > 0)
                        {
                            ret += "<ul>";
                            ret += getMenuHTML(menuList, _menuQuery);
                            ret += "</ul></li>";
                        }
                    }
                    else
                    {
                        ret += "</li>";
                    }
                }
            }
            return ret;
        }

      
        #endregion
    }
}
