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
	[TableView("GROUP_ATTENDANT", true)]
	[LkDataBase("BASIC")]
	[Serializable]
	public class GroupAttendant_Record : RecordBase{
		public GroupAttendant_Record(){}
		/*欄位資訊 Start*/
		string _UUID=null;
		string _IS_ACTIVE=null;
		DateTime? _CREATE_DATE=null;
		string _CREATE_USER=null;
		DateTime? _UPDATE_DATE=null;
		string _UPDATE_USER=null;
		string _GROUP_HEAD_UUID=null;
		string _ATTENDANT_UUID=null;
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
		public GroupAttendant_Record Clone(){
			try{
				return this.Clone<GroupAttendant_Record>(this);
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		public GroupAttendant gotoTable(){
			try{
				var dbc = LK.Config.DataBase.Factory.getInfo();
				GroupAttendant ret = new GroupAttendant(dbc,this);
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
