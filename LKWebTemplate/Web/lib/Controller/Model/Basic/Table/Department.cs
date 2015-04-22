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
	[TableView("DEPARTMENT", true)]
	public partial class Department : TableBase{
	/*固定物件*/
	//LK.DB.SQLCreater.ASQLCreater sqlCreater = null;
	/*固定物件但名稱需更新*/
	private Department_Record _currentRecord = null;
	private IList<Department_Record> _All_Record = new List<Department_Record>();
		/*建構子*/
		public Department(){}
		public Department(IDataBaseConfigInfo dbc,string db): base(dbc,db){}
		public Department(IDataBaseConfigInfo dbc): base(dbc){}
		public Department(IDataBaseConfigInfo dbc,Department_Record currenData){
			this.setDataBaseConfigInfo(dbc);
			this._currentRecord = currenData;
		}
		public Department(IList<Department_Record> currenData){
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
		public string COMPANY_UUID {
			[ColumnName("COMPANY_UUID",false,typeof(string))]
			get{return "COMPANY_UUID" ; }}
		public string ID {
			[ColumnName("ID",false,typeof(string))]
			get{return "ID" ; }}
		public string C_NAME {
			[ColumnName("C_NAME",false,typeof(string))]
			get{return "C_NAME" ; }}
		public string E_NAME {
			[ColumnName("E_NAME",false,typeof(string))]
			get{return "E_NAME" ; }}
		public string PARENT_DEPARTMENT_UUID {
			[ColumnName("PARENT_DEPARTMENT_UUID",false,typeof(string))]
			get{return "PARENT_DEPARTMENT_UUID" ; }}
		public string MANAGER_UUID {
			[ColumnName("MANAGER_UUID",false,typeof(string))]
			get{return "MANAGER_UUID" ; }}
		public string PARENT_DEPARTMENT_ID {
			[ColumnName("PARENT_DEPARTMENT_ID",false,typeof(string))]
			get{return "PARENT_DEPARTMENT_ID" ; }}
		public string MANAGER_ID {
			[ColumnName("MANAGER_ID",false,typeof(string))]
			get{return "MANAGER_ID" ; }}
		public string PARENT_DEPARTMENT_UUID_LIST {
			[ColumnName("PARENT_DEPARTMENT_UUID_LIST",false,typeof(string))]
			get{return "PARENT_DEPARTMENT_UUID_LIST" ; }}
		public string S_NAME {
			[ColumnName("S_NAME",false,typeof(string))]
			get{return "S_NAME" ; }}
		public string COST_CENTER {
			[ColumnName("COST_CENTER",false,typeof(string))]
			get{return "COST_CENTER" ; }}
		public string SRC_UUID {
			[ColumnName("SRC_UUID",false,typeof(string))]
			get{return "SRC_UUID" ; }}
		public string FULL_DEPARTMENT_NAME {
			[ColumnName("FULL_DEPARTMENT_NAME",false,typeof(string))]
			get{return "FULL_DEPARTMENT_NAME" ; }}
		/*欄位資訊 End*/
		/*固定的方法，但名稱需變更 Start*/
		public Department_Record CurrentRecord(){
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
		public Department_Record CreateNew(){
			try{
				Department_Record newData = new Department_Record();
				return newData;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		public IList<Department_Record> AllRecord(){
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
				_All_Record = new List<Department_Record>();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*固定的方法，但名稱需變更 End*/
		/*有關PK的方法*/
		//TEMPLATE TABLE 201303180156
		public Department Fill_By_PK(string puuid){
			try{
				IList<Department_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<Department_Record>()  ;  
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
		public Department Fill_By_PK(string puuid,DB db){
			try{
				IList<Department_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<Department_Record>(db)  ;  
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
		public Department_Record Fetch_By_PK(string puuid){
			try{
				IList<Department_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<Department_Record>()  ;  
				return ret.First();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		//TEMPLATE TABLE 20130319044
		public Department_Record Fetch_By_PK(string puuid,DB db){
			try{
				IList<Department_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<Department_Record>(db)  ;  
				return ret.First();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		//TEMPLATE TABLE 20130319045
		public Department Fill_By_Uuid(string puuid){
			try{
				IList<Department_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<Department_Record>()  ;  
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
		public Department Fill_By_Uuid(string puuid,DB db){
			try{
				IList<Department_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<Department_Record>(db)  ;  
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
		public Department_Record Fetch_By_Uuid(string puuid){
			try{
				IList<Department_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<Department_Record>()  ;  
				return ret.First();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.ErrorNoThrowException(this, ex);
				return null;
			}
		}
		//TEMPLATE TABLE 20130319048
		public Department_Record Fetch_By_Uuid(string puuid,DB db){
			try{
				IList<Department_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<Department_Record>(db)  ;  
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
				UpdateAllRecord<Department_Record>(this.AllRecord());   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來更新資料行*/
		public void UpdateAllRecord(DB db) {
			try{
				UpdateAllRecord<Department_Record>(this.AllRecord(),db);   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來新增資料行*/
		public void InsertAllRecord() {
			try{
				InsertAllRecord<Department_Record>(this.AllRecord());   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來新增資料行*/
		public void InsertAllRecord(DB db) {
			try{
				InsertAllRecord<Department_Record>(this.AllRecord(),db);   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來刪除資料行*/
		public void DeleteAllRecord() {
			try{
				DeleteAllRecord<Department_Record>(this.AllRecord());   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來刪除資料行*/
		public void DeleteAllRecord(DB db) {
			try{
				DeleteAllRecord<Department_Record>(this.AllRecord(),db);   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*依照資料表與資料表的關係，產生出來的方法*/
		/*201303180320*/
		public List<Department_Record> Link_Department_By_ParentDepartmentUuid()
		{
			try{
				List<Department_Record> ret= new List<Department_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Department ___table = new Department(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.PARENT_DEPARTMENT_UUID,item.UUID).R().Or()  ; 
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
		public List<Attendant_Record> Link_Attendant_By_DepartmentUuid()
		{
			try{
				List<Attendant_Record> ret= new List<Attendant_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Attendant ___table = new Attendant(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.DEPARTMENT_UUID,item.UUID).R().Or()  ; 
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
		/*201303180321*/
		public List<Department_Record> Link_Department_By_ParentDepartmentUuid(OrderLimit limit)
		{
			try{
				List<Department_Record> ret= new List<Department_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Department ___table = new Department(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.PARENT_DEPARTMENT_UUID,item.UUID).R().Or()  ; 
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
		public List<Attendant_Record> Link_Attendant_By_DepartmentUuid(OrderLimit limit)
		{
			try{
				List<Attendant_Record> ret= new List<Attendant_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Attendant ___table = new Attendant(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.DEPARTMENT_UUID,item.UUID).R().Or()  ; 
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
						.L().Equal(___table.UUID,item.PARENT_DEPARTMENT_UUID).R().Or()  ; 
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
		public List<Attendant_Record> Link_Attendant_By_Uuid()
		{
			try{
				List<Attendant_Record> ret= new List<Attendant_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Attendant ___table = new Attendant(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.UUID,item.MANAGER_UUID).R().Or()  ; 
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
						.L().Equal(___table.UUID,item.PARENT_DEPARTMENT_UUID).R().Or()  ; 
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
		public List<Attendant_Record> Link_Attendant_By_Uuid(OrderLimit limit)
		{
			try{
				List<Attendant_Record> ret= new List<Attendant_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Attendant ___table = new Attendant(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.UUID,item.MANAGER_UUID).R().Or()  ; 
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
		/*201303180324*/
		public Department LinkFill_Department_By_ParentDepartmentUuid()
		{
			try{
				var data = Link_Department_By_ParentDepartmentUuid();
				Department ret=new Department(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180324*/
		public Attendant LinkFill_Attendant_By_DepartmentUuid()
		{
			try{
				var data = Link_Attendant_By_DepartmentUuid();
				Attendant ret=new Attendant(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180325*/
		public Department LinkFill_Department_By_ParentDepartmentUuid(OrderLimit limit)
		{
			try{
				var data = Link_Department_By_ParentDepartmentUuid(limit);
				Department ret=new Department(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180325*/
		public Attendant LinkFill_Attendant_By_DepartmentUuid(OrderLimit limit)
		{
			try{
				var data = Link_Attendant_By_DepartmentUuid(limit);
				Attendant ret=new Attendant(data);
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
		public Attendant LinkFill_Attendant_By_Uuid()
		{
			try{
				var data = Link_Attendant_By_Uuid();
				Attendant ret=new Attendant(data);
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
		public Attendant LinkFill_Attendant_By_Uuid(OrderLimit limit)
		{
			try{
				var data = Link_Attendant_By_Uuid(limit);
				Attendant ret=new Attendant(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
	}
}
