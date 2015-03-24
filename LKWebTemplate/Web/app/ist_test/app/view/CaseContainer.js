Ext.define('Ist.view.CaseContainer', {
    extend: 'Ext.Container',
    xtype: 'CaseContainer',
    requires: ['Ext.Button', 'Ext.field.Text'],
    config: {
        layout: {
            type: 'vbox',
            align: 'stretch'
        },
        height: 130,
        cls: 'LIST',
        border: true,
        padding: 5,
        margin: 10,
        items: [{
            xtype: 'container',
            layout: 'hbox',
            flex:1,
            items: [{
                xtype: 'container',
                layout: 'vbox',
                flex: 1,
                items: [{
                    xtype: 'label',
                    action: 'caseNumber'
                }, {
                    xtype: 'label',
                    action: 'projectNumber'
                }, {
                    xtype: 'label',
                    action: 'getTime'
                }, {
                    xtype: 'label',
                    action: 'closeTime'
                }]
            }, {
                xtype: 'image',
                width:50,
                height:120,
                style:'background-color:#8ABAE2;border-radius:5px 5px 5px 5px',
                padding:'0 0 0 0',
                src: './resources/css/images/right.png',
                mode: ''
            }]
        }]
    },
    initialize: function(self, eOpts) {
        this.callParent();
        this.caseNumber = this.down("label[action=caseNumber]");
        this.projectNumber = this.down("label[action=projectNumber]");
        this.getTime = this.down('label[action=getTime]');
        this.closeTime = this.down('label[action=closeTime]');    
        this.image = this.down('image');
    },
    listeners:{
        afterrender:function(self,eOpts){
            console.log('log');
        }
    },
    setCaseNumber: function(value) {
        this.caseNumber.setHtml('<span style="color:blue;font-size:14px;">工單代碼</span> : ' + '<span style="font-size:14px;">' + value + '</span>');
    },
    setProjectNumber: function(value) {
        this.projectNumber.setHtml('<span style="color:black;font-size:14px;">專案代碼</span> : ' + '<span style="font-size:14px;">' + value + '</span>');
    },
    setGetTime: function(value) {
        this.getTime.setHtml('<span style="color:black;font-size:14px;">收件時間</span> : ' + '<span style="font-size:14px;">' + value + '</span>');
    },
    setCloseTime: function(value) {
        this.closeTime.setHtml('<span style="color:black;font-size:14px;">結案時間</span> : ' + '<span style="font-size:14px;">' + value + '</span>');
    }
});
