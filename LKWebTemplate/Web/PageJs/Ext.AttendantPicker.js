Ext.Loader.setConfig({
    enabled: true
});
Ext.Loader.setPath('Ext.ux', SYSTEM_ROOT_PATH + '/js/ux');
Ext.require(['*', 'Ext.ux.DataTip']);
Ext.MessageBox.buttonText.yes = "確定";
Ext.MessageBox.buttonText.no = "取消";
Ext.onReady(function () {
    /*:::加入Direct:::*/
    Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION + ".AttendantAction"));
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
                    read: WS.AttendantAction.load
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
                        Ext.MessageBox.show({
                            title: 'REMOTE EXCEPTION',
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
                load: function () {}
            },
            remoteSort: true,
            sorters: [{
                property: 'C_NAME',
                direction: 'ASC'
            }]
        });

    /*:::Calss Name:::*/
    Ext.define('AttendantPicker', {
        extend: 'Ext.window.Window',        
        title: '<img src="' + SYSTEM_URL_ROOT + '/css/images/man.png" style="height:16px;vertical-align:middle;margin-top:-2px;margin-right:5px;">挑選人員',
        closeAction: 'hide',
        /*:::自訂查詢用的key欄位:::*/
        uuid: undefined,
        /*:::元件的ID在系統中是唯一的:::*/
        id: 'ExtAttendantPicker',
        /*選擇圖片的位置*/
        iconSelectUrl: SYSTEM_URL_ROOT+'/css/custImages/mouse_select_left.gif',
        companyUuid: undefined,
        width: 750,
        height: 450,
        layout: 'fit',
        resizable: false,
        draggable: true,
        /*定義事件的方法*/
        initComponent: function () {
            /*:::新增事件:::*/
            this.addEvents('closeEvent');
            this.addEvents('selectedEvent');

            var me = this;
            me.items = [Ext.create('Ext.form.Panel', {
                layout: {
                    type: 'form',
                    align: 'stretch'
                },                
                /*:::Panel的ID(唯一性):::*/
                id: 'Ext.AttendantPicker.Form',
                /*:::參數傳遞的順序:::*/
                paramOrder: ['pUuid'],
                border: true,
                bodyPadding: 5,
                defaultType: 'textfield',
                buttonAlign: 'center',
                items: [
                    /*:::你要顯示的欄位資訊:::*/
                    {
                        xtype: 'container',
                        layout: 'hbox',
                        items: [{
                            xtype: 'textfield',
                            fieldLabel: '關鍵字',
                            id: 'Ext.AttendantPicker.keyWord',
                            labelAlign: 'right',
                            enableKeyEvents:true,
                            listeners:{
                                keyup:function(e,t,eOpts){
                                    if(t.button==12){
                                        this.up('panel').down('#btnQuery').handler();
                                    }
                                }
                            }
                        }, {
                            xtype: 'label',
                            text: '',
                            style: 'display:block; padding:4px 4px 4px 4px'
                        }, {
                            xtype: 'button',
                            text: '<img src="' + SYSTEM_URL_ROOT + '/css/custimages/search.gif" style="height:16px;vertical-align:middle;margin-top:-2px;margin-right:5px;">查詢',
                            itemId:'btnQuery',
                            handler: function () {
                                storeAttendant.getProxy().setExtraParam('company_uuid', Ext.getCmp('ExtAttendantPicker').companyUuid);
                                storeAttendant.getProxy().setExtraParam('keyword', Ext.getCmp('Ext.AttendantPicker.keyWord').getValue());
                                storeAttendant.load();
                            }
                        }]
                    },

                    {
                        xtype: 'gridpanel',
                        store: storeAttendant,
                        idProperty: 'UUID',
                        paramsAsHash: false,
                        padding: 5,
                        columns: [{
                            text: "挑選",
                            dataIndex: 'UUID',
                            align: 'center',
                            renderer: function (value, m, r) {
                                var id = Ext.id();
                                Ext.defer(function () {
                                    Ext.widget('button', {
                                        renderTo: id,
                                        text: '<img src="' + Ext.getCmp('ExtAttendantPicker').iconSelectUrl + '" style="height:16px;vertical-align:middle">&nbsp;選擇',
                                        record: r,
                                        width: 75,
                                        handler: function () {

                                            
                                            Ext.getCmp('ExtAttendantPicker').selectedEvent(this.record);

                                        }
                                    });
                                }, 50);
                                return Ext.String.format('<div id="{0}"></div>', id);
                            },
                            sortable: false,
                            hideable: false
                        }, {
                            text: "中文姓名",
                            dataIndex: 'C_NAME',
                            align: 'center',
                            flex: 1
                        }, {
                            text: "英文姓名",
                            dataIndex: 'E_NAME',
                            align: 'center',
                            flex: 1
                        }, {
                            text: "帳號",
                            dataIndex: 'ACCOUNT',
                            align: 'center',
                            flex: 2
                        }, {
                            text: "E-Mail",
                            dataIndex: 'EMAIL',
                            align: 'center',
                            flex: 2
                        }],
                        anchor: '95%',
                        height: 450,
                        bbar: Ext.create('Ext.toolbar.Paging', {
                            store: storeAttendant,
                            displayInfo: true,
                            displayMsg: '第{0}~{1}資料/共{2}筆',
                            emptyMsg: "無資料顯示"
                        }),
                        listeners: {
                            'beforerender': function () {}
                        },
                        tbarCfg: {
                            buttonAlign: 'right'
                        }
                    }
                ],
                fbar: [{
                    type: 'button',
                    text: '<img src="' + SYSTEM_URL_ROOT + '/css/images/leave.png" style="height:20px;vertical-align:middle;margin-top:-2px;margin-right:5px;">關閉',
                    handler: function () {
                        Ext.getCmp('ExtAttendantPicker').hide();
                    }
                }]
            })]
            me.callParent(arguments);
        },
        closeEvent: function () {
            this.fireEvent('closeEvent', this);
        },
        selectedEvent: function (result) {
            this.fireEvent('selectedEvent', result);
        },
        listeners: {
            'beforeshow': function () {
                /*:::畫面開啟後載入資料:::*/
                Ext.getBody().mask();
                storeAttendant.getProxy().setExtraParam('company_uuid', Ext.getCmp('ExtAttendantPicker').companyUuid);
                storeAttendant.getProxy().setExtraParam('keyword', Ext.getCmp('Ext.AttendantPicker.keyWord').getValue());
                storeAttendant.load();
            },
            'hide': function () {
                Ext.getBody().unmask();
                Ext.getCmp('Ext.AttendantPicker.keyWord').setValue('');
                this.closeEvent();
            }
        }
    });
});