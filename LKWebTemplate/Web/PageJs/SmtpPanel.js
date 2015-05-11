Ext.define('WS.SmtpPanel', {
    extend: 'Ext.form.Panel',
    closeAction: 'destroy',
    title: 'Smtp 設定',
    icon:SYSTEM_URL_ROOT+'/css/images/mail16x16.png',
    border: true,
    frame: true,
    autoWidth: true,
    //minHeight: $(document).height() - 140,
    autoHeight: true,
    autoScroll: true,
    defaults: {
        labelWidth: 150
    },
    layout: {
        type: 'anchor'
    },
    initComponent: function() {
        this.items = [{
                xtype: 'textfield',
                fieldLabel: 'SMTP Host',
                allowBlank: false,
                padding: '5 0 0 0',
                itemId: 'SMTPSERVERHOST',
                anchor: '-5 0'
            }, {
                xtype: 'numberfield',
                itemId: 'SMTPSERVERPORT',
                fieldLabel: 'SMTP Port',
                allowBlank: false,
                minValue: 0,
                anchor: '-5 0'
            }, {
                xtype: 'textfield',
                itemId: 'CREDENTIALSACCOUNT',
                fieldLabel: 'SMTP認證Account',
                allowBlank: true,
                anchor: '-5 0'
            }, {
                xtype: 'textfield',
                itemId: 'CREDENTIALSPASSWORD',
                fieldLabel: 'SMTP認證password',
                allowBlank: true,
                anchor: '-5 0'
            }, {
                xtype: 'combo',
                itemId: 'ISSEND',
                fieldLabel: '啟用寄信功能',
                queryMode: 'local',
                displayField: 'text',
                valueField: 'value',
                anchor: '-5 0',
                editable: false,
                hidden: false,
                value: 'Y',
                store: new Ext.data.ArrayStore({
                    fields: ['text', 'value'],
                    data: [
                        ['是', 'Y'],
                        ['否', 'N']
                    ]
                }),
                listeners: {
                    'select': function(combo, records, eOpts) {

                    }
                }
            }, {
                xtype: 'textfield',
                itemId: 'FROMEMAIL',
                fieldLabel: '寄件人',
                allowBlank: false,
                anchor: '-5 0'
            }

            , {
                xtype: 'combo',
                itemId: 'ISSENDMAIL',
                fieldLabel: '正式寄信',
                queryMode: 'local',
                displayField: 'text',
                valueField: 'value',
                anchor: '-5 0',
                editable: false,
                hidden: false,
                value: 'Y',
                store: new Ext.data.ArrayStore({
                    fields: ['text', 'value'],
                    data: [
                        ['是', 'Y'],
                        ['否', 'N']
                    ]
                }),
                listeners: {
                    'select': function(combo, records, eOpts) {

                    }
                }
            }, {
                xtype: 'combo',
                itemId: 'ISSENDADMINMAIL',
                fieldLabel: '寄信給管理者',
                queryMode: 'local',
                value: 'Y',
                displayField: 'text',
                valueField: 'value',
                anchor: '-5 0',
                editable: false,
                hidden: false,
                store: new Ext.data.ArrayStore({
                    fields: ['text', 'value'],
                    data: [
                        ['是', 'Y'],
                        ['否', 'N']
                    ]
                }),
                listeners: {
                    'select': function(combo, records, eOpts) {

                    }
                }
            }, {
                xtype: 'container',
                layout: 'hbox',
                margin: '0 0 5 0',
                defaults: {
                    labelWidth: 150
                },
                items: [{
                    xtype: 'textfield',
                    itemId: 'ADMINISTRATOREMAIL',
                    fieldLabel: '管理者清單',
                    allowBlank: false,
                    height: 60,
                    flex: 1
                }, {
                    xtype: 'label',
                    html: '多人時以「;」分隔',
                    width:140
                }]
            }, {
                xtype: 'combo',
                itemId: 'ISSENDDEBUGMAIL',
                fieldLabel: '寄信給開發人員',
                queryMode: 'local',
                value: 'Y',
                displayField: 'text',
                valueField: 'value',
                anchor: '-5 0',
                editable: false,
                hidden: false,
                store: new Ext.data.ArrayStore({
                    fields: ['text', 'value'],
                    data: [
                        ['是', 'Y'],
                        ['否', 'N']
                    ]
                }),
                listeners: {
                    'select': function(combo, records, eOpts) {

                    }
                }
            }, {
                xtype: 'container',
                layout: 'hbox',
                margin: '0 0 5 0',
                defaults: {
                    labelWidth: 150
                },
                items: [{
                    xtype: 'textfield',
                    itemId: 'DEBUGEMAIL',
                    fieldLabel: '開發人員清單',
                    allowBlank: false,
                    height: 60,
                    flex: 1
                }, {
                    xtype: 'label',
                    html: '多人時以「;」分隔',
                    width:140
                }]
            }
        ];
        this.buttonAlign = 'center';
        this.buttons = [{
            xtype: 'button',
            text: '儲存',
            icon: SYSTEM_URL_ROOT + '/css/custimages/save16x16.png',
            handler: function(handler, scope) {
                if (this.up('panel').getForm().isValid() == false) {
                    return;
                };

                var mainPanel = this.up('panel'),
                    SMTPServerHost, SMTPServerPort, IsSend, FromEmail, IsSendMail, IsSendAdminMail, AdministratorEmail, IsSendDebugMail, DebugEmail, CredentialsAccount, CredentialsPassword;

                SMTPServerHost = mainPanel.down('#SMTPSERVERHOST').getValue(), SMTPServerPort = mainPanel.down('#SMTPSERVERPORT').getValue(), IsSend = mainPanel.down('#ISSEND').getValue(), FromEmail = mainPanel.down('#FROMEMAIL').getValue(), IsSendMail = mainPanel.down('#ISSENDMAIL').getValue(), IsSendAdminMail = mainPanel.down('#ISSENDADMINMAIL').getValue(), AdministratorEmail = mainPanel.down('#ADMINISTRATOREMAIL').getValue(), IsSendDebugMail = mainPanel.down('#ISSENDDEBUGMAIL').getValue(), DebugEmail = mainPanel.down('#DEBUGEMAIL').getValue(), CredentialsAccount = mainPanel.down('#CREDENTIALSACCOUNT').getValue(), CredentialsPassword = mainPanel.down('#CREDENTIALSPASSWORD').getValue();

                WS.InitAction.submitSMTP(SMTPServerHost, SMTPServerPort, IsSend, FromEmail, IsSendMail, IsSendAdminMail, AdministratorEmail, IsSendDebugMail, DebugEmail, CredentialsAccount, CredentialsPassword, function(obj, jsonObj) {
                    if (jsonObj.result.success) {
                        Ext.MessageBox.show({
                            title: '系統提示',
                            icon: Ext.MessageBox.INFO,
                            buttons: Ext.Msg.OK,
                            msg: '儲存完成!'
                        });
                    }
                });
            }
        }];
        this.callParent(arguments);
    },
    listeners: {
        afterrender: function(self, eOpts) {
            WS.InitAction.loadSmtp(function(obj, jsonObj) {
                if (jsonObj.result.success) {
                    var data = jsonObj.result.data[0];
                    console.log(data);
                    this.down("#SMTPSERVERHOST").setValue(data.SMTPSERVERHOST);
                    this.down("#SMTPSERVERPORT").setValue(data.SMTPSERVERPORT);
                    this.down("#ISSEND").setValue(data.ISSEND);
                    this.down("#FROMEMAIL").setValue(data.FROMEMAIL);
                    this.down("#ISSENDMAIL").setValue(data.ISSENDMAIL);
                    this.down("#ISSENDADMINMAIL").setValue(data.ISSENDADMINMAIL);
                    this.down("#ADMINISTRATOREMAIL").setValue(data.ADMINISTRATOREMAIL);
                    this.down("#ISSENDDEBUGMAIL").setValue(data.ISSENDDEBUGMAIL);
                    this.down("#DEBUGEMAIL").setValue(data.DEBUGEMAIL);
                    this.down("#CREDENTIALSACCOUNT").setValue(data.CREDENTIALSACCOUNT);
                    this.down("#CREDENTIALSPASSWORD").setValue(data.CREDENTIALSPASSWORD);
                }
            }, this);

        }
    }
});
