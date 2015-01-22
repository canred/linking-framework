using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;  
using LK.Attribute;
using LK.DB;
using LK.Config.DataBase;
using LK.DB.SQLCreater;
using LK.ActionLog.Table.Record;
namespace LK.ActionLog.Table
{
    [LkDataBase("ActionLog")]
    [TableView("ACTION_LOG", true)]
	public partial class ActionLog : TableBase{
	/*固定物件*/
	//LK.DB.SQLCreater.ASQLCreater sqlCreater = null;
	/*固定物件但名稱需更新*/
	private ActionLog_Record _currentRecord = null;
    private IList<ActionLog_Record> _All_Record = new List<ActionLog_Record>();
		/*建構子*/
		public ActionLog(){}
		public ActionLog(IDataBaseConfigInfo dbc,string db): base(dbc,db){}
		public ActionLog(IDataBaseConfigInfo dbc): base(dbc){}
        public ActionLog(IDataBaseConfigInfo dbc, ActionLog_Record currenData)
        {
			this.setDataBaseConfigInfo(dbc);
			this._currentRecord = currenData;
		}
        public ActionLog(IList<ActionLog_Record> currenData)
        {
			this._All_Record = currenData;
		}
		/*欄位資訊 Start*/
		public string UUID {get{return "UUID" ; }}
		public string CREATE_USER {get{return "CREATE_USER" ; }}
		public string CREATE_DATE {get{return "CREATE_DATE" ; }}
		public string UPDATE_USER {get{return "UPDATE_USER" ; }}
		public string UPDATE_DATE {get{return "UPDATE_DATE" ; }}
		public string IS_ACTIVE {get{return "IS_ACTIVE" ; }}
		public string ATTENDANT_UUID {get{return "ATTENDANT_UUID" ; }}
		public string CLASS_NAME {get{return "CLASS_NAME" ; }}
		public string FUNCTION_NAME {get{return "FUNCTION_NAME" ; }}
		public string PARAMETER {get{return "PARAMETER" ; }}
		/*欄位資訊 End*/
		/*固定的方法，但名稱需變更 Start*/
        public ActionLog_Record CurrentRecord()
        {
			try{
				if (_currentRecord == null){
					if (this._All_Record.Count > 0){
						_currentRecord = this._All_Record.First();
					}
				}
				return _currentRecord;
			}
			catch (Exception ex){
                log.Error(ex); LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
        public ActionLog_Record CreateNew()
        {
			try{
                ActionLog_Record newData = new ActionLog_Record();
				return newData;
			}
			catch (Exception ex){
                log.Error(ex); LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
        public IList<ActionLog_Record> AllRecord()
        {
			try{
				return _All_Record;
			}
			catch (Exception ex){
                log.Error(ex); LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		public void RemoveAllRecord(){
			try{
                _All_Record = new List<ActionLog_Record>();
			}
			catch (Exception ex){
                log.Error(ex); LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*固定的方法，但名稱需變更 End*/
		/*有關PK的方法*/
		/*利用物件自已的AllRecord的資料來更新資料行*/
		public void UpdateAllRecord() {
			try{
                UpdateAllRecord<ActionLog_Record>(this.AllRecord());   
			}
			catch (Exception ex){
                log.Error(ex); LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來更新資料行*/
        public void UpdateAllRecord(LK.DB.DB db)
        {
			try{
                UpdateAllRecord<ActionLog_Record>(this.AllRecord(), db);   
			}
			catch (Exception ex){
                log.Error(ex); LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來新增資料行*/
		public void InsertAllRecord() {
			try{
                InsertAllRecord<ActionLog_Record>(this.AllRecord());   
			}
			catch (Exception ex){
                log.Error(ex); LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來新增資料行*/
        public void InsertAllRecord(LK.DB.DB db)
        {
			try{
                InsertAllRecord<ActionLog_Record>(this.AllRecord(), db);   
			}
			catch (Exception ex){
                log.Error(ex); LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來刪除資料行*/
		public void DeleteAllRecord() {
			try{
                DeleteAllRecord<ActionLog_Record>(this.AllRecord());   
			}
			catch (Exception ex){
                log.Error(ex); LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來刪除資料行*/
        public void DeleteAllRecord(LK.DB.DB db)
        {
			try{
                DeleteAllRecord<ActionLog_Record>(this.AllRecord(), db);   
			}
			catch (Exception ex){
                log.Error(ex); LK.MyException.MyException.Error(this, ex);
				throw ex;
			}
		}
		/*依照資料表與資料表的關係，產生出來的方法*/
	}
}
