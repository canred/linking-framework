<%@ Page Title="" Language="C#" MasterPageFile="~/mpStand.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="LKWebTemplate.Default" EnableViewState="false"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="Div1" style="float:left;margin-bottom:5px;margin-top:5px;">    
    <div id="systemInfo" style="margin-bottom:5px;margin-top:5px;"></div>
</div>
<script type="text/javascript">
    Ext.onReady(function () {
        WS_LOGONPANEL = Ext.create('WS.LogonPanel', {
            val: {
                company: '<%= getCompany() %>',
            account: '<%= getAccount() %>',
            password: '<%= getPassword() %>'
        }
    });
        
    WS_LOGONPANEL.urlSuccess = '<%= Page.ResolveUrl(LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().DefaultPage)%>';
    WS_LOGONPANEL.urlFail = '<%= Page.ResolveUrl(LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().NoPermissionPage)%>';
    WS_LOGONPANEL.down('#ExtLogonForm').title = '<img src="' + SYSTEM_ROOT_PATH + '/css/custimages/login.gif" style="height:16px;margin-bottom:4px;margin-right:10px;" align="middle"><%= LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().SystemName%>';
    var myWin = Ext.create('WS.LogonWindow',{
        param:{
            logonPanel:WS_LOGONPANEL
        }
    });

    myWin.show();        
    $('#divDefault').css("height",$(document).height()-130);
});
</script>
<!--Echart Demo--> 

<div id='divDefault' style="width:90%;"></div>   

</asp:Content>
