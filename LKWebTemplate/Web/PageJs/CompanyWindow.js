/*columns 使用default*/
Ext.define('WS.CompanyWindow', {
    extend: 'Ext.window.Window',
    title: '公司維護',
    closeAction: 'destroy',
    icon: SYSTEM_URL_ROOT + '/css/images/company16x16.png',
    closable: false,
    param: {
        uuid: undefined
    },
    modal: true,
    width: 500,
    height: 400,
    resizable: false,
    draggable: false,
    layout: 'fit',
    initComponent: function() {
        this.items = [Ext.create('Ext.form.Panel', {
            api: {
                load: WS.AdminCompanyAction.info,
                submit: WS.AdminCompanyAction.submit
            },
            itemId: 'CompanyForm',
            paramOrder: ['pUuid'],
            border: true,
            autoHeight: true,
            buttonAlign: 'center',
            items: [{
                    xtype: 'container',
                    layout: 'anchor',
                    margin:'5 0 0 0',
                    defaultType: 'textfield',
                    items: [{
                        xtype: 'textfield',
                        fieldLabel: '公司代碼',
                        itemId: 'ID',
                        name: 'ID',
                        anchor: '0 0',
                        maxLength: 12,
                        allowBlank: false,
                        labelAlign: 'right'
                    }, {
                        fieldLabel: '名稱-繁中',
                        labelWidth: 100,
                        itemId:'C_NAME',
                        name: 'C_NAME',
                        anchor: '100%',
                        maxLength: 84,
                        allowBlank: false,
                        labelAlign: 'right'
                    }, {
                        fieldLabel: '名稱-英文',
                        labelWidth: 100,
                        name: 'E_NAME',
                        anchor: '0 0',
                        maxLength: 340,
                        labelAlign: 'right'
                    }, {
                        fieldLabel: '名稱-簡中',
                        labelWidth: 100,
                        name: 'NAME_ZH_CN',
                        anchor: '100%',
                        maxLength: 84,
                        allowBlank: false,
                        labelAlign: 'right'
                    }, {
                        xtype: 'fieldcontainer',
                        labelAlign: 'right',
                        fieldLabel: '是否啟用',
                        layout: 'hbox',
                        defaults: {
                            margins: '0 10 0 0'
                        },
                        defaultType: 'radiofield',
                        items: [{
                            xtype: 'radiofield',
                            boxLabel: '啟用',
                            inputValue: 'Y',
                            name: 'IS_ACTIVE',
                            checked: true,
                            flex: 2,
                        }, {
                            xtype: 'radiofield',
                            boxLabel: '不啟用',
                            inputValue: 'N',
                            name: 'IS_ACTIVE',
                            flex: 2,
                        }]
                    }]
                },
                Ext.create('Ext.form.Panel', {
                    border: true,
                    padding: 5,
                    bodyStyle: {
                        "background-color": "#F2E77A"
                    },
                    defaultType: 'textfield',
                    title: '*人員帳號同步設定 ',
                    items: [{
                        xtype: 'fieldcontainer',
                        labelAlign: 'right',
                        fieldLabel: '同步AD人員',
                        layout: 'hbox',
                        defaults: {
                            margins: '0 10 0 0'
                        },
                        defaultType: 'radiofield',
                        items: [{
                            boxLabel: '啟用',
                            inputValue: 'Y',
                            name: 'IS_SYNC_AD_USER',
                            checked: true
                        }, {
                            boxLabel: '不啟用',
                            inputValue: 'N',
                            margin: '0 0 0 10',
                            name: 'IS_SYNC_AD_USER'
                        }]
                    }, {
                        xtype: 'container',
                        layout: 'hbox',
                        margin: '0 0 5 0',
                        items: [{
                            xtype: 'textfield',
                            fieldLabel: 'LADP',
                            name: 'AD_LDAP',
                            itemId: 'AD_LDAP',
                            allowBlank: true,
                            readOnly: false,
                            labelAlign: 'right',
                            flex: 1
                        }, {
                            xtype: 'button',
                            text: '測試LDAP',
                            margin: '0 5 0 5',
                            handler: function(handler, scope) {
                                var ldap = this.up("panel").down("#AD_LDAP").getValue(),
                                    account = this.up("panel").down("#AD_LDAP_USER").getValue(),
                                    pwd = this.up("panel").down("#AD_LDAP_USER_PASSWORD").getValue();
                                WS.ADAction.testLDAP(ldap, account, pwd, function(obj, jsonObj) {
                                    if (jsonObj.result.success) {
                                        Ext.MessageBox.show({
                                            title: '測試LDAP',
                                            icon: Ext.MessageBox.INFO,
                                            buttons: Ext.Msg.OK,
                                            msg: 'LDAP Path有效!'
                                        });
                                    } else {
                                        Ext.MessageBox.show({
                                            title: '測試LDAP',
                                            icon: Ext.MessageBox.WARNING,
                                            buttons: Ext.Msg.OK,
                                            msg: jsonObj.result.message
                                        });
                                    };
                                });
                            }
                        }]
                    }, {
                        xtype: 'textfield',
                        fieldLabel: 'LADP使用者',
                        name: 'AD_LDAP_USER',
                        itemId: "AD_LDAP_USER",
                        allowBlank: true,
                        readOnly: false,
                        labelAlign: 'right',
                        flex: 1
                    }, {
                        xtype: 'textfield',
                        fieldLabel: 'LADP密碼',
                        inputType: 'password',
                        name: 'AD_LDAP_USER_PASSWORD',
                        itemId: 'AD_LDAP_USER_PASSWORD',
                        allowBlank: true,
                        readOnly: false,
                        labelAlign: 'right',
                        flex: 1
                    }]
                }), {
                    xtype: 'hiddenfield',
                    fieldLabel: 'UUID',
                    name: 'UUID',
                    anchor: '100%',
                    maxLength: 84,
                    itemId: 'UUID'
                }
            ],
            buttons: [{                
                icon: SYSTEM_URL_ROOT + '/css/custimages/save16x16.png',
                text: '儲存',
                enableKeyEvents:true,    
                itemId:'btnSave',            
                handler: function() {
                    var form = this.up('window').down("#CompanyForm").getForm();
                    if (form.isValid() == false) {
                        return;
                    };
                    this.up('window').down('#ID').setDisabled(false);
                    form.submit({
                        waitMsg: '更新中...',
                        success: function(form, action) {
                            this.param.uuid = action.result.UUID;
                            this.down("#UUID").setValue(action.result.UUID);
                            this.down("#ID").setDisabled(true);
                            Ext.MessageBox.show({
                                title: '操作完成',
                                msg: '操作完成',
                                icon: Ext.MessageBox.INFO,
                                buttons: Ext.Msg.OK
                            });
                        },
                        failure: function(form, action) {
                            Ext.MessageBox.show({
                                title: 'Warning',
                                msg: action.result.message,
                                icon: Ext.MessageBox.ERROR,
                                buttons: Ext.Msg.OK
                            });
                        },
                        scope: this.up('window')
                    });
                }
            }, {
                icon: SYSTEM_URL_ROOT + '/css/custimages/exit16x16.png',
                text: '關閉',
                itemId:'btnClose',
                handler: function() {
                    this.up('window').close();
                }
            }]
        })]
        this.callParent(arguments);
    },
    closeEvent: function() {
        this.fireEvent('closeEvent', this);
    },
    listeners: {
        'show': function() {
            if (this.param.uuid != undefined) {
                this.down('#ID').setDisabled(true);
                this.down("#CompanyForm").getForm().load({
                    params: {
                        'pUuid': this.param.uuid
                    },
                    success: function(response, jsonObj) {},
                    failure: function(response, jsonObj) {
                        if (!jsonObj.result.success) {
                            Ext.MessageBox.show({
                                title: 'Warning',
                                icon: Ext.MessageBox.WARNING,
                                buttons: Ext.Msg.OK,
                                msg: jsonObj.result.message
                            });
                        };
                    }
                });
            } else {
                this.down('#ID').setDisabled(false);
                this.down("#CompanyForm").getForm().reset();
            };
        },
        'close': function() {
            this.closeEvent();
        }
    }
});
