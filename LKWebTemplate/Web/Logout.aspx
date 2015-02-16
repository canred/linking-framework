<%@ Page Title="" Language="C#" MasterPageFile="~/mpStand.Master" AutoEventWireup="true" CodeBehind="Logout.aspx.cs" Inherits="LKWebTemplate.Logout" EnableViewState="false"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="Div1" style="float:left;margin-bottom:5px;margin-top:5px;">    
    <div id="systemInfo" style="margin-bottom:5px;margin-top:5px;"></div>
</div>
<script type="text/javascript">
    Ext.onReady(function () {
        var logoutPanel = Ext.create('WS.LogoutPanel', {
            val: {
                logonUrl: '<%= Page.ResolveUrl(LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().LogonPage) %>'
        }
    });
    logoutPanel.render('logon');
});
</script>
<table width="100%">
    <tr>
        <td width="30%"></td>
        <td width="40%" >
			<div id="logon" style="margin-bottom:5px;margin-top:5px;"></div>
        </td>
        <td width="30%"></td>
    </tr>
</table>
</asp:Content>