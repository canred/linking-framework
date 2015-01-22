Ext.Loader.setConfig({
    enabled: true
});

Ext.Loader.setPath('Ext.ux', SYSTEM_ROOT_PATH + '/js/ux');

Ext.require(['*', 'Ext.ux.DataTip']);

Ext.form.VTypes['vdoubleVal'] = /^[0-9]*(\.)?([0-9])*[0-9]$/;
Ext.form.VTypes['vdoubleMask'] = /[0-9.]/;
Ext.form.VTypes['vdoubleText'] = '請輸入大於0的數字';
Ext.form.VTypes['vdouble'] = function (v) {
    return Ext.form.VTypes['vdoubleVal'].test(v);
}
var AttendantQueryForm = undefined;
Ext.onReady(function () {
    /*:::加入Direct:::*/    
    Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION + ".OperationalBoundaryAction"));
    Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION + ".DeptAction"));
    Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION + ".CompanyAction"));
    Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION + ".AttendantAction"));
    Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION + ".IndustryAction"));
    Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION + ".AdministrativeAreaAction"));

    /*:::Calss Name:::*/
    Ext.define('DepartmentForm', {
        extend: 'Ext.window.Window',
        title: '部門 新增/修改',
        closeAction: 'hide',
        /*:::自訂查詢用的key欄位:::*/
        uuid: undefined,
        parentUuid: undefined,
        is_operational_boundary: undefined,
        companyUuid: undefined,
        createUuid: undefined,
        /*:::元件的ID在系統中是唯一的:::*/
        id: 'ExtDepartmentForm',
        width: $(window).width() * 0.9,
        height: $(window).height() * 0.9,
        layout: 'fit',
        resizable: false,
        draggable: true,
        autoFitErrors: true,
        /*展示錯誤信息時 是否自動調整字段組件的寬度*/
        /*定義事件的方法*/
        initComponent: function () {
            /*:::新增事件:::*/
            this.addEvents('closeEvent');
            var me = this;
            me.items = [Ext.create('Ext.form.Panel', {
                layout: {
                    type: 'form',
                    align: 'stretch'
                },
                api: {
                    /*:::讀取的Direct Url:::*/
                    load: WS.DeptAction.info,
                    submit: WS.DeptAction.submit,
                    destroy: WS.DeptAction.destroy
                },
                /*:::Panel的ID(唯一性):::*/
                id: 'Ext.DepartmentForm.Form',
                /*:::參數傳遞的順序:::*/
                paramOrder: ['pUuid'],
                border: true,
                bodyPadding: 5,
                defaultType: 'textfield',
                buttonAlign: 'center',
                autoScroll: true,
                items: [{
                        fieldLabel: '公司',
                        padding: 5,
                        allowBlank: false,
                        maxLength: 33,
                        id: 'Ext.DepartmentForm.Form.CompanyName'
                    }, { /*PK值，必須存在*/
                        xtype: 'hidden',
                        fieldLabel: 'COMPANY_UUID',
                        name: 'COMPANY_UUID',
                        id: 'Ext.DepartmentForm.Form.Company_uuid'
                    }, {
                        fieldLabel: '部門代碼',
                        name: 'ID',
                        padding: 5,
                        anchor: '50%',
                        allowBlank: false,
                        maxLength: 128
                    }, {
                        fieldLabel: '名稱-繁中',
                        name: 'C_NAME',
                        padding: 5,
                        anchor: '50%',
                        allowBlank: false,
                        maxLength: 128
                    }, {
                        fieldLabel: '名稱-英文',
                        name: 'E_NAME',
                        padding: 5,
                        anchor: '50%',
                        allowBlank: false,
                        maxLength: 128
                    }, {
                        fieldLabel: '部門簡稱',
                        name: 'S_NAME',
                        padding: 5,
                        anchor: '50%',
                        allowBlank: true,
                        maxLength: 128
                    }, { /*是否為營運邊界由前面的畫面帶入不可修改，因此也不顯示*/
                        fieldLabel: '有效性',
                        xtype: 'combobox',
                        id: 'Ext.DepartmentForm.Form.IS_ACTIVE',
                        queryMode: 'local',
                        displayField: 'name',
                        valueField: 'value',
                        name: 'IS_ACTIVE',
                        padding: 5,
                        editable: false,
                        hidden: false,
                        store: {
                            xtype: 'store',
                            fields: ['name', 'value'],
                            data: [{
                                name: "是",
                                value: "Y"
                            }, {
                                name: "否",
                                value: "N"
                            }]
                        },
                        value: 'Y'
                    }                    
                    , {
                        xtype: 'container',
                        layout: 'hbox',
                        items: [{
                            xtype: 'textfield',
                            fieldLabel: '部門主管',
                            name: '_MARGER_ID',
                            allowBlank: true,
                            readOnly: true,
                            flex: 1,
                        }, {
                            xtype: 'button',
                            style: 'margin-right:75px;margin-left:2px;',
                            text: '<img src="../../css/custImages/mouse_select_left.gif" height="15"  style="vertical-align:middle">',
                            handler: function () {

                                var companyUuid = Ext.getCmp('ExtDepartmentForm').companyUuid;
                                if (companyUuid == undefined) {
                                    Ext.MessageBox.show({
                                        title: '挑選人員視窗',
                                        msg: '系統偵測時，發現公司資訊未設定!',
                                        icon: Ext.MessageBox.WARNING,
                                        buttons: Ext.Msg.OK
                                    });
                                    return;
                                }
                                if (AttendantQueryForm == undefined) {
                                    AttendantQueryForm = Ext.create('AttendantPicker', {
                                        'iconSelectUrl': '../../css/custImages/mouse_select_left.gif'
                                    });
                                    AttendantQueryForm.on('selectedEvent', function (record) {
                                        /*更新form中的「系統操作人員」資訊*/
                                        Ext.getCmp('Ext.DepartmentForm.Form').getForm().findField("MANAGER_UUID").setValue(record.data["UUID"]);
                                        /*更新form中的「系統操作人員」名稱資訊*/
                                        Ext.getCmp('Ext.DepartmentForm.Form').getForm().findField("_MARGER_ID").setValue(record.data["C_NAME"]);
                                        Ext.getCmp('Ext.DepartmentForm.Form').unmask();
                                        AttendantQueryForm.hide();
                                    });

                                    AttendantQueryForm.on('closeEvent', function (record) {
                                        Ext.getBody().mask();
                                        Ext.getCmp('Ext.DepartmentForm.Form').unmask();
                                        AttendantQueryForm.hide();
                                    });
                                }
                                AttendantQueryForm.companyUuid = companyUuid;
                                Ext.getCmp('Ext.DepartmentForm.Form').mask();
                                AttendantQueryForm.show();
                            }

                        }]
                    }, {
                        xtype: 'hidden',
                        fieldLabel: 'MANAGER_UUID',
                        name: 'MANAGER_UUID'
                    }, {
                        fieldLabel: '隸屬部門',
                        id: 'Ext.DepartmentForm.Form.PARENT_DEPARTMENT_NAME',
                        padding: 5,
                        allowBlank: true,
                        readOnly: true
                    }, {
                        xtype: 'hidden',
                        fieldLabel: 'PARENT_DEPARTMENT_UUID',
                        name: 'PARENT_DEPARTMENT_UUID',
                        id: 'Ext.DepartmentForm.Form.PARENT_DEPARTMENT_UUID'
                    }, { /*PK值，必須存在*/
                        xtype: 'hidden',
                        fieldLabel: 'UUID',
                        name: 'UUID'
                    }
                ],
                fbar: [{
                    type: 'button',
                    text: '儲存',
                    handler: function () {
                        var form = Ext.getCmp('Ext.DepartmentForm.Form').getForm();
                        if (form.isValid() == false) {
                            return;
                        }

                        form.submit({
                            waitMsg: '更新中...',
                            success: function (form, action) {
                                Ext.MessageBox.show({
                                    title: '維護部門',
                                    msg: '操作完成',
                                    icon: Ext.MessageBox.INFO,
                                    buttons: Ext.Msg.OK
                                });
                            },
                            failure: function (form, action) {
                                Ext.MessageBox.show({
                                    title: 'System Error',
                                    msg: action.result.message,
                                    icon: Ext.MessageBox.ERROR,
                                    buttons: Ext.Msg.OK
                                });
                            }
                        });
                    }
                }, {
                    type: 'button',
                    text: '刪除',
                    id: 'ExtDepartmentForm.delete',
                    handler: function () {
                        var form = Ext.getCmp('Ext.DepartmentForm.Form').getForm();
                        /*:::變更Submit的方向:::*/
                        form.api.submit = WS.DeptAction.destroy;
                        form.submit({
                            params: {
                                requestAction: 'delete'
                            },
                            waitMsg: '刪除中...',
                            success: function (form, action) {
                                Ext.MessageBox.show({
                                    title: '維護部門/營運邊界',
                                    msg: '刪除完成',
                                    icon: Ext.MessageBox.INFO,
                                    buttons: Ext.Msg.OK,
                                    fn: function () {
                                        /*:::變更Submit的方向:::*/
                                        form.api.submit = WS.DeptAction.submit;
                                        Ext.getCmp('ExtDepartmentForm').hide();
                                    }
                                });
                            },
                            failure: function (form, action) {
                                Ext.MessageBox.show({
                                    title: 'System Error',
                                    msg: action.result.message,
                                    icon: Ext.MessageBox.ERROR,
                                    buttons: Ext.Msg.OK
                                });
                            }
                        });
                    }
                }, {
                    type: 'button',
                    text: '關閉',
                    handler: function () {
                        Ext.getCmp('ExtDepartmentForm').hide();
                    }
                }]
            })]
            me.callParent(arguments);
        },
        closeEvent: function () {
            this.fireEvent('closeEvent', this);
        },
        listeners: {

            'show': function () {
                /*:::畫面開啟後載入資料:::*/
                myMask = new Ext.LoadMask(
                    Ext.getCmp('Ext.DepartmentForm.Form'), {
                        msg: "資料載入中，請稍等...",
                        removeMask: true
                    }); 
                myMask.show();

                /*this.uuid 是 undefined 的話；表示執行新增的動作*/
                if (this.uuid != undefined) {
                    
                    /*When 編輯/刪除資料*/
                    Ext.getCmp('ExtDepartmentForm.delete').setDisabled(false);

                    Ext.getCmp('Ext.DepartmentForm.Form').getForm().load({
                        params: {
                            'pUuid': Ext.getCmp('ExtDepartmentForm').uuid
                        },
                        success: function (response, jsonObj, b) {
                            /*取得公司資訊*/
                            WS.CompanyAction.getCompany(Ext.getCmp('ExtDepartmentForm').companyUuid, function (RESULT, b, c) {
                                try {
                                    if (RESULT.data.length > 0) {
                                        Ext.getCmp('Ext.DepartmentForm.Form.CompanyName').setValue(RESULT.data[0].C_NAME);
                                    }
                                } catch (ex) {
                                    Ext.MessageBox.show({
                                        title: '維護部門',
                                        msg: '操作發生異常錯誤，無法取得公司基本資訊!',
                                        icon: Ext.MessageBox.ERROR,
                                        buttons: Ext.Msg.OK
                                    });
                                }
                            });

                            /*取得部門主管人員資訊*/
                           WS.AttendantAction.getUserName(jsonObj.result.data.MANAGER_UUID, function (JSONDATA) {
                                Ext.getCmp('Ext.DepartmentForm.Form').getForm().findField("_MARGER_ID").setValue(JSONDATA);                                
                            });

                            /*取得父部門資訊*/
                            if (jsonObj.result.data.PARENT_DEPARTMENT_UUID.length > 0) {
                                DeptAction.info(jsonObj.result.data.PARENT_DEPARTMENT_UUID, function (JSONDATA) {
                                    Ext.getCmp('Ext.DepartmentForm.Form.PARENT_DEPARTMENT_NAME').setValue(JSONDATA.data.C_NAME);
                                    myMask.hide();
                                });
                            }
                        },
                        failure: function (response, a, b) {
                            myMask.hide();
                            r = Ext.decode(response.responseText);
                            alert('err:' + r);
                        }
                    });
                } else {
                    /*When 新增資料*/
                    /*取得公司資訊*/
                    Ext.getCmp('Ext.DepartmentForm.Form.Company_uuid').setValue(this.companyUuid);

                    WS.CompanyAction.getCompany(this.companyUuid, function (RESULT, b, c) {
                        try {
                            if (RESULT.data.length > 0) {
                                Ext.getCmp('Ext.DepartmentForm.Form.CompanyName').setValue(RESULT.data[0].C_NAME);
                            }
                        } catch (ex) {
                            Ext.MessageBox.show({
                                title: '維護部門',
                                msg: '操作發生異常錯誤，無法取得公司基本資訊!',
                                icon: Ext.MessageBox.ERROR,
                                buttons: Ext.Msg.OK
                            });
                        }
                    });
                    Ext.getCmp('ExtDepartmentForm.delete').setDisabled(true);
                    
                    /*由前一頁帶進的資訊*/
                    var _parentUuid = Ext.getCmp('ExtDepartmentForm').parentUuid;
                    Ext.getCmp('Ext.DepartmentForm.Form.PARENT_DEPARTMENT_UUID').setValue(_parentUuid);
                    /*取得父部門資訊*/
                    WS.DeptAction.info(_parentUuid, function (JSONDATA) {
                        Ext.getCmp('Ext.DepartmentForm.Form.PARENT_DEPARTMENT_NAME').setValue(JSONDATA.data.C_NAME);
                        myMask.hide();
                    });
                }
            },
            'hide': function () {
                Ext.getBody().unmask();
                this.closeEvent();
            }
        }
    });
});