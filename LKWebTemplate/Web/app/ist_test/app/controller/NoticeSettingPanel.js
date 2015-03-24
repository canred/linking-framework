Ext.define('Ist.controller.NoticeSettingPanel', {
    extend: 'Ext.app.Controller',

    config: {
        refs: {            
            btnGoToMainMenu: 'button[action=NoticeSettingPanelToMain]'
        },
        control: {
            btnGoToMainMenu: {
                tap: 'onBtnNoticeSettingPanel'
            }
        }
    },
    launch: function() {                
    },
    onBtnNoticeSettingPanel:function(cm, e, opts) {
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
