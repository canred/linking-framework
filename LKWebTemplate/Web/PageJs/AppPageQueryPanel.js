/*全局變量*/
var WS_APPPAGEQUERYPANEL;
/*WS.CompanyQueryPanel物件類別*/
/*TODO*/
/*
1.Model 要集中                                 [YES]
2.panel 的title要換成icon , title的方式        [YES]
3.add 的icon要換成icon , title的方式           [YES]
*/
/*columns 使用default*/
Ext.define('WS.AppPageQueryPanel', {
    extend: 'Ext.panel.Panel',
    closeAction: 'destroy',
    subWinAppPage: undefined,
    /*語言擴展*/
    lan: {},
    /*參數擴展*/
    param: {},
    /*值擴展*/
    val: {},
    /*物件會用到的Store物件*/
    myStore: {
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
        }),
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
                            title: 'Warning',
                            msg: response.result.message,
                            icon: Ext.MessageBox.WARNING,
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
    fnActiveRender: function(value, id, r) {
        var html = "<img src='" + SYSTEM_URL_ROOT;
        return value === "Y" ? html + "/css/custimages/active03.png'>" : html + "/css/custimages/unactive03.png'>";
    },
    fnCallBackReloadGrid: function(main) {
        this.down("#grdAppPage").getStore().load();
    },
    initComponent: function() {
        if (Ext.isEmpty(this.subWinAppPage)) {
            Ext.MessageBox.show({
                title: '系統提示',
                icon: Ext.MessageBox.WARNING,
                buttons: Ext.Msg.OK,
                msg: '未實現 subWinAppPage 物件,無法進行編輯操作!'
            });
            return;
        };
        this.items = [{
            xtype: 'panel',
            title: '功能清單',
            icon: SYSTEM_URL_ROOT + '/css/images/apppage16x16.png',
            frame: true,
            height: 580,
            autoWidth: true,
            items: [{
                xtype: 'container',
                layout: 'hbox',
                margin: 5,
                items: [{
                    xtype: 'combo',
                    margin: '0 5 0 5',
                    editable: false,
                    store: this.myStore.application,
                    enableKeyEvents: true,
                    displayField: 'NAME',
                    fieldLabel: '系統',
                    labelAlign: 'right',
                    labelWidth: 50,
                    valueField: 'UUID',
                    itemId: 'function_Query_Application',
                    listeners: {
                        keyup: function(e, t, eOpts) {
                            var keyCode = t.parentEvent.keyCode,mainPanel=this.up('panel');
                            if (keyCode == Ext.event.Event.ENTER) {
                                mainPanel.down("#btnQuery").handler();
                            };
                        }
                    }
                }, {
                    xtype: 'textfield',
                    itemId: 'txt_search',
                    margin: '0 5 0 5',
                    fieldLabel: '關鍵字',
                    labelWidth: 50,
                    enableKeyEvents: true,
                    listeners: {
                        keyup: function(e, t, eOpts) {
                            var keyCode = t.parentEvent.keyCode,mainPanel=this.up('panel');
                            if (keyCode == Ext.event.Event.ENTER) {
                                mainPanel.down("#btnQuery").handler();
                            };
                        }
                    }
                }, {
                    xtype: 'button',
                    icon: SYSTEM_URL_ROOT + '/css/custimages/find.png',
                    text: '查詢',
                    width: 80,
                    margin: '0 5 0 5',
                    itemId: 'btnQuery',
                    handler: function() {
                        var main = this.up('panel').up('panel'),_txtSearch = main.down("#txt_search").getValue(),_application = main.down("#function_Query_Application").getValue();
                        if (Ext.isEmpty(_application)) {
                            Ext.MessageBox.show({
                                title: '系統提示',
                                icon: Ext.MessageBox.WARNING,
                                buttons: Ext.Msg.OK,
                                msg: '請先選擇系統!'
                            });
                            return false;
                        };
                        main.myStore.apppage.getProxy().setExtraParam('pKeyword', _txtSearch);
                        main.myStore.apppage.getProxy().setExtraParam('pApplicationHeadUuid', _application);
                        main.myStore.apppage.loadPage(1);
                    }
                }, {
                    xtype: 'button',
                    width: 80,
                    margin: '0 0 0 5',
                    icon: SYSTEM_URL_ROOT + '/css/custimages/clear.png',
                    text: '清除',
                    handler: function() {
                        this.up('panel').up('panel').down("#txt_search").setValue('');
                    }
                }]
            }, {
                xtype: 'gridpanel',
                store: this.myStore.apppage,
                itemId: 'grdAppPage',
                height: 500,
                padding: 5,
                border: true,
                columns: [{
                    text: "編輯",
                    xtype: 'actioncolumn',
                    dataIndex: 'UUID',
                    align: 'center',
                    width: 60,
                    items: [{
                        tooltip: '*編輯',
                        icon: SYSTEM_URL_ROOT + '/css/images/edit16x16.png',
                        handler: function(grid, rowIndex, colIndex) {
                            var main = grid.up('panel').up('panel').up('panel');
                            if (!main.subWinAppPage) {
                                Ext.MessageBox.show({
                                    title: '系統訊息',
                                    icon: Ext.MessageBox.INFO,
                                    buttons: Ext.Msg.OK,
                                    msg: '未實現 subWinAppPage 物件,無法進行編輯操作!'
                                });
                                return false;
                            };
                            var subWin = Ext.create(main.subWinAppPage,{
                                param:{
                                    uuid:grid.getStore().getAt(rowIndex).data.UUID
                                }
                            });
                            subWin.on('closeEvent', main.fnCallBackReloadGrid, main);
                            subWin.show();
                        }
                    }],
                    sortable: false,
                    hideable: false
                }, {
                    header: "<center>功能代碼</center>",
                    dataIndex: 'ID',
                    align: 'left',
                    flex: 1
                }, {

                    header: "功能名稱",
                    dataIndex: 'NAME',
                    align: 'left',
                    flex: 1
                }, {
                    header: "<center>功能描述</center>",
                    align: 'left',
                    dataIndex: 'DESCRIPTION',
                    flex: 1,
                    renderer: function(value) {
                        return '<div align="left">' + value + '</div>';
                    }
                }, {
                    header: "<center>路徑</center>",
                    dataIndex: 'URL',
                    align: 'left',
                    flex: 1
                }, {
                    header: "<center>行為</center>",
                    align: 'center',
                    dataIndex: 'P_MODE',
                    flex: 1,
                    renderer: function(value) {
                        return '<div align="left">' + value + '</div>';
                    }
                }, {
                    header: '<center>啟用</center>',
                    dataIndex: 'IS_ACTIVE',
                    align: 'center',
                    flex: 1,
                    renderer: this.fnActiveRender
                }],
                tbarCfg: {
                    buttonAlign: 'right'
                },
                bbar: Ext.create('Ext.toolbar.Paging', {
                    store: this.myStore.apppage,
                    displayInfo: true,
                    displayMsg: '第{0}~{1}資料/共{2}筆',
                    emptyMsg: "無資料顯示"
                }),
                tbar: [{
                    icon: SYSTEM_URL_ROOT + '/css/images/add16x16.png',
                    text: '新增',
                    handler: function() {
                        var main = this.up('panel').up('panel').up('panel');
                        if (!main.subWinAppPage) {
                            Ext.MessageBox.show({
                                title: '系統訊息',
                                icon: Ext.MessageBox.INFO,
                                buttons: Ext.Msg.OK,
                                msg: '未實現 subWinAppPage 物件,無法進行編輯操作!'
                            });
                            return false;
                        };
                        var subWin = Ext.create(main.subWinAppPage,{
                            param:{
                                uuid:undefined
                            }
                        });
                        subWin.on('closeEvent', main.fnCallBackReloadGrid, main);
                        subWin.show();
                    }
                }]
            }]
        }];
        this.callParent(arguments);
    },
    listeners:{
        afterrender:function(obj,eOpts){
            this.myStore.application.load({
                callback : function(obj) {
                    console.log(obj[0]);
                    if(obj.length>0){
                        this.down('#function_Query_Application').setValue(obj[0].data.UUID);
                        this.down('#btnQuery').handler(this.down('#btnQuery'));
                    };
                },
                scope:this
            })
        }
    }
});
