Ext.define('Model.SiteMap', {
    extend: 'Ext.data.Model',
    fields: [{
        name: 'UUID'
    }, {
        name: 'NAME'
    }, {
        name: 'DESCRIPTION'
    }]
});
Ext.define('WS.SitemapTreeStore', {
    extend: 'Ext.data.TreeStore',
    root: {
        expanded: true
    },
    autoLoad: false,
    successProperty: 'success',
    model: 'Model.SiteMap',
    nodeParam: 'id',
    proxy: {
        paramOrder: ['UUID'],
        type: 'direct',
        directFn: WS.SiteMapAction.loadSiteMapTree,
        extraParams: {
            "UUID": ''
        }
    }
});
