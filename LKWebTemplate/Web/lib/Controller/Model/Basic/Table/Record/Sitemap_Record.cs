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
	[TableView("SITEMAP", true)]
	[LkDataBase("BASIC")]
	[Serializable]
	public class Sitemap_Record : RecordBase{
		public Sitemap_Record(){}
		/*欄位資訊 Start*/
		string _UUID=null;
		string _IS_ACTIVE=null;
		DateTime? _CREATE_DATE=null;
		string _CREATE_USER=null;
		DateTime? _UPDATE_DATE=null;
		string _UPDATE_USER=null;
		string _SITEMAP_UUID=null;
		string _APPPAGE_UUID=null;
		string _ROOT_UUID=null;
		string _HASCHILD=null;
		string _APPLICATION_HEAD_UUID=null;
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

		[ColumnName("SITEMAP_UUID",false,typeof(string))]
		public string SITEMAP_UUID
		{
			set
			{
				_SITEMAP_UUID=value;
			}
			get
			{
				return _SITEMAP_UUID;
			}
		}

		[ColumnName("APPPAGE_UUID",false,typeof(string))]
		public string APPPAGE_UUID
		{
			set
			{
				_APPPAGE_UUID=value;
			}
			get
			{
				return _APPPAGE_UUID;
			}
		}

		[ColumnName("ROOT_UUID",false,typeof(string))]
		public string ROOT_UUID
		{
			set
			{
				_ROOT_UUID=value;
			}
			get
			{
				return _ROOT_UUID;
			}
		}

		[ColumnName("HASCHILD",false,typeof(string))]
		public string HASCHILD
		{
			set
			{
				_HASCHILD=value;
			}
			get
			{
				return _HASCHILD;
			}
		}

		[ColumnName("APPLICATION_HEAD_UUID",false,typeof(string))]
		public string APPLICATION_HEAD_UUID
		{
			set
			{
				_APPLICATION_HEAD_UUID=value;
			}
			get
			{
				return _APPLICATION_HEAD_UUID;
			}
		}
		public Sitemap_Record Clone(){
			try{
				return this.Clone<Sitemap_Record>(this);
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		public Sitemap gotoTable(){
			try{
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Sitemap ret = new Sitemap(dbc,this);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180347*/
		public List<Sitemap_Record> Link_Sitemap_By_SitemapUuid()
		{
			try{
				List<Sitemap_Record> ret= new List<Sitemap_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Sitemap ___table = new Sitemap(dbc);
				ret=(List<Sitemap_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.SITEMAP_UUID,this.UUID))
					.FetchAll<Sitemap_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180347*/
		public List<Sitemap_Record> Link_Sitemap_By_RootUuid()
		{
			try{
				List<Sitemap_Record> ret= new List<Sitemap_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Sitemap ___table = new Sitemap(dbc);
				ret=(List<Sitemap_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.ROOT_UUID,this.UUID))
					.FetchAll<Sitemap_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180347*/
		public List<Appmenu_Record> Link_Appmenu_By_SitemapUuid()
		{
			try{
				List<Appmenu_Record> ret= new List<Appmenu_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Appmenu ___table = new Appmenu(dbc);
				ret=(List<Appmenu_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.SITEMAP_UUID,this.UUID))
					.FetchAll<Appmenu_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180347*/
		public List<AppmenuApppageV_Record> Link_AppmenuApppageV_By_SitemapUuid()
		{
			try{
				List<AppmenuApppageV_Record> ret= new List<AppmenuApppageV_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				AppmenuApppageV ___table = new AppmenuApppageV(dbc);
				ret=(List<AppmenuApppageV_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.SITEMAP_UUID,this.UUID))
					.FetchAll<AppmenuApppageV_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180348*/
		public List<Sitemap_Record> Link_Sitemap_By_SitemapUuid(OrderLimit limit)
		{
			try{
				List<Sitemap_Record> ret= new List<Sitemap_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Sitemap ___table = new Sitemap(dbc);
				ret=(List<Sitemap_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.SITEMAP_UUID,this.UUID))
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
		public List<Sitemap_Record> Link_Sitemap_By_RootUuid(OrderLimit limit)
		{
			try{
				List<Sitemap_Record> ret= new List<Sitemap_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Sitemap ___table = new Sitemap(dbc);
				ret=(List<Sitemap_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.ROOT_UUID,this.UUID))
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
		public List<Appmenu_Record> Link_Appmenu_By_SitemapUuid(OrderLimit limit)
		{
			try{
				List<Appmenu_Record> ret= new List<Appmenu_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Appmenu ___table = new Appmenu(dbc);
				ret=(List<Appmenu_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.SITEMAP_UUID,this.UUID))
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
		public List<AppmenuApppageV_Record> Link_AppmenuApppageV_By_SitemapUuid(OrderLimit limit)
		{
			try{
				List<AppmenuApppageV_Record> ret= new List<AppmenuApppageV_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				AppmenuApppageV ___table = new AppmenuApppageV(dbc);
				ret=(List<AppmenuApppageV_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.SITEMAP_UUID,this.UUID))
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
		public List<ApplicationHead_Record> Link_ApplicationHead_By_Uuid()
		{
			try{
				List<ApplicationHead_Record> ret= new List<ApplicationHead_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				ApplicationHead ___table = new ApplicationHead(dbc);
				ret=(List<ApplicationHead_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.UUID,this.APPLICATION_HEAD_UUID))
					.FetchAll<ApplicationHead_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		public List<Sitemap_Record> Link_Sitemap_By_Uuid()
		{
			try{
				List<Sitemap_Record> ret= new List<Sitemap_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Sitemap ___table = new Sitemap(dbc);
				ret=(List<Sitemap_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.UUID,this.SITEMAP_UUID))
					.FetchAll<Sitemap_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		public List<Apppage_Record> Link_Apppage_By_Uuid()
		{
			try{
				List<Apppage_Record> ret= new List<Apppage_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Apppage ___table = new Apppage(dbc);
				ret=(List<Apppage_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.UUID,this.APPPAGE_UUID))
					.FetchAll<Apppage_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180404*/
		public List<ApplicationHead_Record> Link_ApplicationHead_By_Uuid(OrderLimit limit)
		{
			try{
				List<ApplicationHead_Record> ret= new List<ApplicationHead_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				ApplicationHead ___table = new ApplicationHead(dbc);
				ret=(List<ApplicationHead_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.UUID,this.APPLICATION_HEAD_UUID))
					.Order(limit)
					.Limit(limit)
					.FetchAll<ApplicationHead_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180404*/
		public List<Sitemap_Record> Link_Sitemap_By_Uuid(OrderLimit limit)
		{
			try{
				List<Sitemap_Record> ret= new List<Sitemap_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Sitemap ___table = new Sitemap(dbc);
				ret=(List<Sitemap_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.UUID,this.SITEMAP_UUID))
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
		/*201303180404*/
		public List<Apppage_Record> Link_Apppage_By_Uuid(OrderLimit limit)
		{
			try{
				List<Apppage_Record> ret= new List<Apppage_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Apppage ___table = new Apppage(dbc);
				ret=(List<Apppage_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.UUID,this.APPPAGE_UUID))
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
		/*201303180357*/
		public Sitemap LinkFill_Sitemap_By_SitemapUuid()
		{
			try{
				var data = Link_Sitemap_By_SitemapUuid();
				Sitemap ret=new Sitemap(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180357*/
		public Sitemap LinkFill_Sitemap_By_RootUuid()
		{
			try{
				var data = Link_Sitemap_By_RootUuid();
				Sitemap ret=new Sitemap(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180357*/
		public Appmenu LinkFill_Appmenu_By_SitemapUuid()
		{
			try{
				var data = Link_Appmenu_By_SitemapUuid();
				Appmenu ret=new Appmenu(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180357*/
		public AppmenuApppageV LinkFill_AppmenuApppageV_By_SitemapUuid()
		{
			try{
				var data = Link_AppmenuApppageV_By_SitemapUuid();
				AppmenuApppageV ret=new AppmenuApppageV(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180358*/
		public Sitemap LinkFill_Sitemap_By_SitemapUuid(OrderLimit limit)
		{
			try{
				var data = Link_Sitemap_By_SitemapUuid(limit);
				Sitemap ret=new Sitemap(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180358*/
		public Sitemap LinkFill_Sitemap_By_RootUuid(OrderLimit limit)
		{
			try{
				var data = Link_Sitemap_By_RootUuid(limit);
				Sitemap ret=new Sitemap(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180358*/
		public Appmenu LinkFill_Appmenu_By_SitemapUuid(OrderLimit limit)
		{
			try{
				var data = Link_Appmenu_By_SitemapUuid(limit);
				Appmenu ret=new Appmenu(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180358*/
		public AppmenuApppageV LinkFill_AppmenuApppageV_By_SitemapUuid(OrderLimit limit)
		{
			try{
				var data = Link_AppmenuApppageV_By_SitemapUuid(limit);
				AppmenuApppageV ret=new AppmenuApppageV(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*2013031800428*/
		public ApplicationHead LinkFill_ApplicationHead_By_Uuid()
		{
			try{
				var data = Link_ApplicationHead_By_Uuid();
				ApplicationHead ret=new ApplicationHead(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*2013031800428*/
		public Sitemap LinkFill_Sitemap_By_Uuid()
		{
			try{
				var data = Link_Sitemap_By_Uuid();
				Sitemap ret=new Sitemap(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*2013031800428*/
		public Apppage LinkFill_Apppage_By_Uuid()
		{
			try{
				var data = Link_Apppage_By_Uuid();
				Apppage ret=new Apppage(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180429*/
		public ApplicationHead LinkFill_ApplicationHead_By_Uuid(OrderLimit limit)
		{
			try{
				var data = Link_ApplicationHead_By_Uuid(limit);
				ApplicationHead ret=new ApplicationHead(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180429*/
		public Sitemap LinkFill_Sitemap_By_Uuid(OrderLimit limit)
		{
			try{
				var data = Link_Sitemap_By_Uuid(limit);
				Sitemap ret=new Sitemap(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180429*/
		public Apppage LinkFill_Apppage_By_Uuid(OrderLimit limit)
		{
			try{
				var data = Link_Apppage_By_Uuid(limit);
				Apppage ret=new Apppage(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
	}
}
