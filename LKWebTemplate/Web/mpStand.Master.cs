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
                menu.InnerHtml = html;//"<ul class='sf-menu'><li><a>溫室氣體盤查邊界設定</a><ul><li><a href='http://localhost/GHG/organization/organization_query.aspx'>組織邊界及營運邊界維護</a></li></ul></li><li><a>基本參數管理</a><ul><li><a href='http://localhost/GHG/ghg_type/ghg_type_query.aspx'>溫室氣體涵蓋種類維護</a></li><li><a href='http://localhost/GHG/method/method_query.aspx'>評估報告出處及年份維護</a></li><li><a href='http://localhost/GHG/reason/reason_query.aspx'>係數種類維護</a></li><li><a href='http://localhost/GHG/gwp/gwp_query.aspx'>全球暖化潛勢(GWP)值維護</a></li><li><a href='http://localhost/GHG/region/region_query.aspx'>地區維護</a></li><li><a href='http://localhost/GHG/factor/factor_query.aspx'>排放係數維護</a></li><li><a href='http://localhost/GHG/emission_source_type/emission_source_type_query.aspx'>排放源型式維護</a></li><li><a href='http://localhost/GHG/mfg_process/mfg_process_query.aspx'>製程代碼及名稱維護</a></li><li><a href='http://localhost/GHG/facility/facility_query.aspx'>設備代碼及名稱維護</a></li><li><a href='http://localhost/GHG/material/material_query.aspx'>原料別維護</a></li><li><a href='http://localhost/GHG/industry/industry_category_query.aspx'>行業別維護</a></li><li><a href='http://localhost/GHG/administrative_area/administrative_area.aspx'>縣市鄉鎮維護</a></li></ul></li><li><a>溫室氣體盤查報告書資訊管理</a><ul><li><a href='http://localhost/GHG/intended_user/intended_user_query.aspx'>預期使用者維護</a></li><li><a href='http://localhost/GHG/job/job_query.aspx'>專案工作維護</a></li><li><a href='http://localhost/GHG/title/title_query.aspx'>盤查專案推動小組維護</a></li></ul></li><li><a>排放源管理</a><ul><li><a href='http://localhost/GHG/emission/emission_data_list_query.aspx'>排放源活動數據</a></li></ul></li><li><a>盤查作業</a><ul><li><a href='http://localhost/GHG/project/project_query.aspx'><img style='height:16px;'src='http://localhost/GHG/css/custImages/project.gif'/>專案</a></li><li><a href='http://localhost/GHG/project/project2_query.aspx'><img style='height:16px;'src='http://localhost/GHG/css/custImages/project.gif'/>專案(營運邊界)</a></li><li><a href='http://localhost/GHG/project/overview.aspx'>專案總覽</a></li></ul></li><li><a href='http://localhost/GHG/product/product_query.aspx'>產品維護</a></li><li><a>管理者</a><ul><li><a>基本資料維護</a><ul><li><a href='http://localhost/GHG/admin/basic/company.aspx'>公司維護</a></li><li><a href='http://localhost/GHG/admin/basic/dept.aspx'>部門維護</a></li><li><a href='http://localhost/GHG/admin/basic/attendant.aspx'>人員維護</a></li></ul></li><li><a>系統設定</a><ul><li><a href='http://localhost/GHG/admin/system/system.aspx'>系統</a></li><li><a href='http://localhost/GHG/admin/system/function.aspx'>功能</a></li><li><a href='http://localhost/GHG/admin/system/sitemap.aspx'>Site Map</a></li><li><a href='http://localhost/GHG/admin/system/menu.aspx'>選單</a></li></ul></li></ul></li><ul>";
            }
        }

        
        #endregion setMenu

        #region getMenuHTML
        private string getMenuHTML(IList<AuthorityMenuV_Record> menuList,
           IEnumerable<AuthorityMenuV_Record> menuQuery)
        {
            string ret = string.Empty;
            foreach (AuthorityMenuV_Record r in menuQuery)
            {
                if (r.IS_ACTIVE == "Y")
                {
                    string hasChild = r.HASCHILD;
                    string url = r.URL;
                    string image = r.IMAGE;
                    string parameter = r.FUNC_PARAMETER_CLASS;
                    string name = r.NAME_ZH_TW;
                    string action_mode = r.ACTION_MODE;
                    string _appmenu_uuid = r.APPMENU_UUID;
                    string _uuid = r.UUID;

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
                        if (image.Trim().Length > 0)
                            ret += " href='" + ResolveUrl(url) + "'><img style='height:14px;padding-right:5px;' src='" + ResolveUrl(image) + "' />" + name + "</a>";
                        else
                            ret += " href='" + ResolveUrl(url) + "'>" + name + "</a>";
                    }
                    else
                    {
                        if (image.Trim().Length > 0)
                            ret += "><img style='height:14px;padding-right:5px;' src='" + ResolveUrl(image) + "' />" + name + "</a>";
                        else
                            ret += ">" + name + "</a>";
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
