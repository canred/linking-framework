Ext.define('Ist.profile.Tablet',{
	extend:'Ist.profile.Base',
	requires: ['Ist.view.tablet.Main'],
	config: {
        controllers: ['Main']
    },
	isActive:function(){		
		if(Ext.os.deviceType=='Desktop' || Ext.os.deviceType=='Tablet'){
			//alert('Tablet');
			return true;
		}else{
			return false;
		}		
	},
	launch:function(){
        Ext.Viewport.add(Ext.create('Ist.view.tablet.Main',{}));
		this.callParent();		
	}
});
