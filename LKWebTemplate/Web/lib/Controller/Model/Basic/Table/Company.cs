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
	[TableView("COMPANY", true)]
	public partial class Company : TableBase{
	/*固定物件*/
	//LK.DB.SQLCreater.ASQLCreater sqlCreater = null;
	/*固定物件但名稱需更新*/
	private Company_Record _currentRecord = null;
	private IList<Company_Record> _All_Record = new List<Company_Record>();
		/*建構子*/
		public Company(){}
		public Company(IDataBaseConfigInfo dbc,string db): base(dbc,db){}
		public Company(IDataBaseConfigInfo dbc): base(dbc){}
		public Company(IDataBaseConfigInfo dbc,Company_Record currenData){
			this.setDataBaseConfigInfo(dbc);
			this._currentRecord = currenData;
		}
		public Company(IList<Company_Record> currenData){
			this._All_Record = currenData;
		}
		/*欄位資訊 Start*/
		public string UUID {get{return "UUID" ; }}
		public string CREATE_DATE {get{return "CREATE_DATE" ; }}
		public string UPDATE_DATE {get{return "UPDATE_DATE" ; }}
		public string IS_ACTIVE {get{return "IS_ACTIVE" ; }}
		public string CLASS {get{return "CLASS" ; }}
		public string ID {get{return "ID" ; }}
		public string C_NAME {get{return "C_NAME" ; }}
		public string E_NAME {get{return "E_NAME" ; }}
		public string VOUCHER_POINT_UUID {get{return "VOUCHER_POINT_UUID" ; }}
		public string WEEK_SHIFT {get{return "WEEK_SHIFT" ; }}
		public string OU_SYNC_TYPE {get{return "OU_SYNC_TYPE" ; }}
		public string NAME_ZH_CN {get{return "NAME_ZH_CN" ; }}
		public string CONCURRENT_USER {get{return "CONCURRENT_USER" ; }}
		public string EXPIRED_DATE {get{return "EXPIRED_DATE" ; }}
		public string SALES_ATTENDANT_UUID {get{return "SALES_ATTENDANT_UUID" ; }}
		public string IS_SYNC_AD_USER {get{return "IS_SYNC_AD_USER" ; }}
		public string AD_LDAP {get{return "AD_LDAP" ; }}
		public string AD_LDAP_USER {get{return "AD_LDAP_USER" ; }}
		public string AD_LDAP_USER_PASSWORD {get{return "AD_LDAP_USER_PASSWORD" ; }}
		/*欄位資訊 End*/
		/*固定的方法，但名稱需變更 Start*/
		public Company_Record CurrentRecord(){
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
		public Company_Record CreateNew(){
			try{
				Company_Record newData = new Company_Record();
				return newData;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		public IList<Company_Record> AllRecord(){
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
				_All_Record = new List<Company_Record>();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*固定的方法，但名稱需變更 End*/
		/*有關PK的方法*/
		//TEMPLATE TABLE 201303180156
		public Company Fill_By_PK(string puuid){
			try{
				IList<Company_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<Company_Record>()  ;  
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
		public Company Fill_By_PK(string puuid,DB db){
			try{
				IList<Company_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<Company_Record>(db)  ;  
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
		public Company_Record Fetch_By_PK(string puuid){
			try{
				IList<Company_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<Company_Record>()  ;  
				return ret.First();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		//TEMPLATE TABLE 20130319044
		public Company_Record Fetch_By_PK(string puuid,DB db){
			try{
				IList<Company_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<Company_Record>(db)  ;  
				return ret.First();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		//TEMPLATE TABLE 20130319045
		public Company Fill_By_Uuid(string puuid){
			try{
				IList<Company_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<Company_Record>()  ;  
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
		public Company Fill_By_Uuid(string puuid,DB db){
			try{
				IList<Company_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<Company_Record>(db)  ;  
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
		public Company_Record Fetch_By_Uuid(string puuid){
			try{
				IList<Company_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<Company_Record>()  ;  
				return ret.First();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.ErrorNoThrowException(this, ex);
				return null;
			}
		}
		//TEMPLATE TABLE 20130319048
		public Company_Record Fetch_By_Uuid(string puuid,DB db){
			try{
				IList<Company_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<Company_Record>(db)  ;  
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
				UpdateAllRecord<Company_Record>(this.AllRecord());   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來更新資料行*/
		public void UpdateAllRecord(DB db) {
			try{
				UpdateAllRecord<Company_Record>(this.AllRecord(),db);   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來新增資料行*/
		public void InsertAllRecord() {
			try{
				InsertAllRecord<Company_Record>(this.AllRecord());   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來新增資料行*/
		public void InsertAllRecord(DB db) {
			try{
				InsertAllRecord<Company_Record>(this.AllRecord(),db);   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來刪除資料行*/
		public void DeleteAllRecord() {
			try{
				DeleteAllRecord<Company_Record>(this.AllRecord());   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來刪除資料行*/
		public void DeleteAllRecord(DB db) {
			try{
				DeleteAllRecord<Company_Record>(this.AllRecord(),db);   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*依照資料表與資料表的關係，產生出來的方法*/
		/*201303180320*/
		public List<Attendant_Record> Link_Attendant_By_CompanyUuid()
		{
			try{
				List<Attendant_Record> ret= new List<Attendant_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Attendant ___table = new Attendant(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.COMPANY_UUID,item.UUID).R().Or()  ; 
 				}
				condition.CheckSQL();
				ret=(List<Attendant_Record>)
						___table.Where(condition)
						.FetchAll<Attendant_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180320*/
		public List<Department_Record> Link_Department_By_CompanyUuid()
		{
			try{
				List<Department_Record> ret= new List<Department_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Department ___table = new Department(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.COMPANY_UUID,item.UUID).R().Or()  ; 
 				}
				condition.CheckSQL();
				ret=(List<Department_Record>)
						___table.Where(condition)
						.FetchAll<Department_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180320*/
		public List<GroupHead_Record> Link_GroupHead_By_CompanyUuid()
		{
			try{
				List<GroupHead_Record> ret= new List<GroupHead_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				GroupHead ___table = new GroupHead(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.COMPANY_UUID,item.UUID).R().Or()  ; 
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
		public List<Site_Record> Link_Site_By_CompanyUuid()
		{
			try{
				List<Site_Record> ret= new List<Site_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Site ___table = new Site(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.COMPANY_UUID,item.UUID).R().Or()  ; 
 				}
				condition.CheckSQL();
				ret=(List<Site_Record>)
						___table.Where(condition)
						.FetchAll<Site_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180320*/
		public List<GroupAttendantV_Record> Link_GroupAttendantV_By_CompanyUuid()
		{
			try{
				List<GroupAttendantV_Record> ret= new List<GroupAttendantV_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				GroupAttendantV ___table = new GroupAttendantV(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.COMPANY_UUID,item.UUID).R().Or()  ; 
 				}
				condition.CheckSQL();
				ret=(List<GroupAttendantV_Record>)
						___table.Where(condition)
						.FetchAll<GroupAttendantV_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180320*/
		public List<GroupHeadV_Record> Link_GroupHeadV_By_CompanyUuid()
		{
			try{
				List<GroupHeadV_Record> ret= new List<GroupHeadV_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				GroupHeadV ___table = new GroupHeadV(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.COMPANY_UUID,item.UUID).R().Or()  ; 
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
		/*201303180321*/
		public List<Attendant_Record> Link_Attendant_By_CompanyUuid(OrderLimit limit)
		{
			try{
				List<Attendant_Record> ret= new List<Attendant_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Attendant ___table = new Attendant(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.COMPANY_UUID,item.UUID).R().Or()  ; 
 				}
				condition.CheckSQL();
				ret=(List<Attendant_Record>)
						___table.Where(condition)
						.Order(limit)
						.Limit(limit)
						.FetchAll<Attendant_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180321*/
		public List<Department_Record> Link_Department_By_CompanyUuid(OrderLimit limit)
		{
			try{
				List<Department_Record> ret= new List<Department_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Department ___table = new Department(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.COMPANY_UUID,item.UUID).R().Or()  ; 
 				}
				condition.CheckSQL();
				ret=(List<Department_Record>)
						___table.Where(condition)
						.Order(limit)
						.Limit(limit)
						.FetchAll<Department_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180321*/
		public List<GroupHead_Record> Link_GroupHead_By_CompanyUuid(OrderLimit limit)
		{
			try{
				List<GroupHead_Record> ret= new List<GroupHead_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				GroupHead ___table = new GroupHead(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.COMPANY_UUID,item.UUID).R().Or()  ; 
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
		public List<Site_Record> Link_Site_By_CompanyUuid(OrderLimit limit)
		{
			try{
				List<Site_Record> ret= new List<Site_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Site ___table = new Site(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.COMPANY_UUID,item.UUID).R().Or()  ; 
 				}
				condition.CheckSQL();
				ret=(List<Site_Record>)
						___table.Where(condition)
						.Order(limit)
						.Limit(limit)
						.FetchAll<Site_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180321*/
		public List<GroupAttendantV_Record> Link_GroupAttendantV_By_CompanyUuid(OrderLimit limit)
		{
			try{
				List<GroupAttendantV_Record> ret= new List<GroupAttendantV_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				GroupAttendantV ___table = new GroupAttendantV(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.COMPANY_UUID,item.UUID).R().Or()  ; 
 				}
				condition.CheckSQL();
				ret=(List<GroupAttendantV_Record>)
						___table.Where(condition)
						.Order(limit)
						.Limit(limit)
						.FetchAll<GroupAttendantV_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180321*/
		public List<GroupHeadV_Record> Link_GroupHeadV_By_CompanyUuid(OrderLimit limit)
		{
			try{
				List<GroupHeadV_Record> ret= new List<GroupHeadV_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				GroupHeadV ___table = new GroupHeadV(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.COMPANY_UUID,item.UUID).R().Or()  ; 
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
		/*201303180324*/
		public Attendant LinkFill_Attendant_By_CompanyUuid()
		{
			try{
				var data = Link_Attendant_By_CompanyUuid();
				Attendant ret=new Attendant(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180324*/
		public Department LinkFill_Department_By_CompanyUuid()
		{
			try{
				var data = Link_Department_By_CompanyUuid();
				Department ret=new Department(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180324*/
		public GroupHead LinkFill_GroupHead_By_CompanyUuid()
		{
			try{
				var data = Link_GroupHead_By_CompanyUuid();
				GroupHead ret=new GroupHead(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180324*/
		public Site LinkFill_Site_By_CompanyUuid()
		{
			try{
				var data = Link_Site_By_CompanyUuid();
				Site ret=new Site(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180324*/
		public GroupAttendantV LinkFill_GroupAttendantV_By_CompanyUuid()
		{
			try{
				var data = Link_GroupAttendantV_By_CompanyUuid();
				GroupAttendantV ret=new GroupAttendantV(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180324*/
		public GroupHeadV LinkFill_GroupHeadV_By_CompanyUuid()
		{
			try{
				var data = Link_GroupHeadV_By_CompanyUuid();
				GroupHeadV ret=new GroupHeadV(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180325*/
		public Attendant LinkFill_Attendant_By_CompanyUuid(OrderLimit limit)
		{
			try{
				var data = Link_Attendant_By_CompanyUuid(limit);
				Attendant ret=new Attendant(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180325*/
		public Department LinkFill_Department_By_CompanyUuid(OrderLimit limit)
		{
			try{
				var data = Link_Department_By_CompanyUuid(limit);
				Department ret=new Department(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180325*/
		public GroupHead LinkFill_GroupHead_By_CompanyUuid(OrderLimit limit)
		{
			try{
				var data = Link_GroupHead_By_CompanyUuid(limit);
				GroupHead ret=new GroupHead(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180325*/
		public Site LinkFill_Site_By_CompanyUuid(OrderLimit limit)
		{
			try{
				var data = Link_Site_By_CompanyUuid(limit);
				Site ret=new Site(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180325*/
		public GroupAttendantV LinkFill_GroupAttendantV_By_CompanyUuid(OrderLimit limit)
		{
			try{
				var data = Link_GroupAttendantV_By_CompanyUuid(limit);
				GroupAttendantV ret=new GroupAttendantV(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180325*/
		public GroupHeadV LinkFill_GroupHeadV_By_CompanyUuid(OrderLimit limit)
		{
			try{
				var data = Link_GroupHeadV_By_CompanyUuid(limit);
				GroupHeadV ret=new GroupHeadV(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
	}
}
