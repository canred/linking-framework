Ext.define('Ist.view.CaseStepContainer', {
    extend: 'Ext.Container',
    xtype: 'CaseStepContainer',
    requires: ['Ext.Button', 'Ext.field.Text'],
    config: {
        layout: {
            type: 'vbox',
            align: 'stretch'
        },
        height: 110,
        cls: 'LIST',
        border: true,
        padding: 5,
        margin: 10,
        items: [{
            xtype: 'container',
            layout: 'hbox',
            flex: 1,
            items: [{
                xtype: 'image',
                width: 50,
                height: 100,
                style: 'background-color:#F2AA80;border-radius:5px 5px 5px 5px',
                padding: '0 0 0 0',
                src: './resources/css/images/12.png',
                mode: ''
            }, {
                xtype: 'container',
                layout: 'vbox',
                margin:'0 0 0 10',
                flex: 1,
                items: [{
                    xtype: 'label',
                    action: 'orgName'
                }, {
                    xtype: 'label',
                    action: 'serviceName'
                }, {
                    xtype: 'label',
                    action: 'inTime'
                }, {
                    xtype: 'label',
                    action: 'outTime'
                }]
            }]
        }]
    },
    initialize: function(self, eOpts) {
        this.callParent();
        this.orgName = this.down("label[action=orgName]");
        this.serviceName = this.down("label[action=serviceName]");
        this.inTime = this.down('label[action=inTime]');
        this.outTime = this.down('label[action=outTime]');
        this.image = this.down('image');
    },
    listeners: {
        afterrender: function(self, eOpts) {}
    },
    setOrgName: function(value) {
        this.orgName.setHtml('<span style="color:blue;font-size:14px;">部門</span> : ' + '<span style="font-size:14px;">' + value + '</span>');
    },
    setServiceName: function(value) {
        this.serviceName.setHtml('<span style="color:black;font-size:14px;">服務項目</span> : ' + '<span style="font-size:14px;">' + value + '</span>');
    },
    setInTime: function(value) {
        this.inTime.setHtml('<span style="color:black;font-size:14px;">進站時間</span> : ' + '<span style="font-size:14px;">' + value + '</span>');
    },
    setOutTime: function(value) {
        this.outTime.setHtml('<span style="color:black;font-size:14px;">出站時間</span> : ' + '<span style="font-size:14px;">' + value + '</span>');
    }
});
