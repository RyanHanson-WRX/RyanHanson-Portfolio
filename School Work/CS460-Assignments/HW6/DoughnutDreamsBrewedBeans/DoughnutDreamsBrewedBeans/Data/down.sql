ALTER TABLE [Item] DROP CONSTRAINT [Item_Fk_Station];
ALTER TABLE [Order] DROP CONSTRAINT [Order_Fk_Delivery];
ALTER TABLE [Order] DROP CONSTRAINT [Order_Fk_Store];
ALTER TABLE [OrderItem] DROP CONSTRAINT [OrderItem_Fk_Item];
ALTER TABLE [OrderItem] DROP CONSTRAINT [OrderItem_Fk_Order];

DROP TABLE [Store];
DROP TABLE [DeliveryLocation];
DROP TABLE [Station];
DROP TABLE [Item];
DROP TABLE [Order];
DROP TABLE [OrderItem];