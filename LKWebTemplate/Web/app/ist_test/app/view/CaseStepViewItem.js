Ext.define('Ist.view.CaseStepViewItem', {
    extend: 'Ext.dataview.component.DataItem',
    requires: ['Ist.view.CaseStepContainer'],
    xtype: 'CaseStepViewItem',
    config: {
        caseStepViewItemViews: true,
        //phoneviews: true,
        dataMap: {
            getCaseStepViewItemViews: {                
                setOrgName: 'caseNumber',
                setServiceName: 'projectNumber',
                setInTime: 'getTime',
                setOutTime: 'closeTime'
            }
        }
    },
    initialize: function() {
        //console.log(this.getItems());
    },
    applyCaseStepViewItemViews: function(config) {
        return Ext.factory(config, 'Ist.view.CaseStepContainer', this.getCaseStepViewItemViews());
    },
    fnTapImage: function(cmp, e, eOpts) {
        alert('OK');
        return ;
        if (!Ext.Viewport.down('CaseViewPanel')) {
            var view = Ext.create('Ist.view.CaseViewPanel');
            Ext.Viewport.setActiveItem(view);
        } else {
            Ext.Viewport.setActiveItem(Ext.Viewport.down('CaseViewPanel'));
        }
        //this.getRecord().data.
        //this.getRecord().data.caseNumber
        //this.getRecord().data.type               
    },
    updateCaseStepViewItemViews: function(newData, oldData) {

        if (oldData) {
            oldData.image.un('tap', this.fnTapImage, this)
            this.remove(oldData);
        };

        if (newData) {
            newData.image.on('tap', this.fnTapImage, this)
            this.add(newData);
        }
    }
});
