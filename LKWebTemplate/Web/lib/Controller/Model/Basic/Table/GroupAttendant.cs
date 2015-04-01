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
	[TableView("GROUP_ATTENDANT", true)]
	public partial class GroupAttendant : TableBase{
	/*固定物件*/
	//LK.DB.SQLCreater.ASQLCreater sqlCreater = null;
	/*固定物件但名稱需更新*/
	private GroupAttendant_Record _currentRecord = null;
	private IList<GroupAttendant_Record> _All_Record = new List<GroupAttendant_Record>();
		/*建構子*/
		public GroupAttendant(){}
		public GroupAttendant(IDataBaseConfigInfo dbc,string db): base(dbc,db){}
		public GroupAttendant(IDataBaseConfigInfo dbc): base(dbc){}
		public GroupAttendant(IDataBaseConfigInfo dbc,GroupAttendant_Record currenData){
			this.setDataBaseConfigInfo(dbc);
			this._currentRecord = currenData;
		}
		public GroupAttendant(IList<GroupAttendant_Record> currenData){
			this._All_Record = currenData;
		}
		/*欄位資訊 Start*/
		public string UUID {get{return "UUID" ; }}
		public string IS_ACTIVE {get{return "IS_ACTIVE" ; }}
		public string CREATE_DATE {get{return "CREATE_DATE" ; }}
		public string CREATE_USER {get{return "CREATE_USER" ; }}
		public string UPDATE_DATE {get{return "UPDATE_DATE" ; }}
		public string UPDATE_USER {get{return "UPDATE_USER" ; }}
		public string GROUP_HEAD_UUID {get{return "GROUP_HEAD_UUID" ; }}
		public string ATTENDANT_UUID {get{return "ATTENDANT_UUID" ; }}
		/*欄位資訊 End*/
		/*固定的方法，但名稱需變更 Start*/
		public GroupAttendant_Record CurrentRecord(){
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
		public GroupAttendant_Record CreateNew(){
			try{
				GroupAttendant_Record newData = new GroupAttendant_Record();
				return newData;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		public IList<GroupAttendant_Record> AllRecord(){
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
				_All_Record = new List<GroupAttendant_Record>();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*固定的方法，但名稱需變更 End*/
		/*有關PK的方法*/
		//TEMPLATE TABLE 201303180156
		public GroupAttendant Fill_By_PK(string puuid){
			try{
				IList<GroupAttendant_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<GroupAttendant_Record>()  ;  
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
		public GroupAttendant Fill_By_PK(string puuid,DB db){
			try{
				IList<GroupAttendant_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<GroupAttendant_Record>(db)  ;  
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
		public GroupAttendant_Record Fetch_By_PK(string puuid){
			try{
				IList<GroupAttendant_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<GroupAttendant_Record>()  ;  
				return ret.First();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		//TEMPLATE TABLE 20130319044
		public GroupAttendant_Record Fetch_By_PK(string puuid,DB db){
			try{
				IList<GroupAttendant_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<GroupAttendant_Record>(db)  ;  
				return ret.First();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		//TEMPLATE TABLE 20130319045
		public GroupAttendant Fill_By_Uuid(string puuid){
			try{
				IList<GroupAttendant_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<GroupAttendant_Record>()  ;  
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
		public GroupAttendant Fill_By_Uuid(string puuid,DB db){
			try{
				IList<GroupAttendant_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<GroupAttendant_Record>(db)  ;  
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
		public GroupAttendant_Record Fetch_By_Uuid(string puuid){
			try{
				IList<GroupAttendant_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<GroupAttendant_Record>()  ;  
				return ret.First();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.ErrorNoThrowException(this, ex);
				return null;
			}
		}
		//TEMPLATE TABLE 20130319048
		public GroupAttendant_Record Fetch_By_Uuid(string puuid,DB db){
			try{
				IList<GroupAttendant_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,puuid)
				).FetchAll<GroupAttendant_Record>(db)  ;  
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
				UpdateAllRecord<GroupAttendant_Record>(this.AllRecord());   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來更新資料行*/
		public void UpdateAllRecord(DB db) {
			try{
				UpdateAllRecord<GroupAttendant_Record>(this.AllRecord(),db);   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來新增資料行*/
		public void InsertAllRecord() {
			try{
				InsertAllRecord<GroupAttendant_Record>(this.AllRecord());   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來新增資料行*/
		public void InsertAllRecord(DB db) {
			try{
				InsertAllRecord<GroupAttendant_Record>(this.AllRecord(),db);   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來刪除資料行*/
		public void DeleteAllRecord() {
			try{
				DeleteAllRecord<GroupAttendant_Record>(this.AllRecord());   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來刪除資料行*/
		public void DeleteAllRecord(DB db) {
			try{
				DeleteAllRecord<GroupAttendant_Record>(this.AllRecord(),db);   
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
		public List<Attendant_Record> Link_Attendant_By_Uuid()
		{
			try{
				List<Attendant_Record> ret= new List<Attendant_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Attendant ___table = new Attendant(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.UUID,item.ATTENDANT_UUID).R().Or()  ; 
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
		public List<Attendant_Record> Link_Attendant_By_Uuid(OrderLimit limit)
		{
			try{
				List<Attendant_Record> ret= new List<Attendant_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Attendant ___table = new Attendant(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.UUID,item.ATTENDANT_UUID).R().Or()  ; 
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
