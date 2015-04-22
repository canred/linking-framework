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
	[TableView("ATTENDANT_V", false)]
	public partial class AttendantV : TableBase{
	/*固定物件*/
	//LK.DB.SQLCreater.ASQLCreater sqlCreater = null;
	/*固定物件但名稱需更新*/
	private AttendantV_Record _currentRecord = null;
	private IList<AttendantV_Record> _All_Record = new List<AttendantV_Record>();
		/*建構子*/
		public AttendantV(){}
		public AttendantV(IDataBaseConfigInfo dbc,string db): base(dbc,db){}
		public AttendantV(IDataBaseConfigInfo dbc): base(dbc){}
		public AttendantV(IDataBaseConfigInfo dbc,AttendantV_Record currenData){
			this.setDataBaseConfigInfo(dbc);
			this._currentRecord = currenData;
		}
		public AttendantV(IList<AttendantV_Record> currenData){
			this._All_Record = currenData;
		}
		/*欄位資訊 Start*/
		public string COMPANY_ID {
			[ColumnName("COMPANY_ID",false,typeof(string))]
			get{return "COMPANY_ID" ; }}
		public string COMPANY_C_NAME {
			[ColumnName("COMPANY_C_NAME",false,typeof(string))]
			get{return "COMPANY_C_NAME" ; }}
		public string COMPANY_E_NAME {
			[ColumnName("COMPANY_E_NAME",false,typeof(string))]
			get{return "COMPANY_E_NAME" ; }}
		public string DEPARTMENT_ID {
			[ColumnName("DEPARTMENT_ID",false,typeof(string))]
			get{return "DEPARTMENT_ID" ; }}
		public string DEPARTMENT_C_NAME {
			[ColumnName("DEPARTMENT_C_NAME",false,typeof(string))]
			get{return "DEPARTMENT_C_NAME" ; }}
		public string DEPARTMENT_E_NAME {
			[ColumnName("DEPARTMENT_E_NAME",false,typeof(string))]
			get{return "DEPARTMENT_E_NAME" ; }}
		public string SITE_ID {
			[ColumnName("SITE_ID",false,typeof(string))]
			get{return "SITE_ID" ; }}
		public string SITE_C_NAME {
			[ColumnName("SITE_C_NAME",false,typeof(string))]
			get{return "SITE_C_NAME" ; }}
		public string SITE_E_NAME {
			[ColumnName("SITE_E_NAME",false,typeof(string))]
			get{return "SITE_E_NAME" ; }}
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
		public string ACCOUNT {
			[ColumnName("ACCOUNT",false,typeof(string))]
			get{return "ACCOUNT" ; }}
		public string C_NAME {
			[ColumnName("C_NAME",false,typeof(string))]
			get{return "C_NAME" ; }}
		public string E_NAME {
			[ColumnName("E_NAME",false,typeof(string))]
			get{return "E_NAME" ; }}
		public string EMAIL {
			[ColumnName("EMAIL",false,typeof(string))]
			get{return "EMAIL" ; }}
		public string PASSWORD {
			[ColumnName("PASSWORD",false,typeof(string))]
			get{return "PASSWORD" ; }}
		public string IS_SUPPER {
			[ColumnName("IS_SUPPER",false,typeof(string))]
			get{return "IS_SUPPER" ; }}
		public string IS_ADMIN {
			[ColumnName("IS_ADMIN",false,typeof(string))]
			get{return "IS_ADMIN" ; }}
		public string CODE_PAGE {
			[ColumnName("CODE_PAGE",false,typeof(string))]
			get{return "CODE_PAGE" ; }}
		public string DEPARTMENT_UUID {
			[ColumnName("DEPARTMENT_UUID",false,typeof(string))]
			get{return "DEPARTMENT_UUID" ; }}
		public string PHONE {
			[ColumnName("PHONE",false,typeof(string))]
			get{return "PHONE" ; }}
		public string SITE_UUID {
			[ColumnName("SITE_UUID",false,typeof(string))]
			get{return "SITE_UUID" ; }}
		public string GENDER {
			[ColumnName("GENDER",false,typeof(string))]
			get{return "GENDER" ; }}
		public string BIRTHDAY {
			[ColumnName("BIRTHDAY",false,typeof(DateTime?))]
			get{return "BIRTHDAY" ; }}
		public string HIRE_DATE {
			[ColumnName("HIRE_DATE",false,typeof(DateTime?))]
			get{return "HIRE_DATE" ; }}
		public string QUIT_DATE {
			[ColumnName("QUIT_DATE",false,typeof(DateTime?))]
			get{return "QUIT_DATE" ; }}
		public string IS_MANAGER {
			[ColumnName("IS_MANAGER",false,typeof(string))]
			get{return "IS_MANAGER" ; }}
		public string IS_DIRECT {
			[ColumnName("IS_DIRECT",false,typeof(string))]
			get{return "IS_DIRECT" ; }}
		public string GRADE {
			[ColumnName("GRADE",false,typeof(string))]
			get{return "GRADE" ; }}
		public string ID {
			[ColumnName("ID",false,typeof(string))]
			get{return "ID" ; }}
		public string IS_DEFAULT_PASS {
			[ColumnName("IS_DEFAULT_PASS",false,typeof(string))]
			get{return "IS_DEFAULT_PASS" ; }}
		public string PICTURE_URL {
			[ColumnName("PICTURE_URL",false,typeof(string))]
			get{return "PICTURE_URL" ; }}
		/*欄位資訊 End*/
		/*固定的方法，但名稱需變更 Start*/
		public AttendantV_Record CurrentRecord(){
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
		public AttendantV_Record CreateNew(){
			try{
				AttendantV_Record newData = new AttendantV_Record();
				return newData;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		public IList<AttendantV_Record> AllRecord(){
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
				_All_Record = new List<AttendantV_Record>();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*固定的方法，但名稱需變更 End*/
		/*有關PK的方法*/
		//TEMPLATE TABLE 201303180156
		public AttendantV Fill_By_PK(string pUUID){
			try{
				IList<AttendantV_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,pUUID)
				).FetchAll<AttendantV_Record>()  ;  
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
		public AttendantV Fill_By_PK(string pUUID,DB db){
			try{
				IList<AttendantV_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,pUUID)
				).FetchAll<AttendantV_Record>(db)  ;  
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
		public AttendantV_Record Fetch_By_PK(string pUUID){
			try{
				IList<AttendantV_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,pUUID)
				).FetchAll<AttendantV_Record>()  ;  
				return ret.First();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		//TEMPLATE TABLE 20130319044
		public AttendantV_Record Fetch_By_PK(string pUUID,DB db){
			try{
				IList<AttendantV_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,pUUID)
				).FetchAll<AttendantV_Record>(db)  ;  
				return ret.First();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		//TEMPLATE TABLE 20130319045
		public AttendantV Fill_By_Uuid(string pUUID){
			try{
				IList<AttendantV_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,pUUID)
				).FetchAll<AttendantV_Record>()  ;  
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
		public AttendantV Fill_By_Uuid(string pUUID,DB db){
			try{
				IList<AttendantV_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,pUUID)
				).FetchAll<AttendantV_Record>(db)  ;  
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
		public AttendantV_Record Fetch_By_Uuid(string pUUID){
			try{
				IList<AttendantV_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,pUUID)
				).FetchAll<AttendantV_Record>()  ;  
				return ret.First();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.ErrorNoThrowException(this, ex);
				return null;
			}
		}
		//TEMPLATE TABLE 20130319048
		public AttendantV_Record Fetch_By_Uuid(string pUUID,DB db){
			try{
				IList<AttendantV_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,pUUID)
				).FetchAll<AttendantV_Record>(db)  ;  
				return ret.First();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*依照資料表與資料表的關係，產生出來的方法*/
	}
}
