var NOPERMISSIONPANEL;
Ext.define('WS.NoPermissionPanel', {
    extend: 'Ext.panel.Panel',
    closeAction: 'destroy',
    defaultType: 'label',
    logonUrl: '',
    frame: true,
    title: '<img src="' + SYSTEM_URL_ROOT + '/css/custimages/contact2.gif" style="height:16px;margin-bottom:8px;" align="middle">網頁存取被拒',
    bodyPadding: '5 5 5 5',
    width: 200,
    fieldDefaults: {
        msgTarget: 'side',
        labelWidth: 75
    },
    initComponent: function() {
        this.items = [{
            html: '由於權限不足原因；可以造成您的困擾。你可以透過以下聯絡資訊確認你應有的權限，謝謝!'
        }, {
            html: '&nbsp;&nbsp;&nbsp;&nbsp;<img src="' + SYSTEM_URL_ROOT + '/css/custimages/man.gif" style="height:16px;margin-bottom:8px;" align="middle">:Canred Chen'
        }, {
            html: '&nbsp;&nbsp;&nbsp;&nbsp;<img src="' + SYSTEM_URL_ROOT + '/css/custimages/email.gif" style="height:16px;margin-bottom:8px;" align="middle">:canred.chen@gmail.com'
        }, {
            html: '&nbsp;&nbsp;&nbsp;&nbsp;<img src="' + SYSTEM_URL_ROOT + '/css/custimages/contact.gif" style="height:16px;margin-bottom:8px;" align="middle">: 0982650501'
        }, {
            xtype: 'container',
            layout: 'hbox',
            height: 60,
            items: [{
                xtype: 'label',
                flex: 1
            }, {
                xtype: 'button',
                text: '重新登入',
                flex: 1,
                handler: function() {
                    location.href = this.up('panel').logonUrl;
                }
            }, {
                xtype: 'label',
                flex: 1
            }]
        }];
        this.callParent(arguments);
    }
});
