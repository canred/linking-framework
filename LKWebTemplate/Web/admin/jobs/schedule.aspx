<%@ Page Title="" Language="C#" MasterPageFile="~/mpStand.Master" AutoEventWireup="true" CodeBehind="schedule.aspx.cs" Inherits="Web.admin.basic.attendant"  EnableViewState="false"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript" src='<%= Page.ResolveUrl("~/js/shared/include-ext.js")%>'></script>
<script type="text/javascript" src='<%= Page.ResolveUrl("~/Proxy.ashx")%>'></script>
<script type="text/javascript" src='<%= Page.ResolveUrl("~/pageJs/Ext.CompanyForm.js")%>'></script>
<script type="text/javascript" src='<%= Page.ResolveUrl("~/pageJs/Ext.ScheduleForm.js")%>'></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<script language="javascript" type="text/javascript">
var ScheduleQuery = undefined;
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
    Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION + ".ScheduleAction"));

    Ext.QuickTips.init();
    /*:::設定Compnay Store物件:::*/
    var storeSchedule =
        Ext.create('Ext.data.Store', {
            successProperty: 'success',
            autoLoad: true,
            /*:::Table設定:::*/
            model: Ext.define('SCHEDULE', {
                extend: 'Ext.data.Model',
                /*:::欄位設定:::*/
                fields: [
                   'UUID',
'SCHEDULE_NAME',
'SCHEDULE_END_DATE',
'LAST_RUN_TIME',
'LAST_RUN_STATUS',
'IS_CYCLE',
'SINGLE_DATE',
'HOUR',
'MINUTE',
'CYCLE_TYPE',
'C_MINUTE',
'C_HOUR',
'C_DAY',
'C_WEEK',
'C_DAY_OF_WEEK',
'C_MONTH',
'C_DAY_OF_MONTH',
'C_WEEK_OF_MONTH',
'C_YEAR',
'C_WEEK_OF_YEAR',
'RUN_URL',
'RUN_URL_PARAMETER',
'RUN_ATTENDANT_UUID',
'IS_ACTIVE',
'START_DATE',
'RUN_SECURITY'
                ]
            }),
            pageSize: 10,
            proxy: {
                type: 'direct',
                api: {
                    read: WS.ScheduleAction.loadSchedule
                },
                reader: {
                    root: 'data'
                },
                paramsAsHash: true,
                /*:::Direct Method Parameter Setting:::*/
                paramOrder: [ 'keyword', 'page', 'limit', 'sort', 'dir'],
                extraParams: {                    
                    keyword: ''
                },
                simpleSortMode: true,
                listeners: {
                    exception: function (proxy, response, operation) {
                            if(!response.result.success){
                                Ext.MessageBox.show({
                                    title:'Warning',
                                    icon : Ext.MessageBox.WARNING,
                                    buttons : Ext.Msg.OK,
                                    msg :response.result.message
                                });                                
                            }
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
                property: 'SCHEDULE_NAME',
                direction: 'ASC'
            }]
        });

    function isActiveRenderer(value, id, r) {
        if (value == "Y")
            return "<img src='" + SYSTEM_URL_ROOT + "/css/custimages/active.gif' style='height:20px;vertical-align:middle'>";
        else if (value == "N")
            return "<img src='" + SYSTEM_URL_ROOT + "/css/custimages/unactive.gif' style='height:20px;vertical-align:middle'>";
    }

    function isStatusRenderer(value, id, r) {
        if (value == "OK")
            return "<img src='" + SYSTEM_URL_ROOT + "/css/custimages/active.gif' style='height:20px;vertical-align:middle'>";
        else if (value == "N")
            return "<img src='" + SYSTEM_URL_ROOT + "/css/custimages/unactive.gif' style='height:20px;vertical-align:middle'>";
    }

    /*設定元件*/
    ScheduleQuery = Ext.widget({
        xtype: 'panel',
        title: '<img src="' + SYSTEM_URL_ROOT + '/css/images/schedule.png" style="height:20px;vertical-align:middle;margin-right:5px;">排程工作',
        frame: true,
        //padding: 5,
        height:$(this).height()-150,
        items: [{
            layout: 'column',
            padding: 10,
            border: 0,
            items: [{
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
                    storeSchedule.getProxy().setExtraParam('keyword', Ext.getCmp('txt_search').getValue());
                    storeSchedule.load();
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
            store: storeSchedule,
            paramOrder: ['SCHEDULE_NAME'],
            idProperty: 'UUID',
            paramsAsHash: false,
            padding: 5,
            height:$(this).height()-240,
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
                                    myForm = Ext.create('ScheduleForm', {});
                                    myForm.on('closeEvent', function (obj) {
                                        storeSchedule.load();
                                    });
                                }
                                myForm.setTitle('<img src="' + SYSTEM_URL_ROOT + '/css/images/schedule.png" style="height:20px;vertical-align:middle;margin-right:5px;">工作排程【維護】');
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
                dataIndex: 'SCHEDULE_NAME',
                align: 'center',
                flex: 2                
            }, {
                header: "週期性",
                dataIndex: 'IS_CYCLE',
                name:'IS_CYCLE',
                
                flex: 1,
                 renderer: isActiveRenderer
            }, {
                header: "執行任務",
                dataIndex: 'RUN_URL',
                align: 'center',
                flex: 1
            }, {
                header: '參數',
                dataIndex: 'RUN_URL_PARAMETER',
                align: 'center',
                flex: 1,                
            },
            {
                header: "有效性",
                dataIndex: 'IS_ACTIVE',
                align: 'center',
                flex: 1,
                 renderer: isActiveRenderer
            },{
                header: "上次執行時間",
                dataIndex: 'LAST_RUN_TIME',
                align: 'center',
                flex: 1
            },{
                header: "執行狀態",
                dataIndex: 'LAST_RUN_STATUS',
                align: 'center',
                flex: 1,
                 renderer: isStatusRenderer
            }],
            
            tbarCfg: {
                buttonAlign: 'right'
            },
            bbar: Ext.create('Ext.toolbar.Paging', {
                store: storeSchedule,
                displayInfo: true,
                displayMsg: '第{0}~{1}資料/共{2}筆',
                emptyMsg: "無資料顯示"
            }),
            tbar: [
            {
                text: '<img src="' + SYSTEM_URL_ROOT + '/css/custimages/add.gif" style="height:12px;vertical-align:middle;margin-top:-2px;margin-right:5px;">新增排程工作',
                handler: function () {
                    if (myForm == undefined) {
                        myForm = Ext.create('ScheduleForm', {});
                        myForm.on('closeEvent', function (obj) {
                            storeSchedule.load();
                        });
                    }
                    myForm.setTitle('排程工作【新增】');
                    myForm.uuid = undefined;
                    myForm.show();
                }
            }            
            ]
        }]
    });
    ScheduleQuery.render('divMain');
});
</script>			
<div id="divMain" style="margin-bottom:5px;margin-top:35px;"></div>
<!-- 使用者session的檢查操作，當逾期時自動轉頁至登入頁面 -->
<script type="text/javascript" src='<%= Page.ResolveUrl("~/pageJs/keeySession.js")%>'></script>           
</asp:Content>