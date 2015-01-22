Ext.Loader.setConfig({
    enabled: true
});

Ext.Loader.setPath('Ext.ux', SYSTEM_ROOT_PATH + '/js/ux');
Ext.require(['*', 'Ext.ux.DataTip']);
Ext.MessageBox.buttonText.yes = "確定";
Ext.MessageBox.buttonText.no = "取消";
Ext.onReady(function() {
    Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION + ".ProxyAction"));
    Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION + ".ApplicationAction"));
    Ext.define('ProxyForm', {
        extend: 'Ext.window.Window',
        title: '資源 新增/修改',
        closeAction: 'hide',
        uuid: undefined,
        id: 'ExtProxyForm',
        width: 550,
        height: 400,
        layout: 'fit',
        resizable: false,
        draggable: true,
        initComponent: function() {
            var me = this;
            me.items = [Ext.create('Ext.form.Panel', {
                layout: {
                    type: 'form',
                    align: 'stretch'
                },
                api: {
                    load: WS.ProxyAction.infoProxy,
                    submit: WS.ProxyAction.submitProxy,
                    destroy: WS.ProxyAction.destroyProxyByUuid
                },
                id: 'Ext.ProxyForm.Form',
                paramOrder: ['pUuid'],
                border: true,
                bodyPadding: 5,
                defaultType: 'textfield',
                buttonAlign: 'center',
                items: [{
                    labelWidth: 100,
                    fieldLabel: '系統',
                    labelAlign: 'right',
                    xtype: 'combo',
                    name: 'APPLICATION_HEAD_UUID',
                    id: 'Ext_ProxyForm_Form_application_head_uuid',
                    displayField: 'NAME',
                    valueField: 'UUID',
                    editable: false,
                    readOnly: true,
                    store: Ext.create('Ext.data.Store', {
                        successProperty: 'success',
                        autoLoad: true,
                        model: Ext.define('APPLICATION', {
                            extend: 'Ext.data.Model',
                            fields: [
                                'CREATE_DATE',
                                'UPDATE_DATE',
                                'IS_ACTIVE',
                                'NAME',
                                'DESCRIPTION',
                                'ID',
                                'CREATE_USER',
                                'UPDATE_USER',
                                'WEB_SITE',
                                'UUID'
                            ]
                        }),
                        pageSize: 10,
                        proxy: {
                            type: 'direct',
                            api: {
                                read: WS.ApplicationAction.loadApplication
                            },
                            reader: {
                                root: 'data'
                            },
                            paramsAsHash: true,
                            paramOrder: ['pKeyword', 'page', 'limit', 'sort', 'dir'],
                            extraParams: {
                                pKeyword: ''
                            },
                            simpleSortMode: true,
                            listeners: {
                                exception: function(proxy, response, operation) {
                                    Ext.MessageBox.show({
                                        title: 'REMOTE EXCEPTION',
                                        msg: operation.getError(),
                                        icon: Ext.MessageBox.ERROR,
                                        buttons: Ext.Msg.OK
                                    });
                                }
                            }
                        },
                        listeners: {
                            load: function(obj) {
                                if (obj.getCount() > 0) {
                                    Ext.getCmp('Ext_ProxyForm_Form_application_head_uuid').setValue(obj.getAt(0).data.UUID);
                                }
                            }
                        },
                        remoteSort: true,
                        sorters: [{
                            property: 'NAME'
                        }]
                    })
                }, {
                    fieldLabel: 'Action',
                    labelAlign: 'right',
                    labelWidth: 100,
                    name: 'PROXY_ACTION',
                    padding: 5,
                    anchor: '100%',
                    maxLength: 84,
                    allowBlank: false
                }, {
                    fieldLabel: 'Method',
                    labelAlign: 'right',
                    labelWidth: 100,
                    name: 'PROXY_METHOD',
                    padding: 5,
                    anchor: '100%',
                    maxLength: 84,
                    allowBlank: false
                }, {
                    fieldLabel: '描述',
                    labelAlign: 'right',
                    labelWidth: 100,
                    name: 'DESCRIPTION',
                    padding: 5,
                    anchor: '100%',
                    maxLength: 84,
                    allowBlank: false
                }, {
                    xtype: 'combo',
                    fieldLabel: 'Type',
                    //id: 'id',
                    labelAlign: 'right',
                    allowBlank: false,
                    queryMode: 'local',
                    itemId: 'PROXY_TYPE',
                    displayField: 'text',
                    valueField: 'value',
                    name: 'PROXY_TYPE',
                    padding: 5,
                    editable: false,
                    hidden: false,
                    store: new Ext.data.ArrayStore({
                        fields: ['text', 'value'],
                        data: [
                            ['Store', 'Store'],
                            ['Form', 'Form'],
                            ['Load', 'Load'],
                            ['TreeStore', 'TreeStore'],
                            ['Update', 'Update']
                        ]
                    })
                }, {
                    xtype: 'fieldcontainer',
                    layout: 'hbox',
                    fieldLabel: '需要跨服務',
                    defaultType: 'radiofield',
                    labelAlign: 'right',
                    items: [{
                        labelAlign: 'right',
                        boxLabel: '是',
                        name: 'NEED_REDIRECT',
                        inputValue: 'Y',
                    }, {
                        boxLabel: '否',
                        name: 'NEED_REDIRECT',
                        inputValue: 'N',
                        margin: '0 0 0 60',
                        checked: true
                    }]
                }, {
                    fieldLabel: 'Action[來源]',
                    labelAlign: 'right',
                    labelWidth: 100,
                    name: 'REDIRECT_SRC',
                    padding: 5,
                    anchor: '100%',
                    maxLength: 84,
                    allowBlank: true
                }, {
                    fieldLabel: 'Action[跨服務]',
                    labelAlign: 'right',
                    labelWidth: 100,
                    name: 'REDIRECT_PROXY_ACTION',
                    padding: 5,
                    anchor: '100%',
                    maxLength: 84,
                    allowBlank: true
                }, {
                    fieldLabel: 'Method[跨服務]',
                    labelAlign: 'right',
                    labelWidth: 100,
                    name: 'REDIRECT_PROXY_METHOD',
                    padding: 5,
                    anchor: '100%',
                    maxLength: 84,
                    allowBlank: true
                }, {
                    xtype: 'hidden',
                    fieldLabel: 'UUID',
                    name: 'UUID',
                    padding: 5,
                    anchor: '100%',
                    maxLength: 84,
                    id: 'ProxyForm.UUID'
                }],
                fbar: [{
                    type: 'button',
                    text: '<img src="' + SYSTEM_URL_ROOT + '/css/images/save.gif" style="width:16px;height:16px;vertical-align:middle;margin-right:5px;"/>' + '儲存',
                    handler: function() {
                        var form = Ext.getCmp('Ext.ProxyForm.Form').getForm();
                        if (form.isValid() == false) {
                            return;
                        }
                        form.submit({
                            waitMsg: '更新中...',
                            success: function(form, action) {
                                Ext.getCmp('ExtProxyForm').uuid = action.result.UUID;
                                Ext.getCmp('ProxyForm.UUID').setValue(action.result.UUID);
                                Ext.getCmp('ExtProxyForm').setTitle(Ext.getCmp('ExtProxyForm').title.replace("新增", "維護"));
                                Ext.MessageBox.show({
                                    title: '功能維護',
                                    msg: '操作完成',
                                    icon: Ext.MessageBox.INFO,
                                    buttons: Ext.Msg.OK,
                                    fn: function() {
                                        Ext.getCmp('Ext.AppPage.Button.Destory').setDisabled(false);
                                    }
                                });
                            },
                            failure: function(form, action) {
                                Ext.MessageBox.show({
                                    title: '功能維護',
                                    msg: action.result.message,
                                    icon: Ext.MessageBox.ERROR,
                                    buttons: Ext.Msg.OK
                                });
                            }
                        });
                    }
                }, {
                    type: 'button',
                    id: 'Ext.AppPage.Button.Destory',
                    text: '<img src="' + SYSTEM_URL_ROOT + '/css/images/delete.png" style="height:16px;vertical-align:middle;margin-right:5px;"/>' + '刪除',
                    handler: function() {

                        Ext.MessageBox.show({
                            title: '功能維護',
                            msg: '刪除此功能項目?',
                            buttons: Ext.Msg.YESNO,
                            fn: function(btn) {
                                if (btn == 'yes') {
                                    WS.ProxyAction.destroyProxyByUuid(Ext.getCmp('ProxyForm.UUID').getValue(), function(obj, a) {
                                        Ext.getCmp('ExtProxyForm').hide();
                                    });
                                }
                            }
                        });
                    }
                }, {
                    type: 'button',
                    text: '<img src="' + SYSTEM_URL_ROOT + '/css/images/leave.png" style="width:20px;height:16px;vertical-align:middle;margin-right:5px;"/>' + '關閉',
                    handler: function() {
                        Ext.getCmp('ExtProxyForm').hide();
                    }
                }]
            })]
            me.callParent(arguments);
        },
        closeEvent: function() {
            this.fireEvent('closeEvent', this);
        },
        listeners: {
            'show': function() {
                Ext.getBody().mask();
                if (this.uuid != undefined) {
                    Ext.getCmp('Ext.AppPage.Button.Destory').setDisabled(false);
                    Ext.getCmp('Ext_ProxyForm_Form_application_head_uuid').setReadOnly(true);
                    Ext.getCmp('Ext.ProxyForm.Form').getForm().load({
                        params: {
                            'pUuid': this.uuid
                        },
                        success: function(response, jsonObj, b) {},
                        failure: function(response, a, b) {
                            r = Ext.decode(response.responseText);
                            alert('err:' + r);
                        }
                    });
                } else {
                    Ext.getCmp('Ext.AppPage.Button.Destory').setDisabled(true);
                    Ext.getCmp('Ext_ProxyForm_Form_application_head_uuid').setReadOnly(false);
                    Ext.getCmp('Ext.ProxyForm.Form').getForm().reset();
                }
            },
            'hide': function() {
                Ext.getBody().unmask();
                this.closeEvent();
            }
        }
    });
});
