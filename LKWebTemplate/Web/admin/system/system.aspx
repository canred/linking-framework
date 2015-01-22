<%@ Page Title="" Language="C#" MasterPageFile="~/mpStand.Master" AutoEventWireup="true" CodeBehind="system.aspx.cs" Inherits="Web.admin.system.system" EnableViewState="false"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript" src='<%= Page.ResolveUrl("~/js/shared/include-ext.js")%>'></script>
<script type="text/javascript" src='<%= Page.ResolveUrl("~/Proxy.ashx")%>'></script>

<script type="text/javascript" src='<%= Page.ResolveUrl("~/pageJs/Ext.CompanyForm.js")%>'></script>
<script type="text/javascript" src='<%= Page.ResolveUrl("~/pageJs/Ext.ApplicationHeadForm.js")%>'></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<script language="javascript" type="text/javascript">
var ApplicationQuery = undefined;
var myForm = undefined;
Ext.onReady(function() {
    Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION + ".ApplicationAction"));
    Ext.QuickTips.init();
    var storeApplication =
        Ext.create('Ext.data.Store', {
            successProperty: 'success',
            autoLoad: true,
            model: Ext.define('APPLICATION', {
                extend: 'Ext.data.Model',
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
                paramOrder: ['pKeyword', 'page', 'limit', 'sort', 'dir'],
                extraParams: {
                    pKeyword: ''
                },
                simpleSortMode: true,
                listeners: {
                    exception: function(proxy, response, operation) {
                        Ext.MessageBox.show({
                            title: 'REMOTE EXCEPTION',
                            msg: operation.getError(),
                            icon: Ext.MessageBox.ERROR,
                            buttons: Ext.Msg.OK
                        });
                    }
                }
            },
            remoteSort: true,
            sorters: [{
                property: 'NAME'
            }]
        });

    function isActiveRenderer(value, id, r) {
        if (value == "Y")
            return "<img src='" + SYSTEM_URL_ROOT + "/css/custimages/active.gif' style='height:20px;vertical-align:middle'>";
        else if (value == "N")
            return "<img src='" + SYSTEM_URL_ROOT + "/css/custimages/unactive.gif' style='height:20px;vertical-align:middle'>";
    }

    ApplicationQuery = Ext.widget({
        xtype: 'panel',
        title: '<img src="' + SYSTEM_URL_ROOT + '/css/images/application.png" style="height:20px;vertical-align:middle;margin-right:5px;">系統維護',
        frame: true,
        height: $(this).height() - 150,
        items: [{
            xtype: 'container',
            layout: 'hbox',
            margin: 5,
            items: [{
                xtype: 'textfield',
                id: 'txt_search',
                labelWidth: 50,
                margin: '2 0 0 0',
                fieldLabel: '關鍵字',
                enableKeyEvents: true,
                listeners: {
                    keyup: function(e, t, eOpts) {
                        if (t.button == 12) {
                            this.up('panel').down("#btnQuery").handler();
                        }
                    }
                }
            }, {
                xtype: 'button',
                text: '<img src="' + SYSTEM_URL_ROOT + '/css/custimages/search.gif" style="height:20px;vertical-align:middle;">查詢',
                width: 70,
                margin: '0 5 0 5',
                itemId: 'btnQuery',
                handler: function() {
                    storeApplication.getProxy().setExtraParam('pKeyword', Ext.getCmp('txt_search').getValue());
                    storeApplication.load();
                }
            }, {
                xtype: 'button',
                text: '<img src="' + SYSTEM_URL_ROOT + '/css/custimages/clear.gif" style="height:20px;vertical-align:middle;">清除',
                width: 70,
                margin: '0 5 0 5',
                handler: function() {
                    Ext.getCmp('txt_search').setValue('');
                }
            }]
        }, {
            xtype: 'gridpanel',
            store: storeApplication,
            paramOrder: ['NAME'],
            idProperty: 'UUID',
            paramsAsHash: false,
            padding: 5,
            border: true,
            height: $(this).height() - 240,
            columns: [{
                header: "編輯",
                dataIndex: 'UUID',
                align: 'center',
                renderer: function(value, m, r) {
                    var id = Ext.id();
                    Ext.defer(function() {
                        Ext.widget('button', {
                            renderTo: id,
                            text: '<img src="' + SYSTEM_URL_ROOT + '/css/custimages/edit.gif" style="height:12px;vertical-align:middle;margin-right:5px;margin-top:-2px;">&nbsp;編輯',
                            width: 75,
                            handler: function() {
                                if (myForm == undefined) {
                                    myForm = Ext.create('ApplicationHeadForm', {});
                                    myForm.on('closeEvent', function(obj) {
                                        storeApplication.load();
                                    });
                                }
                                myForm.setTitle('<img src="' + SYSTEM_URL_ROOT + '/css/images/application.png" style="height:20px;vertical-align:middle;margin-right:5px;margin-top:-2px;">&nbsp;系統【維護】');
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
                header: "名稱",
                dataIndex: 'NAME',
                align: 'left',
                flex: 1
            }, {
                header: "描述",
                align: 'left',
                dataIndex: 'DESCRIPTION',
                flex: 1,
                renderer: function(value) {
                    return '<div align="left">' + value + '</div>';
                }
            }, {

                header: "ID代碼",
                dataIndex: 'ID',
                align: 'left',
                flex: 1
            }, {

                header: "網址",
                align: 'left',
                dataIndex: 'WEB_SITE',
                flex: 1,
                renderer: function(value) {
                    return '<div align="left">' + value + '</div>';
                }
            }, {
                header: '啟用',
                dataIndex: 'IS_ACTIVE',
                align: 'center',
                flex: 1,
                renderer: isActiveRenderer
            }],
            tbarCfg: {
                buttonAlign: 'right'
            },
            bbar: Ext.create('Ext.toolbar.Paging', {
                store: storeApplication,
                displayInfo: true,
                displayMsg: '第{0}~{1}資料/共{2}筆',
                emptyMsg: "無資料顯示"
            }),
            tbar: [{
                text: '<img src="' + SYSTEM_URL_ROOT + '/css/custimages/add.gif" style="height:12px;vertical-align:middle;margin-top:-2px;margin-right:5px;">新增',
                handler: function() {
                    if (myForm == undefined) {
                        myForm = Ext.create('ApplicationHeadForm', {});
                        myForm.on('closeEvent', function(obj) {
                            storeApplication.load();
                        });
                    }
                    myForm.setTitle('<img src="' + SYSTEM_URL_ROOT + '/css/images/application.png" style="height:20px;vertical-align:middle;margin-right:5px;margin-top:-2px;">&nbsp;系統【新增】');
                    myForm.uuid = undefined;
                    myForm.show();
                }
            }]
        }]
    });
    ApplicationQuery.render('divMain');
});
</script>
			
			<div id="divMain" style="margin-bottom:5px;margin-top:35px;"></div>
<!-- 使用者session的檢查操作，當逾期時自動轉頁至登入頁面 -->
<script type="text/javascript" src='<%= Page.ResolveUrl("~/pageJs/keeySession.js")%>'></script>           
</asp:Content>
