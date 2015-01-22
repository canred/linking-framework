<%@ Page Title="" Language="C#" MasterPageFile="~/mpStand.Master" AutoEventWireup="true" CodeBehind="company.aspx.cs" Inherits="Web.admin.basic.mainpage" EnableViewState="false"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript" src='<%= Page.ResolveUrl("~/js/shared/include-ext.js")%>'></script>
<script type="text/javascript" src='<%= Page.ResolveUrl("~/Proxy.ashx")%>'></script>
<script type="text/javascript" src='<%= Page.ResolveUrl("~/pageJs/Ext.CompanyForm.js")%>'></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script language="javascript" type="text/javascript">
var CompanyQuery = undefined;
var myForm = undefined;
Ext.onReady(function() {
    Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION + ".AdminCompanyAction"));
    Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION + ".ADAction"));
    Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION + ".SyncClientAction"));
    Ext.QuickTips.init();
    var storeCompany =
        Ext.create('Ext.data.Store', {
            successProperty: 'success',
            autoLoad: true,
            model: Ext.define('COMPANY', {
                extend: 'Ext.data.Model',
                fields: ['UUID', 'ID', 'C_NAME', 'E_NAME', 'WEEK_SHIFT', 'NAME_ZH_CN', 'IS_ACTIVE']
            }),
            pageSize: 10,
            proxy: {
                type: 'direct',
                api: {
                    read: WS.AdminCompanyAction.loadCompany
                },
                reader: {
                    root: 'data'
                },
                paramsAsHash: true,
                paramOrder: ['pKeyword', 'pIsActive', 'page', 'limit', 'sort', 'dir'],
                extraParams: {
                    pKeyword: '',
                    pIsActive: 'Y'
                },
                simpleSortMode: true,
                listeners: {
                    exception: function(proxy, response, operation) {
                        Ext.MessageBox.show({
                            title: 'Warning',
                            msg: response.result.message,
                            icon: Ext.MessageBox.ERROR,
                            buttons: Ext.Msg.OK
                        });
                    },
                    beforeload: function() {
                        alert('beforeload proxy');
                    }
                }
            },
            listeners: {
                write: function(proxy, operation) {},
                read: function(proxy, operation) {},
                beforeload: function() {},
                afterload: function() {},
                load: function() {}
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

    CompanyQuery = Ext.widget({
        xtype: 'panel',
        title: '<img src="' + SYSTEM_URL_ROOT + '/css/images/company.png" style="height:20px;vertical-align:middle;margin-right:5px;">公司維護',
        frame: true,
        padding: 5,
        border: false,
        height: $(document).height() - 150,
        autoWidth: true,
        padding: '5 20 5 5',
        items: [{
            xtype: 'container',
            layout: 'hbox',
            margin: '5 0 0 5',
            items: [{
                xtype: 'textfield',
                fieldLabel: '關鍵字',
                margin: '2 0 0 5',
                id: 'txt_search',
                labelWidth: 50,
                enableKeyEvents: true,
                listeners: {
                    keyup: function(e, t, eOpts) {
                        if (t.button == 12) {
                            this.up('panel').down("#btnQuery").handler();
                        }
                    }
                }
            }, {
                xtype: 'combobox',
                queryMode: 'local',
                fieldLabel: '是否啟用',
                margin: '2 0 0 5',
                labelWidth: 60,
                displayField: 'name',
                valueField: 'value',
                id: 'cmb_isActive',
                editable: false,
                store: {
                    xtype: 'store',
                    fields: ['name', 'value'],
                    data: [{
                        name: "啟用",
                        value: "Y"
                    }, {
                        name: "不啟用",
                        value: "N"
                    }]
                },
                value: 'Y',
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
                text: '<img src="' + SYSTEM_URL_ROOT + '/css/custimages/search.gif" style="height:20px;vertical-align:middle;"/>查詢',
                margin: '0 0 0 5',
                itemId: 'btnQuery',
                width: 70,
                handler: function() {
                    storeCompany.getProxy().setExtraParam('pKeyword', Ext.getCmp('txt_search').getValue());
                    storeCompany.getProxy().setExtraParam('pIsActive', Ext.getCmp('cmb_isActive').getValue());
                    storeCompany.load();
                }
            }, {
                xtype: 'button',
                width: 70,
                margin: '0 0 0 5',
                text: '<img src="' + SYSTEM_URL_ROOT + '/css/custimages/clear.gif" style="height:20px;vertical-align:middle;"/>清除',
                handler: function() {
                    Ext.getCmp('txt_search').setValue('');
                    Ext.getCmp('cmb_isActive').setValue('Y');
                }
            }]
        }, {
            xtype: 'gridpanel',
            store: storeCompany,
            paramOrder: ['C_NAME'],
            idProperty: 'UUID',
            paramsAsHash: false,
            border: true,
            height: $(this).height() - 240,
            padding: '5 15 5 5',
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
                                    myForm = Ext.create('CompanyForm', {});
                                    myForm.on('closeEvent', function(obj) {
                                        storeCompany.load();
                                    });
                                }
                                myForm.setTitle('<img src="' + SYSTEM_URL_ROOT + '/css/images/company.png" style="height:20px;vertical-align:middle;margin-right:5px;">公司【維護】');
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
                header: "名稱-繁中",
                dataIndex: 'C_NAME',
                align: 'left',
                flex: 1
            }, {
                header: "名稱-簡中",
                align: 'left',
                dataIndex: 'NAME_ZH_CN',
                flex: 1,
                renderer: function(value) {
                    return '<div align="left">' + value + '</div>';
                }
            }, {
                header: "名稱-英文",
                dataIndex: 'E_NAME',
                align: 'left',
                flex: 1
            }, {
                header: "每週第一天為",
                align: 'left',
                dataIndex: 'WEEK_SHIFT',
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
                store: storeCompany,
                displayInfo: true,
                displayMsg: '第{0}~{1}資料/共{2}筆',
                emptyMsg: "無資料顯示"
            }),
            tbar: [{
                text: '<img src="' + SYSTEM_URL_ROOT + '/css/custimages/add.gif" style="height:12px;vertical-align:middle;margin-top:-2px;margin-right:5px;">新增',
                handler: function() {
                    if (myForm == undefined) {
                        myForm = Ext.create('CompanyForm', {});
                        myForm.on('closeEvent', function(obj) {
                            storeCompany.load();
                        });
                    }
                    myForm.setTitle('<img src="' + SYSTEM_URL_ROOT + '/css/images/company.png" style="height:20px;vertical-align:middle;margin-right:5px;">公司【新增】');
                    myForm.uuid = undefined;
                    myForm.show();
                }
            }, {
                text: '<img src="' + SYSTEM_URL_ROOT + '/css/images/Cloud_Sync.png" style="height:20px;vertical-align:middle;margin-top:-2px;margin-right:5px;">同步公司(主伺服器)',
                hidden: <%= showSync() %> ,
                handler: function() {
                    Ext.getBody().mask("正在處理中…請稍後…");

                    WS.SyncClientAction.SyncCompany(function(obj, jsonObj) {
                        Ext.getBody().unmask();
                        if (jsonObj.result.success) {
                            Ext.MessageBox.show({
                                title: '同步公司(主伺服器)',
                                icon: Ext.MessageBox.INFO,
                                buttons: Ext.Msg.OK,
                                msg: '已成功完成同步作業。',
                                fn: function() {
                                    storeCompany.load();
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
    CompanyQuery.render('divMain');
});

</script>
<div id="divMain" style="margin-bottom:5px;margin-top:35px;"></div>
<!-- 使用者session的檢查操作，當逾期時自動轉頁至登入頁面 -->
<script type="text/javascript" src='<%= Page.ResolveUrl("~/pageJs/keeySession.js")%>'></script>           
</asp:Content>





