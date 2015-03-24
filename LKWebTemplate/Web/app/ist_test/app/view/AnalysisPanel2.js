Ext.define('Ist.view.AnalysisPanel2', {
    extend: 'Ext.form.Panel',
    xtype: 'AnalysisPanel2',
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
                title: '工程樣品製備',
                 listeners:{
                    leafitemtap:function(self,list,index,target,record,e,eOpts){                        
                        window.open(record.data.url);
                    }
                },
                 getItemTextTpl:function(node){
                    return "<img style='height:30px;vertical-align:middle' src='./resources/css/images/radio.png'></img>&nbsp;&nbsp;{text}";
                },
                store: Ext.create('Ext.data.TreeStore', {
                    model: Ext.define('ListItem', {
                        extend: 'Ext.data.Model',
                        config: {
                            fields: [{
                                name: 'text',
                                type: 'string'
                            },{
                                name: 'url',
                                type: 'string'
                            }]
                        }
                    }),
                    defaultRootProperty: 'items',
                    root: {
                        text: '工程樣品製備',
                        items: [{
                            text: '印刷電路板 (PCB)',
                            leaf: false,
                            items: [{
                                text: '印刷電路板服務簡介',
                                url:'http://www.istgroup.com/chinese/3_service/03_01_detail.php?MID=90&SID=91&ID=234',
                                leaf: true
                            }, {
                                text: '模擬實驗板(Emulation Board)',
                                url:'http://www.istgroup.com/chinese/3_service/03_01_detail.php?MID=90&SID=91&ID=235',
                                leaf: true
                            }, {
                                text: '展示板(Demo Board)',
                                url:'http://www.istgroup.com/chinese/3_service/03_01_detail.php?MID=90&SID=91&ID=236',
                                leaf: true
                            }, {
                                text: '測試板',
                                url:'http://www.istgroup.com/chinese/3_service/03_01_detail.php?MID=90&SID=91&ID=237',
                                leaf: true
                            }, {
                                text: '模組板',
                                url:'http://www.istgroup.com/chinese/3_service/03_01_detail.php?MID=90&SID=91&ID=238',
                                leaf: true
                            }, {
                                text: '系統板',
                                url:'http://www.istgroup.com/chinese/3_service/03_01_detail.php?MID=90&SID=91&ID=239',
                                leaf: true
                            }]
                        }, {
                            text: '快速封裝',
                            leaf: false,
                            items: [{
                                text: '晶圓切割服務(Wafer Dicing )',
                                url:'http://www.istgroup.com/chinese/3_service/03_01_detail.php?MID=90&SID=92&ID=240',
                                leaf: true
                            }, {
                                text: 'IC打線(Wire Bonding)',
                                url:'http://www.istgroup.com/chinese/3_service/03_01_detail.php?MID=90&SID=92&ID=241',
                                leaf: true
                            }, {
                                text: 'IC封裝整合',
                                url:'http://www.istgroup.com/chinese/3_service/03_01_detail.php?MID=90&SID=92&ID=286',
                                leaf: true
                            }]
                        }, {
                            text: ' IC開蓋',
                            leaf: false,
                            items: [{
                                text: 'C去除封膠(Decap)',
                                url:'http://www.istgroup.com/chinese/3_service/03_01_detail.php?MID=90&SID=93&ID=292',
                                leaf: true
                            }]
                        }, {
                            text: ' IC層次去除',
                            leaf: false,
                            items: [{
                                text: 'IC層次去除(Delayer)',
                                url:'http://www.istgroup.com/chinese/3_service/03_01_detail.php?MID=90&SID=95&ID=291',
                                leaf: true
                            }]
                        }, {
                            text: ' 研磨處理',
                            leaf: false,
                            items: [{
                                text: '傳統式剖面研磨(Cross-section)',
                                url:'http://www.istgroup.com/chinese/3_service/03_01_detail.php?MID=90&SID=96&ID=288',
                                leaf: true
                            }, {
                                text: '離子束剖面研磨 (CP)',
                                url:'http://www.istgroup.com/chinese/3_service/03_01_detail.php?MID=90&SID=96&ID=289',
                                leaf: true
                            }, {
                                text: 'IC晶背研磨(Backside Polishing)',
                                url:'http://www.istgroup.com/chinese/3_service/03_01_detail.php?MID=90&SID=96&ID=290',
                                leaf: true
                            }]
                        }, {
                            text: ' 表面黏著技術(SMT)',
                            leaf: false,
                            items: [{
                                text: '表面黏著技術(SMT)',
                                url:'http://www.istgroup.com/chinese/3_service/03_01_detail.php?MID=90&SID=97',
                                leaf: true
                            }]
                        }, {
                            text: ' 雙束聚焦離子束(Dual-beam FIB)',
                            leaf: false,
                            items: [{
                                text: '雙束聚焦離子束樣品製備(Dual-beam FIB)',
                                url:'http://www.istgroup.com/chinese/3_service/03_01_detail.php?MID=90&SID=119',
                                leaf: true
                            }]
                        }]
                    }
                }),
                width: '100%',
                height: '100%'
            })]
        }]
    }
});
