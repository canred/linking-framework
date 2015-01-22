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
	[TableView("APPMENU_PROXY_MAP", true)]
	public partial class AppmenuProxyMap : TableBase{
	/*固定物件*/
	//LK.DB.SQLCreater.ASQLCreater sqlCreater = null;
	/*固定物件但名稱需更新*/
	private AppmenuProxyMap_Record _currentRecord = null;
	private IList<AppmenuProxyMap_Record> _All_Record = new List<AppmenuProxyMap_Record>();
		/*建構子*/
		public AppmenuProxyMap(){}
		public AppmenuProxyMap(IDataBaseConfigInfo dbc,string db): base(dbc,db){}
		public AppmenuProxyMap(IDataBaseConfigInfo dbc): base(dbc){}
		public AppmenuProxyMap(IDataBaseConfigInfo dbc,AppmenuProxyMap_Record currenData){
			this.setDataBaseConfigInfo(dbc);
			this._currentRecord = currenData;
		}
		public AppmenuProxyMap(IList<AppmenuProxyMap_Record> currenData){
			this._All_Record = currenData;
		}
		/*欄位資訊 Start*/
		public string UUID {get{return "UUID" ; }}
		public string PROXY_UUID {get{return "PROXY_UUID" ; }}
		public string APPMENU_UUID {get{return "APPMENU_UUID" ; }}
		/*欄位資訊 End*/
		/*固定的方法，但名稱需變更 Start*/
		public AppmenuProxyMap_Record CurrentRecord(){
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
		public AppmenuProxyMap_Record CreateNew(){
			try{
				AppmenuProxyMap_Record newData = new AppmenuProxyMap_Record();
				return newData;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		public IList<AppmenuProxyMap_Record> AllRecord(){
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
				_All_Record = new List<AppmenuProxyMap_Record>();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*固定的方法，但名稱需變更 End*/
		/*有關PK的方法*/
		//TEMPLATE TABLE 201303180156
		public AppmenuProxyMap Fill_By_PK(string puuid){
			try{
				IList<AppmenuProxyMap_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<AppmenuProxyMap_Record>()  ;  
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
		public AppmenuProxyMap Fill_By_PK(string puuid,DB db){
			try{
				IList<AppmenuProxyMap_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<AppmenuProxyMap_Record>(db)  ;  
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
		public AppmenuProxyMap_Record Fetch_By_PK(string puuid){
			try{
				IList<AppmenuProxyMap_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<AppmenuProxyMap_Record>()  ;  
				return ret.First();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		//TEMPLATE TABLE 20130319044
		public AppmenuProxyMap_Record Fetch_By_PK(string puuid,DB db){
			try{
				IList<AppmenuProxyMap_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<AppmenuProxyMap_Record>(db)  ;  
				return ret.First();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		//TEMPLATE TABLE 20130319045
		public AppmenuProxyMap Fill_By_Uuid(string puuid){
			try{
				IList<AppmenuProxyMap_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<AppmenuProxyMap_Record>()  ;  
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
		public AppmenuProxyMap Fill_By_Uuid(string puuid,DB db){
			try{
				IList<AppmenuProxyMap_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<AppmenuProxyMap_Record>(db)  ;  
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
		public AppmenuProxyMap_Record Fetch_By_Uuid(string puuid){
			try{
				IList<AppmenuProxyMap_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<AppmenuProxyMap_Record>()  ;  
				return ret.First();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.ErrorNoThrowException(this, ex);
				return null;
			}
		}
		//TEMPLATE TABLE 20130319048
		public AppmenuProxyMap_Record Fetch_By_Uuid(string puuid,DB db){
			try{
				IList<AppmenuProxyMap_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<AppmenuProxyMap_Record>(db)  ;  
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
				UpdateAllRecord<AppmenuProxyMap_Record>(this.AllRecord());   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來更新資料行*/
		public void UpdateAllRecord(DB db) {
			try{
				UpdateAllRecord<AppmenuProxyMap_Record>(this.AllRecord(),db);   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來新增資料行*/
		public void InsertAllRecord() {
			try{
				InsertAllRecord<AppmenuProxyMap_Record>(this.AllRecord());   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來新增資料行*/
		public void InsertAllRecord(DB db) {
			try{
				InsertAllRecord<AppmenuProxyMap_Record>(this.AllRecord(),db);   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來刪除資料行*/
		public void DeleteAllRecord() {
			try{
				DeleteAllRecord<AppmenuProxyMap_Record>(this.AllRecord());   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來刪除資料行*/
		public void DeleteAllRecord(DB db) {
			try{
				DeleteAllRecord<AppmenuProxyMap_Record>(this.AllRecord(),db);   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*依照資料表與資料表的關係，產生出來的方法*/
		/*201303180320*/
		public List<VAppmenuProxyMap_Record> Link_VAppmenuProxyMap_By_AppmenuProxyUuid()
		{
			try{
				List<VAppmenuProxyMap_Record> ret= new List<VAppmenuProxyMap_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				VAppmenuProxyMap ___table = new VAppmenuProxyMap(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.APPMENU_PROXY_UUID,item.UUID).R().Or()  ; 
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
		public List<VAppmenuProxyMap_Record> Link_VAppmenuProxyMap_By_AppmenuProxyUuid(OrderLimit limit)
		{
			try{
				List<VAppmenuProxyMap_Record> ret= new List<VAppmenuProxyMap_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				VAppmenuProxyMap ___table = new VAppmenuProxyMap(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.APPMENU_PROXY_UUID,item.UUID).R().Or()  ; 
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
		public List<Proxy_Record> Link_Proxy_By_Uuid()
		{
			try{
				List<Proxy_Record> ret= new List<Proxy_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Proxy ___table = new Proxy(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.UUID,item.PROXY_UUID).R().Or()  ; 
 				}
				condition.CheckSQL();
				ret=(List<Proxy_Record>)
						___table.Where(condition)
						.FetchAll<Proxy_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180340*/
		public List<Proxy_Record> Link_Proxy_By_Uuid(OrderLimit limit)
		{
			try{
				List<Proxy_Record> ret= new List<Proxy_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Proxy ___table = new Proxy(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.UUID,item.PROXY_UUID).R().Or()  ; 
 				}
				condition.CheckSQL();
				ret=(List<Proxy_Record>)
						___table.Where(condition)
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
		/*201303180324*/
		public VAppmenuProxyMap LinkFill_VAppmenuProxyMap_By_AppmenuProxyUuid()
		{
			try{
				var data = Link_VAppmenuProxyMap_By_AppmenuProxyUuid();
				VAppmenuProxyMap ret=new VAppmenuProxyMap(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180325*/
		public VAppmenuProxyMap LinkFill_VAppmenuProxyMap_By_AppmenuProxyUuid(OrderLimit limit)
		{
			try{
				var data = Link_VAppmenuProxyMap_By_AppmenuProxyUuid(limit);
				VAppmenuProxyMap ret=new VAppmenuProxyMap(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180336*/
		public Proxy LinkFill_Proxy_By_Uuid()
		{
			try{
				var data = Link_Proxy_By_Uuid();
				Proxy ret=new Proxy(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180337*/
		public Proxy LinkFill_Proxy_By_Uuid(OrderLimit limit)
		{
			try{
				var data = Link_Proxy_By_Uuid(limit);
				Proxy ret=new Proxy(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
	}
}
