Ext.define('Ist.view.ContactPanel', {
    extend: 'Ext.form.Panel',
    xtype: 'ContactPanel',
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
            items: [Ext.create('Ext.NestedList', {
                fullscreen: true,
                listeners: {
                    leafitemtap: function(self, list, index, target, record, e, eOpts) {

                        Ext.Viewport.getLayout().setAnimation({
                            type: 'reveal',
                            direction: 'right'
                        });

                        if (record.data.text == '收送件') {
                            if (!Ext.Viewport.down('ContactrReceivingPanel')) {
                                var view = Ext.create('Ist.view.ContactrReceivingPanel');
                                Ext.Viewport.setActiveItem(view);

                            } else {
                                Ext.Viewport.setActiveItem(Ext.Viewport.down('ContactrReceivingPanel'));
                            };
                        } else if (record.data.text == '工程') {
                            if (!Ext.Viewport.down('ContactEngPanel')) {
                                var view = Ext.create('Ist.view.ContactEngPanel');
                                Ext.Viewport.setActiveItem(view);

                            } else {
                                Ext.Viewport.setActiveItem(Ext.Viewport.down('ContactEngPanel'));
                            };
                        } else {
                            Ext.Msg.alert({
                                title: '通知',
                                message: '功能開發中…',
                                autoDestroy: true
                            });
                        }
                    }
                },
                title: '聯絡窗口',
                getItemTextTpl: function(node) {
                    return "<img style='height:30px;vertical-align:middle' src='./resources/css/images/radio.png'></img>&nbsp;&nbsp;{text}";
                },
                store: Ext.create('Ext.data.TreeStore', {
                    model: Ext.define('ListItem', {
                        extend: 'Ext.data.Model',
                        config: {
                            fields: [{
                                name: 'text',
                                type: 'string'
                            }, {
                                name: 'contact',
                                type: 'auto'
                            }]
                        }
                    }),
                    defaultRootProperty: 'items',
                    root: {
                        text: '聯絡窗口',
                        items: [{
                            text: '收送件',
                            contact: [{
                                title: '收送件時間週一至週五 08:30AM~9:00PM',
                                phone: '收送件專線 ：03-5799909  分機1111'
                            }, {
                                title: '收送件時間週一至週五 08:30AM~9:00PM',
                                phone: '收送件專線 ：03-5799909  分機1111'
                            }],
                            leaf: true
                        }, {
                            text: '工程',
                            contact: [{
                                title: '收送件時間週一至週五 08:30AM~9:00PM',
                                phone: '收送件專線 ：03-5799909  分機1111'
                            }, {
                                title: '收送件時間週一至週五 08:30AM~9:00PM',
                                phone: '收送件專線 ：03-5799909  分機1111'
                            }],
                            leaf: true
                        }, {
                            text: '業務',
                            contact: [{
                                title: '收送件時間週一至週五 08:30AM~9:00PM',
                                phone: '收送件專線 ：03-5799909  分機1111'
                            }, {
                                title: '收送件時間週一至週五 08:30AM~9:00PM',
                                phone: '收送件專線 ：03-5799909  分機1111'
                            }],
                            leaf: true
                        }, {
                            text: '投資人關系',
                            contact: [{
                                title: '收送件時間週一至週五 08:30AM~9:00PM',
                                phone: '收送件專線 ：03-5799909  分機1111'
                            }, {
                                title: '收送件時間週一至週五 08:30AM~9:00PM',
                                phone: '收送件專線 ：03-5799909  分機1111'
                            }],
                            leaf: true
                        }, {
                            text: '人力資源',
                            contact: [{
                                title: '收送件時間週一至週五 08:30AM~9:00PM',
                                phone: '收送件專線 ：03-5799909  分機1111'
                            }, {
                                title: '收送件時間週一至週五 08:30AM~9:00PM',
                                phone: '收送件專線 ：03-5799909  分機1111'
                            }],
                            leaf: true
                        }, {
                            text: '公共與媒體關系',
                            contact: [{
                                title: '收送件時間週一至週五 08:30AM~9:00PM',
                                phone: '收送件專線 ：03-5799909  分機1111'
                            }, {
                                title: '收送件時間週一至週五 08:30AM~9:00PM',
                                phone: '收送件專線 ：03-5799909  分機1111'
                            }],
                            leaf: true
                        }, {
                            text: '宜特委案諮詢專線',
                            contact: [{
                                title: '收送件時間週一至週五 08:30AM~9:00PM',
                                phone: '收送件專線 ：03-5799909  分機1111'
                            }, {
                                title: '收送件時間週一至週五 08:30AM~9:00PM',
                                phone: '收送件專線 ：03-5799909  分機1111'
                            }],
                            leaf: true
                        }]
                    }
                }),
                width: '100%',
                height: '100%'
            })]
        }]
    }
});
