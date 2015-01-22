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
	[TableView("GROUP_HEAD", true)]
	[LkDataBase("BASIC")]
	[Serializable]
	public class GroupHead_Record : RecordBase{
		public GroupHead_Record(){}
		/*欄位資訊 Start*/
		string _UUID=null;
		DateTime? _CREATE_DATE=null;
		DateTime? _UPDATE_DATE=null;
		string _IS_ACTIVE=null;
		string _NAME_ZH_TW=null;
		string _NAME_ZH_CN=null;
		string _NAME_EN_US=null;
		string _COMPANY_UUID=null;
		string _ID=null;
		string _CREATE_USER=null;
		string _UPDATE_USER=null;
		string _APPLICATION_HEAD_UUID=null;
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

		[ColumnName("NAME_ZH_TW",false,typeof(string))]
		public string NAME_ZH_TW
		{
			set
			{
				_NAME_ZH_TW=value;
			}
			get
			{
				return _NAME_ZH_TW;
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

		[ColumnName("NAME_EN_US",false,typeof(string))]
		public string NAME_EN_US
		{
			set
			{
				_NAME_EN_US=value;
			}
			get
			{
				return _NAME_EN_US;
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
		public GroupHead_Record Clone(){
			try{
				return this.Clone<GroupHead_Record>(this);
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		public GroupHead gotoTable(){
			try{
				var dbc = LK.Config.DataBase.Factory.getInfo();
				GroupHead ret = new GroupHead(dbc,this);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180347*/
		public List<GroupAppmenu_Record> Link_GroupAppmenu_By_GroupHeadUuid()
		{
			try{
				List<GroupAppmenu_Record> ret= new List<GroupAppmenu_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				GroupAppmenu ___table = new GroupAppmenu(dbc);
				ret=(List<GroupAppmenu_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.GROUP_HEAD_UUID,this.UUID))
					.FetchAll<GroupAppmenu_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180347*/
		public List<GroupAttendant_Record> Link_GroupAttendant_By_GroupHeadUuid()
		{
			try{
				List<GroupAttendant_Record> ret= new List<GroupAttendant_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				GroupAttendant ___table = new GroupAttendant(dbc);
				ret=(List<GroupAttendant_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.GROUP_HEAD_UUID,this.UUID))
					.FetchAll<GroupAttendant_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180347*/
		public List<GroupAttendantV_Record> Link_GroupAttendantV_By_GroupHeadUuid()
		{
			try{
				List<GroupAttendantV_Record> ret= new List<GroupAttendantV_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				GroupAttendantV ___table = new GroupAttendantV(dbc);
				ret=(List<GroupAttendantV_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.GROUP_HEAD_UUID,this.UUID))
					.FetchAll<GroupAttendantV_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180347*/
		public List<GroupAppmenuV_Record> Link_GroupAppmenuV_By_GroupHeadUuid()
		{
			try{
				List<GroupAppmenuV_Record> ret= new List<GroupAppmenuV_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				GroupAppmenuV ___table = new GroupAppmenuV(dbc);
				ret=(List<GroupAppmenuV_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.GROUP_HEAD_UUID,this.UUID))
					.FetchAll<GroupAppmenuV_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180348*/
		public List<GroupAppmenu_Record> Link_GroupAppmenu_By_GroupHeadUuid(OrderLimit limit)
		{
			try{
				List<GroupAppmenu_Record> ret= new List<GroupAppmenu_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				GroupAppmenu ___table = new GroupAppmenu(dbc);
				ret=(List<GroupAppmenu_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.GROUP_HEAD_UUID,this.UUID))
					.Order(limit)
					.Limit(limit)
					.FetchAll<GroupAppmenu_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180348*/
		public List<GroupAttendant_Record> Link_GroupAttendant_By_GroupHeadUuid(OrderLimit limit)
		{
			try{
				List<GroupAttendant_Record> ret= new List<GroupAttendant_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				GroupAttendant ___table = new GroupAttendant(dbc);
				ret=(List<GroupAttendant_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.GROUP_HEAD_UUID,this.UUID))
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
		public List<GroupAttendantV_Record> Link_GroupAttendantV_By_GroupHeadUuid(OrderLimit limit)
		{
			try{
				List<GroupAttendantV_Record> ret= new List<GroupAttendantV_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				GroupAttendantV ___table = new GroupAttendantV(dbc);
				ret=(List<GroupAttendantV_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.GROUP_HEAD_UUID,this.UUID))
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
		public List<GroupAppmenuV_Record> Link_GroupAppmenuV_By_GroupHeadUuid(OrderLimit limit)
		{
			try{
				List<GroupAppmenuV_Record> ret= new List<GroupAppmenuV_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				GroupAppmenuV ___table = new GroupAppmenuV(dbc);
				ret=(List<GroupAppmenuV_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.GROUP_HEAD_UUID,this.UUID))
					.Order(limit)
					.Limit(limit)
					.FetchAll<GroupAppmenuV_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		public List<ApplicationHead_Record> Link_ApplicationHead_By_Uuid()
		{
			try{
				List<ApplicationHead_Record> ret= new List<ApplicationHead_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				ApplicationHead ___table = new ApplicationHead(dbc);
				ret=(List<ApplicationHead_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.UUID,this.APPLICATION_HEAD_UUID))
					.FetchAll<ApplicationHead_Record>() ; 
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
		/*201303180404*/
		public List<ApplicationHead_Record> Link_ApplicationHead_By_Uuid(OrderLimit limit)
		{
			try{
				List<ApplicationHead_Record> ret= new List<ApplicationHead_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				ApplicationHead ___table = new ApplicationHead(dbc);
				ret=(List<ApplicationHead_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.UUID,this.APPLICATION_HEAD_UUID))
					.Order(limit)
					.Limit(limit)
					.FetchAll<ApplicationHead_Record>() ; 
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
		/*201303180357*/
		public GroupAppmenu LinkFill_GroupAppmenu_By_GroupHeadUuid()
		{
			try{
				var data = Link_GroupAppmenu_By_GroupHeadUuid();
				GroupAppmenu ret=new GroupAppmenu(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180357*/
		public GroupAttendant LinkFill_GroupAttendant_By_GroupHeadUuid()
		{
			try{
				var data = Link_GroupAttendant_By_GroupHeadUuid();
				GroupAttendant ret=new GroupAttendant(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180357*/
		public GroupAttendantV LinkFill_GroupAttendantV_By_GroupHeadUuid()
		{
			try{
				var data = Link_GroupAttendantV_By_GroupHeadUuid();
				GroupAttendantV ret=new GroupAttendantV(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180357*/
		public GroupAppmenuV LinkFill_GroupAppmenuV_By_GroupHeadUuid()
		{
			try{
				var data = Link_GroupAppmenuV_By_GroupHeadUuid();
				GroupAppmenuV ret=new GroupAppmenuV(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180358*/
		public GroupAppmenu LinkFill_GroupAppmenu_By_GroupHeadUuid(OrderLimit limit)
		{
			try{
				var data = Link_GroupAppmenu_By_GroupHeadUuid(limit);
				GroupAppmenu ret=new GroupAppmenu(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180358*/
		public GroupAttendant LinkFill_GroupAttendant_By_GroupHeadUuid(OrderLimit limit)
		{
			try{
				var data = Link_GroupAttendant_By_GroupHeadUuid(limit);
				GroupAttendant ret=new GroupAttendant(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180358*/
		public GroupAttendantV LinkFill_GroupAttendantV_By_GroupHeadUuid(OrderLimit limit)
		{
			try{
				var data = Link_GroupAttendantV_By_GroupHeadUuid(limit);
				GroupAttendantV ret=new GroupAttendantV(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180358*/
		public GroupAppmenuV LinkFill_GroupAppmenuV_By_GroupHeadUuid(OrderLimit limit)
		{
			try{
				var data = Link_GroupAppmenuV_By_GroupHeadUuid(limit);
				GroupAppmenuV ret=new GroupAppmenuV(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*2013031800428*/
		public ApplicationHead LinkFill_ApplicationHead_By_Uuid()
		{
			try{
				var data = Link_ApplicationHead_By_Uuid();
				ApplicationHead ret=new ApplicationHead(data);
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
		/*201303180429*/
		public ApplicationHead LinkFill_ApplicationHead_By_Uuid(OrderLimit limit)
		{
			try{
				var data = Link_ApplicationHead_By_Uuid(limit);
				ApplicationHead ret=new ApplicationHead(data);
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
	}
}
