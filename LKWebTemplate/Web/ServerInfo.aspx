<%@ Page Title="" Language="C#" MasterPageFile="~/mpStand.Master" AutoEventWireup="true" CodeBehind="ServerInfo.aspx.cs" Inherits="LKWebTemplate.ServerInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style>
.diskTip{
   font-style:'30px'
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div id="divRegist"></div>
<script language="javascript" type="text/javascript">
var panel;
Ext.Loader.setPath('WS', SYSTEM_URL_ROOT + '/pagejs');


Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION + ".ServerAction"));
var isLoadDisk = false;

function loadDiskInfo() {
    WS.ServerAction.loadDisk(function(obj, jsonObj) {
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

                require(
                    [
                        'echarts',                        
                        'echarts/chart/pie',
                    ],
                    function(ec) {
                        console.log(tmpStore);
                        var labelTop = {
                            normal: {
                                label: {
                                    show: true,
                                    position: 'center',
                                    formatter: '{b}',
                                    textStyle: {
                                        baseline: 'bottom'
                                    }
                                },
                                labelLine: {
                                    show: false
                                }
                            }
                        };
                        var labelFromatter = {
                            normal: {
                                label: {
                                    formatter: function(params) {
                                        return 100 - params.value + '%'
                                    },
                                    textStyle: {
                                        baseline: 'top'
                                    }
                                }
                            },
                        }
                        var labelBottom = {
                            normal: {
                                color: '#ccc',
                                label: {
                                    show: true,
                                    position: 'center'
                                },
                                labelLine: {
                                    show: false
                                }
                            },
                            emphasis: {
                                color: 'rgba(0,0,0,0)'
                            }
                        };

                        var radius = [30, 55];
                        var optionDisk = {
                            legend: {
                                x: 'center',
                                y: 'center',
                                data: [
                                    'C', 'D'
                                ]
                            },
                            title: {
                                text: '磁碟資訊',
                                x: 'center'
                            },
                            toolbox: {
                                show: true,
                                feature: {
                                    saveAsImage: {
                                        show: true
                                    }
                                }
                            },
                            series: [{
                                type: 'pie',
                                center: ['10%', '30%'],
                                radius: radius,
                                x: '0%', // for funnel
                                itemStyle: labelFromatter,
                                data: [{
                                    name: 'free',
                                    value: 46,
                                    itemStyle: labelBottom
                                }, {
                                    name: 'C',
                                    value: 54,
                                    itemStyle: labelTop
                                }]
                            }, {
                                type: 'pie',
                                center: ['30%', '30%'],
                                radius: radius,
                                x: '50%', // for funnel
                                itemStyle: labelFromatter,
                                data: [{
                                    name: 'free',
                                    value: 56,
                                    itemStyle: labelBottom
                                }, {
                                    name: 'D',
                                    value: 44,
                                    itemStyle: labelTop
                                }]
                            }]
                        };
                        var myChart = ec.init(document.getElementById(panel.down('#pnlDisk').id));
                        myChart.setOption(optionDisk);
                    });
            }
        }
    });
}
var storeCpu;
Ext.onReady(function() {
    var objName = {
        run: function() {
            WS.ServerAction.loadRuntimeCpuMem(function(obj, jsonObj) {
                if (isLoadDisk == false) {
                    loadDiskInfo();
                    isLoadDisk = true;
                }
                if (jsonObj.result.success) {
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

                        //echart start

                        require(
                            [
                                'echarts',
                                'echarts/chart/line', // 按需加载所需图表，如需动态类型切换功能，别忘了同时加载相应图表
                                'echarts/chart/bar'
                            ],
                            function(ec) {
                                
                                var arrXAxis = [];
                                var arrXAxisCpuValue = [];
                                var arrXAxisMenValue = [];



                                Ext.each(storeCpu.data.items, function(item) {
                                    arrXAxis.push(item.data.name);
                                    arrXAxisCpuValue.push(item.data.cpu);
                                    arrXAxisMenValue.push(item.data.men);

                                });

                                var myChart = ec.init(document.getElementById(panel.down('#pnlCpu').id));
                                var optionCpu = {
                                    animation: false,
                                    title: {
                                        text: 'CPU使用率'
                                    },
                                    tooltip: {
                                        trigger: 'axis'
                                    },
                                    legend: {
                                        data: ['CPU']
                                    },
                                    toolbox: {
                                        show: true,
                                        feature: {
                                            saveAsImage: {
                                                show: true,
                                                title: '下載圖片',
                                            }
                                        }
                                    },
                                    calculable: true,
                                    xAxis: [{
                                        type: 'category',
                                        boundaryGap: false,
                                        data: arrXAxis
                                    }],
                                    yAxis: [{
                                        type: 'value',
                                        axisLabel: {
                                            formatter: '{value}'
                                        }
                                    }],
                                    series: [{
                                        name: 'CPU',
                                        type: 'line',
                                        data: arrXAxisCpuValue,
                                        markPoint: {
                                            data: [{
                                                type: 'max',
                                                name: '最大值'
                                            }, {
                                                type: 'min',
                                                name: '最小值'
                                            }]
                                        },
                                        markLine: {
                                            data: [{
                                                type: 'average',
                                                name: '平均值'
                                            }]
                                        }
                                    }]
                                };
                                myChart.setOption(optionCpu);


                                var myChart = ec.init(document.getElementById(panel.down('#pnlMen').id));
                                var optionMen = {
                                    animation: false,
                                    title: {
                                        text: '記憶體使用量'
                                    },
                                    tooltip: {
                                        trigger: 'axis'
                                    },
                                    legend: {
                                        data: ['MEN']
                                    },
                                    toolbox: {
                                        show: true,
                                        feature: {
                                            saveAsImage: {
                                                show: true,
                                                title: '下載圖片',
                                            }
                                        }
                                    },
                                    calculable: true,
                                    xAxis: [{
                                        type: 'category',
                                        boundaryGap: false,
                                        data: arrXAxis
                                    }],
                                    yAxis: [{
                                        type: 'value',
                                        axisLabel: {
                                            formatter: '{value}'
                                        }
                                    }],
                                    series: [{
                                        name: 'CPU',
                                        type: 'line',
                                        data: arrXAxisMenValue,
                                        markPoint: {
                                            data: [{
                                                type: 'max',
                                                name: '最大值'
                                            }, {
                                                type: 'min',
                                                name: '最小值'
                                            }]
                                        },
                                        markLine: {
                                            data: [{
                                                type: 'average',
                                                name: '平均值'
                                            }]
                                        }
                                    }]
                                };
                                myChart.setOption(optionMen);
                            }
                        );
                        //echart end
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
    panel = Ext.create('Ext.form.Panel', {
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
            'afterrender': function(obj, eOpts) {
                var thisObj = obj;
                WS.ServerAction.loadCpuInfo(function(obj, jsonObj, a, b) {
                    if (jsonObj.result.success) {
                        Ext.getCmp('cpu.caption').setValue(jsonObj.result.data.Item[0].CAPTION);
                        Ext.getCmp('cpu.name').setValue(jsonObj.result.data.Item[0].NAME);
                        Ext.getCmp('cpu.numberoflobicalprocessors').setValue(jsonObj.result.data.Item[0].NUMBEROFLOGICALPROCESSORS);
                        Ext.getCmp('cpu.systemname').setValue(jsonObj.result.data.Item[0].SYSTEMNAME);
                    }
                });

                WS.ServerAction.loadOSInfo(function(obj, jsonObj, a, b) {
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
                items: [{
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
                }]
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
            itemId: 'pnlCpu',
            padding: 5,
            title: 'CPU使用率(現況)',
            height: 300,
            //items: chart
        }, {
            xtype: 'panel',
            layout: 'fit',
            itemId: 'pnlMen',
            padding: 5,
            title: 'Free記憶體(現況)',
            height: 300,
            //items: chartMen
        }, {
            xtype: 'panel',
            title: '磁碟資訊',
            layout: 'fit',
            itemId: 'pnlDisk',
            height: 300
        }]
    });
});

</script>
</asp:Content>
