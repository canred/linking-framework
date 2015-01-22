Ext.Loader.setConfig({
    enabled: true
});
Ext.Loader.setPath('Ext.ux', SYSTEM_ROOT_PATH + '/js/ux');
Ext.require(['*', 'Ext.ux.DataTip']);
Ext.MessageBox.buttonText.yes = "確定";
Ext.MessageBox.buttonText.no = "取消";
Ext.onReady(function() {
    Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION + ".ScheduleAction"));
    Ext.define('ScheduleForm', {
        extend: 'Ext.window.Window',
        title: '人員 新增/修改',
        closeAction: 'hide',
        uuid: undefined,
        id: 'ExtScheduleForm',
        width: $(window).width() * 0.9,
        height: $(window).height() * 0.9,
        maxHeight: 600,
        layout: 'fit',
        resizable: false,
        draggable: true,
        weekChangeInWeek: function(obj) {
            var thisValue = obj.data;
            var org = this.down("#C_DAY_OF_WEEK").getValue();
            var newVar = '';
            if (obj.checked) {
                if (org.indexOf(thisValue) >= 0) {
                    return;
                } else {
                    newVar = this.down("#C_DAY_OF_WEEK").getValue();
                    newVar += "," + thisValue;
                }
            } else {
                newVar = org.replace(thisValue, '');
            }
            newVar = newVar.split(',');
            var thisValue = new Array();
            for (var tmp in newVar) {
                if (typeof(newVar[tmp]) == 'string') {
                    if (newVar[tmp] != '') {
                        thisValue.push(newVar[tmp]);
                    }
                }
                thisValue.sort();
            }
            var resultData = '';
            for (var tmp in thisValue) {
                if (typeof(thisValue[tmp]) == 'string') {
                    resultData += thisValue[tmp] + ',';
                }
            }

            if (resultData.length > 0 && resultData.substr(-1) == ',') {
                resultData = resultData.substr(0, resultData.length - 1);
            }


            this.down("#C_DAY_OF_WEEK").setValue(resultData);
        },
        dayChangeInMonth: function(obj) {
            var thisValue = obj.data;
            var org = this.down("#C_DAY_OF_MONTH").getValue();
            var newVar = '';
            if (obj.checked) {
                if (org.indexOf(thisValue) >= 0) {
                    return;
                } else {
                    newVar = this.down("#C_DAY_OF_MONTH").getValue();
                    newVar += "," + thisValue;
                }
            } else {
                newVar = org.replace(thisValue, '');
            }
            newVar = newVar.split(',');
            var thisValue = new Array();
            for (var tmp in newVar) {
                if (typeof(newVar[tmp]) == 'string') {
                    if (newVar[tmp] != '') {
                        thisValue.push(newVar[tmp]);
                    }
                }
                thisValue.sort();
            }
            var resultData = '';
            for (var tmp in thisValue) {
                if (typeof(thisValue[tmp]) == 'string') {
                    resultData += thisValue[tmp] + ',';
                }
            }
            if (resultData.length > 0 && resultData.substr(-1) == ',') {
                resultData = resultData.substr(0, resultData.length - 1);
            }
            this.down("#C_DAY_OF_MONTH").setValue(resultData);
        },
        weekChangeInMonth: function(obj) {
            var thisValue = obj.data;
            var org = this.down("#C_WEEK_OF_MONTH").getValue();
            var newVar = '';
            if (obj.checked) {
                if (org.indexOf(thisValue) >= 0) {
                    return;
                } else {
                    newVar = this.down("#C_WEEK_OF_MONTH").getValue();
                    newVar += "," + thisValue;
                }
            } else {
                newVar = org.replace(thisValue, '');
            }
            newVar = newVar.split(',');
            var thisValue = new Array();
            for (var tmp in newVar) {
                if (typeof(newVar[tmp]) == 'string') {
                    if (newVar[tmp] != '') {
                        thisValue.push(newVar[tmp]);
                    }
                }
                thisValue.sort();
            }
            var resultData = '';
            for (var tmp in thisValue) {
                if (typeof(thisValue[tmp]) == 'string') {
                    resultData += thisValue[tmp] + ',';
                }
            }

            if (resultData.length > 0 && resultData.substr(-1) == ',') {
                resultData = resultData.substr(0, resultData.length - 1);
            }
            this.down("#C_WEEK_OF_MONTH").setValue(resultData);
        },
        initComponent: function() {
            var me = this;
            me.items = [Ext.create('Ext.form.Panel', {
                layout: {
                    type: 'form',
                    align: 'stretch'
                },
                api: {
                    load: WS.ScheduleAction.infoSchedule,
                    submit: WS.ScheduleAction.submitSchedule
                },
                id: 'Ext.ScheduleForm.Form',
                paramOrder: ['pUuid'],
                border: true,
                bodyPadding: 5,
                autoScroll: true,
                defaultType: 'textfield',
                buttonAlign: 'center',
                items: [{
                        xtype: 'container',
                        layout: 'vbox',
                        items: [{
                            xtype: 'textfield',
                            fieldLabel: '任務名稱',
                            labelAlign: 'right',
                            labelWidth: 100,
                            name: 'SCHEDULE_NAME',
                            padding: 5,
                            anchor: '100%',
                            maxLength: 84,
                            allowBlank: false
                        }, {
                            xtype: 'container',
                            layout: 'hbox',
                            items: [{
                                xtype: 'textfield',
                                flex: 1,
                                name: 'LAST_RUN_TIME',
                                fieldLabel: '最近執行時間',
                                readOnly: true,
                                labelAlign: 'right'
                            }, {
                                xtype: 'textfield',
                                fieldLabel: '執行狀態',
                                name: 'LAST_RUN_STATUS',
                                labelAlign: 'right',
                                readOnly: true,
                                flex: 1
                            }, {
                                xtype: 'combo',
                                fieldLabel: '有效性',
                                labelAlign: 'right',
                                queryMode: 'local',
                                itemId: 'itemId',
                                displayField: 'text',
                                valueField: 'value',
                                name: 'IS_ACTIVE',
                                editable: false,
                                hidden: false,
                                store: new Ext.data.ArrayStore({
                                    fields: ['text', 'value'],
                                    data: [
                                        ['是', 'Y'],
                                        ['否', 'N']
                                    ]
                                }),
                                value: 'Y'
                            }]
                        }, {
                            xtype: 'container',
                            layout: 'hbox',
                            items: [{
                                xtype: 'radiogroup',
                                fieldLabel: '週期性',
                                labelAlign: 'right',
                                width: 200,
                                name: 'IS_CYCLE',
                                itemId: 'test',
                                items: [{
                                    boxLabel: '是',
                                    name: 'IS_CYCLE',
                                    inputValue: 'Y',
                                    itemId: 'IS_CYCLE_Y',
                                    checked: true
                                }, {
                                    boxLabel: '否',
                                    name: 'IS_CYCLE',
                                    inputValue: 'N',
                                    itemId: 'IS_CYCLE_N'
                                }],
                                listeners: {
                                    change: function(obj) {
                                        if (obj.getChecked()[0].itemId != null && obj.getChecked()[0].itemId == "IS_CYCLE_Y") {
                                            /*顯示控制*/
                                            this.up('panel').down('#dtSingleDate').setVisible(false);
                                            this.up('panel').down('#cmbSingleHour').setVisible(false);
                                            this.up('panel').down('#cmbSingleMinute').setVisible(false);
                                            this.up('panel').down('#labSingleHour').setVisible(false);
                                            this.up('panel').down('#labSingleMinute').setVisible(false);
                                            this.up('panel').down('#tpCycle').setVisible(true);
                                            this.up('panel').down('#cmbHour').setVisible(true);
                                            this.up('panel').down('#cmbMinute').setVisible(true);
                                            this.up('panel').down('#labHour').setVisible(true);
                                            this.up('panel').down('#labMinute').setVisible(true);
                                            /*驗証控制*/
                                            this.up('panel').down('#dtSingleDate').allowBlank = true;
                                            this.up('panel').down('#cmbSingleHour').allowBlank = true;
                                            this.up('panel').down('#cmbSingleMinute').allowBlank = true;
                                            this.up('panel').down('#cmbHour').allowBlank = false;
                                            this.up('panel').down('#cmbMinute').allowBlank = false;
                                        } else {
                                            /*顯示控制*/
                                            this.up('window').down('#dtSingleDate').setVisible(true);
                                            this.up('window').down('#cmbSingleHour').setVisible(true);
                                            this.up('window').down('#cmbSingleMinute').setVisible(true);
                                            this.up('window').down('#labSingleHour').setVisible(true);
                                            this.up('window').down('#labSingleMinute').setVisible(true);
                                            this.up('window').down('#tpCycle').setVisible(false);
                                            this.up('window').down('#cmbHour').setVisible(false);
                                            this.up('window').down('#cmbMinute').setVisible(false);
                                            this.up('window').down('#labHour').setVisible(false);
                                            this.up('window').down('#labMinute').setVisible(false);
                                            /*驗証控制*/
                                            this.up('window').down('#dtSingleDate').allowBlank = false;
                                            this.up('window').down('#cmbSingleHour').allowBlank = false;
                                            this.up('window').down('#cmbSingleMinute').allowBlank = false;
                                            this.up('window').down('#cmbHour').allowBlank = true;
                                            this.up('window').down('#cmbMinute').allowBlank = true;
                                            this.up('window').down('#cmbHour').setValue('');
                                            this.up('window').down('#cmbMinute').setValue('');
                                            this.up('window').down('#txtCDay').allowBlank = false;
                                            this.up('window').down('#txtCWeek').allowBlank = true;
                                        }
                                    }
                                }
                            }, {
                                fieldLabel: '單次執行時間',
                                xtype: 'datefield',
                                name: 'SINGLE_DATE',
                                format: 'Y-m-d',
                                dateFormat: 'Y-m-d',
                                renderer: Ext.util.Format.dateRenderer('Y-m-d'),
                                itemId: 'dtSingleDate',
                                editable: false,
                                allowBlank: false,
                                flex: 1
                            }, {
                                xtype: 'combo',
                                queryMode: 'local',
                                itemId: 'cmbSingleHour',
                                displayField: 'text',
                                valueField: 'value',
                                name: 'cmbSingleHour',
                                editable: false,
                                hidden: false,
                                allowBlank: false,
                                margin: '0 0 0 10',
                                store: new Ext.data.ArrayStore({
                                    fields: ['text', 'value'],
                                    data: [
                                        ['0', '0'],
                                        ['1', '1'],
                                        ['2', '2'],
                                        ['3', '3'],
                                        ['4', '4'],
                                        ['5', '5'],
                                        ['6', '6'],
                                        ['7', '7'],
                                        ['8', '8'],
                                        ['9', '9'],
                                        ['10', '10'],
                                        ['11', '11'],
                                        ['12', '12'],
                                        ['13', '13'],
                                        ['14', '14'],
                                        ['15', '15'],
                                        ['16', '16'],
                                        ['17', '17'],
                                        ['18', '18'],
                                        ['19', '19'],
                                        ['20', '20'],
                                        ['21', '21'],
                                        ['22', '22'],
                                        ['23', '23'],
                                    ]
                                })
                            }, {
                                xtype: 'label',
                                margin: '0 0 0 10',
                                text: '時',
                                itemId: 'labSingleHour'
                            }, {
                                xtype: 'combo',
                                itemId: 'cmbSingleMinute',
                                queryMode: 'local',
                                displayField: 'text',
                                valueField: 'value',
                                name: 'cmbSingleMinute',
                                editable: false,
                                hidden: false,
                                allowBlank: false,
                                margin: '0 0 0 10',
                                store: new Ext.data.ArrayStore({
                                    fields: ['text', 'value'],
                                    data: [
                                        ['0', '0'],
                                        ['1', '1'],
                                        ['2', '2'],
                                        ['3', '3'],
                                        ['4', '4'],
                                        ['5', '5'],
                                        ['6', '6'],
                                        ['7', '7'],
                                        ['8', '8'],
                                        ['9', '9'],
                                        ['10', '10'],
                                        ['11', '11'],
                                        ['12', '12'],
                                        ['13', '13'],
                                        ['14', '14'],
                                        ['15', '15'],
                                        ['16', '16'],
                                        ['17', '17'],
                                        ['18', '18'],
                                        ['19', '19'],
                                        ['20', '20'],
                                        ['21', '21'],
                                        ['22', '22'],
                                        ['23', '23'],
                                        ['24', '24'],
                                        ['25', '25'],
                                        ['26', '26'],
                                        ['27', '27'],
                                        ['28', '28'],
                                        ['29', '29'],
                                        ['30', '30'],
                                        ['31', '31'],
                                        ['32', '32'],
                                        ['33', '33'],
                                        ['34', '34'],
                                        ['35', '35'],
                                        ['36', '36'],
                                        ['37', '37'],
                                        ['38', '38'],
                                        ['39', '39'],
                                        ['40', '40'],
                                        ['41', '41'],
                                        ['42', '42'],
                                        ['43', '43'],
                                        ['44', '44'],
                                        ['45', '45'],
                                        ['46', '46'],
                                        ['47', '47'],
                                        ['48', '48'],
                                        ['49', '49'],
                                        ['50', '50'],
                                        ['51', '51'],
                                        ['52', '52'],
                                        ['53', '53'],
                                        ['54', '54'],
                                        ['55', '55'],
                                        ['56', '56'],
                                        ['57', '57'],
                                        ['58', '58'],
                                        ['59', '59']

                                    ]
                                })
                            }, {
                                xtype: 'label',
                                margin: '0 0 0 10',
                                text: '分',
                                itemId: 'labSingleMinute'
                            }]
                        }, {
                            xtype: 'container',
                            layout: 'hbox',
                            items: [{
                                fieldLabel: '排程開始日期',
                                xtype: 'datefield',
                                labelAlign: 'right',
                                name: 'START_DATE',
                                itemId: 'START_DATE',
                                format: 'Y/m/d',
                                dateFormat: 'Y/m/d h:i:s',
                                renderer: Ext.util.Format.dateRenderer('Y/m/d h:i:s'),
                                editable: false,
                                allowBlank: false,
                                flex: 1
                            }, {
                                fieldLabel: '排程結束日期',
                                xtype: 'datefield',
                                labelAlign: 'right',
                                name: 'SCHEDULE_END_DATE',
                                itemId: 'SCHEDULE_END_DATE',
                                format: 'Y/m/d',
                                dateFormat: 'Y/m/d h:i:s',
                                renderer: Ext.util.Format.dateRenderer('Y/m/d h:i:s'),
                                editable: false,
                                allowBlank: false,
                                flex: 1
                            }]
                        }]
                    }, {
                        xtype: 'container',
                        layout: 'hbox',
                        items: [{
                            xtype: 'tabpanel',
                            padding: 10,
                            width: $(window).width() * 0.9,
                            itemId: 'tpCycle',
                            items: [{
                                title: '<img src="' + SYSTEM_URL_ROOT + '/css/images/cal-day.png" style="width:16px;height:16px;vertical-align:middle;margin-right:5px;"/>' + '每天重複',
                                itemId: 'tabDay',
                                listeners: {
                                    activate: function(obj, eOpts) {

                                        this.up('panel').up('panel').down('#txtCycleType').setValue('每天重複');
                                        this.up('panel').down('#txtCDay').allowBlank = false;
                                        this.up('panel').down('#txtCWeek').allowBlank = true;
                                        this.up('panel').down('#txtCMonth').allowBlank = true;
                                        this.up('panel').down('#txtCHour').allowBlank = true;
                                        this.up('panel').down('#txtCMinute').allowBlank = true;
                                        this.up('panel').up('panel').down('#cmbHour').setDisabled(false);
                                        this.up('panel').up('panel').down('#cmbMinute').setDisabled(false);
                                        this.up('window').down('#dtSingleDate').setVisible(false);
                                        this.up('window').down('#cmbSingleHour').setVisible(false);
                                        this.up('window').down('#cmbSingleMinute').setVisible(false);
                                        this.up('window').down('#labSingleHour').setVisible(false);
                                        this.up('window').down('#labSingleMinute').setVisible(false);
                                        this.up('window').down('#tpCycle').setVisible(true);
                                        this.up('window').down('#cmbHour').setVisible(true);
                                        this.up('window').down('#cmbMinute').setVisible(true);
                                        this.up('window').down('#labHour').setVisible(true);
                                        this.up('window').down('#labMinute').setVisible(true);
                                        /*驗証控制*/
                                        this.up('window').down('#dtSingleDate').allowBlank = true;
                                        this.up('window').down('#cmbSingleHour').allowBlank = true;
                                        this.up('window').down('#cmbSingleMinute').allowBlank = true;
                                        this.up('window').down('#cmbHour').allowBlank = false;
                                        this.up('window').down('#cmbMinute').allowBlank = false;
                                        this.up('window').down('#txtCWeek').allowBlank = true;
                                        this.up('window').down('#txtCDay').allowBlank = false;
                                        this.up('window').down("#txtCycleType").setValue("每天重複");
                                    }
                                },
                                items: [{
                                    xtype: 'panel',
                                    border: false,
                                    margin: '0 0 0 50',
                                    items: [{
                                        xtype: 'container',
                                        layout: 'hbox',
                                        items: [{
                                            xtype: 'label',
                                            text: '每',
                                            margin: '0 0 0 10'
                                        }, {
                                            xtype: 'textfield',
                                            itemId: 'txtCDay',
                                            margin: '0 0 0 10',
                                            name: 'C_DAY',
                                            width: 80
                                        }, {
                                            xtype: 'label',
                                            text: '天',
                                            margin: '0 0 0 10'
                                        }]
                                    }]
                                }]
                            }, {
                                title: '<img src="' + SYSTEM_URL_ROOT + '/css/images/cal-week.png" style="width:16px;height:16px;vertical-align:middle;margin-right:5px;"/>' + '每週重複',
                                itemId: 'tabWeek',
                                listeners: {
                                    activate: function(obj, eOpts) {
                                        this.up('window').down('#txtCycleType').setValue('每週重複');

                                        this.up('window').down('#txtCWeek').allowBlank = false;
                                        this.up('window').down('#txtCMonth').allowBlank = true;
                                        this.up('window').down('#txtCDay').allowBlank = true;
                                        this.up('window').down('#txtCHour').allowBlank = true;
                                        this.up('window').down('#txtCMinute').allowBlank = true;
                                        this.up('window').down('#cmbHour').setDisabled(false);
                                        this.up('window').down('#cmbMinute').setDisabled(false);
                                        //C_DAY_OF_WEEK
                                        if (this.up('window').down('#txtCycleType_Org').getValue() != "每週重複") {
                                            this.up('window').down('#C_DAY_OF_WEEK').setValue("");
                                        }
                                    }
                                },
                                items: [{
                                    xtype: 'panel',
                                    border: false,
                                    margin: '0 0 0 50',
                                    items: [{
                                        xtype: 'container',
                                        layout: 'hbox',
                                        items: [{
                                            xtype: 'label',
                                            text: '每',
                                            margin: '0 0 0 10'
                                        }, {
                                            xtype: 'textfield',
                                            itemId: 'txtCWeek',
                                            name: 'C_WEEK',
                                            width: 80,
                                            margin: '0 0 0 10'
                                        }, {
                                            xtype: 'label',
                                            text: '週',
                                            margin: '0 0 0 10'
                                        }]
                                    }, {
                                        xtype: 'container',
                                        layout: 'hbox',
                                        margin: '0 0 0 50',
                                        items: [{
                                            xtype: 'checkbox',
                                            fieldLabel: '星期一',
                                            labelAlign: 'right',
                                            itemId: 'chk_day_week_1',
                                            data: '1',
                                            listeners: {
                                                change: function(obj) {
                                                    this.up('window').weekChangeInWeek(obj);
                                                }
                                            }
                                        }, {
                                            xtype: 'checkbox',
                                            fieldLabel: '星期二',
                                            labelAlign: 'right',
                                            itemId: 'chk_day_week_2',
                                            data: '2',
                                            listeners: {
                                                change: function(obj) {
                                                    this.up('window').weekChangeInWeek(obj);
                                                }
                                            }
                                        }, {
                                            xtype: 'checkbox',
                                            fieldLabel: '星期三',
                                            labelAlign: 'right',
                                            itemId: 'chk_day_week_3',
                                            data: '3',
                                            listeners: {
                                                change: function(obj) {
                                                    this.up('window').weekChangeInWeek(obj);
                                                }
                                            }
                                        }]
                                    }, {
                                        xtype: 'container',
                                        layout: 'hbox',
                                        margin: '0 0 0 50',
                                        items: [{
                                            xtype: 'checkbox',
                                            fieldLabel: '星期四',
                                            labelAlign: 'right',
                                            itemId: 'chk_day_week_4',
                                            data: '4',
                                            listeners: {
                                                change: function(obj) {
                                                    this.up('window').weekChangeInWeek(obj);
                                                }
                                            }
                                        }, {
                                            xtype: 'checkbox',
                                            fieldLabel: '星期五',
                                            itemId: 'chk_day_week_5',
                                            labelAlign: 'right',
                                            data: '5',
                                            listeners: {
                                                change: function(obj) {
                                                    this.up('window').weekChangeInWeek(obj);
                                                }
                                            }
                                        }, {
                                            xtype: 'checkbox',
                                            fieldLabel: '星期六',
                                            itemId: 'chk_day_week_6',
                                            labelAlign: 'right',
                                            data: '6',
                                            listeners: {
                                                change: function(obj) {
                                                    this.up('window').weekChangeInWeek(obj);
                                                }
                                            }
                                        }, {
                                            xtype: 'checkbox',
                                            fieldLabel: '星期日',
                                            labelAlign: 'right',
                                            itemId: 'chk_day_week_0',
                                            data: '0',
                                            listeners: {
                                                change: function(obj) {
                                                    this.up('window').weekChangeInWeek(obj);
                                                }
                                            }
                                        }]
                                    }]
                                }]
                            }, {
                                title: '<img src="' + SYSTEM_URL_ROOT + '/css/images/cal-month.png" style="width:16px;height:16px;vertical-align:middle;margin-right:5px;"/>' + '每月重複',
                                itemId: 'tabMonth',
                                listeners: {
                                    activate: function(obj, eOpts) {
                                        this.up('panel').up('panel').down('#txtCycleType').setValue('每月重複');
                                        this.up('panel').down('#txtCMonth').allowBlank = false;
                                        this.up('panel').down('#txtCWeek').allowBlank = true;
                                        this.up('panel').down('#txtCDay').allowBlank = true;
                                        this.up('panel').down('#txtCHour').allowBlank = true;
                                        this.up('panel').down('#txtCMinute').allowBlank = true;

                                        this.up('panel').up('panel').down('#cmbHour').setDisabled(false);
                                        this.up('panel').up('panel').down('#cmbMinute').setDisabled(false);
                                        //C_DAY_OF_WEEK
                                        if (this.up('panel').up('panel').down('#txtCycleType_Org').getValue() != "每月重複") {
                                            this.up('panel').up('panel').down('#C_DAY_OF_WEEK').setValue("");
                                        }
                                    }
                                },
                                items: [{
                                    xtype: 'panel',
                                    border: false,
                                    margin: '0 0 0 50',
                                    items: [{
                                        xtype: 'panel',
                                        border: false,
                                        items: [{
                                            xtype: 'container',
                                            layout: 'hbox',
                                            items: [{
                                                xtype: 'label',
                                                text: '每',
                                                margin: '0 0 0 10'
                                            }, {
                                                xtype: 'textfield',
                                                itemId: 'txtCMonth',
                                                name: 'C_MONTH',
                                                width: 80,
                                                margin: '0 0 0 10'
                                            }, {
                                                xtype: 'label',
                                                text: '月',
                                                margin: '0 0 0 10'
                                            }]
                                        }]
                                    }, {
                                        xtype: 'combo',
                                        fieldLabel: '月排程方式',
                                        queryMode: 'local',
                                        itemId: 'cmbMonthType',
                                        displayField: 'text',
                                        valueField: 'value',
                                        name: 'name',
                                        editable: false,
                                        hidden: false,
                                        labelWidth: 80,
                                        store: new Ext.data.ArrayStore({
                                            fields: ['text', 'value'],
                                            data: [
                                                ['日為主', '日為主'],
                                                ['週為主', '週為主', ]
                                            ]
                                        }),
                                        listeners: {
                                            'select': function(combo, records, eOpts) {
                                                if (combo.getValue() == '週為主') {
                                                    this.up('panel').down('#plMonthWeek').setVisible(true);
                                                    this.up('panel').down('#plMonthDay').setVisible(false);
                                                } else {
                                                    this.up('panel').down('#plMonthWeek').setVisible(false);
                                                    this.up('panel').down('#plMonthDay').setVisible(true);
                                                }
                                            }
                                        }
                                    }, {
                                        xtype: 'panel',
                                        border: true,
                                        margin: '0 0 0 75',
                                        padding: '10 10 0 10',
                                        itemId: 'plMonthDay',
                                        hidden: true,
                                        items: [{
                                            xtype: 'container',
                                            layout: 'hbox',
                                            defaults: {
                                                labelWidth: 50
                                            },
                                            items: [{
                                                    xtype: 'checkbox',
                                                    fieldLabel: '1日',
                                                    labelAlign: 'right',
                                                    data: '1',
                                                    itemId: 'chk_day_month_1',
                                                    listeners: {
                                                        change: function(obj) {
                                                            this.up('window').dayChangeInMonth(obj);
                                                        }
                                                    }
                                                }, {
                                                    xtype: 'checkbox',
                                                    fieldLabel: '2日',
                                                    labelAlign: 'right',
                                                    itemId: 'chk_day_month_2',
                                                    data: '2',
                                                    listeners: {
                                                        change: function(obj) {
                                                            this.up('window').dayChangeInMonth(obj);
                                                        }
                                                    }
                                                }, {
                                                    xtype: 'checkbox',
                                                    fieldLabel: '3日',
                                                    labelAlign: 'right',
                                                    itemId: 'chk_day_month_3',
                                                    data: '3',
                                                    listeners: {
                                                        change: function(obj) {
                                                            this.up('window').dayChangeInMonth(obj);
                                                        }
                                                    }
                                                }, {
                                                    xtype: 'checkbox',
                                                    fieldLabel: '4日',
                                                    labelAlign: 'right',
                                                    itemId: 'chk_day_month_4',
                                                    data: '4',
                                                    listeners: {
                                                        change: function(obj) {
                                                            this.up('window').dayChangeInMonth(obj);
                                                        }
                                                    }
                                                }, {
                                                    xtype: 'checkbox',
                                                    fieldLabel: '5日',
                                                    labelAlign: 'right',
                                                    itemId: 'chk_day_month_5',
                                                    data: '5',
                                                    listeners: {
                                                        change: function(obj) {
                                                            this.up('window').dayChangeInMonth(obj);
                                                        }
                                                    }
                                                }, {
                                                    xtype: 'checkbox',
                                                    fieldLabel: '6日',
                                                    labelAlign: 'right',
                                                    itemId: 'chk_day_month_6',
                                                    data: '6',
                                                    listeners: {
                                                        change: function(obj) {
                                                            this.up('window').dayChangeInMonth(obj);
                                                        }
                                                    }
                                                }, {
                                                    xtype: 'checkbox',
                                                    fieldLabel: '7日',
                                                    labelAlign: 'right',
                                                    itemId: 'chk_day_month_7',
                                                    data: '7',
                                                    listeners: {
                                                        change: function(obj) {
                                                            this.up('window').dayChangeInMonth(obj);
                                                        }
                                                    }
                                                }, {
                                                    xtype: 'checkbox',
                                                    fieldLabel: '8日',
                                                    labelAlign: 'right',
                                                    itemId: 'chk_day_month_8',
                                                    data: '8',
                                                    listeners: {
                                                        change: function(obj) {
                                                            this.up('window').dayChangeInMonth(obj);
                                                        }
                                                    }
                                                }, {
                                                    xtype: 'checkbox',
                                                    fieldLabel: '9日',
                                                    labelAlign: 'right',
                                                    itemId: 'chk_day_month_9',
                                                    data: '9',
                                                    listeners: {
                                                        change: function(obj) {
                                                            this.up('window').dayChangeInMonth(obj);
                                                        }
                                                    }
                                                }, {
                                                    xtype: 'checkbox',
                                                    fieldLabel: '10日',
                                                    labelAlign: 'right',
                                                    itemId: 'chk_day_month_10',
                                                    data: '10',
                                                    listeners: {
                                                        change: function(obj) {
                                                            this.up('window').dayChangeInMonth(obj);
                                                        }
                                                    }
                                                }

                                            ]
                                        }, {
                                            xtype: 'container',
                                            layout: 'hbox',
                                            defaults: {
                                                labelWidth: 50
                                            },
                                            items: [{
                                                xtype: 'checkbox',
                                                fieldLabel: '11日',
                                                labelAlign: 'right',
                                                itemId: 'chk_day_month_11',
                                                data: '11',
                                                listeners: {
                                                    change: function(obj) {
                                                        this.up('window').dayChangeInMonth(obj);
                                                    }
                                                }
                                            }, {
                                                xtype: 'checkbox',
                                                fieldLabel: '12日',
                                                labelAlign: 'right',
                                                itemId: 'chk_day_month_12',
                                                data: '12',
                                                listeners: {
                                                    change: function(obj) {
                                                        this.up('window').dayChangeInMonth(obj);
                                                    }
                                                }
                                            }, {
                                                xtype: 'checkbox',
                                                fieldLabel: '13日',
                                                labelAlign: 'right',
                                                itemId: 'chk_day_month_13',
                                                data: '13',
                                                listeners: {
                                                    change: function(obj) {
                                                        this.up('window').dayChangeInMonth(obj);
                                                    }
                                                }
                                            }, {
                                                xtype: 'checkbox',
                                                fieldLabel: '14日',
                                                labelAlign: 'right',
                                                itemId: 'chk_day_month_14',
                                                data: '14',
                                                listeners: {
                                                    change: function(obj) {
                                                        this.up('window').dayChangeInMonth(obj);
                                                    }
                                                }
                                            }, {
                                                xtype: 'checkbox',
                                                fieldLabel: '15日',
                                                labelAlign: 'right',
                                                itemId: 'chk_day_month_15',
                                                data: '15',
                                                listeners: {
                                                    change: function(obj) {
                                                        this.up('window').dayChangeInMonth(obj);
                                                    }
                                                }
                                            }, {
                                                xtype: 'checkbox',
                                                fieldLabel: '16日',
                                                labelAlign: 'right',
                                                itemId: 'chk_day_month_16',
                                                data: '16',
                                                listeners: {
                                                    change: function(obj) {
                                                        this.up('window').dayChangeInMonth(obj);
                                                    }
                                                }
                                            }, {
                                                xtype: 'checkbox',
                                                fieldLabel: '17日',
                                                labelAlign: 'right',
                                                itemId: 'chk_day_month_17',
                                                data: '17',
                                                listeners: {
                                                    change: function(obj) {
                                                        this.up('window').dayChangeInMonth(obj);
                                                    }
                                                }
                                            }, {
                                                xtype: 'checkbox',
                                                fieldLabel: '18日',
                                                labelAlign: 'right',
                                                itemId: 'chk_day_month_18',
                                                data: '18',
                                                listeners: {
                                                    change: function(obj) {
                                                        this.up('window').dayChangeInMonth(obj);
                                                    }
                                                }
                                            }, {
                                                xtype: 'checkbox',
                                                fieldLabel: '19日',
                                                labelAlign: 'right',
                                                itemId: 'chk_day_month_19',
                                                data: '19',
                                                listeners: {
                                                    change: function(obj) {
                                                        this.up('window').dayChangeInMonth(obj);
                                                    }
                                                }
                                            }, {
                                                xtype: 'checkbox',
                                                fieldLabel: '20日',
                                                labelAlign: 'right',
                                                itemId: 'chk_day_month_20',
                                                data: '20',
                                                listeners: {
                                                    change: function(obj) {
                                                        this.up('window').dayChangeInMonth(obj);
                                                    }
                                                }
                                            }]
                                        }, {
                                            xtype: 'container',
                                            layout: 'hbox',
                                            defaults: {
                                                labelWidth: 50
                                            },
                                            items: [

                                                {
                                                    xtype: 'checkbox',
                                                    fieldLabel: '21日',
                                                    labelAlign: 'right',
                                                    itemId: 'chk_day_month_21',
                                                    data: '21',
                                                    listeners: {
                                                        change: function(obj) {
                                                            this.up('window').dayChangeInMonth(obj);
                                                        }
                                                    }
                                                }, {
                                                    xtype: 'checkbox',
                                                    fieldLabel: '22日',
                                                    labelAlign: 'right',
                                                    itemId: 'chk_day_month_22',
                                                    data: '22',
                                                    listeners: {
                                                        change: function(obj) {
                                                            this.up('window').dayChangeInMonth(obj);
                                                        }
                                                    }
                                                }, {
                                                    xtype: 'checkbox',
                                                    fieldLabel: '23日',
                                                    labelAlign: 'right',
                                                    itemId: 'chk_day_month_23',
                                                    data: '23',
                                                    listeners: {
                                                        change: function(obj) {
                                                            this.up('window').dayChangeInMonth(obj);
                                                        }
                                                    }
                                                }, {
                                                    xtype: 'checkbox',
                                                    fieldLabel: '24日',
                                                    labelAlign: 'right',
                                                    itemId: 'chk_day_month_24',
                                                    data: '24',
                                                    listeners: {
                                                        change: function(obj) {
                                                            this.up('window').dayChangeInMonth(obj);
                                                        }
                                                    }
                                                }, {
                                                    xtype: 'checkbox',
                                                    fieldLabel: '25日',
                                                    labelAlign: 'right',
                                                    itemId: 'chk_day_month_25',
                                                    data: '25',
                                                    listeners: {
                                                        change: function(obj) {
                                                            this.up('window').dayChangeInMonth(obj);
                                                        }
                                                    }
                                                }, {
                                                    xtype: 'checkbox',
                                                    fieldLabel: '26日',
                                                    labelAlign: 'right',
                                                    itemId: 'chk_day_month_26',
                                                    data: '26',
                                                    listeners: {
                                                        change: function(obj) {
                                                            this.up('window').dayChangeInMonth(obj);
                                                        }
                                                    }
                                                }, {
                                                    xtype: 'checkbox',
                                                    fieldLabel: '27日',
                                                    labelAlign: 'right',
                                                    itemId: 'chk_day_month_27',
                                                    data: '27',
                                                    listeners: {
                                                        change: function(obj) {
                                                            this.up('window').dayChangeInMonth(obj);
                                                        }
                                                    }
                                                }, {
                                                    xtype: 'checkbox',
                                                    fieldLabel: '28日',
                                                    labelAlign: 'right',
                                                    itemId: 'chk_day_month_28',
                                                    data: '28',
                                                    listeners: {
                                                        change: function(obj) {
                                                            this.up('window').dayChangeInMonth(obj);
                                                        }
                                                    }
                                                }, {
                                                    xtype: 'checkbox',
                                                    fieldLabel: '29日',
                                                    labelAlign: 'right',
                                                    itemId: 'chk_day_month_29',
                                                    data: '29',
                                                    listeners: {
                                                        change: function(obj) {
                                                            this.up('window').dayChangeInMonth(obj);
                                                        }
                                                    }
                                                }, {
                                                    xtype: 'checkbox',
                                                    fieldLabel: '30日',
                                                    labelAlign: 'right',
                                                    itemId: 'chk_day_month_30',
                                                    data: '30',
                                                    listeners: {
                                                        change: function(obj) {
                                                            this.up('window').dayChangeInMonth(obj);
                                                        }
                                                    }
                                                }
                                            ]
                                        }, {
                                            xtype: 'container',
                                            layout: 'hbox',
                                            defaults: {
                                                labelWidth: 50
                                            },
                                            items: [

                                                {
                                                    xtype: 'checkbox',
                                                    fieldLabel: '31日',
                                                    labelAlign: 'right',
                                                    itemId: 'chk_day_month_31',
                                                    data: '31',
                                                    listeners: {
                                                        change: function(obj) {
                                                            this.up('window').dayChangeInMonth(obj);
                                                        }
                                                    }
                                                }
                                            ]
                                        }]
                                    }, {
                                        xtype: 'panel',
                                        border: true,
                                        margin: '0 0 0 75',
                                        padding: '10 10 0 10',
                                        hidden: true,
                                        itemId: 'plMonthWeek',
                                        items: [{
                                            xtype: 'container',
                                            layout: 'hbox',
                                            items: [{
                                                xtype: 'checkbox',
                                                fieldLabel: '第1週',
                                                itemId: 'chk_week_month_1',
                                                labelAlign: 'right',
                                                labelWidth: 60,
                                                data: '1',
                                                listeners: {
                                                    change: function(obj) {
                                                        this.up('window').weekChangeInMonth(obj);
                                                    }
                                                }
                                            }, {
                                                xtype: 'checkbox',
                                                fieldLabel: '第2週',
                                                itemId: 'chk_week_month_2',
                                                labelAlign: 'right',
                                                labelWidth: 60,
                                                data: '2',
                                                listeners: {
                                                    change: function(obj) {
                                                        this.up('window').weekChangeInMonth(obj);
                                                    }
                                                }
                                            }, {
                                                xtype: 'checkbox',
                                                fieldLabel: '第3週',
                                                itemId: 'chk_week_month_3',
                                                labelAlign: 'right',
                                                labelWidth: 60,
                                                data: '3',
                                                listeners: {
                                                    change: function(obj) {
                                                        this.up('window').weekChangeInMonth(obj);
                                                    }
                                                }
                                            }, {
                                                xtype: 'checkbox',
                                                fieldLabel: '第4週',
                                                itemId: 'chk_week_month_4',
                                                labelAlign: 'right',
                                                labelWidth: 60,
                                                data: '4',
                                                listeners: {
                                                    change: function(obj) {
                                                        this.up('window').weekChangeInMonth(obj);
                                                    }
                                                }
                                            }, {
                                                xtype: 'checkbox',
                                                fieldLabel: '第5週',
                                                itemId: 'chk_week_month_5',
                                                labelAlign: 'right',
                                                labelWidth: 60,
                                                data: '5',
                                                listeners: {
                                                    change: function(obj) {
                                                        this.up('window').weekChangeInMonth(obj);
                                                    }
                                                }
                                            }]
                                        }, {
                                            xtype: 'container',
                                            layout: 'hbox',
                                            items: [{
                                                xtype: 'checkbox',
                                                fieldLabel: '星期一',
                                                itemId: 'chk_day_week_month_1',
                                                labelAlign: 'right',
                                                labelWidth: 60,
                                                data: '1',
                                                listeners: {
                                                    change: function(obj) {
                                                        this.up('window').weekChangeInWeek(obj);
                                                    }
                                                }
                                            }, {
                                                xtype: 'checkbox',
                                                fieldLabel: '星期二',
                                                itemId: 'chk_day_week_month_2',
                                                labelAlign: 'right',
                                                labelWidth: 60,
                                                data: '2',
                                                listeners: {
                                                    change: function(obj) {
                                                        this.up('window').weekChangeInWeek(obj);
                                                    }
                                                }
                                            }, {
                                                xtype: 'checkbox',
                                                fieldLabel: '星期三',
                                                itemId: 'chk_day_week_month_3',
                                                labelAlign: 'right',
                                                labelWidth: 60,
                                                data: '3',
                                                listeners: {
                                                    change: function(obj) {
                                                        this.up('window').weekChangeInWeek(obj);
                                                    }
                                                }
                                            }, {
                                                xtype: 'checkbox',
                                                fieldLabel: '星期四',
                                                itemId: 'chk_day_week_month_4',
                                                labelAlign: 'right',
                                                labelWidth: 60,
                                                data: '4',
                                                listeners: {
                                                    change: function(obj) {
                                                        this.up('window').weekChangeInWeek(obj);
                                                    }
                                                }
                                            }, {
                                                xtype: 'checkbox',
                                                fieldLabel: '星期五',
                                                itemId: 'chk_day_week_month_5',
                                                labelAlign: 'right',
                                                labelWidth: 60,
                                                data: '5',
                                                listeners: {
                                                    change: function(obj) {
                                                        this.up('window').weekChangeInWeek(obj);
                                                    }
                                                }
                                            }, {
                                                xtype: 'checkbox',
                                                fieldLabel: '星期六',
                                                itemId: 'chk_day_week_month_6',
                                                labelAlign: 'right',
                                                labelWidth: 60,
                                                data: '6',
                                                listeners: {
                                                    change: function(obj) {
                                                        this.up('window').weekChangeInWeek(obj);
                                                    }
                                                }
                                            }, {
                                                xtype: 'checkbox',
                                                fieldLabel: '星期日',
                                                itemId: 'chk_day_week_month_0',
                                                labelAlign: 'right',
                                                labelWidth: 60,
                                                data: '0',
                                                listeners: {
                                                    change: function(obj) {
                                                        this.up('window').weekChangeInWeek(obj);
                                                    }
                                                }
                                            }]
                                        }]
                                    }]
                                }]
                            }, {
                                title: '<img src="' + SYSTEM_URL_ROOT + '/css/images/cal-hour.png" style="width:16px;height:16px;vertical-align:middle;margin-right:5px;"/>' + '每時重複',
                                itemId: 'tabHour',
                                listeners: {
                                    activate: function(obj, eOpts) {
                                        this.up('panel').up('panel').down('#txtCycleType').setValue('每時重複');

                                        this.up('panel').down('#txtCHour').allowBlank = false;
                                        this.up('panel').down('#txtCMonth').allowBlank = true;
                                        this.up('panel').down('#txtCWeek').allowBlank = true;
                                        this.up('panel').down('#txtCDay').allowBlank = true;
                                        this.up('panel').up('panel').down('#cmbHour').setValue('');
                                        this.up('panel').up('panel').down('#cmbHour').setDisabled(true);
                                        this.up('panel').up('panel').down('#cmbMinute').setDisabled(false);
                                    }
                                },
                                items: [{
                                    xtype: 'panel',
                                    border: false,
                                    margin: '0 0 0 50',
                                    items: [{
                                        xtype: 'container',
                                        layout: 'hbox',
                                        items: [{
                                            xtype: 'label',
                                            text: '每',
                                            margin: '0 0 0 10'
                                        }, {
                                            xtype: 'textfield',
                                            width: 80,
                                            itemId: 'txtCHour',
                                            name: 'C_HOUR',
                                            margin: '0 0 0 10'
                                        }, {
                                            xtype: 'label',
                                            text: '小時',
                                            margin: '0 0 0 10'
                                        }]
                                    }]
                                }]
                            }, {
                                title: '<img src="' + SYSTEM_URL_ROOT + '/css/images/cal-minute.png" style="width:16px;height:16px;vertical-align:middle;margin-right:5px;"/>' + '每分重複',
                                itemId: 'tabMinute',
                                listeners: {
                                    activate: function(obj, eOpts) {
                                        this.up('panel').up('panel').down('#txtCycleType').setValue('每分重複');
                                        this.up('panel').down('#txtCMinute').allowBlank = false;
                                        this.up('panel').down('#txtCHour').allowBlank = true;
                                        this.up('panel').down('#txtCMonth').allowBlank = true;
                                        this.up('panel').down('#txtCWeek').allowBlank = true;
                                        this.up('panel').down('#txtCDay').allowBlank = true;
                                        this.up('panel').up('panel').down('#cmbHour').setValue('');
                                        this.up('panel').up('panel').down('#cmbHour').setDisabled(true);
                                        this.up('panel').up('panel').down('#cmbMinute').setValue('');
                                        this.up('panel').up('panel').down('#cmbMinute').setDisabled(true);
                                    }
                                },
                                items: [{
                                    xtype: 'panel',
                                    border: false,
                                    margin: '0 0 0 50',
                                    items: [{
                                        xtype: 'container',
                                        layout: 'hbox',
                                        items: [{
                                            xtype: 'label',
                                            text: '每',
                                            margin: '0 0 0 10'
                                        }, {
                                            xtype: 'textfield',
                                            width: 80,
                                            itemId: 'txtCMinute',
                                            name: 'C_MINUTE',
                                            margin: '0 0 0 10'
                                        }, {
                                            xtype: 'label',
                                            text: '分',
                                            margin: '0 0 0 10'
                                        }]
                                    }]
                                }]
                            }]
                        }]
                    }, {
                        xtype: 'hidden',
                        fieldLabel: 'UUID',
                        name: 'UUID',
                        itemId: 'ScheduleForm.UUID',
                        padding: 5,
                        anchor: '100%',
                        maxLength: 84,
                        id: 'ScheduleForm.UUID'
                    }, {
                        xtype: 'hidden',
                        fieldLabel: 'COMPANY_UUID',
                        name: 'COMPANY_UUID',
                        padding: 5,
                        anchor: '100%',
                        maxLength: 84
                    }, {
                        xtype: 'hidden',
                        fieldLabel: 'ID',
                        name: 'ID',
                        padding: 5,
                        anchor: '100%',
                        maxLength: 84,
                        id: 'ScheduleForm.ID'
                    }, {
                        xtype: 'container',
                        layout: 'hbox',
                        items: [{
                            xtype: 'combo',
                            fieldLabel: '執行時間(H:M)',
                            labelAlign: 'right',
                            queryMode: 'local',
                            displayField: 'text',
                            valueField: 'value',
                            name: 'HOUR',
                            itemId: 'cmbHour',
                            editable: false,
                            hidden: false,
                            allowBlank: false,
                            store: new Ext.data.ArrayStore({
                                fields: ['text', 'value'],
                                data: [
                                    ['0', '0'],
                                    ['1', '1'],
                                    ['2', '2'],
                                    ['3', '3'],
                                    ['4', '4'],
                                    ['5', '5'],
                                    ['6', '6'],
                                    ['7', '7'],
                                    ['8', '8'],
                                    ['9', '9'],
                                    ['10', '10'],
                                    ['11', '11'],
                                    ['12', '12'],
                                    ['13', '13'],
                                    ['14', '14'],
                                    ['15', '15'],
                                    ['16', '16'],
                                    ['17', '17'],
                                    ['18', '18'],
                                    ['19', '19'],
                                    ['20', '20'],
                                    ['21', '21'],
                                    ['22', '22'],
                                    ['23', '23'],
                                ]
                            })
                        }, {
                            xtype: 'label',
                            margin: '0 0 0 10',
                            text: '時',
                            itemId: 'labHour'
                        }, {
                            xtype: 'combo',
                            queryMode: 'local',
                            displayField: 'text',
                            valueField: 'value',
                            name: 'MINUTE',
                            itemId: 'cmbMinute',
                            editable: false,
                            hidden: false,
                            allowBlank: false,
                            margin: '0 0 0 10',
                            store: new Ext.data.ArrayStore({
                                fields: ['text', 'value'],
                                data: [
                                    ['0', '0'],
                                    ['1', '1'],
                                    ['2', '2'],
                                    ['3', '3'],
                                    ['4', '4'],
                                    ['5', '5'],
                                    ['6', '6'],
                                    ['7', '7'],
                                    ['8', '8'],
                                    ['9', '9'],
                                    ['10', '10'],
                                    ['11', '11'],
                                    ['12', '12'],
                                    ['13', '13'],
                                    ['14', '14'],
                                    ['15', '15'],
                                    ['16', '16'],
                                    ['17', '17'],
                                    ['18', '18'],
                                    ['19', '19'],
                                    ['20', '20'],
                                    ['21', '21'],
                                    ['22', '22'],
                                    ['23', '23'],
                                    ['24', '24'],
                                    ['25', '25'],
                                    ['26', '26'],
                                    ['27', '27'],
                                    ['28', '28'],
                                    ['29', '29'],
                                    ['30', '30'],
                                    ['31', '31'],
                                    ['32', '32'],
                                    ['33', '33'],
                                    ['34', '34'],
                                    ['35', '35'],
                                    ['36', '36'],
                                    ['37', '37'],
                                    ['38', '38'],
                                    ['39', '39'],
                                    ['40', '40'],
                                    ['41', '41'],
                                    ['42', '42'],
                                    ['43', '43'],
                                    ['44', '44'],
                                    ['45', '45'],
                                    ['46', '46'],
                                    ['47', '47'],
                                    ['48', '48'],
                                    ['49', '49'],
                                    ['50', '50'],
                                    ['51', '51'],
                                    ['52', '52'],
                                    ['53', '53'],
                                    ['54', '54'],
                                    ['55', '55'],
                                    ['56', '56'],
                                    ['57', '57'],
                                    ['58', '58'],
                                    ['59', '59']

                                ]
                            })
                        }, {
                            xtype: 'label',
                            margin: '0 0 0 10',
                            itemId: 'labMinute',
                            text: '分'
                        }]
                    }, {
                        xtype: 'container',
                        layout: 'hbox',
                        margin: '5 0 0 0',
                        items: [{
                            xtype: 'textfield',
                            fieldLabel: '執行程式URL',
                            name: 'RUN_URL',
                            itemId: 'RUN_URL',
                            allowBlank: false,
                            labelAlign: 'right',
                            flex: 1
                        }, {
                            xtype: 'textfield',
                            fieldLabel: '執行參數',
                            name: 'RUN_URL_PARAMETER',
                            itemId: 'RUN_URL_PARAMETER',
                            allowBlank: true,
                            labelAlign: 'right',
                            flex: 1
                        }, {
                            xtype: 'button',
                            margin: '0 0 0 10',
                            text: '<img src="' + SYSTEM_URL_ROOT + '/css/images/run.png" style="width:16px;height:16px;vertical-align:middle;margin-right:5px;"/>' + '執行',
                            handler: function(handler, scope) {
                                var requestUrl = this.up('window').down('#RUN_URL').getValue() + '?' + this.up('window').down('#RUN_URL_PARAMETER').getValue();
                                Ext.Ajax.request({
                                    url: requestUrl,
                                    disableCaching: true,
                                    method: 'GET',
                                    waitTitle: 'Connecting',
                                    waitMsg: 'Sending data...',
                                    success: function(response, opts) {
                                        Ext.MessageBox.show({
                                            title: '回覆的內容為',
                                            icon: Ext.MessageBox.INFO,
                                            buttons: Ext.Msg.OK,
                                            msg: response.responseText
                                        });
                                    }
                                });
                            }
                        }]
                    }, {
                        xtype: 'container',
                        layout: 'hbox',
                        items: [{
                            xtype: 'combo',
                            fieldLabel: '執行安全',
                            queryMode: 'local',
                            displayField: 'text',
                            valueField: 'value',
                            name: 'RUN_SECURITY',
                            padding: 5,
                            editable: false,
                            labelAlign: 'right',
                            hidden: false,
                            allowBlank: false,
                            store: new Ext.data.ArrayStore({
                                fields: ['text', 'value'],
                                data: [
                                    ['本地執行', 'localhost'],
                                    ['本地/遠端執行', 'localhost,remote']
                                ]
                            })
                        }]
                    }, {
                        xtype: 'container',
                        layout: 'hbox',
                        hidden: true,
                        items: [{
                            xtype: 'textfield',
                            fieldLabel: '排程類型',
                            itemId: 'txtCycleType',
                            name: 'CYCLE_TYPE',
                            allowBlank: true,
                            labelAlign: 'right',
                            flex: 1
                        }, {
                            xtype: 'textfield',
                            fieldLabel: '排程類型(原)',
                            itemId: 'txtCycleType_Org',
                            allowBlank: true,
                            labelAlign: 'right',
                            flex: 1
                        }]
                    }, {
                        xtype: 'container',
                        layout: 'hbox',
                        hidden: true,
                        items: [{
                            xtype: 'textfield',
                            fieldLabel: '星期S',
                            itemId: 'C_DAY_OF_WEEK',
                            name: 'C_DAY_OF_WEEK',
                            hidden: false,
                            allowBlank: true,
                            labelAlign: 'right',
                            flex: 1
                        }, {
                            xtype: 'textfield',
                            fieldLabel: '日S',
                            hidden: false,
                            itemId: 'C_DAY_OF_MONTH',
                            name: 'C_DAY_OF_MONTH',
                            allowBlank: true,
                            labelAlign: 'right',
                            flex: 1
                        }, {
                            xtype: 'textfield',
                            fieldLabel: '週S',
                            hidden: false,
                            itemId: 'C_WEEK_OF_MONTH',
                            name: 'C_WEEK_OF_MONTH',
                            allowBlank: true,
                            labelAlign: 'right',
                            flex: 1
                        }]
                    }

                ],
                fbar: [{
                    type: 'button',
                    text: '<img src="' + SYSTEM_URL_ROOT + '/css/images/save.gif" style="width:16px;height:16px;vertical-align:middle;margin-right:5px;"/>' + '儲存',
                    handler: function() {
                        var form = Ext.getCmp('Ext.ScheduleForm.Form').getForm();
                        if (this.up('window').down('#IS_CYCLE_N').getValue() == true) {
                            this.up('window').down('#txtCDay').allowBlank = true;
                            this.up('window').down('#txtCycleType').setValue('單次任務');
                        }
                        if (form.isValid() == false) {
                            return;
                        }
                        if (this.up('window').down('#txtCycleType').getValue() == '每天重複' ||
                            this.up('window').down('#txtCycleType').getValue() == '每時重複' ||
                            this.up('window').down('#txtCycleType').getValue() == '每分重複'
                        ) {
                            this.up('window').down('#C_DAY_OF_WEEK').setValue('');
                            this.up('window').down('#C_DAY_OF_MONTH').setValue('');
                            this.up('window').down('#C_WEEK_OF_MONTH').setValue('');
                        };
                        if (this.up('window').down('#txtCycleType').getValue() == '每週重複') {
                            this.up('window').down('#C_DAY_OF_MONTH').setValue('');
                            this.up('window').down('#C_WEEK_OF_MONTH').setValue('');
                            for (var i = 1; i <= 5; i++) {
                                var _script = 'this.up("window").down("#chk_week_month_' + i + '").setValue(false);';
                                eval(_script);
                            }
                            for (var i = 0; i <= 6; i++) {
                                var _script = 'this.up("window").down("#chk_day_week_month_' + i + '").setValue(false);';
                                eval(_script);
                            }
                            if (this.up('window').down('#C_DAY_OF_WEEK').getValue() == '') {
                                Ext.MessageBox.show({
                                    title: '資料驗証問題',
                                    icon: Ext.MessageBox.INFO,
                                    buttons: Ext.Msg.OK,
                                    msg: '每週重複，必須指定星期一~星期日!'
                                });
                                return;
                            }
                        };
                        if (this.up('window').down('#txtCycleType').getValue() == '每月重複') {
                            if (this.up('window').down('#cmbMonthType').getValue() == '週為主') {
                                this.up('window').down('#C_DAY_OF_MONTH').setValue('');
                                for (var i = 1; i <= 31; i++) {
                                    var _script = 'this.up("window").down("#chk_day_month_' + i + '").setValue(false);';
                                    eval(_script);
                                }
                                for (var i = 0; i <= 6; i++) {
                                    var _script = 'this.up("window").down("#chk_day_week_' + i + '").setValue(false);';
                                    eval(_script);
                                }
                                if (this.up('window').down('#C_WEEK_OF_MONTH').getValue() == '') {
                                    Ext.MessageBox.show({
                                        title: '資料驗証問題',
                                        icon: Ext.MessageBox.INFO,
                                        buttons: Ext.Msg.OK,
                                        msg: '每月重複之週為主，必須指定星期!'
                                    });
                                    return;
                                }
                                if (this.up('window').down('#C_DAY_OF_WEEK').getValue() == '') {
                                    Ext.MessageBox.show({
                                        title: '資料驗証問題',
                                        icon: Ext.MessageBox.INFO,
                                        buttons: Ext.Msg.OK,
                                        msg: '每月重複之週為主，必須指定星期!'
                                    });
                                    return;
                                }
                            } else {
                                this.up('window').down('#C_WEEK_OF_MONTH').setValue('');
                                this.up('window').down('#C_DAY_OF_WEEK').setValue('');
                                for (var i = 1; i <= 5; i++) {
                                    var _script = 'this.up("window").down("#chk_week_month_' + i + '").setValue(false);';
                                    eval(_script);
                                }
                                for (var i = 0; i <= 6; i++) {
                                    var _script = 'this.up("window").down("#chk_day_week_month_' + i + '").setValue(false);';
                                    eval(_script);
                                    var _script = 'this.up("window").down("#chk_day_week_' + i + '").setValue(false);';
                                    eval(_script);
                                }
                                if (this.up('window').down('#C_DAY_OF_MONTH').getValue() == '') {
                                    Ext.MessageBox.show({
                                        title: '資料驗証問題',
                                        icon: Ext.MessageBox.INFO,
                                        buttons: Ext.Msg.OK,
                                        msg: '每月重複之日為主，必須指定日期!'
                                    });
                                    return;
                                }
                            }
                        };
                        var mainObj = this;
                        form.submit({
                            waitMsg: '更新中...',
                            success: function(form, action) {
                                Ext.getCmp('ExtScheduleForm').uuid = action.result.UUID;
                                Ext.getCmp('ScheduleForm.UUID').setValue(action.result.UUID);
                                Ext.getCmp('ExtScheduleForm').setTitle('工作排程【維護】');

                                mainObj.up('window').down("#txtCycleType_Org").setValue(mainObj.up('window').down("#txtCycleType").getValue());

                                mainObj.up('window').uuid = ScheduleForm.UUID;
                                Ext.MessageBox.show({
                                    title: '工作排程',
                                    msg: '操作完成',
                                    icon: Ext.MessageBox.INFO,
                                    buttons: Ext.Msg.OK
                                });
                            },
                            failure: function(form, action) {
                                Ext.MessageBox.show({
                                    title: 'Warning',
                                    msg: action.result.message,
                                    icon: Ext.MessageBox.ERROR,
                                    buttons: Ext.Msg.OK
                                });
                            }
                        });
                    }
                }, {
                    type: 'button',
                    text: '<img src="' + SYSTEM_URL_ROOT + '/css/images/delete.png" style="width:20px;height:16px;vertical-align:middle;margin-right:5px;"/>' + '刪除',
                    handler: function() {
                        var mainObj = this;
                        Ext.Msg.show({
                            title: '刪除i工作排程操作',
                            msg: '確定執行刪除動作?',
                            buttons: Ext.Msg.YESNO,
                            fn: function(btn) {
                                if (btn == "yes") {
                                    WS.ScheduleAction.deleteSchedule(mainObj.up('window').uuid, function(data) {
                                        mainObj.up('window').hide();
                                    });
                                }
                            }
                        });
                    }
                }, {
                    type: 'button',
                    text: '<img src="' + SYSTEM_URL_ROOT + '/css/images/leave.png" style="width:20px;height:16px;vertical-align:middle;margin-right:5px;"/>' + '關閉',
                    handler: function() {
                        Ext.getCmp('ExtScheduleForm').hide();
                    }
                }]
            })]
            me.callParent(arguments);
        },
        closeEvent: function() {
            this.fireEvent('closeEvent', this);
        },
        listeners: {
            'show': function() {
                Ext.getBody().mask();
                if (this.uuid != undefined) {
                    Ext.getCmp('Ext.ScheduleForm.Form').getForm().load({
                        params: {
                            'pUuid': this.uuid
                        },
                        success: function(response, jsonObj, b) {
                            if (jsonObj.result.data.IS_CYCLE == 'Y') {
                                this.down('#IS_CYCLE_Y').setValue(true);
                            } else {
                                this.down('#IS_CYCLE_N').setValue(true);
                            }

                            if (jsonObj.result.data.START_DATE.length > 0) {
                                var startDate = Date.parse(jsonObj.result.data.START_DATE);
                                startDate = new Date(startDate);
                                this.down('#START_DATE').setValue(startDate);
                            } else {
                                this.down('#START_DATE').setValue(new Date());
                            }

                            if (jsonObj.result.data.SCHEDULE_END_DATE.length > 0) {
                                var endDate = Date.parse(jsonObj.result.data.SCHEDULE_END_DATE);
                                endDate = new Date(endDate);
                                this.down('#SCHEDULE_END_DATE').setValue(endDate);
                            } else {
                                this.down('#SCHEDULE_END_DATE').setValue(new Date());
                            }

                            if (jsonObj.result.data.SINGLE_DATE.length > 0) {
                                var startDate = Date.parse(jsonObj.result.data.SINGLE_DATE);
                                startDate = new Date(startDate);
                                this.down('#dtSingleDate').setValue(startDate);

                                this.down('#cmbSingleHour').setValue(jsonObj.result.data.HOUR);
                                this.down('#cmbSingleMinute').setValue(jsonObj.result.data.MINUTE);
                            }

                            if (jsonObj.result.data.CYCLE_TYPE == '每天重複') {
                                this.down("#txtCycleType_Org").setValue("每天重複");
                                var obj = this.down('#tabDay');
                                this.down('#tpCycle').setActiveTab(obj);

                            } else if (jsonObj.result.data.CYCLE_TYPE == '每週重複') {
                                this.down("#txtCycleType_Org").setValue("每週重複");
                                var obj = this.down('#tabWeek');
                                this.down('#tpCycle').setActiveTab(obj);
                                var hasCheck = jsonObj.result.data.C_DAY_OF_WEEK.split(',');
                                for (var tmp in hasCheck) {
                                    if (typeof(hasCheck[tmp] == 'string')) {
                                        if (hasCheck[tmp] == '0') {
                                            this.down("#chk_day_week_0").setValue(true);
                                        } else if (hasCheck[tmp] == '1') {
                                            this.down("#chk_day_week_1").setValue(true);
                                        } else if (hasCheck[tmp] == '2') {
                                            this.down("#chk_day_week_2").setValue(true);
                                        } else if (hasCheck[tmp] == '3') {
                                            this.down("#chk_day_week_3").setValue(true);
                                        } else if (hasCheck[tmp] == '4') {
                                            this.down("#chk_day_week_4").setValue(true);
                                        } else if (hasCheck[tmp] == '5') {
                                            this.down("#chk_day_week_5").setValue(true);
                                        } else if (hasCheck[tmp] == '6') {
                                            this.down("#chk_day_week_6").setValue(true);
                                        }
                                    }
                                }
                            } else if (jsonObj.result.data.CYCLE_TYPE == '每月重複') {
                                this.down("#txtCycleType_Org").setValue("每月重複");
                                var obj = this.down('#tabMonth');
                                this.down('#tpCycle').setActiveTab(obj);

                                var hasCheck = jsonObj.result.data.C_DAY_OF_MONTH.split(',');
                                /*每月重複的以日為主*/
                                for (var tmp in hasCheck) {
                                    if (typeof(hasCheck[tmp]) == 'string') {
                                        if (hasCheck[tmp] == '')
                                            continue;
                                        var _script = 'this.down("#chk_day_month_' + hasCheck[tmp] + '").setValue(true);';
                                        eval(_script);
                                        this.down('#cmbMonthType').setValue('日為主');
                                        this.down('#plMonthWeek').setVisible(false);
                                        this.down('#plMonthDay').setVisible(true);
                                    }
                                }
                                var hasCheck = jsonObj.result.data.C_WEEK_OF_MONTH.split(',');
                                var isDayWeekInMonth = false;
                                /*每月重複的以週為主*/
                                for (var tmp in hasCheck) {
                                    if (typeof(hasCheck[tmp]) == 'string') {
                                        if (hasCheck[tmp] == '')
                                            continue;
                                        var _script = 'this.down("#chk_week_month_' + hasCheck[tmp] + '").setValue(true);';
                                        eval(_script);
                                        this.down('#cmbMonthType').setValue('週為主');
                                        this.down('#plMonthWeek').setVisible(true);
                                        this.down('#plMonthDay').setVisible(false);
                                        isDayWeekInMonth = true;
                                    }
                                }
                                if (isDayWeekInMonth) {
                                    var hasCheck = jsonObj.result.data.C_DAY_OF_WEEK.split(',');
                                    /*每月重複的以週為主的星期1-7*/
                                    for (var tmp in hasCheck) {
                                        if (typeof(hasCheck[tmp]) == 'string') {
                                            if (hasCheck[tmp] == '')
                                                continue;

                                            var _script = 'this.down("#chk_day_week_month_' + hasCheck[tmp] + '").setValue(true);';
                                            eval(_script);
                                        }
                                    }
                                }
                            } else if (jsonObj.result.data.CYCLE_TYPE == '每時重複') {
                                this.down("#txtCycleType_Org").setValue("每時重複");
                                var obj = this.down('#tabHour');
                                this.down('#tpCycle').setActiveTab(obj);
                            } else if (jsonObj.result.data.CYCLE_TYPE == '每分重複') {
                                this.down("#txtCycleType_Org").setValue("每分重複");
                                var obj = this.down('#tabMinute');
                                this.down('#tpCycle').setActiveTab(obj);
                            } else if (jsonObj.result.data.CYCLE_TYPE == '單次任務') {
                                var aDate = new Date(jsonObj.result.data.SINGLE_DATE);
                                var hour = aDate.getHours();
                                var minutes = aDate.getMinutes();
                                this.down("#cmbSingleHour").setValue(hour);
                                this.down("#cmbSingleMinute").setValue(minutes);
                            }
                        },
                        failure: function(response, jsonObj, b) {
                            if (!jsonObj.result.success) {
                                Ext.MessageBox.show({
                                    title: 'Warning',
                                    icon: Ext.MessageBox.WARNING,
                                    buttons: Ext.Msg.OK,
                                    msg: jsonObj.result.message
                                });
                            }

                        },
                        scope: this
                    });
                } else {
                    /*When 新增資料*/
                    Ext.getCmp('Ext.ScheduleForm.Form').getForm().reset();
                    /*顯示控制*/
                    this.down('#dtSingleDate').setVisible(false);
                    this.down('#cmbSingleHour').setVisible(false);
                    this.down('#cmbSingleMinute').setVisible(false);
                    this.down('#labSingleHour').setVisible(false);
                    this.down('#labSingleMinute').setVisible(false);
                    this.down('#tpCycle').setVisible(true);
                    this.down('#cmbHour').setVisible(true);
                    this.down('#cmbMinute').setVisible(true);
                    this.down('#labHour').setVisible(true);
                    this.down('#labMinute').setVisible(true);
                    /*驗証控制*/
                    this.down('#dtSingleDate').allowBlank = true;
                    this.down('#cmbSingleHour').allowBlank = true;
                    this.down('#cmbSingleMinute').allowBlank = true;
                    this.down('#cmbHour').allowBlank = false;
                    this.down('#cmbMinute').allowBlank = false;
                    this.down('#txtCWeek').allowBlank = true;
                    this.down('#txtCDay').allowBlank = false;
                    var obj = this.down('#tabDay');
                    this.down('#tpCycle').setActiveTab(obj);
                    this.down("#txtCycleType").setValue("每天重複");
                }
            },
            'hide': function() {
                Ext.getBody().unmask();
                this.closeEvent();
            }
        }
    });
});
