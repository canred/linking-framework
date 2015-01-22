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
		public string IS_USER_DEFAULT_PAGE {get{return "IS_USER_DEFAULT_PAGE" ; }}
		public string UUID {get{return "UUID" ; }}
		public string IS_ACTIVE {get{return "IS_ACTIVE" ; }}
		public string CREATE_DATE {get{return "CREATE_DATE" ; }}
		public string CREATE_USER {get{return "CREATE_USER" ; }}
		public string UPDATE_DATE {get{return "UPDATE_DATE" ; }}
		public string UPDATE_USER {get{return "UPDATE_USER" ; }}
		public string NAME_ZH_TW {get{return "NAME_ZH_TW" ; }}
		public string NAME_ZH_CN {get{return "NAME_ZH_CN" ; }}
		public string NAME_EN_US {get{return "NAME_EN_US" ; }}
		public string ID {get{return "ID" ; }}
		public string APPMENU_UUID {get{return "APPMENU_UUID" ; }}
		public string HASCHILD {get{return "HASCHILD" ; }}
		public string APPLICATION_HEAD_UUID {get{return "APPLICATION_HEAD_UUID" ; }}
		public string ORD {get{return "ORD" ; }}
		public string PARAMETER_CLASS {get{return "PARAMETER_CLASS" ; }}
		public string IMAGE {get{return "IMAGE" ; }}
		public string SITEMAP_UUID {get{return "SITEMAP_UUID" ; }}
		public string ACTION_MODE {get{return "ACTION_MODE" ; }}
		public string IS_DEFAULT_PAGE {get{return "IS_DEFAULT_PAGE" ; }}
		public string IS_ADMIN {get{return "IS_ADMIN" ; }}
		public string ATTENDANT_UUID {get{return "ATTENDANT_UUID" ; }}
		public string APPLICATION_NAME {get{return "APPLICATION_NAME" ; }}
		public string URL {get{return "URL" ; }}
		public string FUNC_PARAMETER_CLASS {get{return "FUNC_PARAMETER_CLASS" ; }}
		public string P_MODE {get{return "P_MODE" ; }}
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
		public AuthorityMenuV Fill_By_PK(string pUUID){
			try{
				IList<AuthorityMenuV_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,pUUID)
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
		public AuthorityMenuV Fill_By_PK(string pUUID,DB db){
			try{
				IList<AuthorityMenuV_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,pUUID)
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
		public AuthorityMenuV_Record Fetch_By_PK(string pUUID){
			try{
				IList<AuthorityMenuV_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,pUUID)
				).FetchAll<AuthorityMenuV_Record>()  ;  
				return ret.First();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		//TEMPLATE TABLE 20130319044
		public AuthorityMenuV_Record Fetch_By_PK(string pUUID,DB db){
			try{
				IList<AuthorityMenuV_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,pUUID)
				).FetchAll<AuthorityMenuV_Record>(db)  ;  
				return ret.First();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		//TEMPLATE TABLE 20130319045
		public AuthorityMenuV Fill_By_Uuid(string pUUID){
			try{
				IList<AuthorityMenuV_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,pUUID)
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
		public AuthorityMenuV Fill_By_Uuid(string pUUID,DB db){
			try{
				IList<AuthorityMenuV_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,pUUID)
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
		public AuthorityMenuV_Record Fetch_By_Uuid(string pUUID){
			try{
				IList<AuthorityMenuV_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,pUUID)
				).FetchAll<AuthorityMenuV_Record>()  ;  
				return ret.First();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.ErrorNoThrowException(this, ex);
				return null;
			}
		}
		//TEMPLATE TABLE 20130319048
		public AuthorityMenuV_Record Fetch_By_Uuid(string pUUID,DB db){
			try{
				IList<AuthorityMenuV_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,pUUID)
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
