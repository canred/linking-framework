<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SampleJob2.aspx.cs" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Web.Script.Serialization" %>
<%@ Import Namespace="ExtDirect" %>
<%@ Import Namespace="ExtDirect.Direct" %>
<%@ Import Namespace="IST.DB.SQLCreater" %>
<%@ Import Namespace="Newtonsoft.Json.Converters" %>
<%@ Import Namespace="Newtonsoft.Json.Linq" %>
<%@ Import Namespace="Newtonsoft.Json.Serialization" %>
<%@ Import Namespace="LKWebTemplate.Model.Basic" %>
<%@ Import Namespace="LKWebTemplate.Model.Basic.Table" %>
<%@ Import Namespace="LKWebTemplate.Model.Basic.Table.Record" %>
<%@ Import Namespace="LKWebTemplate" %>
<%@ Import Namespace="System.Text" %>
<%@ Import Namespace="IST.Util" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Diagnostics" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Linq" %>
<%
    var mod = new BasicModel();
    var drsCompany = mod.getCompany();
    foreach (var dr in drsCompany) {
        Response.Write("公司名稱："+dr.NAME_ZH_CN + "<BR>");
    }
    mod = null;
%>