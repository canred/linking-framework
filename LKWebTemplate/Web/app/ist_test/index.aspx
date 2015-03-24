<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="LKWebTemplate.app.ist.index" %>

<!DOCTYPE HTML>
<html xmlns="http://www.w3.org/1999/xhtml">

<head>
    <meta http-equiv="content-type" content="text/html; charset=utf-8" />
    <title>Ist</title>
    
    <link rel="stylesheet" href="<%= Page.ResolveUrl("~/css/mpStand.css")%>" type="text/css" />
    <link rel="stylesheet" href="<%= Page.ResolveUrl("~/css/menu.css")%>" type="text/css" />
    <script type="text/javascript" src='<%= Page.ResolveUrl("~/js/jquery.js")%>'></script>
    <script type="text/javascript" src='<%= Page.ResolveUrl("~/js/superfish.js")%>'></script>    
    <script type="text/javascript" src='<%= Page.ResolveUrl("~/js/system-config.ashx")%>'></script>
    

    <!--引入ExtJs的程式-->
    
    <style type="text/css">
    /**
         * Example of an initial loading indicator.
         * It is recommended to keep this as minimal as possible to provide instant feedback
         * while other resources are still being loaded for the first time
         */    
    html,
    body {
        height: 100%;
        background-color: #1985D0
    }
    #appLoadingIndicator {
        position: absolute;
        // top: 50%;
        // margin-top: -15px;
        text-align: center;
        width: 100%;
        height: 100%;
        -webkit-animation-name: appLoadingIndicator;
        -webkit-animation-duration: 3s;
        -webkit-animation-iteration-count: infinite;
        -webkit-animation-direction: linear;
        background-image: url('./resources/css/images/start.png');
         background-size:cover; 
    }
    #appLoadingIndicator > * {
        background-color: #FFFFFF;
        display: inline-block;
        // height: 30px;
        -webkit-border-radius: 15px;
        margin: 0 5px;
        width: 30px;
        opacity: 0.8;
    }
    @-webkit-keyframes appLoadingIndicator {
        0% {
            opacity: 1
        }
        50% {
            opacity: 0.8
        }
        100% {
            opacity: 1
        }
    }
   
    </style>
    <!-- The line below must be kept intact for Sencha Command to build your application -->
    <script id="microloader" type="text/javascript" src=".sencha/app/microloader/development.js"></script>
    <script language="javascript" type="text/javascript">
        Ext.Loader.setConfig({ disableCaching:true });
    </script>
    <script src="../../proxyTouch.ashx"></script>
    <script type="text/javascript" charset="utf-8">
//alert(device.name);
// Wait for Wormhole to load
//
//document.addEventListener("deviceready", onDeviceReady, false);

// Wormhole is ready
//

// function onDeviceReady() {
//     alert(device.name);
//     alert(device.platform);
//     alert(device.uuid);
//     alert(device.version);

// }

</script>
</head>
<body>
    <div id="appLoadingIndicator">
        <div></div>
        <div></div>
        <div></div>
    </div>
</body>
</html>
