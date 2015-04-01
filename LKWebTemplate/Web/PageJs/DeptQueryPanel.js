/*全局變量*/
var WS_DEPTQUERYPANEL;
Ext.define('WS.DeptQueryPanel', {
    extend: 'Ext.panel.Panel',
    closeAction: 'destroy',
    subWinDept: undefined,
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
        company: Ext.create('Ext.data.Store', {
            successProperty: 'success',
            autoLoad: false,
            model: 'COMPANY',
            pageSize: 10,
            proxy: {
                type: 'direct',
                api: {
                    read: WS.AdminCompanyAction.loadCompany
                },
                reader: {
                    root: 'data'
                },
                paramsAsHash: true,
                paramOrder: ['pKeyword', 'pIsActive', 'page', 'limit', 'sort', 'dir'],
                extraParams: {
                    pKeyword: '',
                    pIsActive: 'Y'
                },
                simpleSortMode: true,
                listeners: {
                    exception: function(proxy, response, operation) {
                        Ext.MessageBox.show({
                            title: 'Warning',
                            msg: response.result.message,
                            icon: Ext.MessageBox.ERROR,
                            buttons: Ext.Msg.OK
                        });
                    }
                }
            },
            remoteSort: true,
            sorters: [{
                property: 'C_NAME',
                direction: 'ASC'
            }]
        })
    },
    fnQuery: function(obj) {
        /*scope要是主體*/
        if (obj !== undefined) {
            WS.DeptAction.loadTreeRoot(obj.down("#cmbCompany").getValue(), function(data) {
                var data = data.data[0].UUID;
                this.myStore.tree.load({
                    params: {
                        UUID: data
                    },
                    callback: function() {
                        this.down('#treeDept').expandAll();
                    },
                    scope: this
                });
            }, obj);
        } else {
            alert('發生錯誤!');
        }
    },
    fnActiveRender: function(value, id, r) {
        var html = "<img src='" + SYSTEM_URL_ROOT;
        return value === "Y" ? html + "/css/custimages/active03.png'>" : html + "/css/custimages/unactive03.png'>";
    },
    fnCallBackReloadGrid: function(main) {
        /*this是由scope來的*/
        this.down("#grdAppPage").getStore().load();
        this.subWinDept.un('closeEvent', this.fnCallBackReloadGrid);
    },
    fnCheckSubComponent: function() {
        /*要把scope變成SitemapQueryPanel主體*/
        if (Ext.isEmpty(this.subWinDept)) {
            Ext.MessageBox.show({
                title: '系統提示',
                icon: Ext.MessageBox.WARNING,
                buttons: Ext.Msg.OK,
                msg: '未指定 subWinDept 物件,無法進行編輯操作!'
            });
            return false;
        };
        return true;
    },
    fnCallBackCloseEvent: function(obj) {
        this.fnQuery(this);
    },
    fnCallBackSelectEvent: function(obj, record) {
        WS.DeptAction.addSiteMap(this.param.PARENTUUID, record.UUID);
        Ext.MessageBox.show({
            title: '節點操作',
            icon: Ext.MessageBox.INFO,
            buttons: Ext.Msg.OK,
            msg: '新增成功'
        });
    },
    fnAddChildDept: function(uuid, parendUuid) {
        /*要把scope變成SitemapQueryPanel主體*/
        if (!this.fnCheckSubComponent()) {
            return false;
        };
        this.param.PARENTUUID = parendUuid;
        var subWin = Ext.create(this.subWinDept, {
            param: {
                uuid: undefined,
                companyUuid: this.down('#cmbCompany').getValue(),
                parentDepartmentUuid: parendUuid,
                parentObj: this
            }
        });
        /*註冊事件*/
        subWin.on('closeEvent', this.fnCallBackCloseEvent, this);
        subWin.show();
    },
    fnOpenDept: function(uuid, parendUuid) {
        /*要把scope變成SitemapQueryPanel主體*/
        if (!this.fnCheckSubComponent()) {
            return false;
        };
        this.param.PARENTUUID = parendUuid;
        var subWin = Ext.create(this.subWinDept, {
            param: {
                uuid: uuid,
                companyUuid: this.down('#cmbCompany').getValue(),
                parentDepartmentUuid: parendUuid,
                parentObj: this
            }
        });
        /*註冊事件*/
        subWin.on('closeEvent', this.fnCallBackCloseEvent, this);
        subWin.show();
    },
    fnDelDept: function(uuid) {
        /*要把scope變成SitemapQueryPanel主體*/
        Ext.Msg.show({
            title: '刪除部門操作',
            msg: '確定執行刪除動作?',
            buttons: Ext.Msg.YESNO,
            fn: function(btn) {
                if (btn == "yes") {
                    var appliction_uuid = this.down('#cmbCompany').getValue();
                    WS.DeptAction.destroy(uuid, function(json) {
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
        this.myStore.tree = Ext.create('WS.DeptTreeStore', {});
        if (!this.fnCheckSubComponent()) {
            return false;
        };
        this.items = [{
            xtype: 'panel',
            title: '部門維護',
            icon: SYSTEM_URL_ROOT + '/css/images/organisation16x16.png',
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
                    store: this.myStore.company,
                    fieldLabel: '公司',
                    displayField: 'C_NAME',
                    labelAlign: 'right',
                    labelWidth: 50,
                    valueField: 'UUID',
                    itemId: 'cmbCompany',
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
                text: '新增子部門',
                icon: SYSTEM_URL_ROOT + '/css/images/add16x16.png',
                handler: function() {
                    var mainPanel = this.up('panel').up('panel'),
                        company = mainPanel.down("#cmbCompany").getValue();
                    if (!mainPanel.fnCheckSubComponent()) {
                        return false;
                    };
                    if (Ext.isEmpty(company)) {
                        Ext.MessageBox.show({
                            title: '系統提示',
                            icon: Ext.MessageBox.WARNING,
                            buttons: Ext.Msg.OK,
                            msg: '請先選擇系統欄位!'
                        });
                        return false;
                    };
                    WS.DeptAction.loadTreeRoot(company, function(data) {
                        this.param.PARENTUUID = data.data[0].UUID;
                        var company = this.down('#cmbCompany').getValue();
                        var subWin = Ext.create(this.subWinDept, {
                            param: {
                                companyUuid: this.down('#cmbCompany').getValue(),
                                parentDepartmentUuid: this.param.PARENTUUID,
                                uuid: undefined,
                                parentObj: mainPanel
                            }
                        });
                        /*註冊事件*/
                        subWin.on('closeEvent', this.fnCallBackCloseEvent, this);
                        /*設定參數*/
                        subWin.param.uuid = undefined;
                        subWin.param.companyUuid = company;
                        subWin.show();
                    }, mainPanel);
                }
            }, {
                xtype: 'treepanel',
                fieldLabel: '部門名稱',
                padding: 5,
                itemId: 'treeDept',
                border: false,
                store: this.myStore.tree,
                rootVisible: false,
                border: true,
                columns: {
                    defaults: {
                        align: 'left',
                    },
                    items: [{
                        xtype: 'treecolumn',
                        text: '部門組織',
                        flex: 2,
                        sortable: false,
                        dataIndex: 'C_NAME'
                    }, {
                        text: '有效',
                        width: 80,
                        dataIndex: 'IS_ACTIVE',
                        align: 'center',
                        sortable: false,
                        hidden: false,
                        renderer: this.fnActiveRender
                    }, {
                        text: "編輯",
                        xtype: 'actioncolumn',
                        dataIndex: 'UUID',
                        align: 'center',
                        width: 80,
                        items: [{
                            tooltip: '*編輯部門',
                            icon: SYSTEM_URL_ROOT + '/css/images/edit16x16.png',
                            handler: function(grid, rowIndex, colIndex) {
                                var mainPanel = grid.up('panel').up('panel').up('panel'),
                                    uuid = grid.getStore().getAt(rowIndex).data.UUID;
                                mainPanel.fnOpenDept(uuid, uuid);
                            }
                        }],
                        sortable: false,
                        hideable: false
                    }, {
                        text: "加入子部門",
                        xtype: 'actioncolumn',
                        dataIndex: 'UUID',
                        align: 'center',
                        width: 80,
                        items: [{
                            tooltip: '*加入子部門',
                            icon: SYSTEM_URL_ROOT + '/css/images/add16x16.png',
                            handler: function(grid, rowIndex, colIndex) {
                                var mainPanel = grid.up('panel').up('panel').up('panel'),
                                    uuid = grid.getStore().getAt(rowIndex).data.UUID;
                                mainPanel.fnAddChildDept(undefined, uuid);
                            }
                        }],
                        sortable: false,
                        hideable: false
                    }, {
                        text: "刪除",
                        xtype: 'actioncolumn',
                        dataIndex: 'UUID',
                        align: 'center',
                        width: 80,
                        items: [{
                            tooltip: '*刪除此部門',
                            icon: SYSTEM_URL_ROOT + '/css/images/delete16x16.png',
                            margin: '0 0 0 40',
                            handler: function(grid, rowIndex, colIndex) {
                                var mainPanel = grid.up('panel').up('panel').up('panel'),
                                    uuid = grid.getStore().getAt(rowIndex).data.UUID;;
                                mainPanel.fnDelDept(uuid);
                            }
                        }],
                        sortable: false,
                        hideable: false
                    }]
                }
            }]
        }];
        this.callParent(arguments);
    },
    listeners: {
        afterrender: function(obj, eOpts) {
            this.myStore.company.load({
                callback: function(obj) {
                    if (obj.length > 0) {
                        this.down('#cmbCompany').setValue(obj[0].data.UUID);
                    };
                },
                scope: this
            });
        }
    }
});
