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
	[TableView("APPPAGE", true)]
	public partial class Apppage : TableBase{
	/*固定物件*/
	//LK.DB.SQLCreater.ASQLCreater sqlCreater = null;
	/*固定物件但名稱需更新*/
	private Apppage_Record _currentRecord = null;
	private IList<Apppage_Record> _All_Record = new List<Apppage_Record>();
		/*建構子*/
		public Apppage(){}
		public Apppage(IDataBaseConfigInfo dbc,string db): base(dbc,db){}
		public Apppage(IDataBaseConfigInfo dbc): base(dbc){}
		public Apppage(IDataBaseConfigInfo dbc,Apppage_Record currenData){
			this.setDataBaseConfigInfo(dbc);
			this._currentRecord = currenData;
		}
		public Apppage(IList<Apppage_Record> currenData){
			this._All_Record = currenData;
		}
		/*欄位資訊 Start*/
		public string UUID {get{return "UUID" ; }}
		public string IS_ACTIVE {get{return "IS_ACTIVE" ; }}
		public string CREATE_DATE {get{return "CREATE_DATE" ; }}
		public string CREATE_USER {get{return "CREATE_USER" ; }}
		public string UPDATE_DATE {get{return "UPDATE_DATE" ; }}
		public string UPDATE_USER {get{return "UPDATE_USER" ; }}
		public string ID {get{return "ID" ; }}
		public string NAME {get{return "NAME" ; }}
		public string DESCRIPTION {get{return "DESCRIPTION" ; }}
		public string URL {get{return "URL" ; }}
		public string PARAMETER_CLASS {get{return "PARAMETER_CLASS" ; }}
		public string APPLICATION_HEAD_UUID {get{return "APPLICATION_HEAD_UUID" ; }}
		public string P_MODE {get{return "P_MODE" ; }}
		public string RUNJSFUNCTION {get{return "RUNJSFUNCTION" ; }}
		/*欄位資訊 End*/
		/*固定的方法，但名稱需變更 Start*/
		public Apppage_Record CurrentRecord(){
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
		public Apppage_Record CreateNew(){
			try{
				Apppage_Record newData = new Apppage_Record();
				return newData;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		public IList<Apppage_Record> AllRecord(){
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
				_All_Record = new List<Apppage_Record>();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*固定的方法，但名稱需變更 End*/
		/*有關PK的方法*/
		//TEMPLATE TABLE 201303180156
		public Apppage Fill_By_PK(string puuid){
			try{
				IList<Apppage_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<Apppage_Record>()  ;  
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
		public Apppage Fill_By_PK(string puuid,DB db){
			try{
				IList<Apppage_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<Apppage_Record>(db)  ;  
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
		public Apppage_Record Fetch_By_PK(string puuid){
			try{
				IList<Apppage_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<Apppage_Record>()  ;  
				return ret.First();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		//TEMPLATE TABLE 20130319044
		public Apppage_Record Fetch_By_PK(string puuid,DB db){
			try{
				IList<Apppage_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<Apppage_Record>(db)  ;  
				return ret.First();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		//TEMPLATE TABLE 20130319045
		public Apppage Fill_By_Uuid(string puuid){
			try{
				IList<Apppage_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<Apppage_Record>()  ;  
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
		public Apppage Fill_By_Uuid(string puuid,DB db){
			try{
				IList<Apppage_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<Apppage_Record>(db)  ;  
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
		public Apppage_Record Fetch_By_Uuid(string puuid){
			try{
				IList<Apppage_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<Apppage_Record>()  ;  
				return ret.First();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.ErrorNoThrowException(this, ex);
				return null;
			}
		}
		//TEMPLATE TABLE 20130319048
		public Apppage_Record Fetch_By_Uuid(string puuid,DB db){
			try{
				IList<Apppage_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<Apppage_Record>(db)  ;  
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
				UpdateAllRecord<Apppage_Record>(this.AllRecord());   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來更新資料行*/
		public void UpdateAllRecord(DB db) {
			try{
				UpdateAllRecord<Apppage_Record>(this.AllRecord(),db);   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來新增資料行*/
		public void InsertAllRecord() {
			try{
				InsertAllRecord<Apppage_Record>(this.AllRecord());   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來新增資料行*/
		public void InsertAllRecord(DB db) {
			try{
				InsertAllRecord<Apppage_Record>(this.AllRecord(),db);   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來刪除資料行*/
		public void DeleteAllRecord() {
			try{
				DeleteAllRecord<Apppage_Record>(this.AllRecord());   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來刪除資料行*/
		public void DeleteAllRecord(DB db) {
			try{
				DeleteAllRecord<Apppage_Record>(this.AllRecord(),db);   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*依照資料表與資料表的關係，產生出來的方法*/
		/*201303180320*/
		public List<Sitemap_Record> Link_Sitemap_By_ApppageUuid()
		{
			try{
				List<Sitemap_Record> ret= new List<Sitemap_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Sitemap ___table = new Sitemap(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.APPPAGE_UUID,item.UUID).R().Or()  ; 
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
		/*201303180321*/
		public List<Sitemap_Record> Link_Sitemap_By_ApppageUuid(OrderLimit limit)
		{
			try{
				List<Sitemap_Record> ret= new List<Sitemap_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Sitemap ___table = new Sitemap(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.APPPAGE_UUID,item.UUID).R().Or()  ; 
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
		public Sitemap LinkFill_Sitemap_By_ApppageUuid()
		{
			try{
				var data = Link_Sitemap_By_ApppageUuid();
				Sitemap ret=new Sitemap(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180325*/
		public Sitemap LinkFill_Sitemap_By_ApppageUuid(OrderLimit limit)
		{
			try{
				var data = Link_Sitemap_By_ApppageUuid(limit);
				Sitemap ret=new Sitemap(data);
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
