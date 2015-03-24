/*WS.ChangeAttendantWindow*/
/*TODO*/
/*
1.Model 要集中                                 [NO]
2.panel 的title要換成icon , title的方式        [YES]
3.add 的icon要換成icon , title的方式           [YES]
*/
/*columns 使用default*/
var WS_CHANGEATTENDANTWINDOW;
Ext.define('WS.ChangeAttendantWindow', {
    extend: 'Ext.window.Window',
    title: '【管理員工具】身份切換',
    icon: SYSTEM_URL_ROOT+'/css/images/people16x16.png',
    closeAction: 'destroy',
    width: 800,
    height: 600,
    resizable: false,
    draggable: false,
    closable: false,
    param: {
        successDirectToUrl: undefined
    },
    fnActiveRender: function(value, id, r) {
        var html = "<img src='" + SYSTEM_URL_ROOT;
        return value === "Y" ? html + "/css/custimages/active03.png'>" : html + "/css/custimages/unactive03.png'>";
    },
    myStore: {
        attendantV: Ext.create('Ext.data.Store', {
            successProperty: 'success',
            autoLoad: false,
            model: 'ATTEDNANTVV',
            pageSize: 10,
            proxy: {
                type: 'direct',
                api: {
                    read: WS.AttendantAction.loadAnyWhere
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
                simpleSortMode: true
            },
            remoteSort: true,
            sorters: [{
                property: 'C_NAME',
                direction: 'ASC'
            }]
        })
    },
    initComponent: function() {
        if (this.param.successDirectToUrl === undefined) {
            Ext.MessageBox.show({
                title: '系統提示',
                icon: Ext.MessageBox.INFO,
                buttons: Ext.Msg.OK,
                msg: '參數設定錯誤，初始化需要 successDirectToUrl '
            });
            return;
        };
        this.items = [{
            xtype: 'panel',
            padding: 5,
            items: [{
                xtype: 'container',
                layout: 'hbox',
                items: [{
                    xtype: 'combo',
                    fieldLabel: '公司',
                    displayField: 'C_NAME',
                    valueField: 'UUID',
                    labelAlign: 'right',
                    itemId: 'cmbCompany',
                    editable: false,
                    hidden: false,
                    store: Ext.create('Ext.data.Store', {
                        extend: 'Ext.data.Store',
                        autoLoad: false,
                        model: Ext.define('VGhgFile', {
                            extend: 'Ext.data.Model',
                            fields: ['C_NAME', 'UUID']
                        }),
                        pageSize: 999,
                        proxy: {
                            type: 'direct',
                            api: {
                                read: WS.CompanyAction.getAllCompany
                            },
                            reader: {
                                root: 'data'
                            },
                            paramsAsHash: true,
                            paramOrder: ['page', 'limit', 'sort', 'dir'],
                            extraParams: {
                                'page': 1,
                                'limit': 9999,
                                'sort': 'C_NAME',
                                'dir': 'ASC'
                            },
                            simpleSortMode: true,
                            listeners: {
                                exception: function(proxy, response, operation) {
                                    var errMsg = response.result.message;
                                    if (!errMsg){
                                        errMsg = 'System error 1501292211';
                                    };
                                    Ext.MessageBox.show({
                                        title: 'REMOTE EXCEPTON-1501292212',
                                        msg: errMsg,
                                        icon: Ext.MessageBox.ERROR,
                                        buttons: Ext.Msg.OK
                                    });
                                }
                            }
                        },
                        sorters: [{
                            property: 'C_NAME',
                            direction: 'ASC'
                        }]
                    }),
                    listeners: {
                        change: function(obj) {
                            obj.up('window').down("#btnQuery").handler();
                        }
                    }
                },  {
                    xtype: 'textfield',
                    itemId: 'txt_search',
                    fieldLabel:'關鍵字',
                    labelAlign:'right',
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
                    margin:'0 0 0 20',
                    icon: SYSTEM_URL_ROOT + '/css/custimages/find.png',
                    itemId: 'btnQuery',
                    width: 80,
                    handler: function() {
                        var mainWin = this.up('window'),
                            _txtSearch = mainWin.down("#txt_search").getValue(),
                            _comapny = mainWin.down("#cmbCompany").getValue();
                        if (Ext.isEmpty(_comapny)) {
                            Ext.MessageBox.show({
                                title: '系統提示',
                                icon: Ext.MessageBox.INFO,
                                buttons: Ext.Msg.OK,
                                msg: '請先選擇公司!'
                            });
                            return false;
                        };
                        mainWin.myStore.attendantV.getProxy().setExtraParam('company_uuid', _comapny);
                        mainWin.myStore.attendantV.getProxy().setExtraParam('keyword', _txtSearch);
                        mainWin.myStore.attendantV.load();
                    }
                }, {
                    xtype: 'button',
                    icon: SYSTEM_URL_ROOT + '/css/custimages/clear.png',
                    text: '清除',
                    width: 80,
                    margin:'0 0 0 20',
                    handler: function() {
                        this.up('window').down("#txt_search").setValue('');
                    }
                }]
            }, {
                xtype: 'gridpanel',
                store: this.myStore.attendantV,
                padding: 5,
                columns: [{
                    text: "切換",
                    xtype: 'actioncolumn',
                    dataIndex: 'UUID',
                    align: 'center',
                    width: 60,
                    items: [{
                        tooltip: '*切換身份',
                        icon: SYSTEM_URL_ROOT+'/css/images/people16x16.png',
                        handler: function(grid, rowIndex, colIndex) {
                            var companyUuid = grid.up('window').down("#cmbCompany").getValue(),
                                attendantUuid = grid.getStore().getAt(rowIndex).data.ACCOUNT;
                            WS.UserAction.changeAccount(companyUuid, attendantUuid, function(josnObj) {
                                if (josnObj.success != undefined) {
                                    location.href = this.param.successDirectToUrl;
                                };
                            }, grid.up('window'));
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
                height: 520,
                bbar: Ext.create('Ext.toolbar.Paging', {
                    store: this.myStore.attendantV,
                    displayInfo: true,
                    displayMsg: '第{0}~{1}資料/共{2}筆',
                    emptyMsg: "無資料顯示"
                })
            }]
        }];
        this.callParent(arguments);
    },    
    listeners: {      
        'afterrender': function() {            
            Ext.getBody().mask();
        }
    }
});
