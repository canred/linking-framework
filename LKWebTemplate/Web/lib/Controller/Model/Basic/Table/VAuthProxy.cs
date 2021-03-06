using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;  
using LK.Attribute;  
using LK.DB;  
using LK.Config.DataBase;  
using LK.DB.SQLCreater;  
using LKWebTemplate.Model.Basic.Table.Record  ;  
namespace LKWebTemplate.Model.Basic.Table
{
	[LkDataBase("BASIC")]
	[TableView("V_AUTH_PROXY", false)]
	public partial class VAuthProxy : TableBase{
	/*固定物件*/
	//LK.DB.SQLCreater.ASQLCreater sqlCreater = null;
	/*固定物件但名稱需更新*/
	private VAuthProxy_Record _currentRecord = null;
	private IList<VAuthProxy_Record> _All_Record = new List<VAuthProxy_Record>();
		/*建構子*/
		public VAuthProxy(){}
		public VAuthProxy(IDataBaseConfigInfo dbc,string db): base(dbc,db){}
		public VAuthProxy(IDataBaseConfigInfo dbc): base(dbc){}
		public VAuthProxy(IDataBaseConfigInfo dbc,VAuthProxy_Record currenData){
			this.setDataBaseConfigInfo(dbc);
			this._currentRecord = currenData;
		}
		public VAuthProxy(IList<VAuthProxy_Record> currenData){
			this._All_Record = currenData;
		}
		/*欄位資訊 Start*/
		public string IS_USER_DEFAULT_PAGE {
			[ColumnName("IS_USER_DEFAULT_PAGE",false,typeof(string))]
			get{return "IS_USER_DEFAULT_PAGE" ; }}
		public string UUID {
			[ColumnName("UUID",false,typeof(string))]
			get{return "UUID" ; }}
		public string IS_ACTIVE {
			[ColumnName("IS_ACTIVE",false,typeof(string))]
			get{return "IS_ACTIVE" ; }}
		public string CREATE_DATE {
			[ColumnName("CREATE_DATE",false,typeof(DateTime?))]
			get{return "CREATE_DATE" ; }}
		public string CREATE_USER {
			[ColumnName("CREATE_USER",false,typeof(string))]
			get{return "CREATE_USER" ; }}
		public string UPDATE_DATE {
			[ColumnName("UPDATE_DATE",false,typeof(DateTime?))]
			get{return "UPDATE_DATE" ; }}
		public string UPDATE_USER {
			[ColumnName("UPDATE_USER",false,typeof(string))]
			get{return "UPDATE_USER" ; }}
		public string NAME_ZH_TW {
			[ColumnName("NAME_ZH_TW",false,typeof(string))]
			get{return "NAME_ZH_TW" ; }}
		public string NAME_JPN {
			[ColumnName("NAME_JPN",false,typeof(string))]
			get{return "NAME_JPN" ; }}
		public string NAME_ZH_CN {
			[ColumnName("NAME_ZH_CN",false,typeof(string))]
			get{return "NAME_ZH_CN" ; }}
		public string NAME_EN_US {
			[ColumnName("NAME_EN_US",false,typeof(string))]
			get{return "NAME_EN_US" ; }}
		public string ID {
			[ColumnName("ID",false,typeof(string))]
			get{return "ID" ; }}
		public string APPMENU_UUID {
			[ColumnName("APPMENU_UUID",false,typeof(string))]
			get{return "APPMENU_UUID" ; }}
		public string HASCHILD {
			[ColumnName("HASCHILD",false,typeof(string))]
			get{return "HASCHILD" ; }}
		public string APPLICATION_HEAD_UUID {
			[ColumnName("APPLICATION_HEAD_UUID",false,typeof(string))]
			get{return "APPLICATION_HEAD_UUID" ; }}
		public string ORD {
			[ColumnName("ORD",false,typeof(decimal?))]
			get{return "ORD" ; }}
		public string PARAMETER_CLASS {
			[ColumnName("PARAMETER_CLASS",false,typeof(string))]
			get{return "PARAMETER_CLASS" ; }}
		public string IMAGE {
			[ColumnName("IMAGE",false,typeof(string))]
			get{return "IMAGE" ; }}
		public string SITEMAP_UUID {
			[ColumnName("SITEMAP_UUID",false,typeof(string))]
			get{return "SITEMAP_UUID" ; }}
		public string ACTION_MODE {
			[ColumnName("ACTION_MODE",false,typeof(string))]
			get{return "ACTION_MODE" ; }}
		public string IS_DEFAULT_PAGE {
			[ColumnName("IS_DEFAULT_PAGE",false,typeof(string))]
			get{return "IS_DEFAULT_PAGE" ; }}
		public string IS_ADMIN {
			[ColumnName("IS_ADMIN",false,typeof(string))]
			get{return "IS_ADMIN" ; }}
		public string ATTENDANT_UUID {
			[ColumnName("ATTENDANT_UUID",false,typeof(string))]
			get{return "ATTENDANT_UUID" ; }}
		public string APPLICATION_NAME {
			[ColumnName("APPLICATION_NAME",false,typeof(string))]
			get{return "APPLICATION_NAME" ; }}
		public string URL {
			[ColumnName("URL",false,typeof(string))]
			get{return "URL" ; }}
		public string FUNC_PARAMETER_CLASS {
			[ColumnName("FUNC_PARAMETER_CLASS",false,typeof(string))]
			get{return "FUNC_PARAMETER_CLASS" ; }}
		public string P_MODE {
			[ColumnName("P_MODE",false,typeof(string))]
			get{return "P_MODE" ; }}
		public string PROXY_UUID {
			[ColumnName("PROXY_UUID",false,typeof(string))]
			get{return "PROXY_UUID" ; }}
		public string PROXY_ACTION {
			[ColumnName("PROXY_ACTION",false,typeof(string))]
			get{return "PROXY_ACTION" ; }}
		public string PROXY_METHOD {
			[ColumnName("PROXY_METHOD",false,typeof(string))]
			get{return "PROXY_METHOD" ; }}
		public string PROXY_DESCRIPTION {
			[ColumnName("PROXY_DESCRIPTION",false,typeof(string))]
			get{return "PROXY_DESCRIPTION" ; }}
		public string PROXY_TYPE {
			[ColumnName("PROXY_TYPE",false,typeof(string))]
			get{return "PROXY_TYPE" ; }}
		public string NEED_REDIRECT {
			[ColumnName("NEED_REDIRECT",false,typeof(string))]
			get{return "NEED_REDIRECT" ; }}
		public string REDIRECT_PROXY_ACTION {
			[ColumnName("REDIRECT_PROXY_ACTION",false,typeof(string))]
			get{return "REDIRECT_PROXY_ACTION" ; }}
		public string REDIRECT_PROXY_METHOD {
			[ColumnName("REDIRECT_PROXY_METHOD",false,typeof(string))]
			get{return "REDIRECT_PROXY_METHOD" ; }}
		public string REDIRECT_SRC {
			[ColumnName("REDIRECT_SRC",false,typeof(string))]
			get{return "REDIRECT_SRC" ; }}
		public string APPMENU_PROXY_UUID {
			[ColumnName("APPMENU_PROXY_UUID",false,typeof(string))]
			get{return "APPMENU_PROXY_UUID" ; }}
		/*欄位資訊 End*/
		/*固定的方法，但名稱需變更 Start*/
		public VAuthProxy_Record CurrentRecord(){
			try{
				if (_currentRecord == null){
					if (this._All_Record.Count > 0){
						_currentRecord = this._All_Record.First();
					}
				}
				return _currentRecord;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		public VAuthProxy_Record CreateNew(){
			try{
				VAuthProxy_Record newData = new VAuthProxy_Record();
				return newData;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		public IList<VAuthProxy_Record> AllRecord(){
			try{
				return _All_Record;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		public void RemoveAllRecord(){
			try{
				_All_Record = new List<VAuthProxy_Record>();
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*固定的方法，但名稱需變更 End*/
		/*有關PK的方法*/
		/*依照資料表與資料表的關係，產生出來的方法*/
		public List<ApplicationHead_Record> Link_ApplicationHead_By_Uuid()
		{
			try{
				List<ApplicationHead_Record> ret= new List<ApplicationHead_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				ApplicationHead ___table = new ApplicationHead(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.UUID,item.APPLICATION_HEAD_UUID).R().Or()  ; 
 				}
				condition.CheckSQL();
				ret=(List<ApplicationHead_Record>)
						___table.Where(condition)
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
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.UUID,item.PROXY_UUID).R().Or()  ; 
 				}
				condition.CheckSQL();
				ret=(List<Proxy_Record>)
						___table.Where(condition)
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
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.UUID,item.ATTENDANT_UUID).R().Or()  ; 
 				}
				condition.CheckSQL();
				ret=(List<Attendant_Record>)
						___table.Where(condition)
						.FetchAll<Attendant_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180340*/
		public List<ApplicationHead_Record> Link_ApplicationHead_By_Uuid(OrderLimit limit)
		{
			try{
				List<ApplicationHead_Record> ret= new List<ApplicationHead_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				ApplicationHead ___table = new ApplicationHead(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.UUID,item.APPLICATION_HEAD_UUID).R().Or()  ; 
 				}
				condition.CheckSQL();
				ret=(List<ApplicationHead_Record>)
						___table.Where(condition)
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
		/*201303180340*/
		public List<Proxy_Record> Link_Proxy_By_Uuid(OrderLimit limit)
		{
			try{
				List<Proxy_Record> ret= new List<Proxy_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Proxy ___table = new Proxy(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.UUID,item.PROXY_UUID).R().Or()  ; 
 				}
				condition.CheckSQL();
				ret=(List<Proxy_Record>)
						___table.Where(condition)
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
		/*201303180340*/
		public List<Attendant_Record> Link_Attendant_By_Uuid(OrderLimit limit)
		{
			try{
				List<Attendant_Record> ret= new List<Attendant_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Attendant ___table = new Attendant(dbc);
				SQLCondition condition = new SQLCondition(___table) ;
				foreach(var item in AllRecord()){
						condition
						.L().Equal(___table.UUID,item.ATTENDANT_UUID).R().Or()  ; 
 				}
				condition.CheckSQL();
				ret=(List<Attendant_Record>)
						___table.Where(condition)
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
		/*201303180336*/
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
		/*201303180336*/
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
		/*201303180336*/
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
		/*201303180337*/
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
		/*201303180337*/
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
		/*201303180337*/
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
