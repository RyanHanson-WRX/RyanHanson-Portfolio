using DoughnutDreamsBrewedBeans.DAL.Abstract;
using DoughnutDreamsBrewedBeans.DAL.Concrete;
using DoughnutDreamsBrewedBeans.Models;
using DoughnutDreamsBrewedBeans.ViewModels;
using Microsoft.EntityFrameworkCore;
using Moq;
using NuGet.Protocol;

namespace DoughnutDreamsBrewedBeans_Tests;

public class OrderRepository_Tests
{
    private Mock<DDBBDbContext> _mockContext;
    private Mock<DbSet<Order>> _mockOrderDbSet;
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
            new Item {Id = 17, Price = 3.99M, StationId = 2, Name = "Apple Fritter", Description = "Deep-fried pastry filled with spiced apples"},
            new Item {Id = 4, Price = 5.99M, StationId = 1, Name = "Mocha Frappuccino", Description = "Creamy coffee-chocolate blend with whipped cream"},
            new Item {Id = 10, Price = 4.49M, StationId = 1, Name = "Chai Latte", Description = "Spiced black tea with steamed milk and foam"},
            new Item {Id = 1, Price = 4.99M, StationId = 1, Name = "Caramel Macchiato", Description = "Rich espresso with caramel and steamed milk"},
            new Item {Id = 30, Price = 4.99M, StationId = 1, Name = "Iced Chai Latte", Description = "Chilled spiced tea with milk and ice"},
            new Item {Id = 20, Price = 5.49M, StationId = 1, Name = "Chocolate Milkshake", Description = "Rich chocolate shake topped with whipped cream"},
            new Item {Id = 36, Price = 4.49M, StationId = 1, Name = "Hot Chocolate", Description = "Creamy and indulgent hot chocolate"},
            new Item {Id = 16, Price = 2.99M, StationId = 1, Name = "Americano", Description = "Strong black coffee with hot water"},
            new Item {Id = 12, Price = 5.49M, StationId = 2, Name = "Breakfast Burrito", Description = "Scrambled eggs, cheese, and veggies in a tortilla"},
            new Item {Id = 9, Price = 4.49M, StationId = 2, Name = "Greek Yogurt Parfait", Description = "Layers of yogurt, granola, and fresh berries"},
            new Item {Id = 18, Price = 7.49M, StationId = 2, Name = "Caprese Salad", Description = "Fresh tomatoes, mozzarella, and basil drizzled with balsamic"},
            new Item {Id = 35, Price = 6.99M, StationId = 2, Name = "Quinoa Salad", Description = "Quinoa, mixed vegetables, and lemon vinaigrette"},
            new Item {Id = 23, Price = 5.49M, StationId = 1, Name = "Pomegranate Paradise", Description = "Pomegranate and berry smoothie with a hint of mint"},
            new Item {Id = 14, Price = 3.49M, StationId = 2, Name = "Chocolate Croissant", Description = "Buttery croissant with a chocolate filling"}
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

        _orders.ForEach(o => o.OrderItems = _orderItems.Where(oi => oi.OrderId == o.Id).ToList());
        _orders.ForEach(o => o.Store = _stores.Where(s => s.Id == o.StoreId).FirstOrDefault());
        _items.ForEach(i => i.Station = _stations.Where(s => s.Id == i.StationId).FirstOrDefault());
        _orders.ForEach(o => o.Delivery = _dlvys.Where(d => d.Id == o.DeliveryId).FirstOrDefault());
        _orderItems.ForEach(oi => oi.Item = _items.Where(o => o.Id == oi.ItemId).FirstOrDefault());

        _mockContext = new Mock<DDBBDbContext>();
        _mockOrderDbSet = MockDbHelper.GetMockDbSet(_orders.AsQueryable());
        _mockContext.Setup(ctx => ctx.Orders).Returns(_mockOrderDbSet.Object);
        _mockContext.Setup(ctx => ctx.Set<Order>()).Returns(_mockOrderDbSet.Object);
    }

    [Test]
    public void GetAllOrders_ReturnsAllOrders()
    {
        // Arrange
        IOrderRepository repo = new OrderRepository(_mockContext.Object);

        // Act
        var allOrders = repo.GetAllOrders();

        // Assert
        Assert.That(allOrders, Is.EqualTo(_mockOrderDbSet.Object));
    }

    [Test]
    public void GetAllOrders_ReturnsCorrectAmountOfOrders()
    {
        // Arrange
        IOrderRepository repo = new OrderRepository(_mockContext.Object);

        // Act
        var allOrders = repo.GetAllOrders();

        // Assert
        Assert.That(allOrders.Count(), Is.EqualTo(5));
    }    
        
    [Test]
    public void GetCurrentOrders_ReturnsCorrectAmountOfCurrentOrders()
    {
        // Arrange
        IOrderRepository repo = new OrderRepository(_mockContext.Object);

        // Act
        var allCurrOrders = repo.GetCurrentOrders();

        // Assert
        Assert.That(allCurrOrders.Count(), Is.EqualTo(4));
    }

    [Test]
    public void GetCurrentOrders_DoesNotReturnCompletedOrders()
    {
        // Arrange
        IOrderRepository repo = new OrderRepository(_mockContext.Object);

        // Act
        var allCurrOrders = repo.GetCurrentOrders();
        var completedOrder = repo.GetOrderById(5);
        // Assert
        Assert.That(allCurrOrders.Contains(completedOrder), Is.EqualTo(false));
    }

    [Test]
    public void GetOrderById_ReturnsCorrectOrder()
    {
        // Arrange
        IOrderRepository repo = new OrderRepository(_mockContext.Object);

        // Act
        var order = repo.GetOrderById(1);

        // Assert
        Assert.That(order, Is.EqualTo(_orders.FirstOrDefault(o => o.Id == 1)));
    }

    [Test]
    public void GetOrderById_ReturnsNullForIncorrectOrder()
    {
        // Arrange
        IOrderRepository repo = new OrderRepository(_mockContext.Object);

        // Act
        var order = repo.GetOrderById(10);

        // Assert
        Assert.That(order, Is.EqualTo(null));
    }

    [Test]
    public void GetOrderTotal_ReturnsCorrectTotal()
    {
        // Arrange
        IOrderRepository repo = new OrderRepository(_mockContext.Object);

        // Act
        var orderTotal = repo.GetOrderTotal(1);

        // Assert
        Assert.That(orderTotal, Is.EqualTo("$29.94"));
    }

    [Test]
    public void GetOrderTotal_Returns0WhenGivenInvalidId()
    {
        // Arrange
        IOrderRepository repo = new OrderRepository(_mockContext.Object);

        // Act
        var orderTotal = repo.GetOrderTotal(20);

        // Assert
        Assert.That(orderTotal, Is.EqualTo("$0.00"));
    }
    
    [Test]
    public void GetOrdersVM_ReturnsCorrectViewModels()
    {
        // Arrange
        IOrderRepository repo = new OrderRepository(_mockContext.Object);
        var orderItemVM1 = new OrderItemViewModel
        {
            Id = 1,
            ItemName = "Apple Fritter",
            Quantity = 3,
            Status = "In Progress",
            StationName = "Pastry Station"
        };
        var orderItemVM2 = new OrderItemViewModel
        {
            Id = 2,
            ItemName = "Mocha Frappuccino",
            Quantity = 3,
            Status = "In Progress",
            StationName = "Espresso Station"
        };
        var orderItems = new List<OrderItemViewModel> {orderItemVM1, orderItemVM2};

        var expected = new OrderViewModel
        {
            Id = 1,
            CustomerName = "Brianna",
            Store = "Doughnut Dreams & Brewed Beans - Downtown",
            DeliveryLocation = "Main Counter",
            Complete = "false",
            TotalPrice = "$29.94",
            OrderItems = orderItems
        };

        // Act
        var ordersVM = repo.GetOrdersVM();
        var firstVM = ordersVM.First();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(firstVM.Id, Is.EqualTo(expected.Id));
            Assert.That(firstVM.CustomerName, Is.EqualTo(expected.CustomerName));
            Assert.That(firstVM.Store, Is.EqualTo(expected.Store));
            Assert.That(firstVM.DeliveryLocation, Is.EqualTo(expected.DeliveryLocation));
            Assert.That(firstVM.Complete, Is.EqualTo(expected.Complete));
            Assert.That(firstVM.TotalPrice, Is.EqualTo(expected.TotalPrice));
            Assert.That(firstVM.OrderItems[0].Id, Is.EqualTo(expected.OrderItems[0].Id));
            Assert.That(firstVM.OrderItems[0].ItemName, Is.EqualTo(expected.OrderItems[0].ItemName));
            Assert.That(firstVM.OrderItems[0].Quantity, Is.EqualTo(expected.OrderItems[0].Quantity));
            Assert.That(firstVM.OrderItems[0].Status, Is.EqualTo(expected.OrderItems[0].Status));
            Assert.That(firstVM.OrderItems[0].StationName, Is.EqualTo(expected.OrderItems[0].StationName));
            Assert.That(firstVM.OrderItems[1].Id, Is.EqualTo(expected.OrderItems[1].Id));
            Assert.That(firstVM.OrderItems[1].ItemName, Is.EqualTo(expected.OrderItems[1].ItemName));
            Assert.That(firstVM.OrderItems[1].Quantity, Is.EqualTo(expected.OrderItems[1].Quantity));
            Assert.That(firstVM.OrderItems[1].Status, Is.EqualTo(expected.OrderItems[1].Status));
            Assert.That(firstVM.OrderItems[1].StationName, Is.EqualTo(expected.OrderItems[1].StationName));
        });

    }

    [Test]
    public void CheckOrderComplete_ReturnsFalseForIncompleteOrder()
    {
        // Arrange
        IOrderRepository repo = new OrderRepository(_mockContext.Object);

        // Act
        var result = repo.CheckOrderComplete(1);

        // Assert
        Assert.That(result, Is.EqualTo(false));
    }

    [Test]
    public void CheckOrderComplete_ReturnsTrueForCompleteOrder()
    {
        // Arrange
        IOrderRepository repo = new OrderRepository(_mockContext.Object);

        // Act
        var result = repo.CheckOrderComplete(5);

        // Assert
        Assert.That(result, Is.EqualTo(true));
    }

    [Test]
    public void GetStationOrdersVM_ReturnsCorrectViewModels()
    {
        // Arrange
        IOrderRepository repo = new OrderRepository(_mockContext.Object);
        var stationOrderItemVM = new StationOrderItemViewModel
        {
            Id = 2,
            ItemName = "Mocha Frappuccino",
            Quantity = 3,
            Status = "In Progress",
            Description = "Creamy coffee-chocolate blend with whipped cream"
        };
        var orderItems = new List<StationOrderItemViewModel> {stationOrderItemVM};

        var expected = new StationOrderViewModel
        {
            Id = 1,
            CustomerName = "Brianna",
            DeliveryLocation = "Main Counter",
            Store = "Doughnut Dreams & Brewed Beans - Downtown",
            OrderItems = orderItems
        };

        // Act
        var ordersVM = repo.GetStationOrdersVM(1);
        var firstVM = ordersVM.First();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(firstVM.Id, Is.EqualTo(expected.Id));
            Assert.That(firstVM.CustomerName, Is.EqualTo(expected.CustomerName));
            Assert.That(firstVM.DeliveryLocation, Is.EqualTo(expected.DeliveryLocation));
            Assert.That(firstVM.OrderItems[0].Id, Is.EqualTo(expected.OrderItems[0].Id));
            Assert.That(firstVM.OrderItems[0].Quantity, Is.EqualTo(expected.OrderItems[0].Quantity));
            Assert.That(firstVM.OrderItems[0].Status, Is.EqualTo(expected.OrderItems[0].Status));
            Assert.That(firstVM.OrderItems[0].Description, Is.EqualTo(expected.OrderItems[0].Description));
            Assert.That(firstVM.OrderItems[0].ItemName, Is.EqualTo(expected.OrderItems[0].ItemName));
            Assert.That(firstVM.Store, Is.EqualTo(expected.Store));
        });
    }
}