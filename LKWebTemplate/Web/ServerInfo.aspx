<%@ Page Title="" Language="C#" MasterPageFile="~/mpStand.Master" AutoEventWireup="true" CodeBehind="ServerInfo.aspx.cs" Inherits="LKWebTemplate.ServerInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style>
.diskTip{
   font-style:'30px'
}
legend {  
  margin-bottom: 0px;
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div id="divRegist"></div>
<script language="javascript" type="text/javascript">
Ext.onReady(function() {
    SERVERINFOPANEL = Ext.create('WS.ServerInfoPanel',{
        title: '服務器資訊',
        width: $("#divRegist").width() - 16,
        autoHeight: true,
        border: true,
        renderTo: 'divRegist'
    });
});
</script>
</asp:Content>
