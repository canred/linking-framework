Ext.define('Ist.view.AnalysisPanel', { 
    extend: 'Ext.form.Panel',
    xtype: 'AnalysisPanel',
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
                action: 'btnAnalysisToMenu',
                text: '返回'
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
                    height: (screen.height - 190) / 3,
                    margin: 1
                },
                items: [{
                    xtype: 'button',
                    baseCls: 'queryAnalysisIc',
                    flex: 1,
                    action: 'btnIcAnalysisIc',
                    handler: function(handler, scope) {
                        //your code
                    }
                }, {
                    xtype: 'button',
                    baseCls: 'queryAnalysisIc2',
                    action: 'btnIcAnalysisIc2',
                    flex: 1,
                    handler: function(handler, scope) {
                        //your code
                    }
                }, {
                    xtype: 'button',
                    baseCls: 'queryAnalysisIc3',
                    action: 'btnIcAnalysisIc3',
                    flex: 1,
                    handler: function(handler, scope) {
                        //your code
                    }
                }]
            }, {
                xtype: 'container',
                layout: 'hbox',
                defaults: {
                    flex: 1,
                    height: (screen.height - 190) / 3,
                    margin: 1
                },
                items: [{
                    xtype: 'button',
                    baseCls: 'queryAnalysisIc4',
                    flex: 1,
                    action: 'btnIcAnalysisIc4',
                    handler: function(handler, scope) {
                        //your code
                    }
                }, {
                    xtype: 'button',
                    baseCls: 'queryAnalysisIc5',
                    action: 'btnIcAnalysisIc5',
                    flex: 1,
                    handler: function(handler, scope) {
                        //your code
                    }
                }, {
                    xtype: 'button',
                    baseCls: 'queryAnalysisIc6',
                    action: 'btnIcAnalysisIc6',
                    flex: 1,
                    handler: function(handler, scope) {
                        //your code
                    }
                }]
            }, {
                xtype: 'container',
                layout: 'hbox',
                defaults: {
                    flex: 1,
                    height: (screen.height - 190) / 3,
                    margin: 1
                },
                items: [{
                    xtype: 'button',
                    baseCls: 'queryAnalysisIc7',
                    flex: 1,
                    action: 'btnIcAnalysisIc7',
                    handler: function(handler, scope) {
                        //your code
                    }
                }, {
                    xtype: 'button',
                    baseCls: 'queryAnalysisIc8',
                    action: 'btnIcAnalysisIc8',
                    flex: 1,
                    handler: function(handler, scope) {
                        //your code
                    }
                }, {
                    xtype: 'button',
                    baseCls: 'queryAnalysisIc9',
                    action: 'btnIcAnalysisIc9',
                    flex: 1,
                    handler: function(handler, scope) {
                        //your code
                    }
                }]
            }]
        }]
    }
});
