Ext.define('WS.DirectRuleWindow', {
    extend: 'Ext.window.Window',
    title: '白名單維護',
    closeAction: 'destory',
    width: 400,
    height: 200,
    resizable: false,
    draggable: false,
    modal: true,
    param: {
        data: undefined,
        idx: undefined
    },
    defaults: {
        labelWidth: 50
    },
    padding: 5,
    layout: 'anchor',
    initComponent: function() {

        this.items = [{
            xtype: 'textfield',
            fieldLabel: '名稱',
            itemId: 'txtName',
            allowBlank: false,
            anchor: '-5'
        }, {
            xtype: 'textfield',
            fieldLabel: '網段',
            itemId: 'txtIp',
            allowBlank: false,
            anchor: '-5'
        }];
        this.callParent(arguments);
    },
    buttons: [{
        xtype: 'button',
        text: 'OK',
        handler: function(handler, scope) {

            var mainWin = this.up('window'),
                name, ip;
            name = mainWin.down('#txtName').getValue();
            ip = mainWin.down('#txtIp').getValue();
            mainWin.fireEvent('closeEvent', mainWin, mainWin.param.idx, name, ip);
            mainWin.close();

        }
    }, {
        xtype: 'button',
        text: 'Close',
        handler: function(handler, scope) {
            this.up('window').close();
        }
    }],
    listeners: {
        'afterrender': function() {
            if (this.param.data) {               
                this.down('#txtName').setValue(this.param.data.NAME);
                this.down('#txtIp').setValue(this.param.data.IP);
            };
        }
    }
});

Ext.define('WS.DirectPanel', {
    extend: 'Ext.form.Panel',
    closeAction: 'destroy',
    title: 'Direct功能設定',
    icon: SYSTEM_URL_ROOT + '/css/images/mail16x16.png',
    border: true,
    frame: true,
    autoWidth: true,
    autoHeight: true,
    autoScroll: true,
    defaults: {
        labelWidth: 150
    },
    layout: {
        type: 'anchor'
    },
    myStore: {},
    initComponent: function() {
        this.items = [{
            xtype: 'combo',
            fieldLabel: '開啟跨網支援',
            queryMode: 'local',
            displayField: 'text',
            valueField: 'value',
            editable: false,
            hidden: false,
            value: true,
            itemId: 'AllowCrossPost',
            store: new Ext.data.ArrayStore({
                fields: ['text', 'value'],
                data: [
                    ['是', true],
                    ['否', false]
                ]
            }),
            padding: '5 0 0 0',
            anchor: '-5 0'
        }, {
            xtype: 'combo',
            fieldLabel: '跨網支援方式',
            queryMode: 'local',
            displayField: 'text',
            valueField: 'value',
            editable: false,
            hidden: false,
            value: true,
            itemId: 'access_all',
            store: new Ext.data.ArrayStore({
                fields: ['text', 'value'],
                data: [
                    ['完成開放', true],
                    ['限定開放', false]
                ]
            }),
            padding: '5 0 0 0',
            anchor: '-5 0'
        }, {
            xtype: 'combo',
            fieldLabel: '開啟Proxy權限卡控',
            queryMode: 'local',
            itemId: 'ProxyPermission',
            displayField: 'text',
            valueField: 'value',
            editable: false,
            hidden: false,
            value: false,
            store: new Ext.data.ArrayStore({
                fields: ['text', 'value'],
                data: [
                    ['是', true],
                    ['否', false]
                ]
            }),
            anchor: '-5 0'
        }, {
            xtype: 'fieldset',
            title: '跨網支援(白名單)',
            anchor: '-5 0',
            border: true,
            items: [{
                xtype: 'gridpanel',
                padding: '5 5 5 5',
                autoScroll: true,
                itemId: 'grdDirect',
                tbar: [{
                    type: 'button',
                    text: '新增',
                    handler: function() {
                        var main = this.up('panel').up('panel');
                        var subWin = Ext.create('WS.DirectRuleWindow', {
                            param: {
                                parentObj: main
                            }
                        });
                        subWin.on('closeEvent', function(openerWin, idx, name, ip) {
                            var s = openerWin.param.parentObj.myStore.direct;
                            s.add({
                                'NAME': name,
                                'IP': ip
                            });
                        });
                        subWin.show();
                    }
                }],
                columns: [{
                    text: "編輯",
                    xtype: 'actioncolumn',
                    dataIndex: 'UUID',
                    align: 'center',
                    width: 60,
                    items: [{
                        tooltip: '*編輯',
                        icon: SYSTEM_URL_ROOT + '/css/custImages/edit.gif',
                        handler: function(grid, rowIndex, colIndex) {
                            var main = grid.up('panel').up('panel');
                            var drIndex = main.myStore.direct.findBy(function(record) {
                                if (record.get("NAME") === grid.getStore().getAt(rowIndex).data.NAME &&
                                    record.get("IP") === grid.getStore().getAt(rowIndex).data.IP) {
                                    return true;
                                } else {
                                    return false;
                                }
                            });
                            var data = undefined;
                            if (main.myStore.direct.getAt(drIndex)) {
                                data = main.myStore.direct.getAt(drIndex).data;
                            } else {
                                return;
                            };
                            var subWin = Ext.create('WS.DirectRuleWindow', {
                                param: {
                                    parentObj: main,
                                    data: data,
                                    idx: drIndex
                                }
                            });
                            subWin.on('closeEvent', function(openerWin, idx, name, ip) {
                                var s = openerWin.param.parentObj.myStore.direct;
                                var d = s.getAt(idx);
                                d.set('NAME', name);
                                d.set('IP', ip);
                            });
                            subWin.show();
                        }
                    }],
                    sortable: false,
                    hideable: false
                }, {
                    text: "刪除",
                    xtype: 'actioncolumn',
                    dataIndex: 'UUID',
                    align: 'center',
                    width: 60,
                    items: [{
                        tooltip: '*刪除',
                        icon: SYSTEM_URL_ROOT + '/css/custImages/delete.gif',
                        handler: function(grid, rowIndex, colIndex) {
                            var main = grid.up('panel').up('panel');
                            Ext.MessageBox.confirm('刪除提示', '是否刪除此筆資料?', function(result) {
                                if (result == 'yes') {
                                    var drIndex = this.myStore.direct.findBy(function(record) {
                                        if (record.get("NAME") === grid.getStore().getAt(rowIndex).data.NAME &&
                                            record.get("IP") === grid.getStore().getAt(rowIndex).data.IP) {
                                            return true;
                                        } else {
                                            return false;
                                        }
                                    });
                                    this.myStore.direct.removeAt(drIndex);
                                }
                            }, main);
                        }
                    }],
                    sortable: false,
                    hideable: false
                }, {
                    text: "NAME",
                    dataIndex: 'NAME',
                    align: 'left',
                    width: 150
                }, {
                    text: "IP",
                    dataIndex: 'IP',
                    align: 'left',
                    flex: 1
                }],
                height: 270
            }]
        }];
        this.buttonAlign = 'center';
        this.buttons = [{
            xtype: 'button',
            text: '儲存',
            icon: SYSTEM_URL_ROOT + '/css/custimages/save16x16.png',
            handler: function(handler, scope) {
                if (this.up('panel').getForm().isValid() == false) {
                    return;
                };
                // var mainPanel = this.up('panel'),
                //     SupportCloud, Role, IsAuthCenter, AuthMaster, AuthCenterPrototype, AuthCenterHost, AuthCenterIP, AuthCenterWebRoot, CloudKeyPub;
                
                // var Slave = Array(),
                //     Twins = Array();

                // SupportCloud = mainPanel.down('#SupportCloud').getValue(),
                //     Role = mainPanel.down('#Role').getValue(),
                //     IsAuthCenter = mainPanel.down('#IsAuthCenter').getValue(),
                //     AuthMaster = mainPanel.down('#AuthMaster').getValue(),
                //     AuthCenterPrototype = mainPanel.down('#AuthCenterPrototype').getValue(),
                //     AuthCenterHost = mainPanel.down('#AuthCenterHost').getValue(),
                //     AuthCenterIP = mainPanel.down('#AuthCenterIP').getValue(),
                //     AuthCenterWebRoot = mainPanel.down('#AuthCenterWebRoot').getValue(),
                //     CloudKeyPub = mainPanel.down('#CloudKeyPub').getValue();

                // Ext.each(mainPanel.down('#grdDirect').getStore().data.items, function(item) {
                //     Slave.push(item.data);
                // });

                // Ext.each(mainPanel.down('#grdTwins').getStore().data.items, function(item) {
                //     Twins.push(item.data);
                // });

                // WS.InitAction.submitCloud(SupportCloud, Role, IsAuthCenter, AuthMaster, AuthCenterPrototype, AuthCenterHost, AuthCenterIP, AuthCenterWebRoot, Slave, Twins, CloudKeyPub, function(obj, jsonObj) {
                //     if (jsonObj.result.success) {
                //         Ext.MessageBox.show({
                //             title: '系統提示',
                //             icon: Ext.MessageBox.INFO,
                //             buttons: Ext.Msg.OK,
                //             msg: '儲存完成!'
                //         });
                //     }
                // });
            }
        }];
        this.callParent(arguments);
    },
    listeners: {
        afterrender: function(self, eOpts) {
            WS.InitAction.loadDirect(function(obj, jsonObj) {
                if (jsonObj.result.success) {
                    var data = jsonObj.result.data[0];
                    this.down('#AllowCrossPost').setValue(data.ALLOWCROSSPOST);
                    this.down('#ProxyPermission').setValue(data.PROXYPERMISSION);
                    this.down('#access_all').setValue(data.ACCESS_ALL);
                    this.myStore.direct = Ext.create('Ext.data.Store', {
                        fields: [{
                            name: 'IP',
                            type: 'string'
                        }, {
                            name: 'NAME',
                            type: 'string'
                        }]
                    });
                    Ext.each(data.RULE, function(item1) {
                        Ext.each(item1.dsource, function(item2) {
                            this.myStore.direct.add({
                                NAME: item1.name,
                                IP: item2.IP
                            });
                        }, this);
                    }, this);
                    this.down('#grdDirect').bindStore(this.myStore.direct);
                }
                console.log(jsonObj);
            }, this);
        }
    }
});
