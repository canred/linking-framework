using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LK.Attribute;
using LK.DB;
using LK.Config.DataBase;
using LK.DB.SQLCreater;
using LK.MyException.Table.Record;
namespace LK.MyException.Table
{
    [LkDataBase("MyException")]
    [TableView("ERROR_LOG", true)]
	public partial class ErrorLog : TableBase{
	/*固定物件*/
	//LK.DB.SQLCreater.ASQLCreater sqlCreater = null;
	/*固定物件但名稱需更新*/
	private ErrorLog_Record _currentRecord = null;
	private IList<ErrorLog_Record> _All_Record = new List<ErrorLog_Record>();
		/*建構子*/
		public ErrorLog(){}
		public ErrorLog(IDataBaseConfigInfo dbc,string db): base(dbc,db){}
		public ErrorLog(IDataBaseConfigInfo dbc): base(dbc){}
		public ErrorLog(IDataBaseConfigInfo dbc,ErrorLog_Record currenData){
			this.setDataBaseConfigInfo(dbc);
			this._currentRecord = currenData;
		}
		public ErrorLog(IList<ErrorLog_Record> currenData){
			this._All_Record = currenData;
		}
		/*欄位資訊 Start*/
		public string UUID {get{return "UUID" ; }}
		public string CREATE_DATE {get{return "CREATE_DATE" ; }}
		public string UPDATE_DATE {get{return "UPDATE_DATE" ; }}
		public string IS_ACTIVE {get{return "IS_ACTIVE" ; }}
		public string ERROR_CODE {get{return "ERROR_CODE" ; }}
		public string ERROR_TIME {get{return "ERROR_TIME" ; }}
		public string ERROR_MESSAGE {get{return "ERROR_MESSAGE" ; }}
        public string APPLICATION_NAME { get { return "APPLICATION_NAME"; } }
		public string ATTENDANT_UUID {get{return "ATTENDANT_UUID" ; }}
		public string ERROR_TYPE {get{return "ERROR_TYPE" ; }}
		/*欄位資訊 End*/
		/*固定的方法，但名稱需變更 Start*/
		public ErrorLog_Record CurrentRecord(){
			try{
				if (_currentRecord == null){
					if (this._All_Record.Count > 0){
						_currentRecord = this._All_Record.First();
					}
				}
				return _currentRecord;
			}
			catch (System.Exception ex){
				log.Error(ex);
				throw ex;
			}
		}
		public ErrorLog_Record CreateNew(){
			try{
				ErrorLog_Record newData = new ErrorLog_Record();
				return newData;
			}
            catch (System.Exception ex)
            {
				log.Error(ex);
				throw ex;
			}
		}
		public IList<ErrorLog_Record> AllRecord(){
			try{
				return _All_Record;
			}
            catch (System.Exception ex)
            {
				log.Error(ex);
				throw ex;
			}
		}
		public void RemoveAllRecord(){
			try{
				_All_Record = new List<ErrorLog_Record>();
			}
            catch (System.Exception ex)
            {
				log.Error(ex);
				throw ex;
			}
		}
		/*固定的方法，但名稱需變更 End*/
		/*有關PK的方法*/
		/*利用物件自已的AllRecord的資料來更新資料行*/
		public void UpdateAllRecord() {
			try{
				UpdateAllRecord<ErrorLog_Record>(this.AllRecord());   
			}
            catch (System.Exception ex)
            {
				log.Error(ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來更新資料行*/
        public void UpdateAllRecord(LK.DB.DB db)
        {
			try{
				UpdateAllRecord<ErrorLog_Record>(this.AllRecord(),db);   
			}
            catch (System.Exception ex)
            {
				log.Error(ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來新增資料行*/
		public void InsertAllRecord() {
			try{
				InsertAllRecord<ErrorLog_Record>(this.AllRecord());   
			}
            catch (System.Exception ex)
            {
				log.Error(ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來新增資料行*/
        public void InsertAllRecord(LK.DB.DB db)
        {
			try{
				InsertAllRecord<ErrorLog_Record>(this.AllRecord(),db);   
			}
            catch (System.Exception ex)
            {
				log.Error(ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來刪除資料行*/
		public void DeleteAllRecord() {
			try{
				DeleteAllRecord<ErrorLog_Record>(this.AllRecord());   
			}
            catch (System.Exception ex)
            {
				log.Error(ex);
				throw ex;
			}
		}
		/*利用物件自已的AllRecord的資料來刪除資料行*/
        public void DeleteAllRecord(LK.DB.DB db)
        {
			try{
				DeleteAllRecord<ErrorLog_Record>(this.AllRecord(),db);   
			}
            catch (System.Exception ex)
            {
				log.Error(ex);
				throw ex;
			}
		}
	}
}
