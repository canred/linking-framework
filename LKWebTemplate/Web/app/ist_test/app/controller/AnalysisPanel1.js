Ext.define('Ist.controller.AnalysisPanel1', {
    extend: 'Ext.app.Controller',
    config: {
        refs: {                        
            btnAnalysisBackToAyalysis:'button[action=btnAnalysisBackToAyalysis]'
        },
        control: {
            btnAnalysisBackToAyalysis: {
                tap: 'fnBtnAnalysisBackToAyalysis'
            }
        }
    },
    launch: function() {        
        
    },   
    fnBtnAnalysisBackToAyalysis: function(cm, e, opts) {      
         Ext.Viewport.getLayout().setAnimation({
            type: 'reveal',
            direction: 'right'
        });
        if (!Ext.Viewport.down('AnalysisPanel')) {
            var view = Ext.create('Ist.view.AnalysisPanel');
            Ext.Viewport.setActiveItem(view);
        } else {
            Ext.Viewport.setActiveItem(Ext.Viewport.down('AnalysisPanel'));
        }
    }
});