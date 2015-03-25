//TODO
//StoreManager 要換
/*columns 使用default*/
Ext.define('WS.MenuWindow', {
    extend: 'Ext.window.Window',
    title: '選單維護',
    subWinProxyPickerWindow: undefined,
    closable: false,
    icon: SYSTEM_URL_ROOT + '/css/images/menu16x16.png',
    closeAction: 'destroy',
    border: true,
    param: {
        uuid: undefined,
        applicationHeadUuid: undefined,
        parentUuid: undefined
    },
    win: {
        proxypicker: undefined
    },
    myStore: {
        sitemap: Ext.create('Ext.data.Store', {
            extend: 'Ext.data.Store',
            autoLoad: false,
            model: 'SITEMAP',
            pageSize: 999,
            proxy: {
                type: 'direct',
                api: {
                    read: WS.SiteMapAction.loadThisApplication
                },
                reader: {
                    root: 'data'
                },
                paramsAsHash: false,
                paramOrder: ['pApplicationHeadUuid', 'pIsActive', 'page', 'limit', 'sort', 'dir'],
                extraParams: {
                    pApplicationHeadUuid: '',
                    pIsActive: 'Y'
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
            listeners: {
                load: function() {
                    this.insert(0, new this.model({
                        'NAME': 'noData',
                        'UUID': ''
                    }));
                }
            },
            remoteSort: true,
            sorters: [{
                property: 'UUID',
                direction: 'ASC'
            }]
        }),
        menu: Ext.create('Ext.data.Store', {
            extend: 'Ext.data.Store',
            autoLoad: false,
            model: 'MENU',
            pageSize: 99999,
            proxy: {
                type: 'direct',
                api: {
                    read: WS.MenuAction.loadThisApplicationMenu
                },
                reader: {
                    root: 'data'
                },
                paramsAsHash: false,
                paramOrder: ['pApplicationHeadUuid', 'page', 'limit', 'sort', 'dir'],
                extraParams: {
                    pApplicationHeadUuid: ''
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
                property: 'NAME_ZH_TW',
                direction: 'ASC'
            }]
        }),
        vappmenuproxymap: Ext.create('Ext.data.Store', {
            extend: 'Ext.data.Store',
            storeId: 'storevappmenuproxymap',
            autoLoad: false,
            remoteSort: true,
            model: 'V_APPMENU_PROXY_MAP',
            pageSize: 5,
            proxy: {
                type: 'direct',
                api: {
                    read: WS.ProxyAction.loadVAppmenuProxyMap
                },
                reader: {
                    root: 'data'
                },
                paramsAsHash: true,
                paramOrder: ['pApplicationHeadUuid', 'pAppmenuUuid', 'pKeyWord', 'page', 'limit', 'sort', 'dir'],
                extraParams: {
                    'pApplicationHeadUuid': '',
                    'pAppmenuUuid': '',
                    'pKeyWord': ''
                },
                simpleSortMode: true,
                listeners: {
                    exception: function(proxy, response, operation) {
                        Ext.MessageBox.show({
                            title: 'REMOTE EXCEPTON A',
                            msg: operation.getError(),
                            icon: Ext.MessageBox.ERROR,
                            buttons: Ext.Msg.OK
                        });
                    }
                }
            },
            sorters: [{
                property: 'PROXY_ACTION',
                direction: 'ASC'
            }]
        })
    },
    is_operational_boundary: undefined,
    width: 800,
    autoHeight: true,
    maxHeight: $(window).height() * 0.9,
    overflowY: 'auto',
    border: false,
    resizable: false,
    draggable: false,
    autoFitErrors: true,
    fnCallBackProxyPickerClose: function() {
        this.down('#gridProxy').getStore().reload();
    },
    fnCallBackProxyPickerSelect: function(obj, record) {
        var appUuid = this.down('#APPLICATION_HEAD_UUID').getValue(),
            menuUuid = this.down('#UUID').getValue();
        WS.ProxyAction.addAppmenuProxyMap(appUuid, menuUuid, record.UUID, function(obj, jsonObj2) {
            if (jsonObj2.result.success) {
                this.win.proxypicker.close();
                this.myStore.vappmenuproxymap.reload();
            } else {
                Ext.MessageBox.show({
                    title: 'Warning',
                    icon: Ext.MessageBox.INFO,
                    buttons: Ext.Msg.OK,
                    msg: jsonObj2.result.message
                });
            }
        }, this);
    },
    fnCheckSubComponent: function() {
        /*要把scope變成SitemapQueryPanel主體*/
        if (Ext.isEmpty(this.subWinProxyPickerWindow)) {
            Ext.MessageBox.show({
                title: '系統提示',
                icon: Ext.MessageBox.WARNING,
                buttons: Ext.Msg.OK,
                msg: '未指定 subWinProxyPickerWindow 物件,無法進行編輯操作!'
            });
            return false;
        };
        return true;
    },
    initComponent: function() {
        if (!this.fnCheckSubComponent()) {
            return false;
        };
        this.items = [Ext.create('Ext.form.Panel', {
            layout: 'anchor',
            api: {
                load: WS.MenuAction.info,
                submit: WS.MenuAction.submit,
                destroy: WS.MenuAction.destroyByUuid
            },
            itemId: 'AppMenuForm',
            paramOrder: ['pUuid'],
            border: false,
            defaultType: 'textfield',
            buttonAlign: 'center',
            autoScroll: false,
            defaults: {
                margin: '5 0 0 0',
                anchor: '-10 0'
            },
            items: [{
                fieldLabel: '代碼',
                allowBlank: false,
                maxLength: 33,
                name: 'ID',
                labelAlign: 'right'
            }, {
                fieldLabel: '排序',
                labelAlign: 'right',
                allowBlank: false,
                maxLength: 33,
                name: 'ORD',
                xtype: 'numberfield',
                minValue: 0
            }, {
                fieldLabel: '繁中名稱',
                labelAlign: 'right',
                name: 'NAME_ZH_TW',
                allowBlank: false,
                maxLength: 128
            }, {
                fieldLabel: '簡中名稱',
                labelAlign: 'right',
                name: 'NAME_ZH_CN',
                allowBlank: false,
                maxLength: 128
            }, {
                fieldLabel: '英文名稱',
                labelAlign: 'right',
                name: 'NAME_EN_US',
                allowBlank: false,
                maxLength: 128
            }, {
                fieldLabel: '參數',
                labelAlign: 'right',
                name: 'PARAMETER_CLASS',
                allowBlank: true,
                maxLength: 128
            }, {
                fieldLabel: 'Icon路徑',
                labelAlign: 'right',
                name: 'IMAGE',
                allowBlank: true,
                maxLength: 128
            }, {
                xtype: 'fieldcontainer',
                fieldLabel: '行為模式',
                labelAlign: 'right',
                layout: 'hbox',
                defaults: {
                    margins: '0 10 0 0'
                },
                defaultType: 'radiofield',
                items: [{
                    boxLabel: 'Empty',
                    inputValue: '',
                    name: 'ACTION_MODE',
                    checked: true
                }, {
                    boxLabel: 'Edit',
                    inputValue: 'Edit',
                    name: 'ACTION_MODE',
                    margin: '0 0 0 60'
                }, {
                    boxLabel: 'View',
                    inputValue: 'View',
                    name: 'ACTION_MODE',
                    margin: '0 0 0 60'
                }, ]
            }, {
                fieldLabel: '入口網頁',
                xtype: 'combobox',
                labelAlign: 'right',
                displayField: 'NAME',
                valueField: 'UUID',
                name: 'SITEMAP_UUID',
                editable: false,
                hidden: false,
                store: this.myStore.sitemap
            }, {
                fieldLabel: '父選單',
                labelAlign: 'right',
                xtype: 'combobox',
                itemId: 'APPMENU_UUID',
                displayField: 'NAME_ZH_TW',
                valueField: 'UUID',
                name: 'APPMENU_UUID',
                editable: false,
                hidden: false,
                store: this.myStore.menu,
                value: ' '
            }, {
                xtype: 'fieldcontainer',
                fieldLabel: '預設頁面',
                layout: 'hbox',
                labelAlign: 'right',
                defaultType: 'radiofield',
                items: [{
                    boxLabel: '是',
                    inputValue: 'Y',
                    name: 'IS_DEFAULT_PAGE'
                }, {
                    boxLabel: '否',
                    inputValue: 'N',
                    name: 'IS_DEFAULT_PAGE',
                    checked: true,
                    margin: '0 0 0 60'
                }]
            }, {
                xtype: 'fieldcontainer',
                fieldLabel: '系統管理員',
                layout: 'hbox',
                labelAlign: 'right',
                defaultType: 'radiofield',
                items: [{
                    boxLabel: '是',
                    inputValue: 'Y',
                    name: 'IS_ADMIN'
                }, {
                    boxLabel: '否',
                    inputValue: 'N',
                    name: 'IS_ADMIN',
                    checked: true,
                    margin: '0 0 0 60'
                }]
            }, {
                xtype: 'fieldcontainer',
                fieldLabel: '是否啟用',
                labelAlign: 'right',
                layout: 'hbox',
                defaultType: 'radiofield',
                items: [{
                    boxLabel: '啟用',
                    inputValue: 'Y',
                    name: 'IS_ACTIVE',
                    checked: true
                }, {
                    boxLabel: '不啟用',
                    inputValue: 'N',
                    name: 'IS_ACTIVE',
                    margin: '0 0 0 47'
                }]
            }, {
                xtype: 'hiddenfield',
                fieldLabel: 'UUID',
                name: 'UUID',
                itemId: 'UUID'
            }, {
                xtype: 'hiddenfield',
                fieldLabel: 'APPLICATION_HEAD_UUID',
                name: 'APPLICATION_HEAD_UUID',
                itemId: 'APPLICATION_HEAD_UUID'
            }],
            buttons: [{
                icon: SYSTEM_URL_ROOT + '/css/custimages/save16x16.png',
                text: '儲存',
                formBind: true,
                handler: function() {
                    var form = this.up('window').down('#AppMenuForm').getForm();
                    if (form.isValid() == false) {
                        return;
                    };
                    form.submit({
                        waitMsg: '更新中...',
                        success: function(form, action) {
                            Ext.MessageBox.show({
                                title: '選擇維護',
                                msg: '操作完成',
                                icon: Ext.MessageBox.INFO,
                                buttons: Ext.Msg.OK,
                                fn: function() {
                                    this.close();
                                }
                            });
                        },
                        failure: function(form, action) {
                            Ext.MessageBox.show({
                                title: 'System Error',
                                msg: action.result.message,
                                icon: Ext.MessageBox.ERROR,
                                buttons: Ext.Msg.OK
                            });
                        },
                        scope: this.up('window')
                    });
                }
            }, {
                itemId: 'btnDelete',
                icon: SYSTEM_URL_ROOT + '/css/images/delete16x16.png',
                text: '刪除',
                handler: function() {
                    var form = this.up('window').down('#AppMenuForm').getForm();
                    form.api.submit = WS.MenuAction.destroy;
                    form.submit({
                        params: {
                            requestAction: 'delete'
                        },
                        waitMsg: '刪除中...',
                        success: function(form, action) {
                            Ext.MessageBox.show({
                                title: '維護部門/營運邊界',
                                msg: '刪除完成',
                                icon: Ext.MessageBox.INFO,
                                buttons: Ext.Msg.OK,
                                fn: function() {
                                    /*:::變更Submit的方向:::*/
                                    form.api.submit = WS.MenuAction.submit;

                                    this.close();
                                }
                            });
                        },
                        failure: function(form, action) {
                            Ext.MessageBox.show({
                                title: 'System Error',
                                msg: action.result.message,
                                icon: Ext.MessageBox.ERROR,
                                buttons: Ext.Msg.OK
                            });
                        },
                        scope: this.up('window')
                    });
                }
            }, {
                icon: SYSTEM_URL_ROOT + '/css/custimages/exit16x16.png',
                text: '關閉',
                handler: function() {
                    this.up('window').close();
                }
            }]
        }), {
            xtype: 'tabpanel',
            padding: 5,
            maxWidth: 880,
            plain: true,
            items: [{
                icon: SYSTEM_URL_ROOT + '/css/images/connector16x16.png',
                title: '資源',
                items: [{
                    xtype: 'gridpanel',
                    border: true,
                    tbar: [{
                        type: 'button',
                        icon: SYSTEM_URL_ROOT + '/css/images/add16x16.png',
                        text: '挑選資源',
                        handler: function() {
                            var mainWin = this.up('window');
                            mainWin.win.proxypicker = Ext.create(mainWin.subWinProxyPickerWindow, {});
                            mainWin.win.proxypicker.on('closeEvent', mainWin.fnCallBackProxyPickerClose, mainWin);
                            mainWin.win.proxypicker.on('selectEvent', mainWin.fnCallBackProxyPickerSelect, mainWin);
                            mainWin.win.proxypicker.param.applicationHeadUuid = mainWin.down('#APPLICATION_HEAD_UUID').getValue();
                            mainWin.win.proxypicker.param.menuUuid = mainWin.down('#UUID').getValue();
                            mainWin.win.proxypicker.param.parentObject = mainWin
                            mainWin.win.proxypicker.show();
                        }
                    }],
                    itemId: 'gridProxy',
                    store: this.myStore.vappmenuproxymap,
                    autoScroll: true,
                    columns: {
                        defaults: {
                            align: 'left'
                        },
                        items: [{
                            text: "操作",
                            xtype: 'actioncolumn',
                            dataIndex: 'UUID',
                            align: 'center',
                            width: 50,
                            items: [{
                                tooltip: '*刪除',
                                icon: SYSTEM_URL_ROOT + '/css/images/delete16x16.png',
                                handler: function(grid, rowIndex, colIndex) {
                                    var mainWin = grid.up('window');
                                    Ext.MessageBox.confirm('刪除資源通知', '要刪除此項資源?', function(result) {
                                        var menUuid = grid.getStore().getAt(rowIndex).data.APPMENU_UUID,
                                            proxyUuid = grid.getStore().getAt(rowIndex).data.PROXY_UUID;
                                        if (result == 'yes') {
                                            WS.ProxyAction.removeAppmenuProxyMap(menUuid, proxyUuid, function(obj, jsonObj) {
                                                if (jsonObj.result.success) {
                                                    Ext.data.StoreManager.lookup('storevappmenuproxymap').loadPage(1);
                                                } else {
                                                    Ext.MessageBox.show({
                                                        title: 'Warning',
                                                        msg: jsonObj.result.message,
                                                        icon: Ext.MessageBox.ERROR,
                                                        buttons: Ext.Msg.OK
                                                    });
                                                };
                                            });
                                        };
                                    });
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
                            text: "說明",
                            dataIndex: 'DESCRIPTION',
                            flex: 1
                        }, {
                            text: "ReDirect",
                            dataIndex: 'NEED_REDIRECT',
                            flex: 1
                        }, {
                            text: "Proxy[R]",
                            dataIndex: 'REDIRECT_PROXY_ACTION',
                            flex: 1
                        }, {
                            text: "Method[R]",
                            dataIndex: 'REDIRECT_PROXY_METHOD',
                            flex: 1
                        }]
                    },
                    height: 300,
                    bbar: Ext.create('Ext.toolbar.Paging', {
                        store: Ext.data.StoreManager.lookup('storevappmenuproxymap'),
                        itemId: 'gridPading',
                        displayInfo: true,
                        displayMsg: '第{0}~{1}資料/共{2}筆',
                        emptyMsg: "無資料顯示"
                    })
                }]
            }]
        }];
        this.callParent(arguments);
    },
    closeEvent: function() {
        this.fireEvent('closeEvent', this);
    },
    listeners: {
        'show': function() {
            Ext.getBody().mask();
            this.myMask = new Ext.LoadMask(this.down('#AppMenuForm'), {
                msg: "資料載入中，請稍等...",
                store: this.myStore.menu,
                removeMask: true
            });
            this.myMask.show();
            this.myStore.sitemap.getProxy().setExtraParam('pApplicationHeadUuid', 'AUTO');
            this.myStore.sitemap.load({
                callback: function() {
                    this.myStore.menu.getProxy().setExtraParam('pApplicationHeadUuid', "ATUO");
                    this.myStore.menu.load();
                },
                scope: this
            });
            if (this.param.uuid != undefined) {
                this.down('#btnDelete').setDisabled(true);
                this.down('#AppMenuForm').getForm().load({
                    params: {
                        'pUuid': this.param.uuid
                    },
                    success: function(response, a, b) {
                        var _gridProxy = this.down("#gridProxy");
                        _gridProxy.getStore().getProxy().setExtraParam('pApplicationHeadUuid', this.down('#APPLICATION_HEAD_UUID').getValue());
                        _gridProxy.getStore().getProxy().setExtraParam('pAppmenuUuid', this.down('#UUID').getValue());
                        _gridProxy.getStore().load({
                            callback: function() {
                                this.myMask.hide();
                            },
                            scope: this
                        });
                    },
                    failure: function(response, a, b) {
                        r = Ext.decode(response.responseText);
                        alert('err:' + r);
                    },
                    scope: this
                });
            } else {
                this.down('#btnDelete').setDisabled(true);
                this.down('#AppMenuForm').getForm().reset();
                this.down('#APPMENU_UUID').setValue(this.param.parentUuid);
                this.down('#APPLICATION_HEAD_UUID').setValue(this.param.applicationHeadUuid);
                this.down('#UUID').setValue('');
                this.myMask.hide();
            };
        },
        'close': function() {
            Ext.getBody().unmask();
            this.closeEvent();
        }
    }
});
