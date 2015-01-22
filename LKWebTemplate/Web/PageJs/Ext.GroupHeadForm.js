Ext.Loader.setConfig({
    enabled: true
});

Ext.Loader.setPath('Ext.ux', SYSTEM_ROOT_PATH + '/js/ux');

Ext.require(['*', 'Ext.ux.DataTip']);

Ext.MessageBox.buttonText.yes = "確定";
Ext.MessageBox.buttonText.no = "取消";

var thisTreeStore = undefined;
var AppMenuPanel = undefined;
var GroupHeadFormPanel = undefined;
var firstGridDDGroup;
var secondGridDDGroup;
var displayPanel;
var bnt_Query;
var fieldSet;
var userPanel;
var AppMenuVTaskFlag = false;
var LoadDataTaskFlag = false;
var COMPANYUUID = undefined;
var AppMenuVTask = {
    run: function () {
        if (AppMenuVTaskFlag == true) {
            Ext.getCmp('AppMenu.Tree').expandAll();
            AppMenuVTaskFlag = false;
        }
    },
    interval: 1000
}
var LoadDataTask = {
    run: function () {
        if (AppMenuVTaskFlag == false) {
            LoadDataTask = true;
        }
    },
    interval: 1000
}
Ext.TaskManager.start(AppMenuVTask);
Ext.TaskManager.start(LoadDataTask);
var PARENTUUID = undefined;
var myMask = undefined;
Ext.onReady(function () {
    Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION + ".UserAction"));

    if (COMPANYUUID == undefined) {
        WS.UserAction.getUserInfo(function (jsonObj) {
            COMPANYUUID = jsonObj.COMPANY_UUID;
        });
    };
});

Ext.onReady(function () {
    /*:::加入Direct:::*/    
    Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION + ".GroupHeadAction"));
    Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION + ".ApplicationAction"));
    Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION + ".AuthorityAction"));
    Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION + ".GroupAttendantAction"));

    function setParentsChecked(obj, checked) {        
        obj.set('checked', checked);
        if (!obj.parentNode.data.root)
            setParentsChecked(obj.parentNode, checked);
    }

    function setChildrenUnchecked(obj, checked) {
        obj.set('checked', checked);
        if (obj.childNodes.length > 0) {
            for (var i = 0; i < obj.childNodes.length; i++) {
                setChildrenUnchecked(obj.childNodes[i], checked);
            }
        }
    }

    function btn_Query_Click() {
        loadAttendantStoreNotInGroup(
            COMPANYUUID,
            Ext.getCmp('ExtGroupHeadForm').uuid,
            Ext.getCmp('_txtSearch').getValue()
        );
    }

    function loadAttendantStoreNotInGroup(company_uuid, group_head_uuid, searchtxt) {
        storeAttendantNotInGroupAttendant.getProxy().setExtraParam('company_uuid', company_uuid);
        storeAttendantNotInGroupAttendant.getProxy().setExtraParam('group_head_uuid', group_head_uuid);
        storeAttendantNotInGroupAttendant.getProxy().setExtraParam('keyword', searchtxt);
        storeAttendantNotInGroupAttendant.getProxy().setExtraParam('is_active', 'Y');
        storeAttendantNotInGroupAttendant.load({
            callback: function () {

            }
        });
    }

    getDeepAllChildNodes = function (node) {
        var allNodes = new Array();
        allNodes = getChildNodes(node, allNodes);
        return allNodes;
    }

    function getChildNodes(node, allNodes) {
        if (!Ext.value(node, false)) {
            return [];
        }
        if (!node.hasChildNodes()) {
            return node;
        } else {
            allNodes.push(node);
            node.eachChild(function (Mynode) {
                allNodes = allNodes.concat(getChildNodes(Mynode));
            });
        }
        return allNodes;
    }

    function isIE6() {
        if (jQuery.browser.msie == true) {
            /*是microsoft時執行*/
            if (jQuery.browser.version.substring(0, 1) == 6) {
                return true;
            } else if (jQuery.browser.version.substring(0, 1) == 7) {
                return true;
            } else {
                return false;
            }
        }
    }

    var GroupHeadForm_storeApplicationHead =
        Ext.create('Ext.data.Store', {
            successProperty: 'success',
            autoLoad: true,
            /*:::Table設定:::*/
            model: Ext.define('APPLICATIONHEADV', {
                extend: 'Ext.data.Model',
                /*:::欄位設定:::*/
                fields: [
                    'UUID',
                    'CREATE_DATE',
                    'UPDATE_DATE',
                    'IS_ACTIVE',
                    'NAME',
                    'DESCRIPTION',
                    'WEB_SITE',
                    'ID'
                ]
            }),
            pageSize: 9999,
            proxy: {
                type: 'direct',
                api: {
                    read: WS.ApplicationAction.loadApplication
                },
                reader: {
                    root: 'data'
                },
                paramsAsHash: true,
                /*:::Direct Method Parameter Setting:::*/
                paramOrder: ['pKeyWord', 'page', 'limit', 'sort', 'dir'],
                extraParams: {
                    pKeyWord: ''
                },
                simpleSortMode: true,
                listeners: {
                    exception: function (proxy, response, operation) {
                         Ext.MessageBox.show({
                            title: 'Warning',
                            msg: response.result.message,
                            icon: Ext.MessageBox.WARNING,
                            buttons: Ext.Msg.OK
                        });
                    },
                    beforeload: function () {
                        alert('beforeload proxy');
                    }
                }
            },
            listeners: {
                write: function (proxy, operation) {},
                read: function (proxy, operation) {},
                beforeload: function () {},
                afterload: function () {},
                load: function () {}
            },
            remoteSort: true,
            sorters: [{
                property: 'NAME',
                direction: 'ASC'
            }]
        });

    var storeAttendantNotInGroupAttendant =
        Ext.create('Ext.data.Store', {
            successProperty: 'success',
            autoLoad: false,
            /*:::Table設定:::*/
            model: Ext.define('ATTEDNANTVV', {
                extend: 'Ext.data.Model',
                /*:::欄位設定:::*/
                fields: [
                    'COMPANY_ID',
                    'COMPANY_C_NAME',
                    'COMPANY_E_NAME',
                    'DEPARTMENT_ID',
                    'DEPARTMENT_C_NAME',
                    'DEPARTMENT_E_NAME',
                    'SITE_ID',
                    'SITE_C_NAME',
                    'SITE_E_NAME',
                    'UUID',
                    'CREATE_DATE',
                    'UPDATE_DATE',
                    'IS_ACTIVE',
                    'COMPANY_UUID',
                    'ACCOUNT',
                    'C_NAME',
                    'E_NAME',
                    'EMAIL',
                    'PASSWORD',
                    'IS_SUPPER',
                    'IS_ADMIN',
                    'CODE_PAGE',
                    'DEPARTMENT_UUID',
                    'PHONE',
                    'SITE_UUID',
                    'GENDER',
                    'BIRTHDAY',
                    'HIRE_DATE',
                    'QUIT_DATE',
                    'IS_MANAGER',
                    'IS_DIRECT',
                    'GRADE',
                    'ID',
                    'IS_DEFAULT_PASS'
                ]
            }),
            pageSize: 9999,
            proxy: {
                type: 'direct',
                api: {
                    read: WS.GroupAttendantAction.loadAttendantStoreNotInGroup
                },
                reader: {
                    root: 'data'
                },
                paramsAsHash: true,
                /*:::Direct Method Parameter Setting:::*/
                paramOrder: ['company_uuid', 'group_head_uuid', 'keyword', 'is_active', 'page', 'limit', 'sort', 'dir'],
                extraParams: {
                    company_uuid: COMPANYUUID,
                    group_head_uuid: '',
                    keyword: '',
                    is_active: 'Y'
                },
                simpleSortMode: true,
                listeners: {
                    exception: function (proxy, response, operation) {
                         Ext.MessageBox.show({
                            title: 'Warning',
                            msg: response.result.message,
                            icon: Ext.MessageBox.WARNING,
                            buttons: Ext.Msg.OK
                        });
                    },
                    beforeload: function () {
                        alert('beforeload proxy');
                    }
                }
            },
            listeners: {
                write: function (proxy, operation) {},
                read: function (proxy, operation) {},
                beforeload: function () {},
                afterload: function () {},
                load: function () {}
            },
            remoteSort: true,
            sorters: [{
                property: 'C_NAME',
                direction: 'ASC'
            }]
        });

    var storeAttendantInGroupAttendant =
        Ext.create('Ext.data.Store', {
            successProperty: 'success',
            autoLoad: false,
            /*:::Table設定:::*/
            model: Ext.define('ATTEDNANTVV', {
                extend: 'Ext.data.Model',
                /*:::欄位設定:::*/
                fields: [
                    'COMPANY_ID',
                    'COMPANY_C_NAME',
                    'COMPANY_E_NAME',
                    'DEPARTMENT_ID',
                    'DEPARTMENT_C_NAME',
                    'DEPARTMENT_E_NAME',
                    'SITE_ID',
                    'SITE_C_NAME',
                    'SITE_E_NAME',
                    'UUID',
                    'CREATE_DATE',
                    'UPDATE_DATE',
                    'IS_ACTIVE',
                    'COMPANY_UUID',
                    'ACCOUNT',
                    'C_NAME',
                    'E_NAME',
                    'EMAIL',
                    'PASSWORD',
                    'IS_SUPPER',
                    'IS_ADMIN',
                    'CODE_PAGE',
                    'DEPARTMENT_UUID',
                    'PHONE',
                    'SITE_UUID',
                    'GENDER',
                    'BIRTHDAY',
                    'HIRE_DATE',
                    'QUIT_DATE',
                    'IS_MANAGER',
                    'IS_DIRECT',
                    'GRADE',
                    'ID',
                    'IS_DEFAULT_PASS'
                ]
            }),
            pageSize: 9999,
            proxy: {
                type: 'direct',
                api: {
                    read: WS.GroupAttendantAction.loadAttendantStoreInGroup
                },
                reader: {
                    root: 'data'
                },
                paramsAsHash: true,
                /*:::Direct Method Parameter Setting:::*/
                paramOrder: ['company_uuid', 'group_head_uuid', 'keyword', 'is_active', 'page', 'limit', 'sort', 'dir'],
                extraParams: {
                    company_uuid: COMPANYUUID,
                    group_head_uuid: '',
                    keyword: '',
                    is_active: 'Y'
                },
                simpleSortMode: true,
                listeners: {
                    exception: function (proxy, response, operation) {
                        Ext.MessageBox.show({
                            title: 'Warning',
                            msg: response.result.message,
                            icon: Ext.MessageBox.WARNING,
                            buttons: Ext.Msg.OK
                        });
                    },
                    beforeload: function () {
                        alert('beforeload proxy');
                    }
                }
            },
            listeners: {
                write: function (proxy, operation) {},
                read: function (proxy, operation) {},
                beforeload: function () {},
                afterload: function () {},
                load: function () {}
            },
            remoteSort: true,
            sorters: [{
                property: 'C_NAME',
                direction: 'ASC'
            }]
        });

    Ext.define('Model.AppMenu', {
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
            convert: function (v) {
                return (v === "Y" || v === true) ? true : false;
            }
        }, {
            name: 'IS_DEFAULT_PAGE',
            type: 'bool',
            convert: function (v) {
                return (v === "Y" || v === true) ? true : false;
            }
        }]
    });

    Ext.define('AppMenuVTree', {
        extend: 'Ext.data.TreeStore',
        root: {
            expanded: true
        },
        autoLoad: false,
        successProperty: 'success',
        model: 'Model.AppMenu',
        nodeParam: 'NAME_ZH_TW',
        proxy: {
            paramOrder: ['UUID', 'GROUPHEADUUID'],
            type: 'direct',
            directFn: WS.AuthorityAction.loadAppmenuTree,
            extraParams: {
                "UUID": '',
                "GROUPHEADUUID": ''
            }
        }        
    });

    thisTreeStore = Ext.create('AppMenuVTree', {});

    AppMenuPanel = Ext.widget({
        id: 'AppMenuPanel',
        xtype: 'panel',
        frame: false,
        padding: 5,
        autoHeight: true,
        autoWidth: true,
        companyUuid: undefined,
        border: false,
        items: [{
            xtype: 'treepanel',
            fieldLabel: '權限維護',
            id: 'AppMenu.Tree',
            padding: 5,
            border:true,
            autoWidth: true,
            autoHeight: true,
            minHeight: 400,
            store: thisTreeStore,
            multiSelect: true,
            rootVisible: false,
            useArrows: true,
            loadMask: true,
            columns: [{
                text: '<center>UUID</center>',
                flex: 2,
                sortable: false,
                dataIndex: 'UUID',
                hidden: true
            }, {
                xtype: 'treecolumn',
                text: '<center>名稱</center>',
                flex: 2,
                sortable: false,
                dataIndex: 'NAME_ZH_TW'
            }, {
                text: '<center>行為模式</center>',
                flex: 1,
                dataIndex: 'ACTION_MODE',
                align: 'left',
                sortable: false
            }, {
                text: "<center>預設頁面</center>",
                dataIndex: 'DEFAULT_PAGE_CHECKED',
                align: 'center',
                flex: 0.5,
                xtype: 'checkcolumn',
                listeners: {         
                    'checkchange': function (obj, rowIndex, checked) {                        
                        var grid = obj.up().view,
                            store = grid.store,
                            record = store.getAt(rowIndex),
                            uuid = record.data.UUID,
                            is_default_page = record.data.IS_DEFAULT_PAGE;
                        if (!is_default_page) {
                            var _item = Ext.get(grid.all.elements[rowIndex].childNodes[0].childNodes[0].childNodes[2].childNodes[0]);
                            //var _item = Ext.get(Ext.get(grid.all.elements[rowIndex].childNodes[2].childNodes[0]).id);
                            if(_item!=null)
                                _item.remove();
                        } else {
                            WS.AuthorityAction.setGroupAppmenuIsDefaultPage(uuid,
                                Ext.getCmp('ExtGroupHeadForm').uuid, checked, function (data) {
                                    /*若是勾選*/
                                    if (checked) {                                        
                                        record.set('checked', checked);
                                    }
                                });
                        }
                    }
                },
                sortable: false,
                hideable: false
            }, {
                text: '<center>功能描述</center>',
                flex: 1,
                dataIndex: 'DESCRIPTION',
                align: 'left',
                sortable: false
            }, {
                text: '<center>虛擬路徑</center>',
                flex: 1,
                dataIndex: 'URL',
                align: 'left',
                sortable: false
            }, {
                text: '<center>參數</center>',
                flex: 1,
                dataIndex: 'PARAMETER_CLASS',
                align: 'left',
                sortable: false
            }],
            listeners: {
                beforeload: function (tree, node) {
                   

                    if (node.isComplete() == false) {
                        //thisTreeStore.getProxy().setExtraParam('UUID', node.node.raw['UUID']);
                        //alert(node.getParams()["UUID"]);
                        if(node.getParams()["UUID"]!=undefined){
                            thisTreeStore.getProxy().setExtraParam('UUID', node.getParams()["UUID"]);
                        }else{
                            thisTreeStore.getProxy().setExtraParam('UUID', node.config.node.data["UUID"]);
                           
                        }
                        thisTreeStore.getProxy().setExtraParam('GROUPHEADUUID', Ext.getCmp('ExtGroupHeadForm').uuid);
                    }
                },
                checkchange: function (a, b, c, d) {
                    var oUuid = a.data.UUID;
                    var _group_head_uuid = Ext.getCmp('ExtGroupHeadForm').uuid;
                    if (a.data.checked == true) {
                        /*表加入*/
                        WS.AuthorityAction.setGroupAppmenu(oUuid, _group_head_uuid, "Y", function (ret) {
                            setParentsChecked(a, a.data.checked);
                        });
                    } else {
                        WS.AuthorityAction.setGroupAppmenu(oUuid, _group_head_uuid, "N", function (ret) {
                            setChildrenUnchecked(a, a.data.checked);
                            /*可以勾選是否為is_default_page*/
                            var _is_default_page = a.data.IS_DEFAULT_PAGE;
                            /*是否勾選為default_page，因為是取消check，所以若有check，則需移除
                            var _default_page_checked = a.data.DEFAULT_PAGE_CHECKED;*/
                            if (_is_default_page && _default_page_checked) {
                                var _tree = Ext.getCmp('AppMenu.Tree');
                                var _rowNumber = _tree.view.store.indexOf(a);
                                Ext.get(Ext.get(_tree.view.all.elements[_rowNumber].childNodes[2].id)).checked = false;
                            }
                        });

                    }
                },
                itemclick: function (view, record, item, index, e) {
                    /*
                     if (record.isLeaf()) {
                     var nodeId = record.raw.id;//获取点击的节点id
                     var nodeText = record.raw.text;//获取点击的节点text
                     alert(nodeId);
                     }
                     */
                },
                afteritemexpand: function (node, index, item, eOpts) {
                    var queryUuid = Ext.getCmp('ExtGroupHeadForm').uuid;
                    if (queryUuid != undefined) {
                        var _default_page_checked = node.data.DEFAULT_PAGE_CHECKED;
                        var _is_default_page = node.data.IS_DEFAULT_PAGE;
                        var _tree = Ext.getCmp('AppMenu.Tree');
                        var _childNode = node.childNodes;
                        var _childNodeCount = _childNode.length;
                        if (_childNodeCount > 0) {
                            for (var i = 0; i < _childNodeCount; i++) {
                                var item_is_default_page = _childNode[i].data.IS_DEFAULT_PAGE;
                                var _rowNumber = _tree.view.store.indexOf(_childNode[i]);
                                Ext.get(Ext.get(_tree.view.all.elements[_rowNumber].childNodes[2].childNodes[0].children[0]).id).remove();
                            }
                        }
                        if (!_is_default_page) {
                            var rowNumber = _tree.view.store.indexOf(node);
                            Ext.get(Ext.get(_tree.view.all.elements[rowNumber].childNodes[2].childNodes[0].children[0]).id).remove();
                        }                        
                    }
                },
                afterlayout: function (obj, eOpts) {
                    /*處理root的下一層*/
                    var _tree = Ext.getCmp('AppMenu.Tree');
                    /*_tree.view.node.childNodes*/
                    var node = _tree.view.node;
                    is_root = _tree.view.node.data.root;
                    if (is_root && node.childNodes.length > 0) {
                        node.eachChild(function (n) {
                            var _is_default_page = n.data.IS_DEFAULT_PAGE;
                            var _rowNumber = _tree.view.store.indexOf(n);
                            if (_rowNumber != -1 && !_is_default_page) {
                                //var _item = Ext.get(_tree.view.all.elements[_rowNumber].childNodes[2].childNodes[0].children[0]);
                                var _item = Ext.get(_tree.view.all.elements[_rowNumber].childNodes[0].childNodes[0].childNodes[2].childNodes[0].childNodes[0]);
                                if (_item != null) {
                                    Ext.get(_item.id).remove();
                                }
                            }
                        });
                    }
                }
            }
        }]
    });

    firstGridDDGroup = new Ext.grid.GridPanel({
        multiSelect: true,
        margin:'0 10 0 0',
        border:true,
        viewConfig: {
            plugins: {
                ptype: 'gridviewdragdrop',
                dragGroup: 'firstGridDDGroup',
                dropGroup: 'secondGridDDGroup'
            },
            listeners: {
                drop: function (node, data, dropRec, dropPosition) {
                    var dropOn = dropRec ? ' ' + dropPosition + ' ' + dropRec.get('account') : ' on empty view';                    
                    var attendant_uuid = data.records[0].get('UUID');
                    var group_head_uuid = Ext.getCmp('ExtGroupHeadForm').uuid;
                    WS.GroupAttendantAction.destroyBy(group_head_uuid, attendant_uuid, function (data) {

                    });
                }
            },
        },
        width: '50%', 
        store: storeAttendantNotInGroupAttendant,
        columns: [
            {
                header: "繁體名稱",
                sortable: true,
                width: '20%', 
                dataIndex: 'C_NAME'
            }, {
                header: "英文名稱",
                sortable: true,
                width: '20%', 
                dataIndex: 'E_NAME'
            }, {
                id: 'unselected_account',
                header: "帳號",
                sortable: true,
                width: '20%', 
                dataIndex: 'ACCOUNT'
            }, {
                header: "信箱",
                sortable: true,
                width: '28%', 
                dataIndex: 'EMAIL'
            }
        ],
        enableDragDrop: true,
        stripeRows: true,
        title: '未選取人員'
    });

    secondGridDDGroup = new Ext.grid.GridPanel({
        multiSelect: true,
        border:true,
        margin:'0 0 0 10',
        viewConfig: {
            plugins: {
                ptype: 'gridviewdragdrop',
                dragGroup: 'secondGridDDGroup',
                dropGroup: 'firstGridDDGroup'
            },
            listeners: {
                drop: function (node, data, dropRec, dropPosition) {
                    var dropOn = dropRec ? ' ' + dropPosition + ' ' + dropRec.get('uuid') : ' on empty view';                    
                    var attendant_uuid = data.records[0].get('UUID');
                    var group_head_uuid = Ext.getCmp('ExtGroupHeadForm').uuid;
                    WS.GroupAttendantAction.addAttendnatGroupHead(group_head_uuid, attendant_uuid, function (data) {

                    });
                }
            }
        },
        width: '50%', 
        store: storeAttendantInGroupAttendant,
        columns: [
            {
                header: "繁體名稱",
                sortable: true,
                width: '20%', 
                dataIndex: 'C_NAME'
            }, {
                header: "英文名稱",
                sortable: true,
                width: '20%', 
                dataIndex: 'E_NAME'
            }, {
                id: 'selected_account',
                header: "帳號",
                width: '20%', 
                sortable: true,
                dataIndex: 'ACCOUNT'
            }, {
                header: "信箱",
                sortable: true,
                width: '28%', 
                dataIndex: 'EMAIL'
            }
        ],
        enableDragDrop: true,
        stripeRows: true,
        title: '已選取人員'
    });

    displayPanel = new Ext.Panel({
        border: false,
        defaults: {
            flex: 1
        },
        layout: {
            type: 'hbox',
            align: 'stretch'
        },
        width: '100%', 
        height: 400,
        items: [firstGridDDGroup, secondGridDDGroup]
    });

    bnt_Query = new Ext.Button({
        id: 'bnt_Query',
        item:'bnt_Query',
        margin:'0 0 0 10',
        //style: 'display:block; padding:4px 0px 0px 0px',
        text: '<img src="../../../css/custImages/search.gif" height="15"  style="vertical-align:middle"> 查詢',
        listeners: {
            "click": btn_Query_Click
        }
    });

    fieldSet = new Ext.form.FieldSet({
        title: '<img src="../../../css/custImages/query.gif" height="15"  style="vertical-align:middle">搜尋條件',
        height: 60,
        width: '100%', 
        columnWidth: 1,
        layout: 'column',
        border: true,
        labelWidth: 60,
        items: [{
                style: 'display:block; padding:4px 0px 0px 0px;margin-right:5px;',
                xtype: 'label',
                text: '關鍵字:'
            }, {                
                xtype: "textfield",
                //style: 'margin-right:5px;padding:4px 0px 0px 0px;',

                name: "_txtSearch",
                id: "_txtSearch",
                width: 200,
                enableKeyEvents:true,
                listeners:{
                    keyup:function(e,t,eOpts){
                        if(t.button==12){
                            btn_Query_Click();
                        }
                    }
                }                
            },
            bnt_Query
        ]
    });

    userPanel = (isIE6() ?
        new Ext.Panel({
            id: 'myUserPanel',
            frame: false,
            padding: 5,            
            width: '100%',             
            border: false,
            items: [fieldSet, displayPanel]
        }) :
        new Ext.Panel({
            id: 'myUserPanel',
            frame: false,
            padding: 5,
            bodyStyle: "padding:2px 0px 0",
            border: false,
            items: [fieldSet, displayPanel]
        })
    );


    GroupHeadFormPanel = Ext.create('Ext.form.Panel', {
        layout: {
            type: 'form',
            align: 'stretch'
        },
        api: {
            /*:::讀取的Direct Url:::*/
            load: WS.GroupHeadAction.info,
            submit: WS.GroupHeadAction.submit
        },
        /*:::Panel的ID(唯一性):::*/
        id: 'Ext_GroupHeadForm_Form',
        /*:::參數傳遞的順序:::*/
        paramOrder: ['pUuid'],
        border: false,
        bodyPadding: 5,
        defaultType: 'textfield',
        buttonAlign: 'center',
        items: [{
                xtype: 'container',
                layout: 'hbox',
                items: [{
                    fieldLabel: '系統',
                    labelAlign: 'right',
                    xtype: 'combobox',
                    id: 'Ext_GroupHeadForm_Form_APPLICATION_HEAD_UUID',
                    queryMode: 'local',
                    displayField: 'NAME',
                    valueField: 'UUID',
                    name: 'APPLICATION_HEAD_UUID',
                    anchor: '100%',
                    padding: 5,
                    editable: false,
                    hidden: false,
                    store: GroupHeadForm_storeApplicationHead
                }, {
                    xtype: 'textfield',
                    fieldLabel: '代碼',
                    labelAlign: 'right',
                    labelWidth: 100,
                    id: 'Ext_GroupHeadForm_Form_ID',
                    name: 'ID',
                    padding: 5,
                    maxLength: 50
                }]
            },
            /*:::你要顯示的欄位資訊:::*/
            {
                xtype: 'container',
                layout: 'hbox',
                items: [{
                    xtype: 'textfield',
                    fieldLabel: '群組繁中',
                    labelAlign: 'right',
                    id: 'NAME_ZH_TW',
                    labelWidth: 100,
                    name: 'NAME_ZH_TW',
                    padding: 5,
                    anchor: '100%',
                    maxLength: 100,
                    allowBlank: false
                }, {
                    xtype: 'textfield',
                    fieldLabel: '群組簡中',
                    labelAlign: 'right',
                    labelWidth: 100,
                    name: 'NAME_ZH_CN',
                    padding: 5,
                    anchor: '100%',
                    maxLength: 100,
                    allowBlank: false
                }, {
                    xtype: 'textfield',
                    fieldLabel: '群組英文',
                    labelWidth: 100,
                    labelAlign: 'right',
                    name: 'NAME_EN_US',
                    padding: 5,
                    maxLength: 100
                }]
            }, {
                /*:::資料的PK欄位:::*/
                xtype: 'hidden',
                fieldLabel: 'UUID',
                name: 'UUID',
                padding: 5,
                anchor: '100%',
                maxLength: 84,
                id: 'GroupHeadForm_UUID'
            }, {
                /*:::資料的IS_ACTIVE欄位:::*/
                xtype: 'hidden',
                fieldLabel: 'IS_ACTIVE',
                name: 'IS_ACTIVE',
                padding: 5,
                anchor: '100%',
                maxLength: 1,
                value: 'Y'
            }
        ],
        fbar: [{
            type: 'button',
            text: '<img src="' + SYSTEM_URL_ROOT + '/css/images/save.gif" style="width:16px;height:16px;vertical-align:middle;margin-right:5px;"/>' + '儲存',
            handler: function () {
                var form = Ext.getCmp('Ext_GroupHeadForm_Form').getForm();

                if (form.isValid() == false) {
                    return;
                }

                form.submit({
                    waitMsg: '更新中...',
                    success: function (form, action) {
                        Ext.getCmp('Ext_GroupHeadForm_Form_ID').setDisabled(false);
                        Ext.getCmp('Ext_GroupHeadForm_Form_APPLICATION_HEAD_UUID').setDisabled(false);
                        Ext.getCmp('bnt_Query').setDisabled(false);
                        Ext.getCmp('bnt_Delete').setDisabled(false);
                        Ext.MessageBox.show({
                            title: '維護群組定義',
                            msg: '操作完成',
                            icon: Ext.MessageBox.INFO,
                            buttons: Ext.Msg.OK,
                            fn: function () {
                                Ext.getCmp('GroupHeadForm_UUID').setValue(action.result.UUID);
                                Ext.getCmp('ExtGroupHeadForm').setTitle('群組定義【維護】');
                                Ext.getCmp('ExtGroupHeadForm').hide();
                                Ext.getCmp('ExtGroupHeadForm').uuid = action.result.UUID;
                                Ext.getCmp('ExtGroupHeadForm').show();
                            }
                        });
                    },
                    failure: function (form, action) {
                        Ext.MessageBox.show({
                            title: '維護群組定義',
                            msg: action.result.message,
                            icon: Ext.MessageBox.ERROR,
                            buttons: Ext.Msg.OK
                        });
                    }
                });
            }
        }, {
            type: 'button',
            id: 'bnt_Delete',
            text: '<img src="' + SYSTEM_URL_ROOT + '/css/images/delete.png" style="width:16px;height:16px;vertical-align:middle;margin-right:5px;"/>' + '刪除',
            handler: function () {
                Ext.Msg.show({
                    title: '刪除節點操作',
                    msg: '確定執行刪除動作?',
                    buttons: Ext.Msg.YESNO,
                    fn: function (btn) {
                        if (btn == "yes") {
                            WS.GroupHeadAction.deleteGroupHead(Ext.getCmp('ExtGroupHeadForm').uuid, function (data) {
                                Ext.getCmp('ExtGroupHeadForm').hide();
                            });
                        }
                    }
                });
            }
        }, {
            type: 'button',
            text: '<img src="' + SYSTEM_URL_ROOT + '/css/images/leave.png" style="width:16px;height:16px;vertical-align:middle;margin-right:5px;"/>' + '關閉',
            handler: function () {
                Ext.getCmp('ExtGroupHeadForm').hide();
            }
        }]
    });

    /*:::Calss Name:::*/
    Ext.define('GroupHeadForm', {
        extend: 'Ext.window.Window',
        title: '<img src="' + SYSTEM_URL_ROOT + '/css/images/permission.png" style="height:12px;vertical-align:middle;margin-right:5px;margin-top:-2px;">群組權限 新增/修改',        
        closeAction: 'hide',
        padding: 10,
        border: false,
        /*:::自訂查詢用的key欄位:::*/
        uuid: undefined,
        /*:::元件的ID在系統中是唯一的:::*/
        id: 'ExtGroupHeadForm',
        width: 1000,
        height: 700,
        maxWidth: 930,
        maxHeight: 550,
        resizable: false,
        draggable: true,
        autoScroll: true,
        /*定義事件的方法*/
        initComponent: function () {
            /*:::新增事件:::*/
            //this.addEvents('closeEvent');
            var win = this;
            win.items = [GroupHeadFormPanel, {
                    xtype: 'tabpanel',
                    //plain: true,
                    padding: 10,
                    maxWidth: 880,

                    items: [{

                        title: '<img src="' + SYSTEM_URL_ROOT + '/css/images/menu.png" style="width:16px;height:16px;vertical-align:middle;margin-right:5px;"/>' + '權限維護',
                        items: [AppMenuPanel]
                    }, {
                        title: '<img src="' + SYSTEM_URL_ROOT + '/css/images/man.png" style="width:16px;height:16px;vertical-align:middle;margin-right:5px;"/>' + '使用者維護',
                        items: [userPanel]
                    }]
                }

            ]
            win.callParent(arguments);
        },
        closeEvent: function () {
            this.fireEvent('closeEvent', this);
        },
        listeners: {
            'show': function () {

                if (COMPANYUUID == undefined) {
                    WS.UserAction.getUserInfo(function (jsonObj) {
                        COMPANYUUID = jsonObj.COMPANY_UUID;
                    });
                }

                /*:::畫面開啟後載入資料:::*/
                myMask = new Ext.LoadMask(Ext.getCmp('AppMenuPanel'), {
                    msg: "資料載入中，請稍等...",
                    store: thisTreeStore
                });
                myMask.show();
                /*this.uuid 是 undefined 的話；表示執行新增的動作*/
                if (this.uuid != undefined) {
                    /*When 編輯/刪除資料*/
                    var queryUuid = Ext.getCmp('ExtGroupHeadForm').uuid;
                    Ext.getCmp('Ext_GroupHeadForm_Form_ID').setDisabled(true);
                    Ext.getCmp('Ext_GroupHeadForm_Form').getForm().load({
                        params: {
                            'pUuid': this.uuid
                        },
                        success: function (response, a, b) {
                            WS.AuthorityAction.loadTreeRoot(Ext.getCmp('Ext_GroupHeadForm_Form_APPLICATION_HEAD_UUID').getValue(), function (data) {                                
                                thisTreeStore.load({
                                    params: {
                                        UUID: data.UUID,
                                        GROUPHEADUUID: queryUuid
                                    }
                                });
                                AppMenuVTaskFlag = true;
                            });

                            if (LoadDataTask) {
                                storeAttendantInGroupAttendant.getProxy().setExtraParam('group_head_uuid', queryUuid);
                                storeAttendantInGroupAttendant.getProxy().setExtraParam('company_uuid', COMPANYUUID);
                                storeAttendantInGroupAttendant.load({
                                    callback: function () {
                                        LoadDataTask = false;
                                    }
                                });
                            }
                        },
                        failure: function (response, a, b) {
                            r = Ext.decode(response.responseText);
                            alert('err:' + r);
                        }
                    });
                } else {
                    /*When 新增資料*/
                    Ext.getCmp('Ext_GroupHeadForm_Form_ID').setDisabled(false);
                    Ext.getCmp('Ext_GroupHeadForm_Form_APPLICATION_HEAD_UUID').setDisabled(false);
                    Ext.getCmp('Ext_GroupHeadForm_Form').getForm().reset();
                    Ext.getCmp('AppMenu.Tree').getRootNode().removeAll();
                    storeAttendantNotInGroupAttendant.removeAll();
                    storeAttendantInGroupAttendant.removeAll();
                    Ext.getCmp('bnt_Query').setDisabled(true);
                    Ext.getCmp('bnt_Delete').setDisabled(true);
                }
            },
            'hide': function () {
                Ext.getBody().unmask();
                this.closeEvent();
            }
        }
    });
});