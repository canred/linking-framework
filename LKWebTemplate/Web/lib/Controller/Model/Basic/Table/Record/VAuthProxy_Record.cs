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
	[TableView("V_AUTH_PROXY", false)]
	[LkDataBase("BASIC")]
	[Serializable]
	public class VAuthProxy_Record : RecordBase{
		public VAuthProxy_Record(){}
		/*欄位資訊 Start*/
		string _IS_USER_DEFAULT_PAGE=null;
		string _UUID=null;
		string _IS_ACTIVE=null;
		DateTime? _CREATE_DATE=null;
		string _CREATE_USER=null;
		DateTime? _UPDATE_DATE=null;
		string _UPDATE_USER=null;
		string _NAME_ZH_TW=null;
		string _NAME_JPN=null;
		string _NAME_ZH_CN=null;
		string _NAME_EN_US=null;
		string _ID=null;
		string _APPMENU_UUID=null;
		string _HASCHILD=null;
		string _APPLICATION_HEAD_UUID=null;
		decimal? _ORD=null;
		string _PARAMETER_CLASS=null;
		string _IMAGE=null;
		string _SITEMAP_UUID=null;
		string _ACTION_MODE=null;
		string _IS_DEFAULT_PAGE=null;
		string _IS_ADMIN=null;
		string _ATTENDANT_UUID=null;
		string _APPLICATION_NAME=null;
		string _URL=null;
		string _FUNC_PARAMETER_CLASS=null;
		string _P_MODE=null;
		string _PROXY_UUID=null;
		string _PROXY_ACTION=null;
		string _PROXY_METHOD=null;
		string _PROXY_DESCRIPTION=null;
		string _PROXY_TYPE=null;
		string _NEED_REDIRECT=null;
		string _REDIRECT_PROXY_ACTION=null;
		string _REDIRECT_PROXY_METHOD=null;
		string _REDIRECT_SRC=null;
		string _APPMENU_PROXY_UUID=null;
		/*欄位資訊 End*/

		[ColumnName("IS_USER_DEFAULT_PAGE",false,typeof(string))]
		public string IS_USER_DEFAULT_PAGE
		{
			set
			{
				_IS_USER_DEFAULT_PAGE=value;
			}
			get
			{
				return _IS_USER_DEFAULT_PAGE;
			}
		}

		[ColumnName("UUID",false,typeof(string))]
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

		[ColumnName("NAME_JPN",false,typeof(string))]
		public string NAME_JPN
		{
			set
			{
				_NAME_JPN=value;
			}
			get
			{
				return _NAME_JPN;
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

		[ColumnName("APPMENU_UUID",false,typeof(string))]
		public string APPMENU_UUID
		{
			set
			{
				_APPMENU_UUID=value;
			}
			get
			{
				return _APPMENU_UUID;
			}
		}

		[ColumnName("HASCHILD",false,typeof(string))]
		public string HASCHILD
		{
			set
			{
				_HASCHILD=value;
			}
			get
			{
				return _HASCHILD;
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

		[ColumnName("ORD",false,typeof(decimal?))]
		public decimal? ORD
		{
			set
			{
				_ORD=value;
			}
			get
			{
				return _ORD;
			}
		}

		[ColumnName("PARAMETER_CLASS",false,typeof(string))]
		public string PARAMETER_CLASS
		{
			set
			{
				_PARAMETER_CLASS=value;
			}
			get
			{
				return _PARAMETER_CLASS;
			}
		}

		[ColumnName("IMAGE",false,typeof(string))]
		public string IMAGE
		{
			set
			{
				_IMAGE=value;
			}
			get
			{
				return _IMAGE;
			}
		}

		[ColumnName("SITEMAP_UUID",false,typeof(string))]
		public string SITEMAP_UUID
		{
			set
			{
				_SITEMAP_UUID=value;
			}
			get
			{
				return _SITEMAP_UUID;
			}
		}

		[ColumnName("ACTION_MODE",false,typeof(string))]
		public string ACTION_MODE
		{
			set
			{
				_ACTION_MODE=value;
			}
			get
			{
				return _ACTION_MODE;
			}
		}

		[ColumnName("IS_DEFAULT_PAGE",false,typeof(string))]
		public string IS_DEFAULT_PAGE
		{
			set
			{
				_IS_DEFAULT_PAGE=value;
			}
			get
			{
				return _IS_DEFAULT_PAGE;
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

		[ColumnName("URL",false,typeof(string))]
		public string URL
		{
			set
			{
				_URL=value;
			}
			get
			{
				return _URL;
			}
		}

		[ColumnName("FUNC_PARAMETER_CLASS",false,typeof(string))]
		public string FUNC_PARAMETER_CLASS
		{
			set
			{
				_FUNC_PARAMETER_CLASS=value;
			}
			get
			{
				return _FUNC_PARAMETER_CLASS;
			}
		}

		[ColumnName("P_MODE",false,typeof(string))]
		public string P_MODE
		{
			set
			{
				_P_MODE=value;
			}
			get
			{
				return _P_MODE;
			}
		}

		[ColumnName("PROXY_UUID",false,typeof(string))]
		public string PROXY_UUID
		{
			set
			{
				_PROXY_UUID=value;
			}
			get
			{
				return _PROXY_UUID;
			}
		}

		[ColumnName("PROXY_ACTION",false,typeof(string))]
		public string PROXY_ACTION
		{
			set
			{
				_PROXY_ACTION=value;
			}
			get
			{
				return _PROXY_ACTION;
			}
		}

		[ColumnName("PROXY_METHOD",false,typeof(string))]
		public string PROXY_METHOD
		{
			set
			{
				_PROXY_METHOD=value;
			}
			get
			{
				return _PROXY_METHOD;
			}
		}

		[ColumnName("PROXY_DESCRIPTION",false,typeof(string))]
		public string PROXY_DESCRIPTION
		{
			set
			{
				_PROXY_DESCRIPTION=value;
			}
			get
			{
				return _PROXY_DESCRIPTION;
			}
		}

		[ColumnName("PROXY_TYPE",false,typeof(string))]
		public string PROXY_TYPE
		{
			set
			{
				_PROXY_TYPE=value;
			}
			get
			{
				return _PROXY_TYPE;
			}
		}

		[ColumnName("NEED_REDIRECT",false,typeof(string))]
		public string NEED_REDIRECT
		{
			set
			{
				_NEED_REDIRECT=value;
			}
			get
			{
				return _NEED_REDIRECT;
			}
		}

		[ColumnName("REDIRECT_PROXY_ACTION",false,typeof(string))]
		public string REDIRECT_PROXY_ACTION
		{
			set
			{
				_REDIRECT_PROXY_ACTION=value;
			}
			get
			{
				return _REDIRECT_PROXY_ACTION;
			}
		}

		[ColumnName("REDIRECT_PROXY_METHOD",false,typeof(string))]
		public string REDIRECT_PROXY_METHOD
		{
			set
			{
				_REDIRECT_PROXY_METHOD=value;
			}
			get
			{
				return _REDIRECT_PROXY_METHOD;
			}
		}

		[ColumnName("REDIRECT_SRC",false,typeof(string))]
		public string REDIRECT_SRC
		{
			set
			{
				_REDIRECT_SRC=value;
			}
			get
			{
				return _REDIRECT_SRC;
			}
		}

		[ColumnName("APPMENU_PROXY_UUID",false,typeof(string))]
		public string APPMENU_PROXY_UUID
		{
			set
			{
				_APPMENU_PROXY_UUID=value;
			}
			get
			{
				return _APPMENU_PROXY_UUID;
			}
		}
		public VAuthProxy_Record Clone(){
			try{
				return this.Clone<VAuthProxy_Record>(this);
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		public VAuthProxy gotoTable(){
			try{
				var dbc = LK.Config.DataBase.Factory.getInfo();
				VAuthProxy ret = new VAuthProxy(dbc,this);
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
		public List<Proxy_Record> Link_Proxy_By_Uuid()
		{
			try{
				List<Proxy_Record> ret= new List<Proxy_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Proxy ___table = new Proxy(dbc);
				ret=(List<Proxy_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.UUID,this.PROXY_UUID))
					.FetchAll<Proxy_Record>() ; 
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
		public List<Proxy_Record> Link_Proxy_By_Uuid(OrderLimit limit)
		{
			try{
				List<Proxy_Record> ret= new List<Proxy_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Proxy ___table = new Proxy(dbc);
				ret=(List<Proxy_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.UUID,this.PROXY_UUID))
					.Order(limit)
					.Limit(limit)
					.FetchAll<Proxy_Record>() ; 
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
		public Proxy LinkFill_Proxy_By_Uuid()
		{
			try{
				var data = Link_Proxy_By_Uuid();
				Proxy ret=new Proxy(data);
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
		public Proxy LinkFill_Proxy_By_Uuid(OrderLimit limit)
		{
			try{
				var data = Link_Proxy_By_Uuid(limit);
				Proxy ret=new Proxy(data);
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
