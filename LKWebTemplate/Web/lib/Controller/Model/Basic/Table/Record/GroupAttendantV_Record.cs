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
	[TableView("GROUP_ATTENDANT_V", false)]
	[LkDataBase("BASIC")]
	[Serializable]
	public class GroupAttendantV_Record : RecordBase{
		public GroupAttendantV_Record(){}
		/*欄位資訊 Start*/
		string _GROUP_NAME_ZH_TW=null;
		string _GROUP_NAME_ZH_CN=null;
		string _GROUP_NAME_EN_US=null;
		string _IS_GROUP_ACTIVE=null;
		string _COMPANY_UUID=null;
		string _COMPANY_ID=null;
		string _COMPANY_C_NAME=null;
		string _COMPANY_E_NAME=null;
		string _GROUP_ID=null;
		string _APPLICATION_HEAD_UUID=null;
		string _ATTENDANT_C_NAME=null;
		string _ATTENDANT_E_NAME=null;
		string _ACCOUNT=null;
		string _EMAIL=null;
		string _IS_ATTENDANT_ACTIVE=null;
		string _UUID=null;
		DateTime? _CREATE_DATE=null;
		DateTime? _UPDATE_DATE=null;
		string _IS_ACTIVE=null;
		string _GROUP_HEAD_UUID=null;
		string _ATTENDANT_UUID=null;
		string _DEPARTMENT_UUID=null;
		/*欄位資訊 End*/

		[ColumnName("GROUP_NAME_ZH_TW",false,typeof(string))]
		public string GROUP_NAME_ZH_TW
		{
			set
			{
				_GROUP_NAME_ZH_TW=value;
			}
			get
			{
				return _GROUP_NAME_ZH_TW;
			}
		}

		[ColumnName("GROUP_NAME_ZH_CN",false,typeof(string))]
		public string GROUP_NAME_ZH_CN
		{
			set
			{
				_GROUP_NAME_ZH_CN=value;
			}
			get
			{
				return _GROUP_NAME_ZH_CN;
			}
		}

		[ColumnName("GROUP_NAME_EN_US",false,typeof(string))]
		public string GROUP_NAME_EN_US
		{
			set
			{
				_GROUP_NAME_EN_US=value;
			}
			get
			{
				return _GROUP_NAME_EN_US;
			}
		}

		[ColumnName("IS_GROUP_ACTIVE",false,typeof(string))]
		public string IS_GROUP_ACTIVE
		{
			set
			{
				_IS_GROUP_ACTIVE=value;
			}
			get
			{
				return _IS_GROUP_ACTIVE;
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

		[ColumnName("GROUP_ID",false,typeof(string))]
		public string GROUP_ID
		{
			set
			{
				_GROUP_ID=value;
			}
			get
			{
				return _GROUP_ID;
			}
		}

		[ColumnName("APPLICATION_HEAD_UUID",false,typeof(string))]
		public string APPLICATION_HEAD_UUID
		{
			set
			{
				_APPLICATION_HEAD_UUID=value;
			}
			get
			{
				return _APPLICATION_HEAD_UUID;
			}
		}

		[ColumnName("ATTENDANT_C_NAME",false,typeof(string))]
		public string ATTENDANT_C_NAME
		{
			set
			{
				_ATTENDANT_C_NAME=value;
			}
			get
			{
				return _ATTENDANT_C_NAME;
			}
		}

		[ColumnName("ATTENDANT_E_NAME",false,typeof(string))]
		public string ATTENDANT_E_NAME
		{
			set
			{
				_ATTENDANT_E_NAME=value;
			}
			get
			{
				return _ATTENDANT_E_NAME;
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

		[ColumnName("IS_ATTENDANT_ACTIVE",false,typeof(string))]
		public string IS_ATTENDANT_ACTIVE
		{
			set
			{
				_IS_ATTENDANT_ACTIVE=value;
			}
			get
			{
				return _IS_ATTENDANT_ACTIVE;
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

		[ColumnName("GROUP_HEAD_UUID",false,typeof(string))]
		public string GROUP_HEAD_UUID
		{
			set
			{
				_GROUP_HEAD_UUID=value;
			}
			get
			{
				return _GROUP_HEAD_UUID;
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
		public GroupAttendantV_Record Clone(){
			try{
				return this.Clone<GroupAttendantV_Record>(this);
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		public GroupAttendantV gotoTable(){
			try{
				var dbc = LK.Config.DataBase.Factory.getInfo();
				GroupAttendantV ret = new GroupAttendantV(dbc,this);
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
		public List<GroupHead_Record> Link_GroupHead_By_Uuid()
		{
			try{
				List<GroupHead_Record> ret= new List<GroupHead_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				GroupHead ___table = new GroupHead(dbc);
				ret=(List<GroupHead_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.UUID,this.GROUP_HEAD_UUID))
					.FetchAll<GroupHead_Record>() ; 
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
		public List<GroupHead_Record> Link_GroupHead_By_Uuid(OrderLimit limit)
		{
			try{
				List<GroupHead_Record> ret= new List<GroupHead_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				GroupHead ___table = new GroupHead(dbc);
				ret=(List<GroupHead_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.UUID,this.GROUP_HEAD_UUID))
					.Order(limit)
					.Limit(limit)
					.FetchAll<GroupHead_Record>() ; 
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
		public GroupHead LinkFill_GroupHead_By_Uuid()
		{
			try{
				var data = Link_GroupHead_By_Uuid();
				GroupHead ret=new GroupHead(data);
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
		public GroupHead LinkFill_GroupHead_By_Uuid(OrderLimit limit)
		{
			try{
				var data = Link_GroupHead_By_Uuid(limit);
				GroupHead ret=new GroupHead(data);
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
