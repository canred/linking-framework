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
            };
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
    },
    trace: {
        winErrorTrace: undefined,
        param: {
            interval: 5,
            stop: false
        },
        _task: {            
            run: function() {
                if (IsProductionServer.toLowerCase() == "false") {
                    if (!this.param.stop) {
                        if (!this.winErrorTrace) {
                            this.winErrorTrace = Ext.create('WS.ErrorTraceWindow', {});
                        };
                        var winErrorTrace = this.winErrorTrace;
                        this.winErrorTrace.myStore.errorlog.reload({
                            callback: function(records, operation, success) {
                                if (records.length > 0) {
                                    winErrorTrace.show();
                                };
                            }
                        });
                    };
                };
            }
        },
        _runStart: function() {
            Ext.TaskManager.start(this._task);
        },
        fnTrace: function() {
            if (Ext.isNumeric(this.param.interval)) {
                this._task.interval = this.param.interval * 1000;
                this._runStart.call(this);
            };
        },
        setInterval: function(sec) {
            if (Ext.isNumeric(sec)) {
                this.param.interval = sec;
            } else {
                alert('設定 Util.trace.param.interval 錯誤!');
            };
        }
    },
    runAll: function() {
        this.trace._task.scope = this.trace;
        this.session._task.scope = this.session;        
        this.session.fnKeep.call(this.session);
        this.trace.fnTrace.call(this.trace);
    }
});