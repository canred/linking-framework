<%@ Page Title="" Language="C#" MasterPageFile="~/mpStand.Master" AutoEventWireup="true" CodeBehind="dept.aspx.cs" Inherits="Web.admin.basic.dept" EnableViewState="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript" src='<%= Page.ResolveUrl("~/js/shared/include-ext.js")%>'></script>
<script type="text/javascript" src='<%= Page.ResolveUrl("~/Proxy.ashx")%>'></script>
<script type="text/javascript" src='<%= Page.ResolveUrl("~/pageJs/Ext.DepartmentForm.js")%>'></script>
<script type="text/javascript" src='<%= Page.ResolveUrl("~/pageJs/Ext.AttendantPicker.js")%>'></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<script language="javascript" type="text/javascript">
				 /*:::加入Direct:::*/
				Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION + ".DeptAction"));
				var MOperationalBoundaryQuery = undefined;
				var myForm = undefined;
				Ext.require(['Ext.direct.*', 'Ext.data.*', 'Ext.tree.*']);

				var storeTree;

				function openDept(uuid, parendUuid, isOB) {
				    if (myForm == undefined) {
				        myForm = Ext.create('DepartmentForm', {});
				        myForm.on('closeEvent', function (obj) {
				            location.href = location.href;
				        });
				    }
				    if (uuid == "undefined")
				        myForm.uuid = undefined;
				    else
				        myForm.uuid = uuid;
				    myForm.setTitle('部門【編輯】');
				    myForm.parentUuid = parendUuid;
				    myForm.is_operational_boundary = isOB;
				    myForm.companyUuid = '<%= getUser().COMPANY_UUID %>';
				    myForm.createUuid = '<%= getUser().UUID %>';
				    myForm.show();
				}


				var DeptQueryTaskFlag = false;
				var OrganizationQueryTask = {
				    run: function () {
				        if (DeptQueryTaskFlag == true) {
				            Ext.getCmp('org').expandAll();
				            DeptQueryTaskFlag = false;
				        }
				    },
				    interval: 1000
				}
				Ext.TaskManager.start(OrganizationQueryTask);

				Ext.onReady(function () {
				    Ext.QuickTips.init();
				    /*設定Store物件*/
				    Ext.define('DEPARTMENT', {
				        extend: 'Ext.data.Model',
				        fields: [
				            'UUID', 'CREATE_DATE', 'UPDATE_DATE', 'IS_ACTIVE',
				            'COMPANY_UUID', 'ID', 'C_NAME',
				            'E_NAME', 'PARENT_DEPARTMENT_UUID',
				            'MANAGER_UUID', 'PARENT_DEPARTMENT_ID', 'MANAGER_ID', 'PARENT_DEPARTMENT_UUID_LIST',
				            'S_NAME', 'COST_CENTER', 'SRC_UUID', 'FULL_DEPARTMENT_NAME'
				        ]
				    });


				    var rootUuid;
				    WS.DeptAction.loadTreeRoot("<%= getUser().COMPANY_UUID %>", function (data) {
				        rootUuid = data.UUID;				        
				        storeTree.load({
				            params: {
				                UUID: rootUuid
				            }
				        });
				    });

				    storeTree = Ext.create('Ext.data.TreeStore', {
				        model: 'DEPARTMENT',
				        root: {
				            expanded: true
				        },
				        nodeParam: 'id',
				        proxy: {
				            paramOrder: ['UUID'],
				            type: 'direct',
				            directFn: WS.DeptAction.loadTree,
				            extraParams: {
				                "UUID": ''
				            }
				        }
				    });



				    /*設定元件*/
				    MethodQuery = Ext.widget({
				        xtype: 'panel',
				        id: 'MethodQuery',
				        title: '部門維護',
				        frame: true,
				        padding: 5,
				        autoHeight: true,
				        autoWidth: true,
				        items: [{
				            layout: 'column',
				            padding: 5,
				            border: 0,
				            items: [{
				                style: 'display:block; padding:4px 0px 0px 0px',
				                xtype: 'label',
				                text: '公司名稱(商號)：' + '<%= getUser().COMPANY_C_NAME %>'
				            }, {
				                xtype: 'label',
				                readOnly: true,
				                queryMode: 'local',
				                displayField: 'BASIC_COMPANY_UUID',
				                valueField: 'UUID',
				                id: 'lab_TreeRoot',
				                store: storeTree,
				                editable: false,
				                style: 'display:block; padding:4px 0px 0px 0px'
				            }]
				        }, {
				            layout: 'column',
				            padding: 5,
				            border: 0,
				            items: [


				                {
				                    xtype: 'button',
				                    text: "<img src='../../css/custimages/add.gif' style='height:20px;vertical-align:middle'>&nbsp;" + '新增部門',
				                    handler: function () {
				                        if (myForm == undefined) {
				                            myForm = Ext.create('DepartmentForm', {});
				                            myForm.on('closeEvent', function (obj) {
				                                location.href = location.href;
				                            });
				                        }
				                        myForm.setTitle('部門【新增】');
				                        myForm.uuid = undefined;
				                        myForm.parentUuid = rootUuid;
				                        myForm.is_operational_boundary = "N";
				                        myForm.companyUuid = '<%= getUser().COMPANY_UUID %>';
				                        myForm.createUuid = '<%= getUser().UUID %>';
				                        myForm.show();
				                    }
				                }
				            ]
				        }, {
				            xtype: 'treepanel',
				            fieldLabel: '設定組織邊界',
				            name: 'org',
				            id: 'org',
				            padding: 5,
				            autoWidth: true,
				            autoHeight: true,
				            minHeight: 400,
				            store: storeTree,
				            rootVisible: false,
				            columns: [{
				                xtype: 'treecolumn',
				                text: '部門名稱',
				                flex: 2,
				                sortable: true,
				                dataIndex: 'C_NAME'
				            }, {
				                text: "功能",
				                dataIndex: 'UUID',
				                align: 'center',
				                flex: 1.5,
				                renderer: function (value, m, r) {
				                    var id = Ext.id();
				                    var is_ob = r.getData().UUID;
				                    var dom;
				                    dom = "<input type='button' class='pure-button pure-button-primary' onclick='openDept(\"" + value + "\",\"\",\"\");' value='編輯'/>  <input type='button'  class='pure-button pure-button-primary'   onclick='openDept(\"undefined\",\"" + value + "\",\"Y\");' value='新增子部門'/>";
				                    return dom;
				                },
				                sortable: false,
				                hideable: false
				            }],
				            listeners: {
				                beforeload: function (tree, node) {
				                    var myMask = new Ext.LoadMask(
				                        Ext.getCmp('MethodQuery'), {
				                            msg: "資料載入中，請稍等...",
				                            store: storeTree,
				                            removeMask: true
				                        });
				                    myMask.show();
				                    if (node.node.isLeaf() == false) {
				                        storeTree.getProxy().setExtraParam('UUID', node.node.raw['UUID']);
				                    }
				                }
				            }
				        }],
				        listeners: {
				            afterrender: function () {
				                Ext.getCmp('org').expandAll();
				            }
				        }
				    });
				    MethodQuery.render('divMain');
				});
</script>
			
			<div id="divMain" style="margin-bottom:5px;margin-top:35px;"></div>
			<!-- 使用者session的檢查操作，當逾期時自動轉頁至登入頁面 -->
			<script type="text/javascript" src='<%= Page.ResolveUrl("~/pageJs/keeySession.js")%>'></script>         
</asp:Content>


