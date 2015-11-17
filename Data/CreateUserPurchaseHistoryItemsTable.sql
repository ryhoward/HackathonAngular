if exists (select * from sysobjects where id = 
object_id(N'[dbo].[UserPurchaseHistoryItems]') and OBJECTPROPERTY(id, N'UserPurchaseHistoryItems') = 1)
drop table [dbo].[UserPurchaseHistoryItems]
GO
CREATE TABLE [dbo].[UserPurchaseHistoryItems] (
   [Id] [int] IDENTITY(1,1),
   [ProductId] [int] NOT NULL,
   [Name] [varchar] (250) NOT NULL,
   [Price] [MONEY] NOT NULL,
   [Date] Datetime2 NOT NULL,
   [UserId] [int] NOT NULL,
   [OrderId] [int] NOT NULL,
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[UserPurchaseHistoryItems] WITH NOCHECK ADD 
   CONSTRAINT [PK_UserPurchaseHistoryItems] PRIMARY KEY  NONCLUSTERED 
   (
      [Id]
   )  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[UserPurchaseHistoryItems]
Add foreign key (UserId)
References Users([Id])
GO

ALTER TABLE [dbo].[UserPurchaseHistoryItems]
Add foreign key (OrderId)
References OrderDetails([OrderId])
GO