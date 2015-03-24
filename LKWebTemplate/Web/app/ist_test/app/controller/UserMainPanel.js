Ext.define('Ist.controller.UserMainPanel', {
    extend: 'Ext.app.Controller',
    config: {
        refs: {
            mainPanel: 'main',
            btnCaseQuery: 'button[action=btnCaseQuery]',
            btnNoticeSetting: 'button[action=btnNoticeSetting]',
            btnAnalysis: 'button[action=btnAnalysis]',
            btnContact: 'button[action=btnContact]',
            btnQuestion: 'button[action=btnQuestion]',
            btnNews: 'button[action=btnNews]',
            btnSubmit: 'button[action=SubmitAccountPassword]',
            btnForgetPassword: 'button[action=ForgetPassword]',
            txtAccount: 'textfield[name=account]',
            txtPassword: 'passwordfield[name=password]',
            btnLogout: 'button[action=btnLogout]'
        },
        control: {
            btnCaseQuery: {
                tap: 'onBtnCaseQuery'
            },
            btnLogout: {
                tap: 'onBtnLogout'
            },
            btnNoticeSetting: {
                tap: 'onBtnNoticeSetting'
            },
            btnAnalysis: {
                tap: 'onBtnAnalysis'
            },
            btnContact: {
                tap: 'onBtnContact'
            },
            btnQuestion: {
                tap: 'onBtnQuestion'
            },
            btnNews: {
                tap: 'onBtnNews'
            }
        }
    },
    launch: function() {

    },
    onBtnNoticeSetting: function(cm, e, opts) {        
        Ext.Viewport.getLayout().setAnimation({
            type: 'reveal',
            direction: 'right'
        });
        if (!Ext.Viewport.down('NoticeSettingPanel')) {
            var view = Ext.create('Ist.view.NoticeSettingPanel');
            Ext.Viewport.setActiveItem(view);

        } else {
            Ext.Viewport.setActiveItem(Ext.Viewport.down('NoticeSettingPanel'));
        };
        
    },
    onBtnAnalysis: function(cm, e, opts) {        
        Ext.Viewport.getLayout().setAnimation({
            type: 'reveal',
            direction: 'right'
        });
        if (!Ext.Viewport.down('AnalysisPanel')) {
            var view = Ext.create('Ist.view.AnalysisPanel');
            Ext.Viewport.setActiveItem(view);

        } else {
            Ext.Viewport.setActiveItem(Ext.Viewport.down('AnalysisPanel'));
        };
    },
    onBtnContact: function(cm, e, opts) {

        
        Ext.Viewport.getLayout().setAnimation({
            type: 'reveal',
            direction: 'right'
        });
        if (!Ext.Viewport.down('ContactPanel')) {
            var view = Ext.create('Ist.view.ContactPanel');
            Ext.Viewport.setActiveItem(view);

        } else {
            Ext.Viewport.setActiveItem(Ext.Viewport.down('ContactPanel'));
        };
    },
    onBtnQuestion: function(cm, e, opts) {
        Ext.Msg.alert({title:'通知',message:'功能開發中…',autoDestroy:true} );
    },
    onBtnNews: function(cm, e, opts) {


        Ext.Viewport.getLayout().setAnimation({
            type: 'reveal',
            direction: 'right'
        });
        if (!Ext.Viewport.down('NewsPanel')) {
            var view = Ext.create('Ist.view.NewsPanel');
            Ext.Viewport.setActiveItem(view);

        } else {
            Ext.Viewport.setActiveItem(Ext.Viewport.down('NewsPanel'));
        };

    },
    onBtnCaseQuery: function(cm, e, opts) {
        Ext.Viewport.getLayout().setAnimation({
            type: 'reveal',
            direction: 'right'
        });
        if (!Ext.Viewport.down('CaseQueryPanel')) {
            var view = Ext.create('Ist.view.CaseQueryPanel');
            Ext.Viewport.setActiveItem(view);

        } else {
            Ext.Viewport.setActiveItem(Ext.Viewport.down('CaseQueryPanel'));
        };
    },
    onBtnLogout: function(cm, e, opts) {
        Ext.Viewport.getLayout().setAnimation({
            type: 'reveal',
            direction: 'left'
        });
        if (!Ext.Viewport.down('main')) {
            var view = Ext.create('Ist.view.Main');
            Ext.Viewport.setActiveItem(view);
        } else {
            Ext.Viewport.setActiveItem(Ext.Viewport.down('main'));
        }

    }
});
