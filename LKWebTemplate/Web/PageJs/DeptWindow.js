/*columns 使用default*/
Ext.define('WS.DeptWindow', {
    extend: 'Ext.window.Window',
    icon: SYSTEM_URL_ROOT + '/css/images/organisation16x16.png',
    title: '部門維護',
    closable: false,
    closeAction: 'destroy',
    param: {
        uuid: undefined,
        companyUuid: undefined,
        parentDepartmentUuid: undefined
    },
    myStore: {
        department: Ext.create('Ext.data.Store', {
            extend: 'Ext.data.Store',
            autoLoad: false,
            model: 'DEPARTMENT',
            pageSize: 99999,
            remoteSort: true,
            proxy: {
                type: 'direct',
                api: {
                    read: WS.DeptAction.loadDepartment
                },
                reader: {
                    root: 'data'
                },
                paramsAsHash: true,
                paramOrder: ['pCompanyUuid', 'page', 'limit', 'sort', 'dir'],
                extraParams: {
                    'pCompanyUuid': ''
                },
                simpleSortMode: true,
                listeners: {
                    exception: function(proxy, response, operation) {
                        Ext.MessageBox.show({
                            title: 'REMOTE EXCEPTON A',
                            msg: operation.getError(),
                            icon: Ext.MessageBox.ERROR,
                            buttons: Ext.Msg.OK
                        });
                    }
                }
            },
            sorters: [{
                property: 'C_NAME',
                direction: 'ASC'
            }]
        })
    },
    height: 380,
    width: 550,
    layout: 'fit',
    resizable: false,
    draggable: false,
    initComponent: function() {
        this.items = [Ext.create('Ext.form.Panel', {
            api: {
                load: WS.DeptAction.info,
                submit: WS.DeptAction.submit
            },
            itemId: 'DeptForm',
            paramOrder: ['pUuid'],
            border: true,
            defaultType: 'textfield',
            buttonAlign: 'center',
            items: [{
                xtype: 'container',
                layout: 'anchor',
                margin: '5 0 0 0',
                defaultType: 'textfield',
                defaults: {
                    width: 500,
                    labelAlign: 'right',
                    labelWidth: 100
                },
                items: [{
                    fieldLabel: '公司',
                    itemId: 'txtCompany',
                    maxLength: 84,
                    readOnly: true
                }, {
                    xtype: 'hiddenfield',
                    itemId: 'COMPANY_UUID',
                    name: 'COMPANY_UUID',
                    maxLength: 84
                }, {
                    fieldLabel: '部門代碼',
                    name: 'ID',
                    maxLength: 84,
                    allowBlank: false
                }, {
                    fieldLabel: '名稱-繁中',
                    name: 'C_NAME',
                    maxLength: 84,
                    allowBlank: false,
                    validator: function(value) {
                        if (value.indexOf(':') != -1) {
                            return "不可以包含「:」字元";
                        } else {
                            return true;
                        }
                    }
                }, {
                    fieldLabel: '名稱-英文',
                    name: 'E_NAME',
                    maxLength: 340,
                    allowBlank: false,
                    validator: function(value) {
                        if (value.indexOf(':') != -1) {
                            return "不可以包含「:」字元";
                        } else {
                            return true;
                        }
                    }
                }, {
                    fieldLabel: '部門簡稱',
                    name: 'S_NAME',
                    maxLength: 84,
                    allowBlank: false,
                    validator: function(value) {
                        if (value.indexOf(':') != -1) {
                            return "不可以包含「:」字元";
                        } else {
                            return true;
                        }
                    }
                }]
            }, {
                xtype: 'container',
                layout: 'hbox',
                defaultType: 'radiofield',
                defaults: {
                    labelAlign: 'right'
                },
                items: [{
                    fieldLabel: '有效性',
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
                xtype: 'container',
                layout: 'hbox',
                width: 500,
                items: [{
                    xtype: 'textfield',
                    fieldLabel: '部門主管',
                    labelAlign: 'right',
                    width: 475,
                    itemId: 'txtManagerName',
                    readOnly: true
                }, {
                    xtype: 'button',
                    icon: SYSTEM_URL_ROOT + '/css/images/manc16x16.png',
                    handler: function(handler, scope) {
                        var mainWin = this.up('window'),
                            subWin = Ext.create('WS.AttendantPickerWindow', {
                                param: {
                                    companyUuid: mainWin.param.companyUuid,
                                    parentObj: mainWin
                                }
                            });
                        subWin.on('selectedEvent', function(obj, returnData) {
                            obj.param.parentObj.down('#txtManagerName').setValue(returnData.C_NAME);
                            obj.param.parentObj.down('#MANAGER_UUID').setValue(returnData.UUID);
                            obj.close();
                        });
                        subWin.on('closeEvent', function(obj) {});
                        subWin.show();
                    }
                }, {
                    xtype: 'hiddenfield',
                    name: 'MANAGER_UUID',
                    itemId: 'MANAGER_UUID'
                }]
            }, {
                xtype: 'container',
                layout: 'hbox',
                width: 500,
                margin: '5 0 0 0',
                items: [{
                    xtype: 'combo',
                    fieldLabel: '隸屬部門',
                    labelAlign: 'right',
                    itemId: 'PARENT_DEPARTMENT_UUID',
                    displayField: 'C_NAME',
                    valueField: 'UUID',
                    name: 'PARENT_DEPARTMENT_UUID',
                    editable: false,
                    hidden: false,
                    width: 475,
                    store: this.myStore.department
                }, {
                    xtype: 'button',
                    icon: SYSTEM_URL_ROOT + '/css/images/organisation16x16.png',
                    handler: function(handler, scope) {
                        var mainWin = this.up('window'),
                            subWin = Ext.create('WS.DepartmentPicker', {
                                param: {
                                    companyUuid: mainWin.param.companyUuid,
                                    parentObj: mainWin
                                }
                            });
                        subWin.on('selected', function(obj, returnData) {
                            obj.param.parentObj.down('#PARENT_DEPARTMENT_UUID').setValue(returnData.UUID);
                            obj.close();
                        });                        
                        subWin.show();
                    }
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
                itemId: 'btnSave',
                handler: function() {
                    var _main = this.up('window').down("#DeptForm"),
                        form = _main.getForm();
                    if (form.isValid() == false) {
                        return;
                    };
                    form.submit({
                        waitMsg: '更新中...',
                        success: function(form, action) {
                            this.param.uuid = action.result.UUID;
                            this.down("#UUID").setValue(action.result.UUID);
                            this.down("#DeptForm").getForm().load({
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
                            Ext.MessageBox.show({
                                title: '維護部門',
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
            if (!this.param.companyUuid) {
                Ext.MessageBox.show({
                    title: '部門物件',
                    icon: Ext.MessageBox.WARNING,
                    buttons: Ext.Msg.OK,
                    msg: '初始化錯誤【1504011343】'
                });
                return false;
            };
            this.param.parentObj.mask();
            var proxy = this.myStore.department.getProxy();
            proxy.setExtraParam('pCompanyUuid', this.param.companyUuid);
            this.myStore.department.load();
            WS.CompanyAction.getCompany(this.param.companyUuid, function(jsonObj) {
                if (jsonObj.data.length == 1) {
                    this.down('#txtCompany').setValue(jsonObj.data[0].C_NAME);
                };
            }, this);
            if (this.param.uuid != undefined) {
                this.down("#DeptForm").getForm().load({
                    params: {
                        'pUuid': this.param.uuid
                    },
                    success: function(response, jsonObj) {
                        if (jsonObj.result.data.MANAGER_UUID != "") {
                            WS.AttendantAction.getUserName(jsonObj.result.data.MANAGER_UUID, function(jsonObj) {
                                this.down("#txtManagerName").setValue(jsonObj);
                            }, this);
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
                    },
                    scope: this
                });
            } else {
                this.down('#DeptForm').getForm().reset();
                this.down('#PARENT_DEPARTMENT_UUID').setValue(this.param.parentDepartmentUuid);
                this.down("#COMPANY_UUID").setValue(this.param.companyUuid);
            };
        },
        'close': function() {
            this.closeEvent();
            this.param.parentObj.unmask();
        }
    }
});
