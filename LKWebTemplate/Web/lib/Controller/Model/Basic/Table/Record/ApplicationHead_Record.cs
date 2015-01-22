using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LK.Attribute;
using LK.DB;  
using LK.DB.SQLCreater;  
using LKWebTemplate.Model.Basic.Table;
namespace LKWebTemplate.Model.Basic.Table.Record
{
	[LkRecord]
	[TableView("APPLICATION_HEAD", true)]
	[LkDataBase("BASIC")]
	[Serializable]
	public class ApplicationHead_Record : RecordBase{
		public ApplicationHead_Record(){}
		/*欄位資訊 Start*/
		string _UUID=null;
		DateTime? _CREATE_DATE=null;
		DateTime? _UPDATE_DATE=null;
		string _IS_ACTIVE=null;
		string _NAME=null;
		string _DESCRIPTION=null;
		string _ID=null;
		string _CREATE_USER=null;
		string _UPDATE_USER=null;
		string _WEB_SITE=null;
		/*欄位資訊 End*/

		[ColumnName("UUID",true,typeof(string))]
		public string UUID
		{
			set
			{
				_UUID=value;
			}
			get
			{
				return _UUID;
			}
		}

		[ColumnName("CREATE_DATE",false,typeof(DateTime?))]
		public DateTime? CREATE_DATE
		{
			set
			{
				_CREATE_DATE=value;
			}
			get
			{
				return _CREATE_DATE;
			}
		}

		[ColumnName("UPDATE_DATE",false,typeof(DateTime?))]
		public DateTime? UPDATE_DATE
		{
			set
			{
				_UPDATE_DATE=value;
			}
			get
			{
				return _UPDATE_DATE;
			}
		}

		[ColumnName("IS_ACTIVE",false,typeof(string))]
		public string IS_ACTIVE
		{
			set
			{
				_IS_ACTIVE=value;
			}
			get
			{
				return _IS_ACTIVE;
			}
		}

		[ColumnName("NAME",false,typeof(string))]
		public string NAME
		{
			set
			{
				_NAME=value;
			}
			get
			{
				return _NAME;
			}
		}

		[ColumnName("DESCRIPTION",false,typeof(string))]
		public string DESCRIPTION
		{
			set
			{
				_DESCRIPTION=value;
			}
			get
			{
				return _DESCRIPTION;
			}
		}

		[ColumnName("ID",false,typeof(string))]
		public string ID
		{
			set
			{
				_ID=value;
			}
			get
			{
				return _ID;
			}
		}

		[ColumnName("CREATE_USER",false,typeof(string))]
		public string CREATE_USER
		{
			set
			{
				_CREATE_USER=value;
			}
			get
			{
				return _CREATE_USER;
			}
		}

		[ColumnName("UPDATE_USER",false,typeof(string))]
		public string UPDATE_USER
		{
			set
			{
				_UPDATE_USER=value;
			}
			get
			{
				return _UPDATE_USER;
			}
		}

		[ColumnName("WEB_SITE",false,typeof(string))]
		public string WEB_SITE
		{
			set
			{
				_WEB_SITE=value;
			}
			get
			{
				return _WEB_SITE;
			}
		}
		public ApplicationHead_Record Clone(){
			try{
				return this.Clone<ApplicationHead_Record>(this);
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		public ApplicationHead gotoTable(){
			try{
				var dbc = LK.Config.DataBase.Factory.getInfo();
				ApplicationHead ret = new ApplicationHead(dbc,this);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180347*/
		public List<Sitemap_Record> Link_Sitemap_By_ApplicationHeadUuid()
		{
			try{
				List<Sitemap_Record> ret= new List<Sitemap_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Sitemap ___table = new Sitemap(dbc);
				ret=(List<Sitemap_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.APPLICATION_HEAD_UUID,this.UUID))
					.FetchAll<Sitemap_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180347*/
		public List<Appmenu_Record> Link_Appmenu_By_ApplicationHeadUuid()
		{
			try{
				List<Appmenu_Record> ret= new List<Appmenu_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Appmenu ___table = new Appmenu(dbc);
				ret=(List<Appmenu_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.APPLICATION_HEAD_UUID,this.UUID))
					.FetchAll<Appmenu_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180347*/
		public List<Apppage_Record> Link_Apppage_By_ApplicationHeadUuid()
		{
			try{
				List<Apppage_Record> ret= new List<Apppage_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Apppage ___table = new Apppage(dbc);
				ret=(List<Apppage_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.APPLICATION_HEAD_UUID,this.UUID))
					.FetchAll<Apppage_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180347*/
		public List<GroupHead_Record> Link_GroupHead_By_ApplicationHeadUuid()
		{
			try{
				List<GroupHead_Record> ret= new List<GroupHead_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				GroupHead ___table = new GroupHead(dbc);
				ret=(List<GroupHead_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.APPLICATION_HEAD_UUID,this.UUID))
					.FetchAll<GroupHead_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180347*/
		public List<GroupHeadV_Record> Link_GroupHeadV_By_ApplicationHeadUuid()
		{
			try{
				List<GroupHeadV_Record> ret= new List<GroupHeadV_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				GroupHeadV ___table = new GroupHeadV(dbc);
				ret=(List<GroupHeadV_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.APPLICATION_HEAD_UUID,this.UUID))
					.FetchAll<GroupHeadV_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180347*/
		public List<AppmenuApppageV_Record> Link_AppmenuApppageV_By_ApplicationHeadUuid()
		{
			try{
				List<AppmenuApppageV_Record> ret= new List<AppmenuApppageV_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				AppmenuApppageV ___table = new AppmenuApppageV(dbc);
				ret=(List<AppmenuApppageV_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.APPLICATION_HEAD_UUID,this.UUID))
					.FetchAll<AppmenuApppageV_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180347*/
		public List<Proxy_Record> Link_Proxy_By_ApplicationHeadUuid()
		{
			try{
				List<Proxy_Record> ret= new List<Proxy_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Proxy ___table = new Proxy(dbc);
				ret=(List<Proxy_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.APPLICATION_HEAD_UUID,this.UUID))
					.FetchAll<Proxy_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180347*/
		public List<VAppmenuProxyMap_Record> Link_VAppmenuProxyMap_By_ApplicationHeadUuid()
		{
			try{
				List<VAppmenuProxyMap_Record> ret= new List<VAppmenuProxyMap_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				VAppmenuProxyMap ___table = new VAppmenuProxyMap(dbc);
				ret=(List<VAppmenuProxyMap_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.APPLICATION_HEAD_UUID,this.UUID))
					.FetchAll<VAppmenuProxyMap_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180347*/
		public List<VAuthProxy_Record> Link_VAuthProxy_By_ApplicationHeadUuid()
		{
			try{
				List<VAuthProxy_Record> ret= new List<VAuthProxy_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				VAuthProxy ___table = new VAuthProxy(dbc);
				ret=(List<VAuthProxy_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.APPLICATION_HEAD_UUID,this.UUID))
					.FetchAll<VAuthProxy_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180348*/
		public List<Sitemap_Record> Link_Sitemap_By_ApplicationHeadUuid(OrderLimit limit)
		{
			try{
				List<Sitemap_Record> ret= new List<Sitemap_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Sitemap ___table = new Sitemap(dbc);
				ret=(List<Sitemap_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.APPLICATION_HEAD_UUID,this.UUID))
					.Order(limit)
					.Limit(limit)
					.FetchAll<Sitemap_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180348*/
		public List<Appmenu_Record> Link_Appmenu_By_ApplicationHeadUuid(OrderLimit limit)
		{
			try{
				List<Appmenu_Record> ret= new List<Appmenu_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Appmenu ___table = new Appmenu(dbc);
				ret=(List<Appmenu_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.APPLICATION_HEAD_UUID,this.UUID))
					.Order(limit)
					.Limit(limit)
					.FetchAll<Appmenu_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180348*/
		public List<Apppage_Record> Link_Apppage_By_ApplicationHeadUuid(OrderLimit limit)
		{
			try{
				List<Apppage_Record> ret= new List<Apppage_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Apppage ___table = new Apppage(dbc);
				ret=(List<Apppage_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.APPLICATION_HEAD_UUID,this.UUID))
					.Order(limit)
					.Limit(limit)
					.FetchAll<Apppage_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180348*/
		public List<GroupHead_Record> Link_GroupHead_By_ApplicationHeadUuid(OrderLimit limit)
		{
			try{
				List<GroupHead_Record> ret= new List<GroupHead_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				GroupHead ___table = new GroupHead(dbc);
				ret=(List<GroupHead_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.APPLICATION_HEAD_UUID,this.UUID))
					.Order(limit)
					.Limit(limit)
					.FetchAll<GroupHead_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180348*/
		public List<GroupHeadV_Record> Link_GroupHeadV_By_ApplicationHeadUuid(OrderLimit limit)
		{
			try{
				List<GroupHeadV_Record> ret= new List<GroupHeadV_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				GroupHeadV ___table = new GroupHeadV(dbc);
				ret=(List<GroupHeadV_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.APPLICATION_HEAD_UUID,this.UUID))
					.Order(limit)
					.Limit(limit)
					.FetchAll<GroupHeadV_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180348*/
		public List<AppmenuApppageV_Record> Link_AppmenuApppageV_By_ApplicationHeadUuid(OrderLimit limit)
		{
			try{
				List<AppmenuApppageV_Record> ret= new List<AppmenuApppageV_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				AppmenuApppageV ___table = new AppmenuApppageV(dbc);
				ret=(List<AppmenuApppageV_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.APPLICATION_HEAD_UUID,this.UUID))
					.Order(limit)
					.Limit(limit)
					.FetchAll<AppmenuApppageV_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180348*/
		public List<Proxy_Record> Link_Proxy_By_ApplicationHeadUuid(OrderLimit limit)
		{
			try{
				List<Proxy_Record> ret= new List<Proxy_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Proxy ___table = new Proxy(dbc);
				ret=(List<Proxy_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.APPLICATION_HEAD_UUID,this.UUID))
					.Order(limit)
					.Limit(limit)
					.FetchAll<Proxy_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180348*/
		public List<VAppmenuProxyMap_Record> Link_VAppmenuProxyMap_By_ApplicationHeadUuid(OrderLimit limit)
		{
			try{
				List<VAppmenuProxyMap_Record> ret= new List<VAppmenuProxyMap_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				VAppmenuProxyMap ___table = new VAppmenuProxyMap(dbc);
				ret=(List<VAppmenuProxyMap_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.APPLICATION_HEAD_UUID,this.UUID))
					.Order(limit)
					.Limit(limit)
					.FetchAll<VAppmenuProxyMap_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180348*/
		public List<VAuthProxy_Record> Link_VAuthProxy_By_ApplicationHeadUuid(OrderLimit limit)
		{
			try{
				List<VAuthProxy_Record> ret= new List<VAuthProxy_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				VAuthProxy ___table = new VAuthProxy(dbc);
				ret=(List<VAuthProxy_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.APPLICATION_HEAD_UUID,this.UUID))
					.Order(limit)
					.Limit(limit)
					.FetchAll<VAuthProxy_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180357*/
		public Sitemap LinkFill_Sitemap_By_ApplicationHeadUuid()
		{
			try{
				var data = Link_Sitemap_By_ApplicationHeadUuid();
				Sitemap ret=new Sitemap(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180357*/
		public Appmenu LinkFill_Appmenu_By_ApplicationHeadUuid()
		{
			try{
				var data = Link_Appmenu_By_ApplicationHeadUuid();
				Appmenu ret=new Appmenu(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180357*/
		public Apppage LinkFill_Apppage_By_ApplicationHeadUuid()
		{
			try{
				var data = Link_Apppage_By_ApplicationHeadUuid();
				Apppage ret=new Apppage(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180357*/
		public GroupHead LinkFill_GroupHead_By_ApplicationHeadUuid()
		{
			try{
				var data = Link_GroupHead_By_ApplicationHeadUuid();
				GroupHead ret=new GroupHead(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180357*/
		public GroupHeadV LinkFill_GroupHeadV_By_ApplicationHeadUuid()
		{
			try{
				var data = Link_GroupHeadV_By_ApplicationHeadUuid();
				GroupHeadV ret=new GroupHeadV(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180357*/
		public AppmenuApppageV LinkFill_AppmenuApppageV_By_ApplicationHeadUuid()
		{
			try{
				var data = Link_AppmenuApppageV_By_ApplicationHeadUuid();
				AppmenuApppageV ret=new AppmenuApppageV(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180357*/
		public Proxy LinkFill_Proxy_By_ApplicationHeadUuid()
		{
			try{
				var data = Link_Proxy_By_ApplicationHeadUuid();
				Proxy ret=new Proxy(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180357*/
		public VAppmenuProxyMap LinkFill_VAppmenuProxyMap_By_ApplicationHeadUuid()
		{
			try{
				var data = Link_VAppmenuProxyMap_By_ApplicationHeadUuid();
				VAppmenuProxyMap ret=new VAppmenuProxyMap(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180357*/
		public VAuthProxy LinkFill_VAuthProxy_By_ApplicationHeadUuid()
		{
			try{
				var data = Link_VAuthProxy_By_ApplicationHeadUuid();
				VAuthProxy ret=new VAuthProxy(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180358*/
		public Sitemap LinkFill_Sitemap_By_ApplicationHeadUuid(OrderLimit limit)
		{
			try{
				var data = Link_Sitemap_By_ApplicationHeadUuid(limit);
				Sitemap ret=new Sitemap(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180358*/
		public Appmenu LinkFill_Appmenu_By_ApplicationHeadUuid(OrderLimit limit)
		{
			try{
				var data = Link_Appmenu_By_ApplicationHeadUuid(limit);
				Appmenu ret=new Appmenu(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180358*/
		public Apppage LinkFill_Apppage_By_ApplicationHeadUuid(OrderLimit limit)
		{
			try{
				var data = Link_Apppage_By_ApplicationHeadUuid(limit);
				Apppage ret=new Apppage(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180358*/
		public GroupHead LinkFill_GroupHead_By_ApplicationHeadUuid(OrderLimit limit)
		{
			try{
				var data = Link_GroupHead_By_ApplicationHeadUuid(limit);
				GroupHead ret=new GroupHead(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180358*/
		public GroupHeadV LinkFill_GroupHeadV_By_ApplicationHeadUuid(OrderLimit limit)
		{
			try{
				var data = Link_GroupHeadV_By_ApplicationHeadUuid(limit);
				GroupHeadV ret=new GroupHeadV(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180358*/
		public AppmenuApppageV LinkFill_AppmenuApppageV_By_ApplicationHeadUuid(OrderLimit limit)
		{
			try{
				var data = Link_AppmenuApppageV_By_ApplicationHeadUuid(limit);
				AppmenuApppageV ret=new AppmenuApppageV(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180358*/
		public Proxy LinkFill_Proxy_By_ApplicationHeadUuid(OrderLimit limit)
		{
			try{
				var data = Link_Proxy_By_ApplicationHeadUuid(limit);
				Proxy ret=new Proxy(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180358*/
		public VAppmenuProxyMap LinkFill_VAppmenuProxyMap_By_ApplicationHeadUuid(OrderLimit limit)
		{
			try{
				var data = Link_VAppmenuProxyMap_By_ApplicationHeadUuid(limit);
				VAppmenuProxyMap ret=new VAppmenuProxyMap(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180358*/
		public VAuthProxy LinkFill_VAuthProxy_By_ApplicationHeadUuid(OrderLimit limit)
		{
			try{
				var data = Link_VAuthProxy_By_ApplicationHeadUuid(limit);
				VAuthProxy ret=new VAuthProxy(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
	}
}
