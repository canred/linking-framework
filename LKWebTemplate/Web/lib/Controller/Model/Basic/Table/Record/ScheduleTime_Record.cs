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
	[TableView("SCHEDULE_TIME", true)]
	[LkDataBase("BASIC")]
	[Serializable]
	public class ScheduleTime_Record : RecordBase{
		public ScheduleTime_Record(){}
		/*欄位資訊 Start*/
		string _UUID=null;
		string _SCHEDULE_UUID=null;
		DateTime? _START_TIME=null;
		DateTime? _FINISH_TIME=null;
		string _STATUS=null;
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

		[ColumnName("SCHEDULE_UUID",false,typeof(string))]
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
		public ScheduleTime_Record Clone(){
			try{
				return this.Clone<ScheduleTime_Record>(this);
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		public ScheduleTime gotoTable(){
			try{
				var dbc = LK.Config.DataBase.Factory.getInfo();
				ScheduleTime ret = new ScheduleTime(dbc,this);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180347*/
		public List<VScheduleTime_Record> Link_VScheduleTime_By_ScheduleTimeUuid()
		{
			try{
				List<VScheduleTime_Record> ret= new List<VScheduleTime_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				VScheduleTime ___table = new VScheduleTime(dbc);
				ret=(List<VScheduleTime_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.SCHEDULE_TIME_UUID,this.UUID))
					.FetchAll<VScheduleTime_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180348*/
		public List<VScheduleTime_Record> Link_VScheduleTime_By_ScheduleTimeUuid(OrderLimit limit)
		{
			try{
				List<VScheduleTime_Record> ret= new List<VScheduleTime_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				VScheduleTime ___table = new VScheduleTime(dbc);
				ret=(List<VScheduleTime_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.SCHEDULE_TIME_UUID,this.UUID))
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
		/*201303180357*/
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
		/*201303180358*/
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
