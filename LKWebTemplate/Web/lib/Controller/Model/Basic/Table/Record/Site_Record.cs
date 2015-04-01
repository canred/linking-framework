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
	[TableView("SITE", true)]
	[LkDataBase("BASIC")]
	[Serializable]
	public class Site_Record : RecordBase{
		public Site_Record(){}
		/*欄位資訊 Start*/
		string _UUID=null;
		DateTime? _CREATE_DATE=null;
		DateTime? _UPDATE_DATE=null;
		string _IS_ACTIVE=null;
		string _COMPANY_UUID=null;
		string _ID=null;
		string _C_NAME=null;
		string _E_NAME=null;
		string _PARENT_SITE_UUID=null;
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

		[ColumnName("PARENT_SITE_UUID",false,typeof(string))]
		public string PARENT_SITE_UUID
		{
			set
			{
				_PARENT_SITE_UUID=value;
			}
			get
			{
				return _PARENT_SITE_UUID;
			}
		}
		public Site_Record Clone(){
			try{
				return this.Clone<Site_Record>(this);
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		public Site gotoTable(){
			try{
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Site ret = new Site(dbc,this);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180347*/
		public List<Site_Record> Link_Site_By_ParentSiteUuid()
		{
			try{
				List<Site_Record> ret= new List<Site_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Site ___table = new Site(dbc);
				ret=(List<Site_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.PARENT_SITE_UUID,this.UUID))
					.FetchAll<Site_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180347*/
		public List<Attendant_Record> Link_Attendant_By_SiteUuid()
		{
			try{
				List<Attendant_Record> ret= new List<Attendant_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Attendant ___table = new Attendant(dbc);
				ret=(List<Attendant_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.SITE_UUID,this.UUID))
					.FetchAll<Attendant_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180348*/
		public List<Site_Record> Link_Site_By_ParentSiteUuid(OrderLimit limit)
		{
			try{
				List<Site_Record> ret= new List<Site_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Site ___table = new Site(dbc);
				ret=(List<Site_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.PARENT_SITE_UUID,this.UUID))
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
		public List<Attendant_Record> Link_Attendant_By_SiteUuid(OrderLimit limit)
		{
			try{
				List<Attendant_Record> ret= new List<Attendant_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Attendant ___table = new Attendant(dbc);
				ret=(List<Attendant_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.SITE_UUID,this.UUID))
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
		public List<Site_Record> Link_Site_By_Uuid()
		{
			try{
				List<Site_Record> ret= new List<Site_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Site ___table = new Site(dbc);
				ret=(List<Site_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.UUID,this.PARENT_SITE_UUID))
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
		public List<Site_Record> Link_Site_By_Uuid(OrderLimit limit)
		{
			try{
				List<Site_Record> ret= new List<Site_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Site ___table = new Site(dbc);
				ret=(List<Site_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.UUID,this.PARENT_SITE_UUID))
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
		public Site LinkFill_Site_By_ParentSiteUuid()
		{
			try{
				var data = Link_Site_By_ParentSiteUuid();
				Site ret=new Site(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180357*/
		public Attendant LinkFill_Attendant_By_SiteUuid()
		{
			try{
				var data = Link_Attendant_By_SiteUuid();
				Attendant ret=new Attendant(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180358*/
		public Site LinkFill_Site_By_ParentSiteUuid(OrderLimit limit)
		{
			try{
				var data = Link_Site_By_ParentSiteUuid(limit);
				Site ret=new Site(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180358*/
		public Attendant LinkFill_Attendant_By_SiteUuid(OrderLimit limit)
		{
			try{
				var data = Link_Attendant_By_SiteUuid(limit);
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
