Ext.Loader.setConfig({
    enabled: true
});
Ext.Loader.setPath('Ext.ux', SYSTEM_ROOT_PATH + '/js/ux');
Ext.require(['*', 'Ext.ux.DataTip']);
Ext.form.VTypes['vdoubleVal'] = /^[0-9]*(\.)?([0-9])*[0-9]$/;
Ext.form.VTypes['vdoubleMask'] = /[0-9.]/;
Ext.form.VTypes['vdoubleText'] = '請輸入大於0的數字';
Ext.form.VTypes['vdouble'] = function(v) {
    return Ext.form.VTypes['vdoubleVal'].test(v);
}
var appMenuForm = undefined;
var myMask = undefined;

Ext.onReady(function() {
    Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION + ".MenuAction"));
    Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION + ".ProxyAction"));
    Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION + ".SiteMapAction"));
    Ext.define('Ext.AppMenuForm.Store.SiteMap', {
        extend: 'Ext.data.Store',
        successProperty: 'success',
        model: Ext.define('SiteMap', {
            extend: 'Ext.data.Model',
            /*:::欄位設定:::*/
            fields: ['UUID', 'IS_ACTIVE', 'CREATE_DATE', 'CREATE_USER', 'UPDATE_DATE', 'UPDATE_USER', 'SITEMAP_UUID', 'APPPAGE_UUID', 'ROOT_UUID', 'HASCHILD', 'APPLICATION_HEAD_UUID', 'NAME', 'DESCRIPTION', 'URL', 'P_MODE', 'PARAMETER_CLASS', 'APPPAGE_IS_ACTIVE']
        }),
        pageSize: 99999,
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
                    'NAME': 'Empty',
                    'UUID': ''
                }));
            }
        },
        remoteSort: true,
        sorters: [{
            property: 'UUID',
            direction: 'ASC'
        }]
    });

    Ext.define('Ext.AppMenuForm.Store.Menu', {
        extend: 'Ext.data.Store',
        successProperty: 'success',
        model: Ext.define('Menu', {
            extend: 'Ext.data.Model',
            fields: ['UUID', 'IS_ACTIVE', 'CREATE_DATE', 'CREATE_USER', 'UPDATE_DATE', 'UPDATE_USER', 'NAME_ZH_TW', 'NAME_ZH_CN', 'NAME_EN_US', 'ID', 'APPMENU_UUID', 'HASCHILD', 'APPLICATION_HEAD_UUID', 'ORD', 'PARAMETER_CLASS', 'IMAGE', 'SITEMAP_UUID', 'ACTION_MODE', 'IS_DEFAULT_PAGE', 'IS_ADMIN']
        }),
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
    });

    Ext.define('AppMenuForm', {
        extend: 'Ext.window.Window',
        title: '選單 新增/修改',
        closeAction: 'hide',
        border: true,
        uuid: undefined,
        parentUuid: undefined,
        applicationHeadUuid: undefined,
        storeSiteMap: Ext.create('Ext.AppMenuForm.Store.SiteMap'),
        storeMenu: Ext.create('Ext.AppMenuForm.Store.Menu'),
        is_operational_boundary: undefined,
        id: 'ExtAppMenuForm',
        width: $(window).width() * 0.9,
        height: $(window).height() * 0.9,
        maxHeight: 600,
        maxWidth: 700,
        overflowY: 'auto',
        border: false,
        resizable: false,
        draggable: true,
        autoFitErrors: true,
        /*展示錯誤信息時 是否自動調整字段組件的寬度*/
        /*定義事件的方法*/
        initComponent: function() {
            var me = this;
            me.items = [Ext.create('Ext.form.Panel', {
                padding: '5 25 5 5',
                layout: {
                    type: 'form',
                    align: 'stretch'
                },
                api: {
                    load: WS.MenuAction.info,
                    submit: WS.MenuAction.submit,
                    destroy: WS.MenuAction.destroyByUuid
                },
                id: 'Ext.AppMenuForm.Form',
                paramOrder: ['pUuid'],
                border: false,
                defaultType: 'textfield',
                buttonAlign: 'center',
                autoScroll: true,
                monitorvalid: true,
                items: [{
                    fieldLabel: '代碼',
                    padding: 5,
                    allowBlank: false,
                    maxLength: 33,
                    name: 'ID',
                    labelAlign: 'right',
                    id: 'Ext.AppMenuForm.Form.ID'
                }, {
                    fieldLabel: '排序',
                    labelAlign: 'right',
                    padding: 5,
                    allowBlank: false,
                    maxLength: 33,
                    name: 'ORD',
                    xtype: 'numberfield',
                    minValue: 0,
                    id: 'Ext_AppMenuForm_Form_ORD'
                }, {
                    fieldLabel: '繁中名稱',
                    labelAlign: 'right',
                    name: 'NAME_ZH_TW',
                    padding: 5,
                    anchor: '50%',
                    allowBlank: false,
                    maxLength: 128
                }, {
                    fieldLabel: '簡中名稱',
                    labelAlign: 'right',
                    name: 'NAME_ZH_CN',
                    padding: 5,
                    anchor: '50%',
                    allowBlank: false,
                    maxLength: 128
                }, {
                    fieldLabel: '英文名稱',
                    labelAlign: 'right',
                    name: 'NAME_EN_US',
                    padding: 5,
                    anchor: '50%',
                    allowBlank: false,
                    maxLength: 128
                }, {
                    fieldLabel: '參數',
                    labelAlign: 'right',
                    name: 'PARAMETER_CLASS',
                    padding: 5,
                    anchor: '50%',
                    allowBlank: true,
                    maxLength: 128
                }, {
                    fieldLabel: 'Icon路徑',
                    labelAlign: 'right',
                    name: 'IMAGE',
                    padding: 5,
                    anchor: '50%',
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
                    id: 'Ext_AppMenuForm_Form_SITEMAP_UUID',
                    displayField: 'NAME',
                    valueField: 'UUID',
                    name: 'SITEMAP_UUID',
                    padding: 5,
                    editable: false,
                    hidden: false,
                    store: this.storeSiteMap
                }, {
                    fieldLabel: '父選單',
                    labelAlign: 'right',
                    xtype: 'combobox',
                    id: 'Ext_AppMenuForm_Form_APPMENU_UUID',
                    displayField: 'NAME_ZH_TW',
                    valueField: 'UUID',
                    name: 'APPMENU_UUID',
                    padding: 5,
                    editable: false,
                    hidden: false,
                    store: this.storeMenu,
                    value: ' '
                }, {
                    xtype: 'fieldcontainer',
                    fieldLabel: '預設頁面',
                    layout: 'hbox',
                    labelAlign: 'right',
                    defaults: {
                        margins: '0 10 0 0'
                    },
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
                    defaults: {
                        margins: '0 10 0 0'
                    },
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
                    defaults: {
                        margins: '0 10 0 0'
                    },
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
                    /*PK值，必須存在*/
                    xtype: 'hidden',
                    fieldLabel: 'UUID',
                    name: 'UUID',
                    id: 'Ext.AppMenuForm.Form.UUID'
                }, {
                    /*appliction_head_uuid值，必須存在*/
                    xtype: 'hidden',
                    fieldLabel: 'APPLICATION_HEAD_UUID',
                    name: 'APPLICATION_HEAD_UUID',
                    id: 'Ext.AppMenuForm.Form.APPLICATION_HEAD_UUID'
                }],
                fbar: [{
                    type: 'button',
                    text: '<img src="' + SYSTEM_URL_ROOT + '/css/images/save.gif" style="width:16px;height:16px;vertical-align:middle;margin-right:5px;"/>' + '儲存',
                    formBind: true,
                    handler: function() {
                        var form = Ext.getCmp('Ext.AppMenuForm.Form').getForm();
                        if (form.isValid() == false) {
                            return;
                        }
                        form.submit({
                            waitMsg: '更新中...',
                            success: function(form, action) {
                                Ext.MessageBox.show({
                                    title: '維護部門',
                                    msg: '操作完成',
                                    icon: Ext.MessageBox.INFO,
                                    buttons: Ext.Msg.OK,
                                    fn: function() {
                                        Ext.getCmp('ExtAppMenuForm').hide();
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
                            }
                        });
                    }
                }, {
                    type: 'button',
                    text: '<img src="' + SYSTEM_URL_ROOT + '/css/images/delete.png" style="width:16px;height:16px;vertical-align:middle;margin-right:5px;"/>' + '刪除',
                    id: 'ExtAppMenuForm.delete',
                    handler: function() {
                        var form = Ext.getCmp('Ext.AppMenuForm.Form').getForm();
                        /*:::變更Submit的方向:::*/
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
                                        Ext.getCmp('ExtAppMenuForm').hide();
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
                            }
                        });
                    }
                }, {
                    type: 'button',
                    text: '<img src="' + SYSTEM_URL_ROOT + '/css/images/leave.png" style="width:16px;height:16px;vertical-align:middle;margin-right:5px;"/>' + '關閉',
                    handler: function() {
                        Ext.getCmp('ExtAppMenuForm').hide();
                    }
                }]
            }), {
                xtype: 'tabpanel',
                padding: '5 25 5 5',
                maxWidth: 880,
                items: [{
                    title: '<img src="' + SYSTEM_URL_ROOT + '/css/images/source.png" style="width:16px;height:16px;vertical-align:middle;margin-right:5px;"/>' + '資源',
                    items: [{
                        xtype: 'gridpanel',
                        border: true,
                        tbar: [{
                            type: 'button',
                            text: '<img src="' + SYSTEM_URL_ROOT + '/css/custimages/add.gif" style="width:16px;height:16px;vertical-align:middle;margin-right:5px;"/>' + '挑選資源',
                            handler: function() {
                                if (WinProxyPicker == undefined) {
                                    WinProxyPicker = Ext.create('ProxyPicker', {});
                                    WinProxyPicker.on('closeEvent', function(obj) {
                                        if (appMenuForm) {
                                            appMenuForm.unmask();
                                        }
                                    });
                                    WinProxyPicker.on('selectedEvent', function(obj, jsonObj) {
                                        var appUuid = Ext.getCmp('Ext.AppMenuForm.Form.APPLICATION_HEAD_UUID').getValue();
                                        var menUuid = Ext.getCmp('Ext.AppMenuForm.Form.UUID').getValue();
                                        WS.ProxyAction.addAppmenuProxyMap(appUuid, menUuid, jsonObj.UUID, function(obj, jsonObj2) {
                                            if (jsonObj2.result.success) {
                                                WinProxyPicker.hide();
                                                Ext.data.StoreManager.lookup('storevappmenuproxymap').reload();
                                            } else {
                                                Ext.MessageBox.show({
                                                    title: 'Warning',
                                                    icon: Ext.MessageBox.INFO,
                                                    buttons: Ext.Msg.OK,
                                                    msg: jsonObj2.result.message
                                                });
                                            }
                                            if (appMenuForm) {
                                                appMenuForm.unmask();
                                            }
                                        });

                                    });
                                }
                                WinProxyPicker.setTitle('<img src="' + SYSTEM_URL_ROOT + '/css/images/source.png" style="height:20px;vertical-align:middle;margin-right:5px;">挑選資源');
                                WinProxyPicker.applicationHeadUuid = Ext.getCmp('Ext.AppMenuForm.Form.APPLICATION_HEAD_UUID').getValue();
                                WinProxyPicker.menuUuid = Ext.getCmp('Ext.AppMenuForm.Form.UUID').getValue();
                                WinProxyPicker.show();
                                if (appMenuForm) {
                                    appMenuForm.mask();
                                }
                            }
                        }],
                        itemId: 'gridProxy',
                        store: Ext.create('Ext.data.Store', {
                            extend: 'Ext.data.Store',
                            storeId: 'storevappmenuproxymap',
                            autoLoad: false,
                            model: Ext.define('Vappmenuproxymap', {
                                extend: 'Ext.data.Model',
                                fields: [
                                    'PROXY_UUID',
                                    'PROXY_ACTION',
                                    'PROXY_METHOD',
                                    'PROXY_DESCRIPTION',
                                    'PROXY_TYPE',
                                    'NEED_REDIRECT',
                                    'REDIRECT_PROXY_ACTION',
                                    'REDIRECT_PROXY_METHOD',
                                    'REDIRECT_SRC',
                                    'APPLICATION_HEAD_UUID',
                                    'NAME_ZH_TW',
                                    'NAME_ZH_CN',
                                    'NAME_EN_US',
                                    'UUID',
                                    'APPMENU_PROXY_UUID',
                                    'APPMENU_UUID',
                                ]
                            }),
                            pageSize: 5,
                            proxy: {
                                type: 'direct',
                                api: {
                                    read: WS.ProxyAction.loadVAppmenuProxyMap
                                },
                                reader: {
                                    root: 'data'
                                },
                                writer: {
                                    type: 'json',
                                    writeAllFields: true,
                                    root: 'updatedata'
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
                        }),
                        paramsAsHash: false,
                        padding: 5,
                        autoScroll: true,
                        columns: [{
                            text: '',
                            dataIndex: 'PROXY_UUID',
                            align: 'center',
                            maxWidth: 50,
                            renderer: function(value, m, r) {
                                var id = Ext.id();
                                Ext.defer(function() {
                                    Ext.widget('button', {
                                        renderTo: id,
                                        text: '<img src="' + SYSTEM_URL_ROOT + '/css/custimages/delete2.png" style="height:12px;vertical-align:middle;margin-right:5px;margin-top:-2px;">',
                                        width: 30,
                                        handler: function() {
                                            Ext.MessageBox.confirm('刪除資源通知', '要刪除此項資源?', function(result) {
                                                //alert(result);
                                                //alert(value);
                                                var menUuid = Ext.getCmp('Ext.AppMenuForm.Form.UUID').getValue();
                                                if (result == 'yes') {
                                                    WS.ProxyAction.removeAppmenuProxyMap(menUuid, value, function(obj, jsonObj) {
                                                        if (jsonObj.result.success) {
                                                            Ext.data.StoreManager.lookup('storevappmenuproxymap').loadPage(1);
                                                        } else {
                                                            Ext.MessageBox.show({
                                                                title: 'Warning',
                                                                msg: jsonObj.result.message,
                                                                icon: Ext.MessageBox.ERROR,
                                                                buttons: Ext.Msg.OK
                                                            });
                                                        }
                                                    });
                                                }
                                            });
                                        }
                                    });
                                }, 50);
                                return Ext.String.format('<div id="{0}"></div>', id);
                            }
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
                            text: "說明",
                            dataIndex: 'DESCRIPTION',
                            align: 'left',
                            flex: 1
                        }, {
                            text: "ReDirect",
                            dataIndex: 'NEED_REDIRECT',
                            align: 'left',
                            flex: 1
                        }, {
                            text: "Proxy[R]",
                            dataIndex: 'REDIRECT_PROXY_ACTION',
                            align: 'left',
                            flex: 1
                        }, {
                            text: "Method[R]",
                            dataIndex: 'REDIRECT_PROXY_METHOD',
                            align: 'left',
                            flex: 1
                        }],
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
            }]
            me.callParent(arguments);
        },
        closeEvent: function() {
            this.fireEvent('closeEvent', this);
        },
        listeners: {
            'show': function() {
                myMask = new Ext.LoadMask(
                    Ext.getCmp('Ext.AppMenuForm.Form'), {
                        msg: "資料載入中，請稍等...",
                        store: this.storeMenu,
                        removeMask: true
                    });
                myMask.show();
                this.storeSiteMap.getProxy().setExtraParam('pApplicationHeadUuid', 'AUTO');
                this.storeSiteMap.load({
                    callback: function() {
                        Ext.getCmp('ExtAppMenuForm').storeMenu.getProxy().setExtraParam('pApplicationHeadUuid', "ATUO");
                        Ext.getCmp('ExtAppMenuForm').storeMenu.load();
                    }
                });
                if (this.uuid != undefined) {
                    Ext.getCmp('ExtAppMenuForm.delete').setDisabled(true);
                    Ext.getCmp('Ext.AppMenuForm.Form').getForm().load({
                        params: {
                            'pUuid': Ext.getCmp('ExtAppMenuForm').uuid
                        },
                        success: function(response, a, b) {
                            var _gridProxy = Ext.getCmp('Ext.AppMenuForm.Form').up('window').down("#gridProxy");
                            _gridProxy.store.getProxy().setExtraParam('pApplicationHeadUuid', Ext.getCmp('Ext.AppMenuForm.Form.APPLICATION_HEAD_UUID').getValue());
                            _gridProxy.store.getProxy().setExtraParam('pAppmenuUuid', Ext.getCmp('Ext.AppMenuForm.Form.UUID').getValue());
                            _gridProxy.store.load();
                        },
                        failure: function(response, a, b) {
                            r = Ext.decode(response.responseText);
                            alert('err:' + r);
                        }
                    }, this);
                } else {
                    Ext.getCmp('ExtAppMenuForm.delete').setDisabled(true);
                    /*When 新增資料*/
                    /*取得公司資訊*/
                    Ext.getCmp('Ext.AppMenuForm.Form').getForm().reset();
                    Ext.getCmp('ExtAppMenuForm.delete').setDisabled(true);
                    Ext.getCmp('Ext_AppMenuForm_Form_APPMENU_UUID').setValue(this.parentUuid);
                    Ext.getCmp('Ext.AppMenuForm.Form.APPLICATION_HEAD_UUID').setValue(this.applicationHeadUuid);
                    Ext.getCmp('Ext.AppMenuForm.Form.UUID').setValue('');
                }
            },
            'hide': function() {
                Ext.getBody().unmask();
                this.closeEvent();
            }
        }
    });
});
