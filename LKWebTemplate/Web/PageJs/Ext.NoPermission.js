Ext.Loader.setConfig({
    enabled: true
});
Ext.Loader.setPath('Ext.ux', SYSTEM_ROOT_PATH + '/js/ux');
var ExtNoPermission;
Ext.onReady(function() {
    var bd = Ext.getBody();

    ExtNoPermission = Ext.widget({
        xtype: 'form',
        layout: 'form',
        collapsible: false,
        id: 'extNoPermission',
        logonUrl: '',
        frame: true,
        title: '<img src="/ghg/css/custimages/contact2.gif" style="height:16px;margin-bottom:8px;" align="middle">網頁存取被拒',
        bodyPadding: '5 5 5 5',
        width: 200,
        fieldDefaults: {
            msgTarget: 'side',
            labelWidth: 75
        },
        defaultType: 'label',
        items: [{
            html: '由於系統權限授權限的原因；可以造成您的困擾。你可以透過以下聯絡資訊確認你應有的權限，謝謝!'
        }, {
            html: '&nbsp;&nbsp;&nbsp;&nbsp;<img src="/ghg/css/custimages/man.gif" style="height:16px;margin-bottom:8px;" align="middle">:Canred Chen'
        }, {
            html: '&nbsp;&nbsp;&nbsp;&nbsp;<img src="/ghg/css/custimages/email.gif" style="height:16px;margin-bottom:8px;" align="middle">:canred.chen@gmail.com'
        }, {
            html: '&nbsp;&nbsp;&nbsp;&nbsp;<img src="/ghg/css/custimages/contact.gif" style="height:16px;margin-bottom:8px;" align="middle">: 3107'
        }, {
            xtype: 'container',
            layout: 'hbox',
            items: [{
                xtype: 'label',
                flex: 1
            }, {
                xtype: 'button',
                text: '重新登入',
                flex: 1,
                handler: function() {
                    location.href = this.up('form').logonUrl;
                }
            }, {
                xtype: 'label',
                flex: 1
            }]
        }]
    });
});
