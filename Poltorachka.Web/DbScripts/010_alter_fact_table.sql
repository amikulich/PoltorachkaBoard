ALTER TABLE fact ADD [app_id] INT NULL

ALTER TABLE [dbo].[fact]  WITH CHECK ADD  CONSTRAINT [FK__fact__app] FOREIGN KEY([app_id])
REFERENCES [dbo].[app] ([app_id])
GO

ALTER TABLE [dbo].[fact] CHECK CONSTRAINT [FK__fact__app]
GO
