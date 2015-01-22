<%@ Page Title="" Language="C#" MasterPageFile="~/mpStand.Master" AutoEventWireup="true" CodeBehind="attendant.aspx.cs" Inherits="Web.admin.basic.attendant"  EnableViewState="false"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript" src='<%= Page.ResolveUrl("~/js/shared/include-ext.js")%>'></script>
<script type="text/javascript" src='<%= Page.ResolveUrl("~/Proxy.ashx")%>'></script>
<script type="text/javascript" src='<%= Page.ResolveUrl("~/pageJs/Ext.CompanyForm.js")%>'></script>
<script type="text/javascript" src='<%= Page.ResolveUrl("~/pageJs/Ext.AttendantForm.js")%>'></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<script language="javascript" type="text/javascript">
var AttendantQuery = undefined;
var myForm = undefined;
Ext.onReady(function() {
    Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION + ".AttendantAction"));
    Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION + ".ADAction"));
    Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION + ".SyncClientAction"));
    Ext.QuickTips.init();
    var storeAttendant =
        Ext.create('Ext.data.Store', {
            successProperty: 'success',
            autoLoad: true,
            model: Ext.define('ATTEDNANTVV', {
                extend: 'Ext.data.Model',
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
            pageSize: 10,
            proxy: {
                type: 'direct',
                api: {
                    read: WS.AttendantAction.load
                },
                reader: {
                    root: 'data'
                },
                paramsAsHash: true,
                paramOrder: ['company_uuid', 'keyword', 'page', 'limit', 'sort', 'dir'],
                extraParams: {
                    company_uuid: '<%= getUser().COMPANY_UUID %>',
                    keyword: ''
                },
                simpleSortMode: true,
                listeners: {
                    exception: function(proxy, response, operation) {
                        if (!response.result.success) {
                            Ext.MessageBox.show({
                                title: 'Warning',
                                icon: Ext.MessageBox.WARNING,
                                buttons: Ext.Msg.OK,
                                msg: response.result.message
                            });
                        }
                    }
                }
            },
            remoteSort: true,
            sorters: [{
                property: 'C_NAME',
                direction: 'ASC'
            }]
        });

    function isActiveRenderer(value, id, r) {
        if (value == "Y")
            return "<img src='" + SYSTEM_URL_ROOT + "/css/custimages/active.gif' style='height:20px;vertical-align:middle'>";
        else if (value == "N")
            return "<img src='" + SYSTEM_URL_ROOT + "/css/custimages/unactive.gif' style='height:20px;vertical-align:middle'>";
    }

    AttendantQuery = Ext.widget({
        xtype: 'panel',
        title: '<img src="' + SYSTEM_URL_ROOT + '/css/images/man.png" style="height:20px;vertical-align:middle;margin-right:5px;">人員維護',
        frame: true,
        height: $(this).height() - 150,
        items: [{
            xtype: 'container',
            layout: 'hbox',
            margin: '5 5 5 5',
            items: [{
                xtype: 'textfield',
                fieldLabel: '關鍵字',
                labelWidth: 50,
                id: 'txt_search',
                margin: '2 0 0 0',
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
                width: 70,
                margin: '0 5 0 5',
                text: '<img src="' + SYSTEM_URL_ROOT + '/css/custimages/search.gif" style="height:20px;vertical-align:middle;"\>查詢',
                itemId: 'btnQuery',
                handler: function() {
                    storeAttendant.getProxy().setExtraParam('company_uuid', "<%= getUser().COMPANY_UUID %>");
                    storeAttendant.getProxy().setExtraParam('keyword', Ext.getCmp('txt_search').getValue());
                    storeAttendant.load();
                }
            }, {
                xtype: 'button',
                width: 70,
                margin: '0 5 0 5',
                text: '<img src="' + SYSTEM_URL_ROOT + '/css/custimages/clear.gif" style="height:20px;vertical-align:middle;"\>清除',
                handler: function() {
                    Ext.getCmp('txt_search').setValue('');
                }
            }]
        }, {
            xtype: 'gridpanel',
            store: storeAttendant,
            paramOrder: ['C_NAME'],
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
                                    myForm = Ext.create('AttendantForm', {});
                                    myForm.on('closeEvent', function(obj) {
                                        storeAttendant.load();
                                    });
                                };
                                myForm.setTitle('<img src="' + SYSTEM_URL_ROOT + '/css/images/man.png" style="height:20px;vertical-align:middle;margin-right:5px;">人員【維護】');
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
                header: "帳號",
                dataIndex: 'ACCOUNT',
                align: 'left',
                flex: 1
            }, {
                header: "名稱-繁中",
                dataIndex: 'C_NAME',
                align: 'left',
                flex: 1
            }, {
                header: "名稱-英文",
                dataIndex: 'E_NAME',
                align: 'left',
                flex: 1
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
                store: storeAttendant,
                displayInfo: true,
                displayMsg: '第{0}~{1}資料/共{2}筆',
                emptyMsg: "無資料顯示"
            }),
            tbar: [{
                text: '<img src="' + SYSTEM_URL_ROOT + '/css/custimages/add.gif" style="height:12px;vertical-align:middle;margin-top:-2px;margin-right:5px;">新增',
                handler: function() {
                    if (myForm == undefined) {
                        myForm = Ext.create('AttendantForm', {});
                        myForm.on('closeEvent', function(obj) {
                            storeAttendant.load();
                        });
                    }
                    myForm.setTitle('人員【新增】');
                    myForm.uuid = undefined;
                    myForm.show();
                }
            }, {
                text: '<img src="' + SYSTEM_URL_ROOT + '/css/images/Cloud_Sync.png" style="height:20px;vertical-align:middle;margin-top:-2px;margin-right:5px;">同步人員(AD)',
                hidden: <%= showAD() %> ,
                handler: function() {
                    Ext.getBody().mask();
                    WS.ADAction.loadUser(function(obj, jsonObj) {
                        if (jsonObj.result.success) {
                            Ext.MessageBox.show({
                                title: '同步AD人員',
                                icon: Ext.MessageBox.WARNING,
                                buttons: Ext.Msg.OK,
                                msg: '完成!',
                                fn: function() {
                                    Ext.getBody().unmask();
                                }
                            });
                            storeAttendant.loadPage(1);
                        } else {
                            Ext.MessageBox.show({
                                title: '同步AD人員',
                                icon: Ext.MessageBox.WARNING,
                                buttons: Ext.Msg.OK,
                                msg: jsonObj.result.message,
                                fn: function() {
                                    Ext.getBody().unmask();
                                }
                            });
                            storeAttendant.loadPage(1);
                        }
                    });
                }
            }, {
                text: '<img src="' + SYSTEM_URL_ROOT + '/css/images/Cloud_Sync.png" style="height:20px;vertical-align:middle;margin-top:-2px;margin-right:5px;">同步人員(主伺服器)',
                hidden: <%= showSync() %> ,
                handler: function() {
                    Ext.getBody().mask("正在處理中…請稍後…");

                    WS.SyncClientAction.SyncAttendant(function(obj, jsonObj) {
                        Ext.getBody().unmask();
                        if (jsonObj.result.success) {
                            Ext.MessageBox.show({
                                title: '同步人員(主伺服器)',
                                icon: Ext.MessageBox.INFO,
                                buttons: Ext.Msg.OK,
                                msg: '已成功完成同步作業。',
                                fn: function() {
                                    storeAttendant.load();
                                }
                            });
                        } else {
                            Ext.MessageBox.show({
                                title: '發生異常',
                                icon: Ext.MessageBox.INFO,
                                buttons: Ext.Msg.OK,
                                msg: jsonObj.result.message
                            });
                        }
                    });
                }
            }]
        }]
    });
    AttendantQuery.render('divMain');
});
</script>			
<div id="divMain" style="margin-bottom:5px;margin-top:35px;"></div>
<!-- 使用者session的檢查操作，當逾期時自動轉頁至登入頁面 -->
<script type="text/javascript" src='<%= Page.ResolveUrl("~/pageJs/keeySession.js")%>'></script>           
</asp:Content>