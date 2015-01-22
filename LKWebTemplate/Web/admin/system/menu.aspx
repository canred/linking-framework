<%@ Page Title="" Language="C#" MasterPageFile="~/mpStand.Master" AutoEventWireup="true" CodeBehind="menu.aspx.cs" Inherits="Web.admin.system.menu"  EnableViewState="false"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript" src='<%= Page.ResolveUrl("~/js/shared/include-ext.js")%>'></script>
<script type="text/javascript" src='<%= Page.ResolveUrl("~/Proxy.ashx")%>'></script>
<script type="text/javascript" src='<%= Page.ResolveUrl("~/pageJs/Ext.CompanyForm.js")%>'></script>
<script type="text/javascript" src='<%= Page.ResolveUrl("~/pageJs/Ext.AppPageForm.js")%>'></script>
<script type="text/javascript" src='<%= Page.ResolveUrl("~/pageJs/Ext.AppMenuForm.js")%>'></script>
<script type="text/javascript" src='<%= Page.ResolveUrl("~/pageJs/Ext.ProxyPicker.js")%>'></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<script language="javascript" type="text/javascript">
var thisTreeStore = undefined;
var AppPageQuery = undefined;
var myForm = undefined;
// Ext.require([
//     'Ext.grid.*',
//     'Ext.data.*',
//     'Ext.util.*',
//     'Ext.toolbar.Paging',
//     'Ext.ux.PreviewPlugin',
//     'Ext.ModelManager',
//     'Ext.tip.QuickTipManager'
// ]);

var AppMenuTaskFlag = false;
var AppMenuTask = {
    run: function () {
        if (AppMenuTaskFlag == true) {
            Ext.getCmp('AppMenu.Tree').expandAll();

            AppMenuTaskFlag = false;
        }
    },
    interval: 1000
}
Ext.TaskManager.start(AppMenuTask);


var PARENTUUID = undefined;

function openOrgn(uuid, parendUuid) {
    PARENTUUID = parendUuid;
}
Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION + ".AppPageAction"));
Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION + ".MenuAction"));
Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION + ".ApplicationAction"));

/*編輯menu的功能*/
function openMenu(uuid) {

    /*appMenuForm 變量保存在 Ext.AppMenuForm.js當中*/
    if (appMenuForm == undefined) {
        Ext.getBody().mask();
        appMenuForm = Ext.create('AppMenuForm');
        appMenuForm.applicationHeadUuid = Ext.getCmp('menu_Query_Application').getValue();

        /*載入關閉後的事件*/
        appMenuForm.on('closeEvent', function (obj) {
            /*重新整理畫面的內容*/
            var btnQuery = Ext.getCmp('menu.Query.Button');
            btnQuery.handler.call(btnQuery.scope);
            Ext.getBody().unmask();
        });

        /*設定開啟事內的條件*/
        appMenuForm.setTitle('<img src="' + SYSTEM_URL_ROOT + '/css/images/menu.png" style="height:20px;vertical-align:middle;margin-right:5px;">選單【編輯】');
        appMenuForm.uuid = uuid;

        appMenuForm.show();
    } else {
        Ext.getBody().mask();
        appMenuForm.uuid = uuid;
        appMenuForm.applicationHeadUuid = Ext.getCmp('menu_Query_Application').getValue();
        appMenuForm.show();
    }
}

function addChild(a, b) {
    WS.MenuAction.loadTreeRoot(Ext.getCmp('menu_Query_Application').getValue(), function (data) {
        PARENTUUID = b;
        Ext.getBody().mask();

        /*appMenuForm 變量保存在 Ext.AppMenuForm.js當中*/
        if (appMenuForm == undefined) {
            appMenuForm = Ext.create('AppMenuForm');
            /*載入關閉後的事件*/
            appMenuForm.on('closeEvent', function (obj) {
                /*重新整理畫面的內容*/
                var btnQuery = Ext.getCmp('menu.Query.Button');
                Ext.getBody().unmask();
                btnQuery.handler.call(btnQuery.scope);
            });
            /*設定開啟事內的條件*/
            appMenuForm.uuid = undefined;
            appMenuForm.setTitle('<img src="' + SYSTEM_URL_ROOT + '/css/images/menu.png" style="height:20px;vertical-align:middle;margin-right:5px;">選單【新增】');
            appMenuForm.parentUuid = PARENTUUID;
            appMenuForm.applicationHeadUuid = Ext.getCmp('menu_Query_Application').getValue();
            appMenuForm.show();
        } else {
            appMenuForm.setTitle('<img src="' + SYSTEM_URL_ROOT + '/css/images/menu.png" style="height:20px;vertical-align:middle;margin-right:5px;">選單【新增】');
            appMenuForm.uuid = undefined;
            appMenuForm.parentUuid = PARENTUUID;
            appMenuForm.applicationHeadUuid = Ext.getCmp('menu_Query_Application').getValue();
            appMenuForm.show();
        }
    });

    AppMenuTaskFlag = true;

}

function delMenu(uuid) {
    Ext.Msg.show({
        title: '刪除節點操作',
        msg: '確定執行刪除動作?',
        buttons: Ext.Msg.YESNO,
        fn: function (btn) {
            if (btn == "yes") {
                WS.MenuAction.deleteAppMenu(uuid, function (json) {
                    var btnQuery = Ext.getCmp('menu.Query.Button')
                    btnQuery.handler.call(btnQuery.scope)
                    AppMenuTaskFlag = true;
                });
            }
        }
    });

}


Ext.onReady(function () {
    /*:::加入Direct:::*/

    Ext.QuickTips.init();
    /*:::設定Compnay Store物件:::*/
    var storeApplication =
        Ext.create('Ext.data.Store', {
            successProperty: 'success',
            autoLoad: true,
            /*:::Table設定:::*/
            model: Ext.define('APPLICATION', {
                extend: 'Ext.data.Model',
                /*:::欄位設定:::*/
                fields: [
                    'CREATE_DATE',
                    'UPDATE_DATE',
                    'IS_ACTIVE',
                    'NAME',
                    'DESCRIPTION',
                    'ID',
                    'CREATE_USER',
                    'UPDATE_USER',
                    'WEB_SITE',
                    'UUID'
                ]
            }),
            pageSize: 10,
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
                paramOrder: ['pKeyword', 'page', 'limit', 'sort', 'dir'],
                extraParams: {
                    pKeyword: ''
                },
                simpleSortMode: true,
                listeners: {
                    exception: function (proxy, response, operation) {
                        Ext.MessageBox.show({
                            title: 'REMOTE EXCEPTION',
                            msg: operation.getError(),
                            icon: Ext.MessageBox.ERROR,
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
                load: function () {
                    if (storeApplication.getCount() > 0) {
                        Ext.getCmp('menu_Query_Application').setValue(storeApplication.data.getAt(0).data['UUID']);
                    }
                }
            },
            remoteSort: true,
            sorters: [{
                property: 'NAME'
            }]
        });

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

    Ext.define('AppMenuTree', {
        extend: 'Ext.data.TreeStore',
        root: {
            expanded: true,
        },
        autoLoad: false,
        successProperty: 'success',
        model: 'Model.AppMenu',
        nodeParam: 'id',
        proxy: {
            paramOrder: ['UUID'],
            type: 'direct',
            directFn: WS.MenuAction.loadMenuTree,
            extraParams: {
                "UUID": ''
            }
        }
    });



    thisTreeStore = Ext.create('AppMenuTree', {});

    function isActiveRenderer(value, id, r) {
        if (value == "Y")
            return "<img src='../../css/custimages/active.gif' style='height:20px;vertical-align:middle'>";
        else if (value == "N")
            return "<img src='../../css/custimages/unactive.gif' style='height:20px;vertical-align:middle'>";
    }

    /*設定元件*/
    AppPageQuery = Ext.widget({
        xtype: 'panel',
        title: '<img src="' + SYSTEM_URL_ROOT + '/css/images/menu.png" style="height:20px;vertical-align:middle;margin-right:5px;">選單維護',
        frame: true,

        //padding: 10,
        autoHeight: true,
        autoWidth: true,
        items: [{
            layout: 'column',
            padding: 10,
            border: 0,
            items: [{
                style: 'display:block; padding:2px 0px 0px 0px',
                xtype: 'label',
                text: '系統：'
            }, {
                xtype: 'combo',
                editable: false,
                store: storeApplication,
                displayField: 'NAME',
                valueField: 'UUID',
                id: 'menu_Query_Application',
                enableKeyEvents: true,
                listeners: {
                    'change': function (obj, value) {
                        var btnQuery = Ext.getCmp('menu.Query.Button')
                        btnQuery.handler.call(btnQuery.scope)
                        AppMenuTaskFlag = true;
                    },
                    keyup: function (e, t, eOpts) {
                        if (t.button == 12) {
                            this.up('panel').down("#btnQuery").handler();
                        }
                    }
                }
            }, {
                xtype: 'label',
                text: '',
                style: 'display:block; padding:4px 4px 4px 4px'
            }, {
                xtype: 'label',
                text: '',
                style: 'display:block; padding:4px 4px 4px 4px'
            }, {
                xtype: 'button',
                text: '<img src="' + SYSTEM_URL_ROOT + '/css/custimages/search.gif" style="height:20px;vertical-align:middle;margin-top:-2px;margin-right:5px;">查詢',
                style: 'display:block; padding:4px 0px 0px 0px',
                id: 'menu.Query.Button',
                itemId: 'btnQuery',
                handler: function () {
                    /*查詢按鈕*/
                    WS.MenuAction.loadTreeRoot(Ext.getCmp('menu_Query_Application').getValue(), function (data) {
                        if (data.UUID != undefined) {

                            thisTreeStore.getProxy().setExtraParam('UUID', data.UUID);
                            thisTreeStore.load({
                                params: {
                                    'UUID': data.UUID
                                }
                            });

                            AppMenuTaskFlag = true;
                        }
                    });

                }
            }, {
                xtype: 'label',
                text: '',
                style: 'display:block; padding:4px 4px 4px 4px'
            }]
        }, {
            xtype: 'button',
            text: '<img src="' + SYSTEM_URL_ROOT + '/css/custimages/add.gif" style="height:12px;vertical-align:middle;margin-top:-2px;margin-right:5px;">新增選單子節點',
            style: 'margin-left:5px',
            handler: function () {

               WS.MenuAction.loadTreeRoot(Ext.getCmp('menu_Query_Application').getValue(), function (data) {
                    PARENTUUID = data.UUID;

                    /*appMenuForm 變量保存在 Ext.AppMenuForm.js當中*/
                    if (appMenuForm == undefined) {
                        appMenuForm = Ext.create('AppMenuForm');
                        appMenuForm.pApplicationHeadUuid = Ext.getCmp('menu_Query_Application').getValue();
                        /*載入關閉後的事件*/
                        appMenuForm.on('closeEvent', function (obj) {
                            /*重新整理畫面的內容*/
                            var btnQuery = Ext.getCmp('menu.Query.Button');
                            btnQuery.handler.call(btnQuery.scope);
                        });
                        /*設定開啟事內的條件*/
                        appMenuForm.uuid = undefined;
                        appMenuForm.parentUuid = PARENTUUID;
                        appMenuForm.applicationHeadUuid = Ext.getCmp('menu_Query_Application').getValue();
                        appMenuForm.show();
                    } else {
                        appMenuForm.uuid = undefined;
                        appMenuForm.parentUuid = PARENTUUID;
                        appMenuForm.applicationHeadUuid = Ext.getCmp('menu_Query_Application').getValue();
                        appMenuForm.show();
                    }
                });

                AppMenuTaskFlag = true;


            }
        }, {
            xtype: 'treepanel',
            fieldLabel: '選單維護',
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
            columns: [{
                xtype: 'treecolumn',
                text: '選單',
                flex: 2,
                sortable: true,
                dataIndex: 'NAME_ZH_TW'
            }, {
                text: '順序',
                flex: .5,
                dataIndex: 'ORD',
                align: 'center',
                sortable: true
            }, {
                text: "維護",
                dataIndex: 'UUID',
                align: 'center',
                flex: 2,
                renderer: function (value, m, r) {
                    var id = Ext.id();
                    var dom;
                    if (r.getData().leaf == true) {
                        dom = "<input type='button' class='edit-button' onclick='openMenu(\"" + value + "\");' value='編輯'/> <input type='button' class='del-button' onclick='delMenu(\"" + value + "\");' value='刪除'/>  <input type='button'  class='add-button'   onclick='addChild(\"undefined\",\"" + value + "\");' value='新增子節點'/>";                        
                    } else {
                        dom = "<input type='button' class='edit-button' onclick='openMenu(\"" + value + "\");' value='編輯'/> <input type='button' class='del-button-disable' value='刪除'/>  <input type='button'  class='add-button'   onclick='addChild(\"undefined\",\"" + value + "\");' value='新增子節點'/>";
                    }
                    return dom;
                },
                sortable: false,
                hideable: false
            }],
            listeners: {
                beforeload: function (tree, node,eOpts) {
                    var myMask = new Ext.LoadMask(
                        Ext.getCmp('AppMenu.Tree'), {
                            msg: "資料載入中，請稍等...",
                            store: thisTreeStore,
                            removeMask: true
                        });
                    myMask.show();
                    /*樹在開始展開前的動作*/
                    if (node.isComplete() == false) {
                        //thisTreeStore.getProxy().setExtraParam('UUID', node.node.raw['UUID']);
                        //alert(node.getParams()["UUID"]);
                        if(node.getParams()["UUID"]!=undefined){
                            thisTreeStore.getProxy().setExtraParam('UUID', node.getParams()["UUID"]);
                        }else{
                            thisTreeStore.getProxy().setExtraParam('UUID', node.config.node.data["UUID"]);
                        }
                     
                    }
                },
                checkchange: function (a, b, c, d) {
                    var oUuid = a.data.UUID;
                    if (a.data.checked == true) {
                        /*表加入*/
                        WS.MenuAction.setAppMenuIsActive(oUuid, "1", function (ret) {
                            if (ret.success == false) {
                                Ext.MessageBox.show({
                                    title: 'WARNING',
                                    msg: "ok",
                                    icon: Ext.MessageBox.WARNING,
                                    buttons: Ext.Msg.OK
                                });
                            }
                        });
                    } else {
                        WS.MenuAction.setAppMenuIsActive(oUuid, "0", function (ret) {
                            if (ret.success == false) {
                                Ext.MessageBox.show({
                                    title: 'WARNING',
                                    msg: "ok",
                                    icon: Ext.MessageBox.WARNING,
                                    buttons: Ext.Msg.OK
                                });
                            }
                        });

                    }
                }
            }
        }]        
    });
    AppPageQuery.render('divMain');
});
</script>
			
			<div id="divMain" style="margin-bottom:5px;margin-top:35px;"></div>
<!-- 使用者session的檢查操作，當逾期時自動轉頁至登入頁面 -->
<script type="text/javascript" src='<%= Page.ResolveUrl("~/pageJs/keeySession.js")%>'></script>           
</asp:Content>
