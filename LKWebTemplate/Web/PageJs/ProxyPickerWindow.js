/*columns 使用default*/
Ext.define('WS.ProxyPickerWindow', {
    extend: 'Ext.window.Window',
    title: '挑選資源',
    icon: SYSTEM_URL_ROOT + '/css/images/connector16x16.png',
    closeAction: 'destroy',
    closable: true,
    modal: true,
    param: {
        applicationHeadUuid: undefined,
        menuUuid: undefined,
        parentObject: undefined
    },
    width: 800,
    minHeight: $(window).height() * 0.9,
    y: 10,
    layout: 'fit',
    resizable: false,
    draggable: true,
    myStore: {
        proxy: Ext.create('Ext.data.Store', {
            successProperty: 'success',
            model: 'PROXY',
            pageSize: 9999,
            proxy: {
                type: 'direct',
                api: {
                    read: WS.ProxyAction.loadAppmenuProxyMapUnSelected
                },
                reader: {
                    root: 'data'
                },
                paramsAsHash: true,
                paramOrder: ['pApplicationHeadUuid', 'pAppMenuUuid', 'pKeyword', 'page', 'limit', 'sort', 'dir'],
                extraParams: {
                    pApplicationHeadUuid: '',
                    pAppMenuUuid: '',
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
    initComponent: function() {
        this.items = [Ext.create('Ext.form.Panel', {
            border: false,
            padding: 5,
            buttonAlign: 'center',
            items: [{
                xtype: 'container',
                layout: 'hbox',
                items: [{
                    xtype: 'textfield',
                    fieldLabel: '關鍵字',
                    itemId: 'txtKeyword',
                    labelAlign: 'right',
                    labelWidth: 50,
                    enableKeyEvents: true,
                    listeners: {
                        keyup: function(obj, t) {
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
                        var mainWin = this.up('window');
                        mainWin.myStore.proxy.getProxy().setExtraParam('pKeyword', mainWin.down('#txtKeyword').getValue());
                        mainWin.myStore.proxy.getProxy().setExtraParam('pApplicationHeadUuid', mainWin.param.applicationHeadUuid);
                        mainWin.myStore.proxy.getProxy().setExtraParam('pAppMenuUuid', mainWin.param.menuUuid);
                        mainWin.myStore.proxy.load();
                    }
                }]
            }, {
                xtype: 'gridpanel',
                store: this.myStore.proxy,
                itemId: 'grdProxy',
                border: true,
                padding: 5,
                autoWidth: true,
                columns: {
                    defaults: {
                        align: 'left'
                    },
                    items: [{
                        text: "挑選",
                        xtype: 'actioncolumn',
                        dataIndex: 'UUID',
                        align: 'center',
                        width: 80,
                        items: [{
                            tooltip: '*挑選',
                            icon: SYSTEM_URL_ROOT + '/css/images/add16x16.png',
                            handler: function(grid, rowIndex, colIndex) {
                                var mainWin = grid.up('window');
                                mainWin.selectEvent(grid.getStore().getAt(rowIndex).data);
                            }
                        }],
                        sortable: false,
                        hideable: false
                    }, {
                        text: "Action",
                        dataIndex: 'PROXY_ACTION',
                        flex: 1
                    }, {
                        text: "Method",
                        dataIndex: 'PROXY_METHOD',
                        flex: 1
                    }, {
                        text: "描述",
                        dataIndex: 'DESCRIPTION',
                        flex: 1
                    }]
                },
                autoHeight: true
            }],
            fbar: [{
                type: 'button',
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
    selectEvent: function(result) {
        this.fireEvent('selectEvent', this, result);
    },
    listeners: {
        'show': function() {
            this.down("#btnQuery").handler();
        },
        'close': function() {
            this.closeEvent();
        }
    }
});
