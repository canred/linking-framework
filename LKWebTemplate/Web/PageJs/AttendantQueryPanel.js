/*全局變量*/
var WS_ATTENDANTQUERYPANEL;
/*WS.CompanyQueryPanel物件類別*/
/*TODO*/
/*
1.Model 要集中                 [YES]
2.不可以有任何的 getCmp 字眼   [YES]
*/
/*columns 使用default*/
Ext.define('WS.AttendantQueryPanel', {
    extend: 'Ext.panel.Panel',
    closeAction: 'destroy',
    subWinAttendant: undefined,
    /*語言擴展*/
    lan: {},
    /*參數擴展*/
    param: {
        companyUuid: undefined,
        showAd: false,
        showSync: false
    },
    /*值擴展*/
    val: {},
    /*物件會用到的Store物件*/
    myStore: {
        attendantV: Ext.create('Ext.data.Store', {
            successProperty: 'success',
            autoLoad: false,
            model: 'ATTEDNANTVV',
            pageSize: 10,
            proxy: {
                type: 'direct',
                api: {
                    read: WS.AttendantAction.load
                },
                reader: {
                    root: 'data'
                },
                paramsAsHash: true,
                paramOrder: ['company_uuid', 'keyword', 'page', 'limit', 'sort', 'dir'],
                extraParams: {
                    company_uuid: '',
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
    fnActiveRender: function isActiveRenderer(value, id, r) {
        var html = "<img src='" + SYSTEM_URL_ROOT;
        return value === "Y" ? html + "/css/custimages/active03.png'>" : html + "/css/custimages/unactive03.png'>";
    },
    initComponent: function() {
        /*參數檢查*/
        if (Ext.isEmpty(this.param.companyUuid)) {
            Ext.MessageBox.show({
                title: '系統提示',
                icon: Ext.MessageBox.WARNING,
                buttons: Ext.Msg.OK,
                msg: '參數companyUuid未設定'
            });
            return false;
        };
        /*分配設定*/
        this.myStore.attendantV.getProxy().setExtraParam('company_uuid', this.param.companyUuid);
        /*佈局設定*/
        this.items = [{
            xtype: 'panel',
            icon: SYSTEM_URL_ROOT + '/css/images/manb16x16.png',
            title: '人員查詢',
            frame: true,
            height: $(document).height() - 150,
            items: [{
                xtype: 'container',
                layout: 'hbox',
                margin: '5 5 5 5',
                items: [{
                    xtype: 'textfield',
                    fieldLabel: '關鍵字',
                    labelWidth: 50,
                    itemId: 'txt_search',
                    enableKeyEvents: true,
                    listeners: {
                        keyup: function(e, t, eOpts) {
                            var keyCode = t.parentEvent.keyCode;
                            if (keyCode == Ext.event.Event.ENTER) {
                                this.up('panel').down("#btnQuery").handler();
                            };
                        }
                    }
                }, {
                    xtype: 'button',
                    width: 70,
                    margin: '0 5 0 5',
                    icon: SYSTEM_URL_ROOT + '/css/custimages/find.png',
                    text: '查詢',
                    itemId: 'btnQuery',
                    handler: function() {
                        var _main = this.up('panel').up('panel'),
                            _companyUuid = _main.param.companyUuid,
                            _txtSearch = _main.down("#txt_search").getValue();
                        _main.myStore.attendantV.getProxy().setExtraParam('company_uuid', _companyUuid);
                        _main.myStore.attendantV.getProxy().setExtraParam('keyword', _txtSearch);
                        _main.myStore.attendantV.loadPage(1);
                    }
                }, {
                    xtype: 'button',
                    width: 70,
                    margin: '0 5 0 5',
                    icon: SYSTEM_URL_ROOT + '/css/custimages/clear.png',
                    text: '清除',
                    tooltip: '*清除目前所有的條件查詢',
                    handler: function() {
                        var _main = this.up('panel').up('panel');
                        _main.down("#txt_search").setValue('');
                    }
                }]
            }, {
                xtype: 'gridpanel',
                store: this.myStore.attendantV,
                itemId: 'grdAttendantQuery',
                padding: 5,
                border: true,
                height: $(document).height() - 240,
                columns: [{
                    text: "編輯",
                    xtype: 'actioncolumn',
                    dataIndex: 'UUID',
                    align: 'center',
                    width: 60,
                    items: [{
                        tooltip: '*編輯',
                        icon: '../../css/images/edit16x16.png',
                        handler: function(grid, rowIndex, colIndex) {
                            var main = grid.up('panel').up('panel').up('panel');
                            if (!main.subWinAttendant) {
                                Ext.MessageBox.show({
                                    title: '系統訊息',
                                    icon: Ext.MessageBox.INFO,
                                    buttons: Ext.Msg.OK,
                                    msg: '未實現 subWinAttendant 物件,無法進行編輯操作!'
                                });
                                return false;
                            };
                            /*註冊事件*/
                            var subWin = Ext.create(main.subWinAttendant,{
                                param:{
                                    uuid:grid.getStore().getAt(rowIndex).data.UUID
                                }
                            });
                            subWin.on('closeEvent', function(obj) {
                                main.down("#grdAttendantQuery").getStore().load();
                            }, main);                            
                            subWin.show();
                        }
                    }],
                    sortable: false,
                    hideable: false
                }, {
                    header: "帳號",
                    dataIndex: 'ACCOUNT',
                    align: 'left',
                    flex: 1
                }, {
                    header: "名稱-繁中",
                    dataIndex: 'C_NAME',
                    align: 'left',
                    flex: 1
                }, {
                    header: "名稱-英文",
                    dataIndex: 'E_NAME',
                    align: 'left',
                    flex: 1
                }, {
                    header: '啟用',
                    dataIndex: 'IS_ACTIVE',
                    align: 'center',
                    flex: 1,
                    renderer: this.fnActiveRender
                }],
                tbarCfg: {
                    buttonAlign: 'right'
                },
                bbar: Ext.create('Ext.toolbar.Paging', {
                    store: this.myStore.attendantV,
                    displayInfo: true,
                    displayMsg: '第{0}~{1}資料/共{2}筆',
                    emptyMsg: "無資料顯示"
                }),
                tbar: [{
                    icon: SYSTEM_URL_ROOT + '/css/images/add16x16.png',
                    text: '新增',
                    handler: function() {
                        var main = this.up('panel').up('panel').up('panel');
                        if (!main.subWinAttendant) {
                            Ext.MessageBox.show({
                                title: '系統訊息',
                                icon: Ext.MessageBox.INFO,
                                buttons: Ext.Msg.OK,
                                msg: '未實現 subWinAttendant 物件,無法進行編輯操作!'
                            });
                            return false;
                        };
                        var subWin = Ext.create(main.subWinAttendant,{
                            param:{
                                uuid:undefined
                            }
                        });
                        /*註冊事件*/
                        subWin.on('closeEvent', function(obj) {
                            main.down("#grdAttendantQuery").getStore().load();
                        }, main);                        
                        subWin.show();
                    }
                }, {
                    icon: SYSTEM_URL_ROOT + '/css/images/cloudsync16x16.png',
                    text: '同步人員(AD)',
                    hidden: this.param.showAd,
                    handler: function() {
                        Ext.getBody().mask();
                        var main = this.up('panel').up('panel').up('panel');
                        WS.ADAction.loadUser(function(obj, jsonObj) {
                            if (jsonObj.result.success) {
                                Ext.MessageBox.show({
                                    title: '同步AD人員',
                                    icon: Ext.MessageBox.WARNING,
                                    buttons: Ext.Msg.OK,
                                    msg: '完成!',
                                    fn: function() {
                                        Ext.getBody().unmask();
                                    }
                                });
                                this.myStore.attendantV.loadPage(1);
                            } else {
                                Ext.MessageBox.show({
                                    title: '同步AD人員',
                                    icon: Ext.MessageBox.WARNING,
                                    buttons: Ext.Msg.OK,
                                    msg: jsonObj.result.message,
                                    fn: function() {
                                        Ext.getBody().unmask();
                                    }
                                });
                                this.myStore.attendantV.loadPage(1);
                            }
                        }, main);
                    }
                }, {
                    icon: SYSTEM_URL_ROOT + '/css/images/cloudsync16x16.png',
                    text: '同步人員(主伺服器)',
                    hidden: this.param.showSync,
                    handler: function() {
                        var main = this.up('panel').up('panel').up('panel');
                        Ext.getBody().mask("正在處理中…請稍後…");
                        WS.SyncClientAction.SyncAttendant(function(obj, jsonObj) {
                            Ext.getBody().unmask();
                            if (jsonObj.result.success) {
                                Ext.MessageBox.show({
                                    title: '同步人員(主伺服器)',
                                    icon: Ext.MessageBox.INFO,
                                    buttons: Ext.Msg.OK,
                                    msg: '已成功完成同步作業。',
                                    fn: function() {
                                        this.myStore.attendantV.loadPage(1);
                                    }
                                });
                            } else {
                                Ext.MessageBox.show({
                                    title: '發生異常',
                                    icon: Ext.MessageBox.INFO,
                                    buttons: Ext.Msg.OK,
                                    msg: jsonObj.result.message
                                });
                            }
                        }, main);
                    }
                }]
            }]
        }];
        this.callParent(arguments);
    },
    listeners:{
        afterrender:function(obj,eOpts){
            this.myStore.attendantV.load({                
                scope:this
            });
        }
    }
});
