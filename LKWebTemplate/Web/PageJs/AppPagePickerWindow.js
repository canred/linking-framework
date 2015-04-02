/*WS.CompanyQueryPanel物件類別*/
/*TODO*/
/*
1.Model 要集中                                 [YES]
2.panel 的title要換成icon , title的方式        [YES]
3.add 的icon要換成icon , title的方式           [YES]
4.不可以有 getCmp                              [YES]
5.有一段程式碼不確定 line 69
*/
/*columns 使用default*/
Ext.define('WS.AppPagePickerWindow', {
    extend: 'Ext.window.Window',
    title: '挑選網站地圖功能',
    icon: SYSTEM_URL_ROOT + '/css/images/map16x16.png',
    closeAction: 'destroy',
    closable: false,
    param: {
        uuid: undefined,
        applicationHeadUuid: undefined
    },
	modal: true,
    width: 800,
    height: 600,
    layout: 'fit',
    resizable: false,
    draggable: false,
    myStore: {
        apppage: Ext.create('Ext.data.Store', {
            successProperty: 'success',
            model: 'APPPAGE',
            pageSize: 10,
            proxy: {
                type: 'direct',
                api: {
                    read: WS.AppPageAction.loadAppPage
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
                property: 'NAME'
            }]
        })
    },
    initComponent: function() {
        this.items = [Ext.create('Ext.panel.Panel', {
            id: 'Ext.AppPagePicker.Form',
            border: false,
            defaultType: 'textfield',
            buttonAlign: 'center',
            items: [{
                xtype: 'container',
                layout: 'hbox',
                margin: 5,
                defaults: {
                    margin: '0 5 0 0',
                },
                items: [{
                    xtype: 'textfield',
                    fieldLabel: '關鍵字',
                    itemId: 'txtKeywork',
                    labelAlign: 'left',
                    labelWidth: 50,
                    enableKeyEvents: true,
                    listeners: {
                        keyup: function(obj, t) {
                            if (t.button == 12) {
                                this.up('panel').down("#btnQuery").handler();
                            };
                        }
                    }
                }, {
                    xtype: 'button',
                    icon: SYSTEM_URL_ROOT + '/css/custimages/find.png',
                    text: '查詢',
                    width: 80,
                    itemId: 'btnQuery',
                    handler: function() {
                        var mainWin = this.up('window'),
                            proxy = mainWin.myStore.apppage.getProxy(),
                            store = mainWin.myStore.apppage;
                        proxy.setExtraParam('pKeyword', mainWin.down('#txtKeywork').getValue());
                        proxy.setExtraParam('pApplicationHeadUuid', mainWin.param.applicationHeadUuid);
                        store.loadPage(1);
                    }
                }]
            }, {
                xtype: 'gridpanel',
                store: this.myStore.apppage,
                idProperty: 'UUID',
                itemId: 'grdAppPage',
                paramsAsHash: false,
                border: true,
                padding: 5,
                height: 480,
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
                                mainWin.selectedEvent(grid.getStore().getAt(rowIndex).data);
                            }
                        }],
                        sortable: false,
                        hideable: false
                    }, {
                        text: "功能名稱",
                        dataIndex: 'NAME',
                        flex: 1
                    }, {
                        text: "描述",
                        dataIndex: 'DESCRIPTION',
                        flex: 1
                    }, {
                        text: "參數",
                        dataIndex: 'PARAMETER_CLASS',
                        flex: 2
                    }]
                },
                bbar: Ext.create('Ext.toolbar.Paging', {
                    store: this.myStore.apppage,
                    displayInfo: true,
                    displayMsg: '第{0}~{1}資料/共{2}筆',
                    emptyMsg: "無資料顯示"
                })
            }],
            buttons: [{
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
    selectedEvent: function(result) {
        this.fireEvent('selectedEvent', this, result);
        this.down('#grdAppPage').getStore().reload();
    },
    listeners: {
        'show': function() {            
            this.down('#btnQuery').handler(this.down('#btnQuery'));
        },
        'close': function() {
            this.closeEvent();            
        }
    }
});
