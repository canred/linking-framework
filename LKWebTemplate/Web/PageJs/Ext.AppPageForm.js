Ext.onReady(function() {
    Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION + ".AppPageAction"));
    Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION + ".ApplicationAction"));
    Ext.define('AppPageForm', {
        extend: 'Ext.window.Window',
        title: '功能 新增/修改',
        closeAction: 'hide',
        uuid: undefined,
        id: 'ExtAppPageForm',
        width: 550,
        height: 350,
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
                    load: WS.AppPageAction.info,
                    submit: WS.AppPageAction.submit,
                    destroy: WS.AppPageAction.destroyByUuid
                },
                id: 'Ext.AppPageForm.Form',
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
                    id: 'Ext_AppPageForm_Form_application_head_uuid',
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
                                    Ext.getCmp('Ext_AppPageForm_Form_application_head_uuid').setValue(obj.getAt(0).data.UUID);
                                }
                            }
                        },
                        remoteSort: true,
                        sorters: [{
                            property: 'NAME'
                        }]
                    })
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
                        id: 'IS_ACTIVE.Y',
                        checked: true
                    }, {
                        boxLabel: '否',
                        name: 'IS_ACTIVE',
                        inputValue: 'N',
                        id: 'IS_ACTIVE.N',
                        margin: '0 0 0 60'
                    }]
                }, {
                    xtype: 'hidden',
                    fieldLabel: 'UUID',
                    name: 'UUID',
                    padding: 5,
                    anchor: '100%',
                    maxLength: 84,
                    id: 'AppPageForm.UUID'
                }],
                fbar: [{
                    type: 'button',
                    text: '<img src="' + SYSTEM_URL_ROOT + '/css/images/save.gif" style="width:16px;height:16px;vertical-align:middle;margin-right:5px;"/>' + '儲存',
                    handler: function() {
                        var form = Ext.getCmp('Ext.AppPageForm.Form').getForm();
                        if (form.isValid() == false) {
                            return;
                        }
                        form.submit({
                            waitMsg: '更新中...',
                            success: function(form, jsonObj) {
                                Ext.getCmp('ExtAppPageForm').uuid = jsonObj.result.UUID;
                                Ext.getCmp('AppPageForm.UUID').setValue(jsonObj.result.UUID);
                                Ext.getCmp('ExtAppPageForm').setTitle(Ext.getCmp('ExtAppPageForm').title.replace("新增", "維護"));
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
                            failure: function(form, jsonObj) {
                                Ext.MessageBox.show({
                                    title: '功能維護',
                                    msg: jsonObj.result.message,
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
                                    WS.AppPageAction.destroyByUuid(Ext.getCmp('AppPageForm.UUID').getValue(), function(obj, jsonObj) {
                                        if (jsonObj.result.success) {
                                            Ext.getCmp('ExtAppPageForm').hide();
                                        } else {
                                            Ext.MessageBox.show({
                                                title: 'Warning',
                                                msg: jsonObj.result.message,
                                                icon: Ext.MessageBox.ERROR,
                                                buttons: Ext.Msg.OK
                                            });
                                        }
                                    });
                                }
                            }
                        });
                    }
                }, {
                    type: 'button',
                    text: '<img src="' + SYSTEM_URL_ROOT + '/css/images/leave.png" style="width:20px;height:16px;vertical-align:middle;margin-right:5px;"/>' + '關閉',
                    handler: function() {
                        Ext.getCmp('ExtAppPageForm').hide();
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
                    Ext.getCmp('Ext_AppPageForm_Form_application_head_uuid').setReadOnly(true);
                    Ext.getCmp('Ext.AppPageForm.Form').getForm().load({
                        params: {
                            'pUuid': this.uuid
                        },
                        success: function(response, jsonObj, b) {},
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
                    Ext.getCmp('Ext.AppPage.Button.Destory').setDisabled(true);
                    Ext.getCmp('Ext_AppPageForm_Form_application_head_uuid').setReadOnly(false);
                    Ext.getCmp('Ext.AppPageForm.Form').getForm().reset();
                }
            },
            'hide': function() {
                Ext.getBody().unmask();
                this.closeEvent();
            }
        }
    });
});
