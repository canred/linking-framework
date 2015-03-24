Ext.define('Ist.view.NoticeSettingPanel', {
    extend: 'Ext.form.Panel',
    xtype: 'NoticeSettingPanel',
    scrollable: false,
    requires: [],
    config: {
        scrollable: false,
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
            }, {
                xtype: 'button',
                align: 'right',
                ui: 'back',
                action: 'NoticeSettingPanelToMain',
                text: '返回'
            }]
        }, {
            xtype: 'fieldset',
            defaults: {
                labelWidth: 200
            },
            items: [{
                xtype:'togglefield',
                label:'案件交期更新通知',
                value:1
            }, {
                xtype:'togglefield',
                label:'送件通知',
                value:1
            }, {
                xtype:'togglefield',
                label:'回貨通知',
                value:1
            }]
        }]
    }
});
