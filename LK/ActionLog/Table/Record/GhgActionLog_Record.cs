using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LK.Attribute;
using LK.DB;
using LK.DB.SQLCreater;
using LK.ActionLog.Table;

namespace LK.ActionLog.Table.Record
{
    [LkRecord]
    [TableView("ACTION_LOG", true)]
    [LkDataBase("ActionLog")]
	[Serializable]
	public class ActionLog_Record : RecordBase{
		public ActionLog_Record(){}
		/*欄位資訊 Start*/
		string _UUID=null;
		string _CREATE_USER=null;
		DateTime? _CREATE_DATE=null;
		string _UPDATE_USER=null;
		DateTime? _UPDATE_DATE=null;
		string _IS_ACTIVE=null;
		string _ATTENDANT_UUID=null;
		string _CLASS_NAME=null;
		string _FUNCTION_NAME=null;
		string _PARAMETER=null;
		/*欄位資訊 End*/

		[ColumnName("UUID",false,typeof(string))]
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

		[ColumnName("CREATE_USER",false,typeof(string))]
		public string CREATE_USER
		{
			set
			{
				_CREATE_USER=value;
			}
			get
			{
				return _CREATE_USER;
			}
		}

		[ColumnName("CREATE_DATE",false,typeof(DateTime?))]
		public DateTime? CREATE_DATE
		{
			set
			{
				_CREATE_DATE=value;
			}
			get
			{
				return _CREATE_DATE;
			}
		}

		[ColumnName("UPDATE_USER",false,typeof(string))]
		public string UPDATE_USER
		{
			set
			{
				_UPDATE_USER=value;
			}
			get
			{
				return _UPDATE_USER;
			}
		}

		[ColumnName("UPDATE_DATE",false,typeof(DateTime?))]
		public DateTime? UPDATE_DATE
		{
			set
			{
				_UPDATE_DATE=value;
			}
			get
			{
				return _UPDATE_DATE;
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

		[ColumnName("ATTENDANT_UUID",false,typeof(string))]
		public string ATTENDANT_UUID
		{
			set
			{
				_ATTENDANT_UUID=value;
			}
			get
			{
				return _ATTENDANT_UUID;
			}
		}

		[ColumnName("CLASS_NAME",false,typeof(string))]
		public string CLASS_NAME
		{
			set
			{
				_CLASS_NAME=value;
			}
			get
			{
				return _CLASS_NAME;
			}
		}

		[ColumnName("FUNCTION_NAME",false,typeof(string))]
		public string FUNCTION_NAME
		{
			set
			{
				_FUNCTION_NAME=value;
			}
			get
			{
				return _FUNCTION_NAME;
			}
		}

		[ColumnName("PARAMETER",false,typeof(string))]
		public string PARAMETER
		{
			set
			{
				_PARAMETER=value;
			}
			get
			{
				return _PARAMETER;
			}
		}
        public ActionLog_Record Clone()
        {
			try{
                return this.Clone<ActionLog_Record>(this);
			}
			catch (Exception ex){
                log.Error(ex); LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		public ActionLog gotoTable(){
			try{
                var dbc = LK.Config.DataBase.Factory.getInfo();
				ActionLog ret = new ActionLog(dbc,this);
				return ret;
			}
			catch (Exception ex){
                log.Error(ex); LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
	}
}
