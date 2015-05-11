Ext.define('WS.LogonWindow', { 
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
        logonPanel: undefined
    },
    initComponent: function() {
        this.items = [this.param.logonPanel];
        this.callParent(arguments);
    }
});
