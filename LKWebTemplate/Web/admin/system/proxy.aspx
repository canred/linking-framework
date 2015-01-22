<%@ Page Title="" Language="C#" MasterPageFile="~/mpStand.Master" AutoEventWireup="true" CodeBehind="proxy.aspx.cs" Inherits="Web.admin.system.proxy"  EnableViewState="false"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript" src='<%= Page.ResolveUrl("~/js/shared/include-ext.js")%>'></script>
<script type="text/javascript" src='<%= Page.ResolveUrl("~/Proxy.ashx")%>'></script>
<script type="text/javascript" src='<%= Page.ResolveUrl("~/pageJs/Ext.CompanyForm.js")%>'></script>
<script type="text/javascript" src='<%= Page.ResolveUrl("~/pageJs/Ext.ProxyForm.js")%>'></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<script language="javascript" type="text/javascript">
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

Ext.onReady(function () {
    /*:::加入Direct:::*/
    //Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION + ".AppPageAction"));
    Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION + ".ProxyAction"));
    Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION + ".ApplicationAction"));
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
                        Ext.getCmp('function_Query_Application').setValue(storeApplication.data.getAt(0).data['UUID']);
                        storeProxy.getProxy().setExtraParam('pApplicationHeadUuid', Ext.getCmp('function_Query_Application').getValue());
                        storeProxy.load();
                    }
                }
            },
            remoteSort: true,
            sorters: [{
                property: 'NAME'
            }]
        });

    var storeProxy =
        Ext.create('Ext.data.Store', {
            successProperty: 'success',
            /*:::Table設定:::*/
            model: Ext.define('APPPAGE', {
                extend: 'Ext.data.Model',
                /*:::欄位設定:::*/
                fields: [
                   'UUID',
'PROXY_ACTION',
'PROXY_METHOD',
'DESCRIPTION',
'PROXY_TYPE',
'NEED_REDIRECT',
'REDIRECT_PROXY_ACTION',
'REDIRECT_PROXY_METHOD',
'APPLICATION_HEAD_UUID',
'REDIRECT_SRC',
                ]
            }),
            pageSize: 10,
            proxy: {
                type: 'direct',
                api: {
                    read: WS.ProxyAction.loadProxy
                },
                reader: {
                    root: 'data'
                },
                paramsAsHash: true,
                /*:::Direct Method Parameter Setting:::*/
                paramOrder: ['pApplicationHeadUuid', 'pKeyword', 'page', 'limit', 'sort', 'dir'],
                extraParams: {
                    pApplicationHeadUuid: '',
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
                load: function () {}
            },
            remoteSort: true,
            sorters: [{
                property: 'PROXY_ACTION'
            }]
        });

    function isActiveRenderer(value, id, r) {
        if (value == "Y")
            return "<img src='../../css/custimages/active.gif' style='height:20px;vertical-align:middle'>";
        else if (value == "N")
            return "<img src='../../css/custimages/unactive.gif' style='height:20px;vertical-align:middle'>";
    }

    /*設定元件*/
    AppPageQuery = Ext.widget({
        xtype: 'panel',
        /*功能清單*/
        title: '<img src="' + SYSTEM_URL_ROOT + '/css/images/source.png" style="height:20px;vertical-align:middle;margin-right:5px;">資源',
        frame: true,
        padding: 5,
        // autoHeight: true,
        // autoWidth: true,
        height:$(this).height()-150,
        items: [{
            layout: 'column',
            padding: 5,
            border: 0,
            items: [{
                style: 'display:block; padding:2px 0px 0px 0px',
                xtype: 'label',
                text: '系統：'
            }, {
                xtype: 'combo',
                editable: false,
                store: storeApplication,
                enableKeyEvents: true,
                displayField: 'NAME',
                valueField: 'UUID',
                id: 'function_Query_Application',
                listeners: {                    
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
                style: 'display:block; padding:2px 0px 0px 0px',
                xtype: 'label',
                text: '關鍵字：'
            }, {
                xtype: 'textfield',
                id: 'txt_search',
                enableKeyEvents: true,
                listeners: {
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
                xtype: 'button',
                text: '<img src="' + SYSTEM_URL_ROOT + '/css/custimages/search.gif" style="height:20px;vertical-align:middle;margin-top:-2px;margin-right:5px;">查詢',
                style: 'display:block; padding:4px 0px 0px 0px',
                itemId: 'btnQuery',
                handler: function () {
                    storeProxy.getProxy().setExtraParam('pKeyword', Ext.getCmp('txt_search').getValue());
                    storeProxy.getProxy().setExtraParam('pApplicationHeadUuid', Ext.getCmp('function_Query_Application').getValue());
                    storeProxy.loadPage(1);
                }
            }, {
                xtype: 'label',
                text: '',
                style: 'display:block; padding:4px 4px 4px 4px'
            }, {
                xtype: 'button',
                text: '<img src="' + SYSTEM_URL_ROOT + '/css/custimages/clear.gif" style="height:18px;vertical-align:middle;margin-top:-2px;margin-right:5px;">清除',
                style: 'display:block; padding:4px 0px 0px 0px',
                handler: function () {
                    Ext.getCmp('txt_search').setValue('');
                }
            }]
        }, {
            xtype: 'gridpanel',
            store: storeProxy,
            border:true,
            paramOrder: ['NAME'],
            idProperty: 'UUID',
            paramsAsHash: false,
            padding: 5,
            columns: [{
                header: "編輯",
                dataIndex: 'UUID',
                align: 'center',
                renderer: function (value, m, r) {
                    var id = Ext.id();
                    Ext.defer(function () {
                        Ext.widget('button', {
                            renderTo: id,
                            text: '<img src="' + SYSTEM_URL_ROOT + '/css/custimages/edit.gif" style="height:12px;vertical-align:middle;margin-right:5px;margin-top:-2px;">&nbsp;編輯',
                            width: 75,
                            handler: function () {
                                if (myForm == undefined) {
                                    myForm = Ext.create('ProxyForm', {});
                                    myForm.on('closeEvent', function (obj) {
                                        storeProxy.load();
                                    });
                                }
                                myForm.setTitle('<img src="' + SYSTEM_URL_ROOT + '/css/images/source.png" style="height:20px;vertical-align:middle;margin-right:5px;">資源【維護】');
                                myForm.uuid = value;
                                myForm.show();
                            }
                        });
                    }, 50);
                    return Ext.String.format('<div id="{0}"></div>', id);
                },
                sortable: false,
                hideable: false
            }, {

                 header: "Action",
                dataIndex: 'PROXY_ACTION',
                align: 'left',
                flex: 2
            }, {

                header: "Method",
                dataIndex: 'PROXY_METHOD',
                align: 'left',
                flex: 2
            }, {

                 header: "功能描述",
                align: 'left',
                dataIndex: 'DESCRIPTION',
                flex: 2,
                renderer: function (value) {
                    return '<div align="left">' + value + '</div>';
                }
            }, {

                 header: "方式",
                align: 'center',
                dataIndex: 'PROXY_TYPE',
                flex: 1,
                renderer: function (value) {
                    return '<div align="left">' + value + '</div>';
                }

            }
            , {
                 header: '跨服務',
                dataIndex: 'NEED_REDIRECT',
                align: 'center',
                flex: 1,
                renderer: isActiveRenderer
            }
             , {
                 header: "Action(跨服務)",
                dataIndex: 'REDIRECT_SRC',
                align: 'left',
                flex: 1,
                hidden:true
            }
            , {
                 header: "Action(跨服務)",
                dataIndex: 'REDIRECT_PROXY_ACTION',
                align: 'left',
                flex: 1,
                hidden:true
            }
            , {
                 header: "Method(跨服務)",
                dataIndex: 'REDIRECT_PROXY_METHOD',
                align: 'left',
                flex: 1,
                hidden:true
            }],
            //anchor: '95%',
            //height: 470,
            height:$(this).height()-240,
            tbarCfg: {
                buttonAlign: 'right'
            },
            bbar: Ext.create('Ext.toolbar.Paging', {
                store: storeProxy,
                displayInfo: true,
                displayMsg: '第{0}~{1}資料/共{2}筆',
                emptyMsg: "無資料顯示"
            }),
            tbar: [{
                text: '<img src="' + SYSTEM_URL_ROOT + '/css/custimages/add.gif" style="height:12px;vertical-align:middle;margin-top:-2px;margin-right:5px;">新增',
                handler: function () {

                    if (myForm == undefined) {
                        myForm = Ext.create('ProxyForm', {});
                        myForm.on('closeEvent', function (obj) {
                            storeProxy.load();
                        });
                    }
                    myForm.setTitle('<img src="' + SYSTEM_URL_ROOT + '/css/images/source.png" style="height:20px;vertical-align:middle;margin-right:5px;">資源【新增】');
                    myForm.uuid = undefined;
                    myForm.show();

                }
            }]

        }]
    });
    AppPageQuery.render('divMain');
});
</script>
			
			<div id="divMain" style="margin-bottom:5px;margin-top:35px;"></div>
<!-- 使用者session的檢查操作，當逾期時自動轉頁至登入頁面 -->
<script type="text/javascript" src='<%= Page.ResolveUrl("~/pageJs/keeySession.js")%>'></script>           
</asp:Content>

