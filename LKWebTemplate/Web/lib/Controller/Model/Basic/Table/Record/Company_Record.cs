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
	[TableView("COMPANY", true)]
	[LkDataBase("BASIC")]
	[Serializable]
	public class Company_Record : RecordBase{
		public Company_Record(){}
		/*欄位資訊 Start*/
		string _UUID=null;
		DateTime? _CREATE_DATE=null;
		DateTime? _UPDATE_DATE=null;
		string _IS_ACTIVE=null;
		string _CLASS=null;
		string _ID=null;
		string _C_NAME=null;
		string _E_NAME=null;
		string _VOUCHER_POINT_UUID=null;
		decimal? _WEEK_SHIFT=null;
		string _OU_SYNC_TYPE=null;
		string _NAME_ZH_CN=null;
		string _CONCURRENT_USER=null;
		DateTime? _EXPIRED_DATE=null;
		string _SALES_ATTENDANT_UUID=null;
		string _IS_SYNC_AD_USER=null;
		string _AD_LDAP=null;
		string _AD_LDAP_USER=null;
		string _AD_LDAP_USER_PASSWORD=null;
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

		[ColumnName("CLASS",false,typeof(string))]
		public string CLASS
		{
			set
			{
				_CLASS=value;
			}
			get
			{
				return _CLASS;
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

		[ColumnName("VOUCHER_POINT_UUID",false,typeof(string))]
		public string VOUCHER_POINT_UUID
		{
			set
			{
				_VOUCHER_POINT_UUID=value;
			}
			get
			{
				return _VOUCHER_POINT_UUID;
			}
		}

		[ColumnName("WEEK_SHIFT",false,typeof(decimal?))]
		public decimal? WEEK_SHIFT
		{
			set
			{
				_WEEK_SHIFT=value;
			}
			get
			{
				return _WEEK_SHIFT;
			}
		}

		[ColumnName("OU_SYNC_TYPE",false,typeof(string))]
		public string OU_SYNC_TYPE
		{
			set
			{
				_OU_SYNC_TYPE=value;
			}
			get
			{
				return _OU_SYNC_TYPE;
			}
		}

		[ColumnName("NAME_ZH_CN",false,typeof(string))]
		public string NAME_ZH_CN
		{
			set
			{
				_NAME_ZH_CN=value;
			}
			get
			{
				return _NAME_ZH_CN;
			}
		}

		[ColumnName("CONCURRENT_USER",false,typeof(string))]
		public string CONCURRENT_USER
		{
			set
			{
				_CONCURRENT_USER=value;
			}
			get
			{
				return _CONCURRENT_USER;
			}
		}

		[ColumnName("EXPIRED_DATE",false,typeof(DateTime?))]
		public DateTime? EXPIRED_DATE
		{
			set
			{
				_EXPIRED_DATE=value;
			}
			get
			{
				return _EXPIRED_DATE;
			}
		}

		[ColumnName("SALES_ATTENDANT_UUID",false,typeof(string))]
		public string SALES_ATTENDANT_UUID
		{
			set
			{
				_SALES_ATTENDANT_UUID=value;
			}
			get
			{
				return _SALES_ATTENDANT_UUID;
			}
		}

		[ColumnName("IS_SYNC_AD_USER",false,typeof(string))]
		public string IS_SYNC_AD_USER
		{
			set
			{
				_IS_SYNC_AD_USER=value;
			}
			get
			{
				return _IS_SYNC_AD_USER;
			}
		}

		[ColumnName("AD_LDAP",false,typeof(string))]
		public string AD_LDAP
		{
			set
			{
				_AD_LDAP=value;
			}
			get
			{
				return _AD_LDAP;
			}
		}

		[ColumnName("AD_LDAP_USER",false,typeof(string))]
		public string AD_LDAP_USER
		{
			set
			{
				_AD_LDAP_USER=value;
			}
			get
			{
				return _AD_LDAP_USER;
			}
		}

		[ColumnName("AD_LDAP_USER_PASSWORD",false,typeof(string))]
		public string AD_LDAP_USER_PASSWORD
		{
			set
			{
				_AD_LDAP_USER_PASSWORD=value;
			}
			get
			{
				return _AD_LDAP_USER_PASSWORD;
			}
		}
		public Company_Record Clone(){
			try{
				return this.Clone<Company_Record>(this);
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		public Company gotoTable(){
			try{
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Company ret = new Company(dbc,this);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180347*/
		public List<Attendant_Record> Link_Attendant_By_CompanyUuid()
		{
			try{
				List<Attendant_Record> ret= new List<Attendant_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Attendant ___table = new Attendant(dbc);
				ret=(List<Attendant_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.COMPANY_UUID,this.UUID))
					.FetchAll<Attendant_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180347*/
		public List<Department_Record> Link_Department_By_CompanyUuid()
		{
			try{
				List<Department_Record> ret= new List<Department_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Department ___table = new Department(dbc);
				ret=(List<Department_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.COMPANY_UUID,this.UUID))
					.FetchAll<Department_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180347*/
		public List<GroupHead_Record> Link_GroupHead_By_CompanyUuid()
		{
			try{
				List<GroupHead_Record> ret= new List<GroupHead_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				GroupHead ___table = new GroupHead(dbc);
				ret=(List<GroupHead_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.COMPANY_UUID,this.UUID))
					.FetchAll<GroupHead_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180347*/
		public List<Site_Record> Link_Site_By_CompanyUuid()
		{
			try{
				List<Site_Record> ret= new List<Site_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Site ___table = new Site(dbc);
				ret=(List<Site_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.COMPANY_UUID,this.UUID))
					.FetchAll<Site_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180347*/
		public List<GroupAttendantV_Record> Link_GroupAttendantV_By_CompanyUuid()
		{
			try{
				List<GroupAttendantV_Record> ret= new List<GroupAttendantV_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				GroupAttendantV ___table = new GroupAttendantV(dbc);
				ret=(List<GroupAttendantV_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.COMPANY_UUID,this.UUID))
					.FetchAll<GroupAttendantV_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180347*/
		public List<GroupHeadV_Record> Link_GroupHeadV_By_CompanyUuid()
		{
			try{
				List<GroupHeadV_Record> ret= new List<GroupHeadV_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				GroupHeadV ___table = new GroupHeadV(dbc);
				ret=(List<GroupHeadV_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.COMPANY_UUID,this.UUID))
					.FetchAll<GroupHeadV_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180348*/
		public List<Attendant_Record> Link_Attendant_By_CompanyUuid(OrderLimit limit)
		{
			try{
				List<Attendant_Record> ret= new List<Attendant_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Attendant ___table = new Attendant(dbc);
				ret=(List<Attendant_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.COMPANY_UUID,this.UUID))
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
		public List<Department_Record> Link_Department_By_CompanyUuid(OrderLimit limit)
		{
			try{
				List<Department_Record> ret= new List<Department_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Department ___table = new Department(dbc);
				ret=(List<Department_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.COMPANY_UUID,this.UUID))
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
		public List<GroupHead_Record> Link_GroupHead_By_CompanyUuid(OrderLimit limit)
		{
			try{
				List<GroupHead_Record> ret= new List<GroupHead_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				GroupHead ___table = new GroupHead(dbc);
				ret=(List<GroupHead_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.COMPANY_UUID,this.UUID))
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
		/*201303180348*/
		public List<Site_Record> Link_Site_By_CompanyUuid(OrderLimit limit)
		{
			try{
				List<Site_Record> ret= new List<Site_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Site ___table = new Site(dbc);
				ret=(List<Site_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.COMPANY_UUID,this.UUID))
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
		/*201303180348*/
		public List<GroupAttendantV_Record> Link_GroupAttendantV_By_CompanyUuid(OrderLimit limit)
		{
			try{
				List<GroupAttendantV_Record> ret= new List<GroupAttendantV_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				GroupAttendantV ___table = new GroupAttendantV(dbc);
				ret=(List<GroupAttendantV_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.COMPANY_UUID,this.UUID))
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
		public List<GroupHeadV_Record> Link_GroupHeadV_By_CompanyUuid(OrderLimit limit)
		{
			try{
				List<GroupHeadV_Record> ret= new List<GroupHeadV_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				GroupHeadV ___table = new GroupHeadV(dbc);
				ret=(List<GroupHeadV_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.COMPANY_UUID,this.UUID))
					.Order(limit)
					.Limit(limit)
					.FetchAll<GroupHeadV_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180357*/
		public Attendant LinkFill_Attendant_By_CompanyUuid()
		{
			try{
				var data = Link_Attendant_By_CompanyUuid();
				Attendant ret=new Attendant(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180357*/
		public Department LinkFill_Department_By_CompanyUuid()
		{
			try{
				var data = Link_Department_By_CompanyUuid();
				Department ret=new Department(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180357*/
		public GroupHead LinkFill_GroupHead_By_CompanyUuid()
		{
			try{
				var data = Link_GroupHead_By_CompanyUuid();
				GroupHead ret=new GroupHead(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180357*/
		public Site LinkFill_Site_By_CompanyUuid()
		{
			try{
				var data = Link_Site_By_CompanyUuid();
				Site ret=new Site(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180357*/
		public GroupAttendantV LinkFill_GroupAttendantV_By_CompanyUuid()
		{
			try{
				var data = Link_GroupAttendantV_By_CompanyUuid();
				GroupAttendantV ret=new GroupAttendantV(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180357*/
		public GroupHeadV LinkFill_GroupHeadV_By_CompanyUuid()
		{
			try{
				var data = Link_GroupHeadV_By_CompanyUuid();
				GroupHeadV ret=new GroupHeadV(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180358*/
		public Attendant LinkFill_Attendant_By_CompanyUuid(OrderLimit limit)
		{
			try{
				var data = Link_Attendant_By_CompanyUuid(limit);
				Attendant ret=new Attendant(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180358*/
		public Department LinkFill_Department_By_CompanyUuid(OrderLimit limit)
		{
			try{
				var data = Link_Department_By_CompanyUuid(limit);
				Department ret=new Department(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180358*/
		public GroupHead LinkFill_GroupHead_By_CompanyUuid(OrderLimit limit)
		{
			try{
				var data = Link_GroupHead_By_CompanyUuid(limit);
				GroupHead ret=new GroupHead(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180358*/
		public Site LinkFill_Site_By_CompanyUuid(OrderLimit limit)
		{
			try{
				var data = Link_Site_By_CompanyUuid(limit);
				Site ret=new Site(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180358*/
		public GroupAttendantV LinkFill_GroupAttendantV_By_CompanyUuid(OrderLimit limit)
		{
			try{
				var data = Link_GroupAttendantV_By_CompanyUuid(limit);
				GroupAttendantV ret=new GroupAttendantV(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180358*/
		public GroupHeadV LinkFill_GroupHeadV_By_CompanyUuid(OrderLimit limit)
		{
			try{
				var data = Link_GroupHeadV_By_CompanyUuid(limit);
				GroupHeadV ret=new GroupHeadV(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
	}
}
