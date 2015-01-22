Ext.Loader.setConfig({
    enabled: true
});
Ext.Loader.setPath('Ext.ux', SYSTEM_ROOT_PATH + '/js/ux');
Ext.require(['*', 'Ext.ux.DataTip']);
Ext.onReady(function() {
    Ext.QuickTips.init();
    var bd = Ext.getBody();
    var simple = Ext.widget({
        xtype: 'form',
        layout: 'form',
        collapsible: true,
        id: 'systemInfoForm',
        frame: true,
        title: '<img src="' + SYSTEM_ROOT_PATH + '/css/custimages/contact2.gif" style="height:16px;margin-bottom:8px;" align="middle">  系統連聯人員',
        bodyPadding: '5 5 5 5',
        width: 200,
        fieldDefaults: {
            msgTarget: 'side',
            labelWidth: 75
        },
        plugins: {
            ptype: 'datatip'
        },
        defaultType: 'label',
        items: [{
            html: '<img src="' + SYSTEM_ROOT_PATH + '/css/custimages/app.gif" style="height:16px;margin-bottom:6px;" align="middle">系統相關'
        }, {
            html: '&nbsp;&nbsp;&nbsp;&nbsp;<img src="' + SYSTEM_ROOT_PATH + '/css/custimages/man.gif" style="height:16px;margin-bottom:8px;" align="middle">:Canred Chen'
        }, {
            html: '&nbsp;&nbsp;&nbsp;&nbsp;<img src="' + SYSTEM_ROOT_PATH + '/css/custimages/email.gif" style="height:16px;margin-bottom:8px;" align="middle">:canred.chen@gmail.com'
        }, {
            html: '&nbsp;&nbsp;&nbsp;&nbsp;<img src="' + SYSTEM_ROOT_PATH + '/css/custimages/contact.gif" style="height:16px;margin-bottom:8px;" align="middle">: 3107'
        }],
        buttons: [{
            text: 'Contanct us',
            handler: function() {
                location.href = "mailto:canred.chen@gmail.com";
            }
        }]
    });
    simple.render('systemInfo');
});
