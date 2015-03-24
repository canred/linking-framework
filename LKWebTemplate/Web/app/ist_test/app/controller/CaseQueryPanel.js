Ext.define('Ist.controller.CaseQueryPanel', {
    extend: 'Ext.app.Controller',

    config: {
        refs: {
            mainPanel: 'main',
            btnGoToMainMenu: 'button[action=CaseQueryPanelToMain]',
            btnCaseQuery: 'button[action=caseQuery]'
        },
        control: {
            btnGoToMainMenu: {
                tap: 'onBtnGoToMainMenu'
            },
            btnCaseQuery: {
                tap: 'onBtnCaseQuery'
            }
        }
    },
    launch: function() {        
        
    },
    onBtnCaseQuery: function(cm, e, opts) {
        Ext.Viewport.getLayout().setAnimation({
            type: 'reveal',
            direction: 'right'
        });
        if (!Ext.Viewport.down('CaseQueryResultPanel')) {
            var view = Ext.create('Ist.view.CaseQueryResultPanel');
            Ext.Viewport.setActiveItem(view);
        } else {
            Ext.Viewport.setActiveItem(Ext.Viewport.down('CaseQueryResultPanel'));
        }
    },
    onBtnGoToMainMenu: function(cm, e, opts) {
        Ext.Viewport.getLayout().setAnimation({
            type: 'reveal',
            direction: 'left'
        });
        if (!Ext.Viewport.down('UserMainPanel')) {
            var view = Ext.create('Ist.view.UserMainPanel');
            Ext.Viewport.setActiveItem(view);
        } else {
            Ext.Viewport.setActiveItem(Ext.Viewport.down('UserMainPanel'));
        }


    }
});
