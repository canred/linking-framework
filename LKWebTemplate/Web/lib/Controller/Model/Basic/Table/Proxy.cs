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
	[TableView("PROXY", true)]
	public partial class Proxy : TableBase{
	/*固定物件*/
	//LK.DB.SQLCreater.ASQLCreater sqlCreater = null;
	/*固定物件但名稱需更新*/
	private Proxy_Record _currentRecord = null;
	private IList<Proxy_Record> _All_Record = new List<Proxy_Record>();
		/*建構子*/
		public Proxy(){}
		public Proxy(IDataBaseConfigInfo dbc,string db): base(dbc,db){}
		public Proxy(IDataBaseConfigInfo dbc): base(dbc){}
		public Proxy(IDataBaseConfigInfo dbc,Proxy_Record currenData){
			this.setDataBaseConfigInfo(dbc);
			this._currentRecord = currenData;
		}
		public Proxy(IList<Proxy_Record> currenData){
			this._All_Record = currenData;
		}
		/*欄位資訊 Start*/
		public string UUID {get{return "UUID" ; }}
		public string PROXY_ACTION {get{return "PROXY_ACTION" ; }}
		public string PROXY_METHOD {get{return "PROXY_METHOD" ; }}
		public string DESCRIPTION {get{return "DESCRIPTION" ; }}
		public string PROXY_TYPE {get{return "PROXY_TYPE" ; }}
		public string NEED_REDIRECT {get{return "NEED_REDIRECT" ; }}
		public string REDIRECT_PROXY_ACTION {get{return "REDIRECT_PROXY_ACTION" ; }}
		public string REDIRECT_PROXY_METHOD {get{return "REDIRECT_PROXY_METHOD" ; }}
		public string APPLICATION_HEAD_UUID {get{return "APPLICATION_HEAD_UUID" ; }}
		public string REDIRECT_SRC {get{return "REDIRECT_SRC" ; }}
		/*欄位資訊 End*/
		/*固定的方法，但名稱需變更 Start*/
		public Proxy_Record CurrentRecord(){
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
		public Proxy_Record CreateNew(){
			try{
				Proxy_Record newData = new Proxy_Record();
				return newData;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		public IList<Proxy_Record> AllRecord(){
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
				_All_Record = new List<Proxy_Record>();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*固定的方法，但名稱需變更 End*/
		/*有關PK的方法*/
		//TEMPLATE TABLE 201303180156
		public Proxy Fill_By_PK(string pUUID){
			try{
				IList<Proxy_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,pUUID)
				).FetchAll<Proxy_Record>()  ;  
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
		public Proxy Fill_By_PK(string pUUID,DB db){
			try{
				IList<Proxy_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,pUUID)
				).FetchAll<Proxy_Record>(db)  ;  
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
		public Proxy_Record Fetch_By_PK(string pUUID){
			try{
				IList<Proxy_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,pUUID)
				).FetchAll<Proxy_Record>()  ;  
				return ret.First();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		//TEMPLATE TABLE 20130319044
		public Proxy_Record Fetch_By_PK(string pUUID,DB db){
			try{
				IList<Proxy_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,pUUID)
				).FetchAll<Proxy_Record>(db)  ;  
				return ret.First();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		//TEMPLATE TABLE 20130319045
		public Proxy Fill_By_Uuid(string pUUID){
			try{
				IList<Proxy_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,pUUID)
				).FetchAll<Proxy_Record>()  ;  
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
		public Proxy Fill_By_Uuid(string pUUID,DB db){
			try{
				IList<Proxy_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,pUUID)
				).FetchAll<Proxy_Record>(db)  ;  
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
		public Proxy_Record Fetch_By_Uuid(string pUUID){
			try{
				IList<Proxy_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,pUUID)
				).FetchAll<Proxy_Record>()  ;  
				return ret.First();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.ErrorNoThrowException(this, ex);
				return null;
			}
		}
		//TEMPLATE TABLE 20130319048
		public Proxy_Record Fetch_By_Uuid(string pUUID,DB db){
			try{
				IList<Proxy_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,pUUID)
				).FetchAll<Proxy_Record>(db)  ;  
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
				UpdateAllRecord<Proxy_Record>(this.AllRecord());   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來更新資料行*/
		public void UpdateAllRecord(DB db) {
			try{
				UpdateAllRecord<Proxy_Record>(this.AllRecord(),db);   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來新增資料行*/
		public void InsertAllRecord() {
			try{
				InsertAllRecord<Proxy_Record>(this.AllRecord());   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來新增資料行*/
		public void InsertAllRecord(DB db) {
			try{
				InsertAllRecord<Proxy_Record>(this.AllRecord(),db);   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來刪除資料行*/
		public void DeleteAllRecord() {
			try{
				DeleteAllRecord<Proxy_Record>(this.AllRecord());   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來刪除資料行*/
		public void DeleteAllRecord(DB db) {
			try{
				DeleteAllRecord<Proxy_Record>(this.AllRecord(),db);   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*依照資料表與資料表的關係，產生出來的方法*/
		/*201303180320*/
		public List<AppmenuProxyMap_Record> Link_AppmenuProxyMap_By_ProxyUuid()
		{
			try{
				List<AppmenuProxyMap_Record> ret= new List<AppmenuProxyMap_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				AppmenuProxyMap ___table = new AppmenuProxyMap(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.PROXY_UUID,item.UUID).R().Or()  ; 
 				}
				condition.CheckSQL();
				ret=(List<AppmenuProxyMap_Record>)
						___table.Where(condition)
						.FetchAll<AppmenuProxyMap_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180320*/
		public List<VAppmenuProxyMap_Record> Link_VAppmenuProxyMap_By_ProxyUuid()
		{
			try{
				List<VAppmenuProxyMap_Record> ret= new List<VAppmenuProxyMap_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				VAppmenuProxyMap ___table = new VAppmenuProxyMap(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.PROXY_UUID,item.UUID).R().Or()  ; 
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
		/*201303180320*/
		public List<VAuthProxy_Record> Link_VAuthProxy_By_ProxyUuid()
		{
			try{
				List<VAuthProxy_Record> ret= new List<VAuthProxy_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				VAuthProxy ___table = new VAuthProxy(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.PROXY_UUID,item.UUID).R().Or()  ; 
 				}
				condition.CheckSQL();
				ret=(List<VAuthProxy_Record>)
						___table.Where(condition)
						.FetchAll<VAuthProxy_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180321*/
		public List<AppmenuProxyMap_Record> Link_AppmenuProxyMap_By_ProxyUuid(OrderLimit limit)
		{
			try{
				List<AppmenuProxyMap_Record> ret= new List<AppmenuProxyMap_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				AppmenuProxyMap ___table = new AppmenuProxyMap(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.PROXY_UUID,item.UUID).R().Or()  ; 
 				}
				condition.CheckSQL();
				ret=(List<AppmenuProxyMap_Record>)
						___table.Where(condition)
						.Order(limit)
						.Limit(limit)
						.FetchAll<AppmenuProxyMap_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180321*/
		public List<VAppmenuProxyMap_Record> Link_VAppmenuProxyMap_By_ProxyUuid(OrderLimit limit)
		{
			try{
				List<VAppmenuProxyMap_Record> ret= new List<VAppmenuProxyMap_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				VAppmenuProxyMap ___table = new VAppmenuProxyMap(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.PROXY_UUID,item.UUID).R().Or()  ; 
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
		/*201303180321*/
		public List<VAuthProxy_Record> Link_VAuthProxy_By_ProxyUuid(OrderLimit limit)
		{
			try{
				List<VAuthProxy_Record> ret= new List<VAuthProxy_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				VAuthProxy ___table = new VAuthProxy(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.PROXY_UUID,item.UUID).R().Or()  ; 
 				}
				condition.CheckSQL();
				ret=(List<VAuthProxy_Record>)
						___table.Where(condition)
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
		/*201303180324*/
		public AppmenuProxyMap LinkFill_AppmenuProxyMap_By_ProxyUuid()
		{
			try{
				var data = Link_AppmenuProxyMap_By_ProxyUuid();
				AppmenuProxyMap ret=new AppmenuProxyMap(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180324*/
		public VAppmenuProxyMap LinkFill_VAppmenuProxyMap_By_ProxyUuid()
		{
			try{
				var data = Link_VAppmenuProxyMap_By_ProxyUuid();
				VAppmenuProxyMap ret=new VAppmenuProxyMap(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180324*/
		public VAuthProxy LinkFill_VAuthProxy_By_ProxyUuid()
		{
			try{
				var data = Link_VAuthProxy_By_ProxyUuid();
				VAuthProxy ret=new VAuthProxy(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180325*/
		public AppmenuProxyMap LinkFill_AppmenuProxyMap_By_ProxyUuid(OrderLimit limit)
		{
			try{
				var data = Link_AppmenuProxyMap_By_ProxyUuid(limit);
				AppmenuProxyMap ret=new AppmenuProxyMap(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180325*/
		public VAppmenuProxyMap LinkFill_VAppmenuProxyMap_By_ProxyUuid(OrderLimit limit)
		{
			try{
				var data = Link_VAppmenuProxyMap_By_ProxyUuid(limit);
				VAppmenuProxyMap ret=new VAppmenuProxyMap(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180325*/
		public VAuthProxy LinkFill_VAuthProxy_By_ProxyUuid(OrderLimit limit)
		{
			try{
				var data = Link_VAuthProxy_By_ProxyUuid(limit);
				VAuthProxy ret=new VAuthProxy(data);
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
	}
}
