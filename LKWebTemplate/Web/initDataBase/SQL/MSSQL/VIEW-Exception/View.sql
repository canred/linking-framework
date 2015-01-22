CREATE VIEW [error_log_v]
AS
SELECT     dbo.ERROR_LOG.uuid, dbo.ERROR_LOG.create_date, dbo.ERROR_LOG.update_date, dbo.ERROR_LOG.is_active, dbo.ERROR_LOG.error_code, 
                      dbo.ERROR_LOG.error_time, dbo.ERROR_LOG.error_message, dbo.ERROR_LOG.application_name, dbo.ERROR_LOG.attendant_uuid, dbo.ERROR_LOG.error_type, 
                      dbo.ERROR_LOG.is_read, dbo.ATTENDANT.c_name, dbo.ATTENDANT.e_name, dbo.ATTENDANT.id
FROM         dbo.ERROR_LOG LEFT JOIN
                      dbo.ATTENDANT ON dbo.ERROR_LOG.attendant_uuid = dbo.ATTENDANT.uuid
;