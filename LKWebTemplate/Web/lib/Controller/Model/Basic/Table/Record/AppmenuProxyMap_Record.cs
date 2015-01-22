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
	[TableView("APPMENU_PROXY_MAP", true)]
	[LkDataBase("BASIC")]
	[Serializable]
	public class AppmenuProxyMap_Record : RecordBase{
		public AppmenuProxyMap_Record(){}
		/*欄位資訊 Start*/
		string _UUID=null;
		string _PROXY_UUID=null;
		string _APPMENU_UUID=null;
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
		public AppmenuProxyMap_Record Clone(){
			try{
				return this.Clone<AppmenuProxyMap_Record>(this);
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		public AppmenuProxyMap gotoTable(){
			try{
				var dbc = LK.Config.DataBase.Factory.getInfo();
				AppmenuProxyMap ret = new AppmenuProxyMap(dbc,this);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180347*/
		public List<VAppmenuProxyMap_Record> Link_VAppmenuProxyMap_By_AppmenuProxyUuid()
		{
			try{
				List<VAppmenuProxyMap_Record> ret= new List<VAppmenuProxyMap_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				VAppmenuProxyMap ___table = new VAppmenuProxyMap(dbc);
				ret=(List<VAppmenuProxyMap_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.APPMENU_PROXY_UUID,this.UUID))
					.FetchAll<VAppmenuProxyMap_Record>() ; 
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180348*/
		public List<VAppmenuProxyMap_Record> Link_VAppmenuProxyMap_By_AppmenuProxyUuid(OrderLimit limit)
		{
			try{
				List<VAppmenuProxyMap_Record> ret= new List<VAppmenuProxyMap_Record>();
				var dbc = LK.Config.DataBase.Factory.getInfo();
				VAppmenuProxyMap ___table = new VAppmenuProxyMap(dbc);
				ret=(List<VAppmenuProxyMap_Record>)
										___table.Where(new SQLCondition(___table)
										.Equal(___table.APPMENU_PROXY_UUID,this.UUID))
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
		/*201303180357*/
		public VAppmenuProxyMap LinkFill_VAppmenuProxyMap_By_AppmenuProxyUuid()
		{
			try{
				var data = Link_VAppmenuProxyMap_By_AppmenuProxyUuid();
				VAppmenuProxyMap ret=new VAppmenuProxyMap(data);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*201303180358*/
		public VAppmenuProxyMap LinkFill_VAppmenuProxyMap_By_AppmenuProxyUuid(OrderLimit limit)
		{
			try{
				var data = Link_VAppmenuProxyMap_By_AppmenuProxyUuid(limit);
				VAppmenuProxyMap ret=new VAppmenuProxyMap(data);
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
	}
}
