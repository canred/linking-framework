Ext.define('Ist.view.CaseQueryResultPanel', {
    extend: 'Ext.form.Panel',
    xtype: 'CaseQueryResultPanel',
    scrollable: false,
    fullscreen: true,
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
                action: 'CaseQueryResultPanelToQuery',
                text: '返回'
            }]
        }, {
            xtype: 'label',
            padding:'0 5 0 5',
            height:25,
            html: '<span style="font-size:14px">查詢到90筆案件資訊</span>',
            style:{"background-color":"#EFCC58"}
        }, {
            xtype: 'dataview',
            useComponents: true,
            border: true,
            //layout: 'fit',
            //height:400,
            flex:1,            
            defaultType: 'CaseViewItem',
            listeners: {
                //itemtap: function(self, index, target, record, e, eOpts) {console.log(item)}
            },
            store: {
                fields: ['type', 'caseNumber', 'projectNumber', 'getTime', 'closeTime'],
                data: [{
                    'type': 'A',
                    'caseNumber': 'HS1301250225A',
                    'projectNumber': 'LT1601',
                    'getTime': '未知',
                    'closeTime': '2013/01/27 02:10:45'
                }, {
                    'type': 'B',
                    'caseNumber': 'HS1301250225A',
                    'projectNumber': 'LT1601',
                    'getTime': '未知',
                    'closeTime': '2013/01/27 02:10:45'
                }, {
                    'type': 'A',
                    'caseNumber': 'HS1301250225A',
                    'projectNumber': 'LT1601',
                    'getTime': '未知',
                    'closeTime': '2013/01/27 02:10:45'
                }, {
                    'type': 'B',
                    'caseNumber': 'HS1301250225A',
                    'projectNumber': 'LT1601',
                    'getTime': '未知',
                    'closeTime': '2013/01/27 02:10:45'
                }, {
                    'type': 'A',
                    'caseNumber': 'HS1301250225A',
                    'projectNumber': 'LT1601',
                    'getTime': '未知',
                    'closeTime': '2013/01/27 02:10:45'
                }, {
                    'type': 'B',
                    'caseNumber': 'HS1301250225A',
                    'projectNumber': 'LT1601',
                    'getTime': '未知',
                    'closeTime': '2013/01/27 02:10:45'
                }, {
                    'type': 'A',
                    'caseNumber': 'HS1301250225A',
                    'projectNumber': 'LT1601',
                    'getTime': '未知',
                    'closeTime': '2013/01/27 02:10:45'
                }, {
                    'type': 'B',
                    'caseNumber': 'HS1301250225A',
                    'projectNumber': 'LT1601',
                    'getTime': '未知',
                    'closeTime': '2013/01/27 02:10:45'
                }, {
                    'type': 'A',
                    'caseNumber': 'HS1301250225A',
                    'projectNumber': 'LT1601',
                    'getTime': '未知',
                    'closeTime': '2013/01/27 02:10:45'
                }, {
                    'type': 'B',
                    'caseNumber': 'HS1301250225A',
                    'projectNumber': 'LT1601',
                    'getTime': '未知',
                    'closeTime': '2013/01/27 02:10:45'
                }, {
                    'type': 'A',
                    'caseNumber': 'HS1301250225A',
                    'projectNumber': 'LT1601',
                    'getTime': '未知',
                    'closeTime': '2013/01/27 02:10:45'
                }, {
                    'type': 'B',
                    'caseNumber': 'HS1301250225A',
                    'projectNumber': 'LT1601',
                    'getTime': '未知',
                    'closeTime': '2013/01/27 02:10:45'
                }, {
                    'type': 'A',
                    'caseNumber': 'HS1301250225A',
                    'projectNumber': 'LT1601',
                    'getTime': '未知',
                    'closeTime': '2013/01/27 02:10:45'
                }, {
                    'type': 'B',
                    'caseNumber': 'HS1301250225A',
                    'projectNumber': 'LT1601',
                    'getTime': '未知',
                    'closeTime': '2013/01/27 02:10:45'
                }, {
                    'type': 'A',
                    'caseNumber': 'HS1301250225A',
                    'projectNumber': 'LT1601',
                    'getTime': '未知',
                    'closeTime': '2013/01/27 02:10:45'
                }, {
                    'type': 'B',
                    'caseNumber': 'HS1301250225A',
                    'projectNumber': 'LT1601',
                    'getTime': '未知',
                    'closeTime': '2013/01/27 02:10:45'
                }, {
                    'type': 'A',
                    'caseNumber': 'HS1301250225A',
                    'projectNumber': 'LT1601',
                    'getTime': '未知',
                    'closeTime': '2013/01/27 02:10:45'
                }, {
                    'type': 'B',
                    'caseNumber': 'HS1301250225A',
                    'projectNumber': 'LT1601',
                    'getTime': '未知',
                    'closeTime': '2013/01/27 02:10:45'
                }, {
                    'type': 'A',
                    'caseNumber': 'HS1301250225A',
                    'projectNumber': 'LT1601',
                    'getTime': '未知',
                    'closeTime': '2013/01/27 02:10:45'
                }, {
                    'type': 'B',
                    'caseNumber': 'HS1301250225A',
                    'projectNumber': 'LT1601',
                    'getTime': '未知',
                    'closeTime': '2013/01/27 02:10:45'
                }, {
                    'type': 'A',
                    'caseNumber': 'HS1301250225A',
                    'projectNumber': 'LT1601',
                    'getTime': '未知',
                    'closeTime': '2013/01/27 02:10:45'
                }, {
                    'type': 'B',
                    'caseNumber': 'HS1301250225A',
                    'projectNumber': 'LT1601',
                    'getTime': '未知',
                    'closeTime': '2013/01/27 02:10:45'
                }, {
                    'type': 'A',
                    'caseNumber': 'HS1301250225A',
                    'projectNumber': 'LT1601',
                    'getTime': '未知',
                    'closeTime': '2013/01/27 02:10:45'
                }, {
                    'type': 'B',
                    'caseNumber': 'HS1301250225A',
                    'projectNumber': 'LT1601',
                    'getTime': '未知',
                    'closeTime': '2013/01/27 02:10:45'
                }, {
                    'type': 'A',
                    'caseNumber': 'HS1301250225A',
                    'projectNumber': 'LT1601',
                    'getTime': '未知',
                    'closeTime': '2013/01/27 02:10:45'
                }, {
                    'type': 'B',
                    'caseNumber': 'HS1301250225A',
                    'projectNumber': 'LT1601',
                    'getTime': '未知',
                    'closeTime': '2013/01/27 02:10:45'
                }, {
                    'type': 'A',
                    'caseNumber': 'HS1301250225A',
                    'projectNumber': 'LT1601',
                    'getTime': '未知',
                    'closeTime': '2013/01/27 02:10:45'
                }, {
                    'type': 'B',
                    'caseNumber': 'HS1301250225A',
                    'projectNumber': 'LT1601',
                    'getTime': '未知',
                    'closeTime': '2013/01/27 02:10:45'
                }]
            }
        }]
    }
});
