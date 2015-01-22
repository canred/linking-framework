using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace LK.Util.Mail
{
    public interface ISMTPConfig
    {
        System.Xml.XmlDocument GetBaseConfig();
        System.Data.DataTable GetBaseConfig_DataTable();
        string GetTag(string actionTag, bool alwaysReLoad);
        string GetTag(string ActionTag);
        //string GetDB(string dataBaseName);
        //string GetDBType();
        //string GetDBType(string dbName);
        //string GetWhere(string dbName);
        //string GetSchema(string dbName);
        //string GetCaseSensitive(string dbName);
    }
}
