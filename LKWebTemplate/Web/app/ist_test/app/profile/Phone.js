Ext.define('Ist.profile.Phone',{
	extend:'Ist.profile.Base',
	requires: ['Ist.view.phone.Main'],
	config: {
        controllers: ['Main']
    },
	isActive:function(){
		if( Ext.os.deviceType=='Phone' ){
			//alert('Phone');
			return true;
		};
	},
	launch:function(){
		//alert(Ist.view.phone.Main);
        Ext.Viewport.add(Ext.create('Ist.view.phone.Main',{}));
		this.callParent();		
	}
});





