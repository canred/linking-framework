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
	[TableView("DEPARTMENT", true)]
	[LkDataBase("BASIC")]
	[Serializable]
	public class Department_Record : RecordBase{
		public Department_Record(){}
		/*欄位資訊 Start*/
		string _UUID=null;
		DateTime? _CREATE_DATE=null;
		DateTime? _UPDATE_DATE=null;
		string _IS_ACTIVE=null;
		string _COMPANY_UUID=null;
		string _ID=null;
		string _C_NAME=null;
		string _E_NAME=null;
		string _PARENT_DEPARTMENT_UUID=null;
		string _MANAGER_UUID=null;
		string _PARENT_DEPARTMENT_ID=null;
		string _MANAGER_ID=null;
		string _PARENT_DEPARTMENT_UUID_LIST=null;
		string _S_NAME=null;
		string _COST_CENTER=null;
		string _SRC_UUID=null;
		string _FULL_DEPARTMENT_NAME=null;
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

		[ColumnName("PARENT_DEPARTMENT_UUID",false,typeof(string))]
		public string PARENT_DEPARTMENT_UUID
		{
			set
			{
				_PARENT_DEPARTMENT_UUID=value;
			}
			get
			{
				return _PARENT_DEPARTMENT_UUID;
			}
		}

		[ColumnName("MANAGER_UUID",false,typeof(string))]
		public string MANAGER_UUID
		{
			set
			{
				_MANAGER_UUID=value;
			}
			get
			{
				return _MANAGER_UUID;
			}
		}

		[ColumnName("PARENT_DEPARTMENT_ID",false,typeof(string))]
		public string PARENT_DEPARTMENT_ID
		{
			set
			{
				_PARENT_DEPARTMENT_ID=value;
			}
			get
			{
				return _PARENT_DEPARTMENT_ID;
			}
		}

		[ColumnName("MANAGER_ID",false,typeof(string))]
		public string MANAGER_ID
		{
			set
			{
				_MANAGER_ID=value;
			}
			get
			{
				return _MANAGER_ID;
			}
		}

		[ColumnName("PARENT_DEPARTMENT_UUID_LIST",false,typeof(string))]
		public string PARENT_DEPARTMENT_UUID_LIST
		{
			set
			{
				_PARENT_DEPARTMENT_UUID_LIST=value;
			}
			get
			{
				return _PARENT_DEPARTMENT_UUID_LIST;
			}
		}

		[ColumnName("S_NAME",false,typeof(string))]
		public string S_NAME
		{
			set
			{
				_S_NAME=value;
			}
			get
			{
				return _S_NAME;
			}
		}

		[ColumnName("COST_CENTER",false,typeof(string))]
		public string COST_CENTER
		{
			set
			{
				_COST_CENTER=value;
			}
			get
			{
				return _COST_CENTER;
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

		[ColumnName("FULL_DEPARTMENT_NAME",false,typeof(string))]
		public string FULL_DEPARTMENT_NAME
		{
			set
			{
				_FULL_DEPARTMENT_NAME=value;
			}
			get
			{
				return _FULL_DEPARTMENT_NAME;
			}
		}
		public Department_Record Clone(){
			try{
				return this.Clone<Department_Record>(this);
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		public Department gotoTable(){
			try{
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Department ret = new Department(dbc,this);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180347*/
		public List<Attendant_Record> Link_Attendant_By_DepartmentUuid()
		{
			try{
				List<Attendant_Record> ret= new List<Attendant_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Attendant ___table = new Attendant(dbc);
				ret=(List<Attendant_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.DEPARTMENT_UUID,this.UUID))
					.FetchAll<Attendant_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180347*/
		public List<Department_Record> Link_Department_By_ParentDepartmentUuid()
		{
			try{
				List<Department_Record> ret= new List<Department_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Department ___table = new Department(dbc);
				ret=(List<Department_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.PARENT_DEPARTMENT_UUID,this.UUID))
					.FetchAll<Department_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180348*/
		public List<Attendant_Record> Link_Attendant_By_DepartmentUuid(OrderLimit limit)
		{
			try{
				List<Attendant_Record> ret= new List<Attendant_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Attendant ___table = new Attendant(dbc);
				ret=(List<Attendant_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.DEPARTMENT_UUID,this.UUID))
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
		/*201303180348*/
		public List<Department_Record> Link_Department_By_ParentDepartmentUuid(OrderLimit limit)
		{
			try{
				List<Department_Record> ret= new List<Department_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Department ___table = new Department(dbc);
				ret=(List<Department_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.PARENT_DEPARTMENT_UUID,this.UUID))
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
		public List<Attendant_Record> Link_Attendant_By_Uuid()
		{
			try{
				List<Attendant_Record> ret= new List<Attendant_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Attendant ___table = new Attendant(dbc);
				ret=(List<Attendant_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.UUID,this.MANAGER_UUID))
					.FetchAll<Attendant_Record>() ; 
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
										.Equal(___table.UUID,this.PARENT_DEPARTMENT_UUID))
					.FetchAll<Department_Record>() ; 
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
										.Equal(___table.UUID,this.MANAGER_UUID))
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
										.Equal(___table.UUID,this.PARENT_DEPARTMENT_UUID))
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
		/*201303180357*/
		public Attendant LinkFill_Attendant_By_DepartmentUuid()
		{
			try{
				var data = Link_Attendant_By_DepartmentUuid();
				Attendant ret=new Attendant(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180357*/
		public Department LinkFill_Department_By_ParentDepartmentUuid()
		{
			try{
				var data = Link_Department_By_ParentDepartmentUuid();
				Department ret=new Department(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180358*/
		public Attendant LinkFill_Attendant_By_DepartmentUuid(OrderLimit limit)
		{
			try{
				var data = Link_Attendant_By_DepartmentUuid(limit);
				Attendant ret=new Attendant(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180358*/
		public Department LinkFill_Department_By_ParentDepartmentUuid(OrderLimit limit)
		{
			try{
				var data = Link_Department_By_ParentDepartmentUuid(limit);
				Department ret=new Department(data);
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
	}
}
