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
       var logoutWin = Ext.create('WS.NoPermissionWindow',{
            param:{
                noPermissionPanel : NOPERMISSIONPANEL
            }
        });
        logoutWin.show();
        $('#divNoPermission').css("height",$(document).height()-130);
        
    });
</script>
<div id='divNoPermission' style="width:90%;"></div>
</asp:Content>
