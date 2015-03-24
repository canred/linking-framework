<%@ Page Title="" Language="C#" MasterPageFile="~/mpStand.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="LKWebTemplate.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="Div1" style="float:left;margin-bottom:5px;margin-top:5px;">    
    <div id="systemInfo" style="margin-bottom:5px;margin-top:5px;"></div>
</div>
<script language="javascript" type="text/javascript">
    Ext.onReady(function() {
        NOPERMISSIONPANEL = Ext.create('WS.NoPermissionPanel',{
            width:400,
            logonUrl : '<%= Page.ResolveUrl(LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().LogonPage) %>'
        });
        NOPERMISSIONPANEL.render('noPermission');
        
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
