Ext.define('Ist.view.LoginPanel', {
    extend: 'Ext.form.Panel',
    xtype: 'LoginPanel',
    itemId: 'LoginPanel',
    requires: [],
    paramsAsHash: true,
    config: {
        height:screen.height,
        scrollable: false,
        api: {
            submit: 'UserAction.logon'
        },
        items: [{
            title: '首頁',
            iconCls: 'home',
            styleHtmlContent: true,
            scrollable: true,
            items: [{
                docked: 'top',
                xtype: 'toolbar',
                items: [{
                    xtype: 'label',
                    html: '<img height="30px" style="vertical-align:middle;" src="./resources/css/images/istlogo.gif"/>    宜特科技股份有限公司',
                    style: {
                        'text-align': 'left'
                    }
                }, {
                    xtype: 'label',
                    flex: 1
                }]
            }]
        }, {
            xtype: 'fieldset',
            defaults: {
                labelWidth: '35%'
            },
            items: [{
                xtype: 'textfield',
                name: 'company',
                label: 'company',
                id: 'txtCompany',
                placeHolder: 'EX:sample@istgroup.com',
                autoCapitalize: true,
                required: true,
                clearIcon: true,
                value: 'myCompany'

            }, {
                xtype: 'textfield',
                name: 'account',
                label: 'Account',
                id: 'txtAccount',
                placeHolder: 'EX:sample@istgroup.com',
                autoCapitalize: true,
                required: true,
                clearIcon: true,
                value: 'myAdmin'

            }, {
                xtype: 'passwordfield',
                name: 'password',
                label: 'Password',
                clearIcon: true,
                value: 'myAdmin'
            }]
        }, {
            xtype: 'container',
            defaults: {
                xtype: 'button',
                style: 'margin: .5em',
                flex: 1
            },
            items: [{
                text: 'Login',
                scope: this,
                hasDisabled: false,
                ui: 'action',
                action: 'SubmitAccountPassword',
                flex: 2,
                handler: function(btn) {

                }
            }, {
                text: 'Forget Password',
                ui: 'decline',
                flex: 2,
                action: 'ForgetPassword',
                handler: function() {

                }
            }]
        }]
    }
});
