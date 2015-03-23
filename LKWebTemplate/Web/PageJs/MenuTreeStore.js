Ext.define('Model.AppMenu', {
    extend: 'Ext.data.Model',
    fields: [{
        name: 'UUID'
    }, {
        name: 'NAME'
    }, {
        name: 'NAME_ZH_TW'
    }, {
        name: 'DESCRIPTION'
    }, {
        name: 'ORD'
    }]
});

Ext.define('WS.MenuTreeStore', {
    extend: 'Ext.data.TreeStore',
    root: {
        expanded: true
    },
    autoLoad: false,
    successProperty: 'success',
    model: 'Model.AppMenu',
    nodeParam: 'id',
    proxy: {
        paramOrder: ['UUID'],
        type: 'direct',
        directFn: WS.MenuAction.loadMenuTree2,
        extraParams: {
            "UUID": ''
        }
    }
});
