Ext.define('Ist.controller.LoginPanel', {
    extend: 'Ext.app.Controller',
    config: {
        refs: {
            btnBackMain: 'button[action=btnBackMain]',
            btnSubmit: 'button[action=SubmitAccountPassword]',
            btnForgetPassword: 'button[action=ForgetPassword]',
            txtAccount: 'textfield[name=account]',
            txtPassword: 'passwordfield[name=password]',
            form: 'panel[itemId=LoginPanel]'
        },
        control: {
            btnBackMain: {
                tap: 'onBtnBackMain'
            },
            btnSubmit: {
                tap: 'onBtnSubmit'
            },
            btnForgetPassword: {
                tap: ''
            }
        }
    },
    launch: function() {
        //alert('lauch');
    },
    onBtnBackMain: function(cm, e, opts) {

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

    },
    onBtnSubmit: function(cm, e, opts) {
        
        Ext.Viewport.getLayout().setAnimation({
            type: 'reveal',
            direction: 'right'
        });
        this.getForm().submit({
            waitMsg: '更新中...',
            success: function(form, result, data, data2) {
                if (data.validation && data.validation == 'OK') {
                    if (!Ext.Viewport.down('UserMainPanel')) {
                        var view = Ext.create('Ist.view.UserMainPanel');
                        Ext.Viewport.setActiveItem(view);
                    } else {
                        Ext.Viewport.setActiveItem(Ext.Viewport.down('UserMainPanel'));
                    }
                } else {
                    Ext.create('Ext.MessageBox',{
                        title: '通知',
                        message: '您的帳號或密碼不正確!'    ,
                        buttons:[{
                            text:'OK',
                            handler:function(obj,a,b,c){
                                this.parent.parent.destroy();
                            }
                        }]
                    }).show();
                }
            },
            failure: function(form, action) {
                Ext.MessageBox.show({
                    title: 'System Error',
                    msg: action.result.message,
                    icon: Ext.MessageBox.ERROR,
                    buttons: Ext.Msg.OK
                });
            }
        },this);
    }
});
