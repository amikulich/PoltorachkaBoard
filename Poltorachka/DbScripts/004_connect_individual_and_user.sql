DECLARE @userId UNIQUEIDENTIFIER = 'd3e24dfd-4f27-4ac1-9009-39c86b03240d',
@userName NVARCHAR(255) = 'Artem Mikulich';


IF EXISTS (SELECT 1 FROM individual WHERE name = @userName AND [user_id] IS NULL)
BEGIN
	UPDATE individual 
	SET [user_id] = @userId
	WHERE name = @userName
END;