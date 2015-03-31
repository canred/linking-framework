var SERVERINFOPANEL;
Ext.Loader.setPath('WS', SYSTEM_URL_ROOT + '/pagejs');
Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION + ".ServerAction"));

Ext.define('WS.ServerInfoPanel', {
    extend: 'Ext.panel.Panel',
    closeAction: 'destroy',
    myStore: {
        cpu: Ext.create('Ext.data.JsonStore', {
            fields: ['name', 'cpu', 'men']
        })
    },
    myTask: {},
    fnLoadDiskInfo: function(obj) {
        WS.ServerAction.loadDisk(function(obj, jsonObj) {
            if (jsonObj.result.success) {
                var tmpArray = new Array();
                var arrChartData = [];
                for (var item in jsonObj.result.data.Item) {
                    var raw = jsonObj.result.data.Item[item];
                    raw.SIZE = Math.round(raw.SIZE / (1024 * 1024 * 1024));
                    raw.FREESPACE = Math.round(raw.FREESPACE / (1024 * 1024 * 1024));
                    var w = this.down('#pnlDisk').getWidth();
                    var myItemId = 'pnlDisk' + item;
                    this.down('#pnlDisk').add({
                        xtype: 'panel',
                        title: raw.DEVICEID,
                        height: 400,
                        width: w / 2,
                        itemId: myItemId
                    });
                    arrChartData.push({
                        domId: this.down('#pnlDisk' + item).id,
                        data: raw
                    });
                };
                //echart start
                require(
                    [
                        'echarts',
                        'echarts/chart/pie',
                        'echarts/chart/funnel'
                    ],
                    function(ec) {
                        for (var i = 0; i < arrChartData.length; i++) {
                            var chart = ec.init(document.getElementById(arrChartData[i].domId));
                            var raw = arrChartData[i].data;
                            var chartOption = {
                                title: {
                                    text: raw.DEVICEID,
                                    x: 'center',
                                    y: 50
                                },
                                tooltip: {
                                    trigger: 'item',
                                    formatter: "{a} <br/>{b} : {c}G ({d}%)"
                                },
                                legend: {
                                    orient: 'vertical',
                                    x: 'left',
                                    data: ['已使用', '未使用']
                                },
                                toolbox: {
                                    show: true,
                                    feature: {
                                        magicType: {
                                            show: true,
                                            type: ['pie', 'funnel'],
                                            option: {
                                                funnel: {
                                                    x: '25%',
                                                    width: '50%',
                                                    funnelAlign: 'left'
                                                }
                                            }
                                        },
                                        restore: {
                                            show: true
                                        },
                                        saveAsImage: {
                                            show: true
                                        }
                                    }
                                },
                                calculable: true,
                                series: [{
                                    name: '磁碟資訊',
                                    type: 'pie',
                                    radius: '55%',
                                    center: ['50%', '60%'],
                                    data: [{
                                        value: raw.SIZE - raw.FREESPACE,
                                        name: '已使用'
                                    }, {
                                        value: raw.FREESPACE,
                                        name: '未使用'
                                    }]
                                }]
                            };
                            chart.setOption(chartOption);
                        };
                    });
                //echart end
            }
        }, obj);
    },
    initComponent: function() {
        this.myTask.loadDisk = {
            run: function(obj, a, b) {
                WS.ServerAction.loadRuntimeCpuMem(function(obj, jsonObj) {
                    if (this.myTask.loadDisk.isLoadDisk == false) {
                        this.fnLoadDiskInfo(this);
                        this.myTask.loadDisk.isLoadDisk = true;
                    };
                    if (jsonObj.result.success) {
                        if (jsonObj.result.data.TotalResults == 2) {
                            this.myStore.cpu.fields = [];
                            var data = [];
                            for (var col in jsonObj.result.data.Item[0]) {
                                this.myStore.cpu.fields.push(col);
                                if (col != 'TYPE') {
                                    data.push({
                                        name: col,
                                        cpu: parseFloat(jsonObj.result.data.Item[0][col]),
                                        men: parseFloat(jsonObj.result.data.Item[1][col])
                                    });
                                }
                            };
                            this.myStore.cpu.loadData(data);
                            var me = this;
                            //echart start
                            require(
                                [
                                    'echarts',
                                    'echarts/chart/line',
                                    'echarts/chart/bar'
                                ],
                                function(ec) {
                                    var arrXAxis = [];
                                    var arrXAxisCpuValue = [];
                                    var arrXAxisMenValue = [];
                                    Ext.each(me.myStore.cpu.data.items, function(item) {
                                        arrXAxis.push(item.data.name);
                                        arrXAxisCpuValue.push(item.data.cpu);
                                        arrXAxisMenValue.push(item.data.men);
                                    });
                                    var myCpuChart = ec.init(document.getElementById(me.down('#pnlCpu').id));
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
                                    myCpuChart.setOption(optionCpu);
                                    var myMemChart = ec.init(document.getElementById(me.down('#pnlMem').id));
                                    var optionMem = {
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
                                            name: 'MEN',
                                            type: 'line',
                                            data: arrXAxisMenValue,
                                            itemStyle: {
                                                normal: {
                                                    color: 'green'
                                                },
                                                emphasis: {
                                                    color: 'red'
                                                }
                                            },
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
                                    myMemChart.setOption(optionMem);
                                }
                            );
                            //echart end
                        }
                    }
                }, this);
            },
            interval: 3000,
            scope: this
        };
        this.myTask.loadDisk.isLoadDisk = false;
        Ext.TaskManager.start(this.myTask.loadDisk, this);
        this.items = [{
            xtype: 'fieldset',
            border: true,
            title: 'CPU資訊',
            items: [{
                xtype: 'container',
                layout: 'hbox',
                items: [{
                    xtype: 'textfield',
                    fieldLabel: 'Caption',
                    itemId: 'txtCpuCaption',
                    readOnly: true,
                    labelAlign: 'right',
                    flex: 1
                }, {
                    xtype: 'textfield',
                    fieldLabel: 'Name',
                    itemId: 'txtCpuName',
                    labelAlign: 'right',
                    readOnly: true,
                    flex: 1
                }]
            }, {
                xtype: 'container',
                layout: 'hbox',
                margin: '5 0 5 0',
                items: [{
                    xtype: 'textfield',
                    fieldLabel: 'CPU數量',
                    labelAlign: 'right',
                    itemId: 'txtCpuNumberOfLobicalProcessors',
                    readOnly: true,
                    flex: 1
                }, {
                    xtype: 'textfield',
                    fieldLabel: 'System Name',
                    labelAlign: 'right',
                    itemId: 'txtCpuSystemname',
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
                layout: 'hbox',
                items: [{
                    xtype: 'textfield',
                    fieldLabel: 'OS Caption',
                    itemId: 'txtOsCaption',
                    readOnly: true,
                    labelAlign: 'right',
                    flex: 1
                }, {
                    xtype: 'textfield',
                    fieldLabel: 'OS Name',
                    itemId: 'txtOsName',
                    labelAlign: 'right',
                    readOnly: true,
                    flex: 1
                }]
            }, {
                xtype: 'container',
                layout: 'hbox',
                margin: '5 0 5 0',
                items: [{
                    xtype: 'textfield',
                    fieldLabel: 'OSArchitechture',
                    itemId: 'txtOsOsarchitecture',
                    labelAlign: 'right',
                    readOnly: true,
                    flex: 1
                }]
            }]
        }, {
            xtype: 'panel',
            layout: 'fit',
            itemId: 'pnlCpu',
            title: 'CPU使用率(現況)',
            height: 300
        }, {
            xtype: 'panel',
            layout: 'fit',
            itemId: 'pnlMem',
            title: 'Free記憶體(現況)',
            height: 300
        }, {
            xtype: 'panel',
            title: '磁碟資訊',
            layout: 'table',
            columns: 2,
            itemId: 'pnlDisk',
            height: 800
        }];
        this.callParent(arguments);
    },
    listeners: {
        'afterrender': function(obj, eOpts) {
            WS.ServerAction.loadCpuInfo(function(obj, jsonObj, a, b) {
                if (jsonObj.result.success) {
                    this.down('#txtCpuCaption').setValue(jsonObj.result.data.Item[0].CAPTION);
                    this.down('#txtCpuName').setValue(jsonObj.result.data.Item[0].NAME);
                    this.down('#txtCpuNumberOfLobicalProcessors').setValue(jsonObj.result.data.Item[0].NUMBEROFLOGICALPROCESSORS);
                    this.down('#txtCpuSystemname').setValue(jsonObj.result.data.Item[0].SYSTEMNAME);
                }
            }, this);
            WS.ServerAction.loadOSInfo(function(obj, jsonObj, a, b) {
                if (jsonObj.result.success) {
                    this.down('#txtOsCaption').setValue(jsonObj.result.data.Item[0].CAPTION);
                    this.down('#txtOsName').setValue(jsonObj.result.data.Item[0].NAME);
                    this.down('#txtOsOsarchitecture').setValue(jsonObj.result.data.Item[0].OSARCHITECTURE);
                }
            }, this);
        }
    }
});
