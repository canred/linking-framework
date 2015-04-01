<%@ Page Title="" Language="C#" MasterPageFile="~/mpStand.Master" AutoEventWireup="true" CodeBehind="dept.aspx.cs" Inherits="Web.admin.basic.dept" EnableViewState="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script language="javascript" type="text/javascript">
    Ext.onReady(function () {
        WS_DEPTQUERYPANEL = Ext.create('WS.DeptQueryPanel', {
            'subWinDept': 'WS.DeptWindow'
        });
        WS_DEPTQUERYPANEL.render('divMain');
        UTIL.runAll();
    });
</script>
<div id="divMain" style="margin-bottom:5px;margin-top:35px;"></div>
</asp:Content>