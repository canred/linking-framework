<%@ Page Title="" Language="C#" MasterPageFile="~/mpEmpty.Master" AutoEventWireup="true" CodeBehind="changeattendant.aspx.cs" Inherits="Web.admin.basic.changeattendant"  EnableViewState="false"  %>
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
    Ext.require([
    'Ext.grid.*',
    'Ext.data.*',
    'Ext.util.*',
    'Ext.toolbar.Paging',
    'Ext.ux.PreviewPlugin',
    'Ext.ModelManager',
    'Ext.tip.QuickTipManager'
]);

    Ext.onReady(function () {
        /*:::加入Direct:::*/
        Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION + ".CompanyAction"));
        Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION + ".AttendantAction"));
        Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION + ".UserAction"));

        Ext.QuickTips.init();
        /*:::設定Compnay Store物件:::*/
        var storeAttendant =
        Ext.create('Ext.data.Store', {
            successProperty: 'success',
            autoLoad: true,
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
            pageSize: 10,
            proxy: {
                type: 'direct',
                api: {
                    read: WS.AttendantAction.loadAnyWhere
                },
                reader: {
                    root: 'data'
                },
                paramsAsHash: true,
                /*:::Direct Method Parameter Setting:::*/
                paramOrder: ['company_uuid', 'keyword', 'page', 'limit', 'sort', 'dir'],
                extraParams: {
                    company_uuid: '',
                    keyword: ''
                },
                simpleSortMode: true,
                listeners: {
                    exception: function (proxy, response, operation) {
                        // if(!response.result.success){
                        //     Ext.MessageBox.show({
                        //         title:'Warning',
                        //         icon : Ext.MessageBox.WARNING,
                        //         buttons : Ext.Msg.OK,
                        //         msg :response.result.message
                        //     });                                
                        // }
                    },
                    beforeload: function () {
                        alert('beforeload proxy');
                    }
                }
            },
            listeners: {
                write: function (proxy, operation) { },
                read: function (proxy, operation) { },
                beforeload: function () { },
                afterload: function () { },
                load: function () { }
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

        /*設定元件*/
        AttendantQuery = Ext.widget({
            xtype: 'panel',
            title: '<img src="' + SYSTEM_URL_ROOT + '/css/images/man.png" style="height:20px;vertical-align:middle;margin-right:5px;">人員身份切換',
            frame: true,
            padding: 5,
            autoHeight: true,
            //autoWidth: true,
            width: 700,

            items: [{
                layout: 'column',
                padding: 10,
                border: 0,
                items: [
            {
                xtype: 'combo',
                fieldLabel: '公司',
                //id: 'id',
                //queryMode : 'remote',

                displayField: 'C_NAME',
                valueField: 'UUID',
                labelAlign: 'right',
                itemId: 'cmbCompany',
                editable: false,
                hidden: false,
                store: Ext.create('Ext.data.Store', {
                    extend: 'Ext.data.Store',
                    autoLoad: false,
                    model: Ext.define('VGhgFile', {
                        extend: 'Ext.data.Model',
                        /*:::欄位設定:::*/
                        fields: ['C_NAME', 'UUID']
                    }),
                    pageSize: 999,
                    proxy: {
                        type: 'direct',
                        api: {
                            read: WS.CompanyAction.getAllCompany
                        },
                        reader: {
                            root: 'data'
                        },
                        paramsAsHash: true,
                        paramOrder: ['page', 'limit', 'sort', 'dir'],
                        extraParams: {
                            'page': 1,
                            'limit': 9999,
                            'sort': 'C_NAME',
                            'dir': 'ASC'
                        },
                        simpleSortMode: true,
                        listeners: {
                            exception: function (proxy, response, operation) {
                                Ext.MessageBox.show({
                                    title: 'REMOTE EXCEPTON A',
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
                    sorters: [{
                        property: 'C_NAME',
                        direction: 'ASC'
                    }]
                }),
                //readOnlyCls : 'readOnly',
                listeners: {
                    'select': function (combo, records, eOpts) {

                    },
                    'beforedeselect': function (combo, record, index, eOpts) {

                    },
                    'beforeselect': function (combo, record, index, eOpts) {

                    }
                }
            },
            {
                style: 'display:block; padding:2px 0px 0px 0px',
                xtype: 'label',
                text: '關鍵字：',
                margin: '0 0 0 5'
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

                    storeAttendant.getProxy().setExtraParam('company_uuid', this.up('panel').down("#cmbCompany").getValue());
                    storeAttendant.getProxy().setExtraParam('keyword', Ext.getCmp('txt_search').getValue());
                    storeAttendant.load();
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
                store: storeAttendant,
                paramOrder: ['C_NAME'],
                idProperty: 'UUID',
                paramsAsHash: false,
                padding: 5,
                columns: [{
                    header: "切換",
                    dataIndex: 'UUID',
                    align: 'center',
                    renderer: function (value, m, r) {
                        var id = Ext.id();
                        var companyUuid = this.up('panel').down('#cmbCompany').getValue();
                        Ext.defer(function () {
                            Ext.widget('button', {
                                renderTo: id,
                                text: '<img src="' + SYSTEM_URL_ROOT + '/css/custimages/edit.gif" style="height:12px;vertical-align:middle;margin-right:5px;margin-top:-2px;">&nbsp;切換',
                                width: 75,
                                handler: function () {
                                    WS.UserAction.changeAccount(companyUuid, r.data["ACCOUNT"], function (josnObj) {
                                        if (josnObj.success != undefined) {
                                            location.href = '<%= Page.ResolveUrl(LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().DefaultPage)%>';
                                        }
                                    })
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
                    align: 'center',
                    flex: 1
                }, {
                    header: "名稱-繁中",
                    dataIndex: 'C_NAME',
                    align: 'center',
                    flex: 1
                }, {
                    header: "名稱-英文",
                    dataIndex: 'E_NAME',
                    align: 'center',
                    flex: 1
                }, {
                    header: '啟用',
                    dataIndex: 'IS_ACTIVE',
                    align: 'center',
                    flex: 1,
                    renderer: isActiveRenderer
                }],
                height: 450,

                bbar: Ext.create('Ext.toolbar.Paging', {
                    store: storeAttendant,
                    displayInfo: true,
                    displayMsg: '第{0}~{1}資料/共{2}筆',
                    emptyMsg: "無資料顯示"
                })

            }]
        });
        AttendantQuery.render('divMain');
    });
</script>			
<center>
<div id="divMain" style="margin-bottom:5px;margin-top:35px;"></div>
</center>
  
</asp:Content>