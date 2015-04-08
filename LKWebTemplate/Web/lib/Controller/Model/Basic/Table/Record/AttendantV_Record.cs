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
	[TableView("ATTENDANT_V", false)]
	[LkDataBase("BASIC")]
	[Serializable]
	public class AttendantV_Record : RecordBase{
		public AttendantV_Record(){}
		/*欄位資訊 Start*/
		string _COMPANY_ID=null;
		string _COMPANY_C_NAME=null;
		string _COMPANY_E_NAME=null;
		string _DEPARTMENT_ID=null;
		string _DEPARTMENT_C_NAME=null;
		string _DEPARTMENT_E_NAME=null;
		string _SITE_ID=null;
		string _SITE_C_NAME=null;
		string _SITE_E_NAME=null;
		string _UUID=null;
		DateTime? _CREATE_DATE=null;
		DateTime? _UPDATE_DATE=null;
		string _IS_ACTIVE=null;
		string _COMPANY_UUID=null;
		string _ACCOUNT=null;
		string _C_NAME=null;
		string _E_NAME=null;
		string _EMAIL=null;
		string _PASSWORD=null;
		string _IS_SUPPER=null;
		string _IS_ADMIN=null;
		string _CODE_PAGE=null;
		string _DEPARTMENT_UUID=null;
		string _PHONE=null;
		string _SITE_UUID=null;
		string _GENDER=null;
		DateTime? _BIRTHDAY=null;
		DateTime? _HIRE_DATE=null;
		DateTime? _QUIT_DATE=null;
		string _IS_MANAGER=null;
		string _IS_DIRECT=null;
		string _GRADE=null;
		string _ID=null;
		string _IS_DEFAULT_PASS=null;
		string _PICTURE_URL=null;
		/*欄位資訊 End*/

		[ColumnName("COMPANY_ID",false,typeof(string))]
		public string COMPANY_ID
		{
			set
			{
				_COMPANY_ID=value;
			}
			get
			{
				return _COMPANY_ID;
			}
		}

		[ColumnName("COMPANY_C_NAME",false,typeof(string))]
		public string COMPANY_C_NAME
		{
			set
			{
				_COMPANY_C_NAME=value;
			}
			get
			{
				return _COMPANY_C_NAME;
			}
		}

		[ColumnName("COMPANY_E_NAME",false,typeof(string))]
		public string COMPANY_E_NAME
		{
			set
			{
				_COMPANY_E_NAME=value;
			}
			get
			{
				return _COMPANY_E_NAME;
			}
		}

		[ColumnName("DEPARTMENT_ID",false,typeof(string))]
		public string DEPARTMENT_ID
		{
			set
			{
				_DEPARTMENT_ID=value;
			}
			get
			{
				return _DEPARTMENT_ID;
			}
		}

		[ColumnName("DEPARTMENT_C_NAME",false,typeof(string))]
		public string DEPARTMENT_C_NAME
		{
			set
			{
				_DEPARTMENT_C_NAME=value;
			}
			get
			{
				return _DEPARTMENT_C_NAME;
			}
		}

		[ColumnName("DEPARTMENT_E_NAME",false,typeof(string))]
		public string DEPARTMENT_E_NAME
		{
			set
			{
				_DEPARTMENT_E_NAME=value;
			}
			get
			{
				return _DEPARTMENT_E_NAME;
			}
		}

		[ColumnName("SITE_ID",false,typeof(string))]
		public string SITE_ID
		{
			set
			{
				_SITE_ID=value;
			}
			get
			{
				return _SITE_ID;
			}
		}

		[ColumnName("SITE_C_NAME",false,typeof(string))]
		public string SITE_C_NAME
		{
			set
			{
				_SITE_C_NAME=value;
			}
			get
			{
				return _SITE_C_NAME;
			}
		}

		[ColumnName("SITE_E_NAME",false,typeof(string))]
		public string SITE_E_NAME
		{
			set
			{
				_SITE_E_NAME=value;
			}
			get
			{
				return _SITE_E_NAME;
			}
		}

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

		[ColumnName("EMAIL",false,typeof(string))]
		public string EMAIL
		{
			set
			{
				_EMAIL=value;
			}
			get
			{
				return _EMAIL;
			}
		}

		[ColumnName("PASSWORD",false,typeof(string))]
		public string PASSWORD
		{
			set
			{
				_PASSWORD=value;
			}
			get
			{
				return _PASSWORD;
			}
		}

		[ColumnName("IS_SUPPER",false,typeof(string))]
		public string IS_SUPPER
		{
			set
			{
				_IS_SUPPER=value;
			}
			get
			{
				return _IS_SUPPER;
			}
		}

		[ColumnName("IS_ADMIN",false,typeof(string))]
		public string IS_ADMIN
		{
			set
			{
				_IS_ADMIN=value;
			}
			get
			{
				return _IS_ADMIN;
			}
		}

		[ColumnName("CODE_PAGE",false,typeof(string))]
		public string CODE_PAGE
		{
			set
			{
				_CODE_PAGE=value;
			}
			get
			{
				return _CODE_PAGE;
			}
		}

		[ColumnName("DEPARTMENT_UUID",false,typeof(string))]
		public string DEPARTMENT_UUID
		{
			set
			{
				_DEPARTMENT_UUID=value;
			}
			get
			{
				return _DEPARTMENT_UUID;
			}
		}

		[ColumnName("PHONE",false,typeof(string))]
		public string PHONE
		{
			set
			{
				_PHONE=value;
			}
			get
			{
				return _PHONE;
			}
		}

		[ColumnName("SITE_UUID",false,typeof(string))]
		public string SITE_UUID
		{
			set
			{
				_SITE_UUID=value;
			}
			get
			{
				return _SITE_UUID;
			}
		}

		[ColumnName("GENDER",false,typeof(string))]
		public string GENDER
		{
			set
			{
				_GENDER=value;
			}
			get
			{
				return _GENDER;
			}
		}

		[ColumnName("BIRTHDAY",false,typeof(DateTime?))]
		public DateTime? BIRTHDAY
		{
			set
			{
				_BIRTHDAY=value;
			}
			get
			{
				return _BIRTHDAY;
			}
		}

		[ColumnName("HIRE_DATE",false,typeof(DateTime?))]
		public DateTime? HIRE_DATE
		{
			set
			{
				_HIRE_DATE=value;
			}
			get
			{
				return _HIRE_DATE;
			}
		}

		[ColumnName("QUIT_DATE",false,typeof(DateTime?))]
		public DateTime? QUIT_DATE
		{
			set
			{
				_QUIT_DATE=value;
			}
			get
			{
				return _QUIT_DATE;
			}
		}

		[ColumnName("IS_MANAGER",false,typeof(string))]
		public string IS_MANAGER
		{
			set
			{
				_IS_MANAGER=value;
			}
			get
			{
				return _IS_MANAGER;
			}
		}

		[ColumnName("IS_DIRECT",false,typeof(string))]
		public string IS_DIRECT
		{
			set
			{
				_IS_DIRECT=value;
			}
			get
			{
				return _IS_DIRECT;
			}
		}

		[ColumnName("GRADE",false,typeof(string))]
		public string GRADE
		{
			set
			{
				_GRADE=value;
			}
			get
			{
				return _GRADE;
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

		[ColumnName("IS_DEFAULT_PASS",false,typeof(string))]
		public string IS_DEFAULT_PASS
		{
			set
			{
				_IS_DEFAULT_PASS=value;
			}
			get
			{
				return _IS_DEFAULT_PASS;
			}
		}

		[ColumnName("PICTURE_URL",false,typeof(string))]
		public string PICTURE_URL
		{
			set
			{
				_PICTURE_URL=value;
			}
			get
			{
				return _PICTURE_URL;
			}
		}
		public AttendantV_Record Clone(){
			try{
				return this.Clone<AttendantV_Record>(this);
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		public AttendantV gotoTable(){
			try{
				var dbc = LK.Config.DataBase.Factory.getInfo();
				AttendantV ret = new AttendantV(dbc,this);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
	}
}
