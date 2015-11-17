
if exists (select * from sysobjects where id = 
object_id(N'[dbo].[Users]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Users]
GO
CREATE TABLE [dbo].[Users] (
   [Id] [int] IDENTITY(1,1),
   [Username] [varchar] (15) NOT NULL ,
   [Password] [varchar] (25) NOT NULL ,
   [RoleId] [int] NOT NULL ,
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Users] WITH NOCHECK ADD 
   CONSTRAINT [PK_Users] PRIMARY KEY  NONCLUSTERED 
   (
      [Id]
   )  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Users]
Add foreign key (RoleId)
References UserRoles([Id])
GO

INSERT INTO Users ([Username], [Password], [RoleId]) values('manager','manager',1)
INSERT INTO Users ([Username], [Password], [RoleId]) values('admin','admin',2)
INSERT INTO Users ([Username], [Password], [RoleId]) values('user','user',3)
GO