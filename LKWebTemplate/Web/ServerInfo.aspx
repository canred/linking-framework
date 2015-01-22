<%@ Page Title="" Language="C#" MasterPageFile="~/mpStand.Master" AutoEventWireup="true" CodeBehind="ServerInfo.aspx.cs" Inherits="LKWebTemplate.ServerInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript" src='<%= Page.ResolveUrl("~/js/shared/include-ext.js")%>'></script>
<script type="text/javascript" src='<%= Page.ResolveUrl("~/Proxy.ashx")%>'></script>
<style>
.diskTip{
   font-style:'30px'
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div id="divRegist"></div>
<script language="javascript" type="text/javascript">
Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION + ".ServerAction"));
Ext.tip.QuickTipManager.init();
var isLoadDisk = false;
function loadDiskInfo() {
    WS.ServerAction.loadDisk(function (obj, jsonObj) {
        if (jsonObj.result.success) {
            var tmpArray = new Array();            
            for (var item in jsonObj.result.data.Item) {
                if (jsonObj.result.data.Item[item]["DEVICEID"] == undefined) {
                    continue;
                }                                
                tmpArray.push(jsonObj.result.data.Item[item]["DEVICEID"]);
                Ext.create('Ext.data.JsonStore', {
                    fields: ['name', 'size'],
                    storeId: jsonObj.result.data.Item[item]["DEVICEID"]
                });
                var tmpStore = Ext.data.StoreManager.lookup(jsonObj.result.data.Item[item]["DEVICEID"]);
                var tmpData = [];
                var use = jsonObj.result.data.Item[item]["SIZE"] - jsonObj.result.data.Item[item]["FREESPACE"];
                tmpData.push({
                    name: "Use",
                    size: use
                });
                tmpData.push({
                    name: "Free",
                    size: jsonObj.result.data.Item[item]["FREESPACE"]
                });
                tmpStore.loadData(tmpData);
                var chart = Ext.create('Ext.chart.Chart', {
                    xtype: 'chart',
                    width: 400,
                    height: 200,
                    animate: true,
                    store: tmpStore,
                    shadow: true,
                    legend: {
                        position: 'right'
                    },
                    theme: 'Base:gradients',
                    items: [{
                        type: 'text',
                        font: '50px Arial',
                        x: 150,
                        y: 100,
                        text: jsonObj.result.data.Item[item]["DEVICEID"]
                    }],
                    series: [{
                        type: 'pie',
                        field: 'size',
                        showInLegend: true,
                        donut: 40,
                        tips: {
                            trackMouse: true,
                            length: 10,
                            width: 200,
                            shim: false,
                            baseCls: 'diskTip',
                            columnWidth: 200,
                            renderer: function (storeItem, item) {
                                var name = storeItem.get('name');
                                var size = storeItem.get('size');
                                this.setTitle((size / 1024 / 1024 / 1024).toString().substring(0, 5) + "Gb");
                            }
                        },
                        highlight: {
                            segment: {
                                margin: 20
                            }
                        },
                        label: {
                            field: 'name',
                            display: 'rotate',
                            contrast: true,
                            font: '18px Arial'
                        }
                    }]
                });
                Ext.getCmp('panelDisk').add(chart);
            }
        }
    });
}
var storeCpu;
Ext.onReady(function () {
    var objName = {
        run: function () {
            WS.ServerAction.loadRuntimeCpuMem(function (obj, jsonObj) {
                if (isLoadDisk == false) {
                    loadDiskInfo();
                    isLoadDisk = true;
                }
                if(jsonObj.result.success){
                    if (jsonObj.result.data.TotalResults == 2) {
                        storeCpu.fields = [];
                        var data = [];
                        for (var col in jsonObj.result.data.Item[0]) {
                            storeCpu.fields.push(col);
                            if (col != 'TYPE') {
                                data.push({
                                    name: col,
                                    cpu: parseFloat(jsonObj.result.data.Item[0][col]),
                                    men: parseFloat(jsonObj.result.data.Item[1][col])
                                });
                            }
                        }
                        storeCpu.loadData(data);
                    }
                }
            });
        },
        interval: 3000
    };
    Ext.TaskManager.start(objName);
    storeCpu = Ext.create('Ext.data.JsonStore', {
        fields: ['name', 'cpu', 'men']
    });
    var chart = Ext.create('Ext.chart.Chart', {
        style: 'background:#fff',
        animate: false,
        store: storeCpu,
        shadow: true,
        width: $(this).width() * .8,
        height: 200,
        hidden: false,
        theme: 'Red',
        border: false,
        axes: [{
            type: 'Numeric',
            minimum: 0,
            maximum: 100,
            position: 'left',
            fields: ['cpu'],
            title: 'CPU使用率%',
            minorTickSteps: 1,
            grid: {
                odd: {
                    opacity: 1,
                    fill: '#ddd',
                    stroke: '#bbb',
                    'stroke-width': 0.5
                }
            }
        }, {
            type: 'Category',
            position: 'bottom',
            fields: ['name'],
            title: '時間m:s'
        }],
        series: [{
                type: 'line',
                highlight: {
                    size: 7,
                    radius: 7
                },
                axis: 'left',
                xField: 'name',
                yField: 'cpu',
                markerConfig: {
                    type: 'cross',
                    size: 4,
                    radius: 2,
                    'stroke-width': 0
                }
            }
        ]
    });
    var chartMen = Ext.create('Ext.chart.Chart', {
        style: 'background:#fff',
        animate: false,
        store: storeCpu,
        shadow: true,
        width: $(this).width() * .8,
        height: 200,
        hidden: false,
        theme: 'Blue',
        border: false,
        axes: [{
            type: 'Numeric',
            minimum: 0,
            position: 'left',
            fields: ['men'],
            title: 'MB',
            minorTickSteps: 1,
            grid: {
                odd: {
                    opacity: 1,
                    fill: '#ddd',
                    stroke: '#bbb',
                    'stroke-width': 0.5
                }
            }
        }, {
            type: 'Category',
            position: 'bottom',
            fields: ['name'],
            title: '時間m:s'
        }],
        series: [{
                type: 'line',
                highlight: {
                    size: 7,
                    radius: 7
                },
                axis: 'left',
                xField: 'name',
                yField: 'men',
                markerConfig: {
                    type: 'cross',
                    size: 4,
                    radius: 2,
                    'stroke-width': 0
                }
            }
        ]
    });
    Ext.create('Ext.form.Panel', {
        layout: {
            type: 'form',
            align: 'stretch'
        },
        title: '服務器資訊',
        width: $("#divRegist").width() - 16,
        autoHeight: true,
        border: true,
        bodyPadding: 5,
        defaultType: 'textfield',
        buttonAlign: 'center',
        renderTo: 'divRegist',
        listeners: {
            'afterrender': function (obj, eOpts) {
                var thisObj = obj;
                WS.ServerAction.loadCpuInfo(function (obj, jsonObj, a, b) {
                    if (jsonObj.result.success) {
                        Ext.getCmp('cpu.caption').setValue(jsonObj.result.data.Item[0].CAPTION);
                        Ext.getCmp('cpu.name').setValue(jsonObj.result.data.Item[0].NAME);
                        Ext.getCmp('cpu.numberoflobicalprocessors').setValue(jsonObj.result.data.Item[0].NUMBEROFLOGICALPROCESSORS);
                        Ext.getCmp('cpu.systemname').setValue(jsonObj.result.data.Item[0].SYSTEMNAME);
                    }
                });
                WS.ServerAction.loadOSInfo(function (obj, jsonObj, a, b) {
                    if (jsonObj.result.success) {
                        Ext.getCmp('os.caption').setValue(jsonObj.result.data.Item[0].CAPTION);
                        Ext.getCmp('os.name').setValue(jsonObj.result.data.Item[0].NAME);
                        Ext.getCmp('os.osarchitecture').setValue(jsonObj.result.data.Item[0].OSARCHITECTURE);
                    }
                });
            }
        },
        items: [{
            xtype: 'fieldset',
            border: true,
            title: 'CPU資訊',
            items: [{
                xtype: 'container',
                layout: 'hbox',
                padding: 5,
                items: [
                    {
                        xtype: 'textfield',
                        fieldLabel: 'Caption',
                        id: 'cpu.caption',
                        allowBlank: false,
                        readOnly: true,
                        labelAlign: 'right',
                        flex: 1
                    }, {
                        xtype: 'textfield',
                        fieldLabel: 'Name',
                        id: 'cpu.name',
                        labelAlign: 'right',
                        allowBlank: false,
                        readOnly: true,
                        flex: 1
                    }
                ]
            }, {
                xtype: 'container',
                layout: 'hbox',
                padding: 5,
                items: [{
                    xtype: 'textfield',
                    fieldLabel: 'CPU數量',
                    labelAlign: 'right',
                    id: 'cpu.numberoflobicalprocessors',
                    allowBlank: false,
                    readOnly: true,
                    flex: 1
                }, {
                    xtype: 'textfield',
                    fieldLabel: 'System Name',
                    labelAlign: 'right',
                    id: 'cpu.systemname',
                    allowBlank: false,
                    readOnly: true,
                    flex: 1
                }]
            }]
        }, {
            xtype: 'fieldset',
            border: true,
            title: "OS",
            items: [{
                xtype: 'container',
                padding: 5,
                layout: 'hbox',
                items: [{
                    xtype: 'textfield',
                    fieldLabel: 'OS Caption',
                    id: 'os.caption',
                    allowBlank: false,
                    readOnly: true,
                    labelAlign: 'right',
                    flex: 1
                }, {
                    xtype: 'textfield',
                    fieldLabel: 'OS Name',
                    id: 'os.name',
                    labelAlign: 'right',
                    allowBlank: false,
                    readOnly: true,
                    flex: 1
                }]
            }, {
                xtype: 'container',
                layout: 'hbox',
                padding: 5,
                items: [{
                    xtype: 'textfield',
                    fieldLabel: 'OSArchitechture',
                    id: 'os.osarchitecture',
                    labelAlign: 'right',
                    allowBlank: false,
                    readOnly: true,
                    flex: 1
                }]
            }]
        }, {
            xtype: 'panel',
            layout: 'fit',
            padding: 5,
            title: 'CPU使用率(現況)',
            items: chart
        }, {
            xtype: 'panel',
            layout: 'fit',
            padding: 5,
            title: 'Free記憶體(現況)',
            items: chartMen
        }, {
            xtype: 'panel',
            title: '磁碟資訊',
            height: 600,
            itemId: 'panelDisk',
            id: "panelDisk",
            items: []
        }]
    });
});
</script>
<!-- 使用者session的檢查操作，當逾期時自動轉頁至登入頁面 -->
<script type="text/javascript" src='<%= Page.ResolveUrl("~/pageJs/keeySession.js")%>'></script>  
</asp:Content>
