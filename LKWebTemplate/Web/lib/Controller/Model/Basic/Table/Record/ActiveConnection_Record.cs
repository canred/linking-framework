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
	[TableView("ACTIVE_CONNECTION", true)]
	[LkDataBase("BASIC")]
	[Serializable]
	public class ActiveConnection_Record : RecordBase{
		public ActiveConnection_Record(){}
		/*欄位資訊 Start*/
		string _UUID=null;
		string _ACCOUNT=null;
		string _COMPANY_UUID=null;
		string _IP=null;
		string _APPLICATION=null;
		DateTime? _STARTTIME=null;
		DateTime? _EXPIRESTIME=null;
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

		[ColumnName("ACCOUNT",false,typeof(string))]
		public string ACCOUNT
		{
			set
			{
				_ACCOUNT=value;
			}
			get
			{
				return _ACCOUNT;
			}
		}

		[ColumnName("COMPANY_UUID",false,typeof(string))]
		public string COMPANY_UUID
		{
			set
			{
				_COMPANY_UUID=value;
			}
			get
			{
				return _COMPANY_UUID;
			}
		}

		[ColumnName("IP",false,typeof(string))]
		public string IP
		{
			set
			{
				_IP=value;
			}
			get
			{
				return _IP;
			}
		}

		[ColumnName("APPLICATION",false,typeof(string))]
		public string APPLICATION
		{
			set
			{
				_APPLICATION=value;
			}
			get
			{
				return _APPLICATION;
			}
		}

		[ColumnName("STARTTIME",false,typeof(DateTime?))]
		public DateTime? STARTTIME
		{
			set
			{
				_STARTTIME=value;
			}
			get
			{
				return _STARTTIME;
			}
		}

		[ColumnName("EXPIRESTIME",false,typeof(DateTime?))]
		public DateTime? EXPIRESTIME
		{
			set
			{
				_EXPIRESTIME=value;
			}
			get
			{
				return _EXPIRESTIME;
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
		public ActiveConnection_Record Clone(){
			try{
				return this.Clone<ActiveConnection_Record>(this);
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		public ActiveConnection gotoTable(){
			try{
				var dbc = LK.Config.DataBase.Factory.getInfo();
				ActiveConnection ret = new ActiveConnection(dbc,this);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
	}
}
