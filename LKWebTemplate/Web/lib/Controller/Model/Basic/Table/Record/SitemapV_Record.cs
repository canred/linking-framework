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
	[TableView("SITEMAP_V", false)]
	[LkDataBase("BASIC")]
	[Serializable]
	public class SitemapV_Record : RecordBase{
		public SitemapV_Record(){}
		/*欄位資訊 Start*/
		string _UUID=null;
		string _IS_ACTIVE=null;
		DateTime? _CREATE_DATE=null;
		string _CREATE_USER=null;
		DateTime? _UPDATE_DATE=null;
		string _UPDATE_USER=null;
		string _SITEMAP_UUID=null;
		string _APPPAGE_UUID=null;
		string _ROOT_UUID=null;
		string _HASCHILD=null;
		string _APPLICATION_HEAD_UUID=null;
		string _NAME=null;
		string _DESCRIPTION=null;
		string _URL=null;
		string _P_MODE=null;
		string _PARAMETER_CLASS=null;
		string _APPPAGE_IS_ACTIVE=null;
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

		[ColumnName("APPPAGE_UUID",false,typeof(string))]
		public string APPPAGE_UUID
		{
			set
			{
				_APPPAGE_UUID=value;
			}
			get
			{
				return _APPPAGE_UUID;
			}
		}

		[ColumnName("ROOT_UUID",false,typeof(string))]
		public string ROOT_UUID
		{
			set
			{
				_ROOT_UUID=value;
			}
			get
			{
				return _ROOT_UUID;
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

		[ColumnName("NAME",false,typeof(string))]
		public string NAME
		{
			set
			{
				_NAME=value;
			}
			get
			{
				return _NAME;
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

		[ColumnName("APPPAGE_IS_ACTIVE",false,typeof(string))]
		public string APPPAGE_IS_ACTIVE
		{
			set
			{
				_APPPAGE_IS_ACTIVE=value;
			}
			get
			{
				return _APPPAGE_IS_ACTIVE;
			}
		}
		public SitemapV_Record Clone(){
			try{
				return this.Clone<SitemapV_Record>(this);
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		public SitemapV gotoTable(){
			try{
				var dbc = LK.Config.DataBase.Factory.getInfo();
				SitemapV ret = new SitemapV(dbc,this);
				return ret;
			}
			catch (Exception ex){
				log.Error(ex);LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
	}
}
