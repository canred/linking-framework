#region USING
using System;
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
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
#endregion
[DirectService("CloudAction")]
public partial class CloudAction : BaseAction
{
    [DirectMethod("loadActiveConnection", DirectAction.Load)]
    public JObject loadActiveConnection(string activeConnectId, Request request)
    {
        LK.Cloud cloud = new LK.Cloud();
        try
        { /*權限檢查
                if (!checkProxy(new StackTrace().GetFrame(0)))
                {
                    throw new Exception("Permission Denied!");
                };
               */
            LKWebTemplate.Controller.Model.Cloud.CloudModel mod = new LKWebTemplate.Controller.Model.Cloud.CloudModel();
            var drsAc = mod.getActiveConnection_By_Uuid(activeConnectId).AllRecord();
            var jobject = JsonHelper.RecordBaseListJObject(drsAc);
            return ExtDirect.Direct.Helper.Store.OutputJObject(jobject, drsAc.Count);
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
        finally
        {
            cloud = null;
        }
    }

    [DirectMethod("connectTest", DirectAction.Load)]
    public JObject connectTest(string serverweburl, Request request)
    {
        LK.Cloud cloud = new LK.Cloud();
        try
        { /*權限檢查
                if (!checkProxy(new StackTrace().GetFrame(0)))
                {
                    throw new Exception("Permission Denied!");
                };
               */
            var requestUrl = "";
            if (serverweburl.EndsWith("\\"))
            {
                serverweburl = serverweburl.Substring(0, serverweburl.Length - 1);
            }
            requestUrl = serverweburl + "\\router.ashx";
            /*呼叫遠端的伺服器回應(是也就對方的電腦)*/
            var reutrnObj = cloud.CallDirect(requestUrl, "CloudAction.connect", new string[1] { serverweburl }, "");

            return (JObject)reutrnObj;
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
        finally
        {
            cloud = null;
        }
    }
    [DirectMethod("connect", DirectAction.Load)]
    public JObject connect(string serverweburl, Request request)
    {
        try
        { /*權限檢查
                if (!checkProxy(new StackTrace().GetFrame(0)))
                {
                    throw new Exception("Permission Denied!");
                };
               */

            if (LK.Config.Cloud.CloudConfigs.GetConfig().Role.ToUpper() != "MEMBER")
            {
                return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(new Exception("Cloud->Rolue must be member"));
            }
            if (LK.Config.Cloud.CloudConfigs.GetConfig().SupportCloud != true)
            {
                return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(new Exception("Cloud->SupportCloud must be true"));
            } 
            if (LK.Config.DirectAuth.DirectAuthConfigs.GetConfig().AllowCrossPost != true)
            {
                return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(new Exception("DirectAuth->AllowCrossPost must be true"));
            }
            return ExtDirect.Direct.Helper.Message.Success.OutputJObject();
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    [DirectMethod("settingCloud", DirectAction.Load)]
    public JObject settingCloud(string serverweburl, Request request)
    {
        try
        { /*權限檢查
                if (!checkProxy(new StackTrace().GetFrame(0)))
                {
                    throw new Exception("Permission Denied!");
                };*/
            var strSlave = LK.Config.Cloud.CloudConfigs.GetConfig().Slave;
            var jSlave = JObject.Parse("{" + strSlave + "}")["Slave"] as JArray;
            var ip = serverweburl.Split('/')[2];
            var isExist = false;
            foreach (var item in jSlave)
            {
                if (ip == item["IP"].Value<string>())
                {
                    item["ACTIVE"] = "R";
                    isExist = true;
                }
            }
            if (isExist == false)
            {
                JObject newSlave = new JObject(
                    new JProperty("IP", ip),
                    new JProperty("ACTIVE", "R"));
                jSlave.Add(newSlave);
            }
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.Load(InitAction.ConfigFilePath(InitAction.ConfigType.CloudFilePath));
            string saveSlave = "<![CDATA[";
            saveSlave += "Slave:";
            saveSlave += jSlave.ToString();
            saveSlave += "";
            saveSlave += "]]>";
            doc.GetElementsByTagName("Slave")[0].InnerXml = saveSlave;
            doc.Save(InitAction.ConfigFilePath(InitAction.ConfigType.CloudFilePath));
            return ExtDirect.Direct.Helper.Message.Success.OutputJObject();
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    [DirectMethod("settingSlave", DirectAction.Load)]
    public JObject settingSlave(string serverweburl, Request request)
    {
        LK.Cloud cloud = new LK.Cloud();
        try
        { /*權限檢查
                if (!checkProxy(new StackTrace().GetFrame(0)))
                {
                    throw new Exception("Permission Denied!");
                };*/
            var requestUrl = "";
            if (serverweburl.EndsWith("\\"))
            {
                serverweburl = serverweburl.Substring(0, serverweburl.Length - 1);
            }
            requestUrl = serverweburl + "\\router.ashx";
            string authCentetUrl = LK.Config.Cloud.CloudConfigs.GetConfig().AuthCenterPrototype + ":////" + LocalIPAddress() + "/" + LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().AppName + ":" + LK.Config.Cloud.CloudConfigs.GetConfig().AuthCenterPort;
            /*呼叫遠端的伺服器回應(是也就對方的電腦)*/
            var reutrnObj = cloud.CallDirect(requestUrl, "CloudAction.settingSlaveAuthMaster", new string[1] { authCentetUrl }, "");

            return (JObject)reutrnObj;
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
        finally
        {
            cloud = null;
        }
    }

    public string LocalIPAddress()
    {
        return LK.Config.Cloud.CloudConfigs.GetConfig().AuthCenterIP;
    }

    [DirectMethod("settingSlaveAuthMaster", DirectAction.Load)]
    public JObject settingSlaveAuthMaster(string authMaster, Request request)
    {
        try
        { /*權限檢查
                if (!checkProxy(new StackTrace().GetFrame(0)))
                {
                    throw new Exception("Permission Denied!");
                };*/
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.Load(InitAction.ConfigFilePath(InitAction.ConfigType.CloudFilePath));
            doc.GetElementsByTagName("AuthMaster")[0].InnerText = authMaster;
            doc.Save(InitAction.ConfigFilePath(InitAction.ConfigType.CloudFilePath));
            return ExtDirect.Direct.Helper.Message.Success.OutputJObject();
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    [DirectMethod("checkConfig", DirectAction.Load)]
    public JObject checkConfig(Request request)
    {
        try
        { /*權限檢查
                if (!checkProxy(new StackTrace().GetFrame(0)))
                {
                    throw new Exception("Permission Denied!");
                };*/
            return ExtDirect.Direct.Helper.Message.Success.OutputJObject();
        }
        catch (Exception ex)
        {
            log.Error(ex); LK.MyException.MyException.Error(this, ex);
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }
}



