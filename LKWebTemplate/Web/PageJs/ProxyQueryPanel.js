/*全局變量*/
var WS_PROXYQUERYPANEL;
/*WS.CompanyQueryPanel物件類別*/
/*TODO*/
/*
1.Model 要集中                                 [NO]
2.panel 的title要換成icon , title的方式        [YES]
3.add 的icon要換成icon , title的方式           [YES]
*/
/*columns 使用default*/
Ext.define('WS.ProxyQueryPanel', {
    extend: 'Ext.panel.Panel',
    closeAction: 'destroy',
    subWinProxy: undefined,
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
        application: Ext.create('Ext.data.Store', {
            successProperty: 'success',
            autoLoad: true,
            /*:::Table設定:::*/
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
        }),
        proxy: Ext.create('Ext.data.Store', {
            successProperty: 'success',
            model: 'PROXY',
            pageSize: 10,
            proxy: {
                type: 'direct',
                api: {
                    read: WS.ProxyAction.loadProxy
                },
                reader: {
                    root: 'data'
                },
                paramsAsHash: true,
                paramOrder: ['pApplicationHeadUuid', 'pKeyword', 'page', 'limit', 'sort', 'dir'],
                extraParams: {
                    pApplicationHeadUuid: '',
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
                property: 'PROXY_ACTION'
            }]
        })
    },
    fnActiveRender: function isActiveRenderer(value, id, r) {
        var html = "<img src='" + SYSTEM_URL_ROOT;
        return value === "Y" ? html + "/css/custimages/active03.png'>" : html + "/css/custimages/unactive03.png'>";
    },
    initComponent: function() {
        if (Ext.isEmpty(this.subWinProxy)) {
            Ext.MessageBox.show({
                title: '系統提示',
                icon: Ext.MessageBox.WARNING,
                buttons: Ext.Msg.OK,
                msg: '未實現 subWinProxy 物件,無法進行編輯操作!'
            });
            return false;
        };
        
        this.items = [{
            xtype: 'panel',
            title: '資源',
            icon: SYSTEM_URL_ROOT + '/css/images/proxy16x16.png',
            frame: true,
            padding: 5,
            height: $(document).height() - 150,
            items: [{
                xtype: 'container',
                layout: 'hbox',
                margin: 5,
                items: [{
                    xtype: 'combo',
                    editable: false,
                    fieldLabel: '系統',
                    labelWidth: 40,
                    store: this.myStore.application,
                    enableKeyEvents: true,
                    displayField: 'NAME',
                    valueField: 'UUID',
                    itemId: 'cmbApplication',
                    listeners: {
                        keyup: function(e, t, eOpts) {
                            var keyCode = t.parentEvent.keyCode;
                            if (keyCode == Ext.event.Event.ENTER) {
                                this.up('panel').down("#btnQuery").handler();
                            };
                        }
                    }
                }, {
                    xtype: 'textfield',
                    itemId: 'txtSearch',
                    fieldLabel: '關鍵字',
                    margin: '0 0 0 20',
                    labelWidth: 50,
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
                    margin: '0 0 0 20',
                    icon: SYSTEM_URL_ROOT + '/css/custimages/find.png',
                    width: 80,
                    itemId: 'btnQuery',
                    handler: function() {
                        var mainPanel = this.up('panel').up('panel'),
                            store = mainPanel.myStore.proxy,
                            proxy = store.getProxy();
                        proxy.setExtraParam('pKeyword', mainPanel.down('#txtSearch').getValue());
                        proxy.setExtraParam('pApplicationHeadUuid', mainPanel.down('#cmbApplication').getValue());
                        store.loadPage(1);
                    }
                }, {
                    xtype: 'button',
                    icon: SYSTEM_URL_ROOT + '/css/custimages/clear.png',
                    text: '清除',
                    width: 80,
                    margin: '0 0 0 20',
                    handler: function() {
                        var mainPanel = this.up('panel').up('panel');
                        mainPanel.down('#txtSearch').setValue('');
                    }
                }]
            }, {
                xtype: 'gridpanel',
                store: this.myStore.proxy,
                border: true,
                paramOrder: ['NAME'],
                idProperty: 'UUID',
                paramsAsHash: false,
                padding: 5,
                columns: [{
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
                            if (!main.subWinProxy) {
                                Ext.MessageBox.show({
                                    title: '系統訊息',
                                    icon: Ext.MessageBox.INFO,
                                    buttons: Ext.Msg.OK,
                                    msg: '未實現 subWinProxy 物件,無法進行編輯操作!'
                                });
                                return false;
                            };
                            var subWin = Ext.create(main.subWinProxy, {
                                param: {
                                    uuid: grid.getStore().getAt(rowIndex).data.UUID
                                }
                            });
                            subWin.on('closeEvent', main.fnCallBackReloadGrid, main);
                            main.subWinProxy.show();
                        }
                    }],
                    sortable: false,
                    hideable: false
                }, {
                    header: "Action",
                    dataIndex: 'PROXY_ACTION',
                    align: 'left',
                    flex: 2
                }, {
                    header: "Method",
                    dataIndex: 'PROXY_METHOD',
                    align: 'left',
                    flex: 2
                }, {
                    header: "功能描述",
                    align: 'left',
                    dataIndex: 'DESCRIPTION',
                    flex: 2,
                    renderer: function(value) {
                        return '<div align="left">' + value + '</div>';
                    }
                }, {
                    header: "方式",
                    align: 'center',
                    dataIndex: 'PROXY_TYPE',
                    flex: 1,
                    renderer: function(value) {
                        return '<div align="left">' + value + '</div>';
                    }
                }, {
                    header: '跨服務',
                    dataIndex: 'NEED_REDIRECT',
                    align: 'center',
                    flex: 1,
                    renderer: this.fnActiveRender
                }, {
                    header: "SRC(跨服務)",
                    dataIndex: 'REDIRECT_SRC',
                    align: 'left',
                    flex: 1,
                    hidden: true
                }, {
                    header: "Action(跨服務)",
                    dataIndex: 'REDIRECT_PROXY_ACTION',
                    align: 'left',
                    flex: 1,
                    hidden: true
                }, {
                    header: "Method(跨服務)",
                    dataIndex: 'REDIRECT_PROXY_METHOD',
                    align: 'left',
                    flex: 1,
                    hidden: true
                }],
                height: $(document).height() - 240,
                tbarCfg: {
                    buttonAlign: 'right'
                },
                bbar: Ext.create('Ext.toolbar.Paging', {
                    store: this.myStore.proxy,
                    displayInfo: true,
                    displayMsg: '第{0}~{1}資料/共{2}筆',
                    emptyMsg: "無資料顯示"
                }),
                tbar: [{
                    icon: SYSTEM_URL_ROOT + '/css/images/add16x16.png',
                    text: '新增',
                    handler: function() {
                        if (myForm == undefined) {
                            myForm = Ext.create('ProxyForm', {});
                            myForm.on('closeEvent', function(obj) {
                                storeProxy.load();
                            });
                        };
                        myForm.uuid = undefined;
                        myForm.show();
                    }
                }]
            }]
        }];
        this.callParent(arguments);
    },
    listeners:{
        afterrender:function(obj,eOpts){
            this.myStore.application.load({
                callback : function(obj) {
                    if(obj.length>0){
                        this.down('#cmbApplication').setValue(obj[0].data.UUID);
                    };
                    this.down('#btnQuery').handler(this.down('#btnQuery'));
                },
                scope:this
            });
        }
    }
});
