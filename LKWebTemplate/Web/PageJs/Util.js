Ext.define('WS.Util', {
    session: {
        param: {
            redirectUrl: SYSTEM_HTTP_URL + SYSTEM_ROOT_PATH + "/default.aspx",
            interval: 30
        },
        _task: {
            run: function() {
                WS.UserAction.keepSession(function(data) {
                    try {
                        console.log(this);
                        if (data.validation != 'ok') {
                            location.href = this.param.redirectUrl;
                        };
                    } catch (ex) {
                        location.href = this.param.redirectUrl;
                    }
                }, this);
            }
        },
        _runStart: function() {
            Ext.TaskManager.start(this._task);
        },
        fnKeep: function() {
            this._task.scope = this;
            if (Ext.isNumeric(this.param.interval)) {
                this._task.interval = this.param.interval * 1000;                
                this._runStart.apply(this);
            } ;
        },
        setRedirectUrl: function(url) {
            this.param.redirectUrl = url;
        },
        setInterval: function(sec) {
            if (Ext.isNumeric(sec)) {
                this.param.interval = sec;
            } else {
                alert('設定 Util.session.param.interval 錯誤!');
            };
        }
    }
});
