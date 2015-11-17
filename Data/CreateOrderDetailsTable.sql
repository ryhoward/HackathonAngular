
if exists (select * from sysobjects where id = 
object_id(N'[dbo].[OrderDetails]') and OBJECTPROPERTY(id, N'OrderDetails') = 1)
drop table [dbo].[OrderDetails]
GO
CREATE TABLE [dbo].[OrderDetails] (
   [OrderId] [int] IDENTITY(1,1),
   [PurchaserName] [nvarchar] (40),
   [PurchaserPhone] [varchar] (20),
   [PurchaserEmail] [varchar] (320),
   [BillingStreet] [varchar] (120),
   [BillingStreet2] [varchar] (120),
   [BillingCity] [varchar] (80),
   [BillingState] [varchar] (40),
   [BillingZip] [varchar] (11),
   [ShippingStreet] [varchar] (120),
   [ShippingStreet2] [varchar] (120),
   [ShippingCity] [varchar] (80),
   [ShippingState] [varchar] (40),
   [ShippingZip] [varchar] (11),
   [UserId] [int] NOT NULL,
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[OrderDetails] WITH NOCHECK ADD 
   CONSTRAINT [PK_OrderDetails] PRIMARY KEY  NONCLUSTERED 
   (
      [OrderId]
   )  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[OrderDetails]
Add foreign key (UserId)
References Users([Id])
GO