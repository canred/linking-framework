Ext.define('Ist.controller.AnalysisPanel', {
    extend: 'Ext.app.Controller',
    config: {
        refs: {                       
        	btnAnalysisToMenu :'button[action=btnAnalysisToMenu]',
        	btnIcAnalysisIc:'button[action=btnIcAnalysisIc]',
        	btnIcAnalysisIc2:'button[action=btnIcAnalysisIc2]',
        	btnIcAnalysisIc3:'button[action=btnIcAnalysisIc3]',
        	btnIcAnalysisIc4:'button[action=btnIcAnalysisIc4]',
        	btnIcAnalysisIc5:'button[action=btnIcAnalysisIc5]',
        	btnIcAnalysisIc6:'button[action=btnIcAnalysisIc6]',
        	btnIcAnalysisIc7:'button[action=btnIcAnalysisIc7]',
        	btnIcAnalysisIc8:'button[action=btnIcAnalysisIc8]',
        	btnIcAnalysisIc9:'button[action=btnIcAnalysisIc9]',        	
        },
        control: {
        	btnAnalysisToMenu:{
        		tap:'fnBtnAnalysisToMenu'
        	},
            btnIcAnalysisIc: {
                tap: 'fnBtnIcAnalysisIc'
            },
            btnIcAnalysisIc2:{
            	tap:'fnBtnIcAnalysisIc2'
            },
            btnIcAnalysisIc3:{
            	tap:'fnBtnIcAnalysisIc3'
            },
            btnIcAnalysisIc4:{
            	tap:'fnBtnIcAnalysisIc4'
            },
            btnIcAnalysisIc5:{
            	tap:'fnBtnIcAnalysisIc5'
            },
            btnIcAnalysisIc6:{
            	tap:'fnBtnIcAnalysisIc6'
            },
            btnIcAnalysisIc7:{
            	tap:'fnBtnIcAnalysisIc7'
            },
            btnIcAnalysisIc8:{
            	tap:'fnBtnIcAnalysisIc8'
            },
            btnIcAnalysisIc9:{
            	tap:'fnBtnIcAnalysisIc9'
            }

        }
    },
    launch: function() {        
        
    },   
    fnBtnAnalysisToMenu: function(cm, e, opts) {    	
    	 Ext.Viewport.getLayout().setAnimation({
            type: 'reveal',
            direction: 'left'
        });
        var nowpanel = Ext.Viewport.getActiveItem();
        var prepanel = Ext.Viewport.items.length-2;
        Ext.Viewport.setActiveItem(Ext.Viewport.items.items[prepanel]);
        Ext.Viewport.remove(nowpanel);
    },
    fnBtnIcAnalysisIc: function(cm, e, opts) {    	
         Ext.Viewport.getLayout().setAnimation({
            type: 'reveal',
            direction: 'right'
        });
        if (!Ext.Viewport.down('AnalysisPanel1')) {
            var view = Ext.create('Ist.view.AnalysisPanel1');
            Ext.Viewport.setActiveItem(view);
        } else {
            Ext.Viewport.setActiveItem(Ext.Viewport.down('AnalysisPanel1'));
        }
    },
    fnBtnIcAnalysisIc2: function(cm, e, opts) {    	
         Ext.Viewport.getLayout().setAnimation({
            type: 'reveal',
            direction: 'right'
        });
        if (!Ext.Viewport.down('AnalysisPanel2')) {
            var view = Ext.create('Ist.view.AnalysisPanel2');
            Ext.Viewport.setActiveItem(view);
        } else {
            Ext.Viewport.setActiveItem(Ext.Viewport.down('AnalysisPanel2'));
        }
    }
    ,
    fnBtnIcAnalysisIc3: function(cm, e, opts) {    	
         Ext.Msg.alert({title:'通知',message:'功能開發中…',autoDestroy:true} );
    }
    ,
    fnBtnIcAnalysisIc4: function(cm, e, opts) {    	
         Ext.Msg.alert({title:'通知',message:'功能開發中…',autoDestroy:true} );
    }
    ,
    fnBtnIcAnalysisIc5: function(cm, e, opts) {    	
         Ext.Msg.alert({title:'通知',message:'功能開發中…',autoDestroy:true} );
    }
    ,
    fnBtnIcAnalysisIc6: function(cm, e, opts) {    	
         Ext.Msg.alert({title:'通知',message:'功能開發中…',autoDestroy:true} );
    }
    ,
    fnBtnIcAnalysisIc7: function(cm, e, opts) {    	
         Ext.Msg.alert({title:'通知',message:'功能開發中…',autoDestroy:true} );
    }
    ,
    fnBtnIcAnalysisIc8: function(cm, e, opts) {    	
         Ext.Msg.alert({title:'通知',message:'功能開發中…',autoDestroy:true} );
    }
    ,
    fnBtnIcAnalysisIc9: function(cm, e, opts) {    	
         Ext.Msg.alert({title:'通知',message:'功能開發中…',autoDestroy:true} );
    }
});