<%@ Page Title="" Language="C#" MasterPageFile="~/mpStand.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="LKWebTemplate.Default" EnableViewState="false"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="Div1" style="float:left;margin-bottom:5px;margin-top:5px;">    
    <div id="systemInfo" style="margin-bottom:5px;margin-top:5px;"></div>
</div>
<script type="text/javascript">
    Ext.onReady(function () {
        WS_LOGONPANEL = Ext.create('WS.LogonPanel', {
            val: {
                company: '<%= getCompany() %>',
            account: '<%= getAccount() %>',
            password: '<%= getPassword() %>'
        }
    });
    WS_LOGONPANEL.urlSuccess = '<%= Page.ResolveUrl(LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().DefaultPage)%>';
    WS_LOGONPANEL.urlFail = '<%= Page.ResolveUrl(LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().NoPermissionPage)%>';
    WS_LOGONPANEL.down('#ExtLogonForm').title = '<img src="' + SYSTEM_ROOT_PATH + '/css/custimages/login.gif" style="height:16px;margin-bottom:4px;margin-right:10px;" align="middle"><%= LKWebTemplate.Parameter.Config.ParemterConfigs.GetConfig().SystemName%>';
    WS_LOGONPANEL.render('logon');
    });
</script>
<!--Echart Demo-->
<script language="javascript" type="text/javascript"> 
require(
    [
        'echarts',
        'echarts/chart/line', // 按需加载所需图表，如需动态类型切换功能，别忘了同时加载相应图表
        'echarts/chart/bar'
    ],
    function(ec) {
        var myChart = ec.init(document.getElementById('echart'));
        option = {
            title: {
                text: '未来一周气温变化',
                subtext: '纯属虚构'
            },
            tooltip: {
                trigger: 'axis'
            },
            legend: {
                data: ['最高气温', '最低气温']
            },
            toolbox: {
                show: true,
                feature: {
                    mark: {
                        show: true
                    },
                    dataView: {
                        show: true,
                        readOnly: false
                    },
                    magicType: {
                        show: true,
                        type: ['line', 'bar']
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
            xAxis: [{
                type: 'category',
                boundaryGap: false,
                data: ['周一', '周二', '周三', '周四', '周五', '周六', '周日']
            }],
            yAxis: [{
                type: 'value',
                axisLabel: {
                    formatter: '{value} °C'
                }
            }],
            series: [{
                name: '最高气温',
                type: 'line',
                data: [11, 11, 15, 13, 12, 13, 10],
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
            }, {
                name: '最低气温',
                type: 'line',
                data: [1, -2, 2, 5, 3, 2, 0],
                markPoint: {
                    data: [{
                        name: '周最低',
                        value: -2,
                        xAxis: 1,
                        yAxis: -1.5
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
        myChart.setOption(option);
    }
);
</script>
<table width="100%">
    <tr>
        <td width="30%"></td>
        <td width="40%" >
        <div id="logon" style="margin-bottom:5px;margin-top:5px;width:2"></div>
        </td>
        <td width="30%"></td>
    </tr>
</table>   
<center>
<div id='echart' style="height:300px;width:800px;"></div>
</center>
</asp:Content>
