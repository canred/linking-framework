/*columns 使用default*/
Ext.define('WS.AppPageWindow', {
    extend: 'Ext.window.Window',
    title: '功能維護',
    icon: SYSTEM_URL_ROOT + '/css/images/apppage16x16.png',
    closable: false,
    closeAction: 'destroy',
    param: {
        uuid: undefined
    },
    width: 550,
    height: 350,
    layout: 'fit',
    resizable: false,
    draggable: true,
    myStore: {
        application: Ext.create('Ext.data.Store', {
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
            remoteSort: true,
            sorters: [{
                property: 'NAME'
            }]
        })
    },
    initComponent: function() {
        this.items = [Ext.create('Ext.form.Panel', {
            layout: {
                type: 'form',
                align: 'stretch'
            },
            api: {
                load: WS.AppPageAction.info,
                submit: WS.AppPageAction.submit,
                destroy: WS.AppPageAction.destroyByUuid
            },
            itemId: 'AppPageForm',
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
                itemId: 'Ext_AppPageForm_Form_application_head_uuid',
                displayField: 'NAME',
                valueField: 'UUID',
                editable: false,
                readOnly: true,
                store: this.myStore.application
            }, {
                fieldLabel: '功能代碼',
                labelAlign: 'right',
                labelWidth: 100,
                name: 'ID',
                padding: 5,
                anchor: '100%',
                maxLength: 84,
                allowBlank: false
            }, {
                fieldLabel: '功能名稱',
                labelAlign: 'right',
                labelWidth: 100,
                name: 'NAME',
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
                allowBlank: true
            }, {
                fieldLabel: '行為',
                labelAlign: 'right',
                labelWidth: 100,
                name: 'P_MODE',
                padding: 5,
                anchor: '100%',
                maxLength: 84,
                allowBlank: true
            }, {
                fieldLabel: '參數',
                labelAlign: 'right',
                labelWidth: 100,
                name: 'PARAMETER_CLASS',
                padding: 5,
                anchor: '100%',
                maxLength: 84,
                allowBlank: true
            }, {
                fieldLabel: '路徑',
                labelAlign: 'right',
                labelWidth: 100,
                name: 'URL',
                padding: 5,
                anchor: '100%',
                maxLength: 84,
                allowBlank: false
            }, {
                xtype: 'container',
                layout: 'hbox',
                fieldLabel: '是否啟用',
                defaultType: 'radiofield',
                items: [{
                    fieldLabel: '啟用',
                    labelAlign: 'right',
                    boxLabel: '是',
                    name: 'IS_ACTIVE',
                    inputValue: 'Y',
                    id: 'IS_ACTIVE_Y',
                    checked: true
                }, {
                    boxLabel: '否',
                    name: 'IS_ACTIVE',
                    inputValue: 'N',
                    id: 'IS_ACTIVE_N',
                    margin: '0 0 0 60'
                }]
            }, {
                xtype: 'hidden',
                fieldLabel: 'UUID',
                name: 'UUID',
                padding: 5,
                anchor: '100%',
                maxLength: 84,
                itemId: 'UUID'
            }],
            fbar: [{
                type: 'button',
                icon: SYSTEM_URL_ROOT + '/css/custimages/save16x16.png',
                text: '儲存',
                handler: function() {
                    var mainWin = this.up('window'),
                        form = mainWin.down('#AppPageForm').getForm();
                    if (form.isValid() == false) {
                        return;
                    };
                    form.submit({
                        waitMsg: '更新中...',
                        success: function(form, jsonObj) {
                            this.param.uuid = jsonObj.result.UUID;
                            this.down("#UUID").setValue(jsonObj.result.UUID);
                            Ext.MessageBox.show({
                                title: '功能維護',
                                msg: '操作完成',
                                icon: Ext.MessageBox.INFO,
                                buttons: Ext.Msg.OK,
                                fn: function() {
                                    this.down('#btnDelete').setDisabled(false);
                                },
                                scope: this
                            });
                        },
                        failure: function(form, jsonObj) {
                            Ext.MessageBox.show({
                                title: '功能維護',
                                msg: jsonObj.result.message,
                                icon: Ext.MessageBox.ERROR,
                                buttons: Ext.Msg.OK
                            });
                        },
                        scope: mainWin
                    });
                }
            }, {
                type: 'button',
                itemId: 'btnDelete',
                icon: SYSTEM_URL_ROOT + '/css/custimages/delete.png',
                text: '刪除',
                handler: function() {
                    Ext.MessageBox.show({
                        title: '功能維護',
                        msg: '刪除此功能項目?',
                        buttons: Ext.Msg.YESNO,
                        fn: function(btn) {
                            if (btn == 'yes') {
                                WS.AppPageAction.destroyByUuid(this.down('#UUID').getValue(), function(obj, jsonObj) {
                                    if (jsonObj.result.success) {
                                        this.hide();
                                    } else {
                                        Ext.MessageBox.show({
                                            title: 'Warning',
                                            msg: jsonObj.result.message,
                                            icon: Ext.MessageBox.ERROR,
                                            buttons: Ext.Msg.OK
                                        });
                                    }
                                }, this);
                            };
                        },
                        scope: this.up('window')
                    });
                }
            }, {
                type: 'button',
                icon: SYSTEM_URL_ROOT + '/css/custimages/exit16x16.png',
                text: '關閉',
                handler: function() {
                    this.up('window').hide();
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
            Ext.getBody().mask();
            this.down('#Ext_AppPageForm_Form_application_head_uuid').getStore().load({
                callback: function() {
                    if (this.param.uuid != undefined) {
                        this.down('#btnDelete').setDisabled(false);
                        this.down('#Ext_AppPageForm_Form_application_head_uuid').setReadOnly(true);
                        this.down('#AppPageForm').getForm().load({
                            params: {
                                'pUuid': this.param.uuid
                            },                            
                            failure: function(form, jsonObj) {
                                Ext.MessageBox.show({
                                    title: 'Warning',
                                    msg: jsonObj.result.message,
                                    icon: Ext.MessageBox.ERROR,
                                    buttons: Ext.Msg.OK
                                });
                            }
                        });
                    } else {
                        this.down('#btnDelete').setDisabled(true);
                        this.down('#Ext_AppPageForm_Form_application_head_uuid').setReadOnly(false);
                        this.down('#AppPageForm').getForm().reset();
                    }
                },
                scope: this
            });

        },
        'hide': function() {
            Ext.getBody().unmask();
            this.closeEvent();
        }
    }
});
