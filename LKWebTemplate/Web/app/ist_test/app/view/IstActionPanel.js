Ext.define('Ist.view.IstActionPanel', {
    extend: 'Ext.form.Panel',
    xtype: 'IstActionPanel',
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
            }, {
                xtype: 'label',
                flex: 1
            }]
        }, {
            xtype: 'panel',
            height: screen.height * .8,
            items: [Ext.create('Ext.NestedList', {
                height: screen.height,
                listeners: {
                    leafitemtap: function(self, list, index, target, record, e, eOpts) {
                        window.open(record.data.url);
                    }
                },
                title: 'IST活動',
                getItemTextTpl: function(node) {
                    return "<img style='height:30px;vertical-align:middle' src='./resources/css/images/radio.png'></img>&nbsp;&nbsp;{text}";
                },
                store: Ext.create('Ext.data.TreeStore', {
                    model: Ext.define('ListItem2', {
                        extend: 'Ext.data.Model',
                        config: {
                            fields: [{
                                name: 'text',
                                type: 'string'
                            }, {
                                name: 'url',
                                type: 'auto'
                            }]
                        }
                    }),
                    defaultRootProperty: 'items',
                    root: {
                        text: 'IST活動',
                        items: [{
                            text: '【參展訊息】iST將參加台積電 2015 科技展',
                            url: 'http://www.istgroup.com/chinese/2_news/032_events_detail.php?ID=106',
                            leaf: true
                        }, {
                            text: '【得獎名單公布】賞片答題抽iPhone 6 plus!',
                            url: 'http://www.istgroup.com/chinese/2_news/032_events_detail.php?ID=105',
                            leaf: true
                        }, {
                            text: '【歡慶宜特二十】賞片答題抽iPhone 6 plus!',
                            url: 'http://www.istgroup.com/chinese/2_news/032_events_detail.php?ID=103',
                            leaf: true
                        }, {
                            text: '【參展訊息】iST宜特受邀於Keysight’s 2015 HSDT論壇演講',
                            url: 'http://www.istgroup.com/chinese/2_news/033_events_result.php?ID=104',
                            leaf: true
                        }, {
                            text: '【設備出售】二手機台',
                            url: 'http://www.istgroup.com/chinese/2_news/032_events_detail.php?ID=98',
                            leaf: true
                        }, {
                            text: '【論文發表】宜特將於IMPACT-EMAP 2014 發表兩篇技術論文',
                            url: 'http://www.istgroup.com/chinese/2_news/033_events_result.php?ID=102',
                            leaf: true
                        }, {
                            text: '【參展訊息】iST將於台積電2014 Open Innovation Platform參展',
                            url: 'http://www.istgroup.com/chinese/2_news/032_events_detail.php?ID=106',
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
