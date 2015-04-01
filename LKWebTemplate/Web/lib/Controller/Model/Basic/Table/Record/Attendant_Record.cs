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
	[TableView("ATTENDANT", true)]
	[LkDataBase("BASIC")]
	[Serializable]
	public class Attendant_Record : RecordBase{
		public Attendant_Record(){}
		/*欄位資訊 Start*/
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
		string _SRC_UUID=null;
		string _IS_DEFAULT_PASS=null;
		string _PICTURE_URL=null;
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

		[ColumnName("SRC_UUID",false,typeof(string))]
		public string SRC_UUID
		{
			set
			{
				_SRC_UUID=value;
			}
			get
			{
				return _SRC_UUID;
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
		public Attendant_Record Clone(){
			try{
				return this.Clone<Attendant_Record>(this);
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		public Attendant gotoTable(){
			try{
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Attendant ret = new Attendant(dbc,this);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180347*/
		public List<ErrorLog_Record> Link_ErrorLog_By_AttendantUuid()
		{
			try{
				List<ErrorLog_Record> ret= new List<ErrorLog_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				ErrorLog ___table = new ErrorLog(dbc);
				ret=(List<ErrorLog_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.ATTENDANT_UUID,this.UUID))
					.FetchAll<ErrorLog_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180347*/
		public List<ErrorLogV_Record> Link_ErrorLogV_By_AttendantUuid()
		{
			try{
				List<ErrorLogV_Record> ret= new List<ErrorLogV_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				ErrorLogV ___table = new ErrorLogV(dbc);
				ret=(List<ErrorLogV_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.ATTENDANT_UUID,this.UUID))
					.FetchAll<ErrorLogV_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180347*/
		public List<GroupAttendant_Record> Link_GroupAttendant_By_AttendantUuid()
		{
			try{
				List<GroupAttendant_Record> ret= new List<GroupAttendant_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				GroupAttendant ___table = new GroupAttendant(dbc);
				ret=(List<GroupAttendant_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.ATTENDANT_UUID,this.UUID))
					.FetchAll<GroupAttendant_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180347*/
		public List<Department_Record> Link_Department_By_ManagerUuid()
		{
			try{
				List<Department_Record> ret= new List<Department_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Department ___table = new Department(dbc);
				ret=(List<Department_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.MANAGER_UUID,this.UUID))
					.FetchAll<Department_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180347*/
		public List<GroupAttendantV_Record> Link_GroupAttendantV_By_AttendantUuid()
		{
			try{
				List<GroupAttendantV_Record> ret= new List<GroupAttendantV_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				GroupAttendantV ___table = new GroupAttendantV(dbc);
				ret=(List<GroupAttendantV_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.ATTENDANT_UUID,this.UUID))
					.FetchAll<GroupAttendantV_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180347*/
		public List<VAuthProxy_Record> Link_VAuthProxy_By_AttendantUuid()
		{
			try{
				List<VAuthProxy_Record> ret= new List<VAuthProxy_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				VAuthProxy ___table = new VAuthProxy(dbc);
				ret=(List<VAuthProxy_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.ATTENDANT_UUID,this.UUID))
					.FetchAll<VAuthProxy_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180348*/
		public List<ErrorLog_Record> Link_ErrorLog_By_AttendantUuid(OrderLimit limit)
		{
			try{
				List<ErrorLog_Record> ret= new List<ErrorLog_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				ErrorLog ___table = new ErrorLog(dbc);
				ret=(List<ErrorLog_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.ATTENDANT_UUID,this.UUID))
					.Order(limit)
					.Limit(limit)
					.FetchAll<ErrorLog_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180348*/
		public List<ErrorLogV_Record> Link_ErrorLogV_By_AttendantUuid(OrderLimit limit)
		{
			try{
				List<ErrorLogV_Record> ret= new List<ErrorLogV_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				ErrorLogV ___table = new ErrorLogV(dbc);
				ret=(List<ErrorLogV_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.ATTENDANT_UUID,this.UUID))
					.Order(limit)
					.Limit(limit)
					.FetchAll<ErrorLogV_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180348*/
		public List<GroupAttendant_Record> Link_GroupAttendant_By_AttendantUuid(OrderLimit limit)
		{
			try{
				List<GroupAttendant_Record> ret= new List<GroupAttendant_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				GroupAttendant ___table = new GroupAttendant(dbc);
				ret=(List<GroupAttendant_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.ATTENDANT_UUID,this.UUID))
					.Order(limit)
					.Limit(limit)
					.FetchAll<GroupAttendant_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180348*/
		public List<Department_Record> Link_Department_By_ManagerUuid(OrderLimit limit)
		{
			try{
				List<Department_Record> ret= new List<Department_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Department ___table = new Department(dbc);
				ret=(List<Department_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.MANAGER_UUID,this.UUID))
					.Order(limit)
					.Limit(limit)
					.FetchAll<Department_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180348*/
		public List<GroupAttendantV_Record> Link_GroupAttendantV_By_AttendantUuid(OrderLimit limit)
		{
			try{
				List<GroupAttendantV_Record> ret= new List<GroupAttendantV_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				GroupAttendantV ___table = new GroupAttendantV(dbc);
				ret=(List<GroupAttendantV_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.ATTENDANT_UUID,this.UUID))
					.Order(limit)
					.Limit(limit)
					.FetchAll<GroupAttendantV_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180348*/
		public List<VAuthProxy_Record> Link_VAuthProxy_By_AttendantUuid(OrderLimit limit)
		{
			try{
				List<VAuthProxy_Record> ret= new List<VAuthProxy_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				VAuthProxy ___table = new VAuthProxy(dbc);
				ret=(List<VAuthProxy_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.ATTENDANT_UUID,this.UUID))
					.Order(limit)
					.Limit(limit)
					.FetchAll<VAuthProxy_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		public List<Company_Record> Link_Company_By_Uuid()
		{
			try{
				List<Company_Record> ret= new List<Company_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Company ___table = new Company(dbc);
				ret=(List<Company_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.UUID,this.COMPANY_UUID))
					.FetchAll<Company_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		public List<Department_Record> Link_Department_By_Uuid()
		{
			try{
				List<Department_Record> ret= new List<Department_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Department ___table = new Department(dbc);
				ret=(List<Department_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.UUID,this.DEPARTMENT_UUID))
					.FetchAll<Department_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		public List<Site_Record> Link_Site_By_Uuid()
		{
			try{
				List<Site_Record> ret= new List<Site_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Site ___table = new Site(dbc);
				ret=(List<Site_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.UUID,this.SITE_UUID))
					.FetchAll<Site_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180404*/
		public List<Company_Record> Link_Company_By_Uuid(OrderLimit limit)
		{
			try{
				List<Company_Record> ret= new List<Company_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Company ___table = new Company(dbc);
				ret=(List<Company_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.UUID,this.COMPANY_UUID))
					.Order(limit)
					.Limit(limit)
					.FetchAll<Company_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180404*/
		public List<Department_Record> Link_Department_By_Uuid(OrderLimit limit)
		{
			try{
				List<Department_Record> ret= new List<Department_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Department ___table = new Department(dbc);
				ret=(List<Department_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.UUID,this.DEPARTMENT_UUID))
					.Order(limit)
					.Limit(limit)
					.FetchAll<Department_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180404*/
		public List<Site_Record> Link_Site_By_Uuid(OrderLimit limit)
		{
			try{
				List<Site_Record> ret= new List<Site_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Site ___table = new Site(dbc);
				ret=(List<Site_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.UUID,this.SITE_UUID))
					.Order(limit)
					.Limit(limit)
					.FetchAll<Site_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180357*/
		public ErrorLog LinkFill_ErrorLog_By_AttendantUuid()
		{
			try{
				var data = Link_ErrorLog_By_AttendantUuid();
				ErrorLog ret=new ErrorLog(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180357*/
		public ErrorLogV LinkFill_ErrorLogV_By_AttendantUuid()
		{
			try{
				var data = Link_ErrorLogV_By_AttendantUuid();
				ErrorLogV ret=new ErrorLogV(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180357*/
		public GroupAttendant LinkFill_GroupAttendant_By_AttendantUuid()
		{
			try{
				var data = Link_GroupAttendant_By_AttendantUuid();
				GroupAttendant ret=new GroupAttendant(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180357*/
		public Department LinkFill_Department_By_ManagerUuid()
		{
			try{
				var data = Link_Department_By_ManagerUuid();
				Department ret=new Department(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180357*/
		public GroupAttendantV LinkFill_GroupAttendantV_By_AttendantUuid()
		{
			try{
				var data = Link_GroupAttendantV_By_AttendantUuid();
				GroupAttendantV ret=new GroupAttendantV(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180357*/
		public VAuthProxy LinkFill_VAuthProxy_By_AttendantUuid()
		{
			try{
				var data = Link_VAuthProxy_By_AttendantUuid();
				VAuthProxy ret=new VAuthProxy(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180358*/
		public ErrorLog LinkFill_ErrorLog_By_AttendantUuid(OrderLimit limit)
		{
			try{
				var data = Link_ErrorLog_By_AttendantUuid(limit);
				ErrorLog ret=new ErrorLog(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180358*/
		public ErrorLogV LinkFill_ErrorLogV_By_AttendantUuid(OrderLimit limit)
		{
			try{
				var data = Link_ErrorLogV_By_AttendantUuid(limit);
				ErrorLogV ret=new ErrorLogV(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180358*/
		public GroupAttendant LinkFill_GroupAttendant_By_AttendantUuid(OrderLimit limit)
		{
			try{
				var data = Link_GroupAttendant_By_AttendantUuid(limit);
				GroupAttendant ret=new GroupAttendant(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180358*/
		public Department LinkFill_Department_By_ManagerUuid(OrderLimit limit)
		{
			try{
				var data = Link_Department_By_ManagerUuid(limit);
				Department ret=new Department(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180358*/
		public GroupAttendantV LinkFill_GroupAttendantV_By_AttendantUuid(OrderLimit limit)
		{
			try{
				var data = Link_GroupAttendantV_By_AttendantUuid(limit);
				GroupAttendantV ret=new GroupAttendantV(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180358*/
		public VAuthProxy LinkFill_VAuthProxy_By_AttendantUuid(OrderLimit limit)
		{
			try{
				var data = Link_VAuthProxy_By_AttendantUuid(limit);
				VAuthProxy ret=new VAuthProxy(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*2013031800428*/
		public Company LinkFill_Company_By_Uuid()
		{
			try{
				var data = Link_Company_By_Uuid();
				Company ret=new Company(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*2013031800428*/
		public Department LinkFill_Department_By_Uuid()
		{
			try{
				var data = Link_Department_By_Uuid();
				Department ret=new Department(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*2013031800428*/
		public Site LinkFill_Site_By_Uuid()
		{
			try{
				var data = Link_Site_By_Uuid();
				Site ret=new Site(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180429*/
		public Company LinkFill_Company_By_Uuid(OrderLimit limit)
		{
			try{
				var data = Link_Company_By_Uuid(limit);
				Company ret=new Company(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180429*/
		public Department LinkFill_Department_By_Uuid(OrderLimit limit)
		{
			try{
				var data = Link_Department_By_Uuid(limit);
				Department ret=new Department(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180429*/
		public Site LinkFill_Site_By_Uuid(OrderLimit limit)
		{
			try{
				var data = Link_Site_By_Uuid(limit);
				Site ret=new Site(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
	}
}
