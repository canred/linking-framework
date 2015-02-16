/*全局變量*/
var WS_SITEMAPQUERYPANEL;
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
Ext.define('WS.SitemapQueryPanel', {
    extend: 'Ext.panel.Panel',
    closeAction: 'destroy',
    subWinApppagePickerWindow: undefined,
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
    fnQuery: function(obj) {
        /*scope要是主體*/
        if (obj !== undefined) {
            WS.SiteMapAction.loadTreeRoot(obj.down("#cmbApplication").getValue(), function(data) {
                this.myStore.tree.load({
                    params: {
                        UUID: data.UUID
                    }
                });
                SiteMapVTaskFlag = true;
            }, obj);
        } else {
            alert('err..................');
        }
    },
    fnActiveRender: function(value, id, r) {
        var html = "<img src='" + SYSTEM_URL_ROOT;
        return value === "Y" ? html + "/css/custimages/active03.png'>" : html + "/css/custimages/unactive03.png'>";
    },
    fnCallBackReloadGrid: function(main) {
        /*this是由scope來的*/
        this.down("#grdAppPage").getStore().load();
        this.subWinApppagePickerWindow.un('closeEvent', this.fnCallBackReloadGrid);
    },
    fnCheckSubComponent: function() {
        /*要把scope變成SitemapQueryPanel主體*/
        if (Ext.isEmpty(this.subWinApppagePickerWindow)) {
            Ext.MessageBox.show({
                title: '系統提示',
                icon: Ext.MessageBox.WARNING,
                buttons: Ext.Msg.OK,
                msg: '未指定 subWinApppagePickerWindow 物件,無法進行編輯操作!'
            });
            return false;
        };
        return true;
    },
    fnCallBackCloseEvent: function(obj) {
        this.fnQuery(this);
    },
    fnCallBackSelectEvent: function(obj, record) {
        WS.SiteMapAction.addSiteMap(this.param.PARENTUUID, record.UUID);
        Ext.MessageBox.show({
            title: '節點操作',
            icon: Ext.MessageBox.INFO,
            buttons: Ext.Msg.OK,
            msg: '新增成功'
        });
    },
    fnOpenOrgn: function(uuid, parendUuid) {
        /*要把scope變成SitemapQueryPanel主體*/
        if (!this.fnCheckSubComponent()) {
            return false;
        };
        this.param.PARENTUUID = parendUuid;
        var subWin = Ext.create(this.subWinApppagePickerWindow, {});
        /*註冊事件*/
        subWin.on('selectedEvent', this.fnCallBackSelectEvent, this);
        subWin.on('closeEvent', this.fnCallBackCloseEvent, this);
        /*設定參數*/
        subWin.param.uuid = uuid;
        subWin.param.applicationHeadUuid = this.down('#cmbApplication').getValue();
        subWin.show();
    },
    fnDelSitemap: function(uuid) {
        /*要把scope變成SitemapQueryPanel主體*/
        Ext.Msg.show({
            title: '刪除節點操作',
            msg: '確定執行刪除動作?',
            buttons: Ext.Msg.YESNO,
            fn: function(btn) {
                if (btn == "yes") {
                    var appliction_uuid = this.down('#cmbApplication').getValue();
                    WS.SiteMapAction.deleteSiteMap(uuid, appliction_uuid, function(json) {
                        if (json.success == false) {
                            Ext.Msg.show({
                                title: '系統通知',
                                msg: json.message,
                                buttons: Ext.Msg.YES
                            });
                        } else {
                            this.fnQuery(this);
                        };
                    }, this);
                };
            },
            scope: this
        });
    },
    initComponent: function() {
        this.myStore.tree = Ext.create('WS.SitemapTreeStore', {});
        if (!this.fnCheckSubComponent()) {
            return false;
        };
        this.items = [{
            xtype: 'panel',
            title: '網站地圖',
            icon: SYSTEM_URL_ROOT + '/css/images/map16x16.png',
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
                    store: this.myStore.application,
                    fieldLabel: '系統',
                    displayField: 'NAME',
                    labelAlign: 'right',
                    labelWidth: 50,
                    valueField: 'UUID',
                    itemId: 'cmbApplication',
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
                text: '新增網站子節點',
                icon: SYSTEM_URL_ROOT + '/css/images/add16x16.png',
                handler: function() {
                    var mainPanel = this.up('panel').up('panel'),
                        application = mainPanel.down("#cmbApplication").getValue();
                    if (!mainPanel.fnCheckSubComponent()) {
                        return false;
                    };
                    if (Ext.isEmpty(application)) {
                        Ext.MessageBox.show({
                            title: '系統提示',
                            icon: Ext.MessageBox.WARNING,
                            buttons: Ext.Msg.OK,
                            msg: '請先選擇系統欄位!'
                        });
                        return false;
                    };
                    WS.SiteMapAction.loadTreeRoot(application, function(data) {
                        this.param.PARENTUUID = data.UUID;
                        var application = this.down('#cmbApplication').getValue();
                        var subWin = Ext.create(this.subWinApppagePickerWindow, {});
                        /*註冊事件*/
                        subWin.on('selectedEvent', this.fnCallBackSelectEvent, this);
                        subWin.on('closeEvent', this.fnCallBackCloseEvent, this);
                        /*設定參數*/
                        subWin.param.uuid = undefined;
                        subWin.param.applicationHeadUuid = application;
                        subWin.show();
                    }, mainPanel);
                }
            }, {
                xtype: 'treepanel',
                fieldLabel: '網站地圖',
                padding: 5,
                border: false,
                store: this.myStore.tree,
                rootVisible: false,
                useArrows: false,
                columns: [{
                    xtype: 'treecolumn',
                    text: '地圖功能',
                    flex: 2,
                    sortable: true,
                    dataIndex: 'NAME'
                }, {
                    text: '描述',
                    flex: 2,
                    dataIndex: 'DESCRIPTION',
                    align: 'left',
                    sortable: true
                }, {
                    text: "維護",
                    xtype: 'actioncolumn',
                    dataIndex: 'UUID',
                    align: 'center',
                    flex: 1,
                    items: [{
                        tooltip: '*新增子節點',
                        icon: '../../css/images/add16x16.png',
                        handler: function(grid, rowIndex, colIndex) {
                            var mainPanel = grid.up('panel').up('panel').up('panel'),
                                uuid = grid.getStore().getAt(rowIndex).data.UUID;
                            mainPanel.fnOpenOrgn(undefined, uuid);
                        }
                    }, {
                        tooltip: '*刪除此節點',
                        icon: '../../css/images/delete16x16.png',
                        margin: '0 0 0 40',
                        handler: function(grid, rowIndex, colIndex) {
                            var mainPanel = grid.up('panel').up('panel').up('panel'),
                                uuid = grid.getStore().getAt(rowIndex).data.UUID;;
                            mainPanel.fnDelSitemap(uuid);
                        }
                    }],
                    sortable: false,
                    hideable: false
                }],
                listeners: {
                    beforeload: function(tree, node, eOpts) {
                        var mainPanel = this.up('panel').up('panel'),
                            treeStore = mainPanel.myStore.tree;
                        if (node.isComplete() == false) {
                            if (node.getParams()["UUID"] != undefined) {
                                treeStore.getProxy().setExtraParam('UUID', node.getParams()["UUID"]);
                            } else {
                                treeStore.getProxy().setExtraParam('UUID', node.config.node.data["UUID"]);
                            };
                        };
                    },
                    checkchange: function(a, b, c, d) {
                        var oUuid = a.data.UUID;
                        if (a.data.checked == true) {
                            /*表加入*/
                            WS.SiteMapAction.setSiteMapIsActive(oUuid, "1", function(ret) {
                                if (ret.success == false) {
                                    Ext.MessageBox.show({
                                        title: 'WARNING',
                                        msg: "ok",
                                        icon: Ext.MessageBox.WARNING,
                                        buttons: Ext.Msg.OK
                                    });
                                };
                            });
                        } else {
                            WS.SiteMapAction.setSiteMapIsActive(oUuid, "0", function(ret) {
                                if (ret.success == false) {
                                    Ext.MessageBox.show({
                                        title: 'WARNING',
                                        msg: "ok",
                                        icon: Ext.MessageBox.WARNING,
                                        buttons: Ext.Msg.OK
                                    });
                                };
                            });
                        };
                    }
                }
            }]
        }];
        this.callParent(arguments);
    },
    listeners: {
        afterrender: function(obj, eOpts) {
            this.myStore.application.load({
                callback: function(obj) {
                    console.log(obj);
                    if (obj.length > 0) {
                        this.down('#cmbApplication').setValue(obj[0].data.UUID);
                    };
                },
                scope: this
            });
        }
    }
});
