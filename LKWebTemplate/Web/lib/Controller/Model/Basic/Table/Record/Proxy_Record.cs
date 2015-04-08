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
	[TableView("PROXY", true)]
	[LkDataBase("BASIC")]
	[Serializable]
	public class Proxy_Record : RecordBase{
		public Proxy_Record(){}
		/*欄位資訊 Start*/
		string _UUID=null;
		string _PROXY_ACTION=null;
		string _PROXY_METHOD=null;
		string _DESCRIPTION=null;
		string _PROXY_TYPE=null;
		string _NEED_REDIRECT=null;
		string _REDIRECT_PROXY_ACTION=null;
		string _REDIRECT_PROXY_METHOD=null;
		string _APPLICATION_HEAD_UUID=null;
		string _REDIRECT_SRC=null;
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

		[ColumnName("DESCRIPTION",false,typeof(string))]
		public string DESCRIPTION
		{
			set
			{
				_DESCRIPTION=value;
			}
			get
			{
				return _DESCRIPTION;
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
		public Proxy_Record Clone(){
			try{
				return this.Clone<Proxy_Record>(this);
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		public Proxy gotoTable(){
			try{
				var dbc = LK.Config.DataBase.Factory.getInfo();
				Proxy ret = new Proxy(dbc,this);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180347*/
		public List<AppmenuProxyMap_Record> Link_AppmenuProxyMap_By_ProxyUuid()
		{
			try{
				List<AppmenuProxyMap_Record> ret= new List<AppmenuProxyMap_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				AppmenuProxyMap ___table = new AppmenuProxyMap(dbc);
				ret=(List<AppmenuProxyMap_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.PROXY_UUID,this.UUID))
					.FetchAll<AppmenuProxyMap_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180347*/
		public List<VAppmenuProxyMap_Record> Link_VAppmenuProxyMap_By_ProxyUuid()
		{
			try{
				List<VAppmenuProxyMap_Record> ret= new List<VAppmenuProxyMap_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				VAppmenuProxyMap ___table = new VAppmenuProxyMap(dbc);
				ret=(List<VAppmenuProxyMap_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.PROXY_UUID,this.UUID))
					.FetchAll<VAppmenuProxyMap_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180347*/
		public List<VAuthProxy_Record> Link_VAuthProxy_By_ProxyUuid()
		{
			try{
				List<VAuthProxy_Record> ret= new List<VAuthProxy_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				VAuthProxy ___table = new VAuthProxy(dbc);
				ret=(List<VAuthProxy_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.PROXY_UUID,this.UUID))
					.FetchAll<VAuthProxy_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180348*/
		public List<AppmenuProxyMap_Record> Link_AppmenuProxyMap_By_ProxyUuid(OrderLimit limit)
		{
			try{
				List<AppmenuProxyMap_Record> ret= new List<AppmenuProxyMap_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				AppmenuProxyMap ___table = new AppmenuProxyMap(dbc);
				ret=(List<AppmenuProxyMap_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.PROXY_UUID,this.UUID))
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
		/*201303180348*/
		public List<VAppmenuProxyMap_Record> Link_VAppmenuProxyMap_By_ProxyUuid(OrderLimit limit)
		{
			try{
				List<VAppmenuProxyMap_Record> ret= new List<VAppmenuProxyMap_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				VAppmenuProxyMap ___table = new VAppmenuProxyMap(dbc);
				ret=(List<VAppmenuProxyMap_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.PROXY_UUID,this.UUID))
					.Order(limit)
					.Limit(limit)
					.FetchAll<VAppmenuProxyMap_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180348*/
		public List<VAuthProxy_Record> Link_VAuthProxy_By_ProxyUuid(OrderLimit limit)
		{
			try{
				List<VAuthProxy_Record> ret= new List<VAuthProxy_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				VAuthProxy ___table = new VAuthProxy(dbc);
				ret=(List<VAuthProxy_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.PROXY_UUID,this.UUID))
					.Order(limit)
					.Limit(limit)
					.FetchAll<VAuthProxy_Record>() ; 
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
		/*201303180357*/
		public AppmenuProxyMap LinkFill_AppmenuProxyMap_By_ProxyUuid()
		{
			try{
				var data = Link_AppmenuProxyMap_By_ProxyUuid();
				AppmenuProxyMap ret=new AppmenuProxyMap(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180357*/
		public VAppmenuProxyMap LinkFill_VAppmenuProxyMap_By_ProxyUuid()
		{
			try{
				var data = Link_VAppmenuProxyMap_By_ProxyUuid();
				VAppmenuProxyMap ret=new VAppmenuProxyMap(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180357*/
		public VAuthProxy LinkFill_VAuthProxy_By_ProxyUuid()
		{
			try{
				var data = Link_VAuthProxy_By_ProxyUuid();
				VAuthProxy ret=new VAuthProxy(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180358*/
		public AppmenuProxyMap LinkFill_AppmenuProxyMap_By_ProxyUuid(OrderLimit limit)
		{
			try{
				var data = Link_AppmenuProxyMap_By_ProxyUuid(limit);
				AppmenuProxyMap ret=new AppmenuProxyMap(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180358*/
		public VAppmenuProxyMap LinkFill_VAppmenuProxyMap_By_ProxyUuid(OrderLimit limit)
		{
			try{
				var data = Link_VAppmenuProxyMap_By_ProxyUuid(limit);
				VAppmenuProxyMap ret=new VAppmenuProxyMap(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180358*/
		public VAuthProxy LinkFill_VAuthProxy_By_ProxyUuid(OrderLimit limit)
		{
			try{
				var data = Link_VAuthProxy_By_ProxyUuid(limit);
				VAuthProxy ret=new VAuthProxy(data);
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
	}
}
