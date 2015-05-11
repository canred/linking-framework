Ext.define('WS.JoinUsPanel', {
    extend: 'Ext.panel.Panel',
    closeAction: 'destroy',
    width: $("#header").width() - 16,
    autoHeight: true,
    border: false,
    initComponent: function() {
        this.items = [{
            xtype: 'tabpanel',
            plain: true,
            border: false,
            autoWidth: true,
            bodyStyle: '{"background-color":"#D1DFF0"; text-align: center;}',
            items: [{
                title: '註冊前的配置',
                icon: SYSTEM_URL_ROOT + '/css/images/warning16x16.png',
                items: [{
                    xtype: 'image',
                    src: SYSTEM_URL_ROOT + '/css/images/CloudJoinUs.png',
                    tag: "img",
                    height: 600
                }],
                border: false
            }, {
                title: '驗証我的配置',
                icon: SYSTEM_URL_ROOT + '/css/images/check16x16.png',
                items: [{
                    xtype: 'container',
                    layout: 'hbox',
                    items: [{
                        xtype: 'textfield',
                        fieldLabel: '輸入網站位置',
                        labelAlign: 'right',
                        width: 600,
                        id: 'txtServerWebUrl',
                        value: 'http://127.0.0.1/LKWebTemplate',
                        emptyText: 'http://127.0.0.1/LKWebTemplate'
                    }, {
                        xtype: 'button',
                        text: '測試',
                        icon: SYSTEM_URL_ROOT + '/css/images/connect16x16.png',
                        margin: '0 5 0 5',
                        handler: function(handler, scope) {
                            var serverWebUrl = Ext.getCmp('txtServerWebUrl').getValue();
                            WS.CloudAction.connectTest(serverWebUrl, function(obj, jsonObj) {
                                if (jsonObj.result.success) {
                                    Ext.getCmp('tab3').setDisabled(false);
                                    var message = "";
                                    message = "身份認證中心已提出連線請求";
                                    message += "<BR>從節點伺服器(" + serverWebUrl + ")";
                                    message += "<BR>也同意這一個請求!";
                                    message += "<BR>連線測試成功!";
                                    Ext.MessageBox.show({
                                        title: 'Connect Test',
                                        icon: Ext.MessageBox.INFO,
                                        buttons: Ext.Msg.OK,
                                        msg: message
                                    });
                                } else {
                                    Ext.getCmp('tab3').setDisabled(true);
                                    var message = jsonObj.result.message;
                                    message += "<BR>請確定你的跨網域提交功能是否開啟!";
                                    message += "<BR>1.DirectAuth的權限";
                                    message += "<BR>2.伺服器設定要允可option的提交!";
                                    Ext.MessageBox.show({
                                        title: 'Warning',
                                        icon: Ext.MessageBox.INFO,
                                        buttons: Ext.Msg.OK,
                                        msg: message
                                    });
                                }
                            });
                        }
                    }]
                }, {
                    xtype: 'image',
                    src: SYSTEM_URL_ROOT + '/css/images/communication.png',
                    tag: "img",
                    height: 200,
                    width: 400
                }]
            }, {
                title: '執行註冊',
                icon: SYSTEM_URL_ROOT + '/css/images/join16x16.png',
                id: 'tab3',
                disabled: true,
                height: 170,
                items: [{
                    xtype: 'panel',
                    border: false,
                    height: 150,
                    width: $("#header").width() - 50,
                    items: [{
                        xtype: 'button',
                        icon: SYSTEM_URL_ROOT + '/css/images/join16x16.png',
                        text: 'Regist Now',
                        margin: '10 0 0 110',
                        handler: function(handler, scope) {
                            var serverWebUrl = Ext.getCmp('txtServerWebUrl').getValue();
                            Ext.getCmp('txtResult').setValue("--------初始設定--------");
                            WS.CloudAction.settingCloud(serverWebUrl, function(obj, jsonObj) {
                                if (jsonObj.result.success) {
                                    Ext.getCmp('txtResult').setValue(Ext.getCmp('txtResult').getValue() + "\n主伺服器設定完成!");
                                    WS.CloudAction.settingSlave(serverWebUrl, function(obj, jsonObj2) {
                                        if (jsonObj2.result.success) {
                                            Ext.getCmp('txtResult').setValue(Ext.getCmp('txtResult').getValue() + "\n從伺服器設定完成!");
                                            Ext.getCmp('txtResult').setValue(Ext.getCmp('txtResult').getValue() + "\n--------設定完成--------");
                                        } else {
                                            Ext.getCmp('txtResult').setValue(Ext.getCmp('txtResult').getValue() + jsonObj2.result.message);
                                        }
                                    });
                                } else {
                                    Ext.getCmp('txtResult').setValue(Ext.getCmp('txtResult').getValue() + "\n" + jsonObj.result.message);
                                }
                            });
                        }
                    }, {
                        xtype: 'textarea',
                        fieldLabel: '註冊結果',
                        labelAlign: 'right',
                        id: 'txtResult',
                        width: $("#header").width() - 50,
                        height: 300,
                        margin: '10 0 0 0',
                        readOnly: true
                    }]
                }]
            }, {
                title: '功能說明',
                icon: SYSTEM_URL_ROOT + '/css/images/doc16x16.png',
                height: 2000,
                autoScroll: false,
                items: [{
                        xtype: 'image',
                        src: '../css/images/CloudDesc.png',
                        tag: "img",
                        width: 800,
                        height: 1700
                    }
                ]
            }]
        }];
        this.callParent(arguments);
    }
});
