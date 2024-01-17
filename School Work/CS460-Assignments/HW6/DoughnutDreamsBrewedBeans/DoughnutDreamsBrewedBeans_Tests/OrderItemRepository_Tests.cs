using DoughnutDreamsBrewedBeans.DAL.Abstract;
using DoughnutDreamsBrewedBeans.DAL.Concrete;
using DoughnutDreamsBrewedBeans.Models;
using DoughnutDreamsBrewedBeans.ViewModels;
using Microsoft.EntityFrameworkCore;
using Moq;
using NuGet.Protocol;

namespace DoughnutDreamsBrewedBeans_Tests;

public class OrderItemRepository_Tests
{
    private Mock<DDBBDbContext> _mockContext;
    private Mock<DbSet<OrderItem>> _mockOrderItemDbSet;
    private List<Order> _orders;
    private List<OrderItem> _orderItems;
    private List<Item> _items;
    private List<Station> _stations;
    private List<DeliveryLocation> _dlvys;
    private List<Store> _stores;

[SetUp]
    public void Setup()
    {
        _orders = new List<Order>
        {
            new Order { Id = 1, Complete = "false", DeliveryId = 1, Name = "Brianna", StoreId = 1, TotalPrice = 29.94M},
            new Order { Id = 2, Complete = "false", DeliveryId = 3, Name = "Shannon", StoreId = 1, TotalPrice = 37.92M},
            new Order { Id = 3, Complete = "false", DeliveryId = 1, Name = "Paulina", StoreId = 1, TotalPrice = 27.94M},
            new Order { Id = 4, Complete = "false", DeliveryId = 1, Name = "Troy", StoreId = 1, TotalPrice = 49.42M},
            new Order { Id = 5, Complete = "true", DeliveryId = 2, Name = "Natalie", StoreId = 1, TotalPrice = 10.47M}
        };
        _orderItems = new List<OrderItem>
        {
            new OrderItem {Id = 1, ItemId = 17, OrderId = 1, Quantity = 3, Status = "In Progress"},
            new OrderItem {Id = 2, ItemId = 4, OrderId = 1, Quantity = 3, Status = "In Progress"},
            new OrderItem {Id = 3, ItemId = 10, OrderId = 2, Quantity = 3, Status = "In Progress"},
            new OrderItem {Id = 4, ItemId = 1, OrderId = 2, Quantity = 1, Status = "In Progress"},
            new OrderItem {Id = 5, ItemId = 30, OrderId = 2, Quantity = 1, Status = "In Progress"},
            new OrderItem {Id = 6, ItemId = 20, OrderId = 2, Quantity = 1, Status = "In Progress"},
            new OrderItem {Id = 7, ItemId = 36, OrderId = 2, Quantity = 2, Status = "In Progress"},
            new OrderItem {Id = 8, ItemId = 16, OrderId = 3, Quantity = 2, Status = "In Progress"},
            new OrderItem {Id = 9, ItemId = 12, OrderId = 3, Quantity = 1, Status = "In Progress"},
            new OrderItem {Id = 10, ItemId = 9, OrderId = 3, Quantity = 2, Status = "In Progress"},
            new OrderItem {Id = 11, ItemId = 18, OrderId = 3, Quantity = 1, Status = "In Progress"},
            new OrderItem {Id = 12, ItemId = 18, OrderId = 4, Quantity = 2, Status = "In Progress"},
            new OrderItem {Id = 13, ItemId = 17, OrderId = 4, Quantity = 1, Status = "In Progress"},
            new OrderItem {Id = 14, ItemId = 35, OrderId = 4, Quantity = 2, Status = "In Progress"},
            new OrderItem {Id = 15, ItemId = 23, OrderId = 4, Quantity = 3, Status = "In Progress"},
            new OrderItem {Id = 16, ItemId = 14, OrderId = 5, Quantity = 3, Status = "Complete"},
        };

        _items = new List<Item>
        {
            new Item {Id = 17, Price = 3.99M, StationId = 2},
            new Item {Id = 4, Price = 5.99M, StationId = 1},
            new Item {Id = 10, Price = 4.49M, StationId = 1},
            new Item {Id = 1, Price = 4.99M, StationId = 1},
            new Item {Id = 30, Price = 4.99M, StationId = 1},
            new Item {Id = 20, Price = 5.49M, StationId = 1},
            new Item {Id = 36, Price = 4.49M, StationId = 1},
            new Item {Id = 16, Price = 2.99M, StationId = 1},
            new Item {Id = 12, Price = 5.49M, StationId = 2},
            new Item {Id = 9, Price = 4.49M, StationId = 2},
            new Item {Id = 18, Price = 7.49M, StationId = 2},
            new Item {Id = 35, Price = 6.99M, StationId = 2},
            new Item {Id = 23, Price = 5.49M, StationId = 1},
            new Item {Id = 14, Price = 3.49M, StationId = 2}
        };

        _dlvys = new List<DeliveryLocation>
        {
            new DeliveryLocation {Id = 1, Name = "Main Counter"},
            new DeliveryLocation {Id = 2, Name = "Drive-Through"},
            new DeliveryLocation {Id = 3, Name = "Walk-in Counter"}
        };

        _stations = new List<Station>
        {
            new Station {Id = 1, Name = "Espresso Station"},
            new Station {Id = 2, Name = "Pastry Station"},
            new Station {Id = 3, Name = "Donut Station"}
        };

        _stores = new List<Store>
        {
            new Store {Id = 1, Name = "Doughnut Dreams & Brewed Beans - Downtown"}
        };

        _orderItems.ForEach(oi => oi.Order = _orders.Where(o => o.Id == oi.OrderId).FirstOrDefault());
        _orders.ForEach(o => o.Store = _stores.Where(s => s.Id == o.StoreId).FirstOrDefault());
        _items.ForEach(i => i.Station = _stations.Where(s => s.Id == i.StationId).FirstOrDefault());
        _orders.ForEach(o => o.Delivery = _dlvys.Where(d => d.Id == o.DeliveryId).FirstOrDefault());
        _orderItems.ForEach(oi => oi.Item = _items.Where(o => o.Id == oi.ItemId).FirstOrDefault());

        _mockContext = new Mock<DDBBDbContext>();
        _mockOrderItemDbSet = MockDbHelper.GetMockDbSet(_orderItems.AsQueryable());
        _mockContext.Setup(ctx => ctx.OrderItems).Returns(_mockOrderItemDbSet.Object);
        _mockContext.Setup(ctx => ctx.Set<OrderItem>()).Returns(_mockOrderItemDbSet.Object);
    }

    [Test]
    public void GetOrderItemsByOrderId_ReturnsCorrectAmountOfOrderItems()
    {
        // Arrange
        IOrderItemRepository repo = new OrderItemRepository(_mockContext.Object);

        // Act
        var orderItems = repo.GetOrderItemsByOrderId(2);

        // Assert
        Assert.That(orderItems.Count(), Is.EqualTo(5));
    }

    [Test]
    public void GetOrderItemsByOrderId_ReturnsNullForInvalidOrderId()
    {
        // Arrange
        IOrderItemRepository repo = new OrderItemRepository(_mockContext.Object);

        // Act
        var orderItems = repo.GetOrderItemsByOrderId(16);

        // Assert
        Assert.That(orderItems, Is.EqualTo(null));
    }

    [Test]
    public void GetOrderItemById_ReturnsCorrectOrderItem()
    {
        // Arrange
        IOrderItemRepository repo = new OrderItemRepository(_mockContext.Object);

        // Act
        var orderItem = repo.GetOrderItemById(1);

        // Assert
        Assert.That(orderItem, Is.EqualTo(_orderItems[0]));
    }

    [Test]
    public void GetOrderItemById_ReturnsNullForInvalidId()
    {
        // Arrange
        IOrderItemRepository repo = new OrderItemRepository(_mockContext.Object);

        // Act
        var orderItem = repo.GetOrderItemById(49);

        // Assert
        Assert.That(orderItem, Is.EqualTo(null));
    }

    [Test]
    public void UpdateOrderStatus_UpdatesOrderStatus()
    {
        // Arrange
        IOrderItemRepository repo = new OrderItemRepository(_mockContext.Object);

        // Act
        var orderItem = repo.UpdateOrderStatus(1);

        // Assert
        Assert.That(orderItem.Status, Is.EqualTo("Complete"));
    }

    [Test]
    public void UpdateOrderStatus_ThrowsExceptionForInvalidId()
    {
        // Arrange
        IOrderItemRepository repo = new OrderItemRepository(_mockContext.Object);

        // Act / Assert
        Assert.That(() => repo.UpdateOrderStatus(49), Throws.Exception);
    }
}