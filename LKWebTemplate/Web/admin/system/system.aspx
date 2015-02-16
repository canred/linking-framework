<%@ Page Title="" Language="C#" MasterPageFile="~/mpStand.Master" AutoEventWireup="true" CodeBehind="system.aspx.cs" Inherits="Web.admin.system.system" EnableViewState="false"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script language="javascript" type="text/javascript">
    var ApplicationQuery = undefined;
    var myForm = undefined;
    Ext.onReady(function () {
        WS_SYSTEMQUERYPANEL = Ext.create('WS.SystemQueryPanel', {
            subWinApplication: 'WS.ApplicationWindow'
        });
        WS_SYSTEMQUERYPANEL.render('divMain');
        UTIL.session.fnKeep();
    });
</script>
<div id="divMain" style="margin-bottom:5px;margin-top:35px;"></div>
</asp:Content>
