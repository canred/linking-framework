Ext.onReady(function() {
    Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION + ".UserAction"));
    var MainTask = {
        run: function() {
            WS.UserAction.keepSession(function(data) {
                try {
                    redirectUrl = SYSTEM_HTTP_URL + SYSTEM_ROOT_PATH + "/default.aspx";
                    if (data.validation == 'ok') {

                    } else {
                        location.href = redirectUrl;
                    }
                } catch (ex) {
                    location.href = redirectUrl;
                }
            });
        },
        interval: 30000
    };
    Ext.TaskManager.start(MainTask);
});
