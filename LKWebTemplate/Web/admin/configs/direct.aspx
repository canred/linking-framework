<%@ Page Title="" Language="C#" MasterPageFile="~/mpStand.Master" AutoEventWireup="true" CodeBehind="direct.aspx.cs" Inherits="LKWebTemplate.admin.configs.direct" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script language="javascript" type="text/javascript">
    Ext.onReady(function () {
        Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION + ".InitAction"));
        WS_SMTPPANEL = Ext.create('WS.DirectPanel', {
        });
        WS_SMTPPANEL.render('divMain');
        UTIL.runAll();
    });
</script>			
<div id="divMain" style="margin-bottom:5px;margin-top:35px;"></div>
</asp:Content>

