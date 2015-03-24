Ext.define('Ist.controller.CaseQueryResultPanel', {
    extend: 'Ext.app.Controller',

    config: {
        refs: {
            mainPanel: 'main',
            btnGoToQuery: 'button[action=CaseQueryResultPanelToQuery]'
        },
        control: {
            btnGoToQuery: {
                tap: 'onBtnGoToQuery'
            }
        }
    },
    launch: function() {        
        
    },
    onBtnGoToQuery: function(cm, e, opts) {
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
