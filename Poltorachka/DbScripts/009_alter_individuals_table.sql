ALTER TABLE individual ADD [app_id] INT NULL

ALTER TABLE [dbo].[individual]  WITH CHECK ADD  CONSTRAINT [FK__individual__app] FOREIGN KEY([app_id])
REFERENCES [dbo].[app] ([app_id])
GO

ALTER TABLE [dbo].[individual] CHECK CONSTRAINT [FK__individual__app]
GO


