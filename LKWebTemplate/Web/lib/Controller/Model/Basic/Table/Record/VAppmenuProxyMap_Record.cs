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
	[TableView("V_APPMENU_PROXY_MAP", false)]
	[LkDataBase("BASIC")]
	[Serializable]
	public class VAppmenuProxyMap_Record : RecordBase{
		public VAppmenuProxyMap_Record(){}
		/*欄位資訊 Start*/
		string _PROXY_UUID=null;
		string _PROXY_ACTION=null;
		string _PROXY_METHOD=null;
		string _PROXY_DESCRIPTION=null;
		string _PROXY_TYPE=null;
		string _NEED_REDIRECT=null;
		string _REDIRECT_PROXY_ACTION=null;
		string _REDIRECT_PROXY_METHOD=null;
		string _REDIRECT_SRC=null;
		string _APPLICATION_HEAD_UUID=null;
		string _NAME_ZH_TW=null;
		string _NAME_ZH_CN=null;
		string _NAME_EN_US=null;
		string _UUID=null;
		string _APPMENU_PROXY_UUID=null;
		string _APPMENU_UUID=null;
		/*欄位資訊 End*/

		[ColumnName("PROXY_UUID",true,typeof(string))]
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

		[ColumnName("APPMENU_PROXY_UUID",true,typeof(string))]
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
		public VAppmenuProxyMap_Record Clone(){
			try{
				return this.Clone<VAppmenuProxyMap_Record>(this);
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		public VAppmenuProxyMap gotoTable(){
			try{
				var dbc = LK.Config.DataBase.Factory.getInfo();
				VAppmenuProxyMap ret = new VAppmenuProxyMap(dbc,this);
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
		public List<Appmenu_Record> Link_Appmenu_By_Uuid()
		{
			try{
				List<Appmenu_Record> ret= new List<Appmenu_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Appmenu ___table = new Appmenu(dbc);
				ret=(List<Appmenu_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.UUID,this.APPMENU_UUID))
					.FetchAll<Appmenu_Record>() ; 
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
		public List<AppmenuProxyMap_Record> Link_AppmenuProxyMap_By_Uuid()
		{
			try{
				List<AppmenuProxyMap_Record> ret= new List<AppmenuProxyMap_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				AppmenuProxyMap ___table = new AppmenuProxyMap(dbc);
				ret=(List<AppmenuProxyMap_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.UUID,this.APPMENU_PROXY_UUID))
					.FetchAll<AppmenuProxyMap_Record>() ; 
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
		public List<Appmenu_Record> Link_Appmenu_By_Uuid(OrderLimit limit)
		{
			try{
				List<Appmenu_Record> ret= new List<Appmenu_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Appmenu ___table = new Appmenu(dbc);
				ret=(List<Appmenu_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.UUID,this.APPMENU_UUID))
					.Order(limit)
					.Limit(limit)
					.FetchAll<Appmenu_Record>() ; 
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
		public List<AppmenuProxyMap_Record> Link_AppmenuProxyMap_By_Uuid(OrderLimit limit)
		{
			try{
				List<AppmenuProxyMap_Record> ret= new List<AppmenuProxyMap_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				AppmenuProxyMap ___table = new AppmenuProxyMap(dbc);
				ret=(List<AppmenuProxyMap_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.UUID,this.APPMENU_PROXY_UUID))
					.Order(limit)
					.Limit(limit)
					.FetchAll<AppmenuProxyMap_Record>() ; 
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
		public Appmenu LinkFill_Appmenu_By_Uuid()
		{
			try{
				var data = Link_Appmenu_By_Uuid();
				Appmenu ret=new Appmenu(data);
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
		public AppmenuProxyMap LinkFill_AppmenuProxyMap_By_Uuid()
		{
			try{
				var data = Link_AppmenuProxyMap_By_Uuid();
				AppmenuProxyMap ret=new AppmenuProxyMap(data);
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
		public Appmenu LinkFill_Appmenu_By_Uuid(OrderLimit limit)
		{
			try{
				var data = Link_Appmenu_By_Uuid(limit);
				Appmenu ret=new Appmenu(data);
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
		public AppmenuProxyMap LinkFill_AppmenuProxyMap_By_Uuid(OrderLimit limit)
		{
			try{
				var data = Link_AppmenuProxyMap_By_Uuid(limit);
				AppmenuProxyMap ret=new AppmenuProxyMap(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
	}
}
