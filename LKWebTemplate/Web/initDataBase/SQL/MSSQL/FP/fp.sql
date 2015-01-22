
CREATE PROCEDURE [dbo].[get_logon_info]
	@i_company                                VARCHAR(4000) ,
	@i_account                                VARCHAR(4000) 
AS 
	BEGIN
		SET NOCOUNT ON
		
		DECLARE @p_str                                    VARCHAR(4000) 
		DECLARE @p_where                                  VARCHAR(2000)                   
		SET @p_where = ' where 1=1' 
		SELECT @p_str  = '' 
		SELECT @p_where  = @p_where + ' and upper(company_id)=upper(''' + @i_company + ''')' 
		SELECT @p_where  = @p_where + ' and account=''' + @i_account + '''' 
		SELECT @p_str  = 'select * from authority_logon_v' + @p_where 
				
		EXEC @p_str 

		SET NOCOUNT OFF

	END;

CREATE FUNCTION [dbo].[PARENTLST] ( @uuid VARCHAR(4000) ) RETURNS VARCHAR(4000) 
AS 
	BEGIN
		
		DECLARE @lst                                      VARCHAR(4000) 
		
		DECLARE @my_uuid                                  VARCHAR(36) 
		
		DECLARE @my_parent_uuid                           VARCHAR(36) 
		
		DECLARE @temp_uuid                                VARCHAR(36) 
		SELECT @my_uuid  = @uuid 
		SELECT @my_parent_uuid  =  
					case
						 when appmenu_uuid  is null then uuid
						 else appmenu_uuid
					 end
		FROM  appmenu 
		WHERE	 uuid  = @my_uuid
		
		IF ( @my_parent_uuid = @my_uuid ) 
		BEGIN 
			SELECT @lst  = @my_uuid 
		END
		ELSE
		BEGIN 
			SELECT @lst  = @my_uuid 
			while  @my_parent_uuid != @my_uuid
			BEGIN
			
				SELECT @lst  = @my_parent_uuid + ',' + @lst 
				SELECT @temp_uuid  =  
							case
								 when appmenu_uuid  is null then uuid
								 else appmenu_uuid
							 end
				FROM  GHG_BASIC.DBO.appmenu 
				WHERE	 uuid  = @my_parent_uuid
				
				SELECT @my_uuid  = @my_parent_uuid 
				SELECT @my_parent_uuid  = @temp_uuid 

			END			
		END
		
		return @lst+','

		RETURN NULL
	END
;
