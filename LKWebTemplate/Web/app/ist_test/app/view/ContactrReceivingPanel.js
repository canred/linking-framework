Ext.define('Ist.view.ContactrReceivingPanel', {
    extend: 'Ext.form.Panel',
    xtype: 'ContactrReceivingPanel',
    requires: [],
    config: {
        layout: 'vbox',
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
                action: 'btnAnalysisBackToAyalysis',
                text: '返回'
            }]
        }, {
            xtype: 'fieldset',
            layout: 'hbox',
            flex: 1,
            items: [{
                xtype: 'container',
                layout: 'vbox',
                items: [{
                    styleHtmlContent: true,
                    html: '收送件時間週一至週五 08:30AM~9:00PM<BR>收送件專線 ：03-5799909  分機1111'
                }, {
                    styleHtmlContent: true,
                    html: '假日(含國定假日)收送件時間：08:30AM~5:00PM<BR>收送件專線：03-5799909  分機1111 或  0928 - 732111'
                }]
            }]
        }]
    }
});
