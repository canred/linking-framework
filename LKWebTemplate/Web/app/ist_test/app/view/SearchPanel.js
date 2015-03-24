Ext.define('Ist.view.SearchPanel', {
    extend: 'Ext.form.Panel',
    xtype: 'SearchPanel',
    config: {
        height:screen.height,
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
            }]
        }, {
            xtype: 'panel',
            items: [{
                xtype: 'fieldset',
                border: false,
                items: [{
                    xtype: 'searchfield',
                    fieldLabel: '關鍵字',
                    listeners: {
                        'change': function() {
                            alert('search');
                        }
                    }
                }]
            }]
        }]
    }
});
