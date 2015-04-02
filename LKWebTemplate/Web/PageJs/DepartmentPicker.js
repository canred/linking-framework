Ext.define('WS.DepartmentPicker', {
    extend: 'Ext.window.Window',
    closeAction: 'destroy',
    title: '挑選部門',
    icon: SYSTEM_URL_ROOT + '/css/images/organisation16x16.png',
    width: 600,
    height: 400,
	modal: true,
    autoScroll: true,
    /*語言擴展*/
    lan: {},
    /*參數擴展*/
    param: {
        companyUuid: undefined,
        parentObj: undefined
    },
    /*值擴展*/
    val: {},
    /*物件會用到的Store物件*/
    myStore: {
        tree: undefined
    },
    fnCallBackCloseEvent: function() {
        this.fnQuery(this);
    },
    fnQuery: function(obj) {
        /*obj要是主體*/
        if (obj !== undefined) {
            WS.DeptAction.loadTreeRoot(obj.param.companyUuid, function(data) {
                var data = data.data[0].UUID;
                this.myStore.tree.load({
                    params: {
                        UUID: data
                    },
                    callback: function() {
                        this.down('#treeDept').expandAll();
                    },
                    scope: this
                });
            }, obj);
        } else {
            alert('發生錯誤!');
        }
    },
    fnActiveRender: function(value, id, r) {
        var html = "<img src='" + SYSTEM_URL_ROOT;
        return value === "Y" ? html + "/css/custimages/active03.png'>" : html + "/css/custimages/unactive03.png'>";
    },
    initComponent: function() {
        this.myStore.tree = Ext.create('WS.DeptTreeStore', {});
        this.items = [{
            xtype: 'panel',
            frame: false,
            autoHeight: true,
            autoWidth: true,
            items: [{
                xtype: 'treepanel',
                itemId: 'treeDept',
                padding: 5,
                border: true,
                autoWidth: true,
                autoHeight: true,
                minHeight: $(document).height() - 230,
                store: this.myStore.tree,
                multiSelect: true,
                rootVisible: false,
                columns: [{
                    text: "選擇",
                    xtype: 'actioncolumn',
                    dataIndex: 'UUID',
                    align: 'center',
                    sortable: false,
                    flex: .5,
                    items: [{
                        tooltip: '*選擇',
                        icon: SYSTEM_URL_ROOT + '/css/images/mouseSelect16x16.png',
                        handler: function(grid, rowIndex, colIndex) {
                            var mainPanel = grid.up('window');
                            var gcategoryUuid = grid.getStore().getAt(rowIndex).data.UUID;
                            mainPanel.fireEvent('selected', mainPanel, grid.getStore().getAt(rowIndex).data);
                        }
                    }],
                    hideable: false
                }, {
                    xtype: 'treecolumn',
                    text: '部門組織',
                    flex: 3,
                    sortable: false,
                    dataIndex: 'C_NAME'
                }, {
                    text: '有效',
                    flex: .5,
                    dataIndex: 'IS_ACTIVE',
                    align: 'center',
                    sortable: false,
                    hidden: false,
                    renderer: this.fnActiveRender
                }]
            }]
        }];
        this.callParent(arguments);
    },
    listeners: {
        'show': function(obj, eOpts) {
            this.fnQuery(obj);
        }
    }
});
