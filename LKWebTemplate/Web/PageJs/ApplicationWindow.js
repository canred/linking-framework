var WS_APPLICATIONWINDOW;
Ext.define('WS.ApplicationWindow', {
    extend: 'Ext.window.Window',
    title: '系統維護',
    icon: SYSTEM_URL_ROOT + '/css/images/application16x16.png',
    closeAction: 'destroy',
    param: {
        uuid: undefined
    },
    closable: false,
    width: 550,
    height: 250,
    layout: 'fit',
    resizable: false,
    draggable: false,
    initComponent: function() {
        this.items = [Ext.create('Ext.form.Panel', {
            layout: {
                type: 'form',
                align: 'stretch'
            },
            api: {
                load: WS.ApplicationAction.info,
                submit: WS.ApplicationAction.submit
            },
            itemId: 'ApplicationForm',
            paramOrder: ['pUuid'],
            border: true,
            bodyPadding: 5,
            defaultType: 'textfield',
            buttonAlign: 'center',
            items: [{
                fieldLabel: '公司代碼',
                itemId: 'ID',
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
                    itemId: 'IS_ACTIVE_Y',
                    checked: true,
                    labelAlign: 'right'
                }, {
                    boxLabel: '否',
                    name: 'IS_ACTIVE',
                    inputValue: 'N',
                    itemId: 'IS_ACTIVE_N',
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
                    var mainWin = this.up('window'),form = mainWin.down("#ApplicationForm").getForm();
                    if (form.isValid() == false) {
                        return;
                    };
                    mainWin.down("#ID").setDisabled(false);
                    form.submit({
                        waitMsg: '更新中...',
                        success: function(form, jsonObj) {
                            this.param.uuid = jsonObj.result.UUID;
                            this.down("#UUID").setValue(jsonObj.result.UUID);
                            this.down("#ID").setDisabled(true);
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
                        },
                        scope: mainWin
                    });
                }
            }, {
                type: 'button',
                icon: SYSTEM_URL_ROOT + '/css/custimages/exit16x16.png',
                text: '關閉',
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
            Ext.getBody().mask();
            if (this.param.uuid != undefined) {
                this.down("#ID").setDisabled(true);
                this.down("#ApplicationForm").getForm().load({
                    params: {
                        'pUuid': this.param.uuid
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
                this.down("#ID").setDisabled(false);
                this.down("#ApplicationForm").getForm().reset();
            };
        },
        'close': function() {
            Ext.getBody().unmask();
            this.closeEvent();
        }
    }
});