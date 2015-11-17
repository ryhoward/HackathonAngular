if exists (select * from sysobjects where id = 
object_id(N'[dbo].[UserCart]') and OBJECTPROPERTY(id, N'UserCart') = 1)
drop table [dbo].[UserCart]
GO
CREATE TABLE [dbo].[UserCart] (
   [Id] [int] IDENTITY(1,1),
   [ProductId] [int] NOT NULL ,
   [UserId] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[UserRoles] WITH NOCHECK ADD 
   CONSTRAINT [PK_UserRoles] PRIMARY KEY  NONCLUSTERED 
   (
      [Id]
   )  ON [PRIMARY] 
GO

INSERT INTO UserRoles (Role) values('Manager')
INSERT INTO UserRoles (Role) values('Admin')
INSERT INTO UserRoles (Role) values('User')
GO