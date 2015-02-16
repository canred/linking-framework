/*columns 使用default*/
Ext.define('WS.LogoutPanel', {
    extend: 'Ext.panel.Panel',
    closeAction: 'destroy',
    val: {
        logonUrl: undefined
    },
    initComponent: function() {
        this.items = [{
            xtype: 'form',
            layout: 'form',
            frame: true,
            autoWidth: true,
            title: '系統登出',
            icon: SYSTEM_URL_ROOT + '/css/images/contact2.gif',
            bodyPadding: '5 5 5 5',
            defaultType: 'label',
            items: [{
                html: '您已完成登出系統。<BR>'
            }, {
                html: '感謝您的使用。<BR>'
            }, {
                html: '如果您對系統有任何建議，請透過Email與我們連聯(canred.chen@gmail.com)'
            }, {
                xtype: 'container',
                layout: {
                    type: 'hbox',
                    align: 'center',
                    pack: 'center'
                },
                items: [{
                    xtype: 'button',
                    text: '登入',
                    margin: '20 0 0 0',
                    width: 200,
                    handler: function() {
                        var _logonUrl = this.up('panel').up('panel').val.logonUrl;
                        if (Ext.isEmpty(_logonUrl) == true) {
                            Ext.MessageBox.show({
                                title: '系統提示',
                                icon: Ext.MessageBox.WARNING,
                                buttons: Ext.Msg.OK,
                                msg: '未設定val.logonUrl參數'
                            });
                        } else {
                            location.href = _logonUrl;
                        };
                    }
                }]
            }]
        }];
        this.callParent(arguments);
    }
});
