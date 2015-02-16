<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/mpStand.Master" CodeBehind="GroupHead_Query.aspx.cs" Inherits="Web.admin.authority.authority.GroupHead_Query"  EnableViewState="false"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script language="javascript" type="text/javascript">
    Ext.onReady(function () {
        WS_GROUPQUERYPANEL = Ext.create('WS.GroupQueryPanel', {
            param: {
                companyUuid: "<%= this.getUser().COMPANY_UUID %>"
        }
    });
    WS_GROUPQUERYPANEL.render('divMain');
    UTIL.session.fnKeep();
});
</script>			
<div id="divMain" style="margin-bottom:5px;margin-top:35px;"></div>    
</asp:Content>