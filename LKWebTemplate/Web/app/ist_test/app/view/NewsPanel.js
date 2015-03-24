Ext.define('Ist.view.NewsPanel', {
    extend: 'Ext.form.Panel',
    xtype: 'NewsPanel',
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
                listeners:{
                    leafitemtap:function(self,list,index,target,record,e,eOpts){                        
                        window.open(record.data.url);
                    }
                },
                title: '最新消息',               
                getItemTextTpl:function(node){
                    return "<img style='height:30px;vertical-align:middle' src='./resources/css/images/radio.png'></img>&nbsp;&nbsp;{text}";
                },
                store: Ext.create('Ext.data.TreeStore', {
                    model: Ext.define('ListItem', {
                        extend: 'Ext.data.Model',
                        config: {
                            fields: [
                            {
                                name:'date',
                                type:'string'
                            },
                            {
                                name: 'text',
                                type: 'string'
                            },{
                                name:'url',
                                type:'string'
                            }]
                        }
                    }),
                    defaultRootProperty: 'items',
                    root: {
                        text: '最新消息',
                        items: [{
                            text: '宜特祝大家羊羊得意!! 春節期間客服專線: 0928-732-111',
                            date:'2015-02-13',
                            url:'http://www.istgroup.com/chinese/2_news/02_news_detail.php?ID=227',
                            leaf: true
                        }, {
                            text: '【宜特技術新聞發佈】宜特正式成為MHL®3.2授權實驗室',
                            date:'2015-02-10',
                            url:'http://www.istgroup.com/chinese/2_news/02_news_detail.php?ID=223',
                            leaf: true
                        }, {
                            text: '【宜特營收新聞發佈】宜特2015年1月合併營收1.66億元，年增31.10%',
                            date:'2015-02-10',
                            url:'http://www.istgroup.com/chinese/2_news/02_news_detail.php?ID=224',
                            leaf: true
                        }, {
                            text: '【宜特營收新聞發佈】宜特2014年第四季自結獲利報告',
                            date:'2015-01-19',
                            url:'http://www.istgroup.com/chinese/2_news/02_news_detail.php?ID=222',
                            leaf: true
                        }, {
                            text: '【宜特技術新聞發佈】立錡與宜特共同揭示MEMS G-Sensor檢測合作成果',
                            date:'2015-01-19',
                            url:'http://www.istgroup.com/chinese/2_news/02_news_detail.php?ID=220',
                            leaf: true
                        }, {
                            text: '【宜特營收新聞發佈】宜特2014年12月合併營收1.63億元，年增11.33%',
                            date:'2015-01-19',
                            url:'http://www.istgroup.com/chinese/2_news/02_news_detail.php?ID=221',
                            leaf: true
                        }, {
                            text: 'iST宜特祝您2014聖誕佳節快樂',
                            date:'2014-12-23',
                            url:'http://www.istgroup.com/chinese/2_news/02_news_detail.php?ID=219',
                            leaf: true
                        }, {
                            text: '【宜特營收新聞發佈】宜特2014年11月合併營收1.54億元，年增8.14%',
                            date:'2014-12-10',
                            url:'http://www.istgroup.com/chinese/2_news/02_news_detail.php?ID=218',
                            leaf: true
                        }, {
                            text: '【宜特技術新聞發佈】宜特推二代MEMS分析，獨家研發「動態檢測異常」',
                            date:'2014-11-10',
                            url:'http://www.istgroup.com/chinese/2_news/02_news_detail.php?ID=216',
                            leaf: true
                        }, {
                            text: '【宜特營收新聞發佈】宜特2014年10月合併營收1.46億元，年減1.42%',
                            date:'2014-11-10',
                            url:'http://www.istgroup.com/chinese/2_news/02_news_detail.php?ID=217',
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
