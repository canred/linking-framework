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
	[TableView("ATTENDANT", true)]
	public partial class Attendant : TableBase{
	/*固定物件*/
	//LK.DB.SQLCreater.ASQLCreater sqlCreater = null;
	/*固定物件但名稱需更新*/
	private Attendant_Record _currentRecord = null;
	private IList<Attendant_Record> _All_Record = new List<Attendant_Record>();
		/*建構子*/
		public Attendant(){}
		public Attendant(IDataBaseConfigInfo dbc,string db): base(dbc,db){}
		public Attendant(IDataBaseConfigInfo dbc): base(dbc){}
		public Attendant(IDataBaseConfigInfo dbc,Attendant_Record currenData){
			this.setDataBaseConfigInfo(dbc);
			this._currentRecord = currenData;
		}
		public Attendant(IList<Attendant_Record> currenData){
			this._All_Record = currenData;
		}
		/*欄位資訊 Start*/
		public string UUID {get{return "UUID" ; }}
		public string CREATE_DATE {get{return "CREATE_DATE" ; }}
		public string UPDATE_DATE {get{return "UPDATE_DATE" ; }}
		public string IS_ACTIVE {get{return "IS_ACTIVE" ; }}
		public string COMPANY_UUID {get{return "COMPANY_UUID" ; }}
		public string ACCOUNT {get{return "ACCOUNT" ; }}
		public string C_NAME {get{return "C_NAME" ; }}
		public string E_NAME {get{return "E_NAME" ; }}
		public string EMAIL {get{return "EMAIL" ; }}
		public string PASSWORD {get{return "PASSWORD" ; }}
		public string IS_SUPPER {get{return "IS_SUPPER" ; }}
		public string IS_ADMIN {get{return "IS_ADMIN" ; }}
		public string CODE_PAGE {get{return "CODE_PAGE" ; }}
		public string DEPARTMENT_UUID {get{return "DEPARTMENT_UUID" ; }}
		public string PHONE {get{return "PHONE" ; }}
		public string SITE_UUID {get{return "SITE_UUID" ; }}
		public string GENDER {get{return "GENDER" ; }}
		public string BIRTHDAY {get{return "BIRTHDAY" ; }}
		public string HIRE_DATE {get{return "HIRE_DATE" ; }}
		public string QUIT_DATE {get{return "QUIT_DATE" ; }}
		public string IS_MANAGER {get{return "IS_MANAGER" ; }}
		public string IS_DIRECT {get{return "IS_DIRECT" ; }}
		public string GRADE {get{return "GRADE" ; }}
		public string ID {get{return "ID" ; }}
		public string SRC_UUID {get{return "SRC_UUID" ; }}
		public string IS_DEFAULT_PASS {get{return "IS_DEFAULT_PASS" ; }}
		public string PICTURE_URL {get{return "PICTURE_URL" ; }}
		/*欄位資訊 End*/
		/*固定的方法，但名稱需變更 Start*/
		public Attendant_Record CurrentRecord(){
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
		public Attendant_Record CreateNew(){
			try{
				Attendant_Record newData = new Attendant_Record();
				return newData;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		public IList<Attendant_Record> AllRecord(){
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
				_All_Record = new List<Attendant_Record>();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*固定的方法，但名稱需變更 End*/
		/*有關PK的方法*/
		//TEMPLATE TABLE 201303180156
		public Attendant Fill_By_PK(string puuid){
			try{
				IList<Attendant_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<Attendant_Record>()  ;  
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
		public Attendant Fill_By_PK(string puuid,DB db){
			try{
				IList<Attendant_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<Attendant_Record>(db)  ;  
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
		public Attendant_Record Fetch_By_PK(string puuid){
			try{
				IList<Attendant_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<Attendant_Record>()  ;  
				return ret.First();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		//TEMPLATE TABLE 20130319044
		public Attendant_Record Fetch_By_PK(string puuid,DB db){
			try{
				IList<Attendant_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<Attendant_Record>(db)  ;  
				return ret.First();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		//TEMPLATE TABLE 20130319045
		public Attendant Fill_By_Uuid(string puuid){
			try{
				IList<Attendant_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<Attendant_Record>()  ;  
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
		public Attendant Fill_By_Uuid(string puuid,DB db){
			try{
				IList<Attendant_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<Attendant_Record>(db)  ;  
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
		public Attendant_Record Fetch_By_Uuid(string puuid){
			try{
				IList<Attendant_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<Attendant_Record>()  ;  
				return ret.First();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.ErrorNoThrowException(this, ex);
				return null;
			}
		}
		//TEMPLATE TABLE 20130319048
		public Attendant_Record Fetch_By_Uuid(string puuid,DB db){
			try{
				IList<Attendant_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<Attendant_Record>(db)  ;  
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
				UpdateAllRecord<Attendant_Record>(this.AllRecord());   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來更新資料行*/
		public void UpdateAllRecord(DB db) {
			try{
				UpdateAllRecord<Attendant_Record>(this.AllRecord(),db);   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來新增資料行*/
		public void InsertAllRecord() {
			try{
				InsertAllRecord<Attendant_Record>(this.AllRecord());   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來新增資料行*/
		public void InsertAllRecord(DB db) {
			try{
				InsertAllRecord<Attendant_Record>(this.AllRecord(),db);   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來刪除資料行*/
		public void DeleteAllRecord() {
			try{
				DeleteAllRecord<Attendant_Record>(this.AllRecord());   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來刪除資料行*/
		public void DeleteAllRecord(DB db) {
			try{
				DeleteAllRecord<Attendant_Record>(this.AllRecord(),db);   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*依照資料表與資料表的關係，產生出來的方法*/
		/*201303180320*/
		public List<ErrorLog_Record> Link_ErrorLog_By_AttendantUuid()
		{
			try{
				List<ErrorLog_Record> ret= new List<ErrorLog_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				ErrorLog ___table = new ErrorLog(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.ATTENDANT_UUID,item.UUID).R().Or()  ; 
 				}
				condition.CheckSQL();
				ret=(List<ErrorLog_Record>)
						___table.Where(condition)
						.FetchAll<ErrorLog_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180320*/
		public List<ErrorLogV_Record> Link_ErrorLogV_By_AttendantUuid()
		{
			try{
				List<ErrorLogV_Record> ret= new List<ErrorLogV_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				ErrorLogV ___table = new ErrorLogV(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.ATTENDANT_UUID,item.UUID).R().Or()  ; 
 				}
				condition.CheckSQL();
				ret=(List<ErrorLogV_Record>)
						___table.Where(condition)
						.FetchAll<ErrorLogV_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180320*/
		public List<GroupAttendant_Record> Link_GroupAttendant_By_AttendantUuid()
		{
			try{
				List<GroupAttendant_Record> ret= new List<GroupAttendant_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				GroupAttendant ___table = new GroupAttendant(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.ATTENDANT_UUID,item.UUID).R().Or()  ; 
 				}
				condition.CheckSQL();
				ret=(List<GroupAttendant_Record>)
						___table.Where(condition)
						.FetchAll<GroupAttendant_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180320*/
		public List<Department_Record> Link_Department_By_ManagerUuid()
		{
			try{
				List<Department_Record> ret= new List<Department_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Department ___table = new Department(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.MANAGER_UUID,item.UUID).R().Or()  ; 
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
		public List<GroupAttendantV_Record> Link_GroupAttendantV_By_AttendantUuid()
		{
			try{
				List<GroupAttendantV_Record> ret= new List<GroupAttendantV_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				GroupAttendantV ___table = new GroupAttendantV(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.ATTENDANT_UUID,item.UUID).R().Or()  ; 
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
		public List<VAuthProxy_Record> Link_VAuthProxy_By_AttendantUuid()
		{
			try{
				List<VAuthProxy_Record> ret= new List<VAuthProxy_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				VAuthProxy ___table = new VAuthProxy(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.ATTENDANT_UUID,item.UUID).R().Or()  ; 
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
		public List<ErrorLog_Record> Link_ErrorLog_By_AttendantUuid(OrderLimit limit)
		{
			try{
				List<ErrorLog_Record> ret= new List<ErrorLog_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				ErrorLog ___table = new ErrorLog(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.ATTENDANT_UUID,item.UUID).R().Or()  ; 
 				}
				condition.CheckSQL();
				ret=(List<ErrorLog_Record>)
						___table.Where(condition)
						.Order(limit)
						.Limit(limit)
						.FetchAll<ErrorLog_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180321*/
		public List<ErrorLogV_Record> Link_ErrorLogV_By_AttendantUuid(OrderLimit limit)
		{
			try{
				List<ErrorLogV_Record> ret= new List<ErrorLogV_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				ErrorLogV ___table = new ErrorLogV(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.ATTENDANT_UUID,item.UUID).R().Or()  ; 
 				}
				condition.CheckSQL();
				ret=(List<ErrorLogV_Record>)
						___table.Where(condition)
						.Order(limit)
						.Limit(limit)
						.FetchAll<ErrorLogV_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180321*/
		public List<GroupAttendant_Record> Link_GroupAttendant_By_AttendantUuid(OrderLimit limit)
		{
			try{
				List<GroupAttendant_Record> ret= new List<GroupAttendant_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				GroupAttendant ___table = new GroupAttendant(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.ATTENDANT_UUID,item.UUID).R().Or()  ; 
 				}
				condition.CheckSQL();
				ret=(List<GroupAttendant_Record>)
						___table.Where(condition)
						.Order(limit)
						.Limit(limit)
						.FetchAll<GroupAttendant_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180321*/
		public List<Department_Record> Link_Department_By_ManagerUuid(OrderLimit limit)
		{
			try{
				List<Department_Record> ret= new List<Department_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Department ___table = new Department(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.MANAGER_UUID,item.UUID).R().Or()  ; 
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
		public List<GroupAttendantV_Record> Link_GroupAttendantV_By_AttendantUuid(OrderLimit limit)
		{
			try{
				List<GroupAttendantV_Record> ret= new List<GroupAttendantV_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				GroupAttendantV ___table = new GroupAttendantV(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.ATTENDANT_UUID,item.UUID).R().Or()  ; 
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
		public List<VAuthProxy_Record> Link_VAuthProxy_By_AttendantUuid(OrderLimit limit)
		{
			try{
				List<VAuthProxy_Record> ret= new List<VAuthProxy_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				VAuthProxy ___table = new VAuthProxy(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.ATTENDANT_UUID,item.UUID).R().Or()  ; 
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
		public List<Company_Record> Link_Company_By_Uuid()
		{
			try{
				List<Company_Record> ret= new List<Company_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Company ___table = new Company(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.UUID,item.COMPANY_UUID).R().Or()  ; 
 				}
				condition.CheckSQL();
				ret=(List<Company_Record>)
						___table.Where(condition)
						.FetchAll<Company_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		public List<Department_Record> Link_Department_By_Uuid()
		{
			try{
				List<Department_Record> ret= new List<Department_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Department ___table = new Department(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.UUID,item.DEPARTMENT_UUID).R().Or()  ; 
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
		public List<Site_Record> Link_Site_By_Uuid()
		{
			try{
				List<Site_Record> ret= new List<Site_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Site ___table = new Site(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.UUID,item.SITE_UUID).R().Or()  ; 
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
		/*201303180340*/
		public List<Company_Record> Link_Company_By_Uuid(OrderLimit limit)
		{
			try{
				List<Company_Record> ret= new List<Company_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Company ___table = new Company(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.UUID,item.COMPANY_UUID).R().Or()  ; 
 				}
				condition.CheckSQL();
				ret=(List<Company_Record>)
						___table.Where(condition)
						.Order(limit)
						.Limit(limit)
						.FetchAll<Company_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180340*/
		public List<Department_Record> Link_Department_By_Uuid(OrderLimit limit)
		{
			try{
				List<Department_Record> ret= new List<Department_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Department ___table = new Department(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.UUID,item.DEPARTMENT_UUID).R().Or()  ; 
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
		/*201303180340*/
		public List<Site_Record> Link_Site_By_Uuid(OrderLimit limit)
		{
			try{
				List<Site_Record> ret= new List<Site_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Site ___table = new Site(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.UUID,item.SITE_UUID).R().Or()  ; 
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
		/*201303180324*/
		public ErrorLog LinkFill_ErrorLog_By_AttendantUuid()
		{
			try{
				var data = Link_ErrorLog_By_AttendantUuid();
				ErrorLog ret=new ErrorLog(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180324*/
		public ErrorLogV LinkFill_ErrorLogV_By_AttendantUuid()
		{
			try{
				var data = Link_ErrorLogV_By_AttendantUuid();
				ErrorLogV ret=new ErrorLogV(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180324*/
		public GroupAttendant LinkFill_GroupAttendant_By_AttendantUuid()
		{
			try{
				var data = Link_GroupAttendant_By_AttendantUuid();
				GroupAttendant ret=new GroupAttendant(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180324*/
		public Department LinkFill_Department_By_ManagerUuid()
		{
			try{
				var data = Link_Department_By_ManagerUuid();
				Department ret=new Department(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180324*/
		public GroupAttendantV LinkFill_GroupAttendantV_By_AttendantUuid()
		{
			try{
				var data = Link_GroupAttendantV_By_AttendantUuid();
				GroupAttendantV ret=new GroupAttendantV(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180324*/
		public VAuthProxy LinkFill_VAuthProxy_By_AttendantUuid()
		{
			try{
				var data = Link_VAuthProxy_By_AttendantUuid();
				VAuthProxy ret=new VAuthProxy(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180325*/
		public ErrorLog LinkFill_ErrorLog_By_AttendantUuid(OrderLimit limit)
		{
			try{
				var data = Link_ErrorLog_By_AttendantUuid(limit);
				ErrorLog ret=new ErrorLog(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180325*/
		public ErrorLogV LinkFill_ErrorLogV_By_AttendantUuid(OrderLimit limit)
		{
			try{
				var data = Link_ErrorLogV_By_AttendantUuid(limit);
				ErrorLogV ret=new ErrorLogV(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180325*/
		public GroupAttendant LinkFill_GroupAttendant_By_AttendantUuid(OrderLimit limit)
		{
			try{
				var data = Link_GroupAttendant_By_AttendantUuid(limit);
				GroupAttendant ret=new GroupAttendant(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180325*/
		public Department LinkFill_Department_By_ManagerUuid(OrderLimit limit)
		{
			try{
				var data = Link_Department_By_ManagerUuid(limit);
				Department ret=new Department(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180325*/
		public GroupAttendantV LinkFill_GroupAttendantV_By_AttendantUuid(OrderLimit limit)
		{
			try{
				var data = Link_GroupAttendantV_By_AttendantUuid(limit);
				GroupAttendantV ret=new GroupAttendantV(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180325*/
		public VAuthProxy LinkFill_VAuthProxy_By_AttendantUuid(OrderLimit limit)
		{
			try{
				var data = Link_VAuthProxy_By_AttendantUuid(limit);
				VAuthProxy ret=new VAuthProxy(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180336*/
		public Company LinkFill_Company_By_Uuid()
		{
			try{
				var data = Link_Company_By_Uuid();
				Company ret=new Company(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180336*/
		public Department LinkFill_Department_By_Uuid()
		{
			try{
				var data = Link_Department_By_Uuid();
				Department ret=new Department(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180336*/
		public Site LinkFill_Site_By_Uuid()
		{
			try{
				var data = Link_Site_By_Uuid();
				Site ret=new Site(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180337*/
		public Company LinkFill_Company_By_Uuid(OrderLimit limit)
		{
			try{
				var data = Link_Company_By_Uuid(limit);
				Company ret=new Company(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180337*/
		public Department LinkFill_Department_By_Uuid(OrderLimit limit)
		{
			try{
				var data = Link_Department_By_Uuid(limit);
				Department ret=new Department(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180337*/
		public Site LinkFill_Site_By_Uuid(OrderLimit limit)
		{
			try{
				var data = Link_Site_By_Uuid(limit);
				Site ret=new Site(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
	}
}
