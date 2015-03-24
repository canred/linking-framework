Ext.define('Ist.view.CaseViewItem', {
    extend: 'Ext.dataview.component.DataItem',
    requires: ['Ist.view.CaseContainer'],
    xtype: 'CaseViewItem',
    config: {
        caseViewItemViews: true,
        //phoneviews: true,
        dataMap: {
            getCaseViewItemViews: {
                setPhoneType: 'type',
                setCaseNumber: 'caseNumber',
                setProjectNumber: 'projectNumber',
                setGetTime: 'getTime',
                setCloseTime: 'closeTime'
            }
        }
    },
    initialize: function() {
        //console.log(this.getItems());
    },
    applyCaseViewItemViews: function(config) {
        return Ext.factory(config, 'Ist.view.CaseContainer', this.getCaseViewItemViews());
    },
    fnTapImage: function(cmp, e, eOpts) {
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
    updateCaseViewItemViews: function(newData, oldData) {

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
