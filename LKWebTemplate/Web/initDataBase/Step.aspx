<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Step.aspx.cs" Inherits="LKWebTemplate.initDataBase.Step" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <script type="text/javascript" src='<%= Page.ResolveUrl("~/js/jquery-1.4.1.js")%>'></script>
    <script type="text/javascript" src='<%= Page.ResolveUrl("~/js/shared/include-ext.js")%>'></script>
    <script type="text/javascript" src='<%= Page.ResolveUrl("~/js/system-config.ashx")%>'></script>
    <script type="text/javascript" src='<%= Page.ResolveUrl("~/Proxy.ashx?init=start")%>'></script>
</head>
<body style="padding:10px;padding-top:10px">    
<script language="javascript" type="text/javascript">
Ext.onReady(function () {    
    Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION+".InitAction"));         
    Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION+".EncryptAction"));        
    Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION+".UtilAction"));        
    Ext.QuickTips.init();    
    var pDb = Ext.create('Ext.form.Panel', {
        layout: {
            type: 'form',
            align: 'stretch'
        },
        margin: 5,
        title: '<img src="<%= Page.ResolveUrl("~/css/images/database-basic.png")%>" style="width:20px;height:20px;vertical-align:middle">Setting "BASIC" DataBase',
        itemId: 'panelDb',
        border: false,
        bodyPadding: 5,
        margin: 5,
        frame: true,
        defaultType: 'textfield',
        buttonAlign: 'center',
        items: [
        {
            xtype: 'container',
            layout: 'hbox',
            items: [{
            xtype: 'combo',
            fieldLabel: '資料庫類型',
            queryMode: 'local',
            itemId: 'basicType',
            displayField: 'text',
            valueField: 'value',
            padding: 5,
            editable: false,
            hidden: false,
            store: new Ext.data.ArrayStore({
                fields: ['text', 'value'],
                data: [
                    ['Oracle', 'ORACLE'],
                    ['MSSQL', 'MSSQL'],
                    ['MySQL', 'MYSQL']
                ]
            })
        }
            ]
        },
        , {
            xtype: 'container',
            layout: 'hbox',
            margin:'5 0 0 0',
            items: [
            {
                xtype: 'textfield',
                fieldLabel: '連接字符串',
                itemId: 'basicConnection',
                allowBlank: false,
                readOnly: false,
                flex: 1
            }, {
                xtype:'button',
                text:'<img src="<%= Page.ResolveUrl("~/css/images/key.png")%>" style="width:16px;height:16px;vertical-align:middle;">',
                 margin: '0 5 0 5',
                    handler:function(handler,scope){
                    var strConnection = this.up('panel').down('#basicConnection').getValue();
                    Ext.MessageBox.confirm('加密確認', '執行加密字串?', function(result){
                        if(result=='yes'){
                            WS.EncryptAction.Encode(strConnection,function(objJson){                        
                                this.up('panel').down('#basicConnection').setValue(objJson.data[0].encrypt);                        
                            },this);
                    }
                    },this);
                }
            },{
                xtype: 'button',
                text: '<img src="<%= Page.ResolveUrl("~/css/images/connection.png")%>" style="width:16px;height:16px;vertical-align:middle;margin-right:5px;">測試連接',
                margin: '0 0 0 5',
                handler: function (handler, scope) {
                    var strConnection = this.up('panel').down('#basicConnection').getValue();
                    strConnection = Ext.util.Format.trim(strConnection);
                    Ext.getBody().mask();
                    Ext.MessageBox.confirm('Message', '測試前，將會自動儲存!請確認!', function (result) {
                        Ext.getBody().mask();
                        if (result == 'yes') {
                            this.saveAction(false, /*doCallBack*/ function () {
                                if (strConnection != '') {
                                    WS.InitAction.testConnection('BASIC', function (objJson) {

                                        if (objJson.success) {
                                            Ext.MessageBox.show({
                                                title: 'Message',
                                                icon: Ext.MessageBox.INFO,
                                                buttons: Ext.Msg.OK,
                                                msg: 'Basic Connection Success!'
                                            });
                                        } else {
                                            Ext.MessageBox.show({
                                                title: 'Message',
                                                icon: Ext.MessageBox.WARNING,
                                                buttons: Ext.Msg.OK,
                                                msg: 'Basic Connection Fail !'
                                            });
                                        }
                                        Ext.getBody().unmask();
                                    });
                                } else {
                                    Ext.MessageBox.show({
                                        title: 'Message',
                                        icon: Ext.MessageBox.INFO,
                                        buttons: Ext.Msg.OK,
                                        msg: '請正確輸入連接字符串文字!'
                                    });
                                }
                            });

                        }

                    }, this.up('panel'));


                }
            }]
        }],
        saveAction: function (popMsg, callBack) {
            popMsg = (popMsg == undefined ? true : false);
            var type = this.up("panel").down("#basicType").getValue();
            var connection = this.up("panel").down("#basicConnection").getValue();
            Ext.getBody().mask();
            WS.InitAction.submitBasic(type, connection, function (objJson) {
                if (objJson.success) {
                    if (popMsg) {
                        Ext.MessageBox.show({
                            title: 'Message',
                            icon: Ext.MessageBox.INFO,
                            buttons: Ext.Msg.OK,
                            msg: 'Save Completed!',
                            fn: function (btn) {
                                if (btn == 'ok') {
                                    Ext.getBody().unmask();
                                    callBack && callBack();
                                }
                            }
                        });
                    } else {
                        Ext.getBody().unmask();
                        callBack && callBack();
                    }
                } else {
                    Ext.MessageBox.show({
                        title: 'Warning',
                        icon: Ext.MessageBox.INFO,
                        buttons: Ext.Msg.OK,
                        msg: objJson.message,
                        fn: function (btn) {
                            if (btn == 'ok') {
                                Ext.getBody().unmask();
                            }
                        }
                    });
                }
            });
        },
        buttonAlign:'right',
        fbar: [{
            type: 'button',
            text: '<img src="<%= Page.ResolveUrl("~/css/custImages/save.png")%>" style="width:15px;height:15px;vertical-align:middle;margin-right:5px;"> Save Config',
            handler: function () {
                this.up('panel').saveAction();
            }
        }, {
            xtype: 'button',
            width:180,
            text: '<img src="<%= Page.ResolveUrl("~/css/images/work.png")%>" style="width:15px;height:15px;vertical-align:middle;"> 初始化資料庫(Basic)',
            handler: function (handler, scope) {
                //InitAction.timeout =  300000;
                Ext.getBody().mask('In process...');
                WS.InitAction.initBasic(function (objJson) {
                    Ext.getBody().unmask();
                    if (objJson.success) {
                        Ext.MessageBox.show({
                            title: 'Message',
                            icon: Ext.MessageBox.INFO,
                            buttons: Ext.Msg.OK,
                            msg: 'Table,View,Procude,Function is created!'
                        });
                    } else {
                        Ext.MessageBox.show({
                            title: 'Error',
                            icon: Ext.MessageBox.INFO,
                            buttons: Ext.Msg.OK,
                            msg: objJson.message
                        });
                    }
                });
            }
        }],
        listeners: {
            'afterrender': function () {
                Ext.getBody().mask('資料載入中');
                WS.InitAction.loadBasic(function (objJson) {
                    if(objJson ==null)
                        return; 
                    if (objJson.success) {
                        this.up("panel").down("#basicType").setValue(objJson.data[0].Type);
                        this.up("panel").down("#basicConnection").setValue(objJson.data[0].value);
                        Ext.getBody().unmask();
                    } else {
                        Ext.MessageBox.show({
                            title: '讀取錯誤',
                            icon: Ext.MessageBox.INFO,
                            buttons: Ext.Msg.OK,
                            msg: 'Basic Setting 讀取錯誤'
                        });
                    }
                }, this);
            }
        }
    });
    var pException = Ext.create('Ext.form.Panel', {
        layout: {
            type: 'form',
            align: 'stretch'
        },
        title: '<img src="<%= Page.ResolveUrl("~/css/images/database-exception.png")%>" style="width:20px;height:20px;vertical-align:middle">Setting "Exception" DataBase  ',
        itemId: 'panelException',
        border: false,
        bodyPadding: 5,
        margin: 5,
        frame: true,
        defaultType: 'textfield',
        buttonAlign: 'center',
        items: [
            {
                xtype : 'container',
                layout : 'hbox',
                items : [{
                xtype: 'combo',
                fieldLabel: '資料庫類型',
                queryMode: 'local',
                itemId: 'exceptionType',
                displayField: 'text',
                valueField: 'value',
                padding: 5,
                editable: false,
                hidden: false,
                store: new Ext.data.ArrayStore({
                    fields: ['text', 'value'],
                    data: [
                        ['Oracle', 'ORACLE'],
                        ['MSSQL', 'MSSQL'],
                        ['MySQL', 'MYSQL']
                    ]
                })
            }]
            },
             {
                xtype: 'container',
                layout: 'hbox',
                margin:'5 0 0 0',
                items: [{
                    xtype: 'textfield',
                    fieldLabel: '連接字符串',
                    itemId: 'exceptionConnection',
                    allowBlank: false,
                    readOnly: false,
                    flex: 1
                }, {
                xtype:'button',
                text:'<img src="<%= Page.ResolveUrl("~/css/images/key.png")%>" style="width:16px;height:16px;vertical-align:middle;">',
                 margin: '0 5 0 5',
                    handler:function(handler,scope){
                    var strConnection = this.up('panel').down('#exceptionConnection').getValue();
                    Ext.MessageBox.confirm('加密確認', '執行加密字串?', function(result){
                        if(result=='yes'){
                            WS.EncryptAction.Encode(strConnection,function(objJson){                        
                                this.up('panel').down('#exceptionConnection').setValue(objJson.data[0].encrypt);                        
                            },this);
                    }
                    },this);
                }
            }, {
                    xtype: 'button',
                    text: '<img src="<%= Page.ResolveUrl("~/css/images/connection.png")%>" style="width:16px;height:16px;vertical-align:middle;margin-right:5px;">測試連接',
                    margin: '0 0 0 5',
                    handler: function (handler, scope) {
                        var strConnection = this.up('panel').down('#exceptionConnection').getValue();
                        strConnection = Ext.util.Format.trim(strConnection);

                        Ext.MessageBox.confirm('Message', '測試前，將會自動儲存!請確認!', function (result) {
                            if (result == 'yes') {
                                this.saveAction(false, /*doCallBack*/ function () {
                                    if (strConnection != '') {
                                        WS.InitAction.testConnection('EXCEPTION', function (objJson) {

                                            if (objJson.success) {
                                                Ext.MessageBox.show({
                                                    title: 'Message',
                                                    icon: Ext.MessageBox.INFO,
                                                    buttons: Ext.Msg.OK,
                                                    msg: 'Exception Connection Success!'
                                                });
                                            } else {
                                                Ext.MessageBox.show({
                                                    title: 'Message',
                                                    icon: Ext.MessageBox.WARNING,
                                                    buttons: Ext.Msg.OK,
                                                    msg: 'Exception Connection Fail !'
                                                });
                                            }
                                        });
                                    } else {
                                        Ext.MessageBox.show({
                                            title: 'Message',
                                            icon: Ext.MessageBox.INFO,
                                            buttons: Ext.Msg.OK,
                                            msg: '請正確輸入連接字符串文字!'
                                        });
                                    }
                                });

                            }

                        }, this.up('panel'));


                    }
                }]
            }

        ],
        saveAction: function (popMsg, callBack) {
            popMsg = (popMsg == undefined ? true : false);
            var type = this.up("panel").down("#exceptionType").getValue();
            var connection = this.up("panel").down("#exceptionConnection").getValue();

            WS.InitAction.submitException(type, connection, function (objJson) {

                if (objJson.success) {
                    if (popMsg) {
                        Ext.MessageBox.show({
                            title: 'Message',
                            icon: Ext.MessageBox.INFO,
                            buttons: Ext.Msg.OK,
                            msg: 'Save Completed!',
                            fn: function (btn) {
                                if (btn == 'ok') {
                                    Ext.getBody().unmask();
                                    callBack && callBack();
                                }
                            }
                        });
                    } else {
                        Ext.getBody().unmask();
                        callBack && callBack();
                    }
                } else {
                    Ext.MessageBox.show({
                        title: 'Warning',
                        icon: Ext.MessageBox.INFO,
                        buttons: Ext.Msg.OK,
                        msg: objJson.message,
                        fn: function (btn) {
                            if (btn == 'ok') {
                                Ext.getBody().unmask();
                            }
                        }
                    });
                }
            });

        },
         buttonAlign:'right',
        fbar: [{
            type: 'button',
            text: '<img src="<%= Page.ResolveUrl("~/css/custImages/save.png")%>" style="width:15px;height:15px;vertical-align:middle;margin-right:5px;"> Save Config',
            handler: function () {

                this.up('panel').saveAction();
            }
        }, {
            xtype: 'button',
              width:180,
            text: '<img src="<%= Page.ResolveUrl("~/css/images/work.png")%>" style="width:15px;height:15px;vertical-align:middle;"> 初始化資料庫(Exception)',
            handler: function (handler, scope) {
                Ext.getBody().mask('In process...');
                WS.InitAction.initException(function (objJson) {
                    Ext.getBody().unmask();
                    if (objJson.success) {
                        Ext.MessageBox.show({
                            title: 'Message',
                            icon: Ext.MessageBox.INFO,
                            buttons: Ext.Msg.OK,
                            msg: 'Table,View is created!'
                        });
                    } else {
                        Ext.MessageBox.show({
                            title: 'Error',
                            icon: Ext.MessageBox.INFO,
                            buttons: Ext.Msg.OK,
                            msg: objJson.message
                        });
                    }
                });
            }
        }],
        listeners: {
            'afterrender': function () {
                WS.InitAction.loadException(function (objJson) {
                    
                    if (objJson!=undefined && objJson.success) {
                        this.up("panel").down("#exceptionType").setValue(objJson.data[0].Type);
                        this.up("panel").down("#exceptionConnection").setValue(objJson.data[0].value);
                    } else {
                        Ext.MessageBox.show({
                            title: '讀取錯誤',
                            icon: Ext.MessageBox.INFO,
                            buttons: Ext.Msg.OK,
                            msg: 'Exception Setting 讀取錯誤'
                        });
                    }
                }, this);
            }
        }
    });

    var pAction = Ext.create('Ext.form.Panel', {
        layout: {
            type: 'form',
            align: 'stretch'
        },
        title: '<img src="<%= Page.ResolveUrl("~/css/images/database-action.png")%>" style="width:20px;height:20px;vertical-align:middle">Setting "Action" DataBase',
        itemId: 'panelAction',
        border: false,
        bodyPadding: 5,
        margin: 5,
        frame: true,
        defaultType: 'textfield',
        buttonAlign: 'center',
        items: [
            {
                xtype : 'container',
                layout : 'hbox',
                items : [{
                xtype: 'combo',
                fieldLabel: '資料庫類型',
                queryMode: 'local',
                itemId: 'actionType',
                displayField: 'text',
                valueField: 'value',
                padding: 5,
                editable: false,
                hidden: false,
                store: new Ext.data.ArrayStore({
                    fields: ['text', 'value'],
                    data: [
                        ['Oracle', 'ORACLE'],
                        ['MSSQL', 'MSSQL'],
                        ['MySQL', 'MYSQL']
                    ]
                })
            }]
            },
             {
                xtype: 'container',
                layout: 'hbox',
                margin:'5 0 0 0',
                items: [{
                    xtype: 'textfield',
                    fieldLabel: '連接字符串',
                    itemId: 'actionConnection',
                    allowBlank: false,
                    readOnly: false,
                    flex: 1
                }
                , {
                xtype:'button',
                text:'<img src="<%= Page.ResolveUrl("~/css/images/key.png")%>" style="width:16px;height:16px;vertical-align:middle;">',
                 margin: '0 5 0 5',
                    handler:function(handler,scope){
                    var strConnection = this.up('panel').down('#actionConnection').getValue();
                    Ext.MessageBox.confirm('加密確認', '執行加密字串?', function(result){
                        if(result=='yes'){
                            WS.EncryptAction.Encode(strConnection,function(objJson){                        
                                this.up('panel').down('#actionConnection').setValue(objJson.data[0].encrypt);                        
                            },this);
                    }
                    },this);
                }
            }, {
                    xtype: 'button',
                    text: '<img src="<%= Page.ResolveUrl("~/css/images/connection.png")%>" style="width:16px;height:16px;vertical-align:middle;margin-right:5px;">測試連接',
                    margin: '0 0 0 5',
                    handler: function (handler, scope) {

                        var strConnection = this.up('panel').down('#actionConnection').getValue();
                        strConnection = Ext.util.Format.trim(strConnection);

                        Ext.MessageBox.confirm('Message', '測試前，將會自動儲存!請確認!', function (result) {
                            if (result == 'yes') {
                                this.saveAction(false, /*doCallBack*/ function () {
                                    if (strConnection != '') {
                                        WS.InitAction.testConnection('ACTION', function (objJson) {

                                            if (objJson.success) {
                                                Ext.MessageBox.show({
                                                    title: 'Message',
                                                    icon: Ext.MessageBox.INFO,
                                                    buttons: Ext.Msg.OK,
                                                    msg: 'Action Connection Success!'
                                                });
                                            } else {
                                                Ext.MessageBox.show({
                                                    title: 'Message',
                                                    icon: Ext.MessageBox.WARNING,
                                                    buttons: Ext.Msg.OK,
                                                    msg: 'Action Connection Fail !'
                                                });
                                            }
                                        });
                                    } else {
                                        Ext.MessageBox.show({
                                            title: 'Message',
                                            icon: Ext.MessageBox.INFO,
                                            buttons: Ext.Msg.OK,
                                            msg: '請正確輸入連接字符串文字!'
                                        });
                                    }
                                });

                            }

                        }, this.up('panel'));

                    }

                }]
            }

        ],
        saveAction: function (popMsg, callBack) {
            popMsg = (popMsg == undefined ? true : false);
            var type = this.up("panel").down("#actionType").getValue();
            var connection = this.up("panel").down("#actionConnection").getValue();

            WS.InitAction.submitActionLog(type, connection, function (objJson) {
                if (objJson.success) {
                    if (popMsg) {
                        Ext.MessageBox.show({
                            title: 'Message',
                            icon: Ext.MessageBox.INFO,
                            buttons: Ext.Msg.OK,
                            msg: 'Save Completed!',
                            fn: function (btn) {
                                if (btn == 'ok') {
                                    Ext.getBody().unmask();
                                    callBack && callBack();
                                }
                            }
                        });
                    } else {
                        Ext.getBody().unmask();
                        callBack && callBack();
                    }


                } else {
                    Ext.MessageBox.show({
                        title: 'Warning',
                        icon: Ext.MessageBox.INFO,
                        buttons: Ext.Msg.OK,
                        msg: objJson.message,
                        fn: function (btn) {
                            if (btn == 'ok') {
                                Ext.getBody().unmask();
                            }
                        }
                    });
                }
            });
        },
         buttonAlign:'right',
        fbar: [{
            type: 'button',
            text: '<img src="<%= Page.ResolveUrl("~/css/custImages/save.png")%>" style="width:15px;height:15px;vertical-align:middle;margin-right:5px;"> Save Config',
            handler: function () {
                this.up('panel').saveAction();
            }
        }, {
            xtype: 'button',
              width:180,
            text: '<img src="<%= Page.ResolveUrl("~/css/images/work.png")%>" style="width:15px;height:15px;vertical-align:middle;"> 初始化資料庫(ActionLog)',
            handler: function (handler, scope) {
                Ext.getBody().mask('In process...');
                WS.InitAction.initAction(function (objJson) {
                    Ext.getBody().unmask();
                    if (objJson.success) {
                        Ext.MessageBox.show({
                            title: 'Message',
                            icon: Ext.MessageBox.INFO,
                            buttons: Ext.Msg.OK,
                            msg: 'Table is created!'
                        });
                    } else {
                        Ext.MessageBox.show({
                            title: 'Error',
                            icon: Ext.MessageBox.INFO,
                            buttons: Ext.Msg.OK,
                            msg: objJson.message
                        });
                    }
                });
            }
        }],
        listeners: {
            'afterrender': function () {
                WS.InitAction.loadAction(function (objJson) {
                    if (objJson!=undefined && objJson.success) {
                        this.up("panel").down("#actionType").setValue(objJson.data[0].Type);
                        this.up("panel").down("#actionConnection").setValue(objJson.data[0].value);
                    } else {
                        Ext.MessageBox.show({
                            title: '讀取錯誤',
                            icon: Ext.MessageBox.INFO,
                            buttons: Ext.Msg.OK,
                            msg: 'Exception Setting 讀取錯誤'
                        });
                    }
                }, this);
            }
        }
    });

    var pPsConnection = Ext.create('Ext.form.Panel', {
        frame:true,
        layout : {
            type : 'form',
            align : 'stretch'
        },                
        border : true,
        title:'Connection Sample',
         bodyPadding: 5,
        margin: 5,
        frame: true,
        defaultType : 'textfield',        
        buttonAlign : 'center',
        items : [
        {
            xtype : 'label',
            html:'<img src="<%= Page.ResolveUrl("~/css/images/oracle.jpg")%>" style="height:50px;vertical-align:middle;"><BR>'            
        },
        {
            xtype : 'label',                 
            align:'right',
            html:'Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=<font color="red">Your Database IP</font>)(PORT=<font color="red">Port</font>))(CONNECT_DATA=(SERVICE_NAME=<font color="red">Service Name</font>)));User Id=<font color="red">User Id</font>;Password=<font color="red">Password</font>;pooling=false;'            
        }
        ,{
            xtype : 'label',                       
            html:'<BR><BR><img src="<%= Page.ResolveUrl("~/css/images/mssql.jpg")%>" style="height:50px;vertical-align:middle;">'
        }
        ,{
            xtype : 'label',
            html:'Data Source=<font color="red">Your Database IP</font>;Initial Catalog=<font color="red">Your Database Name</font>;User ID=<font color="red">User Id</font>;password=<font color="red">Password</font>;'
        }
        ,{
            xtype:'label',
            html:'<BR><BR><img src="<%= Page.ResolveUrl("~/css/images/mysql.jpg")%>" style="height:50px;vertical-align:middle;">'
        }
        ,{
            xtype : 'label',
            html:'Server=<font color="red">Your Database IP</font>;Port=<font color="red">Port</font>;Database=<font color="red">Your Database Name</font>;Uid=<font color="red">User Id</font>;Pwd=<font color="red">Password</font>;'
        }
        ]
    })
    var pApplication = Ext.create('Ext.form.Panel', {
        layout: {
            type: 'form',
            align: 'stretch'
        },
        title: 'Setting "Application" Info',
        itemId: 'panelApplication',
        border: false,
        bodyPadding: 5,        
        margin: 5,
        frame: true,
        defaultType: 'textfield',
        buttonAlign: 'center',
        items: [
            {
                xtype : 'container',
                layout : 'hbox',
                items : [{
                xtype: 'textfield',
                fieldLabel: '應用程式名稱',
                itemId: 'AppName',
                allowBlank: false,
                readOnly: false,
                maxWidth:300,
                width:300
            }]
            },
            {
                xtype : 'container',
                layout : 'hbox',
                margin:'5 0 0 0',
                items : [{
                xtype: 'combo',
                width:300,
                fieldLabel: '認證方式',
                queryMode: 'local',
                displayField: 'text',
                valueField: 'value',
                itemId: 'AuthenticationType',
                padding: 5,
                editable: false,
                hidden: false,
                store: new Ext.data.ArrayStore({
                    fields: ['text', 'value'],
                    data: [
                        ['AD認證', 'AD'],
                        ['密碼認證', 'PWD']
                    ]
                })
            }]
            },
             
            {
                xtype : 'container',
                layout : 'hbox',
                margin:'5 0 0 0',
                items : [{
                xtype: 'textfield',
                fieldLabel: 'Direct.Application Name',
                labelWidth:150,
                itemId: 'DirectApplicationName',
                id:'DirectApplicationName',
                allowBlank: false,
                readOnly: false,
                maxWidth:300,flex:3
            },
            {
                xtype:'button',
                text:'變更',
                margin:'0 5 0 5',
                handler:function(handler,scope){
                    var oldName = Ext.getCmp('DirectApplicationName').getValue();
                    var newName = undefined;
                    Ext.Msg.prompt('【警告】Direct NameSpace 修改是件危險的行為!!!', '修改Direct的命名，將需要重新編譯你的程式。<br>並且自動修改你的程式代碼。<br>如非專案初始階段請先備份你的程式代碼!!<BR>請確認『重新命名』操作!<br>請在下方輸入你的新命名空間名稱。', function(btn, text){
                        if (btn == 'ok'){
                            newName = text;       
                            newName  = newName.trim();
                            
                            if(newName.length==0){
                                Ext.MessageBox.show({
                                    title:'輸入錯誤',
                                    icon : Ext.MessageBox.INFO,
                                    buttons : Ext.Msg.OK,
                                    msg : '請輸入有效的命名空間!' 
                                });
                                return;
                            }
                            Ext.getCmp('DirectApplicationName').setValue(newName);
                            Ext.getCmp('btnParameterSave').handler();
                            WS.InitAction.renameDirectNamespace(oldName,newName,function(obj,jsonObj){
                                window.location.href = window.location.href;
                            });
                        }
                    });
                    
                }
            },
            {
                xtype: 'numberfield',
                labelWidth:150,
                labelAlign:'right',
                fieldLabel: 'Direct TimeOut',
                itemId: 'DirectTimeOut',
                allowBlank: false,
                readOnly: false,
                maxWidth:300,flex:1
                

            }]
            }
            
            , {
                xtype: 'fieldset',
                border: true,                
                style:'background:#EFFCF5',
                title: '應用程式基本設定',
                items: [{
                        xtype: 'container',
                        layout: 'hbox',
                        
                        margin: '3 0 0 0',
                        items: [{
                            xtype: 'textfield',
                            fieldLabel: '公司名稱',
                            itemId: 'InitCompany',
                            allowBlank: false,
                            readOnly: false,
                            flex:1
                        }, {
                            xtype: 'textfield',
                            fieldLabel: '公司UID',
                            itemId: 'InitCompanyUuid',
                            labelAlign: 'right',
                            allowBlank: false,
                            readOnly: false,
                            flex:1
                        },{
                          xtype:'button',
                          text:'<img src="<%= Page.ResolveUrl("~/css/Images/refresh.png")%>" style="width:15px;height:15px;vertical-align:middle;margin-right:5px;">',
                          width:40, 
                          margin:'0 5 0 5',
                          tooltip:'*產生新亂數',
                          handler:function(handler,scope){
                              WS.UtilAction.getUid(function(objJson){
                                if(objJson.success){
                                   this.up('panel').down('#InitCompanyUuid').setValue(objJson.data[0].uid);                                
                                }
                              },this)
                          }
                        }]

                    },

                    {
                        xtype: 'container',
                        layout: 'hbox',
                           
                        margin: '3 0 0 0',
                        items: [{
                            xtype: 'textfield',
                            fieldLabel: '管理員帳號',
                            itemId: 'InitAdmin',
                            allowBlank: false,
                            readOnly: false,
                            flex:1
                        }]

                    }

                    ,

                    {
                        xtype: 'container',
                        layout: 'hbox',
                           
                        margin: '3 0 5 0',
                        items: [{
                            xtype: 'textfield',
                            fieldLabel: '管理員Uid',
                            itemId: 'InitAdminUuid',
                            allowBlank: false,
                            readOnly: false,
                            flex:1
                        },{
                          xtype:'button',
                          text:'<img src="<%= Page.ResolveUrl("~/css/Images/refresh.png")%>" style="width:15px;height:15px;vertical-align:middle;margin-right:5px;">',
                          width:40, 
                          tooltip: '*產生新亂數',
                          margin:'0 0 0 5',
                          handler:function(handler,scope){
                              WS.UtilAction.getUid(function(objJson){
                                if(objJson.success){
                                   this.up('panel').down('#InitAdminUuid').setValue(objJson.data[0].uid);                                
                                }
                              },this)
                          }
                        }, {
                            xtype: 'textfield',
                            fieldLabel: '應用程式Uid',
                            labelAlign: 'right',
                            itemId: 'InitAppUuid',
                            allowBlank: false,
                            readOnly: false,
                            flex:1
                        },{
                          xtype:'button',
                          tooltip: '*產生新亂數',
                          text:'<img src="<%= Page.ResolveUrl("~/css/Images/refresh.png")%>" style="width:15px;height:15px;vertical-align:middle;margin-right:5px;">',
                          width:40,                          
                          margin:'0 5 0 5',
                          handler:function(handler,scope){
                              WS.UtilAction.getUid(function(objJson){
                                if(objJson.success){
                                   this.up('panel').down('#InitAppUuid').setValue(objJson.data[0].uid);                                
                                }
                              },this)
                          }
                        }]
                    },

                ]
            },


            {
                xtype: 'fieldset',
                border: true,
                margin: '3 0 5 0',
                title: '匿名設定',
                style:'background:#D6FCED',
                items: [{
                    xtype: 'container',
                    layout: 'hbox',
                    items: [{
                        xtype: 'combo',
                        fieldLabel: '允許匿名登入',
                        queryMode: 'local',
                        itemId: 'EnableGuestLogin',
                        displayField: 'text',
                        valueField: 'value',
                        
                        editable: false,
                        hidden: false,
                        store: new Ext.data.ArrayStore({
                            fields: ['text', 'value'],
                            data: [
                                ['是', 'true'],
                                ['否', 'false']
                            ]
                        })
                    }, {
                        xtype: 'textfield',
                        fieldLabel: '允許匿名公司',
                        itemId: 'GuestCompany',
                        labelAlign:'right',
                        allowBlank: true,
                        readOnly: false,
                        flex: 1
                    }, {
                        xtype: 'textfield',
                        fieldLabel: '允許匿名公司',
                        itemId: 'GuestAccount',
                        margin:'0 0 5 0',
                        labelAlign:'right',
                        allowBlank: true,
                        readOnly: false,
                        flex: 1
                    }]
                }]
            }, {
                xtype: 'fieldset',
                border: true,
                title: '網址相關',
                style:'background:#EFFCF5',
                items: [{
                    xtype: 'container',
                    layout: 'hbox',
                    items: [{
                        xtype: 'textfield',
                        fieldLabel: '預設登入頁',
                        itemId: 'LogonPage',
                        allowBlank: false,
                        readOnly: false,
                        flex: 1
                    }, {
                        xtype: 'textfield',
                        fieldLabel: '預設網頁',
                        itemId: 'DefaultPage',
                        labelAlign:'right',
                        allowBlank: false,
                        readOnly: false,
                        flex: 1
                    }]
                }, {
                    xtype: 'container',
                    layout: 'hbox',
                    margin: '3 0 0 0',
                    items: [{
                        xtype: 'textfield',
                        fieldLabel: '網站根URL',
                        itemId: 'WebRoot',
                        allowBlank: false,
                        readOnly: false,
                        flex: 1
                    }, {
                        xtype: 'textfield',
                        fieldLabel: '無權限URL',
                        itemId: 'NoPermissionPage',
                        labelAlign:'right',
                        allowBlank: false,
                        readOnly: false,
                        flex: 1
                    }]

                }, {
                    xtype: 'container',
                    layout: 'hbox',
                    margin: '3 0 0 0 ',
                    items: [{
                        xtype: 'textfield',
                        fieldLabel: '上傳資料夾',
                        itemId: "UploadFolder",
                        allowBlank: false,
                        readOnly: false,
                        flex: 1
                    }, {
                        xtype: 'textfield',
                        fieldLabel: '系統Icon URL',
                        itemId: "SystemIcon",
                        labelAlign:'right',
                        allowBlank: false,
                        readOnly: false,
                        flex: 1
                    }]
                }, {
                    xtype: 'container',
                    layout: 'hbox',
                    margin: '3 0 5 0 ',
                    items: [{
                        xtype: 'textfield',
                        fieldLabel: '公司Logon Url',
                        itemId: "CompanyImage",
                        allowBlank: false,
                        readOnly: false,
                        flex: 1
                    },
                    {
                      xtype: 'textfield',
                      labelAlign:'right',
                        fieldLabel: '登出網頁',
                        itemId: "LogoutPage",
                        allowBlank: false,
                        readOnly: false,
                        flex: 1  
                    }]
                }]
            }, {
                xtype: 'fieldset',
                border: true,
                margin: '3 0 0 0',
                title: '狀態&文字',
                style:'background:#D6FCED',
                items: [
                
                {
                    xtype : 'container',
                    layout : 'hbox',
                    items : [{
                    xtype: 'combo',
                    fieldLabel: '正式機',
                    queryMode: 'local',
                    itemId: 'IsProductionServer',
                    displayField: 'text',
                    valueField: 'value',
                    
                    editable: false,
                    hidden: false,
                    store: new Ext.data.ArrayStore({
                        fields: ['text', 'value'],
                        data: [
                            ['是', 'true'],
                            ['否', 'false']
                        ]
                    })
                },{
                    xtype:'textfield',
                    fieldLabel:'測試帳號公司',
                    labelAlign:'right',
                    name:'DevUserCompany',
                    itemId:'DevUserCompany',
                    allowBlank:true,
                    readOnly:false,
                    flex:1                
                },{
                    xtype:'textfield',
                    fieldLabel:'帳號',
                    labelAlign:'right',
                    name:'DevUserAccount',
                    itemId:'DevUserAccount',
                    allowBlank:true,
                    readOnly:false,
                    flex:1                
                },{
                    xtype:'textfield',
                    fieldLabel:'密碼',
                    labelAlign:'right',
                    name:'DevUserPassword',
                    itemId:'DevUserPassword',
                    allowBlank:true,
                    readOnly:false,
                    flex:1                
                }]
                }
                , {
                    xtype: 'container',
                    layout: 'hbox',
                    margin:'3 0 0 0',
                    items: [{
                        xtype: 'textfield',
                        fieldLabel: '系統Title',
                        itemId: "Title",
                        allowBlank: false,
                        readOnly: false,
                        flex: 1
                    }, {
                        xtype: 'textfield',
                        fieldLabel: '系統名稱',
                        itemId: "SystemName",
                        allowBlank: false,
                        readOnly: false,
                        labelAlign:'right',
                        flex: 1
                    }]
                }, {
                    xtype: 'container',
                    layout: 'hbox',
                    margin:'3 0 0 0',
                    items: [{
                        xtype: 'textfield',
                        fieldLabel: '系統詳細說明',
                        itemId: "SystemDescription",
                        allowBlank: false,
                        readOnly: false,
                        flex: 1
                    }, {
                        xtype: 'textfield',
                        fieldLabel: '系統Follter',
                        itemId: "SystemFoolter",
                        labelAlign:'right',
                        allowBlank: false,
                        readOnly: false,
                        flex: 1
                    }]
                }]
            }

        ],
         buttonAlign:'right',
        fbar: [{
            type: 'button',
            id:'btnParameterSave',
            text: '<img src="<%= Page.ResolveUrl("~/css/custImages/save.png")%>" style="width:15px;height:15px;vertical-align:middle;margin-right:5px;"> Save Config',
            handler: function () {
                Ext.getBody().mask();
                var AppName = '',
                    AuthenticationType = '',
                    EnableGuestLogin = '';
                var GuestCompany = '',
                    GuestAccount = '',
                    IsProductionServer = '';
                var DefaultPage = '',
                    LogonPage = '',
                    WebRoot = '';
                var NoPermissionPage = '',
                    UploadFolder = '',
                    Title = '';
                var SystemIcon = '',
                    CompanyImage = '',
                    SystemName = '';
                var SystemDescription = '',
                    SystemFoolter = '';
                var InitCompany = '';
                var InitCompanyUuid = '';
                var InitAdmin = '';
                var InitAdminUuid = '';
                var InitAppUuid = '';
                var LogoutPage = '';
                var DevUserCompany = '',DevUserAccount='',DevUserPassword='';
                var DirectApplicationName='';
                var DirectTimeOut='';

                AppName = this.up("panel").down("#AppName").getValue();
                AuthenticationType = this.up("panel").down("#AuthenticationType").getValue();
                EnableGuestLogin = this.up("panel").down("#EnableGuestLogin").getValue();
                GuestCompany = this.up("panel").down("#GuestCompany").getValue();
                GuestAccount = this.up("panel").down("#GuestAccount").getValue();
                IsProductionServer = this.up("panel").down("#IsProductionServer").getValue();
                DefaultPage = this.up("panel").down("#DefaultPage").getValue();
                LogonPage = this.up("panel").down("#LogonPage").getValue();
                WebRoot = this.up("panel").down("#WebRoot").getValue();
                NoPermissionPage = this.up("panel").down("#NoPermissionPage").getValue();
                UploadFolder = this.up("panel").down("#UploadFolder").getValue();
                Title = this.up("panel").down("#Title").getValue();
                SystemIcon = this.up("panel").down("#SystemIcon").getValue();
                CompanyImage = this.up("panel").down("#CompanyImage").getValue();
                SystemName = this.up("panel").down("#SystemName").getValue();
                SystemDescription = this.up("panel").down("#SystemDescription").getValue();
                SystemFoolter = this.up("panel").down("#SystemFoolter").getValue();
                LogoutPage = this.up("panel").down("#LogoutPage").getValue();

                InitCompany = this.up("panel").down("#InitCompany").getValue();
                InitCompanyUuid = this.up("panel").down("#InitCompanyUuid").getValue();
                InitAdmin = this.up("panel").down("#InitAdmin").getValue();
                InitAdminUuid = this.up("panel").down("#InitAdminUuid").getValue();
                InitAppUuid = this.up("panel").down("#InitAppUuid").getValue();

                DevUserCompany = this.up("panel").down("#DevUserCompany").getValue();
                DevUserAccount = this.up("panel").down("#DevUserAccount").getValue();
                DevUserPassword = this.up("panel").down("#DevUserPassword").getValue();

                DirectApplicationName = this.up("panel").down("#DirectApplicationName").getValue();
                DirectTimeOut = this.up("panel").down("#DirectTimeOut").getValue();

                


                

                WS.InitAction.submitParameter(AppName, AuthenticationType, EnableGuestLogin, GuestCompany, GuestAccount, IsProductionServer, DefaultPage, LogonPage, WebRoot, NoPermissionPage, UploadFolder, Title, SystemIcon, CompanyImage, SystemName, SystemDescription, SystemFoolter,
                    InitCompany, InitCompanyUuid, InitAdmin, InitAdminUuid, InitAppUuid,LogoutPage,DevUserCompany,DevUserAccount,DevUserPassword,DirectApplicationName,DirectTimeOut, function (objJson) {
                        Ext.getBody().unmask();
                        if (objJson.success) {
                            Ext.MessageBox.show({
                                title: 'Message',
                                icon: Ext.MessageBox.INFO,
                                buttons: Ext.Msg.OK,
                                msg: 'Save Completed!'
                            });
                        } else {
                            Ext.MessageBox.show({
                                title: 'Warning',
                                icon: Ext.MessageBox.INFO,
                                buttons: Ext.Msg.OK,
                                msg: 'Save Error,請通知系統管理人員!'
                            });
                        }
                    });
            }
        }, {
            xtype: 'button',
            enable:false,
            text: '<img src="<%= Page.ResolveUrl("~/css/Images/download.png")%>" style="width:15px;height:15;vertical-align:middle;">下載基本資料(執行前請先Save!)',
            handler: function (handler, scope) {
                var isDownload = true;                
                WS.InitAction.importData(isDownload,function (objJson) {
                    var link = document.createElement("a");
                    link.download = "save.sql";                    
                    var dbStr = this.up('panel').down("#basicType").getValue();
                    link.href = "./SQL/"+dbStr+"/DATA/save.sql";
                    alert("./SQL/"+dbStr+"/DATA/save.sql");
                    link.click();                    
                },this.up('panel'));
            }
        }
        , {
            xtype: 'button',
            text: '<img src="<%= Page.ResolveUrl("~/css/Images/work.png")%>" style="width:15px;height:15;vertical-align:middle;">產生基本資料(執行前請先Save!)',
            handler: function (handler, scope) {
                var isDownload = false;
                Ext.getBody().mask('In process...');
                WS.InitAction.importData(isDownload,function (objJson) {
                    Ext.getBody().unmask();;
                    if(objJson.success){
                            Ext.MessageBox.show({
                                title: 'Message',
                                icon: Ext.MessageBox.INFO,
                                buttons: Ext.Msg.OK,
                                msg: 'Completed!'
                            });
                    }else{
                        var box = Ext.MessageBox.show({        
                            title:'Warning',
                            icon : Ext.MessageBox.INFO,
                            buttons : Ext.Msg.OK,
                            msg : objJson.message 
                        });
                        box.setAutoScroll(true);                       
                    }
                });
            }
        }],
        listeners: {
            'afterrender': function () {
                WS.InitAction.loadParameter(function (objJson) {
                    if (objJson!=undefined && objJson.success) {
                        this.up("panel").down("#AppName").setValue(objJson.data[0].AppName);
                        this.up("panel").down("#AuthenticationType").setValue(objJson.data[0].AuthenticationType);
                        this.up("panel").down("#EnableGuestLogin").setValue(objJson.data[0].EnableGuestLogin);
                        this.up("panel").down("#GuestCompany").setValue(objJson.data[0].GuestCompany);
                        this.up("panel").down("#GuestAccount").setValue(objJson.data[0].GuestAccount);
                        this.up("panel").down("#IsProductionServer").setValue(objJson.data[0].IsProductionServer);
                        this.up("panel").down("#DefaultPage").setValue(objJson.data[0].DefaultPage);
                        this.up("panel").down("#LogonPage").setValue(objJson.data[0].LogonPage);
                        this.up("panel").down("#WebRoot").setValue(objJson.data[0].WebRoot);
                        this.up("panel").down("#NoPermissionPage").setValue(objJson.data[0].NoPermissionPage);
                        this.up("panel").down("#UploadFolder").setValue(objJson.data[0].UploadFolder);
                        this.up("panel").down("#Title").setValue(objJson.data[0].Title);
                        this.up("panel").down("#SystemIcon").setValue(objJson.data[0].SystemIcon);
                        this.up("panel").down("#CompanyImage").setValue(objJson.data[0].CompanyImage);
                        this.up("panel").down("#LogoutPage").setValue(objJson.data[0].LogoutPage);
                        this.up("panel").down("#SystemName").setValue(objJson.data[0].SystemName);
                        this.up("panel").down("#SystemDescription").setValue(objJson.data[0].SystemDescription);
                        this.up("panel").down("#SystemFoolter").setValue(objJson.data[0].SystemFoolter);

                        this.up("panel").down("#InitCompany").setValue(objJson.data[0].InitCompany);
                        this.up("panel").down("#InitCompanyUuid").setValue(objJson.data[0].InitCompanyUuid);
                        this.up("panel").down("#InitAdmin").setValue(objJson.data[0].InitAdmin);
                        this.up("panel").down("#InitAdminUuid").setValue(objJson.data[0].InitAdminUuid);
                        this.up("panel").down("#InitAppUuid").setValue(objJson.data[0].InitAppUuid);

                        this.up("panel").down("#DevUserCompany").setValue(objJson.data[0].DEVUserCompany);
                        this.up("panel").down("#DevUserAccount").setValue(objJson.data[0].DEVUserAccount);
                        this.up("panel").down("#DevUserPassword").setValue(objJson.data[0].DEVUserPassword);

                        this.up("panel").down("#DirectApplicationName").setValue(objJson.data[0].DirectApplicationName);
                        this.up("panel").down("#DirectTimeOut").setValue(objJson.data[0].DirectTimeOut);



                    } else {
                        Ext.MessageBox.show({
                            title: '讀取錯誤',
                            icon: Ext.MessageBox.INFO,
                            buttons: Ext.Msg.OK,
                            msg: 'Exception Setting 讀取錯誤',
                            fn:function(){
                                Ext.getBody().unmask();
                            }
                        });
                    }
                }, this);
            }
        }
    });

    
    Ext.create('Ext.form.Panel', {
        layout: {
            type: 'form',
            align: 'stretch'
        },
        border: false,
        bodyPadding: 5,
        defaultType: 'textfield',
        buttonAlign: 'center',
        items: [pDb, pException, pAction,pPsConnection, pApplication],
        renderTo: 'divMain',
        width:$(this).width()*.95                
    })
});
</script>
    <div id="divMain"></div>    
</body>
</html>