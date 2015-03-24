Ext.define('Ist.view.UserMainPanel', {
    extend: 'Ext.tab.Panel',
    xtype: 'UserMainPanel',
    config: {
        tabBarPosition: 'bottom',
        items: [{
            title: '首頁',
            iconCls: 'home',
            styleHtmlContent: true,
            scrollable: false,
            padding: 0,
            items: [{
                xtype:'UserMainSubPanel'
            }]
        }, {
            title: '快查',
            iconCls: 'search',
            items: [{
                xtype: 'SearchPanel'
            }]
        }, {
            title: '活動',
            iconCls: 'action',
            items: [{
                xtype:'IstActionPanel'
            }]
        }, {
            title: '登出',
            iconCls: 'action',
            listeners: {
                activate: function() {
                    location.href = location.href;
                }
            }

        }, {
            title: '設定',
            iconCls: 'star',
            items: [{
                docked: 'top',
                xtype: 'toolbar',
                items: [{
                    xtype: 'label',
                    html: '<img height="30px" style="vertical-align:middle;" src="./resources/css/images/istlogo.gif"/>    宜特科技股份有限公司',
                    style: {
                        'text-align': 'left'
                    }
                }, {
                    xtype: 'label',
                    flex: 1
                }]
            }, {
                xtype: 'video',
                url: 'http://av.vimeo.com/64284/137/87347327.mp4?token=1330978144_f9b698fea38cd408d52a2393240c896c',
                posterUrl: 'http://b.vimeocdn.com/ts/261/062/261062119_640.jpg'
            }]
        }]
    },
    initialize: function() {
        Ext.Viewport.on('orientationchange', 'handleOrientationChange', this, {
            buffer: 50
        });
        this.callParent(arguments);
    },
    handleOrientationChange: function() {}
});
