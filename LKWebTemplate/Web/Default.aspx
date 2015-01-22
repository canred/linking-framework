<%@ Page Title="" Language="C#" MasterPageFile="~/mpStand.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="LKWebTemplate.Default" EnableViewState="false"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8"/>
<script type="text/javascript" src='<%= Page.ResolveUrl("~/js/shared/include-ext.js")%>'></script>
<script type="text/javascript" src='<%= Page.ResolveUrl("~/Proxy.ashx")%>'></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="Div1" style="float:left;margin-bottom:5px;margin-top:5px;">    
    <div id="systemInfo" style="margin-bottom:5px;margin-top:5px;"></div>
</div>
<script type="text/javascript">
Ext.Loader.setPath('Ext.ux', SYSTEM_ROOT_PATH + '/js/ux');
Ext.require(['*', 'Ext.ux.DataTip']);
var ExtLogon;
Ext.onReady(function () {
    Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION + ".UserAction"));
    Ext.QuickTips.init();
    var bd = Ext.getBody();
    var required = '<span style="color:red;font-weight:bold" data-qtip="Required">*</span>';
    ExtLogon = Ext.widget({
        api: {
            submit: WS.UserAction.logon
        },
        xtype: 'form',
        layout: 'form',
        id: 'ExtLogonForm',
        url: '<%= Page.ResolveUrl(LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().NoPermissionPage) %>',
        urlSuccess: '',
        urlFail: '',
        frame: true,
        title: '<img src="' + SYSTEM_ROOT_PATH + '/css/custimages/login.gif" style="height:16px;margin-right:10px;" align="middle" >登入',
        width: 200,
        fieldDefaults: {
            msgTarget: 'side',
            labelWidth: 75
        },
        plugins: {
            ptype: 'datatip'
        },
        items: [{
            xtype: 'textfield',
            fieldLabel: '公司',
            labelAlign: 'right',
            afterLabelTextTpl: required,
            name: 'company',
            allowBlank: false,
            tooltip: '*公司名稱',
            value: '<%= getCompany() %>',
            id: 'txt_company',
            enableKeyEvents: true,
            listeners: {
                keyup: function (e, t, eOpts) {
                    if (t.button == 12) {
                        Ext.getCmp('bntLogin').handler();
                    }
                }
            }
        }, {
            xtype: 'textfield',
            fieldLabel: '帳號',
            labelAlign: 'right',
            afterLabelTextTpl: required,
            name: 'account',
            allowBlank: false,
            tooltip: '*帳號',
            value: '<%= getAccount() %>',
            id: 'txt_account',
            enableKeyEvents: true,
            listeners: {
                keyup: function (e, t, eOpts) {
                    if (t.button == 12) {
                        Ext.getCmp('bntLogin').handler();
                    }
                }
            }
        }, {
            xtype: 'textfield',
            inputType: 'password',
            fieldLabel: '密碼',
            labelAlign: 'right',
            afterLabelTextTpl: required,
            name: 'password',
            allowBlank: false,
            tooltip: '*密碼',
            value: '<%= getPassword() %>',
            id: 'txt_pw',
            enableKeyEvents: true,
            listeners: {
                keyup: function (e, t, eOpts) {
                    if (t.button == 12) {
                        Ext.getCmp('bntLogin').handler();
                    }
                }
            }
        }, {
            xtype: 'container',
            id: 'Form.Validate',
            layout: {
                type: 'hbox',
                pack: 'end'
            },
            hidden:<%= getGraphicsCertification() %>,
            height: 35,
            width: '100%',
            defaultType: 'textfield',
            items: [{
                fieldLabel: '',
                id: 'validateText',
                labelWidth: 0,

                name: '',
                padding: 5,
                anchor: '100%',
                maxLength: 5,
                emptyText: '請輸入驗証碼',
                allowBlank: true //false
            }, {
                xtype: 'image',
                height: 35,
                width: 100,
                src: './ValidateCode/ValidateCode.ashx',
                padding: 5,
                anchor: '100%',
                id: 'img'
            }]
        }],
        buttons: [
            {
                text: '<img src="<%= Page.ResolveUrl("~/css/images/question.png")%>" style="width:14px;height:14px;vertical-align:middle;margin-right:5px;">忘記密碼',
                handler: function () {
                    var company = Ext.getCmp('txt_company');
                    var account = Ext.getCmp('txt_account');
                    if (company.getValue() == "" || account.getValue() == "") {
                        Ext.Msg.alert('密碼取回', '必須提供你的公司、帳號資料!');
                    } else {
                        /*:::變更Submit的方向:::*/
                        ExtLogon.getForm().api.submit = WS.UserAction.forgetPassword;
                        Ext.getCmp('txt_pw').allowBlank = true;
                        ExtLogon.getForm().submit({
                            success: function (f, a) {
                                ExtLogon.getForm().api.submit = WS.UserAction.logon;

                                if (a.result.status == 'OK') {
                                    Ext.Msg.alert('密碼取回', '您的密碼已寄至：' + a.result.email);
                                } else {
                                    Ext.Msg.alert('密碼取回', '無此帳號資訊!')
                                }
                            },
                            failure: function (f, a) {
                                ExtLogon.getForm().api.submit = WS.UserAction.logon;
                            }
                        });
                    }
                }
            }, {
                text: '<img src="<%= Page.ResolveUrl("~/css/images/login.png")%>" style="width:14px;height:14px;vertical-align:middle;margin-right:5px;">登入',
                id: 'bntLogin',
                handler: function () {
                    Ext.getBody().mask();
                    Ext.getCmp('txt_pw').allowBlank = false;
                    this.up('form').getForm().isValid();
                    var urlSuccess = this.up('form').urlSuccess;
                    var urlFail = this.up('form').urlFail;
                    var validateText = Ext.getCmp('validateText').getValue();
                    WS.UserAction.ValidateCode(validateText, function (data) {
                        try {
                            if (data.validation == 'ok') {
                                ExtLogon.getForm().submit({
                                    success: function (f, a) {
                                        Ext.getBody().unmask();
                                        if (a.result.validation == 'OK') {
                                            location.href = urlSuccess;
                                        } else {
                                            if (urlFail == '') {
                                                Ext.Msg.alert('Logon Failure', '請檢查您的帳號密碼是否正確。');
                                            } else {
                                                location.href = urlFail;
                                            }
                                        }
                                    },
                                    failure: function (f, a) {
                                        Ext.getBody().unmask();
                                        if (a.failureType === Ext.form.Action.CONNECT_FAILURE) {
                                            Ext.Msg.alert('Logon Failure', '請檢查您的帳號密碼是否正確。');
                                        }
                                        if (a.failureType === Ext.form.Action.SERVER_INVALID) {
                                            Ext.Msg.alert('Warning', a.result.errormsg);
                                        }
                                        if (!a.result.success) {
                                            Ext.MessageBox.show({
                                                title: 'Warning',
                                                icon: Ext.MessageBox.WARNING,
                                                buttons: Ext.Msg.OK,
                                                msg: a.result.message
                                            });
                                        }
                                    }
                                });
                            } else {
                                Ext.MessageBox.show({
                                    title: '驗証失敗',
                                    msg: '請重新輸入',
                                    icon: Ext.MessageBox.INFO,
                                    buttons: Ext.Msg.OK
                                });
                                Ext.getBody().unmask();                            }
                        } catch (ex) {
                            Ext.MessageBox.show({
                                title: '發生異常錯誤',
                                msg: '請通知系統管理人員協助處理!',
                                icon: Ext.MessageBox.INFO,
                                buttons: Ext.Msg.OK
                            });
                            Ext.getBody().unmask();
                        }
                    });
                }
            }
        ]
    });
    ExtLogon.width = "100%";
    ExtLogon.urlSuccess = '<%= Page.ResolveUrl(LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().DefaultPage)%>';
    ExtLogon.urlFail = '<%= Page.ResolveUrl(LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().NoPermissionPage)%>';
    ExtLogon.title = '<img src="' + SYSTEM_ROOT_PATH + '/css/custimages/login.gif" style="height:16px;margin-bottom:4px;margin-right:10px;" align="middle"><%= LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().SystemName%>';
    ExtLogon.render('logon');
});
</script>
<table width="100%">
    <tr>
        <td width="30%"></td>
        <td width="40%" >
        <div id="logon" style="margin-bottom:5px;margin-top:5px;width:2"></div>
        </td>
        <td width="30%"></td>
    </tr>
</table>                       
</asp:Content>
