<%@ Page Title="" Language="C#" MasterPageFile="~/mpStand.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="LKWebTemplate.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript" src='<%= Page.ResolveUrl("~/js/shared/include-ext.js")%>'></script>
<script type="text/javascript" src='<%= Page.ResolveUrl("~/Proxy.ashx")%>'></script>
<script type="text/javascript" src='<%= Page.ResolveUrl("~/pagejs/Ext.NoPermission.js")%>'></script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="Div1" style="float:left;margin-bottom:5px;margin-top:5px;">
    
    <div id="systemInfo" style="margin-bottom:5px;margin-top:5px;"></div>
</div>

<script language="javascript" type="text/javascript">

    Ext.onReady(function() {
        ExtNoPermission.width = 400;
        ExtNoPermission.logonUrl = '<%= Page.ResolveUrl(LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().LogonPage) %>';
        ExtNoPermission.render('noPermission');
        ExtNoPermission.show();
    });

</script>
<table>
    <tr>
        <td width="30%"></td>
        <td width="40%" align="center">
        <div id="noPermission" style="margin-bottom:5px;margin-top:5px;"></div>
        </td>
        <td width="30%"></td>
    </tr>
</table>

</center>
                           

</asp:Content>
