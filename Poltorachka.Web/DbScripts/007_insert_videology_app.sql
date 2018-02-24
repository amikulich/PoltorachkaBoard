IF NOT EXISTS(SELECT 1 FROM [dbo].[app])
BEGIN 
    INSERT INTO [dbo].[app]
           ([app_name]
           ,[active]
           ,[description])
     VALUES
           ('Videology'
           ,1
           ,'Poltorachka board for Minsk Videology Team')
END


