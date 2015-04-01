using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LK.Attribute;
using LK.DB;  
using log4net;  
using System.Reflection;  
using LK.DB.SQLCreater;  
using LKWebTemplate.Model.Basic.Table;
namespace LKWebTemplate.Model.Basic
{
	[ModelName("Basic")]
	[LkDataBase("BASIC")]
	public partial class BasicModel
	{
		public new static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		private LK.Config.DataBase.IDataBaseConfigInfo dbc = null;
		public BasicModel(){}
		/*Templete Model A001*/
		public LKWebTemplate.Model.Basic.Table.ApplicationHead getApplicationHead_By_Uuid(string pUUID){
			try{
				dbc = LK.Config.DataBase.Factory.getInfo();
				LKWebTemplate.Model.Basic.Table.ApplicationHead applicationhead = new LKWebTemplate.Model.Basic.Table.ApplicationHead(dbc);
				applicationhead.Fill_By_PK(pUUID);
				return applicationhead;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}

		/*Templete Model A001*/
		public LKWebTemplate.Model.Basic.Table.Sitemap getSitemap_By_Uuid(string pUUID){
			try{
				dbc = LK.Config.DataBase.Factory.getInfo();
				LKWebTemplate.Model.Basic.Table.Sitemap sitemap = new LKWebTemplate.Model.Basic.Table.Sitemap(dbc);
				sitemap.Fill_By_PK(pUUID);
				return sitemap;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}

		/*Templete Model A001*/
		public LKWebTemplate.Model.Basic.Table.Appmenu getAppmenu_By_Uuid(string pUUID){
			try{
				dbc = LK.Config.DataBase.Factory.getInfo();
				LKWebTemplate.Model.Basic.Table.Appmenu appmenu = new LKWebTemplate.Model.Basic.Table.Appmenu(dbc);
				appmenu.Fill_By_PK(pUUID);
				return appmenu;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}

		/*Templete Model A001*/
		public LKWebTemplate.Model.Basic.Table.Apppage getApppage_By_Uuid(string pUUID){
			try{
				dbc = LK.Config.DataBase.Factory.getInfo();
				LKWebTemplate.Model.Basic.Table.Apppage apppage = new LKWebTemplate.Model.Basic.Table.Apppage(dbc);
				apppage.Fill_By_PK(pUUID);
				return apppage;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}

		/*Templete Model A001*/
		public LKWebTemplate.Model.Basic.Table.Company getCompany_By_Uuid(string pUUID){
			try{
				dbc = LK.Config.DataBase.Factory.getInfo();
				LKWebTemplate.Model.Basic.Table.Company company = new LKWebTemplate.Model.Basic.Table.Company(dbc);
				company.Fill_By_PK(pUUID);
				return company;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}

		/*Templete Model A001*/
		public LKWebTemplate.Model.Basic.Table.Department getDepartment_By_Uuid(string pUUID){
			try{
				dbc = LK.Config.DataBase.Factory.getInfo();
				LKWebTemplate.Model.Basic.Table.Department department = new LKWebTemplate.Model.Basic.Table.Department(dbc);
				department.Fill_By_PK(pUUID);
				return department;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}

		/*Templete Model A001*/
		public LKWebTemplate.Model.Basic.Table.GroupAppmenu getGroupAppmenu_By_Uuid(string pUUID){
			try{
				dbc = LK.Config.DataBase.Factory.getInfo();
				LKWebTemplate.Model.Basic.Table.GroupAppmenu groupappmenu = new LKWebTemplate.Model.Basic.Table.GroupAppmenu(dbc);
				groupappmenu.Fill_By_PK(pUUID);
				return groupappmenu;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}

		/*Templete Model A001*/
		public LKWebTemplate.Model.Basic.Table.GroupAttendant getGroupAttendant_By_Uuid(string pUUID){
			try{
				dbc = LK.Config.DataBase.Factory.getInfo();
				LKWebTemplate.Model.Basic.Table.GroupAttendant groupattendant = new LKWebTemplate.Model.Basic.Table.GroupAttendant(dbc);
				groupattendant.Fill_By_PK(pUUID);
				return groupattendant;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}

		/*Templete Model A001*/
		public LKWebTemplate.Model.Basic.Table.GroupHead getGroupHead_By_Uuid(string pUUID){
			try{
				dbc = LK.Config.DataBase.Factory.getInfo();
				LKWebTemplate.Model.Basic.Table.GroupHead grouphead = new LKWebTemplate.Model.Basic.Table.GroupHead(dbc);
				grouphead.Fill_By_PK(pUUID);
				return grouphead;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}

		/*Templete Model A001*/
		public LKWebTemplate.Model.Basic.Table.Site getSite_By_Uuid(string pUUID){
			try{
				dbc = LK.Config.DataBase.Factory.getInfo();
				LKWebTemplate.Model.Basic.Table.Site site = new LKWebTemplate.Model.Basic.Table.Site(dbc);
				site.Fill_By_PK(pUUID);
				return site;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}

		/*Templete Model A001*/
		public LKWebTemplate.Model.Basic.Table.AttendantV getAttendantV_By_Uuid(string pUUID){
			try{
				dbc = LK.Config.DataBase.Factory.getInfo();
				LKWebTemplate.Model.Basic.Table.AttendantV attendantv = new LKWebTemplate.Model.Basic.Table.AttendantV(dbc);
				attendantv.Fill_By_PK(pUUID);
				return attendantv;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}

		/*Templete Model A001*/
		public LKWebTemplate.Model.Basic.Table.SitemapV getSitemapV_By_Uuid(string pUUID){
			try{
				dbc = LK.Config.DataBase.Factory.getInfo();
				LKWebTemplate.Model.Basic.Table.SitemapV sitemapv = new LKWebTemplate.Model.Basic.Table.SitemapV(dbc);
				sitemapv.Fill_By_PK(pUUID);
				return sitemapv;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}

		/*Templete Model A001*/
		public LKWebTemplate.Model.Basic.Table.GroupHeadV getGroupHeadV_By_Uuid(string pUUID){
			try{
				dbc = LK.Config.DataBase.Factory.getInfo();
				LKWebTemplate.Model.Basic.Table.GroupHeadV groupheadv = new LKWebTemplate.Model.Basic.Table.GroupHeadV(dbc);
				groupheadv.Fill_By_PK(pUUID);
				return groupheadv;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}

		/*Templete Model A001*/
		public LKWebTemplate.Model.Basic.Table.GroupAttendantV getGroupAttendantV_By_Uuid(string pUUID){
			try{
				dbc = LK.Config.DataBase.Factory.getInfo();
				LKWebTemplate.Model.Basic.Table.GroupAttendantV groupattendantv = new LKWebTemplate.Model.Basic.Table.GroupAttendantV(dbc);
				groupattendantv.Fill_By_PK(pUUID);
				return groupattendantv;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}

		/*Templete Model A001*/
		public LKWebTemplate.Model.Basic.Table.AppmenuApppageV getAppmenuApppageV_By_Uuid(string pUUID){
			try{
				dbc = LK.Config.DataBase.Factory.getInfo();
				LKWebTemplate.Model.Basic.Table.AppmenuApppageV appmenuapppagev = new LKWebTemplate.Model.Basic.Table.AppmenuApppageV(dbc);
				appmenuapppagev.Fill_By_PK(pUUID);
				return appmenuapppagev;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}

		/*Templete Model A001*/
		public LKWebTemplate.Model.Basic.Table.GroupAppmenuV getGroupAppmenuV_By_Uuid(string pUUID){
			try{
				dbc = LK.Config.DataBase.Factory.getInfo();
				LKWebTemplate.Model.Basic.Table.GroupAppmenuV groupappmenuv = new LKWebTemplate.Model.Basic.Table.GroupAppmenuV(dbc);
				groupappmenuv.Fill_By_PK(pUUID);
				return groupappmenuv;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}

		/*Templete Model A001*/
		public LKWebTemplate.Model.Basic.Table.ErrorLog getErrorLog_By_Uuid(string pUUID){
			try{
				dbc = LK.Config.DataBase.Factory.getInfo();
				LKWebTemplate.Model.Basic.Table.ErrorLog errorlog = new LKWebTemplate.Model.Basic.Table.ErrorLog(dbc);
				errorlog.Fill_By_PK(pUUID);
				return errorlog;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}

		/*Templete Model A001*/
		public LKWebTemplate.Model.Basic.Table.ErrorLogV getErrorLogV_By_Uuid(string pUUID){
			try{
				dbc = LK.Config.DataBase.Factory.getInfo();
				LKWebTemplate.Model.Basic.Table.ErrorLogV errorlogv = new LKWebTemplate.Model.Basic.Table.ErrorLogV(dbc);
				errorlogv.Fill_By_PK(pUUID);
				return errorlogv;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}

		/*Templete Model A001*/
		public LKWebTemplate.Model.Basic.Table.Proxy getProxy_By_Uuid(string pUUID){
			try{
				dbc = LK.Config.DataBase.Factory.getInfo();
				LKWebTemplate.Model.Basic.Table.Proxy proxy = new LKWebTemplate.Model.Basic.Table.Proxy(dbc);
				proxy.Fill_By_PK(pUUID);
				return proxy;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}

		/*Templete Model A001*/
		public LKWebTemplate.Model.Basic.Table.VAppmenuProxyMap getVAppmenuProxyMap_By_ProxyUuid_And_Uuid_And_AppmenuProxyUuid(string pPROXY_UUID,string pUUID,string pAPPMENU_PROXY_UUID){
			try{
				dbc = LK.Config.DataBase.Factory.getInfo();
				LKWebTemplate.Model.Basic.Table.VAppmenuProxyMap vappmenuproxymap = new LKWebTemplate.Model.Basic.Table.VAppmenuProxyMap(dbc);
				vappmenuproxymap.Fill_By_PK(pPROXY_UUID,pUUID,pAPPMENU_PROXY_UUID);
				return vappmenuproxymap;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}

		/*Templete Model A001*/
		public LKWebTemplate.Model.Basic.Table.AppmenuProxyMap getAppmenuProxyMap_By_Uuid(string pUUID){
			try{
				dbc = LK.Config.DataBase.Factory.getInfo();
				LKWebTemplate.Model.Basic.Table.AppmenuProxyMap appmenuproxymap = new LKWebTemplate.Model.Basic.Table.AppmenuProxyMap(dbc);
				appmenuproxymap.Fill_By_PK(pUUID);
				return appmenuproxymap;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}

		/*Templete Model A001*/
		public LKWebTemplate.Model.Basic.Table.ScheduleTime getScheduleTime_By_Uuid(string pUUID){
			try{
				dbc = LK.Config.DataBase.Factory.getInfo();
				LKWebTemplate.Model.Basic.Table.ScheduleTime scheduletime = new LKWebTemplate.Model.Basic.Table.ScheduleTime(dbc);
				scheduletime.Fill_By_PK(pUUID);
				return scheduletime;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}

		/*Templete Model A001*/
		public LKWebTemplate.Model.Basic.Table.VScheduleTime getVScheduleTime_By_ScheduleUuid_And_ScheduleTimeUuid(string pSCHEDULE_UUID,string pSCHEDULE_TIME_UUID){
			try{
				dbc = LK.Config.DataBase.Factory.getInfo();
				LKWebTemplate.Model.Basic.Table.VScheduleTime vscheduletime = new LKWebTemplate.Model.Basic.Table.VScheduleTime(dbc);
				vscheduletime.Fill_By_PK(pSCHEDULE_UUID,pSCHEDULE_TIME_UUID);
				return vscheduletime;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}

		/*Templete Model A001*/
		public LKWebTemplate.Model.Basic.Table.ActiveConnection getActiveConnection_By_Uuid(string pUUID){
			try{
				dbc = LK.Config.DataBase.Factory.getInfo();
				LKWebTemplate.Model.Basic.Table.ActiveConnection activeconnection = new LKWebTemplate.Model.Basic.Table.ActiveConnection(dbc);
				activeconnection.Fill_By_PK(pUUID);
				return activeconnection;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}

		/*Templete Model A001*/
		public LKWebTemplate.Model.Basic.Table.Schedule getSchedule_By_Uuid(string pUUID){
			try{
				dbc = LK.Config.DataBase.Factory.getInfo();
				LKWebTemplate.Model.Basic.Table.Schedule schedule = new LKWebTemplate.Model.Basic.Table.Schedule(dbc);
				schedule.Fill_By_PK(pUUID);
				return schedule;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}

		/*Templete Model A001*/
		public LKWebTemplate.Model.Basic.Table.AuthorityMenuV getAuthorityMenuV_By_Uuid(string pUUID){
			try{
				dbc = LK.Config.DataBase.Factory.getInfo();
				LKWebTemplate.Model.Basic.Table.AuthorityMenuV authoritymenuv = new LKWebTemplate.Model.Basic.Table.AuthorityMenuV(dbc);
				authoritymenuv.Fill_By_PK(pUUID);
				return authoritymenuv;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}

		/*Templete Model A001*/
		public LKWebTemplate.Model.Basic.Table.Attendant getAttendant_By_Uuid(string pUUID){
			try{
				dbc = LK.Config.DataBase.Factory.getInfo();
				LKWebTemplate.Model.Basic.Table.Attendant attendant = new LKWebTemplate.Model.Basic.Table.Attendant(dbc);
				attendant.Fill_By_PK(pUUID);
				return attendant;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}

	}
}
