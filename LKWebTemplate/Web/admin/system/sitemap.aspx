
<%@ Page Title="" Language="C#" MasterPageFile="~/mpStand.Master" AutoEventWireup="true" CodeBehind="sitemap.aspx.cs" Inherits="Web.admin.system.sitemap" EnableViewState="false"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style>
.x-action-col-icon {
    height: 16px;
    width: 16px;
    margin-right: 8px;
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script language="javascript" type="text/javascript">
    Ext.onReady(function () {
        WS_SITEMAPQUERYPANEL = Ext.create('WS.SitemapQueryPanel', {
            subWinApppagePickerWindow: 'WS.AppPagePickerWindow'
        });
        WS_SITEMAPQUERYPANEL.render('divMain');
        UTIL.runAll();
    });
</script>
<div id="divMain" style="margin-bottom:5px;margin-top:35px;"></div>    
</asp:Content>