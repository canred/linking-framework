Ext.Loader.setConfig({
    enabled: true
});
Ext.Loader.setPath('Ext.ux', SYSTEM_ROOT_PATH + '/js/ux');

Ext.require(['*', 'Ext.ux.DataTip']);
var ExtLogon;
Ext.onReady(function () {
    Ext.QuickTips.init();
    var bd = Ext.getBody();
    var required = '<span style="color:red;font-weight:bold" data-qtip="Required">*</span>';
    ExtLogon = Ext.widget({
        api: {
            submit: WS.UserAction.logon
        },
        xtype: 'form',
        layout: 'form',
        collapsible: true,
        id: 'ExtLogonForm',
        url: '/User/ajaxLogon.aspx',
        urlSuccess: '',
        urlFail: '',
        frame: true,
        title: '<img src="' + SYSTEM_ROOT_PATH + '/css/custimages/login.gif" style="height:16px;margin-bottom:6px;" align="middle">登入',
        bodyPadding: '5 5 5 5',
        width: 200,
        fieldDefaults: {
            msgTarget: 'side',
            labelWidth: 75
        },
        plugins: {
            ptype: 'datatip'
        },
        defaultType: 'textfield',
        items: [{
            fieldLabel: 'Company',
            afterLabelTextTpl: required,
            name: 'company',
            allowBlank: false,
            tooltip: '*Enter your Company'
        }, {
            fieldLabel: 'Account',
            afterLabelTextTpl: required,
            name: 'account',
            allowBlank: false,
            tooltip: '*Enter your account'
        }, {
            inputType: 'password',
            fieldLabel: 'Password',
            afterLabelTextTpl: required,
            name: 'password',
            allowBlank: false,
            tooltip: '*Enter your password'
        }],
        buttons: [{
            text: '登入',
            handler: function () {
                this.up('form').getForm().isValid();
                var urlSuccess = this.up('form').urlSuccess;
                var urlFail = this.up('form').urlFail;

                ExtLogon.getForm().submit({
                    success: function (f, a) {
                        if (a.result.message == 'ok') {
                            location.href = urlSuccess;
                        } else {
                            if (urlFail == '') {
                                Ext.Msg.alert('Logon Failure', '請檢查您的帳號密碼是否正確。');
                            } else {
                                location.href = urlFail;
                            }
                        }
                    },
                    failure: function (f, a) {
                        if (a.failureType === Ext.form.Action.CONNECT_FAILURE) {
                            Ext.Msg.alert('Failure', 'Server reported:' + a.response.status + ' ' + a.response.statusText);
                        }
                        if (a.failureType === Ext.form.Action.SERVER_INVALID) {
                            Ext.Msg.alert('Warning', a.result.errormsg);
                        }
                    }
                });

            }
        }]
    });

});