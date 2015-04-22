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
	[TableView("AUTHORITY_MENU_V", false)]
	public partial class AuthorityMenuV : TableBase{
	/*固定物件*/
	//LK.DB.SQLCreater.ASQLCreater sqlCreater = null;
	/*固定物件但名稱需更新*/
	private AuthorityMenuV_Record _currentRecord = null;
	private IList<AuthorityMenuV_Record> _All_Record = new List<AuthorityMenuV_Record>();
		/*建構子*/
		public AuthorityMenuV(){}
		public AuthorityMenuV(IDataBaseConfigInfo dbc,string db): base(dbc,db){}
		public AuthorityMenuV(IDataBaseConfigInfo dbc): base(dbc){}
		public AuthorityMenuV(IDataBaseConfigInfo dbc,AuthorityMenuV_Record currenData){
			this.setDataBaseConfigInfo(dbc);
			this._currentRecord = currenData;
		}
		public AuthorityMenuV(IList<AuthorityMenuV_Record> currenData){
			this._All_Record = currenData;
		}
		/*欄位資訊 Start*/
		public string IS_USER_DEFAULT_PAGE {
			[ColumnName("IS_USER_DEFAULT_PAGE",false,typeof(string))]
			get{return "IS_USER_DEFAULT_PAGE" ; }}
		public string UUID {
			[ColumnName("UUID",true,typeof(string))]
			get{return "UUID" ; }}
		public string IS_ACTIVE {
			[ColumnName("IS_ACTIVE",false,typeof(string))]
			get{return "IS_ACTIVE" ; }}
		public string CREATE_DATE {
			[ColumnName("CREATE_DATE",false,typeof(DateTime?))]
			get{return "CREATE_DATE" ; }}
		public string CREATE_USER {
			[ColumnName("CREATE_USER",false,typeof(string))]
			get{return "CREATE_USER" ; }}
		public string UPDATE_DATE {
			[ColumnName("UPDATE_DATE",false,typeof(DateTime?))]
			get{return "UPDATE_DATE" ; }}
		public string UPDATE_USER {
			[ColumnName("UPDATE_USER",false,typeof(string))]
			get{return "UPDATE_USER" ; }}
		public string NAME_ZH_TW {
			[ColumnName("NAME_ZH_TW",false,typeof(string))]
			get{return "NAME_ZH_TW" ; }}
		public string NAME_ZH_CN {
			[ColumnName("NAME_ZH_CN",false,typeof(string))]
			get{return "NAME_ZH_CN" ; }}
		public string NAME_EN_US {
			[ColumnName("NAME_EN_US",false,typeof(string))]
			get{return "NAME_EN_US" ; }}
		public string NAME_JPN {
			[ColumnName("NAME_JPN",false,typeof(string))]
			get{return "NAME_JPN" ; }}
		public string ID {
			[ColumnName("ID",false,typeof(string))]
			get{return "ID" ; }}
		public string APPMENU_UUID {
			[ColumnName("APPMENU_UUID",false,typeof(string))]
			get{return "APPMENU_UUID" ; }}
		public string HASCHILD {
			[ColumnName("HASCHILD",false,typeof(string))]
			get{return "HASCHILD" ; }}
		public string APPLICATION_HEAD_UUID {
			[ColumnName("APPLICATION_HEAD_UUID",false,typeof(string))]
			get{return "APPLICATION_HEAD_UUID" ; }}
		public string ORD {
			[ColumnName("ORD",false,typeof(decimal?))]
			get{return "ORD" ; }}
		public string PARAMETER_CLASS {
			[ColumnName("PARAMETER_CLASS",false,typeof(string))]
			get{return "PARAMETER_CLASS" ; }}
		public string IMAGE {
			[ColumnName("IMAGE",false,typeof(string))]
			get{return "IMAGE" ; }}
		public string SITEMAP_UUID {
			[ColumnName("SITEMAP_UUID",false,typeof(string))]
			get{return "SITEMAP_UUID" ; }}
		public string ACTION_MODE {
			[ColumnName("ACTION_MODE",false,typeof(string))]
			get{return "ACTION_MODE" ; }}
		public string IS_DEFAULT_PAGE {
			[ColumnName("IS_DEFAULT_PAGE",false,typeof(string))]
			get{return "IS_DEFAULT_PAGE" ; }}
		public string IS_ADMIN {
			[ColumnName("IS_ADMIN",false,typeof(string))]
			get{return "IS_ADMIN" ; }}
		public string ATTENDANT_UUID {
			[ColumnName("ATTENDANT_UUID",false,typeof(string))]
			get{return "ATTENDANT_UUID" ; }}
		public string APPLICATION_NAME {
			[ColumnName("APPLICATION_NAME",false,typeof(string))]
			get{return "APPLICATION_NAME" ; }}
		public string URL {
			[ColumnName("URL",false,typeof(string))]
			get{return "URL" ; }}
		public string FUNC_PARAMETER_CLASS {
			[ColumnName("FUNC_PARAMETER_CLASS",false,typeof(string))]
			get{return "FUNC_PARAMETER_CLASS" ; }}
		public string P_MODE {
			[ColumnName("P_MODE",false,typeof(string))]
			get{return "P_MODE" ; }}
		public string RUNJSFUNCTION {
			[ColumnName("RUNJSFUNCTION",false,typeof(string))]
			get{return "RUNJSFUNCTION" ; }}
		/*欄位資訊 End*/
		/*固定的方法，但名稱需變更 Start*/
		public AuthorityMenuV_Record CurrentRecord(){
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
		public AuthorityMenuV_Record CreateNew(){
			try{
				AuthorityMenuV_Record newData = new AuthorityMenuV_Record();
				return newData;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		public IList<AuthorityMenuV_Record> AllRecord(){
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
				_All_Record = new List<AuthorityMenuV_Record>();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*固定的方法，但名稱需變更 End*/
		/*有關PK的方法*/
		//TEMPLATE TABLE 201303180156
		public AuthorityMenuV Fill_By_PK(string puuid){
			try{
				IList<AuthorityMenuV_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<AuthorityMenuV_Record>()  ;  
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
		public AuthorityMenuV Fill_By_PK(string puuid,DB db){
			try{
				IList<AuthorityMenuV_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<AuthorityMenuV_Record>(db)  ;  
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
		public AuthorityMenuV_Record Fetch_By_PK(string puuid){
			try{
				IList<AuthorityMenuV_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<AuthorityMenuV_Record>()  ;  
				return ret.First();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		//TEMPLATE TABLE 20130319044
		public AuthorityMenuV_Record Fetch_By_PK(string puuid,DB db){
			try{
				IList<AuthorityMenuV_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<AuthorityMenuV_Record>(db)  ;  
				return ret.First();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		//TEMPLATE TABLE 20130319045
		public AuthorityMenuV Fill_By_Uuid(string puuid){
			try{
				IList<AuthorityMenuV_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<AuthorityMenuV_Record>()  ;  
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
		public AuthorityMenuV Fill_By_Uuid(string puuid,DB db){
			try{
				IList<AuthorityMenuV_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<AuthorityMenuV_Record>(db)  ;  
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
		public AuthorityMenuV_Record Fetch_By_Uuid(string puuid){
			try{
				IList<AuthorityMenuV_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<AuthorityMenuV_Record>()  ;  
				return ret.First();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.ErrorNoThrowException(this, ex);
				return null;
			}
		}
		//TEMPLATE TABLE 20130319048
		public AuthorityMenuV_Record Fetch_By_Uuid(string puuid,DB db){
			try{
				IList<AuthorityMenuV_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<AuthorityMenuV_Record>(db)  ;  
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
