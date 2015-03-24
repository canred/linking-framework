Ext.define('Ist.view.ContactEngPanel', {
    extend: 'Ext.form.Panel',
    xtype: 'ContactEngPanel',
    requires: [],
    config: {
        layout: 'vbox',
        scrollable: 'vertical',
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
            title:'聚焦離子束(FIB)',
            //flex: 1,
            items: [{
                xtype: 'container',
                layout: 'vbox',
                items: [{
                    styleHtmlContent: true,
                    html: '王先生 886-3-5799909 Ext.6000 <BR>Email:web_FIB@istgroup.com'
                }]
            }]
        }, {
            xtype: 'fieldset',
            layout: 'hbox',
            title:'非破壞性故障分析 SAT/X-ray/3D X-ray/BGA Scope',
            //flex: 1,
            items: [{
                xtype: 'container',
                layout: 'vbox',
                items: [{
                    styleHtmlContent: true,
                    html: '陳小姐 886-3-5799909 Ext.6071 <BR>E-mail: web_NDE@istgroup.com'
                }]
            }]
        }, {
            xtype: 'fieldset',
            layout: 'hbox',
            title:'IC開蓋(Decapsulation/Delayer)',
            //flex: 1,
            items: [{
                xtype: 'container',
                layout: 'vbox',
                items: [{
                    styleHtmlContent: true,
                    html: '吳先生 886-3-5799909 Ext.6731 <BR>E-mail: web_Decap@istgroup.com'
                }]
            }]
        }, {
            xtype: 'fieldset',
            layout: 'hbox',
            title:'研磨(Cross-section/CP/Grinding) ',
            //flex: 1,
            items: [{
                xtype: 'container',
                layout: 'vbox',
                items: [{
                    styleHtmlContent: true,
                    html: '林先生 886-3-5799909 Ext.6632 <BR>E-mail: web_PFA@istgroup.com'
                }]
            }]
        }, {
            xtype: 'fieldset',
            layout: 'hbox',
            title:'研磨(Cross-section/CP/Grinding) ',
            //flex: 1,
            items: [{
                xtype: 'container',
                layout: 'vbox',
                items: [{
                    styleHtmlContent: true,
                    html: '林先生 886-3-5799909 Ext.6632 <BR>E-mail: web_PFA@istgroup.com'
                }]
            }]
        }, {
            xtype: 'fieldset',
            layout: 'hbox',
            title:'研磨(Cross-section/CP/Grinding) ',
            //flex: 1,
            items: [{
                xtype: 'container',
                layout: 'vbox',
                items: [{
                    styleHtmlContent: true,
                    html: '林先生 886-3-5799909 Ext.6632 <BR>E-mail: web_PFA@istgroup.com'
                }]
            }]
        }, {
            xtype: 'fieldset',
            layout: 'hbox',
            title:'研磨(Cross-section/CP/Grinding) ',
            //flex: 1,
            items: [{
                xtype: 'container',
                layout: 'vbox',
                items: [{
                    styleHtmlContent: true,
                    html: '林先生 886-3-5799909 Ext.6632 <BR>E-mail: web_PFA@istgroup.com'
                }]
            }]
        }, {
            xtype: 'fieldset',
            layout: 'hbox',
            title:'研磨(Cross-section/CP/Grinding) ',
            //flex: 1,
            items: [{
                xtype: 'container',
                layout: 'vbox',
                items: [{
                    styleHtmlContent: true,
                    html: '林先生 886-3-5799909 Ext.6632 <BR>E-mail: web_PFA@istgroup.com'
                }]
            }]
        }, {
            xtype: 'fieldset',
            layout: 'hbox',
            title:'研磨(Cross-section/CP/Grinding) ',
            //flex: 1,
            items: [{
                xtype: 'container',
                layout: 'vbox',
                items: [{
                    styleHtmlContent: true,
                    html: '林先生 886-3-5799909 Ext.6632 <BR>E-mail: web_PFA@istgroup.com'
                }]
            }]
        }, {
            xtype: 'fieldset',
            layout: 'hbox',
            title:'研磨(Cross-section/CP/Grinding) ',
            //flex: 1,
            items: [{
                xtype: 'container',
                layout: 'vbox',
                items: [{
                    styleHtmlContent: true,
                    html: '林先生 886-3-5799909 Ext.6632 <BR>E-mail: web_PFA@istgroup.com'
                }]
            }]
        }, {
            xtype: 'fieldset',
            layout: 'hbox',
            title:'研磨(Cross-section/CP/Grinding) ',
            //flex: 1,
            items: [{
                xtype: 'container',
                layout: 'vbox',
                items: [{
                    styleHtmlContent: true,
                    html: '林先生 886-3-5799909 Ext.6632 <BR>E-mail: web_PFA@istgroup.com'
                }]
            }]
        }, {
            xtype: 'fieldset',
            layout: 'hbox',
            title:'研磨(Cross-section/CP/Grinding) ',
            //flex: 1,
            items: [{
                xtype: 'container',
                layout: 'vbox',
                items: [{
                    styleHtmlContent: true,
                    html: '林先生 886-3-5799909 Ext.6632 <BR>E-mail: web_PFA@istgroup.com'
                }]
            }]
        }, {
            xtype: 'fieldset',
            layout: 'hbox',
            title:'研磨(Cross-section/CP/Grinding) ',
            //flex: 1,
            items: [{
                xtype: 'container',
                layout: 'vbox',
                items: [{
                    styleHtmlContent: true,
                    html: '林先生 886-3-5799909 Ext.6632 <BR>E-mail: web_PFA@istgroup.com'
                }]
            }]
        }, {
            xtype: 'fieldset',
            layout: 'hbox',
            title:'研磨(Cross-section/CP/Grinding) ',
            //flex: 1,
            items: [{
                xtype: 'container',
                layout: 'vbox',
                items: [{
                    styleHtmlContent: true,
                    html: '林先生 886-3-5799909 Ext.6632 <BR>E-mail: web_PFA@istgroup.com'
                }]
            }]
        }, {
            xtype: 'fieldset',
            layout: 'hbox',
            title:'研磨(Cross-section/CP/Grinding) ',
            //flex: 1,
            items: [{
                xtype: 'container',
                layout: 'vbox',
                items: [{
                    styleHtmlContent: true,
                    html: '林先生 886-3-5799909 Ext.6632 <BR>E-mail: web_PFA@istgroup.com'
                }]
            }]
        }]
    }
});
