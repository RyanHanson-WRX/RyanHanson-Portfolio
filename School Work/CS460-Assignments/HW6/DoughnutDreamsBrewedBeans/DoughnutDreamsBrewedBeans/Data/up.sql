CREATE TABLE [Store] (
  [Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  [Name] VARCHAR(255) NOT NULL
)
GO

CREATE TABLE [DeliveryLocation] (
  [Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  [Name] VARCHAR(255) NOT NULL
)
GO

CREATE TABLE [Station] (
  [Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  [Name] VARCHAR(255) NOT NULL
)
GO

CREATE TABLE [Item] (
  [Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  [Name] VARCHAR(255) NOT NULL,
  [Price] DECIMAL(10,2) NOT NULL,
  [Description] VARCHAR(255),
  [StationId] INT
)
GO

CREATE TABLE [Order] (
  [Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  [Name] VARCHAR(255) NOT NULL,
  [DeliveryId] INT,
  [StoreId] INT,
  [TotalPrice] DECIMAL(10,2) NOT NULL,
  [Complete] VARCHAR(10) NOT NULL
)
GO

CREATE TABLE [OrderItem] (
  [Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  [ItemId] INT,
  [OrderId] INT,
  [Quantity] INT NOT NULL,
  [Status] VARCHAR(255) NOT NULL
)
GO

ALTER TABLE [Item] ADD CONSTRAINT [Item_Fk_Station] FOREIGN KEY ([StationId]) REFERENCES [Station] ([Id])
GO

ALTER TABLE [Order] ADD CONSTRAINT [Order_Fk_Delivery] FOREIGN KEY ([DeliveryId]) REFERENCES [DeliveryLocation] ([Id])
GO

ALTER TABLE [Order] ADD CONSTRAINT [Order_Fk_Store] FOREIGN KEY ([StoreId]) REFERENCES [Store] ([Id])
GO

ALTER TABLE [OrderItem] ADD CONSTRAINT [OrderItem_Fk_Item] FOREIGN KEY ([ItemId]) REFERENCES [Item] ([Id])
GO

ALTER TABLE [OrderItem] ADD CONSTRAINT [OrderItem_Fk_Order] FOREIGN KEY ([OrderId]) REFERENCES [Order] ([Id])
GO
