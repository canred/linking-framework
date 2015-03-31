/*全局變量*/
var WS_MENUQUERYPANEL;
/*WS.CompanyQueryPanel物件類別*/
/*TODO*/
/*
1.Model 要集中                                 [YES]
2.panel 的title要換成icon , title的方式        [YES]
3.add 的icon要換成icon , title的方式           [YES]
4.不可以有 getCmp                              [YES]
5.有一段程式碼不確定 line 69
6. tree 要改變一次顯示                         [YES]
7. 如果menu不是有效要表現出來                  [YES]
8. 新增子節點的功能未完成                      [YES]
9. 子視窗的功能未完成                          [YES]  
10.新增畫面開啟後自動選擇系統                  [YES]  
11.加上一個LoadMask當資料正在讀取時            [NO] 
*/
/*columns 使用default*/
Ext.define('WS.MenuQueryPanel', {
    extend: 'Ext.panel.Panel',
    closeAction: 'destroy',
    subWinMenuWindow: undefined,
    /*語言擴展*/
    lan: {},
    /*參數擴展*/
    param: {
        PARENTUUID: undefined
    },
    /*值擴展*/
    val: {},
    /*物件會用到的Store物件*/
    myStore: {
        tree: undefined,
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
    fnCallBackCloseEvent: function() {
        this.fnQuery(this);
    },
    fnQuery: function(obj) {
        /*obj要是主體*/
        WS.MenuAction.loadTreeRoot(obj.down('#cmbApplication').getValue(), function(data) {
            if (data.UUID != undefined) {
                var store = this.myStore.tree,
                    proxy = store.getProxy();
                proxy.setExtraParam('UUID', data.UUID);
                this.myStore.tree.load({
                    params: {
                        'UUID': data.UUID
                    },
                    callback: function() {
                        this.down('#AppMenuTree').expandAll();
                    },
                    scope: this
                });
            };
        }, obj);
    },
    fnActiveRender: function(value, id, r) {
        var html = "<img src='" + SYSTEM_URL_ROOT;
        return value === "Y" ? html + "/css/custimages/active03.png'>" : html + "/css/custimages/unactive03.png'>";
    },
    fnCheckSubComponent: function() {
        /*要把scope變成SitemapQueryPanel主體*/
        if (Ext.isEmpty(this.subWinMenuWindow)) {
            Ext.MessageBox.show({
                title: '系統提示',
                icon: Ext.MessageBox.WARNING,
                buttons: Ext.Msg.OK,
                msg: '未指定 subWinMenuWindow ,無法進行編輯操作!'
            });
            return false;
        };
        return true;
    },
    fnEditMenu: function(menuUuid) {
        /*要把scope變成SitemapQueryPanel主體*/
        if (!this.fnCheckSubComponent()) {
            return false;
        };
        this.param.PARENTUUID = menuUuid;
        var subWin = Ext.create(this.subWinMenuWindow, {
            subWinProxyPickerWindow: 'WS.ProxyPickerWindow',
            param: {
                uuid: menuUuid,
                applicationHeadUuid: this.down('#cmbApplication').getValue()
            }
        });
        /*註冊事件*/
        subWin.on('closeEvent', this.fnCallBackCloseEvent, this);
        /*設定參數*/
        subWin.show();
    },
    fnAddMenuChild: function(parentMenuUuid) {
        /*要把scope變成SitemapQueryPanel主體*/
        if (!this.fnCheckSubComponent()) {
            return false;
        };
        this.param.PARENTUUID = parentMenuUuid;
        var subWin = Ext.create(this.subWinMenuWindow, {
            subWinProxyPickerWindow: 'WS.ProxyPickerWindow',
            param: {
                uuid: undefined,
                parentUuid: parentMenuUuid,
                applicationHeadUuid: this.down('#cmbApplication').getValue()
            }
        });
        subWin.on('closeEvent', this.fnCallBackCloseEvent, this);
        subWin.show();
    },
    fnRemoveMenu: function(mainPanel, menuUuid) {
        Ext.Msg.show({
            title: '刪除節點操作',
            msg: '確定執行刪除動作?',
            buttons: Ext.Msg.YESNO,
            fn: function(btn) {
                if (btn == "yes") {
                    WS.MenuAction.deleteAppMenu(menuUuid, function(json) {
                        mainPanel.fnQuery(mainPanel);
                    });
                }
            }
        });
    },
    initComponent: function() {
        this.myStore.tree = Ext.create('WS.MenuTreeStore', {});
        if (!this.fnCheckSubComponent()) {
            return false;
        };
        this.items = [{
            xtype: 'panel',
            title: '選單',
            icon: SYSTEM_URL_ROOT + '/css/images/menu16x16.png',
            frame: true,
            minHeight: $(document).height() - 150,
            autoHeight: true,
            autoWidth: true,
            items: [{
                xtype: 'container',
                layout: 'hbox',
                margin: 5,
                items: [{
                    xtype: 'combo',
                    editable: false,
                    fieldLabel: '系 統',
                    store: this.myStore.application,
                    labelWidth: 40,
                    displayField: 'NAME',
                    valueField: 'UUID',
                    itemId: 'cmbApplication',
                    enableKeyEvents: true,
                    listeners: {
                        'change': function(obj, value) {
                            var mainPanel = this.up('panel').up('panel');
                            mainPanel.fnQuery(mainPanel);
                        }
                    }
                }]
            }, {
                xtype: 'button',
                margin: '0 0 0 5',
                text: '新增選單子節點',
                icon: SYSTEM_URL_ROOT + '/css/images/add16x16.png',
                handler: function() {
                    var mainPanel = this.up('panel').up('panel');
                    WS.MenuAction.loadTreeRoot(mainPanel.down('#cmbApplication').getValue(), function(data) {
                        PARENTUUID = data.UUID;
                        if (!this.fnCheckSubComponent()) {
                            return false;
                        };
                        this.param.PARENTUUID = PARENTUUID;
                        var subWin = Ext.create(this.subWinMenuWindow, {
                            subWinProxyPickerWindow: 'WS.ProxyPickerWindow',
                            param: {
                                uuid: undefined,
                                parentUuid: PARENTUUID,
                                applicationHeadUuid: this.down('#cmbApplication').getValue()
                            }
                        });
                        /*註冊事件*/
                        subWin.on('closeEvent', this.fnCallBackCloseEvent, this);
                        /*設定參數*/
                        subWin.show();
                    }, mainPanel);
                }
            }, {
                xtype: 'treepanel',
                fieldLabel: '選單維護',
                itemId: 'AppMenuTree',
                padding: 5,
                border: true,
                autoWidth: true,
                autoHeight: true,
                minHeight: $(document).height() - 230,
                store: this.myStore.tree,
                rootVisible: false,
                columns: {
                    defaults: {
                        align: 'left'
                    },
                    items: [{
                        xtype: 'treecolumn',
                        text: '選單',
                        flex: 2,
                        sortable: false,
                        dataIndex: 'NAME_ZH_TW'
                    }, {
                        text: '啟用',
                        width: 60,
                        dataIndex: 'IS_ACTIVE',
                        align: 'center',
                        sortable: false,
                        hidden: true,
                        renderer: this.fnActiveRender
                    }, {
                        text: '順序',
                        width: 60,
                        dataIndex: 'ORD',
                        align: 'center',
                        sortable: false
                    }, {
                        text: "維護",
                        xtype: 'actioncolumn',
                        dataIndex: 'UUID',
                        align: 'center',
                        sortable: false,
                        width: 100,
                        items: [{
                            tooltip: '*編輯',
                            icon: SYSTEM_URL_ROOT + '/css/images/edit16x16.png',
                            handler: function(grid, rowIndex, colIndex) {
                                var mainPanel = grid.up('panel').up('panel').up('panel'),
                                    uuid = grid.getStore().getAt(rowIndex).data.UUID;
                                mainPanel.fnEditMenu(uuid);
                            }
                        }, {
                            tooltip: '*新增子節點',
                            icon: SYSTEM_URL_ROOT + '/css/images/add16x16.png',
                            handler: function(grid, rowIndex, colIndex) {
                                var mainPanel = grid.up('panel').up('panel').up('panel'),
                                    uuid = grid.getStore().getAt(rowIndex).data.UUID;
                                mainPanel.fnAddMenuChild(uuid);
                            }
                        }, {
                            tooltip: '*刪除',
                            icon: SYSTEM_URL_ROOT + '/css/images/delete16x16.png',
                            margin: '0 0 0 40',
                            handler: function(grid, rowIndex, colIndex) {
                                var mainPanel = grid.up('panel').up('panel').up('panel'),
                                    uuid = grid.getStore().getAt(rowIndex).data.UUID;;
                                mainPanel.fnRemoveMenu(mainPanel, uuid);
                            }
                        }],
                        hideable: false
                    }]
                }
            }]
        }];
        this.callParent(arguments);
    },
    listeners: {
        'afterrender': function(obj, eOpts) {
            obj.myStore.application.load({
                'callback': function(obj) {
                    if (obj.length > 0) {
                        this.down('#cmbApplication').setValue(obj[0].data.UUID);
                    };
                },
                'scope': this
            });
        }
    }
});
