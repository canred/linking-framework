var SCHEDULEPANEL;

Ext.define('WS.SchedulePanel', {
    extend: 'Ext.panel.Panel',
    closeAction: 'destroy',
    icon: SYSTEM_URL_ROOT + '/css/images/schedule16x16.png',
    title: '排程工作',
    frame: true,
    height: $(document).height() - 150,
    myStore: {
        schedule: Ext.create('Ext.data.Store', {
            successProperty: 'success',
            autoLoad: false,
            model: 'SCHEDULE',
            pageSize: 10,
            remoteSort: true,
            proxy: {
                type: 'direct',
                api: {
                    read: WS.ScheduleAction.loadSchedule
                },
                reader: {
                    root: 'data'
                },
                paramsAsHash: true,
                paramOrder: ['keyword', 'page', 'limit', 'sort', 'dir'],
                extraParams: {
                    keyword: ''
                },
                simpleSortMode: true,
                listeners: {
                    exception: function(proxy, response, operation) {
                        if (!response.result.success) {
                            Ext.MessageBox.show({
                                title: 'Warning',
                                icon: Ext.MessageBox.WARNING,
                                buttons: Ext.Msg.OK,
                                msg: response.result.message
                            });
                        }
                    },
                    beforeload: function() {
                        alert('beforeload proxy');
                    }
                }
            },
            sorters: [{
                property: 'SCHEDULE_NAME',
                direction: 'ASC'
            }]
        })
    },
    fnIsActiveRender: function(value, id, r) {
        if (value == "Y") {
            return "<img src='" + SYSTEM_URL_ROOT + "/css/custimages/active.gif' style='height:14px;vertical-align:middle'>";
        } else if (value == "N") {
            return "<img src='" + SYSTEM_URL_ROOT + "/css/custimages/unactive.gif' style='height:14px;vertical-align:middle'>";
        };
    },
    fnIsStatusRender: function(value, id, r) {
        if (value == "OK") {
            return "<img src='" + SYSTEM_URL_ROOT + "/css/custimages/active.gif' style='height:14px;vertical-align:middle'>";
        } else if (value == "N") {
            return "<img src='" + SYSTEM_URL_ROOT + "/css/custimages/unactive.gif' style='height:14px;vertical-align:middle'>";
        };
    },

    initComponent: function() {
        this.items = [{
            xtype: 'container',
            layout: 'hbox',
            defaults: {
                margin: '5 5 0 5'
            },
            items: [{
                xtype: 'textfield',
                itemId: 'txtSearch',
                fieldLabel: '關鍵字',
                labelWidth: 50,
                labelAlign: 'right',
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
                text: '查詢',
                icon: SYSTEM_URL_ROOT + '/css/custimages/find.png',
                itemId: 'btnQuery',
                handler: function() {
                    var mainPanel = this.up('panel');
                    mainPanel.myStore.schedule.getProxy().setExtraParam('keyword', mainPanel.down('#txtSearch').getValue());
                    mainPanel.myStore.schedule.load();
                }
            }, {
                xtype: 'button',
                text: '清除',
                icon: SYSTEM_URL_ROOT + '/css/custimages/clear.gif',
                handler: function() {
                    var mainPanel = this.up('panel');
                    mainPanel.down('#txtSearch').setValue('');
                }
            }]
        }, {
            xtype: 'gridpanel',
            store: this.myStore.schedule,
            padding: 5,
            height: $(document).height() - 220,
            border: true,
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
                        icon: SYSTEM_URL_ROOT + '/css/custImages/edit.gif',
                        handler: function(grid, rowIndex, colIndex) {
                            var subWin = Ext.create('WS.ScheduleWindow', {
                                param: {
                                    uuid: grid.getStore().getAt(rowIndex).data.UUID
                                }
                            });
                            subWin.show();
                        }
                    }],
                    sortable: false,
                    hideable: false
                }, {
                    header: "名稱",
                    dataIndex: 'SCHEDULE_NAME',
                    width: 200
                }, {
                    header: "週期性",
                    dataIndex: 'IS_CYCLE',
                    name: 'IS_CYCLE',
                    width: 80,
                    align: 'center',
                    renderer: this.fnIsActiveRender
                }, {
                    header: "執行任務",
                    dataIndex: 'RUN_URL',
                    flex: 1
                }, {
                    header: '參數',
                    dataIndex: 'RUN_URL_PARAMETER',
                    width: 100
                }, {
                    header: "有效性",
                    dataIndex: 'IS_ACTIVE',
                    width: 80,
                    align: 'center',
                    renderer: this.fnIsActiveRender
                }, {
                    header: "上次執行時間",
                    dataIndex: 'LAST_RUN_TIME',
                    width: 150
                }, {
                    header: "執行狀態",
                    dataIndex: 'LAST_RUN_STATUS',
                    width: 80,
                    align: 'center',
                    renderer: this.fnIsStatusRender
                }]
            },
            bbar: Ext.create('Ext.toolbar.Paging', {
                store: this.myStore.schedule,
                displayInfo: true,
                displayMsg: '第{0}~{1}資料/共{2}筆',
                emptyMsg: "無資料顯示"
            }),
            tbar: [{
                text: '<img src="' + SYSTEM_URL_ROOT + '/css/custimages/add.gif" style="height:12px;vertical-align:middle;margin-top:-2px;margin-right:5px;">新增排程工作',
                handler: function() {
                    var subWin = Ext.create('WS.ScheduleWindow', {
                        param: {
                            uuid: undefined
                        }
                    });
                    subWin.show();
                }
            }]
        }];
        this.listeners = {
            'afterrender': function() {
                this.myStore.schedule.load();
            },
            scope: this
        };
        this.callParent(arguments);
    }
});
