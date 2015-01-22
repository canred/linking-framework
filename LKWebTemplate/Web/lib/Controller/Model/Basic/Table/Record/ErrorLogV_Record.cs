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
	[TableView("ERROR_LOG_V", false)]
	[LkDataBase("BASIC")]
	[Serializable]
	public class ErrorLogV_Record : RecordBase{
		public ErrorLogV_Record(){}
		/*欄位資訊 Start*/
		string _UUID=null;
		DateTime? _CREATE_DATE=null;
		DateTime? _UPDATE_DATE=null;
		string _IS_ACTIVE=null;
		string _ERROR_CODE=null;
		string _ERROR_TIME=null;
		string _ERROR_MESSAGE=null;
		string _APPLICATION_NAME=null;
		string _ATTENDANT_UUID=null;
		string _ERROR_TYPE=null;
		string _IS_READ=null;
		string _C_NAME=null;
		string _E_NAME=null;
		string _ID=null;
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
				_APPLICATION_NAME=value;
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

		[ColumnName("IS_READ",false,typeof(string))]
		public string IS_READ
		{
			set
			{
				_IS_READ=value;
			}
			get
			{
				return _IS_READ;
			}
		}

		[ColumnName("C_NAME",false,typeof(string))]
		public string C_NAME
		{
			set
			{
				_C_NAME=value;
			}
			get
			{
				return _C_NAME;
			}
		}

		[ColumnName("E_NAME",false,typeof(string))]
		public string E_NAME
		{
			set
			{
				_E_NAME=value;
			}
			get
			{
				return _E_NAME;
			}
		}

		[ColumnName("ID",false,typeof(string))]
		public string ID
		{
			set
			{
				_ID=value;
			}
			get
			{
				return _ID;
			}
		}
		public ErrorLogV_Record Clone(){
			try{
				return this.Clone<ErrorLogV_Record>(this);
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		public ErrorLogV gotoTable(){
			try{
				var dbc = LK.Config.DataBase.Factory.getInfo();
				ErrorLogV ret = new ErrorLogV(dbc,this);
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
				ret=(List<Attendant_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.UUID,this.ATTENDANT_UUID))
					.FetchAll<Attendant_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180404*/
		public List<Attendant_Record> Link_Attendant_By_Uuid(OrderLimit limit)
		{
			try{
				List<Attendant_Record> ret= new List<Attendant_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Attendant ___table = new Attendant(dbc);
				ret=(List<Attendant_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.UUID,this.ATTENDANT_UUID))
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
		/*2013031800428*/
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
		/*201303180429*/
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
