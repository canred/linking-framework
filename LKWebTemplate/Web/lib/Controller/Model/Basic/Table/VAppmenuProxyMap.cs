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
	[TableView("V_APPMENU_PROXY_MAP", false)]
	public partial class VAppmenuProxyMap : TableBase{
	/*固定物件*/
	//LK.DB.SQLCreater.ASQLCreater sqlCreater = null;
	/*固定物件但名稱需更新*/
	private VAppmenuProxyMap_Record _currentRecord = null;
	private IList<VAppmenuProxyMap_Record> _All_Record = new List<VAppmenuProxyMap_Record>();
		/*建構子*/
		public VAppmenuProxyMap(){}
		public VAppmenuProxyMap(IDataBaseConfigInfo dbc,string db): base(dbc,db){}
		public VAppmenuProxyMap(IDataBaseConfigInfo dbc): base(dbc){}
		public VAppmenuProxyMap(IDataBaseConfigInfo dbc,VAppmenuProxyMap_Record currenData){
			this.setDataBaseConfigInfo(dbc);
			this._currentRecord = currenData;
		}
		public VAppmenuProxyMap(IList<VAppmenuProxyMap_Record> currenData){
			this._All_Record = currenData;
		}
		/*欄位資訊 Start*/
		public string PROXY_UUID {get{return "PROXY_UUID" ; }}
		public string PROXY_ACTION {get{return "PROXY_ACTION" ; }}
		public string PROXY_METHOD {get{return "PROXY_METHOD" ; }}
		public string PROXY_DESCRIPTION {get{return "PROXY_DESCRIPTION" ; }}
		public string PROXY_TYPE {get{return "PROXY_TYPE" ; }}
		public string NEED_REDIRECT {get{return "NEED_REDIRECT" ; }}
		public string REDIRECT_PROXY_ACTION {get{return "REDIRECT_PROXY_ACTION" ; }}
		public string REDIRECT_PROXY_METHOD {get{return "REDIRECT_PROXY_METHOD" ; }}
		public string REDIRECT_SRC {get{return "REDIRECT_SRC" ; }}
		public string APPLICATION_HEAD_UUID {get{return "APPLICATION_HEAD_UUID" ; }}
		public string NAME_ZH_TW {get{return "NAME_ZH_TW" ; }}
		public string NAME_ZH_CN {get{return "NAME_ZH_CN" ; }}
		public string NAME_EN_US {get{return "NAME_EN_US" ; }}
		public string UUID {get{return "UUID" ; }}
		public string APPMENU_PROXY_UUID {get{return "APPMENU_PROXY_UUID" ; }}
		public string APPMENU_UUID {get{return "APPMENU_UUID" ; }}
		/*欄位資訊 End*/
		/*固定的方法，但名稱需變更 Start*/
		public VAppmenuProxyMap_Record CurrentRecord(){
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
		public VAppmenuProxyMap_Record CreateNew(){
			try{
				VAppmenuProxyMap_Record newData = new VAppmenuProxyMap_Record();
				return newData;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		public IList<VAppmenuProxyMap_Record> AllRecord(){
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
				_All_Record = new List<VAppmenuProxyMap_Record>();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*固定的方法，但名稱需變更 End*/
		/*有關PK的方法*/
		//TEMPLATE TABLE 201303180156
		public VAppmenuProxyMap Fill_By_PK(string pproxy_uuid,string puuid,string pappmenu_proxy_uuid){
			try{
				IList<VAppmenuProxyMap_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.PROXY_UUID,pproxy_uuid)
									.And()
									.Equal(this.UUID,puuid)
									.And()
									.Equal(this.APPMENU_PROXY_UUID,pappmenu_proxy_uuid)
				).FetchAll<VAppmenuProxyMap_Record>()  ;  
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
		public VAppmenuProxyMap Fill_By_PK(string pproxy_uuid,string puuid,string pappmenu_proxy_uuid,DB db){
			try{
				IList<VAppmenuProxyMap_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.PROXY_UUID,pproxy_uuid)
									.And()
									.Equal(this.UUID,puuid)
									.And()
									.Equal(this.APPMENU_PROXY_UUID,pappmenu_proxy_uuid)
				).FetchAll<VAppmenuProxyMap_Record>(db)  ;  
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
		public VAppmenuProxyMap_Record Fetch_By_PK(string pproxy_uuid,string puuid,string pappmenu_proxy_uuid){
			try{
				IList<VAppmenuProxyMap_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.PROXY_UUID,pproxy_uuid)
									.Equal(this.UUID,puuid)
									.Equal(this.APPMENU_PROXY_UUID,pappmenu_proxy_uuid)
				).FetchAll<VAppmenuProxyMap_Record>()  ;  
				return ret.First();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		//TEMPLATE TABLE 20130319044
		public VAppmenuProxyMap_Record Fetch_By_PK(string pproxy_uuid,string puuid,string pappmenu_proxy_uuid,DB db){
			try{
				IList<VAppmenuProxyMap_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.PROXY_UUID,pproxy_uuid)
									.Equal(this.UUID,puuid)
									.Equal(this.APPMENU_PROXY_UUID,pappmenu_proxy_uuid)
				).FetchAll<VAppmenuProxyMap_Record>(db)  ;  
				return ret.First();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		//TEMPLATE TABLE 20130319045
		public VAppmenuProxyMap Fill_By_ProxyUuid_And_Uuid_And_AppmenuProxyUuid(string pproxy_uuid,string puuid,string pappmenu_proxy_uuid){
			try{
				IList<VAppmenuProxyMap_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.PROXY_UUID,pproxy_uuid)
									.Equal(this.UUID,puuid)
									.Equal(this.APPMENU_PROXY_UUID,pappmenu_proxy_uuid)
				).FetchAll<VAppmenuProxyMap_Record>()  ;  
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
		public VAppmenuProxyMap Fill_By_ProxyUuid_And_Uuid_And_AppmenuProxyUuid(string pproxy_uuid,string puuid,string pappmenu_proxy_uuid,DB db){
			try{
				IList<VAppmenuProxyMap_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.PROXY_UUID,pproxy_uuid)
									.Equal(this.UUID,puuid)
									.Equal(this.APPMENU_PROXY_UUID,pappmenu_proxy_uuid)
				).FetchAll<VAppmenuProxyMap_Record>(db)  ;  
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
		public VAppmenuProxyMap_Record Fetch_By_ProxyUuid_And_Uuid_And_AppmenuProxyUuid(string pproxy_uuid,string puuid,string pappmenu_proxy_uuid){
			try{
				IList<VAppmenuProxyMap_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.PROXY_UUID,pproxy_uuid)
									.Equal(this.UUID,puuid)
									.Equal(this.APPMENU_PROXY_UUID,pappmenu_proxy_uuid)
				).FetchAll<VAppmenuProxyMap_Record>()  ;  
				return ret.First();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.ErrorNoThrowException(this, ex);
				return null;
			}
		}
		//TEMPLATE TABLE 20130319048
		public VAppmenuProxyMap_Record Fetch_By_ProxyUuid_And_Uuid_And_AppmenuProxyUuid(string pproxy_uuid,string puuid,string pappmenu_proxy_uuid,DB db){
			try{
				IList<VAppmenuProxyMap_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.PROXY_UUID,pproxy_uuid)
									.Equal(this.UUID,puuid)
									.Equal(this.APPMENU_PROXY_UUID,pappmenu_proxy_uuid)
				).FetchAll<VAppmenuProxyMap_Record>(db)  ;  
				return ret.First();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*依照資料表與資料表的關係，產生出來的方法*/
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
		public List<AppmenuProxyMap_Record> Link_AppmenuProxyMap_By_Uuid()
		{
			try{
				List<AppmenuProxyMap_Record> ret= new List<AppmenuProxyMap_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				AppmenuProxyMap ___table = new AppmenuProxyMap(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.UUID,item.APPMENU_PROXY_UUID).R().Or()  ; 
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
		/*201303180340*/
		public List<AppmenuProxyMap_Record> Link_AppmenuProxyMap_By_Uuid(OrderLimit limit)
		{
			try{
				List<AppmenuProxyMap_Record> ret= new List<AppmenuProxyMap_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				AppmenuProxyMap ___table = new AppmenuProxyMap(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.UUID,item.APPMENU_PROXY_UUID).R().Or()  ; 
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
		/*201303180336*/
		public AppmenuProxyMap LinkFill_AppmenuProxyMap_By_Uuid()
		{
			try{
				var data = Link_AppmenuProxyMap_By_Uuid();
				AppmenuProxyMap ret=new AppmenuProxyMap(data);
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
		/*201303180337*/
		public AppmenuProxyMap LinkFill_AppmenuProxyMap_By_Uuid(OrderLimit limit)
		{
			try{
				var data = Link_AppmenuProxyMap_By_Uuid(limit);
				AppmenuProxyMap ret=new AppmenuProxyMap(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
	}
}
