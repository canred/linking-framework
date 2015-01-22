<%@ Page Title="" Language="C#" MasterPageFile="~/mpStand.Master" AutoEventWireup="true" CodeBehind="joinUs.aspx.cs" Inherits="LKWebTemplate.cloud.joinUs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript" src='<%= Page.ResolveUrl("~/js/shared/include-ext.js")%>'></script>
<script type="text/javascript" src='<%= Page.ResolveUrl("~/Proxy.ashx")%>'></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div id="divJoinUs"></div>
<script language="javascript" type="text/javascript">
Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION + ".CloudAction"));
Ext.onReady(function(){
	Ext.create('Ext.form.Panel', {
		layout : {
			type : 'form',
			align : 'stretch'
		},		
		width: $("#header").width()-16,
		height:670,
		//height:500,
		//id : 'id',
		// itemId:'itemId',				
		border : true,
		bodyPadding : 5,		
		buttonAlign : 'center',
		renderTo:'divJoinUs',
		items : [
			{
				xtype:'tabpanel',
				plain : true,
				padding:10,
				width: $("#header").width()-20,
				bodyStyle:'{"background-color":"#D1DFF0"; text-align: center;}',
				items:[
					{		
						title: '<img src="' + SYSTEM_URL_ROOT + '/css/images/info.png" style="width:16px;height:16px;vertical-align:middle;margin-right:5px;"/>' + '註冊前的配置',
						items:[{							
							xtype:'image',
							src:'../css/images/CloudJoinUs.png'  ,
							tag:"img",
							height:600							
						}],
						border:false
					},
					{
						title: '<img src="' + SYSTEM_URL_ROOT + '/css/images/check_red.png" style="width:16px;height:16px;vertical-align:middle;margin-right:5px;"/>' + '驗証我的配置',
						items:{
							xtype:'panel',
							border:false,
							height:250,							
							bodyStyle:'{"background-color":"#D1DFF0"; text-align: center;}',
							items:[
								{
									xtype : 'container',
									layout : 'hbox',
									items : [{
										xtype:'textfield',
										fieldLabel:'輸入網站位置',
										labelAlign:'right',
										flex:1,	
										id:'txtServerWebUrl',
										value:'http://127.0.0.1/LKWebTemplate3',									
										emptyText:'http://127.0.0.1/LKWebTemplate3'
									},{
										xtype:'button',
										text:'<img src="' + SYSTEM_URL_ROOT + '/css/images/connection.png" style="width:16px;height:16px;vertical-align:middle;margin-right:5px;"/>測試',
										margin:'0 5 0 5',
										handler:function(handler,scope){																
											var serverWebUrl = Ext.getCmp('txtServerWebUrl').getValue();
											WS.CloudAction.connectTest(serverWebUrl,function(obj,jsonObj){												
												if(jsonObj.result.success){
													Ext.getCmp('tab3').setDisabled(false);
													var message = "";
													message="身份認證中心已提出連線請求";
													message+="<BR>從節點伺服器("+serverWebUrl+")";
													message+="<BR>也同意這一個請求!";
													message+="<BR>連線測試成功!";
													Ext.MessageBox.show({
														title:'Connect Test',
														icon : Ext.MessageBox.INFO,
														buttons : Ext.Msg.OK,
														msg : message
													});
												}else{
													Ext.getCmp('tab3').setDisabled(true);
													var message = jsonObj.result.message;
													message += "<BR>請確定你的跨網域提交功能是否開啟!";
													message += "<BR>1.DirectAuth的權限";
													message += "<BR>2.伺服器設定要允可option的提交!";
													Ext.MessageBox.show({
														title:'Warning',
														icon : Ext.MessageBox.INFO,
														buttons : Ext.Msg.OK,
														msg : message
													});
												}
											});
											//測試cloudAction.checkConfig
											
										}
									}]
								}
								,
								{
									xtype:'image',
									src:'../css/images/communication.png'  ,
									tag:"img",
									height:200,
									bodyStyle:'{"background-color":"#D1DFF0"; text-align: center;}'
								}
								
							]
						}
					},
					{
						title: '<img src="' + SYSTEM_URL_ROOT + '/css/images/join.png" style="width:16px;height:16px;vertical-align:middle;margin-right:5px;"/>' + '執行註冊',
						id:'tab3',
						disabled:true,		
						height:170,			
						items:[{
							xtype:'panel',
							border:false,
							height:150,
							width:$("#header").width()-50,
							bodyStyle:'{"background-color":"#D1DFF0"; text-align: left;}',
							items:[
								{xtype:'button',
								text:'<img src="' + SYSTEM_URL_ROOT + '/css/images/join.png" style="width:16px;height:16px;vertical-align:middle;margin-right:5px;"/>Regist Now',
								margin:'10 0 0 110',
								handler:function(handler,scope){
									var serverWebUrl = Ext.getCmp('txtServerWebUrl').getValue();
									Ext.getCmp('txtResult').setValue("--------初始設定--------");
									WS.CloudAction.settingCloud(serverWebUrl,function(obj,jsonObj){										
										if(jsonObj.result.success){
											Ext.getCmp('txtResult').setValue(Ext.getCmp('txtResult').getValue()+"\n主伺服器設定完成!");
											WS.CloudAction.settingSlave(serverWebUrl,function(obj,jsonObj2){												
												if(jsonObj2.result.success){
													Ext.getCmp('txtResult').setValue(Ext.getCmp('txtResult').getValue()+"\n從伺服器設定完成!");
													Ext.getCmp('txtResult').setValue(Ext.getCmp('txtResult').getValue()+"\n--------設定完成--------");
												}else{
													Ext.getCmp('txtResult').setValue(Ext.getCmp('txtResult').getValue()+jsonObj2.result.message);
												}
											});
										}else{
											Ext.getCmp('txtResult').setValue(Ext.getCmp('txtResult').getValue()+"\n"+jsonObj.result.message);
										}
									});
								}}
								,
								{
									xtype : 'textarea',
									fieldLabel : '註冊結果',
									labelAlign:'right',
									id:'txtResult',
									width:$("#header").width()-50,
									height:100,
									margin:'10 0 0 0',
									readOnly:true
								}
							]
						}]
					},
					{
						title: '<img src="' + SYSTEM_URL_ROOT + '/css/images/man.png" style="width:16px;height:16px;vertical-align:middle;margin-right:5px;"/>' + '功能說明',
						bodyStyle:'{"background-color":"#D1DFF0"; text-align: left;}',
						height:1000,
						autoScroll:true,
						items:[
							{
							xtype:'image',
							src:'../css/images/CloudDesc.png'  ,
							tag:"img",
							width:800,
							height:2000
							}
							
						]
					}
				]
			}
		]
	});

});
</script>

</asp:Content>
