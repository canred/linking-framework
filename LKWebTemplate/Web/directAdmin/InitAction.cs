#region USING
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using ExtDirect;
using ExtDirect.Direct;
using LK.DB.SQLCreater;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using LKWebTemplate.Model.Basic;
using LKWebTemplate.Model.Basic.Table;
using LKWebTemplate.Model.Basic.Table.Record;
using LKWebTemplate;
using System.Text;
using LK.Util;
using System.Data;
using System.Collections.Specialized;
using System.Reflection;
using System.Configuration;
using System.Xml;
using System.Diagnostics;
#endregion
[DirectService("InitAction")]
public partial class InitAction : BaseAction
{
    [DirectMethod("renameDirectNamespace", DirectAction.Load)]
    public JObject renameDirectNamespace(string orgName, string newName, Request request)
    {
        #region Declare
        List<JObject> jobject = new List<JObject>();
        BasicModel basicModel = new BasicModel();
        ErrorLog table = new ErrorLog();
        #endregion
        try
        { /*權限檢查*/
            /*
            if (!checkProxy(new StackTrace().GetFrame(0)))
            {
                throw new Exception("Permission Denied!");
            };
             */
            /*是Store操作一下就可能含有分頁資訊。*/

            /*
                *.aspx
                *.js(除了/js)
             */
            string rootPath = request.HttpRequest.MapPath("~");
            var allfilepath = getAllChangeFilePath(rootPath);
            var allReplaceKeyWord = getAllFunction(orgName);
            foreach (string file in allfilepath)
            {
                System.IO.StreamReader sr = new StreamReader(file);
                var content = sr.ReadToEnd();
                sr.Close();
                var needReplace = false;
                foreach (string keyWord in allReplaceKeyWord)
                {
                    content = content.Replace(keyWord, newName + "." + keyWord.Substring(keyWord.IndexOf(".") + 1));
                    needReplace = true;
                }

                if (needReplace)
                {
                    System.IO.StreamWriter sw = new StreamWriter(file, false, Encoding.UTF8);
                    sw.WriteLine(content);
                    sw.Close();
                }
            }           
            /*使用Store Std out 『Sotre物件標準輸出格式』*/
            return ExtDirect.Direct.Helper.Message.Success.OutputJObject();
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
            /*將Exception轉成EXT Exception JSON格式*/
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    public List<string> getAllFunction(string prexString)
    {
        List<string> ret = new List<string>();
        AppDomain MyDomain = AppDomain.CurrentDomain;
        Assembly[] AssembliesLoaded = MyDomain.GetAssemblies();
        foreach (var allAssembly in AssembliesLoaded)
        {
            foreach (var theType in allAssembly.GetTypes())
            {
                object[] allCustomAttribute = theType.GetCustomAttributes(false);
                foreach (var _theType in allCustomAttribute)
                {
                    Type directType = _theType.GetType();
                    if (typeof(DirectServiceAttribute) == directType)
                    {
                        string className = theType.Name;
                        List<string> allFunction = DirectProxyGenerator.generateDirectApiNameList(className, prexString);
                        if (allFunction.Count() > 0)
                        {
                            foreach (var item in allFunction)
                            {
                                ret.Add(item);
                            }
                        }
                    }
                }
            }
        }
        return ret;
    }

    public List<string> getAllChangeFilePath(string rootPath)
    {
        List<string> ret = new List<string>();
        if (rootPath.EndsWith("\\js"))
        {
            return ret;
        }
        System.IO.DirectoryInfo di = new DirectoryInfo(rootPath);
        foreach (var tmpDi in di.GetDirectories())
        {
            var itemall = getAllChangeFilePath(tmpDi.FullName);
            foreach (string subItem in itemall)
            {
                ret.Add(subItem);
            }
        }
        foreach (var tmpItem in di.GetFiles("*.js"))
        {
            ret.Add(tmpItem.FullName);
        }
        foreach (var tmpItem in di.GetFiles("*.aspx"))
        {
            ret.Add(tmpItem.FullName);
        }
        return ret;
    }

    [DirectMethod("loadBasic", DirectAction.Load)]
    public JObject loadBasic(Request request)
    {
        #region Declare
        List<JObject> jobject = new List<JObject>();
        BasicModel basicModel = new BasicModel();
        ErrorLog table = new ErrorLog();
        #endregion
        try
        { /*權限檢查*/
            if (!checkProxy(new StackTrace().GetFrame(0)))
            {
                throw new Exception("Permission Denied!");
            };
            /*是Store操作一下就可能含有分頁資訊。*/
            var dbc = LK.Config.DataBase.Factory.getInfo();
            var dt = dbc.GetBaseConfig_DataTable();
            var row = dt.Select("KEY='DB->BASIC'");
            if (row.Count() > 0)
            {
                /*使用Store Std out 『Sotre物件標準輸出格式』*/
                return ExtDirect.Direct.Helper.Store.OutputJObject(JsonHelper.DataRowSerializerJObject(row[0]));
            }
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(new Exception("未知的錯誤!"));
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
            /*將Exception轉成EXT Exception JSON格式*/
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    [DirectMethod("loadException", DirectAction.Load)]
    public JObject loadException(Request request)
    {
        #region Declare
        List<JObject> jobject = new List<JObject>();
        BasicModel basicModel = new BasicModel();
        ErrorLog table = new ErrorLog();
        #endregion
        try
        { /*權限檢查*/
            if (!checkProxy(new StackTrace().GetFrame(0)))
            {
                throw new Exception("Permission Denied!");
            };
            /*是Store操作一下就可能含有分頁資訊。*/
            var dbc = LK.Config.DataBase.Factory.getInfo();
            var dt = dbc.GetBaseConfig_DataTable();
            var row = dt.Select("KEY='DB->MyException'");
            if (row.Count() > 0)
            {
                /*使用Store Std out 『Sotre物件標準輸出格式』*/
                return ExtDirect.Direct.Helper.Store.OutputJObject(JsonHelper.DataRowSerializerJObject(row[0]));
            }
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(new Exception("未知的錯誤!"));
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
            /*將Exception轉成EXT Exception JSON格式*/
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    [DirectMethod("loadAction", DirectAction.Load)]
    public JObject loadAction(Request request)
    {
        #region Declare
        List<JObject> jobject = new List<JObject>();
        BasicModel basicModel = new BasicModel();
        ErrorLog table = new ErrorLog();
        #endregion
        try
        { /*權限檢查*/
            if (!checkProxy(new StackTrace().GetFrame(0)))
            {
                throw new Exception("Permission Denied!");
            };
            /*是Store操作一下就可能含有分頁資訊。*/
            var dbc = LK.Config.DataBase.Factory.getInfo();
            var dt = dbc.GetBaseConfig_DataTable();
            var row = dt.Select("KEY='DB->ActionLog'");
            if (row.Count() > 0)
            {
                /*使用Store Std out 『Sotre物件標準輸出格式』*/
                return ExtDirect.Direct.Helper.Store.OutputJObject(JsonHelper.DataRowSerializerJObject(row[0]));
            }
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(new Exception("未知的錯誤!"));
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
            /*將Exception轉成EXT Exception JSON格式*/
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    [DirectMethod("loadParameter", DirectAction.Load)]
    public JObject loadParameter(Request request)
    {
        #region Declare
        List<JObject> jobject = new List<JObject>();
        BasicModel basicModel = new BasicModel();
        ErrorLog table = new ErrorLog();
        // OrderLimit orderLimit = null;
        #endregion
        try
        { /*權限檢查*/
            if (!checkProxy(new StackTrace().GetFrame(0)))
            {
                throw new Exception("Permission Denied!");
            };
            DataTable dt = new DataTable();
            dt.Columns.Add("AppName");
            dt.Columns.Add("AuthenticationType");
            dt.Columns.Add("EnableGuestLogin");
            dt.Columns.Add("GuestCompany");
            dt.Columns.Add("GuestAccount");
            dt.Columns.Add("IsProductionServer");
            dt.Columns.Add("DefaultPage");
            dt.Columns.Add("LogonPage");
            dt.Columns.Add("WebRoot");
            dt.Columns.Add("DEVUserCompany");
            dt.Columns.Add("DEVUserAccount");
            dt.Columns.Add("DEVUserPassword");
            dt.Columns.Add("NoPermissionPage");
            dt.Columns.Add("UploadFolder");
            dt.Columns.Add("Title");
            dt.Columns.Add("SystemIcon");
            dt.Columns.Add("CompanyImage");
            dt.Columns.Add("SystemName");
            dt.Columns.Add("SystemDescription");
            dt.Columns.Add("SystemFoolter");
            dt.Columns.Add("InitCompany");
            dt.Columns.Add("InitCompanyUuid");
            dt.Columns.Add("InitAdmin");
            dt.Columns.Add("InitAdminUuid");
            dt.Columns.Add("InitAppUuid");
            dt.Columns.Add("LogoutPage");
            dt.Columns.Add("DirectApplicationName");
            dt.Columns.Add("DirectTimeOut");
            dt.AcceptChanges();
            var row = dt.NewRow();
            row["AppName"] = LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().AppName;
            row["AuthenticationType"] = LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().AuthenticationType;
            row["EnableGuestLogin"] = LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().EnableGuestLogin.ToString().ToLower();
            row["GuestCompany"] = LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().GuestCompany;
            row["GuestAccount"] = LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().GuestAccount;
            row["IsProductionServer"] = LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().IsProductionServer.ToString().ToLower();
            row["DefaultPage"] = LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().DefaultPage;
            row["LogonPage"] = LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().LogonPage;
            row["WebRoot"] = LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().WebRoot;
            row["DEVUserCompany"] = LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().DEVUserCompany;
            row["DEVUserAccount"] = LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().DEVUserAccount;
            row["DEVUserPassword"] = LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().DEVUserPassword;
            row["NoPermissionPage"] = LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().NoPermissionPage;
            row["UploadFolder"] = LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().UploadFolder;
            row["Title"] = LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().Title;
            row["SystemIcon"] = LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().SystemIcon;
            row["CompanyImage"] = LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().CompanyImage;
            row["SystemName"] = LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().SystemName;
            row["SystemDescription"] = LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().SystemDescription;
            row["SystemFoolter"] = LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().SystemFoolter;
            row["InitCompany"] = LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().InitCompany;
            row["InitCompanyUuid"] = LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().InitCompanyUuid == "" ? LK.Util.UID.Instance.GetUniqueID() : LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().InitCompanyUuid;
            row["InitAdmin"] = LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().InitAdmin;
            row["InitAdminUuid"] = LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().InitAdminUuid == "" ? LK.Util.UID.Instance.GetUniqueID() : LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().InitAdminUuid;
            row["InitAppUuid"] = LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().InitAppUuid == "" ? LK.Util.UID.Instance.GetUniqueID() : LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().InitAppUuid;
            row["LogoutPage"] = LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().LogoutPage;
            row["DirectApplicationName"] = LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().DirectApplicationName;
            row["DirectTimeOut"] = LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().DirectTimeOut.ToString();
            /*使用Store Std out 『Sotre物件標準輸出格式』*/
            return ExtDirect.Direct.Helper.Store.OutputJObject(JsonHelper.DataRowSerializerJObject(row));
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
            /*將Exception轉成EXT Exception JSON格式*/
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    [DirectMethod("submitParameter", DirectAction.Load)]
    public JObject submitParameter(
        string AppName, string AuthenticationType, string EnableGuestLogin,
        string GuestCompany, string GuestAccount, string IsProductionServer,
        string DefaultPage, string LogonPage, string WebRoot,
        string NoPermissionPage, string UploadFolder, string Title,
        string SystemIcon, string CompanyImage, string SystemName,
        string SystemDescription, string SystemFoolter,
        string InitCompany,
        string InitCompanyUuid,
        string InitAdmin,
        string InitAdminUuid,
        string InitAppUuid,
        string LogoutPage,
        string DEVUserCompany,
        string DEVUserAccount,
        string DEVUserPassword,
        string DirectApplicationName,
        string DirectTimeOut,
        Request request)
    {
        try
        {
            /*權限檢查*/
            if (!checkProxy(new StackTrace().GetFrame(0)))
            {
                throw new Exception("Permission Denied!");
            };
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.Load(ConfigFilePath(ConfigType.ParemterFilePath));
            doc.GetElementsByTagName("AppName")[0].InnerText = AppName;
            doc.GetElementsByTagName("AuthenticationType")[0].InnerText = AuthenticationType;
            doc.GetElementsByTagName("EnableGuestLogin")[0].InnerText = EnableGuestLogin;
            doc.GetElementsByTagName("DefaultPage")[0].InnerText = DefaultPage;
            doc.GetElementsByTagName("LogonPage")[0].InnerText = LogonPage;
            doc.GetElementsByTagName("WebRoot")[0].InnerText = WebRoot;
            doc.GetElementsByTagName("NoPermissionPage")[0].InnerText = NoPermissionPage;
            doc.GetElementsByTagName("UploadFolder")[0].InnerText = UploadFolder;
            doc.GetElementsByTagName("Title")[0].InnerText = Title;
            doc.GetElementsByTagName("SystemIcon")[0].InnerText = SystemIcon;
            doc.GetElementsByTagName("CompanyImage")[0].InnerText = CompanyImage;
            doc.GetElementsByTagName("SystemName")[0].InnerText = SystemName;
            doc.GetElementsByTagName("SystemDescription")[0].InnerText = SystemDescription;
            doc.GetElementsByTagName("SystemFoolter")[0].InnerText = SystemFoolter;
            doc.GetElementsByTagName("InitCompany")[0].InnerText = InitCompany;
            doc.GetElementsByTagName("InitCompanyUuid")[0].InnerText = InitCompanyUuid;
            doc.GetElementsByTagName("InitAdmin")[0].InnerText = InitAdmin;
            doc.GetElementsByTagName("InitAdminUuid")[0].InnerText = InitAdminUuid;
            doc.GetElementsByTagName("InitAppUuid")[0].InnerText = InitAppUuid;
            doc.GetElementsByTagName("LogoutPage")[0].InnerText = LogoutPage;
            doc.GetElementsByTagName("DEVUserAccount")[0].InnerText = DEVUserAccount;
            doc.GetElementsByTagName("DEVUserPassword")[0].InnerText = DEVUserPassword;
            doc.GetElementsByTagName("DEVUserCompany")[0].InnerText = DEVUserCompany;
            doc.GetElementsByTagName("IsProductionServer")[0].InnerText = IsProductionServer;
            doc.GetElementsByTagName("DirectApplicationName")[0].InnerText = DirectApplicationName;
            doc.GetElementsByTagName("DirectTimeOut")[0].InnerText = DirectTimeOut;
            doc.Save(ConfigFilePath(ConfigType.ParemterFilePath));
            return ExtDirect.Direct.Helper.Message.Success.OutputJObject();
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
            /*將Exception轉成EXT Exception JSON格式*/
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    [DirectMethod("testConnection", DirectAction.Load)]
    public JObject testConnection(string type, Request request)
    {
        try
        {
            /*權限檢查*/
            if (!checkProxy(new StackTrace().GetFrame(0)))
            {
                throw new Exception("Permission Denied!");
            };
            var dbc = LK.Config.DataBase.Factory.getInfo();
            LK.DB.DataBaseConnection dbConnection = new LK.DB.DataBaseConnection(dbc);
            LK.DB.DB db = null;
            type = type.ToUpper();
            switch (type)
            {
                case "BASIC":
                    db = new LK.DB.DB(dbConnection, "BASIC");
                    break;
                case "EXCEPTION":
                    db = new LK.DB.DB(dbConnection, "MyException");
                    break;
                case "ACTION":
                    db = new LK.DB.DB(dbConnection, "ActionLog");
                    break;
            }
            if (db != null && db.TestConnection())
            {
                return ExtDirect.Direct.Helper.Message.Success.OutputJObject();
            }
            else
            {
                return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(new Exception("無法連接!"));
            }
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
            /*將Exception轉成EXT Exception JSON格式*/
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    [DirectMethod("submitBasic", DirectAction.Store)]
    public JObject submitBasic(string type, string connection, Request request)
    {
        #region Declare
        List<JObject> jobject = new List<JObject>();
        #endregion
        try
        {
            /*權限檢查*/
            if (!checkProxy(new StackTrace().GetFrame(0)))
            {
                throw new Exception("Permission Denied!");
            };
            /*是Store操作一下就可能含有分頁資訊。*/
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.Load(ConfigFilePath(ConfigType.DataBaseConfig));

            var allDb = doc.GetElementsByTagName("DB");
            foreach (XmlNode item in allDb)
            {
                if (item.Attributes["Name"].Value.ToUpper() == "BASIC")
                {
                    item.Attributes["Type"].Value = type.ToUpper();
                    item.ChildNodes[0].InnerText = connection;
                }
            }
            doc.Save(ConfigFilePath(ConfigType.DataBaseConfig));
            return ExtDirect.Direct.Helper.Message.Success.OutputJObject();
        }
        catch (Exception ex)
        {
            log.Error(ex);
            LK.MyException.MyException.Error(this, ex);
            /*將Exception轉成EXT Exception JSON格式*/
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    [DirectMethod("submitException", DirectAction.Store)]
    public JObject submitException(string type, string connection, Request request)
    {
        #region Declare
        List<JObject> jobject = new List<JObject>();
        #endregion
        try
        {
            /*權限檢查*/
            if (!checkProxy(new StackTrace().GetFrame(0)))
            {
                throw new Exception("Permission Denied!");
            };
            /*是Store操作一下就可能含有分頁資訊。*/
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.Load(ConfigFilePath(ConfigType.DataBaseConfig));

            var allDb = doc.GetElementsByTagName("DB");
            foreach (XmlNode item in allDb)
            {
                if (item.Attributes["Name"].Value == "MyException")
                {
                    item.Attributes["Type"].Value = type.ToUpper();
                    item.ChildNodes[0].InnerText = connection;
                }
            }
            doc.Save(ConfigFilePath(ConfigType.DataBaseConfig));
            return ExtDirect.Direct.Helper.Message.Success.OutputJObject();
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
            /*將Exception轉成EXT Exception JSON格式*/
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    [DirectMethod("submitActionLog", DirectAction.Store)]
    public JObject submitActionLog(string type, string connection, Request request)
    {
        #region Declare
        List<JObject> jobject = new List<JObject>();
        #endregion
        try
        {
            /*權限檢查*/
            if (!checkProxy(new StackTrace().GetFrame(0)))
            {
                throw new Exception("Permission Denied!");
            };
            /*是Store操作一下就可能含有分頁資訊。*/
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.Load(ConfigFilePath(ConfigType.DataBaseConfig));

            var allDb = doc.GetElementsByTagName("DB");
            foreach (XmlNode item in allDb)
            {
                if (item.Attributes["Name"].Value == "ActionLog")
                {
                    item.Attributes["Type"].Value = type.ToUpper();
                    item.ChildNodes[0].InnerText = connection;
                }
            }
            doc.Save(ConfigFilePath(ConfigType.DataBaseConfig));
            return ExtDirect.Direct.Helper.Message.Success.OutputJObject();
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
            /*將Exception轉成EXT Exception JSON格式*/
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    [DirectMethod("initBasic", DirectAction.Store)]
    public JObject initBasic(Request request)
    {
        try
        {
            /*權限檢查*/
            if (!checkProxy(new StackTrace().GetFrame(0)))
            {
                throw new Exception("Permission Denied!");
            };
            var dbc = LK.Config.DataBase.Factory.getInfo();
            LK.DB.DataBaseConnection dbcon = new LK.DB.DataBaseConnection(dbc);
            LK.DB.DB db = new LK.DB.DB(dbcon, "BASIC");
            var dbType = dbcon.getDataBaseType("BASIC");
            var dbConnectionUser = dbcon.getConnectionUser("BASIC");
            var dbTypeStr = "";
            if (dbType == LK.DB.ADataBaseConnection.DataBaseType.MsSQL)
            {
                dbTypeStr = "MSSQL";
            }
            else if (dbType == LK.DB.ADataBaseConnection.DataBaseType.Oracle)
            {
                dbTypeStr = "ORACLE";
            }
            else if (dbType == LK.DB.ADataBaseConnection.DataBaseType.MySql)
            {
                dbTypeStr = "MYSQL";
            }
            else
            {
                return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(new Exception("未實作的資料庫!"));
            }
            db.TestConnection();
            var fileUrlTable = AppDomain.CurrentDomain.BaseDirectory + "initDataBase\\SQL\\" + dbTypeStr + "\\TABLE\\Table.sql";
            var fileUrlView = AppDomain.CurrentDomain.BaseDirectory + "initDataBase\\SQL\\" + dbTypeStr + "\\VIEW\\view.sql";
            var fileUrlFP = AppDomain.CurrentDomain.BaseDirectory + "initDataBase\\SQL\\" + dbTypeStr + "\\FP\\fp.sql";
            FileInfo fileTable = new FileInfo(fileUrlTable);
            string scriptTable = fileTable.OpenText().ReadToEnd();
            if (dbType == LK.DB.ADataBaseConnection.DataBaseType.Oracle)
            {
                scriptTable = scriptTable.Replace("{user}", dbConnectionUser).Replace("\r", "").Replace("\n", "");
            }
            if (dbType == LK.DB.ADataBaseConnection.DataBaseType.Oracle)
            {
                foreach (var sql in scriptTable.Split(';'))
                {
                    if (sql.Trim().Length > 0)
                    {
                        db.CommandText = sql;
                        db.ExecuteNonQuery();
                    }
                }
            }
            else
            {
                db.CommandText = scriptTable;
                db.setTimeout(1000);
                db.ExecuteNonQuery();
            }
            FileInfo fileFP = new FileInfo(fileUrlFP);
            string scriptFp = fileFP.OpenText().ReadToEnd();
            if (dbType == LK.DB.ADataBaseConnection.DataBaseType.Oracle)
            {
                scriptFp = scriptFp.Replace("{user}", dbConnectionUser);
            }
            if (dbType == LK.DB.ADataBaseConnection.DataBaseType.Oracle)
            {
                foreach (string itemSql in scriptFp.Split('/'))
                {
                    if (itemSql.Trim().Length > 0)
                    {
                        db.CommandText = itemSql;
                        db.ExecuteNonQuery();
                    }
                }
            }
            else if (dbType == LK.DB.ADataBaseConnection.DataBaseType.MsSQL)
            {
                foreach (string itemSql in scriptFp.Split(';'))
                {
                    if (itemSql.Trim().Length > 0)
                    {
                        db.CommandText = itemSql;
                        db.ExecuteNonQuery();
                    }
                }
            }
            else if (dbType == LK.DB.ADataBaseConnection.DataBaseType.MySql)
            {
                foreach (string itemSql in scriptFp.Split('/'))
                {
                    if (itemSql.Trim().Length > 0)
                    {
                        db.CommandText = itemSql;
                        db.ExecuteNonQuery();
                    }
                }
            }
            FileInfo fileView = new FileInfo(fileUrlView);
            string scriptView = fileView.OpenText().ReadToEnd();
            if (dbType == LK.DB.ADataBaseConnection.DataBaseType.Oracle)
            {
                scriptView = scriptView.Replace("{user}", dbConnectionUser);
            }
            foreach (string itemSql in scriptView.Split(';'))
            {
                if (itemSql.Trim().Length > 0)
                {
                    try
                    {
                        db.CommandText = itemSql;
                        db.ExecuteNonQuery();
                    }
                    catch (Exception oracleException)
                    {
                        if (oracleException.ToString().IndexOf("ORA-06575") >= 0)
                        {
                            continue;
                        }
                    }
                }
            }
            return ExtDirect.Direct.Helper.Message.Success.OutputJObject();
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
            /*將Exception轉成EXT Exception JSON格式*/
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    [DirectMethod("initException", DirectAction.Store)]
    public JObject initException(Request request)
    {
        try
        {
            /*權限檢查*/
            if (!checkProxy(new StackTrace().GetFrame(0)))
            {
                throw new Exception("Permission Denied!");
            };
            var dbc = LK.Config.DataBase.Factory.getInfo();
            LK.DB.DataBaseConnection dbcon = new LK.DB.DataBaseConnection(dbc);
            LK.DB.DB db = new LK.DB.DB(dbcon, "MyException");
            var dbType = dbcon.getDataBaseType("MyException");
            var dbConnectionUser = dbcon.getConnectionUser("MyException");
            var dbTypeStr = "";
            if (dbType == LK.DB.ADataBaseConnection.DataBaseType.MsSQL)
            {
                dbTypeStr = "MSSQL";
            }
            else if (dbType == LK.DB.ADataBaseConnection.DataBaseType.Oracle)
            {
                dbTypeStr = "ORACLE";
            }
            else if (dbType == LK.DB.ADataBaseConnection.DataBaseType.MySql)
            {
                dbTypeStr = "MYSQL";
            }
            else
            {
                return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(new Exception("未實作的資料庫!"));
            }
            db.TestConnection();
            var fileUrlTable = AppDomain.CurrentDomain.BaseDirectory + "initDataBase\\SQL\\" + dbTypeStr + "\\TABLE-Exception\\Table.sql";
            var fileUrlView = AppDomain.CurrentDomain.BaseDirectory + "initDataBase\\SQL\\" + dbTypeStr + "\\VIEW-Exception\\view.sql";
            FileInfo fileTable = new FileInfo(fileUrlTable);
            string scriptTable = fileTable.OpenText().ReadToEnd();
            if (dbType == LK.DB.ADataBaseConnection.DataBaseType.Oracle)
            {
                scriptTable = scriptTable.Replace("{user}", dbConnectionUser).Replace("\r", "").Replace("\n", "");
            }
            if (dbType == LK.DB.ADataBaseConnection.DataBaseType.Oracle)
            {
                foreach (var sql in scriptTable.Split(';'))
                {
                    if (sql.Trim().Length > 0)
                    {
                        db.CommandText = sql;
                        db.ExecuteNonQuery();
                    }
                }
            }
            else
            {
                db.CommandText = scriptTable;
                db.ExecuteNonQuery();
            }
            FileInfo fileView = new FileInfo(fileUrlView);
            string scriptView = fileView.OpenText().ReadToEnd();
            if (dbType == LK.DB.ADataBaseConnection.DataBaseType.Oracle)
            {
                scriptView = scriptView.Replace("{user}", dbConnectionUser);
            }
            foreach (string itemSql in scriptView.Split('/'))
            {
                var sql = itemSql.ToString().Trim();

                if (sql.Trim().Length > 0)
                {
                    while (sql.ToString().Trim().EndsWith(";"))
                    {
                        sql = sql.Substring(0, sql.ToString().Length - 1);
                    }
                    db.CommandText = sql;
                    db.ExecuteNonQuery();
                }
            }
            return ExtDirect.Direct.Helper.Message.Success.OutputJObject();
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
            /*將Exception轉成EXT Exception JSON格式*/
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    [DirectMethod("initAction", DirectAction.Store)]
    public JObject initAction(Request request)
    {
        try
        {
            /*權限檢查*/
            if (!checkProxy(new StackTrace().GetFrame(0)))
            {
                throw new Exception("Permission Denied!");
            };
            var dbc = LK.Config.DataBase.Factory.getInfo();
            LK.DB.DataBaseConnection dbcon = new LK.DB.DataBaseConnection(dbc);
            LK.DB.DB db = new LK.DB.DB(dbcon, "ActionLog");
            var dbType = dbcon.getDataBaseType("ActionLog");
            var dbConnectionUser = dbcon.getConnectionUser("ActionLog");
            var dbTypeStr = "";
            if (dbType == LK.DB.ADataBaseConnection.DataBaseType.MsSQL)
            {
                dbTypeStr = "MSSQL";
            }
            else if (dbType == LK.DB.ADataBaseConnection.DataBaseType.Oracle)
            {
                dbTypeStr = "ORACLE";
            }
            else if (dbType == LK.DB.ADataBaseConnection.DataBaseType.MySql)
            {
                dbTypeStr = "MYSQL";
            }
            else
            {
                return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(new Exception("未實作的資料庫!"));
            }
            db.TestConnection();
            var fileUrlTable = AppDomain.CurrentDomain.BaseDirectory + "initDataBase\\SQL\\" + dbTypeStr + "\\TABLE-Action\\Table.sql";
            FileInfo fileTable = new FileInfo(fileUrlTable);
            string scriptTable = fileTable.OpenText().ReadToEnd();
            if (dbType == LK.DB.ADataBaseConnection.DataBaseType.Oracle)
            {
                scriptTable = scriptTable.Replace("{user}", dbConnectionUser).Replace("\r", "").Replace("\n", "");
            }
            if (dbType == LK.DB.ADataBaseConnection.DataBaseType.Oracle)
            {
                foreach (var sql in scriptTable.Split(';'))
                {
                    if (sql.Trim().Length > 0)
                    {
                        db.CommandText = sql;
                        db.ExecuteNonQuery();
                    }
                }
            }
            else
            {
                db.CommandText = scriptTable;
                db.ExecuteNonQuery();
            }
            return ExtDirect.Direct.Helper.Message.Success.OutputJObject();
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
            /*將Exception轉成EXT Exception JSON格式*/
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }



    [DirectMethod("importData", DirectAction.Store)]
    public JObject importData(string isDownload, Request request)
    {
        try
        {
            /*權限檢查*/
            if (!checkProxy(new StackTrace().GetFrame(0)))
            {
                throw new Exception("Permission Denied!");
            };
            isDownload = isDownload.Trim().ToLower();
            if (isDownload.Length == 0)
            {
                return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(new Exception("參數錯誤!"));
            }
            if (!(isDownload == "true" || isDownload == "false"))
            {
                return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(new Exception("參數錯誤!"));
            }
            var dbc = LK.Config.DataBase.Factory.getInfo();
            LK.DB.DataBaseConnection dbcon = new LK.DB.DataBaseConnection(dbc);
            LK.DB.DB db = new LK.DB.DB(dbcon, "BASIC");
            var dbType = dbcon.getDataBaseType("BASIC");
            var dbConnectionUser = dbcon.getConnectionUser("BASIC");
            var dbTypeStr = "";
            if (dbType == LK.DB.ADataBaseConnection.DataBaseType.MsSQL)
            {
                dbTypeStr = "MSSQL";
            }
            else if (dbType == LK.DB.ADataBaseConnection.DataBaseType.Oracle)
            {
                dbTypeStr = "ORACLE";
            }
            else if (dbType == LK.DB.ADataBaseConnection.DataBaseType.MySql)
            {
                dbTypeStr = "MYSQL";
            }
            else
            {
                return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(new Exception("未實作的資料庫!"));
            }
            db.TestConnection();
            var importDataUrl = AppDomain.CurrentDomain.BaseDirectory + "initDataBase\\SQL\\" + dbTypeStr + "\\DATA\\importData.sql";
            var saveUrl = AppDomain.CurrentDomain.BaseDirectory + "initDataBase\\SQL\\" + dbTypeStr + "\\DATA\\save.sql";
            var keyUrl = AppDomain.CurrentDomain.BaseDirectory + "initDataBase\\SQL\\" + dbTypeStr + "\\DATA\\key.txt";
            FileInfo importData = new FileInfo(importDataUrl);
            string scriptImport = importData.OpenText().ReadToEnd();
            System.Collections.Hashtable ht = new System.Collections.Hashtable();
            using (System.IO.TextReader tr = File.OpenText(keyUrl))
            {
                var tmpKey = "";
                while ((tmpKey = tr.ReadLine()) != null)
                {
                    var uuid = LK.Util.UID.Instance.GetUniqueID();
                    switch (tmpKey)
                    {
                        case "{admin}":

                            if (ht.ContainsKey(tmpKey) == false)
                            {
                                ht.Add(tmpKey, LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().InitAdmin);
                            }
                            break;
                        case "{admin_uuid}":
                            if (ht.ContainsKey(tmpKey) == false)
                            {
                                ht.Add(tmpKey, LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().InitAdminUuid);
                            }
                            break;
                        case "{application_name}":
                            if (ht.ContainsKey(tmpKey) == false)
                            {
                                ht.Add(tmpKey, LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().AppName);
                            }
                            break;
                        case "{application_head_uuid}":
                            if (ht.ContainsKey(tmpKey) == false)
                            {
                                ht.Add(tmpKey, LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().InitAppUuid);
                            }
                            break;
                        case "{company}":
                            if (ht.ContainsKey(tmpKey) == false)
                            {
                                ht.Add(tmpKey, LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().InitCompany);
                            }
                            break;
                        case "{company_uuid}":
                            if (ht.ContainsKey(tmpKey) == false)
                            {
                                ht.Add(tmpKey, LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().InitCompanyUuid);
                            }
                            break;
                        default:
                            if (ht.ContainsKey(tmpKey) == false)
                            {
                                ht.Add(tmpKey, LK.Util.UID.Instance.GetUniqueID());
                            }
                            break;
                    }
                }
            }
            foreach (System.Collections.DictionaryEntry item in ht)
            {
                scriptImport = scriptImport.Replace(item.Key.ToString(), item.Value.ToString());
            }
            System.IO.StreamWriter sw = new StreamWriter(saveUrl);
            sw.Write(scriptImport);
            sw.Close();
            var sql = System.IO.File.OpenText(saveUrl).ReadToEnd();
            if (isDownload == "false")
            {
                db = new LK.DB.DB(dbcon, "BASIC");
                if (dbType == LK.DB.ADataBaseConnection.DataBaseType.Oracle || dbType == LK.DB.ADataBaseConnection.DataBaseType.MySql)
                {
                    foreach (var _sql in sql.Split(';'))
                    {
                        if (_sql.Trim().Length > 0)
                        {
                            db.CommandText = _sql;
                            db.ExecuteNonQuery();
                        }
                    }
                }
                else
                {
                    db.CommandText = sql;
                    db.ExecuteNonQuery();
                }
            }
            return ExtDirect.Direct.Helper.Message.Success.OutputJObject();
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
            /*將Exception轉成EXT Exception JSON格式*/
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }
    /*load Config*/
    public enum ConfigType
    {
        DataBaseConfig, ParemterFilePath, CloudFilePath, SMTPConfig
    }

    public static string ConfigFilePath(ConfigType configType)
    {
        string filename = null;
        try
        {
            HttpContext context = HttpContext.Current;
            var nv = new NameValueCollection();
            nv = (NameValueCollection)ConfigurationManager.GetSection("APConfigFiles");
            for (int i = 0; i < nv.AllKeys.Length; i++)
            {
                if (nv.AllKeys[i].ToUpper() == configType.ToString().ToUpper())
                {
                    filename = nv[i];
                    break;
                }
            }
            if (context != null)
            {
                if (filename.IndexOf("~") > -1)
                {
                    filename = context.Server.MapPath(filename);
                }
                else
                {
                    filename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename);
                }
            }
            else
            {
                filename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename.Replace("~/", ""));
            }


            if (!File.Exists(filename))
            {
                throw new Exception("發生錯誤: 沒有正確的" + configType.ToString().ToUpper() + "文件");
            }
            return filename;
        }
        catch (Exception ex)
        {
            log.Error(ex);
            throw ex;
        }
    }


    [DirectMethod("loadSmtp", DirectAction.Load)]
    public JObject loadSmtp(Request request)
    {
        #region Declare
        List<JObject> jobject = new List<JObject>();
        BasicModel basicModel = new BasicModel();
        ErrorLog table = new ErrorLog();        
        #endregion
        try
        { /*權限檢查*/
            if (!checkProxy(new StackTrace().GetFrame(0)))
            {
                throw new Exception("Permission Denied!");
            };
            DataTable dt = new DataTable();
            dt.Columns.Add("SMTPSERVERHOST");
            dt.Columns.Add("SMTPSERVERPORT");
            dt.Columns.Add("ISSEND");
            dt.Columns.Add("FROMEMAIL");
            dt.Columns.Add("ISSENDMAIL");
            dt.Columns.Add("ISSENDADMINMAIL");
            dt.Columns.Add("ADMINISTRATOREMAIL");
            dt.Columns.Add("ISSENDDEBUGMAIL");
            dt.Columns.Add("DEBUGEMAIL");
            dt.Columns.Add("CREDENTIALSACCOUNT");
            dt.Columns.Add("CREDENTIALSPASSWORD");
            dt.AcceptChanges();
            var row = dt.NewRow();
            var smtp = new LK.Util.Mail.SMTPConfigInfo();

            row["SMTPSERVERHOST"] = smtp.SMTPServerHost;
            row["SMTPSERVERPORT"] = smtp.SMTPServerPort;
            row["ISSEND"] = smtp.IsSend;
            row["FROMEMAIL"] = smtp.FromEmail;
            row["ISSENDMAIL"] = smtp.IsSendMail;
            row["ISSENDADMINMAIL"] = smtp.IsSendAdminMail;
            row["ADMINISTRATOREMAIL"] = smtp.AdministratorEmail;
            row["ISSENDDEBUGMAIL"] = smtp.IsSendDebugMail;
            row["CREDENTIALSACCOUNT"] = smtp.CredentialsAccount;
            row["CREDENTIALSPASSWORD"] = smtp.CredentialsPassword;
            row["DEBUGEMAIL"] = smtp.DebugEmail;
          
            /*使用Store Std out 『Sotre物件標準輸出格式』*/
            return ExtDirect.Direct.Helper.Store.OutputJObject(JsonHelper.DataRowSerializerJObject(row));
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
            /*將Exception轉成EXT Exception JSON格式*/
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    [DirectMethod("submitSMTP", DirectAction.Load)]
    public JObject submitSMTP(
        string SMTPServerHost, string SMTPServerPort, string IsSend,
        string FromEmail, string IsSendMail, string IsSendAdminMail,
        string AdministratorEmail, string IsSendDebugMail, string DebugEmail,
        string CredentialsAccount, string CredentialsPassword,Request request)
    {
        try
        {
            /*權限檢查*/
            if (!checkProxy(new StackTrace().GetFrame(0)))
            {
                throw new Exception("Permission Denied!");
            };
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.Load(ConfigFilePath(ConfigType.SMTPConfig));
            doc.GetElementsByTagName("SMTPServerHost")[0].InnerText = SMTPServerHost;
            doc.GetElementsByTagName("SMTPServerPort")[0].InnerText = SMTPServerPort;
            doc.GetElementsByTagName("IsSend")[0].InnerText = IsSend;
            doc.GetElementsByTagName("FromEmail")[0].InnerText = FromEmail;
            doc.GetElementsByTagName("IsSendMail")[0].InnerText = IsSendMail;
            doc.GetElementsByTagName("IsSendAdminMail")[0].InnerText = IsSendAdminMail;
            doc.GetElementsByTagName("AdministratorEmail")[0].InnerText = AdministratorEmail;
            doc.GetElementsByTagName("IsSendDebugMail")[0].InnerText = IsSendDebugMail;
            doc.GetElementsByTagName("DebugEmail")[0].InnerText = DebugEmail;
            doc.GetElementsByTagName("CredentialsAccount")[0].InnerText = CredentialsAccount;
            doc.GetElementsByTagName("CredentialsPassword")[0].InnerText = CredentialsPassword;
            doc.Save(ConfigFilePath(ConfigType.SMTPConfig));
            return ExtDirect.Direct.Helper.Message.Success.OutputJObject();
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
            /*將Exception轉成EXT Exception JSON格式*/
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    //[DirectMethod("loadDirectAuth", DirectAction.Load)]
    //public JObject loadDirectAuth(Request request)
    //{
    //    #region Declare
    //    List<JObject> jobject = new List<JObject>();
    //    BasicModel basicModel = new BasicModel();
    //    ErrorLog table = new ErrorLog();
    //    #endregion
    //    try
    //    { /*權限檢查*/
    //        if (!checkProxy(new StackTrace().GetFrame(0)))
    //        {
    //            throw new Exception("Permission Denied!");
    //        };
    //        DataTable dt = new DataTable();
    //        dt.Columns.Add("AllowCrossPost");
    //        dt.Columns.Add("Rule");
    //        dt.Columns.Add("ProxyPermission");
    //        dt.Columns.Add("NoPermissionAction");         
    //        dt.AcceptChanges();
    //        var row = dt.NewRow();
    //        var cloud = new LK.Config.Cloud.CloudConfigInfo();

    //        row["SMTPSERVERHOST"] = cloud.all;
    //        row["SMTPSERVERPORT"] = cloud.SMTPServerPort;
    //        row["ISSEND"] = cloud.IsSend;
    //        row["FROMEMAIL"] = cloud.FromEmail;           

    //        /*使用Store Std out 『Sotre物件標準輸出格式』*/
    //        return ExtDirect.Direct.Helper.Store.OutputJObject(JsonHelper.DataRowSerializerJObject(row));
    //    }
    //    catch (Exception ex)
    //    {
    //        log.Error(ex); LK.MyException.MyException.Error(this, ex);
    //        /*將Exception轉成EXT Exception JSON格式*/
    //        return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
    //    }
    //}

    [DirectMethod("loadCloud", DirectAction.Load)]
    public JObject loadCloud(Request request)
    {
        #region Declare
        List<JObject> jobject = new List<JObject>();
        BasicModel basicModel = new BasicModel();
        ErrorLog table = new ErrorLog();
        #endregion
        try
        { /*權限檢查*/
            if (!checkProxy(new StackTrace().GetFrame(0)))
            {
                throw new Exception("Permission Denied!");
            };
            DataTable dt = new DataTable();
            dt.Columns.Add("SUPPORTCLOUD");
            dt.Columns.Add("ROLE");
            dt.Columns.Add("ISAUTHCENTER");
            dt.Columns.Add("AUTHMASTER");
            dt.Columns.Add("AUTHCENTERPROTOTYPE");
            dt.Columns.Add("AUTHCENTERHOST");
            dt.Columns.Add("AUTHCENTERIP");
            dt.Columns.Add("AUTHCENTERWEBROOT");
            dt.Columns.Add("SLAVE");
            dt.Columns.Add("TWINS");
            dt.Columns.Add("CLOUDKEYPUB");            
            dt.AcceptChanges();
            var row = dt.NewRow();

            var cloud = LK.Config.Cloud.CloudConfigs.GetConfig();
            row["SUPPORTCLOUD"] = cloud.SupportCloud;
            row["ROLE"] = cloud.Role;
            row["ISAUTHCENTER"] = cloud.IsAuthCenter;
            row["AUTHMASTER"] = cloud.AuthMaster;
            row["AUTHCENTERPROTOTYPE"] = cloud.AuthCenterPrototype;
            row["AUTHCENTERHOST"] = cloud.AuthCenterHost;
            row["AUTHCENTERIP"] = cloud.AuthCenterIP;
            row["AUTHCENTERWEBROOT"] = cloud.AuthCenterWebRoot;
            row["SLAVE"] = cloud.Slave;
            //hmo p JObject.Parse("{"+cloud.Slave+"}")
            row["TWINS"] = cloud.Twins;
            row["CLOUDKEYPUB"] = cloud.CloudKeyPub;
            /*使用Store Std out 『Sotre物件標準輸出格式』*/
            var jObject = new JObject();
            jObject["SUPPORTCLOUD"] = cloud.SupportCloud;
            jObject["ROLE"] = cloud.Role;
            jObject["ISAUTHCENTER"] = cloud.IsAuthCenter;
            jObject["AUTHMASTER"] = cloud.AuthMaster;
            jObject["AUTHCENTERPROTOTYPE"] = cloud.AuthCenterPrototype;
            jObject["AUTHCENTERHOST"] = cloud.AuthCenterHost;
            jObject["AUTHCENTERIP"] = cloud.AuthCenterIP;
            jObject["AUTHCENTERWEBROOT"] = cloud.AuthCenterWebRoot;
            //jObject.Add(JObject.Parse("{" + cloud.Slave + "}"));
            jObject["SLAVE"] = JObject.Parse("{" + cloud.Slave + "}")["Slave"];
            jObject["TWINS"] = JObject.Parse("{" + cloud.Twins + "}")["Twins"];
            jObject["CLOUDKEYPUB"] = cloud.CloudKeyPub;


            //return ExtDirect.Direct.Helper.Store.OutputJObject(JsonHelper.DataRowSerializerJObject(row));
            return ExtDirect.Direct.Helper.Store.OutputJObject(jObject);
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
            /*將Exception轉成EXT Exception JSON格式*/
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    [DirectMethod("submitCloud", DirectAction.Load)]
    public JObject submitCloud(
        string SupportCloud, string Role, string IsAuthCenter,
        string AuthMaster, string AuthCenterPrototype, string AuthCenterHost,
        string AuthCenterIP, string AuthCenterWebRoot, JArray Slave,
        JArray Twins, string CloudKeyPub, Request request)
    {
        try
        {
            /*權限檢查*/
            if (!checkProxy(new StackTrace().GetFrame(0)))
            {
                throw new Exception("Permission Denied!");
            };
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.Load(ConfigFilePath(ConfigType.CloudFilePath));
            doc.GetElementsByTagName("SupportCloud")[0].InnerText = SupportCloud.ToLower();
            doc.GetElementsByTagName("Role")[0].InnerText = Role;
            doc.GetElementsByTagName("IsAuthCenter")[0].InnerText = IsAuthCenter.ToLower();
            doc.GetElementsByTagName("AuthMaster")[0].InnerText = AuthMaster;
            doc.GetElementsByTagName("AuthCenterPrototype")[0].InnerText = AuthCenterPrototype;
            doc.GetElementsByTagName("AuthCenterHost")[0].InnerText = AuthCenterHost;
            doc.GetElementsByTagName("AuthCenterIP")[0].InnerText = AuthCenterIP;
            doc.GetElementsByTagName("AuthCenterWebRoot")[0].InnerText = AuthCenterWebRoot;
            doc.GetElementsByTagName("CloudKeyPub")[0].InnerText = CloudKeyPub;
            //bool hasCData = false;
            //string strSlave = "<![CDATA[Slave:[";
            //foreach (var item in Slave) {
            //    hasCData = true;
            //    strSlave += "{";
            //    strSlave += "IP:\""+item["IP"]+"\",";
            //    strSlave += "ACTIVE:\"" + item["ACTIVE"] + "\"";
            //    strSlave += "},";
            //}
            //strSlave = strSlave.Substring(0, strSlave.Length - 1);
            //strSlave += "]]]>";

            //if (hasCData) {
            //    doc.GetElementsByTagName("Slave")[0].InnerXml=strSlave;
            //}
            
            //hasCData = false;

            //var strTwins = "<![CDATA[Twins:[";
            //foreach (var item in Twins)
            //{
            //    hasCData = true;
            //    strTwins += "{";
            //    strTwins += "IP:\"" + item["IP"] + "\",";
            //    strTwins += "ACTIVE:\"" + item["ACTIVE"] + "\"";
            //    strTwins += "},";
            //}
            //strTwins = strTwins.Substring(0, strTwins.Length - 1);
            //strTwins += "]]]>";

            //if (hasCData)
            //{
            //    doc.GetElementsByTagName("Twins")[0].InnerXml = strTwins;
            //}
            
            doc.Save(ConfigFilePath(ConfigType.CloudFilePath));
            return ExtDirect.Direct.Helper.Message.Success.OutputJObject();
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
            /*將Exception轉成EXT Exception JSON格式*/
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    [DirectMethod("loadDirect", DirectAction.Load)]
    public JObject loadDirect(Request request)
    {
        #region Declare
        List<JObject> jobject = new List<JObject>();
        BasicModel basicModel = new BasicModel();
        ErrorLog table = new ErrorLog();
        #endregion
        try
        { /*權限檢查*/
            if (!checkProxy(new StackTrace().GetFrame(0)))
            {
                throw new Exception("Permission Denied!");
            };
            var direct = LK.Config.DirectAuth.DirectAuthConfigs.GetConfig();            
            var jObject = new JObject();
            jObject["ALLOWCROSSPOST"] = direct.AllowCrossPost;
            jObject["PROXYPERMISSION"] = direct.ProxyPermission;
            jObject["ACCESS_ALL"] = JObject.Parse("{" + direct.Rule + "}")["access_all"];
            jObject["RULE"] = JObject.Parse("{" + direct.Rule + "}")["directUrl"];
            jObject["NOPERMISSIONACTION"] = JObject.Parse("{" + direct.NoPermissionAction + "}")["list"];            
            return ExtDirect.Direct.Helper.Store.OutputJObject(jObject);
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
            /*將Exception轉成EXT Exception JSON格式*/
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }
}