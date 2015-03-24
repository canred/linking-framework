<%@ Page Title="" Language="C#" MasterPageFile="~/mpEmpty.Master" AutoEventWireup="true" CodeBehind="changeattendant.aspx.cs" Inherits="Web.admin.basic.changeattendant"  EnableViewState="false"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script language="javascript" type="text/javascript">
    Ext.onReady(function () {
        WS_CHANGEATTENDANTWINDOW = Ext.create('WS.ChangeAttendantWindow', {
            param: {
                successDirectToUrl: '<%= Page.ResolveUrl(LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().DefaultPage)%>'
        	}
        });
        WS_CHANGEATTENDANTWINDOW.show();
        UTIL.runAll();
    });
</script>			
<center>
<div id="divMain" style="margin-bottom:5px;margin-top:35px;"></div>
</center>  
</asp:Content>