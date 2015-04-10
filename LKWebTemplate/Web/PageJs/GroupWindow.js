/*TODO*/
/*
1.Model 要集中                                 [NO]
2.panel 的title要換成icon , title的方式        [YES]
3.add 的icon要換成icon , title的方式           [YES]
4. getCmp
5. tree 要改變一次顯示                         [YES]
6. 左右拉的功能未完成                          [YES]
7. 新增的測式未完成                            [YES]
   7.1  勾選功能失效                           [YES]
8. 刪除的測式未完成                            [YES]
9. 勾選測式                                    [YES]
10.有一些程式碼已經沒有在使用了哦，要刪除      [YES]
11.預設頁面的功能                              [YES]
12.icon & title                                [YES]
13.當是編輯的狀態要等到所有的資料都已經ready
   才可以開啟維護模式
*/
/*columns 使用default*/
Ext.define('WS.GroupWindow', {
    extend: 'Ext.window.Window',
    title: '權限維護',
    icon: SYSTEM_URL_ROOT + '/css/images/lock16x16.png',
    closeAction: 'destroy',
    border: false,
	modal: true,
    param: {
        uuid: undefined,
        companyUuid: undefined,
    },
    fnQuery: function() {
        var mainWin = this.up('window'),
            store = mainWin.myStore.attendantnotingroupattendant,
            proxy = store.getProxy();
        proxy.setExtraParam('group_head_uuid', mainWin.param.uuid);
        proxy.setExtraParam('keyword', mainWin.down('#txtSearch').getValue());
        proxy.setExtraParam('company_uuid', mainWin.param.companyUuid);
        store.loadPage(1);
    },
    myStore: {
        applicationheadheadv: Ext.create('Ext.data.Store', {
            successProperty: 'success',
            autoLoad: true,
            model: 'APPLICATIONHEADV',
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
                paramOrder: ['pKeyWord', 'page', 'limit', 'sort', 'dir'],
                extraParams: {
                    pKeyWord: ''
                },
                simpleSortMode: true,
                listeners: {
                    exception: function(proxy, response, operation) {
                        Ext.MessageBox.show({
                            title: 'Warning',
                            msg: response.result.message,
                            icon: Ext.MessageBox.WARNING,
                            buttons: Ext.Msg.OK
                        });
                    }
                }
            },
            remoteSort: true,
            sorters: [{
                property: 'NAME',
                direction: 'ASC'
            }]
        }),
        attendantnotingroupattendant: Ext.create('Ext.data.Store', {
            successProperty: 'success',
            autoLoad: false,
            model: 'ATTENDANT_V',
            pageSize: 9999,
            proxy: {
                type: 'direct',
                api: {
                    read: WS.GroupAttendantAction.loadAttendantStoreNotInGroup
                },
                reader: {
                    root: 'data'
                },
                paramsAsHash: true,
                paramOrder: ['company_uuid', 'group_head_uuid', 'keyword', 'is_active', 'page', 'limit', 'sort', 'dir'],
                extraParams: {
                    company_uuid: '',
                    group_head_uuid: '',
                    keyword: '',
                    is_active: 'Y'
                },
                simpleSortMode: true,
                listeners: {
                    exception: function(proxy, response, operation) {
                        Ext.MessageBox.show({
                            title: 'Warning',
                            msg: response.result.message,
                            icon: Ext.MessageBox.WARNING,
                            buttons: Ext.Msg.OK
                        });
                    }
                }
            },
            remoteSort: true,
            sorters: [{
                property: 'C_NAME',
                direction: 'ASC'
            }]
        }),
        attendantingroupattendant: Ext.create('Ext.data.Store', {
            successProperty: 'success',
            autoLoad: false,
            model: 'ATTENDANT_V',
            pageSize: 9999,
            proxy: {
                type: 'direct',
                api: {
                    read: WS.GroupAttendantAction.loadAttendantStoreInGroup
                },
                reader: {
                    root: 'data'
                },
                paramsAsHash: true,
                paramOrder: ['company_uuid', 'group_head_uuid', 'keyword', 'is_active', 'page', 'limit', 'sort', 'dir'],
                extraParams: {
                    company_uuid: '',
                    group_head_uuid: '',
                    keyword: '',
                    is_active: 'Y'
                },
                simpleSortMode: true,
                listeners: {
                    exception: function(proxy, response, operation) {
                        Ext.MessageBox.show({
                            title: 'Warning',
                            msg: response.result.message,
                            icon: Ext.MessageBox.WARNING,
                            buttons: Ext.Msg.OK
                        });
                    }
                }
            },
            remoteSort: true,
            sorters: [{
                property: 'C_NAME',
                direction: 'ASC'
            }]
        }),
        appmenutree: Ext.create('WS.AppMenuVTree', {})
    },
    fnSetParentsChecked: function(obj, checked) {
        obj.set('checked', checked);
        if (!obj.parentNode.data.root) {
            this.fnSetParentsChecked(obj.parentNode, checked);
        };
    },
    fnSetChildrenUnchecked: function(obj, checked) {
        obj.set('checked', checked);
        if (obj.childNodes.length > 0) {
            for (var i = 0; i < obj.childNodes.length; i++) {
                this.fnSetChildrenUnchecked(obj.childNodes[i], checked);
            }
        };
    },
    width: 1000,
    height: 700,
    maxWidth: 930,
    maxHeight: 550,
    resizable: false,
    draggable: true,
    autoScroll: true,
    initComponent: function() {
        this.items = [Ext.create('Ext.form.Panel', {
            api: {
                load: WS.GroupHeadAction.info,
                submit: WS.GroupHeadAction.submit
            },
            itemId: 'groupHeadForm',
            paramOrder: ['pUuid'],
            border: false,
            defaultType: 'textfield',
            buttonAlign: 'center',
            items: [{
                xtype: 'container',
                layout: 'hbox',
                margin: '5 0 0 0',
                items: [{
                    fieldLabel: '系統',
                    labelAlign: 'right',
                    xtype: 'combobox',
                    itemId: 'groupheafFormApplicationHead',
                    queryMode: 'local',
                    displayField: 'NAME',
                    valueField: 'UUID',
                    name: 'APPLICATION_HEAD_UUID',
                    anchor: '100%',
                    editable: false,
                    hidden: false,
                    store: this.myStore.applicationheadheadv
                }, {
                    xtype: 'textfield',
                    fieldLabel: '代碼',
                    labelAlign: 'right',
                    labelWidth: 100,
                    itemId: 'groupHeadId',
                    name: 'ID',
                    maxLength: 50
                }]
            }, {
                xtype: 'container',
                layout: 'hbox',
                margin: '5 0 5 0',
                defaults: {
                    anchor: '0 0',
                    labelWidth: 100,
                    labelAlign: 'right'
                },
                items: [{
                    xtype: 'textfield',
                    fieldLabel: '群組繁中',
                    name: 'NAME_ZH_TW',
                    maxLength: 100,
                    allowBlank: false
                }, {
                    xtype: 'textfield',
                    fieldLabel: '群組簡中',
                    name: 'NAME_ZH_CN',
                    maxLength: 100,
                    allowBlank: false
                }, {
                    xtype: 'textfield',
                    fieldLabel: '群組英文',
                    labelWidth: 100,
                    name: 'NAME_EN_US',
                    maxLength: 100
                }]
            }, {
                xtype: 'hiddenfield',
                fieldLabel: 'UUID',
                name: 'UUID',
                maxLength: 84,
                itemId: 'groupHeadFormUuid'
            }, {
                xtype: 'hiddenfield',
                fieldLabel: 'IS_ACTIVE',
                name: 'IS_ACTIVE',
                maxLength: 1,
                value: 'Y'
            }],
            fbar: [{
                type: 'button',
                icon: SYSTEM_URL_ROOT + '/css/custimages/save16x16.png',
                text: '儲存',
                handler: function() {
                    var mainWin = this.up('window');
                    var form = mainWin.down('#groupHeadForm').getForm();
                    if (form.isValid() == false) {
                        return;
                    };
                    if (Ext.isEmpty(mainWin.down('#groupHeadFormUuid').getValue())) {
                        mainWin.param.isNew = true;
                    } else {
                        mainWin.param.isNew = false;
                    };
                    form.submit({
                        waitMsg: '更新中...',
                        success: function(form, action) {
                            this.down('#groupheafFormApplicationHead').setDisabled(false);
                            this.down('#bnt_Query').setDisabled(false);
                            this.down('#bnt_Delete').setDisabled(false);
                            var mainWin = this;
                            if (!mainWin.param.isNew) {
                                Ext.MessageBox.show({
                                    title: '維護群組定義',
                                    msg: '操作完成',
                                    icon: Ext.MessageBox.INFO,
                                    buttons: Ext.Msg.OK,
                                    fn: function() {
                                        mainWin.down('#groupHeadFormUuid').setValue(action.result.UUID);
                                    }
                                });
                            } else {
                                Ext.MessageBox.show({
                                    title: '維護群組定義',
                                    msg: '新增操作完成，系統將自動關閉視窗,請由編輯功能進入再完成相關設定!',
                                    icon: Ext.MessageBox.INFO,
                                    buttons: Ext.Msg.OK,
                                    fn: function() {
                                        mainWin.close();
                                    }
                                });
                            };
                        },
                        failure: function(form, action) {
                            Ext.MessageBox.show({
                                title: '維護群組定義',
                                msg: action.result.message,
                                icon: Ext.MessageBox.ERROR,
                                buttons: Ext.Msg.OK
                            });
                        },
                        scope: mainWin
                    });
                }
            }, {
                itemId: 'bnt_Delete',
                type: 'button',
                icon: SYSTEM_URL_ROOT + '/css/images/delete16x16.png',
                text: '刪除',
                handler: function() {
                    var mainWin = this.up('window');
                    Ext.Msg.show({
                        title: '刪除節點操作',
                        msg: '確定執行刪除動作?',
                        buttons: Ext.Msg.YESNO,
                        fn: function(btn) {
                            if (btn == "yes") {
                                WS.GroupHeadAction.deleteGroupHead(mainWin.param.uuid, function(data) {
                                    this.close();
                                }, mainWin);
                            };
                        }
                    });
                }
            }, {
                type: 'button',
                icon: SYSTEM_URL_ROOT + '/css/custimages/exit16x16.png',
                text: '關閉',
                handler: function() {
                    this.up('window').close();
                }
            }]
        }), {
            xtype: 'tabpanel',
            itemId: 'tabMain',
            plain: true,
            padding: 10,
            border: false,
            maxWidth: 880,
            items: [{
                title: '權限維護',
                icon: SYSTEM_URL_ROOT + '/css/images/menu16x16.png',
                items: [{
                    itemId: 'AppMenuPanel',
                    xtype: 'panel',
                    frame: false,
                    autoHeight: true,
                    autoWidth: true,
                    companyUuid: undefined,
                    border: false,
                    items: [{
                        xtype: 'treepanel',
                        fieldLabel: '權限維護',
                        itemId: 'appMenuTree',
                        border: true,
                        autoWidth: true,
                        autoHeight: true,
                        autoLoad: false,
                        minHeight: 400,
                        store: this.myStore.appmenutree,
                        multiSelect: true,
                        rootVisible: false,
                        loadMask: true,
                        columns: {
                            defaults: {
                                align: 'left',
                                sortable: false
                            },
                            items: [{
                                text: '<center>UUID</center>',
                                flex: 2,
                                dataIndex: 'UID值',
                                hidden: true
                            }, {
                                xtype: 'treecolumn',
                                text: '名稱',
                                flex: 2,
                                dataIndex: 'NAME_ZH_TW'
                            }, {
                                text: '<center>行為模式</center>',
                                flex: 1,
                                dataIndex: 'ACTION_MODE',
                                sortable: false
                            }, {
                                text: "預設頁面",
                                dataIndex: 'DEFAULT_PAGE_CHECKED',
                                align: 'center',
                                width: 80,
                                xtype: 'checkcolumn',
                                listeners: {
                                    'checkchange': function(obj, rowIndex, checked) {
                                        var mainWin = this.up('window'),
                                            store = mainWin.myStore.appmenutree,
                                            record = store.getAt(rowIndex),
                                            pUuid = record.data.UUID,
                                            pIsDefaultPage = record.data.IS_DEFAULT_PAGE;
                                        WS.AuthorityAction.setGroupAppmenuIsDefaultPage(pUuid,
                                            mainWin.param.uuid, checked,
                                            function(data) {
                                                if (checked) {
                                                    record.set('checked', checked);
                                                };
                                            });
                                    }
                                },
                                sortable: false,
                                hideable: false
                            }, {
                                text: '<center>功能描述</center>',
                                flex: 1,
                                dataIndex: 'DESCRIPTION',
                                hidden: true
                            }, {
                                text: '<center>虛擬路徑</center>',
                                flex: 1,
                                dataIndex: 'URL'
                            }, {
                                text: '<center>參數</center>',
                                flex: 1,
                                dataIndex: 'PARAMETER_CLASS'
                            }]
                        },
                        listeners: {
                            checkchange: function(node, checked, eOpts) {
                                var mainWin = this.up('window'),
                                    pGroupHeadUuid = mainWin.param.uuid,
                                    pUuid = node.data.UUID;
                                if (node.data.checked == true) {
                                    WS.AuthorityAction.setGroupAppmenu(pUuid, pGroupHeadUuid, "Y", function(ret) {
                                        this.fnSetParentsChecked(node, node.data.checked);
                                    }, mainWin);
                                } else {
                                    WS.AuthorityAction.setGroupAppmenu(pUuid, pGroupHeadUuid, "N", function(ret) {
                                        this.fnSetChildrenUnchecked(node, node.data.checked);
                                        var _is_default_page = node.data.IS_DEFAULT_PAGE;
                                        if (_is_default_page && _default_page_checked) {
                                            var _tree = this.down('#appMenuTree');
                                            var _rowNumber = _tree.view.store.indexOf(node);
                                            Ext.get(Ext.get(_tree.view.all.elements[_rowNumber].childNodes[3].id)).checked = false;
                                        };
                                    }, mainWin);
                                };
                            }
                        }
                    }]
                }]
            }, {
                title: '使用者維護',
                icon: SYSTEM_URL_ROOT + '/css/images/manb16x16.png',
                border: true,
                items: [{
                    xtype: 'panel',
                    frame: false,
                    border: false,
                    padding: 5,
                    items: [{
                        xtype: 'fieldset',
                        title: '搜尋條件',
                        collapsible: true,
                        height: 60,
                        width: '100%',
                        border: true,
                        labelWidth: 60,
                        items: [{
                            xtype: 'container',
                            layout: 'hbox',
                            items: [{
                                xtype: "textfield",
                                name: "_txtSearch",
                                itemId: 'txtSearch',
                                fieldLabel: '關鍵字',
                                width: 200,
                                enableKeyEvents: true,
                                listeners: {
                                    keyup: function(e, t, eOpts) {
                                        var keyCode = t.keyCode;
                                        if (keyCode == Ext.event.Event.ENTER) {
                                            this.up('window').fnQuery.call(this.up('window').down('#bnt_Query'));
                                        };
                                    }
                                }
                            }, {
                                xtype: 'button',
                                itemId: 'bnt_Query',
                                margin: '0 0 0 10',
                                text: '查詢',
                                icon: SYSTEM_URL_ROOT + '/css/custimages/find.png',
                                width: 80,
                                listeners: {
                                    "click": this.fnQuery
                                }
                            }]
                        }]
                    }, {
                        xtype: 'panel',
                        border: false,
                        defaults: {
                            flex: 1
                        },
                        layout: {
                            type: 'hbox',
                            align: 'stretch'
                        },
                        height: 400,
                        items: [{
                            xtype: 'gridpanel',
                            multiSelect: true,
                            margin: '5 5 0 0',
                            border: true,
                            viewConfig: {
                                plugins: {
                                    ptype: 'gridviewdragdrop',
                                },
                                listeners: {
                                    drop: function(node, data, dropRec, dropPosition) {
                                        var mainWin = this.up('window'),
                                            dropOn = dropRec ? ' ' + dropPosition + ' ' + dropRec.get('account') : ' on empty view',
                                            attendant_uuid = data.records[0].get('UUID'),
                                            group_head_uuid = mainWin.param.uuid;
                                        WS.GroupAttendantAction.destroyBy(group_head_uuid, attendant_uuid, function(data) {});
                                    }
                                },
                            },
                            width: '50%',
                            store: this.myStore.attendantnotingroupattendant,
                            columns: [{
                                header: "名稱",
                                sortable: true,
                                width: '20%',
                                dataIndex: 'C_NAME'
                            }, {
                                header: "英文名稱",
                                sortable: true,
                                width: '20%',
                                dataIndex: 'E_NAME'
                            }, {

                                header: "帳號",
                                sortable: true,
                                width: '20%',
                                dataIndex: 'ACCOUNT'
                            }, {
                                header: "信箱",
                                sortable: true,
                                width: '28%',
                                dataIndex: 'EMAIL'
                            }],
                            enableDragDrop: true,
                            stripeRows: true,
                            title: '未選取人員'
                        }, {
                            xtype: 'gridpanel',
                            multiSelect: true,
                            border: true,
                            margin: '5 0 0 5',
                            viewConfig: {
                                plugins: {
                                    ptype: 'gridviewdragdrop',
                                },
                                listeners: {
                                    drop: function(node, data, dropRec, dropPosition) {
                                        var mainWin = this.up('window'),
                                            dropOn = dropRec ? ' ' + dropPosition + ' ' + dropRec.get('uuid') : ' on empty view',
                                            attendant_uuid = data.records[0].get('UUID'),
                                            group_head_uuid = mainWin.param.uuid;
                                        WS.GroupAttendantAction.addAttendnatGroupHead(group_head_uuid, attendant_uuid, function(data) {});
                                    }
                                }
                            },
                            width: '50%',
                            store: this.myStore.attendantingroupattendant,
                            columns: [{
                                header: "名稱",
                                sortable: true,
                                width: '20%',
                                dataIndex: 'C_NAME'
                            }, {
                                header: "英文名稱",
                                sortable: true,
                                width: '20%',
                                dataIndex: 'E_NAME'
                            }, {

                                header: "帳號",
                                width: '20%',
                                sortable: true,
                                dataIndex: 'ACCOUNT'
                            }, {
                                header: "信箱",
                                sortable: true,
                                width: '28%',
                                dataIndex: 'EMAIL'
                            }],
                            enableDragDrop: true,
                            stripeRows: true,
                            title: '已選取人員'
                        }]
                    }]
                }]
            }]
        }];
        this.callParent(arguments);
    },
    closeEvent: function() {
        this.fireEvent('closeEvent', this);
    },
    listeners: {
        'show': function() {            
            var mainWin = this;
            if (this.param.companyUuid == undefined) {
                WS.UserAction.getUserInfo(function(jsonObj) {
                    mainWin.param.companyUuid = jsonObj.COMPANY_UUID;
                    var store = mainWin.myStore.attendantingroupattendant,
                        proxy = store.getProxy();
                    proxy.setExtraParam('group_head_uuid', mainWin.param.uuid);
                    proxy.setExtraParam('company_uuid', mainWin.param.companyUuid);
                    store.loadPage(1);
                });
            };
            myMask = new Ext.LoadMask(mainWin.down('#AppMenuPanel'), {
                msg: "資料載入中，請稍等...",
                store: this.myStore.appmenutree
            });
            myMask.show();
            if (this.param.uuid != undefined) {
                /*When 編輯/刪除資料*/
                var queryUuid = this.param.uuid;
                mainWin.down('#groupHeadId').setDisabled(true);
                mainWin.down('#groupHeadForm').getForm().load({
                    params: {
                        'pUuid': this.param.uuid
                    },
                    success: function(response, jsonObj) {
                        WS.AuthorityAction.loadTreeRoot(
                            this.down('#groupheafFormApplicationHead').getValue(),
                            function(data) {
                                this.myStore.appmenutree.load({
                                    params: {
                                        UUID: data.UUID,
                                        GROUPHEADUUID: this.param.uuid
                                    },
                                    callback: function() {
                                        this.down('#appMenuTree').expandAll();
                                    },
                                    scope: this
                                });
                            }, this);
                    },
                    failure: function(response, jsonObj) {
                        r = Ext.decode(response.responseText);
                        alert('err:' + r);
                    },
                    scope: mainWin
                });
            } else {
                /*When 新增資料*/
                mainWin.down('#appMenuTree').getRootNode().removeAll();
                mainWin.down('#groupHeadId').setDisabled(false);
                mainWin.down('#groupheafFormApplicationHead').setDisabled(false);
                mainWin.down('#groupHeadForm').getForm().reset();
                mainWin.down('#bnt_Query').setDisabled(true);
                mainWin.down('#bnt_Delete').setDisabled(true);
                mainWin.down('#tabMain').setDisabled(true);
            };
        },
        'close': function() {
            this.closeEvent();
        }
    }
});
