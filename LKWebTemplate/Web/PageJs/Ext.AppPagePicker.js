Ext.onReady(function() {
    Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION + ".AppPageAction"));
    Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION + ".ApplicationAction"));
    var storeAppPage =
        Ext.create('Ext.data.Store', {
            successProperty: 'success',
            model: Ext.define('APPPAGE', {
                extend: 'Ext.data.Model',
                fields: [
                    'UUID',
                    'IS_ACTIVE',
                    'CREATE_DATE',
                    'CREATE_USER',
                    'UPDATE_DATE',
                    'UPDATE_USER',
                    'ID',
                    'NAME',
                    'DESCRIPTION',
                    'URL',
                    'PARAMETER_CLASS',
                    'APPLICATION_HEAD_UUID',
                    'P_MODE'
                ]
            }),
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
        });
    Ext.define('AppPagePicker', {
        extend: 'Ext.window.Window',
        title: '挑選網站地圖功能',
        closeAction: 'hide',
        uuid: undefined,
        id: 'ExtAppPagePicker',
        applicationHeadUuid: undefined,
        width: 800,
        height: 600,
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
                id: 'Ext.AppPagePicker.Form',
                paramOrder: ['pUuid'],
                border: false,
                defaultType: 'textfield',
                buttonAlign: 'center',
                items: [{
                        xtype: 'container',
                        layout: 'hbox',
                        items: [{
                            xtype: 'textfield',
                            fieldLabel: '關鍵字',
                            margin: '4 5 0 0',
                            id: 'Ext.AppPagePicker.keyWork',
                            labelAlign: 'left',
                            labelWidth: 50,
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
                            text: '<img src="' + SYSTEM_URL_ROOT + '/css/custimages/search.gif" style="height:15px;vertical-align:middle;">查詢',
                            width: 70,
                            itemId: 'btnQuery',
                            handler: function() {
                                storeAppPage.getProxy().setExtraParam('pKeyword', Ext.getCmp('Ext.AppPagePicker.keyWork').getValue());
                                storeAppPage.getProxy().setExtraParam('pApplicationHeadUuid', Ext.getCmp('ExtAppPagePicker').applicationHeadUuid);
                                storeAppPage.load();
                            }
                        }]
                    }, {
                        xtype: 'gridpanel',
                        store: storeAppPage,
                        idProperty: 'UUID',
                        paramsAsHash: false,
                        border: true,
                        padding: 5,
                        height: 480,
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
                                            Ext.getCmp('ExtAppPagePicker').selectedEvent(this.record.data);
                                        }
                                    });
                                }, 50);
                                return Ext.String.format('<div id="{0}"></div>', id);
                            },
                            sortable: false,
                            hideable: false
                        }, {
                            text: "功能名稱",
                            dataIndex: 'NAME',
                            align: 'left',
                            flex: 1
                        }, {
                            text: "描述",
                            dataIndex: 'DESCRIPTION',
                            align: 'left',
                            flex: 1
                        }, {
                            text: "參數",
                            dataIndex: 'PARAMETER_CLASS',
                            align: 'left',
                            flex: 2
                        }],
                        bbar: Ext.create('Ext.toolbar.Paging', {
                            store: storeAppPage,
                            displayInfo: true,
                            displayMsg: '第{0}~{1}資料/共{2}筆',
                            emptyMsg: "無資料顯示"
                        }),
                        listeners: {
                            'beforerender': function() {}
                        },
                        tbarCfg: {
                            buttonAlign: 'right'
                        }
                    }
                ],
                fbar: [{
                    type: 'button',
                    text: '<img src="' + SYSTEM_URL_ROOT + '/css/images/leave.png" style="width:20px;height:16px;vertical-align:middle;margin-right:5px;"/>' + '關閉',
                    handler: function() {
                        Ext.getCmp('ExtAppPagePicker').hide();
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
                Ext.getBody().mask();
                if (this.uuid != undefined) {
                    Ext.getCmp('Ext.AppPagePicker.Form.application_head_uuid').setReadOnly(true);
                    Ext.getCmp('Ext.AppPagePicker.Form').getForm().load({
                        params: {
                            'pUuid': this.uuid
                        },
                        success: function(response, jsonObj, b) {},
                        failure: function(response, a, b) {
                            r = Ext.decode(response.responseText);
                            alert('err:' + r);
                        }
                    });
                } else {
                    Ext.getCmp('Ext.AppPagePicker.Form').getForm().reset();
                }
            },
            'hide': function() {
                Ext.getBody().unmask();
                this.closeEvent();
            }
        }
    });
});
