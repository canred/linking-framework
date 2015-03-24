Ext.define('Ist.view.AnalysisPanel1', {
    extend: 'Ext.form.Panel',
    xtype: 'AnalysisPanel1',
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
                title: 'IC電路修補',               
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
                                name:'url',
                                type:'string'
                            }]
                        }
                    }),
                    defaultRootProperty: 'items',
                    root: {
                        text: 'IC電路修補',
                        items: [{
                            text: 'IC電路修改',
                            url:'http://www.istgroup.com/chinese/3_service/03_01_detail.php?MID=8&SID=85&ID=230',
                            leaf: true
                        }, {
                            text: '晶背FIB電路修改(Backside FIB )',
                            url:'http://www.istgroup.com/chinese/3_service/03_01_detail.php?MID=8&SID=85&ID=300',
                            leaf: true
                        }, {
                            text: '點針墊偵錯 (CAD Probe Pad)',
                             url:'http://www.istgroup.com/chinese/3_service/03_01_detail.php?MID=8&SID=85&ID=231',
                            leaf: true
                        }, {
                            text: '新型FIB電路修正技術 (N-FIB)',
                             url:'http://www.istgroup.com/chinese/3_service/03_01_detail.php?MID=8&SID=85&ID=233',
                            leaf: true
                        }, {
                            text: '新型WLCSP電路修正技術',
                             url:'http://www.istgroup.com/chinese/3_service/03_01_detail.php?MID=8&SID=85&ID=336',
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
