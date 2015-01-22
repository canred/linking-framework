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
	[TableView("SCHEDULE", true)]
	[LkDataBase("BASIC")]
	[Serializable]
	public class Schedule_Record : RecordBase{
		public Schedule_Record(){}
		/*欄位資訊 Start*/
		string _UUID=null;
		string _SCHEDULE_NAME=null;
		DateTime? _SCHEDULE_END_DATE=null;
		DateTime? _LAST_RUN_TIME=null;
		string _LAST_RUN_STATUS=null;
		string _IS_CYCLE=null;
		DateTime? _SINGLE_DATE=null;
		string _HOUR=null;
		string _MINUTE=null;
		string _CYCLE_TYPE=null;
		int? _C_MINUTE=null;
		int? _C_HOUR=null;
		int? _C_DAY=null;
		int? _C_WEEK=null;
		string _C_DAY_OF_WEEK=null;
		int? _C_MONTH=null;
		string _C_DAY_OF_MONTH=null;
		string _C_WEEK_OF_MONTH=null;
		int? _C_YEAR=null;
		string _C_WEEK_OF_YEAR=null;
		string _RUN_URL=null;
		string _RUN_URL_PARAMETER=null;
		string _RUN_ATTENDANT_UUID=null;
		string _IS_ACTIVE=null;
		DateTime? _START_DATE=null;
		string _RUN_SECURITY=null;
		string _EXPEND_ALL=null;
		DateTime? _CONTIUNE_DATETIME=null;
		/*欄位資訊 End*/

		[ColumnName("UUID",true,typeof(string))]
		public string UUID
		{
			set
			{
				_UUID=value;
			}
			get
			{
				return _UUID;
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

		[ColumnName("START_DATE",false,typeof(DateTime?))]
		public DateTime? START_DATE
		{
			set
			{
				_START_DATE=value;
			}
			get
			{
				return _START_DATE;
			}
		}

		[ColumnName("RUN_SECURITY",false,typeof(string))]
		public string RUN_SECURITY
		{
			set
			{
				_RUN_SECURITY=value;
			}
			get
			{
				return _RUN_SECURITY;
			}
		}

		[ColumnName("EXPEND_ALL",false,typeof(string))]
		public string EXPEND_ALL
		{
			set
			{
				_EXPEND_ALL=value;
			}
			get
			{
				return _EXPEND_ALL;
			}
		}

		[ColumnName("CONTIUNE_DATETIME",false,typeof(DateTime?))]
		public DateTime? CONTIUNE_DATETIME
		{
			set
			{
				_CONTIUNE_DATETIME=value;
			}
			get
			{
				return _CONTIUNE_DATETIME;
			}
		}
		public Schedule_Record Clone(){
			try{
				return this.Clone<Schedule_Record>(this);
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		public Schedule gotoTable(){
			try{
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Schedule ret = new Schedule(dbc,this);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180347*/
		public List<VScheduleTime_Record> Link_VScheduleTime_By_ScheduleUuid()
		{
			try{
				List<VScheduleTime_Record> ret= new List<VScheduleTime_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				VScheduleTime ___table = new VScheduleTime(dbc);
				ret=(List<VScheduleTime_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.SCHEDULE_UUID,this.UUID))
					.FetchAll<VScheduleTime_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180347*/
		public List<ScheduleTime_Record> Link_ScheduleTime_By_ScheduleUuid()
		{
			try{
				List<ScheduleTime_Record> ret= new List<ScheduleTime_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				ScheduleTime ___table = new ScheduleTime(dbc);
				ret=(List<ScheduleTime_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.SCHEDULE_UUID,this.UUID))
					.FetchAll<ScheduleTime_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180348*/
		public List<VScheduleTime_Record> Link_VScheduleTime_By_ScheduleUuid(OrderLimit limit)
		{
			try{
				List<VScheduleTime_Record> ret= new List<VScheduleTime_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				VScheduleTime ___table = new VScheduleTime(dbc);
				ret=(List<VScheduleTime_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.SCHEDULE_UUID,this.UUID))
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
		/*201303180348*/
		public List<ScheduleTime_Record> Link_ScheduleTime_By_ScheduleUuid(OrderLimit limit)
		{
			try{
				List<ScheduleTime_Record> ret= new List<ScheduleTime_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				ScheduleTime ___table = new ScheduleTime(dbc);
				ret=(List<ScheduleTime_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.SCHEDULE_UUID,this.UUID))
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
		/*201303180357*/
		public VScheduleTime LinkFill_VScheduleTime_By_ScheduleUuid()
		{
			try{
				var data = Link_VScheduleTime_By_ScheduleUuid();
				VScheduleTime ret=new VScheduleTime(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180357*/
		public ScheduleTime LinkFill_ScheduleTime_By_ScheduleUuid()
		{
			try{
				var data = Link_ScheduleTime_By_ScheduleUuid();
				ScheduleTime ret=new ScheduleTime(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180358*/
		public VScheduleTime LinkFill_VScheduleTime_By_ScheduleUuid(OrderLimit limit)
		{
			try{
				var data = Link_VScheduleTime_By_ScheduleUuid(limit);
				VScheduleTime ret=new VScheduleTime(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180358*/
		public ScheduleTime LinkFill_ScheduleTime_By_ScheduleUuid(OrderLimit limit)
		{
			try{
				var data = Link_ScheduleTime_By_ScheduleUuid(limit);
				ScheduleTime ret=new ScheduleTime(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
	}
}
