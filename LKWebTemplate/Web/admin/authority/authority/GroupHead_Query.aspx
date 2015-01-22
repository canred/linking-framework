<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/mpStand.Master" CodeBehind="GroupHead_Query.aspx.cs" Inherits="Web.admin.authority.authority.GroupHead_Query"  EnableViewState="false"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript" src='<%= Page.ResolveUrl("~/js/shared/include-ext.js")%>'></script>
<script type="text/javascript" src='<%= Page.ResolveUrl("~/Proxy.ashx")%>'></script>
<script type="text/javascript" src='<%= Page.ResolveUrl("~/pageJs/Ext.CompanyForm.js")%>'></script>
<script type="text/javascript" src='<%= Page.ResolveUrl("~/pageJs/Ext.GroupHeadForm.js")%>'></script>
<script type="text/javascript" src='<%= Page.ResolveUrl("~/pageJs/Ext.AttendantPicker.js")%>'></script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script language="javascript" type="text/javascript">
var GroupHeadQuery = undefined;
var AttendantQueryForm = undefined;
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
    Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION + ".GroupHeadAction"));
    Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION + ".AdminCompanyAction"));
    Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION + ".AttendantAction"));
    Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION + ".ApplicationAction"));

    Ext.QuickTips.init();
    /*:::設定Compnay Store物件:::*/
    var storeGroupHead =
        Ext.create('Ext.data.Store', {
            successProperty: 'success',
            autoLoad: true,
            /*:::Table設定:::*/
            model: Ext.define('GROUPHEADV', {
                extend: 'Ext.data.Model',
                /*:::欄位設定:::*/
                fields: [
                    'UUID',
                    'CREATE_DATE',
                    'UPDATE_DATE',
                    'IS_ACTIVE',
                    'NAME_ZH_TW',
                    'NAME_ZH_CN',
                    'NAME_EN_US',
                    'COMPANY_UUID',
                    'ID',
                    'APPLICATION_HEAD_UUID',
                    'APPLICATION_HEAD_ID',
                    'APPLICATION_HEAD_NAME'
                ]
            }),
            pageSize: 10,
            proxy: {
                type: 'direct',
                api: {
                    read: WS.GroupHeadAction.load
                },
                reader: {
                    root: 'data'
                },
                paramsAsHash: true,
                /*:::Direct Method Parameter Setting:::*/
                paramOrder: ['company_uuid', 'application_head_uuid', 'attendant_uuid', 'keyword', 'page', 'limit', 'sort', 'dir'],
                extraParams: {
                    company_uuid: '<%= getUser().COMPANY_UUID %>',
                    application_head_uuid: '',
                    attendant_uuid: '',
                    keyword: ''
                },
                simpleSortMode: true,
                listeners: {
                    exception: function (proxy, response, operation) {
                        Ext.MessageBox.show({
                            title: 'REMOTE EXCEPTION1',
                            msg: response.result.message,
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
                property: 'NAME_ZH_TW',
                direction: 'ASC'
            }]
        });

    var storeApplicationHead =
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
                            title: 'REMOTE EXCEPTION3',
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
                    this.insert(0, new this.model({
                        'NAME': '全部',
                        'UUID': ''
                    }));
                    Ext.getCmp('cmp_application').setValue('');
                }
            },
            remoteSort: true,
            sorters: [{
                property: 'NAME',
                direction: 'ASC'
            }]
        });

    var display_attendant_uuid = new Ext.form.TextField({
        readOnly: true,
        xtype: 'textfield',
        name: 'display_attendant_uuid',
        id: 'display_attendant_uuid',
        emptyText: '',
        allowBlank: true,
        width: 160
    });

    var fld_attendant_uuid = new Ext.form.TextField({
        readOnly: true,
        xtype: 'textfield',
        name: 'fld_attendant_uuid',
        id: 'fld_attendant_uuid',
        emptyText: '',
        allowBlank: true,
        width: 160,
        hidden: true
    });

    var btn_attendant = new Ext.create('Ext.Button', {
        xtype: 'button',
        style: 'margin-right:65px;margin-left:2px;',
        text: '<img src="' + SYSTEM_URL_ROOT + '/css/images/mouse_select_left.gif" style="height:18px;vertical-align:middle;margin-top:-2px;margin-left:5px;margin-right:5px;">',

        handler: function () {
            var companyUuid = '<%= getUser().COMPANY_UUID %>';
            if (AttendantQueryForm == undefined) {
                AttendantQueryForm = Ext.create('AttendantPicker', {});
                AttendantQueryForm.on('selectedEvent', function (record) {
                    Ext.getCmp('display_attendant_uuid').setValue(record.data["C_NAME"]);
                    Ext.getCmp('fld_attendant_uuid').setValue(record.data["UUID"]);
                    Ext.getCmp('btnQuery').handler();
                    AttendantQueryForm.hide();
                });
            }            
            AttendantQueryForm.companyUuid = companyUuid;
            AttendantQueryForm.show();
        }
    });

    /*設定元件*/
    GroupHeadQuery = Ext.widget({
        xtype: 'panel',
        title: '<img src="' + SYSTEM_URL_ROOT + '/css/images/permission.png" style="height:20px;vertical-align:middle;margin-right:5px;">權限維護',
        frame: true,
        //padding: 5,
        autoHeight: true,
        autoWidth: true,
        items: [{
            layout: 'column',
            padding: 5,
            border: 0,
            items: [{
                style: 'display:block; padding:2px 0px 0px 0px',
                xtype: 'label',
                text: '系統：',
                width: 70
            }, {
                xtype: 'combobox',
                queryMode: 'local',
                displayField: 'NAME',
                valueField: 'UUID',
                id: 'cmp_application',
                store: storeApplicationHead,
                editable: false,
                triggerAction: 'all',
                selectOnFocus: true,
                enableKeyEvents:true,
                listeners: {
                    afterrender: function (combo) {
                        var recordSelected = combo.getStore().getAt(0);

                    }
                    ,
                    keyup:function(e,t,eOpts){
                        if(t.button==12){
                            Ext.getCmp('btnQuery').handler();
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
                text: '關鍵字：',
                width: 100,
                padding: '0 0 0 40'
            }, {
                xtype: 'textfield',
                id: 'txt_search',
                 enableKeyEvents:true,
                 listeners:{
                    keyup:function(e,t,eOpts){                        
                        if(t.button==12){
                            Ext.getCmp('btnQuery').handler();
                        }
                    }
                }
            }]
        }, {
            layout: 'column',
            padding: 5,
            border: 0,
            items: [{
                    style: 'display:block; padding:2px 0px 0px 0px',
                    xtype: 'label',
                    text: '內含人員：',
                    width: 70
                },
                display_attendant_uuid, btn_attendant, {
                    xtype: 'button',
                    text: '<img src="' + SYSTEM_URL_ROOT + '/css/custimages/search.gif" style="height:20px;vertical-align:middle;margin-top:-2px;margin-right:5px;">查詢',
                    style: 'display:block; padding:4px 0px 0px 0px',
                    id:'btnQuery',
                    handler: function () {
                        storeGroupHead.getProxy().setExtraParam('company_uuid', "<%= this.getUser().COMPANY_UUID %>"); 
                        storeGroupHead.getProxy().setExtraParam('application_head_uuid', Ext.getCmp('cmp_application').getValue());
                        storeGroupHead.getProxy().setExtraParam('attendant_uuid', Ext.getCmp('fld_attendant_uuid').getValue());                        
                        storeGroupHead.getProxy().setExtraParam('keyword', Ext.getCmp('txt_search').getValue());
                        storeGroupHead.load();
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
                        Ext.getCmp('display_attendant_uuid').setValue('');
                        Ext.getCmp('fld_attendant_uuid').setValue('');
                    }
                }
            ]
        }, {
            xtype: 'gridpanel',
            store: storeGroupHead,
            paramOrder: ['NAME_ZH_TW'],
            idProperty: 'UUID',
            paramsAsHash: false,
            padding: 5,
            border:true,
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
                                    myForm = Ext.create('GroupHeadForm', {});
                                    myForm.on('closeEvent', function (obj) {
                                        storeGroupHead.load();
                                    });
                                }
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
                header: "群組繁中",
                dataIndex: 'NAME_ZH_TW',
                align: 'center',
                flex: 1
            }, {
                header: "群組簡中",
                dataIndex: 'NAME_ZH_CN',
                align: 'center',
                flex: 1
            }, {
                header: "群組英文",
                dataIndex: 'NAME_EN_US',
                align: 'center',
                flex: 1
            }, {
                header: '代碼',
                dataIndex: 'ID',
                align: 'center',
                flex: 1
            }],
            height: 450,
            tbarCfg: {
                buttonAlign: 'right'
            },
            bbar: Ext.create('Ext.toolbar.Paging', {
                store: storeGroupHead,
                displayInfo: true,
                displayMsg: '第{0}~{1}資料/共{2}筆',
                emptyMsg: "無資料顯示"
            }),
            tbar: [{
                text: '<img src="' + SYSTEM_URL_ROOT + '/css/custimages/add.gif" style="height:12px;vertical-align:middle;margin-top:-2px;margin-right:5px;">新增',
                handler: function () {
                    if (myForm == undefined) {
                        myForm = Ext.create('GroupHeadForm', {});
                        myForm.on('closeEvent', function (obj) {
                            storeGroupHead.load();
                        });
                    }
                    myForm.setTitle('群組定義【新增】');
                    myForm.uuid = undefined;
                    myForm.show();
                }
            }]
        }]
    });
    GroupHeadQuery.render('divMain');
});
</script>			
<div id="divMain" style="margin-bottom:5px;margin-top:35px;"></div>
<!-- 使用者session的檢查操作，當逾期時自動轉頁至登入頁面 -->
<script type="text/javascript" src='<%= Page.ResolveUrl("~/pageJs/keeySession.js")%>'></script>      
</asp:Content>