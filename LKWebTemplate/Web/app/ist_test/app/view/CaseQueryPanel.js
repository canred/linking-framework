Ext.define('Ist.view.CaseQueryPanel', {
    extend: 'Ext.form.Panel',
    xtype: 'CaseQueryPanel',    
    scrollable:false,
    requires: [],
    config: {
        scrollable:false,
        items: [{
            title: '首頁',
            iconCls: 'home',
            styleHtmlContent: true,
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
                    action: 'CaseQueryPanelToMain',
                    text: '目錄'
                }]
            }]
        }, {
            xtype: 'fieldset',
            defaults: {
                labelWidth: '35%'
            },
            items: [{
                xtype: 'textfield',
                name: 'txtCaseNumber',
                label: '工單號碼',
                placeHolder: '工單號碼',
                autoCapitalize: true,
                clearIcon: true
            }, {
                xtype: 'textfield',
                name: 'txtProjectCode',
                label: '專案代碼',
                placeHolder: '專案代碼',
                autoCapitalize: true,
                clearIcon: true
            }, {
                xtype: 'datepickerfield',
                destroyPickerOnHide: true,
                name: 'date',
                label: '開始日期',
                value: new Date(),
                picker: {
                    yearFrom: 1990
                }
            }, {
                xtype: 'datepickerfield',
                destroyPickerOnHide: true,
                name: 'date',
                label: '結束日期',
                value: new Date(),
                picker: {
                    yearFrom: 1990
                }
            }, {
                xtype: 'selectfield',
                name: 'rank',
                label: '結案狀態',
                options: [{
                    text: '全部',
                    value: 'All'
                }, {
                    text: '處理中',
                    value: 'InProcess'
                }, {
                    text: '完成',
                    value: 'Completed'
                }]
            }, {
                xtype: 'container',
                defaults: {
                    xtype: 'button',
                    style: 'margin: .5em',
                    flex: 1
                },
                layout: {
                    type: 'hbox'
                },
                items: [{
                    xtype: 'spacer'
                }, {
                    text: '查詢',
                    scope: this,
                    hasDisabled: false,
                    ui: 'action',    
                    action:'caseQuery',                
                    flex: 2,
                    handler: function(btn) {
                    }
                }, {
                    text: '清除',
                    ui: 'decline',
                    flex: 2,
                    handler: function() {
                    }
                }, {
                    xtype: 'spacer'
                }]
            }]
        }]
    }
});
