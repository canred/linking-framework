Ext.define('Ist.view.CaseViewPanel', {
    extend: 'Ext.Panel',
    xtype: 'CaseViewPanel',
    requires: [],
    config: {
        scrollable: false,
        layout: 'vbox',
        //minHeight:800,
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
                    action: 'btnCaseViewBack',
                    text: '返回'
                }]

            }, {
                xtype: 'fieldset',
                //instructions: '資料更新時間為：2015/03/03 12:30',
                height: 180,
                defaults: {
                    labelWidth: '35%'
                },
                items: [{
                    xtype: 'textfield',
                    readOnly: true,
                    label: '工單號碼',
                    value: 'HS14069010001A'

                }, {
                    xtype: 'textfield',
                    readOnly: true,
                    label: '專案代碼',
                    value: 'DAZ76869'
                }, {
                    xtype: 'textfield',
                    readOnly: true,
                    label: '收件時間',
                    value: '2015/02/02 10:15'
                }, {
                    xtype: 'textfield',
                    readOnly: true,
                    label: '結案時間',
                    value: '2015/03/15 23:55'
                }]
            }, {
                xtype: 'container',
                layout: 'hbox',
                margin: '0 0 0 15',
                items: [{
                    xtype: 'image',
                    src: './resources/css/images/truck.png',
                    tag: "img",
                    mode: ''
                }, {
                    xtype: 'label',
                    margin: '0 0 0 5',
                    html: '站別資訊'
                },{
                    xtype:'spacer'
                },{
                    xtype:'togglefield',
                    height:30,
                    margin:'-20 0 0 0',
                    value:true,
                    style:'background-color:transparent'
                }]
            }, {
                xtype: 'dataview',
                useComponents: true,
                border: true,
                //height: 500,
                flex: 1,
                defaultType: 'CaseStepViewItem',
                listeners: {
                    //itemtap: function(self, index, target, record, e, eOpts) {console.log(item)}
                },
                store: {
                    fields: ['type', 'caseNumber', 'projectNumber', 'getTime', 'closeTime'],
                    data: [{
                        'type': 'A',
                        'caseNumber': 'HAAA',
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
            }

        ]
    }
});
