var SYSTEM_ROOT;
var SYSTEM_ROOT_PATH;
var SYSTEM_HTTP_URL;
var SYSTEM_APPLICATION;
SYSTEM_ROOT = location.href.split('/')[3];
SYSTEM_ROOT_PATH = "/" + SYSTEM_ROOT;
SYSTEM_HTTP_URL = location.href.split('/')[0] + "/" + location.href.split('/')[1] + "/" + location.href.split('/')[2] ;
SYSTEM_URL_ROOT = SYSTEM_HTTP_URL+'/' + SYSTEM_ROOT;
SYSTEM_APPLICATION = "MyApplication";

function isIE6() {
    return true;
}
