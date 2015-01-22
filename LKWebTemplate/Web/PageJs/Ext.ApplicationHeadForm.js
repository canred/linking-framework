Ext.onReady(function() {
    Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION + ".ApplicationAction"));
    Ext.define('ApplicationHeadForm', {
        extend: 'Ext.window.Window',
        title: '系統 新增/修改',
        closeAction: 'hide',
        uuid: undefined,
        id: 'ExtApplicationHeadForm',
        width: 550,
        height: 250,
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
                    load: WS.ApplicationAction.info,
                    submit: WS.ApplicationAction.submit
                },
                id: 'Ext.ApplicationHeadForm.Form',
                paramOrder: ['pUuid'],
                border: true,
                bodyPadding: 5,
                defaultType: 'textfield',
                buttonAlign: 'center',
                items: [{
                    fieldLabel: '公司代碼',
                    id: 'ID',
                    labelWidth: 100,
                    name: 'ID',
                    padding: 5,
                    anchor: '100%',
                    maxLength: 36,
                    allowBlank: false,
                    labelAlign: 'right'
                }, {
                    fieldLabel: '名稱',
                    labelWidth: 100,
                    name: 'NAME',
                    padding: 5,
                    anchor: '100%',
                    maxLength: 84,
                    allowBlank: false,
                    labelAlign: 'right'
                }, {
                    fieldLabel: '描述',
                    labelWidth: 100,
                    name: 'DESCRIPTION',
                    padding: 5,
                    anchor: '100%',
                    maxLength: 84,
                    allowBlank: false,
                    labelAlign: 'right'
                }, {
                    fieldLabel: 'Web Site',
                    labelWidth: 100,
                    name: 'WEB_SITE',
                    padding: 5,
                    anchor: '100%',
                    maxLength: 84,
                    allowBlank: true,
                    labelAlign: 'right'
                }, {
                    xtype: 'container',
                    layout: 'hbox',
                    defaultType: 'radiofield',
                    items: [{
                        fieldLabel: '啟用',
                        boxLabel: '是',
                        name: 'IS_ACTIVE',
                        inputValue: 'Y',
                        id: 'IS_ACTIVE.Y',
                        checked: true,
                        labelAlign: 'right'
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
                    id: 'ApplicationHeadForm.UUID'
                }],
                fbar: [{
                    type: 'button',
                    text: '<img src="' + SYSTEM_URL_ROOT + '/css/images/save.gif" style="width:16px;height:16px;vertical-align:middle;margin-right:5px;"/>' + '儲存',
                    handler: function() {
                        var form = Ext.getCmp('Ext.ApplicationHeadForm.Form').getForm();
                        if (form.isValid() == false) {
                            return;
                        }
                        Ext.getCmp('ID').setDisabled(false);
                        form.submit({
                            waitMsg: '更新中...',
                            success: function(form, jsonObj) {
                                Ext.getCmp('ExtApplicationHeadForm').uuid = jsonObj.result.UUID;
                                Ext.getCmp('ApplicationHeadForm.UUID').setValue(jsonObj.result.UUID);
                                Ext.getCmp('ExtApplicationHeadForm').setTitle('系統【維護】');
                                Ext.getCmp('ID').setDisabled(true);
                                Ext.MessageBox.show({
                                    title: '系統維護',
                                    msg: '操作完成',
                                    icon: Ext.MessageBox.INFO,
                                    buttons: Ext.Msg.OK
                                });
                            },
                            failure: function(form, jsonObj) {
                                Ext.MessageBox.show({
                                    title: '系統維護',
                                    msg: jsonObj.result.message,
                                    icon: Ext.MessageBox.ERROR,
                                    buttons: Ext.Msg.OK
                                });
                            }
                        });
                    }
                }, {
                    type: 'button',
                    text: '<img src="' + SYSTEM_URL_ROOT + '/css/images/leave.png" style="width:20px;height:16px;vertical-align:middle;margin-right:5px;"/>' + '關閉',
                    handler: function() {
                        Ext.getCmp('ExtApplicationHeadForm').hide();
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
                    Ext.getCmp('ID').setDisabled(true);

                    Ext.getCmp('Ext.ApplicationHeadForm.Form').getForm().load({
                        params: {
                            'pUuid': this.uuid
                        },
                        success: function(response, a, b) {},
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
                    Ext.getCmp('ID').setDisabled(false);
                    Ext.getCmp('Ext.ApplicationHeadForm.Form').getForm().reset();
                }
            },
            'hide': function() {
                Ext.getBody().unmask();
                this.closeEvent();
            }
        }
    });
});
