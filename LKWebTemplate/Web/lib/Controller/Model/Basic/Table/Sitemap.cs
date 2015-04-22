using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;  
using LK.Attribute;  
using LK.DB;  
using LK.Config.DataBase;  
using LK.DB.SQLCreater;  
using LKWebTemplate.Model.Basic.Table.Record  ;  
namespace LKWebTemplate.Model.Basic.Table
{
	[LkDataBase("BASIC")]
	[TableView("SITEMAP", true)]
	public partial class Sitemap : TableBase{
	/*固定物件*/
	//LK.DB.SQLCreater.ASQLCreater sqlCreater = null;
	/*固定物件但名稱需更新*/
	private Sitemap_Record _currentRecord = null;
	private IList<Sitemap_Record> _All_Record = new List<Sitemap_Record>();
		/*建構子*/
		public Sitemap(){}
		public Sitemap(IDataBaseConfigInfo dbc,string db): base(dbc,db){}
		public Sitemap(IDataBaseConfigInfo dbc): base(dbc){}
		public Sitemap(IDataBaseConfigInfo dbc,Sitemap_Record currenData){
			this.setDataBaseConfigInfo(dbc);
			this._currentRecord = currenData;
		}
		public Sitemap(IList<Sitemap_Record> currenData){
			this._All_Record = currenData;
		}
		/*欄位資訊 Start*/
		public string UUID {
			[ColumnName("UUID",true,typeof(string))]
			get{return "UUID" ; }}
		public string IS_ACTIVE {
			[ColumnName("IS_ACTIVE",false,typeof(string))]
			get{return "IS_ACTIVE" ; }}
		public string CREATE_DATE {
			[ColumnName("CREATE_DATE",false,typeof(DateTime?))]
			get{return "CREATE_DATE" ; }}
		public string CREATE_USER {
			[ColumnName("CREATE_USER",false,typeof(string))]
			get{return "CREATE_USER" ; }}
		public string UPDATE_DATE {
			[ColumnName("UPDATE_DATE",false,typeof(DateTime?))]
			get{return "UPDATE_DATE" ; }}
		public string UPDATE_USER {
			[ColumnName("UPDATE_USER",false,typeof(string))]
			get{return "UPDATE_USER" ; }}
		public string SITEMAP_UUID {
			[ColumnName("SITEMAP_UUID",false,typeof(string))]
			get{return "SITEMAP_UUID" ; }}
		public string APPPAGE_UUID {
			[ColumnName("APPPAGE_UUID",false,typeof(string))]
			get{return "APPPAGE_UUID" ; }}
		public string ROOT_UUID {
			[ColumnName("ROOT_UUID",false,typeof(string))]
			get{return "ROOT_UUID" ; }}
		public string HASCHILD {
			[ColumnName("HASCHILD",false,typeof(string))]
			get{return "HASCHILD" ; }}
		public string APPLICATION_HEAD_UUID {
			[ColumnName("APPLICATION_HEAD_UUID",false,typeof(string))]
			get{return "APPLICATION_HEAD_UUID" ; }}
		/*欄位資訊 End*/
		/*固定的方法，但名稱需變更 Start*/
		public Sitemap_Record CurrentRecord(){
			try{
				if (_currentRecord == null){
					if (this._All_Record.Count > 0){
						_currentRecord = this._All_Record.First();
					}
				}
				return _currentRecord;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		public Sitemap_Record CreateNew(){
			try{
				Sitemap_Record newData = new Sitemap_Record();
				return newData;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		public IList<Sitemap_Record> AllRecord(){
			try{
				return _All_Record;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		public void RemoveAllRecord(){
			try{
				_All_Record = new List<Sitemap_Record>();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*固定的方法，但名稱需變更 End*/
		/*有關PK的方法*/
		//TEMPLATE TABLE 201303180156
		public Sitemap Fill_By_PK(string puuid){
			try{
				IList<Sitemap_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<Sitemap_Record>()  ;  
				_All_Record = ret;
				if (_All_Record.Count > 0){
					_currentRecord = ret.First();}
				else{
					_currentRecord = null;}
				return this;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		//TEMPLATE TABLE 201303180156
		public Sitemap Fill_By_PK(string puuid,DB db){
			try{
				IList<Sitemap_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<Sitemap_Record>(db)  ;  
				_All_Record = ret;
				if (_All_Record.Count > 0){
					_currentRecord = ret.First();}
				else{
					_currentRecord = null;}
				return this;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		//TEMPLATE TABLE 20130319042
		public Sitemap_Record Fetch_By_PK(string puuid){
			try{
				IList<Sitemap_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<Sitemap_Record>()  ;  
				return ret.First();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		//TEMPLATE TABLE 20130319044
		public Sitemap_Record Fetch_By_PK(string puuid,DB db){
			try{
				IList<Sitemap_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<Sitemap_Record>(db)  ;  
				return ret.First();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		//TEMPLATE TABLE 20130319045
		public Sitemap Fill_By_Uuid(string puuid){
			try{
				IList<Sitemap_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<Sitemap_Record>()  ;  
				_All_Record = ret;
				_currentRecord = ret.First();
				return this;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		//TEMPLATE TABLE 20130319046
		public Sitemap Fill_By_Uuid(string puuid,DB db){
			try{
				IList<Sitemap_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<Sitemap_Record>(db)  ;  
				_All_Record = ret;
				_currentRecord = ret.First();
				return this;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		//TEMPLATE TABLE 20130319047
		public Sitemap_Record Fetch_By_Uuid(string puuid){
			try{
				IList<Sitemap_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<Sitemap_Record>()  ;  
				return ret.First();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.ErrorNoThrowException(this, ex);
				return null;
			}
		}
		//TEMPLATE TABLE 20130319048
		public Sitemap_Record Fetch_By_Uuid(string puuid,DB db){
			try{
				IList<Sitemap_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<Sitemap_Record>(db)  ;  
				return ret.First();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來更新資料行*/
		public void UpdateAllRecord() {
			try{
				UpdateAllRecord<Sitemap_Record>(this.AllRecord());   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來更新資料行*/
		public void UpdateAllRecord(DB db) {
			try{
				UpdateAllRecord<Sitemap_Record>(this.AllRecord(),db);   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來新增資料行*/
		public void InsertAllRecord() {
			try{
				InsertAllRecord<Sitemap_Record>(this.AllRecord());   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來新增資料行*/
		public void InsertAllRecord(DB db) {
			try{
				InsertAllRecord<Sitemap_Record>(this.AllRecord(),db);   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來刪除資料行*/
		public void DeleteAllRecord() {
			try{
				DeleteAllRecord<Sitemap_Record>(this.AllRecord());   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來刪除資料行*/
		public void DeleteAllRecord(DB db) {
			try{
				DeleteAllRecord<Sitemap_Record>(this.AllRecord(),db);   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*依照資料表與資料表的關係，產生出來的方法*/
		/*201303180320*/
		public List<Sitemap_Record> Link_Sitemap_By_SitemapUuid()
		{
			try{
				List<Sitemap_Record> ret= new List<Sitemap_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Sitemap ___table = new Sitemap(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.SITEMAP_UUID,item.UUID).R().Or()  ; 
 				}
				condition.CheckSQL();
				ret=(List<Sitemap_Record>)
						___table.Where(condition)
						.FetchAll<Sitemap_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180320*/
		public List<Sitemap_Record> Link_Sitemap_By_RootUuid()
		{
			try{
				List<Sitemap_Record> ret= new List<Sitemap_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Sitemap ___table = new Sitemap(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.ROOT_UUID,item.UUID).R().Or()  ; 
 				}
				condition.CheckSQL();
				ret=(List<Sitemap_Record>)
						___table.Where(condition)
						.FetchAll<Sitemap_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180320*/
		public List<Appmenu_Record> Link_Appmenu_By_SitemapUuid()
		{
			try{
				List<Appmenu_Record> ret= new List<Appmenu_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Appmenu ___table = new Appmenu(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.SITEMAP_UUID,item.UUID).R().Or()  ; 
 				}
				condition.CheckSQL();
				ret=(List<Appmenu_Record>)
						___table.Where(condition)
						.FetchAll<Appmenu_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180320*/
		public List<AppmenuApppageV_Record> Link_AppmenuApppageV_By_SitemapUuid()
		{
			try{
				List<AppmenuApppageV_Record> ret= new List<AppmenuApppageV_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				AppmenuApppageV ___table = new AppmenuApppageV(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.SITEMAP_UUID,item.UUID).R().Or()  ; 
 				}
				condition.CheckSQL();
				ret=(List<AppmenuApppageV_Record>)
						___table.Where(condition)
						.FetchAll<AppmenuApppageV_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180321*/
		public List<Sitemap_Record> Link_Sitemap_By_SitemapUuid(OrderLimit limit)
		{
			try{
				List<Sitemap_Record> ret= new List<Sitemap_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Sitemap ___table = new Sitemap(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.SITEMAP_UUID,item.UUID).R().Or()  ; 
 				}
				condition.CheckSQL();
				ret=(List<Sitemap_Record>)
						___table.Where(condition)
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
		/*201303180321*/
		public List<Sitemap_Record> Link_Sitemap_By_RootUuid(OrderLimit limit)
		{
			try{
				List<Sitemap_Record> ret= new List<Sitemap_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Sitemap ___table = new Sitemap(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.ROOT_UUID,item.UUID).R().Or()  ; 
 				}
				condition.CheckSQL();
				ret=(List<Sitemap_Record>)
						___table.Where(condition)
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
		/*201303180321*/
		public List<Appmenu_Record> Link_Appmenu_By_SitemapUuid(OrderLimit limit)
		{
			try{
				List<Appmenu_Record> ret= new List<Appmenu_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Appmenu ___table = new Appmenu(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.SITEMAP_UUID,item.UUID).R().Or()  ; 
 				}
				condition.CheckSQL();
				ret=(List<Appmenu_Record>)
						___table.Where(condition)
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
		/*201303180321*/
		public List<AppmenuApppageV_Record> Link_AppmenuApppageV_By_SitemapUuid(OrderLimit limit)
		{
			try{
				List<AppmenuApppageV_Record> ret= new List<AppmenuApppageV_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				AppmenuApppageV ___table = new AppmenuApppageV(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.SITEMAP_UUID,item.UUID).R().Or()  ; 
 				}
				condition.CheckSQL();
				ret=(List<AppmenuApppageV_Record>)
						___table.Where(condition)
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
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.UUID,item.APPLICATION_HEAD_UUID).R().Or()  ; 
 				}
				condition.CheckSQL();
				ret=(List<ApplicationHead_Record>)
						___table.Where(condition)
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
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.UUID,item.SITEMAP_UUID).R().Or()  ; 
 				}
				condition.CheckSQL();
				ret=(List<Sitemap_Record>)
						___table.Where(condition)
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
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.UUID,item.APPPAGE_UUID).R().Or()  ; 
 				}
				condition.CheckSQL();
				ret=(List<Apppage_Record>)
						___table.Where(condition)
						.FetchAll<Apppage_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180340*/
		public List<ApplicationHead_Record> Link_ApplicationHead_By_Uuid(OrderLimit limit)
		{
			try{
				List<ApplicationHead_Record> ret= new List<ApplicationHead_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				ApplicationHead ___table = new ApplicationHead(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.UUID,item.APPLICATION_HEAD_UUID).R().Or()  ; 
 				}
				condition.CheckSQL();
				ret=(List<ApplicationHead_Record>)
						___table.Where(condition)
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
		/*201303180340*/
		public List<Sitemap_Record> Link_Sitemap_By_Uuid(OrderLimit limit)
		{
			try{
				List<Sitemap_Record> ret= new List<Sitemap_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Sitemap ___table = new Sitemap(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.UUID,item.SITEMAP_UUID).R().Or()  ; 
 				}
				condition.CheckSQL();
				ret=(List<Sitemap_Record>)
						___table.Where(condition)
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
		/*201303180340*/
		public List<Apppage_Record> Link_Apppage_By_Uuid(OrderLimit limit)
		{
			try{
				List<Apppage_Record> ret= new List<Apppage_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Apppage ___table = new Apppage(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.UUID,item.APPPAGE_UUID).R().Or()  ; 
 				}
				condition.CheckSQL();
				ret=(List<Apppage_Record>)
						___table.Where(condition)
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
		/*201303180324*/
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
		/*201303180324*/
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
		/*201303180324*/
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
		/*201303180324*/
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
		/*201303180325*/
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
		/*201303180325*/
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
		/*201303180325*/
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
		/*201303180325*/
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
		/*201303180336*/
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
		/*201303180336*/
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
		/*201303180336*/
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
		/*201303180337*/
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
		/*201303180337*/
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
		/*201303180337*/
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
