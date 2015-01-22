<%@ Page Title="" Language="C#" MasterPageFile="~/mpStand.Master" AutoEventWireup="true" CodeBehind="Logout.aspx.cs" Inherits="LKWebTemplate.Logout" EnableViewState="false"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript" src='<%= Page.ResolveUrl("~/js/shared/include-ext.js")%>'></script>
<script type="text/javascript" src='<%= Page.ResolveUrl("~/Proxy.ashx")%>'></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="Div1" style="float:left;margin-bottom:5px;margin-top:5px;">    
    <div id="systemInfo" style="margin-bottom:5px;margin-top:5px;"></div>
</div>
<script type="text/javascript">
Ext.onReady(function () {
    var bd = Ext.getBody();
    ExtLogout = Ext.widget({
        xtype: 'form',
        layout: 'form',
        collapsible: false,
        id: 'ExtLogout',
        frame: true,
        autoWidth: true,
        title: '<img src="./css/custimages/contact2.gif" style="height:16px;margin-bottom:8px;margin-right:10px;" align="middle">系統登出',
        bodyPadding: '5 5 5 5',
        fieldDefaults: {
            msgTarget: 'side',
            labelWidth: 75
        },
        defaultType: 'label',
        items: [{
            html: '您已完成登出系統。'
        }, {
            html: '感謝您的使用。'
        }, {
            html: '如果您對系統有任何建議，請透過Email與我們連聯(canred_chen@istgroup.com)'
        }, {
            html: ''
        }, {
            xtype: 'container',
            layout: 'hbox',
            items: [{
                xtype: 'label',
                flex: 1
            }, {
                xtype: 'button',
                text: '登入',
                flex: 1,
                handler: function () {
                    location.href = '<%= Page.ResolveUrl(LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().LogonPage) %>';
                }
            }, {
                xtype: 'label',
                flex: 1
            }]
        }]
    });
    ExtLogout.render('logon');
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
