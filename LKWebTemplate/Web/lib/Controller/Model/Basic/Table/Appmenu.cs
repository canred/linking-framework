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
	[TableView("APPMENU", true)]
	public partial class Appmenu : TableBase{
	/*固定物件*/
	//LK.DB.SQLCreater.ASQLCreater sqlCreater = null;
	/*固定物件但名稱需更新*/
	private Appmenu_Record _currentRecord = null;
	private IList<Appmenu_Record> _All_Record = new List<Appmenu_Record>();
		/*建構子*/
		public Appmenu(){}
		public Appmenu(IDataBaseConfigInfo dbc,string db): base(dbc,db){}
		public Appmenu(IDataBaseConfigInfo dbc): base(dbc){}
		public Appmenu(IDataBaseConfigInfo dbc,Appmenu_Record currenData){
			this.setDataBaseConfigInfo(dbc);
			this._currentRecord = currenData;
		}
		public Appmenu(IList<Appmenu_Record> currenData){
			this._All_Record = currenData;
		}
		/*欄位資訊 Start*/
		public string UUID {get{return "UUID" ; }}
		public string IS_ACTIVE {get{return "IS_ACTIVE" ; }}
		public string CREATE_DATE {get{return "CREATE_DATE" ; }}
		public string CREATE_USER {get{return "CREATE_USER" ; }}
		public string UPDATE_DATE {get{return "UPDATE_DATE" ; }}
		public string UPDATE_USER {get{return "UPDATE_USER" ; }}
		public string NAME_ZH_TW {get{return "NAME_ZH_TW" ; }}
		public string NAME_ZH_CN {get{return "NAME_ZH_CN" ; }}
		public string NAME_EN_US {get{return "NAME_EN_US" ; }}
		public string ID {get{return "ID" ; }}
		public string APPMENU_UUID {get{return "APPMENU_UUID" ; }}
		public string HASCHILD {get{return "HASCHILD" ; }}
		public string APPLICATION_HEAD_UUID {get{return "APPLICATION_HEAD_UUID" ; }}
		public string ORD {get{return "ORD" ; }}
		public string PARAMETER_CLASS {get{return "PARAMETER_CLASS" ; }}
		public string IMAGE {get{return "IMAGE" ; }}
		public string SITEMAP_UUID {get{return "SITEMAP_UUID" ; }}
		public string ACTION_MODE {get{return "ACTION_MODE" ; }}
		public string IS_DEFAULT_PAGE {get{return "IS_DEFAULT_PAGE" ; }}
		public string IS_ADMIN {get{return "IS_ADMIN" ; }}
		public string NAME_JPN {get{return "NAME_JPN" ; }}
		/*欄位資訊 End*/
		/*固定的方法，但名稱需變更 Start*/
		public Appmenu_Record CurrentRecord(){
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
		public Appmenu_Record CreateNew(){
			try{
				Appmenu_Record newData = new Appmenu_Record();
				return newData;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		public IList<Appmenu_Record> AllRecord(){
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
				_All_Record = new List<Appmenu_Record>();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*固定的方法，但名稱需變更 End*/
		/*有關PK的方法*/
		//TEMPLATE TABLE 201303180156
		public Appmenu Fill_By_PK(string puuid){
			try{
				IList<Appmenu_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<Appmenu_Record>()  ;  
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
		public Appmenu Fill_By_PK(string puuid,DB db){
			try{
				IList<Appmenu_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<Appmenu_Record>(db)  ;  
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
		public Appmenu_Record Fetch_By_PK(string puuid){
			try{
				IList<Appmenu_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<Appmenu_Record>()  ;  
				return ret.First();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		//TEMPLATE TABLE 20130319044
		public Appmenu_Record Fetch_By_PK(string puuid,DB db){
			try{
				IList<Appmenu_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<Appmenu_Record>(db)  ;  
				return ret.First();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		//TEMPLATE TABLE 20130319045
		public Appmenu Fill_By_Uuid(string puuid){
			try{
				IList<Appmenu_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<Appmenu_Record>()  ;  
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
		public Appmenu Fill_By_Uuid(string puuid,DB db){
			try{
				IList<Appmenu_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<Appmenu_Record>(db)  ;  
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
		public Appmenu_Record Fetch_By_Uuid(string puuid){
			try{
				IList<Appmenu_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<Appmenu_Record>()  ;  
				return ret.First();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.ErrorNoThrowException(this, ex);
				return null;
			}
		}
		//TEMPLATE TABLE 20130319048
		public Appmenu_Record Fetch_By_Uuid(string puuid,DB db){
			try{
				IList<Appmenu_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<Appmenu_Record>(db)  ;  
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
				UpdateAllRecord<Appmenu_Record>(this.AllRecord());   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來更新資料行*/
		public void UpdateAllRecord(DB db) {
			try{
				UpdateAllRecord<Appmenu_Record>(this.AllRecord(),db);   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來新增資料行*/
		public void InsertAllRecord() {
			try{
				InsertAllRecord<Appmenu_Record>(this.AllRecord());   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來新增資料行*/
		public void InsertAllRecord(DB db) {
			try{
				InsertAllRecord<Appmenu_Record>(this.AllRecord(),db);   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來刪除資料行*/
		public void DeleteAllRecord() {
			try{
				DeleteAllRecord<Appmenu_Record>(this.AllRecord());   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來刪除資料行*/
		public void DeleteAllRecord(DB db) {
			try{
				DeleteAllRecord<Appmenu_Record>(this.AllRecord(),db);   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*依照資料表與資料表的關係，產生出來的方法*/
		/*201303180320*/
		public List<Appmenu_Record> Link_Appmenu_By_AppmenuUuid()
		{
			try{
				List<Appmenu_Record> ret= new List<Appmenu_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Appmenu ___table = new Appmenu(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.APPMENU_UUID,item.UUID).R().Or()  ; 
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
		public List<GroupAppmenu_Record> Link_GroupAppmenu_By_AppmenuUuid()
		{
			try{
				List<GroupAppmenu_Record> ret= new List<GroupAppmenu_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				GroupAppmenu ___table = new GroupAppmenu(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.APPMENU_UUID,item.UUID).R().Or()  ; 
 				}
				condition.CheckSQL();
				ret=(List<GroupAppmenu_Record>)
						___table.Where(condition)
						.FetchAll<GroupAppmenu_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180320*/
		public List<GroupAppmenuV_Record> Link_GroupAppmenuV_By_AppmenuUuid()
		{
			try{
				List<GroupAppmenuV_Record> ret= new List<GroupAppmenuV_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				GroupAppmenuV ___table = new GroupAppmenuV(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.APPMENU_UUID,item.UUID).R().Or()  ; 
 				}
				condition.CheckSQL();
				ret=(List<GroupAppmenuV_Record>)
						___table.Where(condition)
						.FetchAll<GroupAppmenuV_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180320*/
		public List<AppmenuApppageV_Record> Link_AppmenuApppageV_By_AppmenuUuid()
		{
			try{
				List<AppmenuApppageV_Record> ret= new List<AppmenuApppageV_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				AppmenuApppageV ___table = new AppmenuApppageV(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.APPMENU_UUID,item.UUID).R().Or()  ; 
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
		/*201303180320*/
		public List<VAppmenuProxyMap_Record> Link_VAppmenuProxyMap_By_AppmenuUuid()
		{
			try{
				List<VAppmenuProxyMap_Record> ret= new List<VAppmenuProxyMap_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				VAppmenuProxyMap ___table = new VAppmenuProxyMap(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.APPMENU_UUID,item.UUID).R().Or()  ; 
 				}
				condition.CheckSQL();
				ret=(List<VAppmenuProxyMap_Record>)
						___table.Where(condition)
						.FetchAll<VAppmenuProxyMap_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180321*/
		public List<Appmenu_Record> Link_Appmenu_By_AppmenuUuid(OrderLimit limit)
		{
			try{
				List<Appmenu_Record> ret= new List<Appmenu_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Appmenu ___table = new Appmenu(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.APPMENU_UUID,item.UUID).R().Or()  ; 
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
		public List<GroupAppmenu_Record> Link_GroupAppmenu_By_AppmenuUuid(OrderLimit limit)
		{
			try{
				List<GroupAppmenu_Record> ret= new List<GroupAppmenu_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				GroupAppmenu ___table = new GroupAppmenu(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.APPMENU_UUID,item.UUID).R().Or()  ; 
 				}
				condition.CheckSQL();
				ret=(List<GroupAppmenu_Record>)
						___table.Where(condition)
						.Order(limit)
						.Limit(limit)
						.FetchAll<GroupAppmenu_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180321*/
		public List<GroupAppmenuV_Record> Link_GroupAppmenuV_By_AppmenuUuid(OrderLimit limit)
		{
			try{
				List<GroupAppmenuV_Record> ret= new List<GroupAppmenuV_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				GroupAppmenuV ___table = new GroupAppmenuV(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.APPMENU_UUID,item.UUID).R().Or()  ; 
 				}
				condition.CheckSQL();
				ret=(List<GroupAppmenuV_Record>)
						___table.Where(condition)
						.Order(limit)
						.Limit(limit)
						.FetchAll<GroupAppmenuV_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180321*/
		public List<AppmenuApppageV_Record> Link_AppmenuApppageV_By_AppmenuUuid(OrderLimit limit)
		{
			try{
				List<AppmenuApppageV_Record> ret= new List<AppmenuApppageV_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				AppmenuApppageV ___table = new AppmenuApppageV(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.APPMENU_UUID,item.UUID).R().Or()  ; 
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
		/*201303180321*/
		public List<VAppmenuProxyMap_Record> Link_VAppmenuProxyMap_By_AppmenuUuid(OrderLimit limit)
		{
			try{
				List<VAppmenuProxyMap_Record> ret= new List<VAppmenuProxyMap_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				VAppmenuProxyMap ___table = new VAppmenuProxyMap(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.APPMENU_UUID,item.UUID).R().Or()  ; 
 				}
				condition.CheckSQL();
				ret=(List<VAppmenuProxyMap_Record>)
						___table.Where(condition)
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
		public List<Appmenu_Record> Link_Appmenu_By_Uuid()
		{
			try{
				List<Appmenu_Record> ret= new List<Appmenu_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Appmenu ___table = new Appmenu(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.UUID,item.APPMENU_UUID).R().Or()  ; 
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
		public List<Appmenu_Record> Link_Appmenu_By_Uuid(OrderLimit limit)
		{
			try{
				List<Appmenu_Record> ret= new List<Appmenu_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Appmenu ___table = new Appmenu(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.UUID,item.APPMENU_UUID).R().Or()  ; 
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
		/*201303180324*/
		public Appmenu LinkFill_Appmenu_By_AppmenuUuid()
		{
			try{
				var data = Link_Appmenu_By_AppmenuUuid();
				Appmenu ret=new Appmenu(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180324*/
		public GroupAppmenu LinkFill_GroupAppmenu_By_AppmenuUuid()
		{
			try{
				var data = Link_GroupAppmenu_By_AppmenuUuid();
				GroupAppmenu ret=new GroupAppmenu(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180324*/
		public GroupAppmenuV LinkFill_GroupAppmenuV_By_AppmenuUuid()
		{
			try{
				var data = Link_GroupAppmenuV_By_AppmenuUuid();
				GroupAppmenuV ret=new GroupAppmenuV(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180324*/
		public AppmenuApppageV LinkFill_AppmenuApppageV_By_AppmenuUuid()
		{
			try{
				var data = Link_AppmenuApppageV_By_AppmenuUuid();
				AppmenuApppageV ret=new AppmenuApppageV(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180324*/
		public VAppmenuProxyMap LinkFill_VAppmenuProxyMap_By_AppmenuUuid()
		{
			try{
				var data = Link_VAppmenuProxyMap_By_AppmenuUuid();
				VAppmenuProxyMap ret=new VAppmenuProxyMap(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180325*/
		public Appmenu LinkFill_Appmenu_By_AppmenuUuid(OrderLimit limit)
		{
			try{
				var data = Link_Appmenu_By_AppmenuUuid(limit);
				Appmenu ret=new Appmenu(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180325*/
		public GroupAppmenu LinkFill_GroupAppmenu_By_AppmenuUuid(OrderLimit limit)
		{
			try{
				var data = Link_GroupAppmenu_By_AppmenuUuid(limit);
				GroupAppmenu ret=new GroupAppmenu(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180325*/
		public GroupAppmenuV LinkFill_GroupAppmenuV_By_AppmenuUuid(OrderLimit limit)
		{
			try{
				var data = Link_GroupAppmenuV_By_AppmenuUuid(limit);
				GroupAppmenuV ret=new GroupAppmenuV(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180325*/
		public AppmenuApppageV LinkFill_AppmenuApppageV_By_AppmenuUuid(OrderLimit limit)
		{
			try{
				var data = Link_AppmenuApppageV_By_AppmenuUuid(limit);
				AppmenuApppageV ret=new AppmenuApppageV(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180325*/
		public VAppmenuProxyMap LinkFill_VAppmenuProxyMap_By_AppmenuUuid(OrderLimit limit)
		{
			try{
				var data = Link_VAppmenuProxyMap_By_AppmenuUuid(limit);
				VAppmenuProxyMap ret=new VAppmenuProxyMap(data);
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
		public Appmenu LinkFill_Appmenu_By_Uuid()
		{
			try{
				var data = Link_Appmenu_By_Uuid();
				Appmenu ret=new Appmenu(data);
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
		public Appmenu LinkFill_Appmenu_By_Uuid(OrderLimit limit)
		{
			try{
				var data = Link_Appmenu_By_Uuid(limit);
				Appmenu ret=new Appmenu(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
	}
}
