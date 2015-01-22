Ext.Loader.setConfig({
    enabled: true
});
Ext.Loader.setPath('Ext.ux', SYSTEM_ROOT_PATH + '/js/ux');
Ext.require(['*', 'Ext.ux.DataTip']);
Ext.MessageBox.buttonText.yes = "確定";
Ext.MessageBox.buttonText.no = "取消";
var WinProxyPicker = undefined;
Ext.onReady(function() {
    Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION + ".ProxyAction"));
    Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION + ".ApplicationAction"));
    var storeProxy =
        Ext.create('Ext.data.Store', {
            successProperty: 'success',
            model: Ext.define('PROXY', {
                extend: 'Ext.data.Model',
                fields: [
                    'UUID',
                    'PROXY_ACTION',
                    'PROXY_METHOD',
                    'DESCRIPTION',
                    'PROXY_TYPE',
                    'NEED_REDIRECT',
                    'REDIRECT_PROXY_ACTION',
                    'REDIRECT_PROXY_METHOD',
                    'APPLICATION_HEAD_UUID',
                    'REDIRECT_SRC'
                ]
            }),
            pageSize: 1000,
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
        });


    Ext.define('ProxyPicker', {
        extend: 'Ext.window.Window',
        title: '挑選資源功能',
        closeAction: 'hide',
        uuid: undefined,
        id: 'ExtProxyPicker',
        applicationHeadUuid: undefined,
        menuUuid: undefined,
        width: 800,
        height: 550,
        layout: 'fit',
        resizable: false,
        draggable: true,
        initComponent: function() {
            var me = this;
            me.items = [Ext.create('Ext.form.Panel', {
                layout: {
                    type: 'form',
                    align: 'stretch'
                },
                id: 'Ext.ProxyPicker.Form',
                paramOrder: ['pUuid'],
                border: true,
                bodyPadding: 5,
                defaultType: 'textfield',
                buttonAlign: 'center',
                items: [{
                        xtype: 'container',
                        layout: 'hbox',
                        items: [{
                            xtype: 'textfield',
                            fieldLabel: '關鍵字',
                            id: 'Ext.ProxyPicker.keyWork',
                            labelAlign: 'right',
                            enableKeyEvents: true,
                            listeners: {
                                keyup: function(obj, t) {
                                    if (t.button == 12) {
                                        this.up('panel').down("#btnQuery").handler();
                                    }
                                }
                            }
                        }, {
                            xtype: 'button',
                            text: '<img src="' + SYSTEM_URL_ROOT + '/css/custimages/search.gif" style="height:15px;vertical-align:middle;margin-top:-2px;margin-right:5px;">查詢',
                            margin: '0 5 0 5',
                            itemId: 'btnQuery',
                            handler: function() {
                                storeProxy.getProxy().setExtraParam('pKeyword', Ext.getCmp('Ext.ProxyPicker.keyWork').getValue());
                                storeProxy.getProxy().setExtraParam('pApplicationHeadUuid', Ext.getCmp('ExtProxyPicker').applicationHeadUuid);
                                storeProxy.getProxy().setExtraParam('pAppMenuUuid', Ext.getCmp('ExtProxyPicker').menuUuid);
                                storeProxy.load();
                            }
                        }]
                    }, {
                        xtype: 'gridpanel',
                        store: storeProxy,
                        idProperty: 'UUID',
                        paramsAsHash: false,
                        border: true,
                        padding: 5,
                        columns: [{
                            text: '挑選',
                            dataIndex: 'UUID',
                            align: 'center',
                            renderer: function(value, m, r) {
                                var id = Ext.id();
                                Ext.defer(function() {
                                    Ext.widget('button', {
                                        renderTo: id,
                                        text: '<img src="' + SYSTEM_URL_ROOT + '/css/custimages/mouse_select_left.gif" style="height:16px;vertical-align:middle;margin-right:5px;margin-top:-2px;">選擇',
                                        record: r,
                                        width: 75,
                                        handler: function() {
                                            Ext.getCmp('ExtProxyPicker').selectedEvent(this.record.raw);
                                        }
                                    });
                                }, 50);
                                return Ext.String.format('<div id="{0}"></div>', id);
                            },
                            sortable: false,
                            hideable: false
                        }, {
                            text: "Action",
                            dataIndex: 'PROXY_ACTION',
                            align: 'left',
                            flex: 1
                        }, {
                            text: "Method",
                            dataIndex: 'PROXY_METHOD',
                            align: 'left',
                            flex: 1
                        }, {
                            text: "描述",
                            dataIndex: 'DESCRIPTION',
                            align: 'left',
                            flex: 1
                        }],
                        anchor: '95%',
                        height: 420,
                        bbar: Ext.create('Ext.toolbar.Paging', {
                            store: storeProxy,
                            displayInfo: true,
                            displayMsg: '第{0}~{1}資料/共{2}筆',
                            emptyMsg: "無資料顯示"
                        }),
                        tbarCfg: {
                            buttonAlign: 'right'
                        }
                    }


                ],
                fbar: [{
                    type: 'button',
                    text: '<img src="' + SYSTEM_URL_ROOT + '/css/images/leave.png" style="width:20px;height:16px;vertical-align:middle;margin-right:5px;"/>' + '關閉',
                    handler: function() {
                        Ext.getCmp('ExtProxyPicker').hide();
                    }
                }]
            })]
            me.callParent(arguments);
        },
        closeEvent: function() {
            this.fireEvent('closeEvent', this);
        },
        selectedEvent: function(result) {
            this.fireEvent('selectedEvent', this, result);
        },
        listeners: {
            'show': function() {
                /*:::畫面開啟後載入資料:::*/
                Ext.getBody().mask();
                /*this.uuid 是 undefined 的話；表示執行新增的動作*/
                Ext.getCmp('Ext.ProxyPicker.Form').getForm().reset();
                this.down("#btnQuery").handler();
            },
            'hide': function() {
                Ext.getBody().unmask();
                this.closeEvent();
            }
        }
    });
});
