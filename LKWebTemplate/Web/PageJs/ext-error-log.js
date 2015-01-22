var panel = undefined;
var panel_w = 1000;
var panel_h = 390;
Ext.onReady(function() {
    var sPath = window.location.pathname;
    var sPage = sPath.substring(sPath.lastIndexOf('/') + 1);
    if (IsProductionServer.toLowerCase() == "false") {
        getLog();
    }
});

function getLog() {
    Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION + ".ErrorLogAction"));
    Ext.QuickTips.init();
    var storeErrorLog = Ext.create('Ext.data.Store', {
        successProperty: 'success',
        autoLoad: false,
        model: Ext.define('ERROR_LOG', {
            extend: 'Ext.data.Model',
            fields: ['UUID', 'ERROR_CODE', 'ERROR_TIME',
                'ERROR_MESSAGE', 'APPLICATION_NAME', 'ATTENDANT_UUID',
                'ERROR_TYPE', 'IS_READ', 'CREATE_DATE', 'C_NAME'
            ]
        }),
        pageSize: 5,
        proxy: {
            type: 'direct',
            api: {
                read: WS.ErrorLogAction.load
            },
            reader: {
                root: 'data'
            },
            paramsAsHash: true,
            paramOrder: ['is_read', 'page', 'limit', 'sort', 'dir'],
            extraParams: {
                is_read: 'N'
            },
            simpleSortMode: true,
            listeners: {
                exception: function(proxy, response, operation) {
                    /*
                    Ext.MessageBox.show({
                        title: 'REMOTE EXCEPTION',
                        msg: operation.getError(),
                        icon: Ext.MessageBox.ERROR,
                        buttons: Ext.Msg.OK
                    });
                    */
                }
            }
        },
        sorters: [{
            property: 'CREATE_DATE',
            direction: 'DESC'
        }]
    });

    panel = Ext.widget({
        xtype: 'panel',
        id: 'panel',
        title: '錯誤訊息清單',
        padding: 5,
        height: panel_h,
        width: panel_w,
        buttonAlign: 'center',
        items: [{
            xtype: 'gridpanel',
            store: storeErrorLog,
            paramOrder: ['CREATE_DATE'],
            idProperty: 'UUID',
            paramsAsHash: false,
            padding: 5,
            autoScroll: true,
            columns: [{
                id: 'UUID',
                text: "編輯",
                dataIndex: 'UUID',
                align: 'center',
                renderer: function(value) {
                    var id = Ext.id();
                    Ext.defer(function() {
                        var url = SYSTEM_URL_ROOT + '/css/custimages/read.png';
                        Ext.widget('button', {
                            renderTo: id,
                            text: '<img src="' + url + '" style="height:16px;vertical-align:middle">&nbsp;已讀',
                            width: 75,
                            handler: function() {
                                WS.ErrorLogAction.UpdateRead(value, function(returnJs) {
                                    storeErrorLog.load({
                                        callback: function() {
                                            if (storeErrorLog.data.length == 0)
                                                closeBtn();
                                        }
                                    });
                                });
                            }
                        });
                    }, 50);
                    return Ext.String.format('<div id="{0}"></div>', id);
                },
                sortable: false,
                hideable: false
            }, {
                text: "時間",
                dataIndex: 'CREATE_DATE',
                align: 'left',
                flex: 2
            }, {
                text: "訊息",
                dataIndex: 'ERROR_MESSAGE',
                align: 'left',
                flex: 8,
                renderer: function(value) {
                    return '<div align="left">' + value + '</div>';
                }
            }, {
                text: "Action",
                dataIndex: 'APPLICATION_NAME',
                align: 'left',
                flex: 1
            }, {
                text: "執行者",
                dataIndex: 'C_NAME',
                align: 'left',
                flex: 1
            }],
            height: 270,
            bbar: Ext.create('Ext.toolbar.Paging', {
                store: storeErrorLog,
                displayInfo: true,
                displayMsg: '第{0}~{1}資料/共{2}筆',
                emptyMsg: "無資料顯示"
            }),
            listeners: {
                cellclick: function(iView, iCellEl, iColIdx, iRecord, iRowEl, iRowIdx, iEvent) {
                    if (iColIdx != 0) {
                        var colCount = iView.getGridColumns().length;
                        var msg = "";
                        for (var i = 1; i < colCount; i++) {
                            var message = iRecord.data[iView.getGridColumns()[i].dataIndex];
                            var field = iView.getGridColumns()[i].text;
                            if (i != colCount - 1)
                                msg += "[<b>" + field + "</b>] " + message + "<br/>";
                            else
                                msg += "[<b>" + field + "</b>] " + message;
                        }
                        var dialog = Ext.Msg.show({
                            title: "Row" + iRowIdx,
                            msg: msg,
                            minWidth: 800,
                            minHeight: 250,
                            icon: Ext.MessageBox.WARNING,
                            buttons: Ext.Msg.OK
                        });
                        dialog.setLocalXY((window.screen.availWidth - 800) / 2, 5);
                    }
                }
            }
        }],
        fbar: [{
            type: 'button',
            text: '關閉',
            handler: function() {
                closeBtn();
            }
        }],
        tbar: [{
            text: '<img src="' + SYSTEM_URL_ROOT + '/css/custimages/read.png' + '"/css/custimages/read.png" style="height:16px;vertical-align:middle">&nbsp;全部已閱讀',
            handler: function() {
                WS.ErrorLogAction.UpdateAllRead(function(returnJs) {
                    closeBtn();
                });
            }
        }]
    });

    storeErrorLog.load({
        callback: function() {
            if (storeErrorLog.data.length > 0)
                Ext.errorLog.msg(panel_h, panel_w, panel);
        }
    });
}

Ext.errorLog = function() {
    var msgCt;

    function createBox(t, s, obj) {
        return '<div class="msg"><h3>' + t + '</h3><p>' + s + '</p><input type="button" value="關閉" onclick="closeBtn();"/></div>';
    }
    return {
        msg: function(h, w, obj) {
            if (!msgCt) {
                msgCt = Ext.core.DomHelper.insertFirst(document.body, {
                    id: 'msg-div'
                }, true);
            }
            obj.render('msg-div');
            showBtn(msgCt, w, h);
        },

        init: function() {

        }
    };
}();

function closeBtn() {
    Ext.get(Ext.get(Ext.getDom('msg-div')).dom.children[0]).ghost('b');
}

function showBtn(msgCt, w, h) {
    msgCt.hide();
    msgCt.shift({
        x: window.screen.availWidth - 20 - w,
        y: window.screen.availHeight - 100 - h
    }).slideIn('b');
}

Array.prototype.contains = function(element) {
    for (var i = 0; i < this.length; i++) {
        if (this[i] == element) {
            return true;
        }
    }
    return false;
}

function getUrlHostNameAndProject() {
    var tmp = window.location.href.toString().split('/');
    var newUrl = '';
    for (var i = 0; i < 4; i++) {
        newUrl += tmp[i] + '/';
    }
    return newUrl;
}
