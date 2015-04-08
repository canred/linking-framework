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
	[TableView("GROUP_APPMENU_V", false)]
	public partial class GroupAppmenuV : TableBase{
	/*固定物件*/
	//LK.DB.SQLCreater.ASQLCreater sqlCreater = null;
	/*固定物件但名稱需更新*/
	private GroupAppmenuV_Record _currentRecord = null;
	private IList<GroupAppmenuV_Record> _All_Record = new List<GroupAppmenuV_Record>();
		/*建構子*/
		public GroupAppmenuV(){}
		public GroupAppmenuV(IDataBaseConfigInfo dbc,string db): base(dbc,db){}
		public GroupAppmenuV(IDataBaseConfigInfo dbc): base(dbc){}
		public GroupAppmenuV(IDataBaseConfigInfo dbc,GroupAppmenuV_Record currenData){
			this.setDataBaseConfigInfo(dbc);
			this._currentRecord = currenData;
		}
		public GroupAppmenuV(IList<GroupAppmenuV_Record> currenData){
			this._All_Record = currenData;
		}
		/*欄位資訊 Start*/
		public string UUID {get{return "UUID" ; }}
		public string IS_ACTIVE {get{return "IS_ACTIVE" ; }}
		public string CREATE_DATE {get{return "CREATE_DATE" ; }}
		public string CREATE_USER {get{return "CREATE_USER" ; }}
		public string UPDATE_DATE {get{return "UPDATE_DATE" ; }}
		public string UPDATE_USER {get{return "UPDATE_USER" ; }}
		public string APPMENU_UUID {get{return "APPMENU_UUID" ; }}
		public string GROUP_HEAD_UUID {get{return "GROUP_HEAD_UUID" ; }}
		public string IS_DEFAULT_PAGE {get{return "IS_DEFAULT_PAGE" ; }}
		/*欄位資訊 End*/
		/*固定的方法，但名稱需變更 Start*/
		public GroupAppmenuV_Record CurrentRecord(){
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
		public GroupAppmenuV_Record CreateNew(){
			try{
				GroupAppmenuV_Record newData = new GroupAppmenuV_Record();
				return newData;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		public IList<GroupAppmenuV_Record> AllRecord(){
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
				_All_Record = new List<GroupAppmenuV_Record>();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*固定的方法，但名稱需變更 End*/
		/*有關PK的方法*/
		//TEMPLATE TABLE 201303180156
		public GroupAppmenuV Fill_By_PK(string pUUID){
			try{
				IList<GroupAppmenuV_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,pUUID)
				).FetchAll<GroupAppmenuV_Record>()  ;  
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
		public GroupAppmenuV Fill_By_PK(string pUUID,DB db){
			try{
				IList<GroupAppmenuV_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,pUUID)
				).FetchAll<GroupAppmenuV_Record>(db)  ;  
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
		public GroupAppmenuV_Record Fetch_By_PK(string pUUID){
			try{
				IList<GroupAppmenuV_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,pUUID)
				).FetchAll<GroupAppmenuV_Record>()  ;  
				return ret.First();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		//TEMPLATE TABLE 20130319044
		public GroupAppmenuV_Record Fetch_By_PK(string pUUID,DB db){
			try{
				IList<GroupAppmenuV_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,pUUID)
				).FetchAll<GroupAppmenuV_Record>(db)  ;  
				return ret.First();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		//TEMPLATE TABLE 20130319045
		public GroupAppmenuV Fill_By_Uuid(string pUUID){
			try{
				IList<GroupAppmenuV_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,pUUID)
				).FetchAll<GroupAppmenuV_Record>()  ;  
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
		public GroupAppmenuV Fill_By_Uuid(string pUUID,DB db){
			try{
				IList<GroupAppmenuV_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,pUUID)
				).FetchAll<GroupAppmenuV_Record>(db)  ;  
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
		public GroupAppmenuV_Record Fetch_By_Uuid(string pUUID){
			try{
				IList<GroupAppmenuV_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,pUUID)
				).FetchAll<GroupAppmenuV_Record>()  ;  
				return ret.First();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.ErrorNoThrowException(this, ex);
				return null;
			}
		}
		//TEMPLATE TABLE 20130319048
		public GroupAppmenuV_Record Fetch_By_Uuid(string pUUID,DB db){
			try{
				IList<GroupAppmenuV_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,pUUID)
				).FetchAll<GroupAppmenuV_Record>(db)  ;  
				return ret.First();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*依照資料表與資料表的關係，產生出來的方法*/
		public List<GroupHead_Record> Link_GroupHead_By_Uuid()
		{
			try{
				List<GroupHead_Record> ret= new List<GroupHead_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				GroupHead ___table = new GroupHead(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.UUID,item.GROUP_HEAD_UUID).R().Or()  ; 
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
		/*201303180340*/
		public List<GroupHead_Record> Link_GroupHead_By_Uuid(OrderLimit limit)
		{
			try{
				List<GroupHead_Record> ret= new List<GroupHead_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				GroupHead ___table = new GroupHead(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.UUID,item.GROUP_HEAD_UUID).R().Or()  ; 
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
		/*201303180336*/
		public GroupHead LinkFill_GroupHead_By_Uuid()
		{
			try{
				var data = Link_GroupHead_By_Uuid();
				GroupHead ret=new GroupHead(data);
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
		/*201303180337*/
		public GroupHead LinkFill_GroupHead_By_Uuid(OrderLimit limit)
		{
			try{
				var data = Link_GroupHead_By_Uuid(limit);
				GroupHead ret=new GroupHead(data);
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
	}
}
