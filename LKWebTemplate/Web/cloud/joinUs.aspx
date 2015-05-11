<%@ Page Title="" Language="C#" MasterPageFile="~/mpStand.Master" AutoEventWireup="true" CodeBehind="joinUs.aspx.cs" Inherits="LKWebTemplate.cloud.joinUs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div id="divJoinUs"></div>
<script language="javascript" type="text/javascript">

Ext.onReady(function(){	 
    Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION + ".CloudAction"));
    JOINUSPANEL = Ext.create('WS.JoinUsPanel', {});            
    JOINUSPANEL.render('divJoinUs');
    UTIL.runAll();
});
</script>

</asp:Content>
