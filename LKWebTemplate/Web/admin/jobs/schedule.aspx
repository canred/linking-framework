<%@ Page Title="" Language="C#" MasterPageFile="~/mpStand.Master" AutoEventWireup="true" CodeBehind="schedule.aspx.cs" Inherits="Web.admin.basic.attendant"  EnableViewState="false"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script language="javascript" type="text/javascript">
Ext.onReady(function () {
    var SCHEDULEPANEL = Ext.create('WS.SchedulePanel',{});
    SCHEDULEPANEL.render('divMain');
    UTIL.runAll();
});
</script>			
<div id="divMain" style="margin-bottom:5px;margin-top:35px;"></div>
</asp:Content>