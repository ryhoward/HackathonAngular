if exists (select * from sysobjects where id = 
object_id(N'[dbo].[UserCartItems]') and OBJECTPROPERTY(id, N'UserCartItems') = 1)
drop table [dbo].[UserCartItems]
GO
CREATE TABLE [dbo].[UserCartItems] (
   [Id] [int] IDENTITY(1,1),
   [ProductId] [int] NOT NULL ,
   [ProductName] [varchar] (250) NOT NULL,
   [Count] [int] NOT NULL,
   [Price] [MONEY] NOT NULL,
   [UserId] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[UserCartItems] WITH NOCHECK ADD 
   CONSTRAINT [PK_UserCartItems] PRIMARY KEY  NONCLUSTERED 
   (
      [Id]
   )  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[UserCartItems]
Add foreign key (UserId)
References Users([Id])
GO