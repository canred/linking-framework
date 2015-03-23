Ext.define('WS.ProxyWindow', {
    extend: 'Ext.window.Window',
    title: '資源 維護',
    icon: SYSTEM_URL_ROOT + '/css/images/proxy16x16.png',
    closeAction: 'destroy',
    param: {
        uuid: undefined,
        parentObj: undefined
    },
    width: 550,
    height: 350,
    layout: 'fit',
    resizable: false,
    draggable: true,
    myStore: {},
    initComponent: function() {
        this.myStore.application = Ext.create('Ext.data.Store', {
            successProperty: 'success',
            autoLoad: false,
            model: 'APPLICATION',
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
                        this.down('#application_head_uuid').setValue(obj.getAt(0).raw.UUID);
                    };
                },
                scope: this
            },
            remoteSort: true,
            sorters: [{
                property: 'NAME'
            }]
        });
        this.items = [Ext.create('Ext.form.Panel', {
            api: {
                load: WS.ProxyAction.infoProxy,
                submit: WS.ProxyAction.submitProxy,
                destroy: WS.ProxyAction.destroyProxyByUuid
            },
            itemId: 'frmProxy',
            paramOrder: ['pUuid'],
            border: true,
            defaultType: 'textfield',
            buttonAlign: 'center',
            defaults: {
                labelWidth: 100,
                labelAlign: 'right',
                width: 520,
                margin: '5 0 0 0'
            },
            items: [{
                fieldLabel: '系統',
                xtype: 'combo',
                name: 'APPLICATION_HEAD_UUID',
                itemId: 'application_head_uuid',
                displayField: 'NAME',
                valueField: 'UUID',
                editable: false,
                readOnly: true,
                store: this.myStore.application
            }, {
                fieldLabel: 'Action',
                name: 'PROXY_ACTION',
                maxLength: 84,
                allowBlank: false
            }, {
                fieldLabel: 'Method',
                name: 'PROXY_METHOD',
                maxLength: 84,
                allowBlank: false
            }, {
                fieldLabel: '描述',
                name: 'DESCRIPTION',
                maxLength: 84,
                allowBlank: false
            }, {
                xtype: 'combo',
                fieldLabel: 'Type',
                allowBlank: false,
                queryMode: 'local',
                itemId: 'PROXY_TYPE',
                displayField: 'text',
                valueField: 'value',
                name: 'PROXY_TYPE',
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
                xtype: 'container',
                layout: 'hbox',
                defaultType: 'radiofield',
                items: [{
                    fieldLabel: '需要跨服務',
                    labelAlign: 'right',
                    boxLabel: '是',
                    name: 'NEED_REDIRECT',
                    inputValue: 'Y'
                }, {
                    boxLabel: '否',
                    name: 'NEED_REDIRECT',
                    inputValue: 'N',
                    margin: '0 0 0 60',
                    checked: true
                }]
            }, {
                fieldLabel: 'Action[來源]',
                name: 'REDIRECT_SRC',
                maxLength: 84,
                allowBlank: true
            }, {
                fieldLabel: 'Action[跨服務]',
                name: 'REDIRECT_PROXY_ACTION',
                maxLength: 84,
                allowBlank: true
            }, {
                fieldLabel: 'Method[跨服務]',
                name: 'REDIRECT_PROXY_METHOD',
                maxLength: 84,
                allowBlank: true
            }, {
                xtype: 'hiddenfield',
                fieldLabel: 'UUID',
                name: 'UUID',
                maxLength: 84,
                itemId: 'UUID'
            }],
            buttons: [{
                icon: SYSTEM_URL_ROOT + '/css/custimages/save16X16.png',
                text: '儲存',
                handler: function() {
                    var mainWin = this.up('window'),
                        form = mainWin.down('#frmProxy').getForm();
                    if (form.isValid() == false) {
                        return;
                    };
                    form.submit({
                        waitMsg: '更新中...',
                        success: function(form, action) {
                            var mainWin = this;
                            mainWin.param.uuid = action.result.UUID;
                            mainWin.down('#UUID').setValue(action.result.UUID);
                            Ext.MessageBox.show({
                                title: '功能維護',
                                msg: '操作完成',
                                icon: Ext.MessageBox.INFO,
                                buttons: Ext.Msg.OK,
                                fn: function() {
                                    this.down('#btnDestory').setDisabled(false);
                                },
                                scope: mainWin
                            });
                        },
                        failure: function(form, action) {
                            Ext.MessageBox.show({
                                title: '功能維護',
                                msg: action.result.message,
                                icon: Ext.MessageBox.ERROR,
                                buttons: Ext.Msg.OK
                            });
                        },
                        scope: mainWin
                    });
                }
            }, {
                itemId: 'btnDestory',
                icon: SYSTEM_URL_ROOT + '/css/images/delete16x16.png',
                text: '刪除',
                handler: function() {
                    Ext.MessageBox.show({
                        title: '功能維護',
                        msg: '刪除此功能項目?',
                        buttons: Ext.Msg.YESNO,
                        fn: function(btn) {
                            if (btn == 'yes') {
                                WS.ProxyAction.destroyProxyByUuid(this.down('#UUID').getValue(), function(obj, jsonObj) {
                                    this.close();
                                }, this);
                            };
                        },
                        scope: this.up('window')
                    });
                }
            }, {
                icon: SYSTEM_URL_ROOT + '/css/custimages/exit16x16.png',
                text: '關閉',
                handler: function() {
                    this.up('window').close();
                }
            }]
        })];
        this.callParent(arguments);
    },
    closeEvent: function() {
        this.fireEvent('closeEvent', this);
    },
    listeners: {
        'show': function() {
            if (this.param.parentObj) {
                this.param.parentObj.mask();
            };
            var form = this.down('#frmProxy');
            this.myStore.application.load({
                callback: function() {
                    if (this.param.uuid != undefined) {
                        /*When 編輯/刪除資料*/
                        this.down('#btnDestory').setDisabled(false);
                        this.down('#application_head_uuid').setReadOnly(true);
                        form.getForm().load({
                            params: {
                                'pUuid': this.param.uuid
                            },
                            success: function(response, jsonObj, b) {},
                            failure: function(response, a, b) {
                                r = Ext.decode(response.responseText);
                                alert('err:' + r);
                            }
                        });
                    } else {
                        /*When 新增資料*/
                        this.down('#btnDestory').setDisabled(true);
                        this.down('#application_head_uuid').setReadOnly(false);
                        form.getForm().reset();
                    };
                },
                scope: this
            })

        },
        'close': function() {
            this.closeEvent();
            if (this.param.parentObj) {
                this.param.parentObj.unmask();
            };
        }
    }
});
