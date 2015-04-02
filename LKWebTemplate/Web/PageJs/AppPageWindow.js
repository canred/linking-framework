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
	modal: true,
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
            api: {
                load: WS.AppPageAction.info,
                submit: WS.AppPageAction.submit,
                destroy: WS.AppPageAction.destroyByUuid
            },
            itemId: 'AppPageForm',
            paramOrder: ['pUuid'],
            border: true,
            defaultType: 'textfield',
            buttonAlign: 'center',
            defaults: {
                anchor: '95%',
                labelAlign: 'right',
                labelWidth: 100
            },
            items: [{
                margin: '5 0 5 0',
                fieldLabel: '系統',
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
                name: 'ID',
                maxLength: 84,
                allowBlank: false
            }, {
                fieldLabel: '功能名稱',
                name: 'NAME',
                maxLength: 84,
                allowBlank: false
            }, {
                fieldLabel: '描述',
                name: 'DESCRIPTION',
                maxLength: 84,
                allowBlank: true
            }, {
                fieldLabel: '行為',
                name: 'P_MODE',
                maxLength: 84,
                allowBlank: true
            }, {
                fieldLabel: '參數',
                name: 'PARAMETER_CLASS',
                maxLength: 84,
                allowBlank: true
            }, {
                fieldLabel: '路徑',
                name: 'URL',
                maxLength: 84,
                allowBlank: false
            },{
                fieldLabel:'RunJsFunction',
                name:'RUNJSFUNCTION',
                maxLength:84,
                allowBlank:true,
                emptyText:'執行Javascript的動作'
            }, {
                xtype: 'container',
                layout: 'hbox',
                fieldLabel: '啟用',                
                defaultType: 'radiofield',                
                items: [{
                    fieldLabel: '啟用',
                    labelAlign: 'right',
                    boxLabel: '是',
                    name: 'IS_ACTIVE',
                    inputValue: 'Y',
                    checked: true
                }, {
                    boxLabel: '否',
                    name: 'IS_ACTIVE',
                    inputValue: 'N',
                    margin: '0 0 0 60'
                }]
            }, {
                xtype: 'hiddenfield',
                fieldLabel: 'UUID',
                name: 'UUID',
                maxLength: 84,
                itemId: 'UUID'
            }],
            buttons: [{
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
                                    };
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
                    this.up('window').hide();
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
                    };
                },
                scope: this
            });

        },
        'hide': function() {            
            this.closeEvent();
        }
    }
});
