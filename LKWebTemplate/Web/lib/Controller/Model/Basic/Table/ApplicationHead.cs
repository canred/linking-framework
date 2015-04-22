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
	[TableView("APPLICATION_HEAD", true)]
	public partial class ApplicationHead : TableBase{
	/*固定物件*/
	//LK.DB.SQLCreater.ASQLCreater sqlCreater = null;
	/*固定物件但名稱需更新*/
	private ApplicationHead_Record _currentRecord = null;
	private IList<ApplicationHead_Record> _All_Record = new List<ApplicationHead_Record>();
		/*建構子*/
		public ApplicationHead(){}
		public ApplicationHead(IDataBaseConfigInfo dbc,string db): base(dbc,db){}
		public ApplicationHead(IDataBaseConfigInfo dbc): base(dbc){}
		public ApplicationHead(IDataBaseConfigInfo dbc,ApplicationHead_Record currenData){
			this.setDataBaseConfigInfo(dbc);
			this._currentRecord = currenData;
		}
		public ApplicationHead(IList<ApplicationHead_Record> currenData){
			this._All_Record = currenData;
		}
		/*欄位資訊 Start*/
		public string UUID {
			[ColumnName("UUID",true,typeof(string))]
			get{return "UUID" ; }}
		public string CREATE_DATE {
			[ColumnName("CREATE_DATE",false,typeof(DateTime?))]
			get{return "CREATE_DATE" ; }}
		public string UPDATE_DATE {
			[ColumnName("UPDATE_DATE",false,typeof(DateTime?))]
			get{return "UPDATE_DATE" ; }}
		public string IS_ACTIVE {
			[ColumnName("IS_ACTIVE",false,typeof(string))]
			get{return "IS_ACTIVE" ; }}
		public string NAME {
			[ColumnName("NAME",false,typeof(string))]
			get{return "NAME" ; }}
		public string DESCRIPTION {
			[ColumnName("DESCRIPTION",false,typeof(string))]
			get{return "DESCRIPTION" ; }}
		public string ID {
			[ColumnName("ID",false,typeof(string))]
			get{return "ID" ; }}
		public string CREATE_USER {
			[ColumnName("CREATE_USER",false,typeof(string))]
			get{return "CREATE_USER" ; }}
		public string UPDATE_USER {
			[ColumnName("UPDATE_USER",false,typeof(string))]
			get{return "UPDATE_USER" ; }}
		public string WEB_SITE {
			[ColumnName("WEB_SITE",false,typeof(string))]
			get{return "WEB_SITE" ; }}
		/*欄位資訊 End*/
		/*固定的方法，但名稱需變更 Start*/
		public ApplicationHead_Record CurrentRecord(){
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
		public ApplicationHead_Record CreateNew(){
			try{
				ApplicationHead_Record newData = new ApplicationHead_Record();
				return newData;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		public IList<ApplicationHead_Record> AllRecord(){
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
				_All_Record = new List<ApplicationHead_Record>();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*固定的方法，但名稱需變更 End*/
		/*有關PK的方法*/
		//TEMPLATE TABLE 201303180156
		public ApplicationHead Fill_By_PK(string puuid){
			try{
				IList<ApplicationHead_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<ApplicationHead_Record>()  ;  
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
		public ApplicationHead Fill_By_PK(string puuid,DB db){
			try{
				IList<ApplicationHead_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<ApplicationHead_Record>(db)  ;  
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
		public ApplicationHead_Record Fetch_By_PK(string puuid){
			try{
				IList<ApplicationHead_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<ApplicationHead_Record>()  ;  
				return ret.First();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		//TEMPLATE TABLE 20130319044
		public ApplicationHead_Record Fetch_By_PK(string puuid,DB db){
			try{
				IList<ApplicationHead_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<ApplicationHead_Record>(db)  ;  
				return ret.First();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		//TEMPLATE TABLE 20130319045
		public ApplicationHead Fill_By_Uuid(string puuid){
			try{
				IList<ApplicationHead_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<ApplicationHead_Record>()  ;  
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
		public ApplicationHead Fill_By_Uuid(string puuid,DB db){
			try{
				IList<ApplicationHead_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<ApplicationHead_Record>(db)  ;  
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
		public ApplicationHead_Record Fetch_By_Uuid(string puuid){
			try{
				IList<ApplicationHead_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<ApplicationHead_Record>()  ;  
				return ret.First();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.ErrorNoThrowException(this, ex);
				return null;
			}
		}
		//TEMPLATE TABLE 20130319048
		public ApplicationHead_Record Fetch_By_Uuid(string puuid,DB db){
			try{
				IList<ApplicationHead_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<ApplicationHead_Record>(db)  ;  
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
				UpdateAllRecord<ApplicationHead_Record>(this.AllRecord());   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來更新資料行*/
		public void UpdateAllRecord(DB db) {
			try{
				UpdateAllRecord<ApplicationHead_Record>(this.AllRecord(),db);   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來新增資料行*/
		public void InsertAllRecord() {
			try{
				InsertAllRecord<ApplicationHead_Record>(this.AllRecord());   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來新增資料行*/
		public void InsertAllRecord(DB db) {
			try{
				InsertAllRecord<ApplicationHead_Record>(this.AllRecord(),db);   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來刪除資料行*/
		public void DeleteAllRecord() {
			try{
				DeleteAllRecord<ApplicationHead_Record>(this.AllRecord());   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來刪除資料行*/
		public void DeleteAllRecord(DB db) {
			try{
				DeleteAllRecord<ApplicationHead_Record>(this.AllRecord(),db);   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*依照資料表與資料表的關係，產生出來的方法*/
		/*201303180320*/
		public List<Sitemap_Record> Link_Sitemap_By_ApplicationHeadUuid()
		{
			try{
				List<Sitemap_Record> ret= new List<Sitemap_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Sitemap ___table = new Sitemap(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.APPLICATION_HEAD_UUID,item.UUID).R().Or()  ; 
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
		public List<Apppage_Record> Link_Apppage_By_ApplicationHeadUuid()
		{
			try{
				List<Apppage_Record> ret= new List<Apppage_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Apppage ___table = new Apppage(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.APPLICATION_HEAD_UUID,item.UUID).R().Or()  ; 
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
		/*201303180320*/
		public List<GroupHead_Record> Link_GroupHead_By_ApplicationHeadUuid()
		{
			try{
				List<GroupHead_Record> ret= new List<GroupHead_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				GroupHead ___table = new GroupHead(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.APPLICATION_HEAD_UUID,item.UUID).R().Or()  ; 
 				}
				condition.CheckSQL();
				ret=(List<GroupHead_Record>)
						___table.Where(condition)
						.FetchAll<GroupHead_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180320*/
		public List<GroupHeadV_Record> Link_GroupHeadV_By_ApplicationHeadUuid()
		{
			try{
				List<GroupHeadV_Record> ret= new List<GroupHeadV_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				GroupHeadV ___table = new GroupHeadV(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.APPLICATION_HEAD_UUID,item.UUID).R().Or()  ; 
 				}
				condition.CheckSQL();
				ret=(List<GroupHeadV_Record>)
						___table.Where(condition)
						.FetchAll<GroupHeadV_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180320*/
		public List<Proxy_Record> Link_Proxy_By_ApplicationHeadUuid()
		{
			try{
				List<Proxy_Record> ret= new List<Proxy_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Proxy ___table = new Proxy(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.APPLICATION_HEAD_UUID,item.UUID).R().Or()  ; 
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
		/*201303180320*/
		public List<Appmenu_Record> Link_Appmenu_By_ApplicationHeadUuid()
		{
			try{
				List<Appmenu_Record> ret= new List<Appmenu_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Appmenu ___table = new Appmenu(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.APPLICATION_HEAD_UUID,item.UUID).R().Or()  ; 
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
		public List<AppmenuApppageV_Record> Link_AppmenuApppageV_By_ApplicationHeadUuid()
		{
			try{
				List<AppmenuApppageV_Record> ret= new List<AppmenuApppageV_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				AppmenuApppageV ___table = new AppmenuApppageV(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.APPLICATION_HEAD_UUID,item.UUID).R().Or()  ; 
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
		public List<VAppmenuProxyMap_Record> Link_VAppmenuProxyMap_By_ApplicationHeadUuid()
		{
			try{
				List<VAppmenuProxyMap_Record> ret= new List<VAppmenuProxyMap_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				VAppmenuProxyMap ___table = new VAppmenuProxyMap(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.APPLICATION_HEAD_UUID,item.UUID).R().Or()  ; 
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
		public List<VAuthProxy_Record> Link_VAuthProxy_By_ApplicationHeadUuid()
		{
			try{
				List<VAuthProxy_Record> ret= new List<VAuthProxy_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				VAuthProxy ___table = new VAuthProxy(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.APPLICATION_HEAD_UUID,item.UUID).R().Or()  ; 
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
		public List<Sitemap_Record> Link_Sitemap_By_ApplicationHeadUuid(OrderLimit limit)
		{
			try{
				List<Sitemap_Record> ret= new List<Sitemap_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Sitemap ___table = new Sitemap(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.APPLICATION_HEAD_UUID,item.UUID).R().Or()  ; 
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
		public List<Apppage_Record> Link_Apppage_By_ApplicationHeadUuid(OrderLimit limit)
		{
			try{
				List<Apppage_Record> ret= new List<Apppage_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Apppage ___table = new Apppage(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.APPLICATION_HEAD_UUID,item.UUID).R().Or()  ; 
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
		/*201303180321*/
		public List<GroupHead_Record> Link_GroupHead_By_ApplicationHeadUuid(OrderLimit limit)
		{
			try{
				List<GroupHead_Record> ret= new List<GroupHead_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				GroupHead ___table = new GroupHead(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.APPLICATION_HEAD_UUID,item.UUID).R().Or()  ; 
 				}
				condition.CheckSQL();
				ret=(List<GroupHead_Record>)
						___table.Where(condition)
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
		/*201303180321*/
		public List<GroupHeadV_Record> Link_GroupHeadV_By_ApplicationHeadUuid(OrderLimit limit)
		{
			try{
				List<GroupHeadV_Record> ret= new List<GroupHeadV_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				GroupHeadV ___table = new GroupHeadV(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.APPLICATION_HEAD_UUID,item.UUID).R().Or()  ; 
 				}
				condition.CheckSQL();
				ret=(List<GroupHeadV_Record>)
						___table.Where(condition)
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
		/*201303180321*/
		public List<Proxy_Record> Link_Proxy_By_ApplicationHeadUuid(OrderLimit limit)
		{
			try{
				List<Proxy_Record> ret= new List<Proxy_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Proxy ___table = new Proxy(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.APPLICATION_HEAD_UUID,item.UUID).R().Or()  ; 
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
		/*201303180321*/
		public List<Appmenu_Record> Link_Appmenu_By_ApplicationHeadUuid(OrderLimit limit)
		{
			try{
				List<Appmenu_Record> ret= new List<Appmenu_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Appmenu ___table = new Appmenu(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.APPLICATION_HEAD_UUID,item.UUID).R().Or()  ; 
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
		public List<AppmenuApppageV_Record> Link_AppmenuApppageV_By_ApplicationHeadUuid(OrderLimit limit)
		{
			try{
				List<AppmenuApppageV_Record> ret= new List<AppmenuApppageV_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				AppmenuApppageV ___table = new AppmenuApppageV(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.APPLICATION_HEAD_UUID,item.UUID).R().Or()  ; 
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
		public List<VAppmenuProxyMap_Record> Link_VAppmenuProxyMap_By_ApplicationHeadUuid(OrderLimit limit)
		{
			try{
				List<VAppmenuProxyMap_Record> ret= new List<VAppmenuProxyMap_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				VAppmenuProxyMap ___table = new VAppmenuProxyMap(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.APPLICATION_HEAD_UUID,item.UUID).R().Or()  ; 
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
		public List<VAuthProxy_Record> Link_VAuthProxy_By_ApplicationHeadUuid(OrderLimit limit)
		{
			try{
				List<VAuthProxy_Record> ret= new List<VAuthProxy_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				VAuthProxy ___table = new VAuthProxy(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.APPLICATION_HEAD_UUID,item.UUID).R().Or()  ; 
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
		/*201303180324*/
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
		/*201303180324*/
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
		/*201303180324*/
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
		/*201303180324*/
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
		/*201303180324*/
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
		/*201303180324*/
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
		/*201303180324*/
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
		/*201303180324*/
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
		/*201303180324*/
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
		/*201303180325*/
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
		/*201303180325*/
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
		/*201303180325*/
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
		/*201303180325*/
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
		/*201303180325*/
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
		/*201303180325*/
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
		/*201303180325*/
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
		/*201303180325*/
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
		/*201303180325*/
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
