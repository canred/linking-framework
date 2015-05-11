Ext.define('WS.LogoutWindow', {
    extend: 'Ext.window.Window',    
    closeAction: 'destory',
    resizable: false,
    draggable: false,
    width: 400,
    style: {
        'border-width': '0px'
    },
    modal: true,    
    closable: false,    
    param: {
        logoutPanel: undefined
    },
    initComponent: function() {
        this.items = [this.param.logoutPanel];
        this.callParent(arguments);
    }
});
