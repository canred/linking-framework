using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LK.Attribute;
using LK.DB;  
using log4net;  
using System.Reflection;  
using LK.DB.SQLCreater;  
using LKWebTemplate.Controller.Model.Cloud.Table;
namespace LKWebTemplate.Controller.Model.Cloud
{
	[ModelName("Cloud")]
	[LkDataBase("BASIC")]
	public partial class CloudModel
	{
		public new static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		private LK.Config.DataBase.IDataBaseConfigInfo dbc = null;
		public CloudModel(){}
		/*Templete Model A001*/

        public LKWebTemplate.Model.Basic.Table.ActiveConnection getActiveConnection_By_Uuid(string pUUID)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                LKWebTemplate.Model.Basic.Table.ActiveConnection activeconnection = new LKWebTemplate.Model.Basic.Table.ActiveConnection(dbc);
                activeconnection.Fill_By_PK(pUUID);
                return activeconnection;
            }
            catch (Exception ex)
            {
                log.Error(ex); LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }


	}
}
