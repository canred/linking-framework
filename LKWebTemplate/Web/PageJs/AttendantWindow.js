/*columns 使用default*/
Ext.define('WS.AttendantWindow', {
    extend: 'Ext.window.Window',
    icon: SYSTEM_URL_ROOT + '/css/images/manb16x16.png',
    title: '人員維護',
    closable: false,
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
        this.items = [Ext.create('Ext.form.Panel', {
            api: {
                load: WS.AttendantAction.info,
                submit: WS.AttendantAction.submit
            },
            itemId: 'AttendantForm',
            paramOrder: ['pUuid'],
            border: true,
            defaultType: 'textfield',
            buttonAlign: 'center',
            items: [{
                xtype: 'container',
                layout: 'hbox',
                items: [{
                    xtype: 'container',
                    layout: 'vbox',
                    flex: 2,
                    items: [{
                        xtype: 'container',
                        layout: 'anchor',
                        margin: '5 0 0 0',
                        defaultType: 'textfield',
                        defaults: {
                            width: 320,
                            labelAlign: 'right',
                            labelWidth: 100
                        },
                        items: [{
                            fieldLabel: '帳號',
                            name: 'ACCOUNT',
                            maxLength: 84,
                            allowBlank: false
                        }, {
                            fieldLabel: '密碼',
                            name: 'PASSWORD',
                            maxLength: 84,
                            allowBlank: false
                        }, {
                            fieldLabel: '名稱-繁中',
                            name: 'C_NAME',
                            maxLength: 84,
                            allowBlank: false
                        }, {
                            fieldLabel: '名稱-英文',
                            name: 'E_NAME',
                            maxLength: 340
                        }, {
                            fieldLabel: 'E-Mail',
                            name: 'EMAIL',
                            maxLength: 84,
                            allowBlank: false
                        }, {
                            fieldLabel: '電話',
                            name: 'PHONE',
                            maxLength: 84,
                            allowBlank: true
                        }]
                    }, {
                        xtype: 'container',
                        layout: 'hbox',
                        defaultType: 'radiofield',
                        defaults: {
                            labelAlign: 'right'
                        },
                        items: [{
                            fieldLabel: '性別',
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
                        defaults: {
                            labelAlign: 'right'
                        },
                        items: [{
                            fieldLabel: '主管',
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
                        defaults: {
                            labelAlign: 'right'
                        },
                        items: [{
                            fieldLabel: '直接人員',
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
                        xtype: 'hiddenfield',
                        fieldLabel: 'UUID',
                        name: 'UUID',
                        maxLength: 84,
                        itemId: 'UUID'
                    }, {
                        xtype: 'hiddenfield',
                        fieldLabel: 'COMPANY_UUID',
                        name: 'COMPANY_UUID',
                        maxLength: 84
                    }, {
                        xtype: 'hiddenfield',
                        fieldLabel: 'ID',
                        name: 'ID',
                        maxLength: 84
                    }]
                }, {
                    xtype: 'container',
                    flex: 1,
                    margin: '10',
                    layout: {
                        type: 'vbox'
                    },
                    items: [{
                        xtype: 'image',
                        src: SYSTEM_URL_ROOT + '/css/images/unknowMan.png',
                        itemId: 'imgUser',
                        tag: "img",
                        height: 150,
                        autoWidth: true,
                        border: true
                    }, {
                        xtype: 'filefield',
                        text: '上傳',
                        buttonOnly: true,
                        name: 'filePictureUrl',
                        width: 170,
                        margin: '3 0 0 0',
                        listeners: {
                            change: function(e, t, eOpts) {
                                var mainWin = this.up('window'),
                                    btnSave = mainWin.down('#btnSave');
                                btnSave.handler();
                            }
                        }
                    }]
                }]
            }],
            buttons: [{
                icon: SYSTEM_URL_ROOT + '/css/custimages/save16x16.png',
                text: '儲存',
                itemId: 'btnSave',
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

                            this.down("#AttendantForm").getForm().load({
                                params: {
                                    'pUuid': this.param.uuid
                                },
                                success: function(response, jsonObj) {
                                    var imgSrc = "";
                                    if (jsonObj.result.data.PICTURE_URL.length > 0) {
                                        imgSrc = jsonObj.result.data.PICTURE_URL.replace('~', SYSTEM_URL_ROOT);
                                    }
                                    if (!Ext.isEmpty(imgSrc)) {
                                        response.owner.down("#imgUser").setSrc(imgSrc);
                                    };
                                },
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
            Ext.getBody().mask();
            if (this.param.uuid != undefined) {
                this.down("#AttendantForm").getForm().load({
                    params: {
                        'pUuid': this.param.uuid
                    },
                    success: function(response, jsonObj) {
                        var imgSrc = "";
                        if (jsonObj.result.data.PICTURE_URL.length > 0) {
                            imgSrc = jsonObj.result.data.PICTURE_URL.replace('~', SYSTEM_URL_ROOT);
                        }
                        if (!Ext.isEmpty(imgSrc)) {
                            response.owner.down("#imgUser").setSrc(imgSrc);
                        };
                    },
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
                this.down('#AttendantForm').getForm().reset();
            };
        },
        'hide': function() {
            Ext.getBody().unmask();
            this.closeEvent();
        }
    }
});
