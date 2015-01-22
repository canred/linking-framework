using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LK.Attribute;
using LK.DB;  
using LK.DB.SQLCreater;  
using LKWebTemplate.Model.Basic.Table;
namespace LKWebTemplate.Model.Basic.Table.Record
{
	[LkRecord]
	[TableView("V_SCHEDULE_TIME", false)]
	[LkDataBase("BASIC")]
	[Serializable]
	public class VScheduleTime_Record : RecordBase{
		public VScheduleTime_Record(){}
		/*欄位資訊 Start*/
		string _SCHEDULE_UUID=null;
		int? _C_DAY=null;
		string _C_DAY_OF_MONTH=null;
		string _C_DAY_OF_WEEK=null;
		int? _C_HOUR=null;
		int? _C_MINUTE=null;
		int? _C_MONTH=null;
		int? _C_WEEK=null;
		string _C_WEEK_OF_MONTH=null;
		string _C_WEEK_OF_YEAR=null;
		int? _C_YEAR=null;
		string _CYCLE_TYPE=null;
		string _HOUR=null;
		string _IS_ACTIVE=null;
		string _IS_CYCLE=null;
		string _LAST_RUN_STATUS=null;
		DateTime? _LAST_RUN_TIME=null;
		string _MINUTE=null;
		string _RUN_ATTENDANT_UUID=null;
		string _RUN_URL=null;
		string _RUN_URL_PARAMETER=null;
		DateTime? _SCHEDULE_END_DATE=null;
		string _SCHEDULE_NAME=null;
		DateTime? _SINGLE_DATE=null;
		string _SCHEDULE_TIME_UUID=null;
		string _STATUS=null;
		DateTime? _START_TIME=null;
		DateTime? _FINISH_TIME=null;
		/*欄位資訊 End*/

		[ColumnName("SCHEDULE_UUID",true,typeof(string))]
		public string SCHEDULE_UUID
		{
			set
			{
				_SCHEDULE_UUID=value;
			}
			get
			{
				return _SCHEDULE_UUID;
			}
		}

		[ColumnName("C_DAY",false,typeof(int?))]
		public int? C_DAY
		{
			set
			{
				_C_DAY=value;
			}
			get
			{
				return _C_DAY;
			}
		}

		[ColumnName("C_DAY_OF_MONTH",false,typeof(string))]
		public string C_DAY_OF_MONTH
		{
			set
			{
				_C_DAY_OF_MONTH=value;
			}
			get
			{
				return _C_DAY_OF_MONTH;
			}
		}

		[ColumnName("C_DAY_OF_WEEK",false,typeof(string))]
		public string C_DAY_OF_WEEK
		{
			set
			{
				_C_DAY_OF_WEEK=value;
			}
			get
			{
				return _C_DAY_OF_WEEK;
			}
		}

		[ColumnName("C_HOUR",false,typeof(int?))]
		public int? C_HOUR
		{
			set
			{
				_C_HOUR=value;
			}
			get
			{
				return _C_HOUR;
			}
		}

		[ColumnName("C_MINUTE",false,typeof(int?))]
		public int? C_MINUTE
		{
			set
			{
				_C_MINUTE=value;
			}
			get
			{
				return _C_MINUTE;
			}
		}

		[ColumnName("C_MONTH",false,typeof(int?))]
		public int? C_MONTH
		{
			set
			{
				_C_MONTH=value;
			}
			get
			{
				return _C_MONTH;
			}
		}

		[ColumnName("C_WEEK",false,typeof(int?))]
		public int? C_WEEK
		{
			set
			{
				_C_WEEK=value;
			}
			get
			{
				return _C_WEEK;
			}
		}

		[ColumnName("C_WEEK_OF_MONTH",false,typeof(string))]
		public string C_WEEK_OF_MONTH
		{
			set
			{
				_C_WEEK_OF_MONTH=value;
			}
			get
			{
				return _C_WEEK_OF_MONTH;
			}
		}

		[ColumnName("C_WEEK_OF_YEAR",false,typeof(string))]
		public string C_WEEK_OF_YEAR
		{
			set
			{
				_C_WEEK_OF_YEAR=value;
			}
			get
			{
				return _C_WEEK_OF_YEAR;
			}
		}

		[ColumnName("C_YEAR",false,typeof(int?))]
		public int? C_YEAR
		{
			set
			{
				_C_YEAR=value;
			}
			get
			{
				return _C_YEAR;
			}
		}

		[ColumnName("CYCLE_TYPE",false,typeof(string))]
		public string CYCLE_TYPE
		{
			set
			{
				_CYCLE_TYPE=value;
			}
			get
			{
				return _CYCLE_TYPE;
			}
		}

		[ColumnName("HOUR",false,typeof(string))]
		public string HOUR
		{
			set
			{
				_HOUR=value;
			}
			get
			{
				return _HOUR;
			}
		}

		[ColumnName("IS_ACTIVE",false,typeof(string))]
		public string IS_ACTIVE
		{
			set
			{
				_IS_ACTIVE=value;
			}
			get
			{
				return _IS_ACTIVE;
			}
		}

		[ColumnName("IS_CYCLE",false,typeof(string))]
		public string IS_CYCLE
		{
			set
			{
				_IS_CYCLE=value;
			}
			get
			{
				return _IS_CYCLE;
			}
		}

		[ColumnName("LAST_RUN_STATUS",false,typeof(string))]
		public string LAST_RUN_STATUS
		{
			set
			{
				_LAST_RUN_STATUS=value;
			}
			get
			{
				return _LAST_RUN_STATUS;
			}
		}

		[ColumnName("LAST_RUN_TIME",false,typeof(DateTime?))]
		public DateTime? LAST_RUN_TIME
		{
			set
			{
				_LAST_RUN_TIME=value;
			}
			get
			{
				return _LAST_RUN_TIME;
			}
		}

		[ColumnName("MINUTE",false,typeof(string))]
		public string MINUTE
		{
			set
			{
				_MINUTE=value;
			}
			get
			{
				return _MINUTE;
			}
		}

		[ColumnName("RUN_ATTENDANT_UUID",false,typeof(string))]
		public string RUN_ATTENDANT_UUID
		{
			set
			{
				_RUN_ATTENDANT_UUID=value;
			}
			get
			{
				return _RUN_ATTENDANT_UUID;
			}
		}

		[ColumnName("RUN_URL",false,typeof(string))]
		public string RUN_URL
		{
			set
			{
				_RUN_URL=value;
			}
			get
			{
				return _RUN_URL;
			}
		}

		[ColumnName("RUN_URL_PARAMETER",false,typeof(string))]
		public string RUN_URL_PARAMETER
		{
			set
			{
				_RUN_URL_PARAMETER=value;
			}
			get
			{
				return _RUN_URL_PARAMETER;
			}
		}

		[ColumnName("SCHEDULE_END_DATE",false,typeof(DateTime?))]
		public DateTime? SCHEDULE_END_DATE
		{
			set
			{
				_SCHEDULE_END_DATE=value;
			}
			get
			{
				return _SCHEDULE_END_DATE;
			}
		}

		[ColumnName("SCHEDULE_NAME",false,typeof(string))]
		public string SCHEDULE_NAME
		{
			set
			{
				_SCHEDULE_NAME=value;
			}
			get
			{
				return _SCHEDULE_NAME;
			}
		}

		[ColumnName("SINGLE_DATE",false,typeof(DateTime?))]
		public DateTime? SINGLE_DATE
		{
			set
			{
				_SINGLE_DATE=value;
			}
			get
			{
				return _SINGLE_DATE;
			}
		}

		[ColumnName("SCHEDULE_TIME_UUID",true,typeof(string))]
		public string SCHEDULE_TIME_UUID
		{
			set
			{
				_SCHEDULE_TIME_UUID=value;
			}
			get
			{
				return _SCHEDULE_TIME_UUID;
			}
		}

		[ColumnName("STATUS",false,typeof(string))]
		public string STATUS
		{
			set
			{
				_STATUS=value;
			}
			get
			{
				return _STATUS;
			}
		}

		[ColumnName("START_TIME",false,typeof(DateTime?))]
		public DateTime? START_TIME
		{
			set
			{
				_START_TIME=value;
			}
			get
			{
				return _START_TIME;
			}
		}

		[ColumnName("FINISH_TIME",false,typeof(DateTime?))]
		public DateTime? FINISH_TIME
		{
			set
			{
				_FINISH_TIME=value;
			}
			get
			{
				return _FINISH_TIME;
			}
		}
		public VScheduleTime_Record Clone(){
			try{
				return this.Clone<VScheduleTime_Record>(this);
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		public VScheduleTime gotoTable(){
			try{
				var dbc = LK.Config.DataBase.Factory.getInfo();
				VScheduleTime ret = new VScheduleTime(dbc,this);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		public List<ScheduleTime_Record> Link_ScheduleTime_By_Uuid()
		{
			try{
				List<ScheduleTime_Record> ret= new List<ScheduleTime_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				ScheduleTime ___table = new ScheduleTime(dbc);
				ret=(List<ScheduleTime_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.UUID,this.SCHEDULE_TIME_UUID))
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
				ret=(List<Schedule_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.UUID,this.SCHEDULE_UUID))
					.FetchAll<Schedule_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180404*/
		public List<ScheduleTime_Record> Link_ScheduleTime_By_Uuid(OrderLimit limit)
		{
			try{
				List<ScheduleTime_Record> ret= new List<ScheduleTime_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				ScheduleTime ___table = new ScheduleTime(dbc);
				ret=(List<ScheduleTime_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.UUID,this.SCHEDULE_TIME_UUID))
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
		/*201303180404*/
		public List<Schedule_Record> Link_Schedule_By_Uuid(OrderLimit limit)
		{
			try{
				List<Schedule_Record> ret= new List<Schedule_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Schedule ___table = new Schedule(dbc);
				ret=(List<Schedule_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.UUID,this.SCHEDULE_UUID))
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
		/*2013031800428*/
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
		/*2013031800428*/
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
		/*201303180429*/
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
		/*201303180429*/
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
