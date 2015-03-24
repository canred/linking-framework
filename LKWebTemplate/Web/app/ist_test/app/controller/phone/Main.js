//alert(screen.width);

Ext.define('Ist.controller.phone.Main', {
    extend: 'Ext.app.Controller',
    config: {
        refs: {
            btnLogin: 'button[action=login]',            
            culMain: 'carousel[itemId=culMain]',
            tabLogin:'tab[iconCls=user]'
        },
        control: {
            btnLogin: {
                tap: 'onBtnLogin'
            }
        }
    },
    param: {
        arrCul: [
            "http://www.istgroup.com/upfiles/banner01418877616.jpg",
            "http://www.istgroup.com/upfiles/banner01401796152.jpg",
            "http://www.istgroup.com/upfiles/banner01400750957.jpg",
            "http://www.istgroup.com/upfiles/banner01418883255.jpg"
        ]
    },
    launch: function() {
        var culWidth = screen.width * .99;        
        //alert(Ext.Viewport.getOrientation());//portrait
        if(Ext.os.is.Phone){
            this.getCulMain().setHeight(120);
        }else{
            this.getCulMain().setHeight(250);
        };
        Ext.each(this.param.arrCul, function(item) {
            this.getCulMain().add({
                html: '<p><img style="width:100%;" src="' + item + '"/></p>'
            });
        }, this);
    },
    onBtnLogin: function(cm, e, opts) {
        Ext.Viewport.getLayout().setAnimation({
            type: 'reveal',
            direction: 'right'
        });
        if (!Ext.Viewport.down('LoginPanel')) {
            var view = Ext.create('Ist.view.LoginPanel');
            Ext.Viewport.setActiveItem(view);
        } else {
            Ext.Viewport.setActiveItem(Ext.Viewport.down('LoginPanel'));
        }
    }
});
