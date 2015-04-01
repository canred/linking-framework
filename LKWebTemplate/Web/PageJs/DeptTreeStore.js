Ext.define('WS.DeptTreeStore', {
    extend: 'Ext.data.TreeStore',
    root: {
        expanded: false
    },
    autoLoad: false,
    successProperty: 'success',
    model: 'DEPARTMENT',
    nodeParam: 'id',
    proxy: {
        paramOrder: ['UUID'],
        type: 'direct',
        directFn: WS.DeptAction.loadTree,
        extraParams: {
            "UUID": ''
        }
    }
});
