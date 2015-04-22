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
	[TableView("V_SCHEDULE_TIME", false)]
	public partial class VScheduleTime : TableBase{
	/*固定物件*/
	//LK.DB.SQLCreater.ASQLCreater sqlCreater = null;
	/*固定物件但名稱需更新*/
	private VScheduleTime_Record _currentRecord = null;
	private IList<VScheduleTime_Record> _All_Record = new List<VScheduleTime_Record>();
		/*建構子*/
		public VScheduleTime(){}
		public VScheduleTime(IDataBaseConfigInfo dbc,string db): base(dbc,db){}
		public VScheduleTime(IDataBaseConfigInfo dbc): base(dbc){}
		public VScheduleTime(IDataBaseConfigInfo dbc,VScheduleTime_Record currenData){
			this.setDataBaseConfigInfo(dbc);
			this._currentRecord = currenData;
		}
		public VScheduleTime(IList<VScheduleTime_Record> currenData){
			this._All_Record = currenData;
		}
		/*欄位資訊 Start*/
		public string SCHEDULE_UUID {
			[ColumnName("SCHEDULE_UUID",true,typeof(string))]
			get{return "SCHEDULE_UUID" ; }}
		public string C_DAY {
			[ColumnName("C_DAY",false,typeof(int?))]
			get{return "C_DAY" ; }}
		public string C_DAY_OF_MONTH {
			[ColumnName("C_DAY_OF_MONTH",false,typeof(string))]
			get{return "C_DAY_OF_MONTH" ; }}
		public string C_DAY_OF_WEEK {
			[ColumnName("C_DAY_OF_WEEK",false,typeof(string))]
			get{return "C_DAY_OF_WEEK" ; }}
		public string C_HOUR {
			[ColumnName("C_HOUR",false,typeof(int?))]
			get{return "C_HOUR" ; }}
		public string C_MINUTE {
			[ColumnName("C_MINUTE",false,typeof(int?))]
			get{return "C_MINUTE" ; }}
		public string C_MONTH {
			[ColumnName("C_MONTH",false,typeof(int?))]
			get{return "C_MONTH" ; }}
		public string C_WEEK {
			[ColumnName("C_WEEK",false,typeof(int?))]
			get{return "C_WEEK" ; }}
		public string C_WEEK_OF_MONTH {
			[ColumnName("C_WEEK_OF_MONTH",false,typeof(string))]
			get{return "C_WEEK_OF_MONTH" ; }}
		public string C_WEEK_OF_YEAR {
			[ColumnName("C_WEEK_OF_YEAR",false,typeof(string))]
			get{return "C_WEEK_OF_YEAR" ; }}
		public string C_YEAR {
			[ColumnName("C_YEAR",false,typeof(int?))]
			get{return "C_YEAR" ; }}
		public string CYCLE_TYPE {
			[ColumnName("CYCLE_TYPE",false,typeof(string))]
			get{return "CYCLE_TYPE" ; }}
		public string HOUR {
			[ColumnName("HOUR",false,typeof(string))]
			get{return "HOUR" ; }}
		public string IS_ACTIVE {
			[ColumnName("IS_ACTIVE",false,typeof(string))]
			get{return "IS_ACTIVE" ; }}
		public string IS_CYCLE {
			[ColumnName("IS_CYCLE",false,typeof(string))]
			get{return "IS_CYCLE" ; }}
		public string LAST_RUN_STATUS {
			[ColumnName("LAST_RUN_STATUS",false,typeof(string))]
			get{return "LAST_RUN_STATUS" ; }}
		public string LAST_RUN_TIME {
			[ColumnName("LAST_RUN_TIME",false,typeof(DateTime?))]
			get{return "LAST_RUN_TIME" ; }}
		public string MINUTE {
			[ColumnName("MINUTE",false,typeof(string))]
			get{return "MINUTE" ; }}
		public string RUN_ATTENDANT_UUID {
			[ColumnName("RUN_ATTENDANT_UUID",false,typeof(string))]
			get{return "RUN_ATTENDANT_UUID" ; }}
		public string RUN_URL {
			[ColumnName("RUN_URL",false,typeof(string))]
			get{return "RUN_URL" ; }}
		public string RUN_URL_PARAMETER {
			[ColumnName("RUN_URL_PARAMETER",false,typeof(string))]
			get{return "RUN_URL_PARAMETER" ; }}
		public string SCHEDULE_END_DATE {
			[ColumnName("SCHEDULE_END_DATE",false,typeof(DateTime?))]
			get{return "SCHEDULE_END_DATE" ; }}
		public string SCHEDULE_NAME {
			[ColumnName("SCHEDULE_NAME",false,typeof(string))]
			get{return "SCHEDULE_NAME" ; }}
		public string SINGLE_DATE {
			[ColumnName("SINGLE_DATE",false,typeof(DateTime?))]
			get{return "SINGLE_DATE" ; }}
		public string SCHEDULE_TIME_UUID {
			[ColumnName("SCHEDULE_TIME_UUID",true,typeof(string))]
			get{return "SCHEDULE_TIME_UUID" ; }}
		public string STATUS {
			[ColumnName("STATUS",false,typeof(string))]
			get{return "STATUS" ; }}
		public string START_TIME {
			[ColumnName("START_TIME",false,typeof(DateTime?))]
			get{return "START_TIME" ; }}
		public string FINISH_TIME {
			[ColumnName("FINISH_TIME",false,typeof(DateTime?))]
			get{return "FINISH_TIME" ; }}
		/*欄位資訊 End*/
		/*固定的方法，但名稱需變更 Start*/
		public VScheduleTime_Record CurrentRecord(){
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
		public VScheduleTime_Record CreateNew(){
			try{
				VScheduleTime_Record newData = new VScheduleTime_Record();
				return newData;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		public IList<VScheduleTime_Record> AllRecord(){
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
				_All_Record = new List<VScheduleTime_Record>();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*固定的方法，但名稱需變更 End*/
		/*有關PK的方法*/
		//TEMPLATE TABLE 201303180156
		public VScheduleTime Fill_By_PK(string pSCHEDULE_UUID,string pschedule_time_uuid){
			try{
				IList<VScheduleTime_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.SCHEDULE_UUID,pSCHEDULE_UUID)
									.And()
									.Equal(this.SCHEDULE_TIME_UUID,pschedule_time_uuid)
				).FetchAll<VScheduleTime_Record>()  ;  
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
		public VScheduleTime Fill_By_PK(string pSCHEDULE_UUID,string pschedule_time_uuid,DB db){
			try{
				IList<VScheduleTime_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.SCHEDULE_UUID,pSCHEDULE_UUID)
									.And()
									.Equal(this.SCHEDULE_TIME_UUID,pschedule_time_uuid)
				).FetchAll<VScheduleTime_Record>(db)  ;  
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
		public VScheduleTime_Record Fetch_By_PK(string pSCHEDULE_UUID,string pschedule_time_uuid){
			try{
				IList<VScheduleTime_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.SCHEDULE_UUID,pSCHEDULE_UUID)
									.Equal(this.SCHEDULE_TIME_UUID,pschedule_time_uuid)
				).FetchAll<VScheduleTime_Record>()  ;  
				return ret.First();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		//TEMPLATE TABLE 20130319044
		public VScheduleTime_Record Fetch_By_PK(string pSCHEDULE_UUID,string pschedule_time_uuid,DB db){
			try{
				IList<VScheduleTime_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.SCHEDULE_UUID,pSCHEDULE_UUID)
									.Equal(this.SCHEDULE_TIME_UUID,pschedule_time_uuid)
				).FetchAll<VScheduleTime_Record>(db)  ;  
				return ret.First();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		//TEMPLATE TABLE 20130319045
		public VScheduleTime Fill_By_ScheduleUuid_And_ScheduleTimeUuid(string pSCHEDULE_UUID,string pschedule_time_uuid){
			try{
				IList<VScheduleTime_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.SCHEDULE_UUID,pSCHEDULE_UUID)
									.Equal(this.SCHEDULE_TIME_UUID,pschedule_time_uuid)
				).FetchAll<VScheduleTime_Record>()  ;  
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
		public VScheduleTime Fill_By_ScheduleUuid_And_ScheduleTimeUuid(string pSCHEDULE_UUID,string pschedule_time_uuid,DB db){
			try{
				IList<VScheduleTime_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.SCHEDULE_UUID,pSCHEDULE_UUID)
									.Equal(this.SCHEDULE_TIME_UUID,pschedule_time_uuid)
				).FetchAll<VScheduleTime_Record>(db)  ;  
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
		public VScheduleTime_Record Fetch_By_ScheduleUuid_And_ScheduleTimeUuid(string pSCHEDULE_UUID,string pschedule_time_uuid){
			try{
				IList<VScheduleTime_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.SCHEDULE_UUID,pSCHEDULE_UUID)
									.Equal(this.SCHEDULE_TIME_UUID,pschedule_time_uuid)
				).FetchAll<VScheduleTime_Record>()  ;  
				return ret.First();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.ErrorNoThrowException(this, ex);
				return null;
			}
		}
		//TEMPLATE TABLE 20130319048
		public VScheduleTime_Record Fetch_By_ScheduleUuid_And_ScheduleTimeUuid(string pSCHEDULE_UUID,string pschedule_time_uuid,DB db){
			try{
				IList<VScheduleTime_Record> ret = null;
				ret = this.Where(
				new SQLCondition(this)
									.Equal(this.SCHEDULE_UUID,pSCHEDULE_UUID)
									.Equal(this.SCHEDULE_TIME_UUID,pschedule_time_uuid)
				).FetchAll<VScheduleTime_Record>(db)  ;  
				return ret.First();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*依照資料表與資料表的關係，產生出來的方法*/
		public List<ScheduleTime_Record> Link_ScheduleTime_By_Uuid()
		{
			try{
				List<ScheduleTime_Record> ret= new List<ScheduleTime_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				ScheduleTime ___table = new ScheduleTime(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.UUID,item.SCHEDULE_TIME_UUID).R().Or()  ; 
 				}
				condition.CheckSQL();
				ret=(List<ScheduleTime_Record>)
						___table.Where(condition)
						.FetchAll<ScheduleTime_Record>() ; 
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
		public List<ScheduleTime_Record> Link_ScheduleTime_By_Uuid(OrderLimit limit)
		{
			try{
				List<ScheduleTime_Record> ret= new List<ScheduleTime_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				ScheduleTime ___table = new ScheduleTime(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.UUID,item.SCHEDULE_TIME_UUID).R().Or()  ; 
 				}
				condition.CheckSQL();
				ret=(List<ScheduleTime_Record>)
						___table.Where(condition)
						.Order(limit)
						.Limit(limit)
						.FetchAll<ScheduleTime_Record>() ; 
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
		/*201303180336*/
		public ScheduleTime LinkFill_ScheduleTime_By_Uuid()
		{
			try{
				var data = Link_ScheduleTime_By_Uuid();
				ScheduleTime ret=new ScheduleTime(data);
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
		public ScheduleTime LinkFill_ScheduleTime_By_Uuid(OrderLimit limit)
		{
			try{
				var data = Link_ScheduleTime_By_Uuid(limit);
				ScheduleTime ret=new ScheduleTime(data);
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
