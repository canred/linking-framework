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
	[TableView("SCHEDULE_TIME", true)]
	public partial class ScheduleTime : TableBase{
	/*固定物件*/
	//LK.DB.SQLCreater.ASQLCreater sqlCreater = null;
	/*固定物件但名稱需更新*/
	private ScheduleTime_Record _currentRecord = null;
	private IList<ScheduleTime_Record> _All_Record = new List<ScheduleTime_Record>();
		/*建構子*/
		public ScheduleTime(){}
		public ScheduleTime(IDataBaseConfigInfo dbc,string db): base(dbc,db){}
		public ScheduleTime(IDataBaseConfigInfo dbc): base(dbc){}
		public ScheduleTime(IDataBaseConfigInfo dbc,ScheduleTime_Record currenData){
			this.setDataBaseConfigInfo(dbc);
			this._currentRecord = currenData;
		}
		public ScheduleTime(IList<ScheduleTime_Record> currenData){
			this._All_Record = currenData;
		}
		/*欄位資訊 Start*/
		public string UUID {get{return "UUID" ; }}
		public string SCHEDULE_UUID {get{return "SCHEDULE_UUID" ; }}
		public string START_TIME {get{return "START_TIME" ; }}
		public string FINISH_TIME {get{return "FINISH_TIME" ; }}
		public string STATUS {get{return "STATUS" ; }}
		/*欄位資訊 End*/
		/*固定的方法，但名稱需變更 Start*/
		public ScheduleTime_Record CurrentRecord(){
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
		public ScheduleTime_Record CreateNew(){
			try{
				ScheduleTime_Record newData = new ScheduleTime_Record();
				return newData;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		public IList<ScheduleTime_Record> AllRecord(){
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
				_All_Record = new List<ScheduleTime_Record>();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*固定的方法，但名稱需變更 End*/
		/*有關PK的方法*/
		//TEMPLATE TABLE 201303180156
		public ScheduleTime Fill_By_PK(string pUUID){
			try{
				IList<ScheduleTime_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,pUUID)
				).FetchAll<ScheduleTime_Record>()  ;  
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
		public ScheduleTime Fill_By_PK(string pUUID,DB db){
			try{
				IList<ScheduleTime_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,pUUID)
				).FetchAll<ScheduleTime_Record>(db)  ;  
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
		public ScheduleTime_Record Fetch_By_PK(string pUUID){
			try{
				IList<ScheduleTime_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,pUUID)
				).FetchAll<ScheduleTime_Record>()  ;  
				return ret.First();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		//TEMPLATE TABLE 20130319044
		public ScheduleTime_Record Fetch_By_PK(string pUUID,DB db){
			try{
				IList<ScheduleTime_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,pUUID)
				).FetchAll<ScheduleTime_Record>(db)  ;  
				return ret.First();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		//TEMPLATE TABLE 20130319045
		public ScheduleTime Fill_By_Uuid(string pUUID){
			try{
				IList<ScheduleTime_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,pUUID)
				).FetchAll<ScheduleTime_Record>()  ;  
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
		public ScheduleTime Fill_By_Uuid(string pUUID,DB db){
			try{
				IList<ScheduleTime_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,pUUID)
				).FetchAll<ScheduleTime_Record>(db)  ;  
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
		public ScheduleTime_Record Fetch_By_Uuid(string pUUID){
			try{
				IList<ScheduleTime_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,pUUID)
				).FetchAll<ScheduleTime_Record>()  ;  
				return ret.First();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.ErrorNoThrowException(this, ex);
				return null;
			}
		}
		//TEMPLATE TABLE 20130319048
		public ScheduleTime_Record Fetch_By_Uuid(string pUUID,DB db){
			try{
				IList<ScheduleTime_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.UUID,pUUID)
				).FetchAll<ScheduleTime_Record>(db)  ;  
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
				UpdateAllRecord<ScheduleTime_Record>(this.AllRecord());   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來更新資料行*/
		public void UpdateAllRecord(DB db) {
			try{
				UpdateAllRecord<ScheduleTime_Record>(this.AllRecord(),db);   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來新增資料行*/
		public void InsertAllRecord() {
			try{
				InsertAllRecord<ScheduleTime_Record>(this.AllRecord());   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來新增資料行*/
		public void InsertAllRecord(DB db) {
			try{
				InsertAllRecord<ScheduleTime_Record>(this.AllRecord(),db);   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來刪除資料行*/
		public void DeleteAllRecord() {
			try{
				DeleteAllRecord<ScheduleTime_Record>(this.AllRecord());   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來刪除資料行*/
		public void DeleteAllRecord(DB db) {
			try{
				DeleteAllRecord<ScheduleTime_Record>(this.AllRecord(),db);   
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*依照資料表與資料表的關係，產生出來的方法*/
		/*201303180320*/
		public List<VScheduleTime_Record> Link_VScheduleTime_By_ScheduleTimeUuid()
		{
			try{
				List<VScheduleTime_Record> ret= new List<VScheduleTime_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				VScheduleTime ___table = new VScheduleTime(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.SCHEDULE_TIME_UUID,item.UUID).R().Or()  ; 
 				}
				condition.CheckSQL();
				ret=(List<VScheduleTime_Record>)
						___table.Where(condition)
						.FetchAll<VScheduleTime_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180321*/
		public List<VScheduleTime_Record> Link_VScheduleTime_By_ScheduleTimeUuid(OrderLimit limit)
		{
			try{
				List<VScheduleTime_Record> ret= new List<VScheduleTime_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				VScheduleTime ___table = new VScheduleTime(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.SCHEDULE_TIME_UUID,item.UUID).R().Or()  ; 
 				}
				condition.CheckSQL();
				ret=(List<VScheduleTime_Record>)
						___table.Where(condition)
						.Order(limit)
						.Limit(limit)
						.FetchAll<VScheduleTime_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		public List<Schedule_Record> Link_Schedule_By_Uuid()
		{
			try{
				List<Schedule_Record> ret= new List<Schedule_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Schedule ___table = new Schedule(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.UUID,item.SCHEDULE_UUID).R().Or()  ; 
 				}
				condition.CheckSQL();
				ret=(List<Schedule_Record>)
						___table.Where(condition)
						.FetchAll<Schedule_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180340*/
		public List<Schedule_Record> Link_Schedule_By_Uuid(OrderLimit limit)
		{
			try{
				List<Schedule_Record> ret= new List<Schedule_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Schedule ___table = new Schedule(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.UUID,item.SCHEDULE_UUID).R().Or()  ; 
 				}
				condition.CheckSQL();
				ret=(List<Schedule_Record>)
						___table.Where(condition)
						.Order(limit)
						.Limit(limit)
						.FetchAll<Schedule_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180324*/
		public VScheduleTime LinkFill_VScheduleTime_By_ScheduleTimeUuid()
		{
			try{
				var data = Link_VScheduleTime_By_ScheduleTimeUuid();
				VScheduleTime ret=new VScheduleTime(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180325*/
		public VScheduleTime LinkFill_VScheduleTime_By_ScheduleTimeUuid(OrderLimit limit)
		{
			try{
				var data = Link_VScheduleTime_By_ScheduleTimeUuid(limit);
				VScheduleTime ret=new VScheduleTime(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180336*/
		public Schedule LinkFill_Schedule_By_Uuid()
		{
			try{
				var data = Link_Schedule_By_Uuid();
				Schedule ret=new Schedule(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180337*/
		public Schedule LinkFill_Schedule_By_Uuid(OrderLimit limit)
		{
			try{
				var data = Link_Schedule_By_Uuid(limit);
				Schedule ret=new Schedule(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
	}
}
