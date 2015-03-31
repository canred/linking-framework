using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LK.Attribute;
using LK.DB;
using LK.DB.SQLCreater;
using LK.MyException.Table;
namespace LK.MyException.Table.Record
{
    [LkRecord]
    [TableView("ERROR_LOG", true)]
    [LkDataBase("MyException")]
	[Serializable]
	public class ErrorLog_Record : RecordBase{
		public ErrorLog_Record(){}
		/*欄位資訊 Start*/
		string _UUID=null;
		DateTime? _CREATE_DATE=null;
		DateTime? _UPDATE_DATE=null;
		string _IS_ACTIVE=null;
		string _ERROR_CODE=null;
		string _ERROR_TIME=null;
		string _ERROR_MESSAGE=null;
        string _APPLICATION_NAME = null;
		string _ATTENDANT_UUID=null;
		string _ERROR_TYPE=null;
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
		[ColumnName("ERROR_CODE",false,typeof(string))]
		public string ERROR_CODE
		{
			set
			{
				_ERROR_CODE=value;
			}
			get
			{
				return _ERROR_CODE;
			}
		}
		[ColumnName("ERROR_TIME",false,typeof(string))]
		public string ERROR_TIME
		{
			set
			{
				_ERROR_TIME=value;
			}
			get
			{
				return _ERROR_TIME;
			}
		}
		[ColumnName("ERROR_MESSAGE",false,typeof(string))]
		public string ERROR_MESSAGE
		{
			set
			{
				_ERROR_MESSAGE=value;
			}
			get
			{
				return _ERROR_MESSAGE;
			}
		}
		[ColumnName("APPLICATION_NAME",false,typeof(string))]
        public string APPLICATION_NAME
		{
			set
			{
                _APPLICATION_NAME = value;
			}
			get
			{
                return _APPLICATION_NAME;
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
		[ColumnName("ERROR_TYPE",false,typeof(string))]
		public string ERROR_TYPE
		{
			set
			{
				_ERROR_TYPE=value;
			}
			get
			{
				return _ERROR_TYPE;
			}
		}
		public ErrorLog_Record Clone(){
			try{
				return this.Clone<ErrorLog_Record>(this);
			}
			catch (System.Exception ex){
				log.Error(ex);
				throw ex;
			}
		}
		public ErrorLog gotoTable(){
			try{
                var dbc = LK.Config.DataBase.Factory.getInfo();
				ErrorLog ret = new ErrorLog(dbc,this);
				return ret;
			}
			catch (System.Exception ex){
				log.Error(ex);
				throw ex;
			}
		}
	}
}
