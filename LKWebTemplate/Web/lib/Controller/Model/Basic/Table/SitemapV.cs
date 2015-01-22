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
	[TableView("SITEMAP_V", false)]
	public partial class SitemapV : TableBase{
	/*固定物件*/
	//LK.DB.SQLCreater.ASQLCreater sqlCreater = null;
	/*固定物件但名稱需更新*/
	private SitemapV_Record _currentRecord = null;
	private IList<SitemapV_Record> _All_Record = new List<SitemapV_Record>();
		/*建構子*/
		public SitemapV(){}
		public SitemapV(IDataBaseConfigInfo dbc,string db): base(dbc,db){}
		public SitemapV(IDataBaseConfigInfo dbc): base(dbc){}
		public SitemapV(IDataBaseConfigInfo dbc,SitemapV_Record currenData){
			this.setDataBaseConfigInfo(dbc);
			this._currentRecord = currenData;
		}
		public SitemapV(IList<SitemapV_Record> currenData){
			this._All_Record = currenData;
		}
		/*欄位資訊 Start*/
		public string UUID {get{return "UUID" ; }}
		public string IS_ACTIVE {get{return "IS_ACTIVE" ; }}
		public string CREATE_DATE {get{return "CREATE_DATE" ; }}
		public string CREATE_USER {get{return "CREATE_USER" ; }}
		public string UPDATE_DATE {get{return "UPDATE_DATE" ; }}
		public string UPDATE_USER {get{return "UPDATE_USER" ; }}
		public string SITEMAP_UUID {get{return "SITEMAP_UUID" ; }}
		public string APPPAGE_UUID {get{return "APPPAGE_UUID" ; }}
		public string ROOT_UUID {get{return "ROOT_UUID" ; }}
		public string HASCHILD {get{return "HASCHILD" ; }}
		public string APPLICATION_HEAD_UUID {get{return "APPLICATION_HEAD_UUID" ; }}
		public string NAME {get{return "NAME" ; }}
		public string DESCRIPTION {get{return "DESCRIPTION" ; }}
		public string URL {get{return "URL" ; }}
		public string P_MODE {get{return "P_MODE" ; }}
		public string PARAMETER_CLASS {get{return "PARAMETER_CLASS" ; }}
		public string APPPAGE_IS_ACTIVE {get{return "APPPAGE_IS_ACTIVE" ; }}
		/*欄位資訊 End*/
		/*固定的方法，但名稱需變更 Start*/
		public SitemapV_Record CurrentRecord(){
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
		public SitemapV_Record CreateNew(){
			try{
				SitemapV_Record newData = new SitemapV_Record();
				return newData;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		public IList<SitemapV_Record> AllRecord(){
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
				_All_Record = new List<SitemapV_Record>();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*固定的方法，但名稱需變更 End*/
		/*有關PK的方法*/
		//TEMPLATE TABLE 201303180156
		public SitemapV Fill_By_PK(string pUUID){
			try{
				IList<SitemapV_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,pUUID)
				).FetchAll<SitemapV_Record>()  ;  
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
		public SitemapV Fill_By_PK(string pUUID,DB db){
			try{
				IList<SitemapV_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,pUUID)
				).FetchAll<SitemapV_Record>(db)  ;  
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
		public SitemapV_Record Fetch_By_PK(string pUUID){
			try{
				IList<SitemapV_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,pUUID)
				).FetchAll<SitemapV_Record>()  ;  
				return ret.First();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		//TEMPLATE TABLE 20130319044
		public SitemapV_Record Fetch_By_PK(string pUUID,DB db){
			try{
				IList<SitemapV_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,pUUID)
				).FetchAll<SitemapV_Record>(db)  ;  
				return ret.First();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		//TEMPLATE TABLE 20130319045
		public SitemapV Fill_By_Uuid(string pUUID){
			try{
				IList<SitemapV_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,pUUID)
				).FetchAll<SitemapV_Record>()  ;  
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
		public SitemapV Fill_By_Uuid(string pUUID,DB db){
			try{
				IList<SitemapV_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,pUUID)
				).FetchAll<SitemapV_Record>(db)  ;  
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
		public SitemapV_Record Fetch_By_Uuid(string pUUID){
			try{
				IList<SitemapV_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,pUUID)
				).FetchAll<SitemapV_Record>()  ;  
				return ret.First();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.ErrorNoThrowException(this, ex);
				return null;
			}
		}
		//TEMPLATE TABLE 20130319048
		public SitemapV_Record Fetch_By_Uuid(string pUUID,DB db){
			try{
				IList<SitemapV_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,pUUID)
				).FetchAll<SitemapV_Record>(db)  ;  
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
