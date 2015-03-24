/*全局變量*/
var WS_LOGONPANEL;
/*WS.LogonPanel物件類別*/
/*columns 使用default*/
Ext.define('WS.LogonPanel', {
    extend: 'Ext.panel.Panel',
    closeAction: 'destroy',
    resizable: false,
    draggable: false,
    /*語言擴展*/
    lan: {},
    /*參數擴展*/
    param: {
        url: '',
        hideGraphicsCertification: true
    },
    /*值擴展*/
    val: {
        company: '公司名稱',
        account: '帳號',
        password: '密碼'
    },
    initComponent: function() {
        var me = this;
        me.items = [{
            api: {
                submit: WS.UserAction.logon
            },
            xtype: 'form',
            layout: 'form',
            itemId: 'ExtLogonForm',
            urlSuccess: '',
            urlFail: '',
            frame: true,
            title: '<img src="' + SYSTEM_ROOT_PATH + '/css/custimages/login.gif" style="height:16px;margin-right:10px;" align="middle" >登入',
            width: '100%',
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
                afterLabelTextTpl: '<span style="color:red;font-weight:bold" data-qtip="Required">*</span>',
                name: 'company',
                allowBlank: false,
                tooltip: '*公司名稱',
                value: this.val.company,
                itemId: 'txt_company',
                blankText: '*請輸入您的公司代碼',
                enableKeyEvents: true,
                listeners: {
                    keyup: function(e, t, eOpts) {
                        var keyCode = t.keyCode;
                        if (keyCode == Ext.event.Event.ENTER) {
                            WS_LOGONPANEL.down("bntLogin").handler();
                        };
                    }
                }
            }, {
                xtype: 'textfield',
                fieldLabel: '帳號',
                labelAlign: 'right',
                afterLabelTextTpl: '<span style="color:red;font-weight:bold" data-qtip="Required">*</span>',
                name: 'account',
                allowBlank: false,
                tooltip: '*帳號',
                blankText: '*請輸入您的帳號',
                value: this.val.account,
                itemId: 'txt_account',
                enableKeyEvents: true,
                listeners: {
                    keyup: function(e, t, eOpts) {
                        var keyCode = t.keyCode;
                        if (keyCode == Ext.event.Event.ENTER) {
                            WS_LOGONPANEL.down("bntLogin").handler();
                        };
                    }
                }
            }, {
                xtype: 'textfield',
                inputType: 'password',
                fieldLabel: '密碼',
                labelAlign: 'right',
                afterLabelTextTpl: '<span style="color:red;font-weight:bold" data-qtip="Required">*</span>',
                name: 'password',
                allowBlank: false,
                tooltip: '*密碼',
                blankText: '*請輸入您的登入密碼',
                value: this.val.password,
                itemId: 'txt_pw',
                enableKeyEvents: true,
                listeners: {
                    keyup: function(e, t, eOpts) {
                        var keyCode = t.keyCode;
                        if (keyCode == Ext.event.Event.ENTER) {
                            WS_LOGONPANEL.down("bntLogin").handler();
                        };
                    }
                }
            }, {
                xtype: 'container',
                layout: {
                    type: 'hbox',
                    pack: 'end'
                },
                hidden: this.param.hideGraphicsCertification,
                height: 35,
                width: '100%',
                defaultType: 'textfield',
                items: [{
                    fieldLabel: '',
                    itemId: 'validateText',
                    labelWidth: 0,
                    name: '',
                    padding: 5,
                    anchor: '100%',
                    maxLength: 5,
                    emptyText: '請輸入驗証碼',
                    allowBlank: true
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
            buttons: [{
                text: '忘記密碼',
                handler: function() {
                    var company = WS_LOGONPANEL.down('#txt_company'),
                        account = WS_LOGONPANEL.down('#txt_account');
                    if (company.getValue() == "" || account.getValue() == "") {
                        Ext.Msg.alert('密碼取回', '必須提供你的公司、帳號資料!');
                    } else {
                        /*:::變更Submit的方向:::*/
                        WS_LOGONPANEL.down('#ExtLogonForm').getForm().api.submit = WS.UserAction.forgetPassword;
                        WS_LOGONPANEL.down("#txt_pw").allowBlank = true;
                        WS_LOGONPANEL.down('#ExtLogonForm').getForm().submit({
                            success: function(obj, res) {
                                WS_LOGONPANEL.down('#ExtLogonForm').getForm().api.submit = WS.UserAction.logon;
                                if (res.result.status == 'OK') {
                                    Ext.Msg.alert('密碼取回', '您的密碼已寄至：' + res.result.email);
                                } else {
                                    Ext.Msg.alert('密碼取回', '無此帳號資訊!')
                                };
                            },
                            failure: function(obj, res) {
                                WS_LOGONPANEL.down('#ExtLogonForm').getForm().api.submit = WS.UserAction.logon;
                            }
                        });
                    };
                }
            }, {
                text: '登入',
                itemId: 'bntLogin',
                handler: function() {
                    Ext.getBody().mask();
                    WS_LOGONPANEL.down("#txt_pw").allowBlank = false;
                    this.up('form').getForm().isValid();
                    var urlSuccess = this.up('panel').up('panel').urlSuccess,
                        urlFail = this.up('panel').up('panel').urlFail,
                        _validateText = WS_LOGONPANEL.down("#validateText").getValue();

                    WS.UserAction.ValidateCode(_validateText, function(data) {
                        try {
                            if (data.validation == 'ok') {
                                WS_LOGONPANEL.down('#ExtLogonForm').getForm().submit({
                                    success: function(obj, res) {
                                        Ext.getBody().unmask();
                                        if (res.result.validation == 'OK') {
                                            location.href = urlSuccess;
                                        } else {
                                            if (urlFail == '') {
                                                Ext.Msg.alert('Logon Failure', '請檢查您的帳號密碼是否正確。');
                                            } else {
                                                location.href = urlFail;
                                            };
                                        };
                                    },
                                    failure: function(obj, res) {
                                        Ext.getBody().unmask();
                                        if (res.failureType === Ext.form.Action.CONNECT_FAILURE) {
                                            Ext.Msg.alert('Logon Failure', '請檢查您的帳號密碼是否正確。');
                                        };
                                        if (res.failureType === Ext.form.Action.SERVER_INVALID) {
                                            Ext.Msg.alert('Warning', res.result.errormsg);
                                        };
                                        if (!res.result.success) {
                                            Ext.MessageBox.show({
                                                title: 'Warning',
                                                icon: Ext.MessageBox.WARNING,
                                                buttons: Ext.Msg.OK,
                                                msg: res.result.message
                                            });
                                        };
                                    }
                                });
                            } else {
                                Ext.MessageBox.show({
                                    title: '驗証失敗',
                                    msg: '請重新輸入',
                                    icon: Ext.MessageBox.INFO,
                                    buttons: Ext.Msg.OK
                                });
                                Ext.getBody().unmask();
                            };
                        } catch (ex) {
                            Ext.MessageBox.show({
                                title: '發生異常錯誤',
                                msg: '請通知系統管理人員協助處理-1501271046!',
                                icon: Ext.MessageBox.INFO,
                                buttons: Ext.Msg.OK
                            });
                            Ext.getBody().unmask();
                        }
                    });
                }
            }]
        }];
        me.callParent(arguments);
    }
});
