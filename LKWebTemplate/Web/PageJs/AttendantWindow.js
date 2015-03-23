/*columns 使用default*/
Ext.define('WS.AttendantWindow', {
    extend: 'Ext.window.Window',
    icon: SYSTEM_URL_ROOT + '/css/images/manb16x16.png',
    title: '人員維護',
    closable:false,
    closeAction: 'destroy',
    param: {
        uuid: undefined
    },
    height: 380,
    width: 550,
    layout: 'fit',
    resizable: false,
    draggable: false,
    initComponent: function() {
        /*uuid的參數設check*/
        this.items = [Ext.create('Ext.form.Panel', {
            layout: {
                type: 'form',
                align: 'stretch'
            },
            api: {
                load: WS.AttendantAction.info,
                submit: WS.AttendantAction.submit
            },
            itemId: 'AttendantForm',
            paramOrder: ['pUuid'],
            border: true,
            autoScroll: true,
            defaultType: 'textfield',
            buttonAlign: 'center',
            items: [{
                xtype: 'container',
                layout: 'anchor',
                defaultType: 'textfield',
                items: [{
                    fieldLabel: '帳號',
                    labelAlign: 'right',
                    labelWidth: 100,
                    name: 'ACCOUNT',
                    anchor: '-50 0',
                    maxLength: 84,
                    allowBlank: false
                }, {
                    fieldLabel: '密碼',
                    labelAlign: 'right',
                    labelWidth: 100,
                    name: 'PASSWORD',
                    anchor: '-50 0',
                    maxLength: 84,
                    allowBlank: false
                }, {
                    fieldLabel: '名稱-繁中',
                    labelAlign: 'right',
                    labelWidth: 100,
                    name: 'C_NAME',
                    anchor: '-50 0',
                    maxLength: 84,
                    allowBlank: false
                }, {
                    fieldLabel: '名稱-英文',
                    labelAlign: 'right',
                    labelWidth: 100,
                    name: 'E_NAME',
                    anchor: '-50 0',
                    maxLength: 340
                }, {
                    fieldLabel: 'E-Mail',
                    labelAlign: 'right',
                    labelWidth: 100,
                    name: 'EMAIL',
                    anchor: '-50 0',
                    maxLength: 84,
                    allowBlank: false
                }, {
                    fieldLabel: '電話',
                    labelAlign: 'right',
                    labelWidth: 100,
                    name: 'PHONE',
                    anchor: '-50 0',
                    maxLength: 84,
                    allowBlank: true
                }]
            }, {
                xtype: 'container',
                layout: 'hbox',
                fieldLabel: '性別',
                defaultType: 'radiofield',
                items: [{
                    fieldLabel: '性別',
                    labelAlign: 'right',
                    boxLabel: '男',
                    name: 'GENDER',
                    inputValue: 'M',
                    checked: true
                }, {
                    boxLabel: '女',
                    name: 'GENDER',
                    inputValue: 'F',
                    padding: '0 0 0 60'
                }]
            }, {
                xtype: 'container',
                layout: 'hbox',
                defaultType: 'radiofield',
                items: [{
                    fieldLabel: '主管',
                    labelAlign: 'right',
                    boxLabel: '是',
                    name: 'IS_MANAGER',
                    inputValue: 'Y',
                }, {
                    boxLabel: '否',
                    name: 'IS_MANAGER',
                    inputValue: 'N',
                    checked: true,
                    padding: '0 0 0 60'
                }]
            }, {
                xtype: 'container',
                layout: 'hbox',
                defaultType: 'radiofield',
                items: [{
                    fieldLabel: '直接人員',
                    labelAlign: 'right',
                    boxLabel: '是',
                    name: 'IS_DIRECT',
                    inputValue: 'Y',
                }, {
                    boxLabel: '否',
                    name: 'IS_DIRECT',
                    inputValue: 'N',
                    checked: true,
                    padding: '0 0 0 60'
                }]
            }, {
                xtype: 'hidden',
                name: 'IS_SUPPER',
                value: 'N'
            }, {
                xtype: 'hidden',
                name: 'IS_ADMIN',
                value: 'N'
            }, {
                xtype: 'container',
                layout: 'hbox',
                defaultType: 'radiofield',
                items: [{
                    fieldLabel: '是否啟用',
                    labelAlign: 'right',
                    boxLabel: '是',
                    name: 'IS_ACTIVE',
                    inputValue: 'Y',
                    checked: true
                }, {
                    boxLabel: '否',
                    name: 'IS_ACTIVE',
                    inputValue: 'N',
                    padding: '0 0 0 60'
                }]
            }, {
                xtype: 'hidden',
                fieldLabel: 'UUID',
                name: 'UUID',
                padding: 5,
                anchor: '100%',
                maxLength: 84,
                itemId: 'UUID'
            }, {
                xtype: 'hidden',
                fieldLabel: 'COMPANY_UUID',
                name: 'COMPANY_UUID',
                padding: 5,
                anchor: '100%',
                maxLength: 84
            }, {
                xtype: 'hidden',
                fieldLabel: 'ID',
                name: 'ID',
                padding: 5,
                anchor: '100%',
                maxLength: 84
            }],
            fbar: [{
                type: 'button',
                icon: SYSTEM_URL_ROOT + '/css/custimages/save16x16.png',
                text: '儲存',
                handler: function() {
                    var _main = this.up('window').down("#AttendantForm"),
                        form = _main.getForm();
                    if (form.isValid() == false) {
                        return;
                    };
                    form.submit({
                        waitMsg: '更新中...',
                        success: function(form, action) {
                            this.param.uuid = action.result.UUID;
                            this.down("#UUID").setValue(action.result.UUID);
                            Ext.MessageBox.show({
                                title: '維護人員',
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
            if (this.param.uuid != undefined) {
                this.down("#AttendantForm").getForm().load({
                    params: {
                        'pUuid': this.param.uuid
                    },
                    success: function(response, a, b) {},
                    failure: function(response, jsonObj, b) {
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
                this.down('#AttendantForm').getForm().reset();
            }
        },
        'hide': function() {
            Ext.getBody().unmask();
            this.closeEvent();
        }
    }
});
