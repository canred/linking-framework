//alert( screen.height );
//alert(screen.width);
Ext.define('Ist.view.phone.Main', {
    extend: 'Ext.tab.Panel',
    //xtype: 'main',
    config: {
        tabBarPosition: 'bottom',
        fullscreen: true,
        items: [{
            title: '首頁',
            iconCls: 'home',
            styleHtmlContent: true,
            scrollable: true,
            padding: 0,
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
                flex: 1,
                items: [{
                        xtype: 'carousel',
                        itemId: 'culMain',
                        height: 520,
                        defaults: {
                            styleHtmlContent: true,
                            layout: 'hbox'
                        },
                        //height: screen.height * 0.18,
                        //maxHeight:screen.height * 0.18,
                        //height:115,
                        items: []
                    },
                    // {
                    //     xtype : 'container',
                    //     layout : 'hbox',
                    //     items : [{
                    //         xtype:'image',
                    //         src:'./resources/css/images/main-1_03.png'  ,
                    //         mode:'',
                    //         height:300
                    //     }]
                    // },
                    {
                        xtype: 'container',
                        layout: 'hbox',
                        //height: screen.height * 0.35,
                        //height: 240,
                        //height:screen.width/2,
                        //height:screen.width/4,
                        height:120,
                        items: [{
                            xtype: 'button',
                            flex:1,
                            //baseCls: 'main1',
                            cls: 'btn-main1-normal-p',
                            margin: '5 5 5 5',
                            handler: function(handler, scope) {}
                        }, {
                            xtype: 'button',
                            margin: '5 5 5 5',
                            flex:1,
                            //height: screen.width / 2,
                            //width: screen.width / 2,
                            //baseCls: 'main2',
                            cls: 'btn-main2-normal',
                            handler: function(handler, scope) {}
                        },{
                            xtype: 'button',
                            //baseCls: 'main3',
                            margin: '5 5 5 5',
                            cls: 'btn-main3-normal-p',
                            flex: 1,
                            handler: function(handler, scope) {}
                        }]
                    }, {
                        xtype: 'button',
                        margin: '5 5 5 5',
                        baseCls: 'main4',
                        //height: screen.height * 0.25,
                        height: 140,
                        handler: function(handler, scope) {}
                    }, {
                        xtype: 'container',
                        layout: 'hbox',
                        //height: screen.height * 0.1,
                        height: 140,
                        items: [{
                            xtype: 'button',
                            margin: '0 5 5 5',
                            baseCls: 'mainCustomerservice',
                            width: 80,
                            handler: function(handler, scope) {}
                        }, {
                            flex: 1,
                            xtype: 'label',
                            html: '<span style="font-size:10pt;color:#2E2931"><span style="color:blue">諮詢信箱</span>,<br>若您在對宜特有任何建議或需求，請點選， 我們將盡速為您處理!</span>'
                        }]
                    }
                ]
            }]
        }, {
            title: '快查',
            iconCls: 'search',
            items: [{
                xtype: 'SearchPanel'
            }]
        }, {
            title: '活動',
            iconCls: 'action',
            items: [{
                xtype: 'IstActionPanel'
            }]
        }, {
            title: '登入',
            iconCls: 'user',
            items: [{
                xtype: 'LoginPanel'
            }]
        }]
    },
    initialize: function() {
        Ext.Viewport.on('orientationchange', 'handleOrientationChange', this, {
            buffer: 50
        });

        this.callParent(arguments);
    },
    handleOrientationChange: function() {
        //alert(Ext.Viewport.getOrientation());//portrait //landscape

    }
});
