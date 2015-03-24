
Ext.define('Ist.view.UserMainSubPanel', {
    extend: 'Ext.form.Panel',
    xtype: 'UserMainSubPanel',
    config: {
        height: screen.height,
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
            }]
        }, {
            xtype: 'container',
            layout: 'vbox',
            width: '100%',
            items: [{
                xtype: 'container',
                layout: 'hbox',
                defaults: {
                    flex: 1,
                    height:150,
                    margin: 5
                },
                items: [{
                    xtype: 'button',
                    baseCls: 'queryBtn',
                    flex: 1,
                    action: 'btnCaseQuery',
                    handler: function(handler, scope) {}
                }, {
                    xtype: 'button',
                    baseCls: 'noticeBtn',
                    action: 'btnNoticeSetting',
                    flex: 1,
                    handler: function(handler, scope) {}
                }, {
                    xtype: 'button',
                    baseCls: 'analysisBtn',
                    action: 'btnAnalysis',
                    flex: 1,
                    handler: function(handler, scope) {}
                }]
            }, {
                xtype: 'container',
                layout: 'hbox',
                defaults: {
                    flex: 1,
                    height: 150,
                    margin: 5
                },
                items: [{
                    xtype: 'button',
                    baseCls: 'contactBtn',
                    flex: 1,
                    action: 'btnContact',
                    handler: function(handler, scope) {}
                }, {
                    xtype: 'button',
                    baseCls: 'questionBtn',
                    action: 'btnQuestion',
                    flex: 1,
                    handler: function(handler, scope) {}
                }, {
                    xtype: 'button',
                    baseCls: 'newsBtn',
                    action: 'btnNews',
                    flex: 1,
                    handler: function(handler, scope) {}
                }]
            }]
        }]
    }
});
