/*全局變量*/
var WS_COMPANYQUERYPANEL;
/*WS.CompanyQueryPanel物件類別*/
/*TODO*/
/*
1.Model 要集中                                 [YES]
2.panel 的title要換成icon , title的方式        [YES]
3.add 的icon要換成icon , title的方式           [YES]
4.自動Query 資料                               [YES]
*/
/*columns 使用default*/
Ext.define('WS.CompanyQueryPanel', {
    extend: 'Ext.panel.Panel',
    closeAction: 'destroy',
    subWinCompany: undefined,
    /*語言擴展*/
    lan: {},
    /*參數擴展*/
    param: {
        showADSync: true
    },
    /*值擴展*/
    val: {},
    /*物件會用到的Store物件*/
    myStore: {
        company: Ext.create('Ext.data.Store', {
            successProperty: 'success',
            autoLoad: false,
            model: 'COMPANY',
            pageSize: 10,
            proxy: {
                type: 'direct',
                api: {
                    read: WS.AdminCompanyAction.loadCompany
                },
                reader: {
                    root: 'data'
                },
                paramsAsHash: true,
                paramOrder: ['pKeyword', 'pIsActive', 'page', 'limit', 'sort', 'dir'],
                extraParams: {
                    pKeyword: '',
                    pIsActive: 'Y'
                },
                simpleSortMode: true,
                listeners: {
                    exception: function(proxy, response, operation) {
                        Ext.MessageBox.show({
                            title: 'Warning',
                            msg: response.result.message,
                            icon: Ext.MessageBox.ERROR,
                            buttons: Ext.Msg.OK
                        });
                    }
                }
            },
            remoteSort: true,
            sorters: [{
                property: 'C_NAME',
                direction: 'ASC'
            }]
        })
    },
    fnActiveRender: function isActiveRenderer(value, id, r) {
        var html = "<img src='" + SYSTEM_URL_ROOT;
        return value === "Y" ? html + "/css/custimages/active03.png'>" : html + "/css/custimages/unactive03.png'>";
    },
    initComponent: function() {
        if (Ext.isEmpty(this.subWinCompany)) {
            Ext.MessageBox.show({
                title: '系統提示',
                icon: Ext.MessageBox.WARNING,
                buttons: Ext.Msg.OK,
                msg: '未實現 subWinCompany 物件,無法進行編輯操作!'
            });
            return false;
        };
        this.items = [{
            xtype: 'panel',
            title: '公司維護',
            icon: SYSTEM_URL_ROOT + '/css/images/company16x16.png',
            frame: true,
            border: true,
            height: $(document).height() - 150,
            autoWidth: true,
            items: [{
                xtype: 'container',
                layout: 'hbox',
                margin: '5 0 0 5',
                defaults: {
                    labelAlign: 'right'
                },
                items: [{
                    xtype: 'textfield',
                    fieldLabel: '關鍵字',
                    itemId: 'txt_search',
                    labelWidth: 50,
                    enableKeyEvents: true,
                    listeners: {
                        keyup: function(e, t, eOpts) {
                            var keyCode = t.keyCode;
                            if (keyCode == Ext.event.Event.ENTER) {
                                this.up('panel').down("#btnQuery").handler();
                            };
                        }
                    }
                }, {
                    xtype: 'combobox',
                    queryMode: 'local',
                    fieldLabel: '啟用',
                    labelWidth: 60,
                    width: 150,
                    displayField: 'name',
                    valueField: 'value',
                    itemId: 'cmb_isActive',
                    editable: false,
                    store: {
                        xtype: 'store',
                        fields: ['name', 'value'],
                        data: [{
                            name: "啟用",
                            value: "Y"
                        }, {
                            name: "不啟用",
                            value: "N"
                        }]
                    },
                    value: 'Y',
                    enableKeyEvents: true,
                    listeners: {
                        keyup: function(e, t, eOpts) {
                            var keyCode = t.keyCode;
                            if (keyCode == Ext.event.Event.ENTER) {
                                this.up('panel').down("#btnQuery").handler();
                            };
                        }
                    }
                }, {
                    xtype: 'button',
                    icon: SYSTEM_URL_ROOT + '/css/custimages/find.png',
                    text: '查詢',
                    margin: '0 0 0 20',
                    itemId: 'btnQuery',
                    width: 80,
                    handler: function() {
                        var store = this.up('panel').down("#grdCompanyQuery").getStore(),
                            doSomeghing = function(obj, pl) {
                                obj.getProxy().setExtraParam('pKeyword', pl.down("#txt_search").getValue());
                                obj.getProxy().setExtraParam('pIsActive', pl.down("#cmb_isActive").getValue());
                                obj.loadPage(1);
                            }(store, this.up('panel'));
                    }
                }, {
                    xtype: 'button',
                    width: 80,
                    margin: '0 0 0 5',
                    icon: SYSTEM_URL_ROOT + '/css/custimages/clear.png',
                    text: '清除',
                    tooltip: '*清除目前所有的條件查詢',
                    handler: function() {
                        var mainPanel = this.up('panel');
                        mainPanel.down("#txt_search").setValue('');
                        mainPanel.down("#cmb_isActive").setValue('Y');
                    }
                }]
            }, {
                xtype: 'gridpanel',
                store: this.myStore.company,
                itemId: 'grdCompanyQuery',
                border: true,
                height: $(document).height() - 220,
                padding: 5,
                columns: {
                    defaults: {
                        align: 'left'
                    },
                    items: [{
                        text: "編輯",
                        xtype: 'actioncolumn',
                        dataIndex: 'UUID',
                        align: 'center',
                        width: 60,
                        items: [{
                            tooltip: '*編輯',
                            icon: SYSTEM_URL_ROOT + '/css/images/edit16x16.png',
                            handler: function(grid, rowIndex, colIndex) {
                                var main = grid.up('panel').up('panel').up('panel');
                                if (!main.subWinCompany) {
                                    Ext.MessageBox.show({
                                        title: '系統訊息',
                                        icon: Ext.MessageBox.INFO,
                                        buttons: Ext.Msg.OK,
                                        msg: '未實現 subWinCompany 物件,無法進行編輯操作!'
                                    });
                                    return false;
                                };
                                var subWin = Ext.create(main.subWinCompany, {});
                                subWin.on('closeEvent', function(obj) {
                                    main.down("#grdCompanyQuery").getStore().load();
                                }, main);
                                subWin.param.uuid = grid.getStore().getAt(rowIndex).data.UUID;
                                subWin.show();
                            }
                        }],
                        sortable: false,
                        hideable: false
                    }, {
                        header: "名稱-繁中",
                        dataIndex: 'C_NAME',
                        flex: 1
                    }, {
                        header: "名稱-簡中",
                        dataIndex: 'NAME_ZH_CN',
                        flex: 1
                    }, {
                        header: "名稱-英文",
                        dataIndex: 'E_NAME',
                        flex: 1
                    }, {
                        header: '啟用',
                        dataIndex: 'IS_ACTIVE',
                        align: 'center',
                        width: 60,
                        renderer: this.fnActiveRender
                    }]
                },
                tbarCfg: {
                    buttonAlign: 'right'
                },
                bbar: Ext.create('Ext.toolbar.Paging', {
                    store: this.myStore.company,
                    displayInfo: true,
                    displayMsg: '第{0}~{1}資料/共{2}筆',
                    emptyMsg: "無資料顯示"
                }),
                tbar: [{
                    icon: SYSTEM_URL_ROOT + '/css/images/add16x16.png',
                    text: '新增',
                    handler: function() {
                        var main = this.up('panel').up('panel').up('panel');
                        if (!main.subWinCompany) {
                            Ext.MessageBox.show({
                                title: '系統訊息',
                                icon: Ext.MessageBox.INFO,
                                buttons: Ext.Msg.OK,
                                msg: '未實現 subWinCompany 物件,無法進行編輯操作!'
                            });
                            return false;
                        };
                        /*註冊事件*/
                        var subWin = Ext.create(main.subWinCompany, {
                            param: {
                                uuid: undefined
                            }
                        });
                        subWin.on('closeEvent', function(obj) {
                            main.down("#grdCompanyQuery").getStore().load();
                        }, main);
                        subWin.show();
                    }
                }, {
                    icon: SYSTEM_URL_ROOT + '/css/images/cloudsync16x16.png',
                    text: '同步公司(主伺服器)',
                    hidden: this.param.showADSync,
                    handler: function() {
                        Ext.getBody().mask("正在處理中…請稍後…");
                        WS.SyncClientAction.SyncCompany(function(obj, jsonObj) {
                            Ext.getBody().unmask();
                            if (jsonObj.result.success) {
                                Ext.MessageBox.show({
                                    title: '同步公司(主伺服器)',
                                    icon: Ext.MessageBox.INFO,
                                    buttons: Ext.Msg.OK,
                                    msg: '已成功完成同步作業。',
                                    fn: function() {
                                        storeCompany.load();
                                    }
                                });
                            } else {
                                Ext.MessageBox.show({
                                    title: '發生異常',
                                    icon: Ext.MessageBox.INFO,
                                    buttons: Ext.Msg.OK,
                                    msg: jsonObj.result.message
                                });
                            };
                        });
                    }
                }]
            }]
        }];
        this.callParent(arguments);
    },
    listeners: {
        afterrender: function(obj, eOpts) {
            this.myStore.company.load({
                scope: this
            });
        }
    }
});
