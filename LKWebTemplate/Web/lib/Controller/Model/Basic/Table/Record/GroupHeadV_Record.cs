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
	[TableView("GROUP_HEAD_V", false)]
	[LkDataBase("BASIC")]
	[Serializable]
	public class GroupHeadV_Record : RecordBase{
		public GroupHeadV_Record(){}
		/*欄位資訊 Start*/
		string _UUID=null;
		DateTime? _CREATE_DATE=null;
		DateTime? _UPDATE_DATE=null;
		string _IS_GROUP_ACTIVE=null;
		string _NAME_ZH_TW=null;
		string _NAME_ZH_CN=null;
		string _NAME_EN_US=null;
		string _COMPANY_UUID=null;
		string _ID=null;
		string _CREATE_USER=null;
		string _UPDATE_USER=null;
		string _APPLICATION_HEAD_UUID=null;
		string _APPLICATION_ID=null;
		string _APPLICATION_NAME=null;
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

		[ColumnName("APPLICATION_ID",false,typeof(string))]
		public string APPLICATION_ID
		{
			set
			{
				_APPLICATION_ID=value;
			}
			get
			{
				return _APPLICATION_ID;
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
		public GroupHeadV_Record Clone(){
			try{
				return this.Clone<GroupHeadV_Record>(this);
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		public GroupHeadV gotoTable(){
			try{
				var dbc = LK.Config.DataBase.Factory.getInfo();
				GroupHeadV ret = new GroupHeadV(dbc,this);
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
