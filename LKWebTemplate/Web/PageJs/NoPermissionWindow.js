Ext.define('WS.NoPermissionWindow', { 
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
        noPermissionPanel: undefined
    },
    initComponent: function() {
        this.items = [this.param.noPermissionPanel];
        this.callParent(arguments);
    }
});
