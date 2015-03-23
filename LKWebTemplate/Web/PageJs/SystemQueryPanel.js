/*全局變量*/
var WS_SYSTEMQUERYPANEL;
/*WS.CompanyQueryPanel物件類別*/
/*TODO*/
/*
1.Model 要集中                                 [YES]
2.panel 的title要換成icon , title的方式        [YES]
3.add 的icon要換成icon , title的方式           [YES]
4.不可以有 getCmp                              [YES]
*/
/*columns 使用default*/
Ext.define('WS.SystemQueryPanel', {
    extend: 'Ext.panel.Panel',
    closeAction: 'destroy',
    closable: false,
    subWinApplication: undefined,
    /*語言擴展*/
    lan: {},
    /*參數擴展*/
    param: {},
    /*值擴展*/
    val: {},
    /*物件會用到的Store物件*/
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
    fnActiveRender: function(value, id, r) {
        var html = "<img src='" + SYSTEM_URL_ROOT;
        return value === "Y" ? html + "/css/custimages/active03.png'>" : html + "/css/custimages/unactive03.png'>";
    },
    initComponent: function() {
        if (Ext.isEmpty(this.subWinApplication) === true) {
            Ext.MessageBox.show({
                title: '系統提示',
                icon: Ext.MessageBox.WARNING,
                buttons: Ext.Msg.OK,
                msg: '物件初始化需要 subWinApplication 子物件'
            });
            return false;
        };
        this.items = [{
            xtype: 'panel',
            title: '系統維護',
            icon: SYSTEM_URL_ROOT + '/css/images/application16x16.png',
            frame: true,
            height: $(document).height() - 150,
            items: [{
                xtype: 'container',
                layout: 'hbox',
                margin: 5,
                defaults: {
                    labelAlign: 'right',
                    margin: '0 5 0 0',
                },
                items: [{
                    xtype: 'textfield',
                    itemId: 'txt_search',
                    labelWidth: 50,
                    fieldLabel: '關鍵字',
                    enableKeyEvents: true,
                    listeners: {
                        keyup: function(e, t, eOpts) {
                            var keyCode = t.parentEvent.keyCode;
                            if (keyCode == Ext.event.Event.ENTER) {
                                this.up('panel').down("#btnQuery").handler();
                            };
                        }
                    }
                }, {
                    xtype: 'button',
                    text: '查詢',
                    icon: SYSTEM_URL_ROOT + '/css/custimages/find.png',
                    width: 80,
                    itemId: 'btnQuery',
                    handler: function() {
                        var store = this.up('panel').up('panel').myStore.application;
                        var _txtSearch = this.up('panel').up('panel').down("#txt_search").getValue();
                        store.getProxy().setExtraParam('pKeyword', _txtSearch);
                        store.loadPage(1);
                    }
                }, {
                    xtype: 'button',
                    icon: SYSTEM_URL_ROOT + '/css/custimages/clear.png',
                    text: '清除',
                    width: 80,
                    handler: function() {
                        this.up('panel').up('panel').down("#txt_search").setValue('');
                    }
                }]
            }, {
                xtype: 'gridpanel',
                store: this.myStore.application,
                padding: 5,
                border: true,
                height: $(document).height() - 225,
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
                                var main = grid.up('panel').up('panel').up('panel'),
                                    subWin = Ext.create(main.subWinApplication, {
                                        param: {
                                            uuid: grid.getStore().getAt(rowIndex).data.UUID
                                        }
                                    });
                                subWin.on('closeEvent', function(obj) {
                                    main.myStore.application.load();
                                });
                                subWin.show();
                            }
                        }],
                        sortable: false,
                        hideable: false
                    }, {
                        header: "名稱",
                        dataIndex: 'NAME',
                        flex: 1
                    }, {
                        header: "描述",
                        align: 'left',
                        dataIndex: 'DESCRIPTION',
                        renderer: function(value) {
                            return '<div align="left">' + value + '</div>';
                        }
                    }, {
                        header: "ID代碼",
                        dataIndex: 'ID',
                        align: 'left',
                        hidden: true
                    }, {

                        header: "網址",
                        dataIndex: 'WEB_SITE',
                        flex: 1,
                        renderer: function(value) {
                            return '<div align="left">' + value + '</div>';
                        }
                    }, {
                        header: '啟用',
                        dataIndex: 'IS_ACTIVE',
                        align: 'center',
                        width:60,
                        renderer: this.fnActiveRender
                    }]
                },
                tbarCfg: {
                    buttonAlign: 'right'
                },
                bbar: Ext.create('Ext.toolbar.Paging', {
                    store: this.myStore.application,
                    displayInfo: true,
                    displayMsg: '第{0}~{1}資料/共{2}筆',
                    emptyMsg: "無資料顯示"
                }),
                tbar: [{
                    text: '新增',
                    icon: SYSTEM_URL_ROOT + '/css/images/add16x16.png',
                    handler: function() {
                        var mainPanel = this.up('panel').up('panel').up('panel'),
                            subWin = Ext.create(mainPanel.subWinApplication, {
                                param: {
                                    uuid: undefined
                                }
                            });
                        subWin.on('closeEvent', function(obj) {
                            mainPanel.myStore.application.load();
                        });
                        subWin.show();
                    }
                }]
            }]
        }];
        this.callParent(arguments);
    },
    listeners: {
        afterrender: function(obj, eOpts) {
            this.myStore.application.load({
                scope: this
            })
        }
    }
});
