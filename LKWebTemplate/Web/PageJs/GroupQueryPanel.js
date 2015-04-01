/*全局變量*/
var WS_GROUPQUERYPANEL;
/*WS.CompanyQueryPanel物件類別*/
/*TODO*/
/*
1.Model 要集中                                 [YES]
2.panel 的title要換成icon , title的方式        [YES]
3.add 的icon要換成icon , title的方式           [YES]
4.getCmp 不可能有
*/
/*columns 使用default*/
Ext.define('WS.GroupQueryPanel', {
    extend: 'Ext.panel.Panel',
    closeAction: 'destroy',
    /*語言擴展*/
    lan: {},
    /*參數擴展*/
    param: {
        showADSync: true
    },
    /*值擴展*/
    val: {
        attendantUuid: '',
    },
    /*物件會用到的Store物件*/
    myStore: {
        grouphead: Ext.create('Ext.data.Store', {
            successProperty: 'success',
            autoLoad: false,
            model: 'GROUP_HEAD_V',
            pageSize: 10,
            proxy: {
                type: 'direct',
                api: {
                    read: WS.GroupHeadAction.load
                },
                reader: {
                    root: 'data'
                },
                paramsAsHash: true,
                paramOrder: ['company_uuid', 'application_head_uuid', 'attendant_uuid', 'keyword', 'page', 'limit', 'sort', 'dir'],
                extraParams: {
                    company_uuid: '<%= getUser().COMPANY_UUID %>',
                    application_head_uuid: '',
                    attendant_uuid: '',
                    keyword: ''
                },
                simpleSortMode: true,
                listeners: {
                    exception: function(proxy, response, operation) {
                        Ext.MessageBox.show({
                            title: 'REMOTE EXCEPTION1',
                            msg: response.result.message,
                            icon: Ext.MessageBox.ERROR,
                            buttons: Ext.Msg.OK
                        });
                    }
                }
            },
            remoteSort: true,
            sorters: [{
                property: 'NAME_ZH_TW',
                direction: 'ASC'
            }]
        }),
        application: Ext.create('Ext.data.Store', {
            successProperty: 'success',
            autoLoad: false,
            remoteSort: true,
            model: 'APPLICATION',
            pageSize: 9999,
            proxy: {
                type: 'direct',
                api: {
                    read: WS.ApplicationAction.loadApplication
                },
                reader: {
                    root: 'data'
                },
                paramsAsHash: true,
                paramOrder: ['pKeyWord', 'page', 'limit', 'sort', 'dir'],
                extraParams: {
                    pKeyWord: ''
                },
                simpleSortMode: true,
                listeners: {
                    exception: function(proxy, response, operation) {
                        Ext.MessageBox.show({
                            title: 'REMOTE EXCEPTION3',
                            msg: operation.getError(),
                            icon: Ext.MessageBox.ERROR,
                            buttons: Ext.Msg.OK
                        });
                    }
                }
            },
            sorters: [{
                property: 'NAME',
                direction: 'ASC'
            }]
        })
    },
    fnActiveRender: function isActiveRenderer(value, id, r) {
        var html = "<img src='" + SYSTEM_URL_ROOT;
        return value === "Y" ? html + "/css/custimages/active03.png'>" : html + "/css/custimages/unactive03.png'>";
    },
    initComponent: function() {
        this.items = [{
            xtype: 'panel',
            title: '權限維護',
            icon: SYSTEM_URL_ROOT + '/css/images/lock16x16.png',
            frame: true,
            autoWidth: true,
            height: $(document).height() - 150,
            items: [{
                xtype: 'container',
                layout: 'hbox',
                margin: 5,
                items: [{
                    xtype: 'combobox',
                    fieldLabel: '系統',
                    queryMode: 'remote',
                    displayField: 'NAME',
                    valueField: 'UUID',
                    itemId: 'cmbApplication',
                    labelWidth: 40,
                    store: this.myStore.application,
                    editable: false,
                    triggerAction: 'all',
                    selectOnFocus: true,
                    enableKeyEvents: true,
                    listeners: {
                        afterrender: function(combo) {
                            var recordSelected = combo.getStore().getAt(0);
                        },
                        keyup: function(e, t, eOpts) {
                            var keyCode = t.keyCode;
                            if (keyCode == Ext.event.Event.ENTER) {
                                this.up('panel').down("#btnQuery").handler();
                            };
                        }
                    }
                }, {
                    xtype: 'textfield',
                    itemId: 'txtSearch',
                    fieldLabel: '關鍵字',
                    enableKeyEvents: true,
                    labelWidth: 60,
                    margin: '0 0 0 20',
                    listeners: {
                        keyup: function(e, t, eOpts) {
                            var keyCode = t.keyCode;
                            if (keyCode == Ext.event.Event.ENTER) {
                                this.up('panel').down("#btnQuery").handler();
                            };
                        }
                    }
                }, {
                    xtype: 'textfield',
                    fieldLabel: '內含人員',
                    name: 'display_attendant_uuid',
                    itemId: 'display_attendant_uuid',
                    emptyText: '',
                    allowBlank: true,
                    width: 160,
                    labelWidth: 60,
                    margin: '0 0 0 20'
                }, {
                    xtype: 'button',
                    icon: SYSTEM_URL_ROOT + '/css/images/manc16x16.png',
                    handler: function() {
                        var mainPanel = this.up('panel').up('panel'),
                            companyUuid = mainPanel.param.companyUuid,
                            application = mainPanel.down("#cmbApplication").getValue();
                        if (Ext.isEmpty(application)) {
                            Ext.MessageBox.show({
                                title: '系統提示',
                                icon: Ext.MessageBox.INFO,
                                buttons: Ext.Msg.OK,
                                msg: '請先選擇系統欄位'
                            });
                            return false;
                        };
                        var subWin = Ext.create('WS.AttendantPickerWindow', {
                            param: {
                                companyUuid: companyUuid
                            }
                        });
                        subWin.on('selectedEvent', function(obj,record) {
                            mainPanel.down('#display_attendant_uuid').setValue(record["C_NAME"]);
                            mainPanel.val.attendantUuid = record["UUID"];
                            mainPanel.down('#btnQuery').handler();
                            subWin.close();
                        });
                        subWin.show();
                    }
                }, {
                    xtype: 'button',
                    text: '查詢',
                    icon: SYSTEM_URL_ROOT + '/css/custimages/find.png',
                    margin: '0 0 0 20',
                    itemId: 'btnQuery',
                    handler: function() {
                        var mainPanel = this.up('panel').up('panel'),
                            store = mainPanel.myStore.grouphead,
                            proxy = store.getProxy(),
                            application = mainPanel.down("#cmbApplication").getValue();
                        if (Ext.isEmpty(application)) {
                            Ext.MessageBox.show({
                                title: '系統提示',
                                icon: Ext.MessageBox.INFO,
                                buttons: Ext.Msg.OK,
                                msg: '請先選擇系統欄位'
                            });
                            return false;
                        };
                        proxy.setExtraParam('company_uuid', mainPanel.param.companyUuid);
                        proxy.setExtraParam('application_head_uuid', mainPanel.down('#cmbApplication').getValue());
                        proxy.setExtraParam('attendant_uuid', mainPanel.val.attendantUuid);
                        proxy.setExtraParam('keyword', mainPanel.down('#txtSearch').getValue());
                        store.load();
                    }
                }, {
                    xtype: 'button',
                    icon: SYSTEM_URL_ROOT + '/css/custimages/clear.png',
                    text: '清除',
                    margin: '0 0 0 20',
                    icon: SYSTEM_URL_ROOT + '/css/custimages/clear.gif',
                    handler: function() {
                        var mainPanel = this.up('panel').up('panel'),
                            clear = function(obj) {
                                obj.down("#txtSearch").setValue('');
                                obj.down('#display_attendant_uuid').setValue('');
                                obj.val.attendantUuid = '';
                            }(mainPanel);
                    }
                }]
            }, {
                xtype: 'gridpanel',
                store: this.myStore.grouphead,
                padding: 5,
                border: true,
                columns: [{
                    text: "編輯",
                    xtype: 'actioncolumn',
                    dataIndex: 'UUID',
                    align: 'center',
                    width: 80,
                    items: [{
                        tooltip: '*編輯',
                        icon: SYSTEM_URL_ROOT + '/css/images/edit16x16.png',
                        handler: function(grid, rowIndex, colIndex) {
                            var mainPanel = grid.up('panel').up('panel').up('panel'),
                                uuid = grid.getStore().getAt(rowIndex).data.UUID,
                                subWin = Ext.create('WS.GroupWindow', {
                                    param: {
                                        'uuid': uuid
                                    }
                                });
                            subWin.on('closeEvent', function(obj) {
                                mainPanel.myStore.grouphead.load();
                            });
                            subWin.show();
                        }
                    }],
                    sortable: false,
                    hideable: false
                }, {
                    header: "群組繁中",
                    dataIndex: 'NAME_ZH_TW',
                    align: 'left',
                    flex: 1
                }, {
                    header: "群組簡中",
                    dataIndex: 'NAME_ZH_CN',
                    align: 'left',
                    flex: 1
                }, {
                    header: "群組英文",
                    dataIndex: 'NAME_EN_US',
                    align: 'left',
                    flex: 1
                }, {
                    header: '代碼',
                    dataIndex: 'ID',
                    align: 'left',
                    flex: 1
                }],
                height: 450,
                tbarCfg: {
                    buttonAlign: 'right'
                },
                bbar: Ext.create('Ext.toolbar.Paging', {
                    store: this.myStore.grouphead,
                    displayInfo: true,
                    displayMsg: '第{0}~{1}資料/共{2}筆',
                    emptyMsg: "無資料顯示"
                }),
                tbar: [{
                    icon: SYSTEM_URL_ROOT + '/css/images/add16x16.png',
                    text: '新增',
                    handler: function() {
                        var mainPanel = this.up('panel').up('panel').up('panel');
                        var subWin = Ext.create('WS.GroupWindow', {
                            param: {
                                uuid: undefined
                            }
                        });
                        subWin.on('closeEvent', function(obj) {
                            mainPanel.myStore.grouphead.load();
                        });
                        subWin.show();
                    }
                }]
            }]
        }];
        this.callParent(arguments);
    },
    listeners: {
        afterrender: function() {
            this.myStore.application.load({
                callback: function(obj) {
                    if (obj.length > 0) {
                        this.down('#cmbApplication').setValue(obj[0].data.UUID);
                    };
                    this.down('#btnQuery').handler(this.down('#btnQuery'));
                },
                scope: this
            });
        }
    }
});
