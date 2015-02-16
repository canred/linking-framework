<%@ Page Title="" Language="C#" MasterPageFile="~/mpStand.Master" AutoEventWireup="true" CodeBehind="company.aspx.cs" Inherits="Web.admin.basic.mainpage" EnableViewState="false"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script language="javascript" type="text/javascript">
    Ext.onReady(function () {
        WS_COMPANYQUERYPANEL = Ext.create('WS.CompanyQueryPanel', {
            'subWinCompany': 'WS.CompanyWindow'
        });
        WS_COMPANYQUERYPANEL.render('divMain');
        UTIL.session.fnKeep();
    });
</script>
<div id="divMain" style="margin-bottom:5px;margin-top:35px;"></div>
</asp:Content>