CREATE OR REPLACE VIEW {user}.ERROR_LOG_V
AS
SELECT     
		ERROR_LOG.uuid, 
		ERROR_LOG.create_date, 
		ERROR_LOG.update_date, 
		ERROR_LOG.is_active, 
		ERROR_LOG.error_code, 
        ERROR_LOG.error_time, 
		ERROR_LOG.error_message, 
		ERROR_LOG.application_name, 
		ERROR_LOG.attendant_uuid, 
		ERROR_LOG.error_type,
		ERROR_LOG.is_read, 
		ATTENDANT.c_name, 
		ATTENDANT.e_name, 
		ATTENDANT.id
FROM    ERROR_LOG 
LEFT JOIN ATTENDANT ON ERROR_LOG.attendant_uuid = ATTENDANT.uuid;
/