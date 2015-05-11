Ext.define('WS.CloudPanel', { 
    extend: 'Ext.form.Panel',
    closeAction: 'destroy',
    title: 'Cloud 設定',
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
            fieldLabel: '支援Cloud功能',
            queryMode: 'local',
            displayField: 'text',
            valueField: 'value',
            editable: false,
            hidden: false,
            value: false,
            itemId: 'SupportCloud',
            store: new Ext.data.ArrayStore({
                fields: ['text', 'value'],
                data: [
                    ['是', true],
                    ['否', false]
                ]
            }),
            listeners: {
                'select': function(combo, records, eOpts) {

                }
            },
            padding: '5 0 0 0',
            anchor: '-5 0'
        }, {
            xtype: 'combo',
            fieldLabel: 'Cloud角色',
            queryMode: 'local',
            itemId: 'Role',
            displayField: 'text',
            valueField: 'value',
            editable: false,
            hidden: false,
            value: 'member',
            //readOnly:true,
            store: new Ext.data.ArrayStore({
                fields: ['text', 'value'],
                data: [
                    ['成員', 'member'],
                    ['中心', 'master']
                ]
            }),
            listeners: {
                'select': function(combo, records, eOpts) {

                }
            },
            anchor: '-5 0'
        }, {
            xtype: 'combo',
            fieldLabel: '是認證中心',
            queryMode: 'local',
            displayField: 'text',
            valueField: 'value',
            itemId: 'IsAuthCenter',
            value: false,
            editable: false,
            hidden: false,
            store: new Ext.data.ArrayStore({
                fields: ['text', 'value'],
                data: [
                    ['是', true],
                    ['否', false]
                ]
            }),
            listeners: {
                'select': function(combo, records, eOpts) {

                }
            },
            anchor: '-5 0'
        }, {
            xtype: 'textfield',
            fieldLabel: '認證中心URL',
            allowBlank: true,
            itemId: 'AuthMaster',
            anchor: '-5 0'
        }, {
            xtype: 'combo',
            fieldLabel: '認證中心通訊方式',
            queryMode: 'local',
            displayField: 'text',
            valueField: 'value',
            value: 'http',
            editable: false,
            itemId: 'AuthCenterPrototype',
            hidden: false,
            store: new Ext.data.ArrayStore({
                fields: ['text', 'value'],
                data: [
                    ['http', 'http']
                ]
            }),
            listeners: {
                'select': function(combo, records, eOpts) {

                }
            },
            allowBlank: true,
            anchor: '-5 0'
        }, {
            xtype: 'textfield',
            fieldLabel: '認識中心主機名稱',
            allowBlank: true,
            anchor: '-5 0',
            itemId: 'AuthCenterHost'

        }, {
            xtype: 'textfield',
            fieldLabel: '認識中心主機IP',
            allowBlank: true,
            itemId: 'AuthCenterIP',
            anchor: '-5 0',
        }, {
            xtype: 'textfield',
            fieldLabel: '認識中心主機網站根目錄',
            allowBlank: true,
            anchor: '-5 0',
            itemId: 'AuthCenterWebRoot'
        }, {
            xtype: 'textfield',
            fieldLabel: '公鑰字串',
            allowBlank: true,
            anchor: '-5 0',
            itemId: 'CloudKeyPub'
        }, {
            xtype: 'fieldset',
            title: 'Slave(成員設定)',
            border: true,
            items: [{
                xtype: 'gridpanel',
                padding: 5,
                autoScroll: true,
                itemId: 'grdSlave',
                // tbar: [{
                //     type: 'button',
                //     text: '新增',
                //     handler: function() {

                //     }
                // }],
                columns: [{
                    text: "IP",
                    dataIndex: 'IP',
                    align: 'center',
                    flex: 1
                }, {
                    text: "有效性",
                    dataIndex: 'ACTIVE',
                    align: 'center',
                    width: 60,
                }],
                height: 270,
                listeners: {
                    cellclick: function(iView, iCellEl, iColIdx, iRecord, iRowEl, iRowIdx, iEvent) {

                    }
                }
            }]
        }, {
            xtype: 'fieldset',
            title: 'Twins(雙胞設定)',
            border: true,
            items: [{
                xtype: 'gridpanel',
                padding: 5,
                autoScroll: true,
                itemId: 'grdTwins',
                // tbar: [{
                //     type: 'button',
                //     text: '新增',
                //     //id : 'id',    
                //     handler: function() {

                //     }
                // }],
                columns: [{
                    text: "IP",
                    dataIndex: 'IP',
                    align: 'center',
                    flex: 1
                }, {
                    text: "有效性",
                    dataIndex: 'ACTIVE',
                    align: 'center',
                    width: 60,
                }],
                height: 270,
                listeners: {
                    cellclick: function(iView, iCellEl, iColIdx, iRecord, iRowEl, iRowIdx, iEvent) {

                    }
                }
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

                var mainPanel = this.up('panel'),
                    SupportCloud, Role, IsAuthCenter, AuthMaster, AuthCenterPrototype, AuthCenterHost, AuthCenterIP, AuthCenterWebRoot, CloudKeyPub;

                var Slave = Array(),
                    Twins = Array();

                SupportCloud = mainPanel.down('#SupportCloud').getValue(),
                    Role = mainPanel.down('#Role').getValue(),
                    IsAuthCenter = mainPanel.down('#IsAuthCenter').getValue(),
                    AuthMaster = mainPanel.down('#AuthMaster').getValue(),
                    AuthCenterPrototype = mainPanel.down('#AuthCenterPrototype').getValue(),
                    AuthCenterHost = mainPanel.down('#AuthCenterHost').getValue(),
                    AuthCenterIP = mainPanel.down('#AuthCenterIP').getValue(),
                    AuthCenterWebRoot = mainPanel.down('#AuthCenterWebRoot').getValue(),
                    CloudKeyPub = mainPanel.down('#CloudKeyPub').getValue();

                Ext.each(mainPanel.down('#grdSlave').getStore().data.items, function(item) {
                    Slave.push(item.data);
                });

                Ext.each(mainPanel.down('#grdTwins').getStore().data.items, function(item) {
                    Twins.push(item.data);
                });

                // console.log(Slave);
                // console.log(Twins);

                    
                WS.InitAction.submitCloud(SupportCloud, Role, IsAuthCenter, AuthMaster, AuthCenterPrototype, AuthCenterHost, AuthCenterIP, AuthCenterWebRoot, Slave, Twins, CloudKeyPub, function(obj, jsonObj) {
                    if (jsonObj.result.success) {
                        Ext.MessageBox.show({
                            title: '系統提示',
                            icon: Ext.MessageBox.INFO,
                            buttons: Ext.Msg.OK,
                            msg: '儲存完成!'
                        });
                    }
                });
            }
        }];
        this.callParent(arguments);
    },
    listeners: {
        afterrender: function(self, eOpts) {
            // WS.InitAction.loadSmtp(function(obj, jsonObj) {
            //     if (jsonObj.result.success) {
            //         var data = jsonObj.result.data[0];
            //         console.log(data);
            //         this.down("#SMTPSERVERHOST").setValue(data.SMTPSERVERHOST);
            //         this.down("#SMTPSERVERPORT").setValue(data.SMTPSERVERPORT);
            //         this.down("#ISSEND").setValue(data.ISSEND);
            //         this.down("#FROMEMAIL").setValue(data.FROMEMAIL);
            //         this.down("#ISSENDMAIL").setValue(data.ISSENDMAIL);
            //         this.down("#ISSENDADMINMAIL").setValue(data.ISSENDADMINMAIL);
            //         this.down("#ADMINISTRATOREMAIL").setValue(data.ADMINISTRATOREMAIL);
            //         this.down("#ISSENDDEBUGMAIL").setValue(data.ISSENDDEBUGMAIL);
            //         this.down("#DEBUGEMAIL").setValue(data.DEBUGEMAIL);
            //         this.down("#CREDENTIALSACCOUNT").setValue(data.CREDENTIALSACCOUNT);
            //         this.down("#CREDENTIALSPASSWORD").setValue(data.CREDENTIALSPASSWORD);
            //     }
            // }, this);

            WS.InitAction.loadCloud(function(obj, jsonObj) {

                if (jsonObj.result.success) {
                    var data = jsonObj.result.data[0];
                    this.down('#SupportCloud').setValue(data.SUPPORTCLOUD);
                    this.down('#Role').setValue(data.ROLE);
                    this.down('#IsAuthCenter').setValue(data.ISAUTHCENTER);
                    this.down('#AuthMaster').setValue(data.AUTHMASTER);
                    this.down('#AuthCenterPrototype').setValue(data.AUTHCENTERPROTOTYPE);
                    this.down('#AuthCenterHost').setValue(data.AUTHCENTERHOST);
                    this.down('#AuthCenterIP').setValue(data.AUTHCENTERIP);
                    this.down('#AuthCenterWebRoot').setValue(data.AUTHCENTERWEBROOT);
                    this.down('#CloudKeyPub').setValue(data.CLOUDKEYPUB);
                    this.myStore.slave = Ext.create('Ext.data.Store', {
                        fields: [{
                            name: 'IP',
                            type: 'string'
                        }, {
                            name: 'ACTIVE',
                            type: 'string'
                        }]
                    });

                    Ext.each(data.SLAVE, function(item) {
                        this.myStore.slave.add(item);
                    }, this);


                    this.down('#grdSlave').bindStore(this.myStore.slave);

                    this.myStore.twins = Ext.create('Ext.data.Store', {
                        fields: [{
                            name: 'IP',
                            type: 'string'
                        }, {
                            name: 'ACTIVE',
                            type: 'string'
                        }]
                    });

                    Ext.each(data.TWINS, function(item) {
                        this.myStore.twins.add(item);
                    }, this);


                    this.down('#grdTwins').bindStore(this.myStore.twins);

                }
                console.log(jsonObj);
            }, this);

        }
    }
});
