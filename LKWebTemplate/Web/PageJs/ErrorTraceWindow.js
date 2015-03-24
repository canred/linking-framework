Ext.define('WS.ErrorTraceWindow', {
    extend: 'Ext.window.Window',
    title: '錯誤訊息清單',
    icon: SYSTEM_URL_ROOT + '/css/images/bug16x16.png',
    closeAction: 'hide',
    width: $(document).width() * .9,
    height: $(document).height() * .9,
    resizable: false,
    draggable: false,
    myStore: {
        errorlog: Ext.create('Ext.data.Store', {
            successProperty: 'success',
            autoLoad: false,
            remoteSort: true,
            model: 'ERROR_LOG',
            pageSize: 5,
            proxy: {
                type: 'direct',
                api: {
                    read: WS.ErrorLogAction.load
                },
                reader: {
                    root: 'data'
                },
                paramsAsHash: true,
                paramOrder: ['is_read', 'page', 'limit', 'sort', 'dir'],
                extraParams: {
                    is_read: 'N'
                },
                simpleSortMode: true
            },
            sorters: [{
                property: 'CREATE_DATE',
                direction: 'DESC'
            }]
        })
    },
    initComponent: function() {
        this.items = [{
            xtype: 'gridpanel',
            store: this.myStore.errorlog,
            padding: 5,
            border: true,
            autoScroll: true,
            tbar: [{
                text: '標記全部已閱讀',
                icon: SYSTEM_URL_ROOT + '/css/images/okA16x16.png',
                style: {
                    'background-color': '#F299A0'
                },
                handler: function() {
                    WS.ErrorLogAction.UpdateAllRead(function(returnJs) {
                        this.close();
                    }, this.up('window'));
                }
            }],
            fbar: [{
                type: 'button',
                text: '關閉',
                handler: function() {
                    this.up('window').close();
                }
            }],
            columns: [{
                text: "操作",
                xtype: 'actioncolumn',
                dataIndex: 'UUID',
                align: 'center',
                width: 60,
                items: [{
                    tooltip: '*標記已讀',
                    icon: SYSTEM_URL_ROOT + '/css/images/ok16x16.png',
                    handler: function(grid, rowIndex, colIndex) {
                        var value = grid.getStore().getAt(rowIndex).data.UUID;
                        WS.ErrorLogAction.UpdateRead(value, function(returnJs) {
                            this.myStore.errorLog.reload();
                        }, this.up('window'));
                    }
                }],
                sortable: false,
                hideable: false
            }, {
                text: "時間",
                dataIndex: 'CREATE_DATE',
                align: 'left',
                width: 150
            }, {
                text: "訊息",
                dataIndex: 'ERROR_MESSAGE',
                align: 'left',
                flex: 1,
                renderer: function(value) {
                    return '<div align="left">' + value + '</div>';
                }
            }, {
                text: "Action",
                dataIndex: 'APPLICATION_NAME',
                align: 'left',
                width: 200
            }, {
                text: "執行者",
                dataIndex: 'C_NAME',
                align: 'left',
                width: 100
            }],
            height: ($(document).height() * .9) - 30,
            bbar: Ext.create('Ext.toolbar.Paging', {
                store: this.myStore.errorlog,
                displayInfo: true,
                displayMsg: '第{0}~{1}資料/共{2}筆',
                emptyMsg: "無資料顯示"
            }),
            listeners: {
                cellclick: function(iView, iCellEl, iColIdx, iRecord, iRowEl, iRowIdx, iEvent) {
                    if (iColIdx != 0) {
                        var colCount = iView.getGridColumns().length;
                        var msg = "";
                        for (var i = 1; i < colCount; i++) {
                            var message = iRecord.data[iView.getGridColumns()[i].dataIndex];
                            var field = iView.getGridColumns()[i].text;
                            if (i != colCount - 1)
                                msg += "[<b>" + field + "</b>] " + message + "<br/>";
                            else
                                msg += "[<b>" + field + "</b>] " + message;
                        };
                        /*暫停偵查動作*/
                        UTIL.trace.param.stop = true;
                        Ext.Msg.show({
                            title: "Row" + iRowIdx,
                            msg: msg,
                            minWidth: 800,
                            minHeight: 250,
                            icon: Ext.MessageBox.WARNING,
                            buttons: Ext.Msg.OK,
                            fn: function() {
                                /*恢復偵查動作*/
                                UTIL.trace.param.stop = false;
                            }
                        });
                    }
                }
            }
        }];
        this.callParent(arguments);
    }
});
