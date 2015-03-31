 Ext.define('TreeAppMenu', {
     extend: 'Ext.data.Model',
     fields: [{
         name: 'UUID'
     }, {
         name: 'NAME_ZH_TW'
     }, {
         name: 'ACTION_MODE'
     }, {
         name: 'DESCRIPTION'
     }, {
         name: 'URL'
     }, {
         name: 'PARAMETER_CLASS'
     }, {
         name: 'DEFAULT_PAGE_CHECKED',
         type: 'bool',
         convert: function(v) {
             return (v === "Y" || v === true) ? true : false;
         }
     }, {
         name: 'IS_DEFAULT_PAGE',
         type: 'bool',
         convert: function(v) {
             return (v === "Y" || v === true) ? true : false;
         }
     }]
 });
 Ext.define('WS.AppMenuVTree', {
     extend: 'Ext.data.TreeStore',
     root: {
         expanded: false
     },
     autoLoad: false,
     autoSync: false,
     successProperty: 'success',
     model: 'TreeAppMenu',
     nodeParam: 'NAME_ZH_TW',
     proxy: {
         paramOrder: ['UUID', 'GROUPHEADUUID'],
         type: 'direct',
         directFn: WS.AuthorityAction.loadAppmenuTree2,
         extraParams: {
             "UUID": 'noData',
             "GROUPHEADUUID": 'noData'
         }
     }
 });
