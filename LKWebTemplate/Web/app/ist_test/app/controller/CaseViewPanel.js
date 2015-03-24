Ext.define('Ist.controller.CaseViewPanel', {
    extend: 'Ext.app.Controller',
    config: {
        refs: {            
            btnCaseViewBack: 'button[action=btnCaseViewBack]'            
        },
        control: {
            btnCaseViewBack: {
                tap: 'onBtnCaseViewBack'
            }
        }
    },
    launch: function() {        
        
    },   
    onBtnCaseViewBack: function(cm, e, opts) {
        Ext.Viewport.getLayout().setAnimation({
            type: 'reveal',
            direction: 'left'
        });
        var nowpanel = Ext.Viewport.getActiveItem();
        var prepanel = Ext.Viewport.items.length-2;
        Ext.Viewport.setActiveItem(Ext.Viewport.items.items[prepanel]);
        Ext.Viewport.remove(nowpanel);
    }
});
