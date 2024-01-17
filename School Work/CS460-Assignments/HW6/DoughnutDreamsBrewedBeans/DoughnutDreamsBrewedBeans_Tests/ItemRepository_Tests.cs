using DoughnutDreamsBrewedBeans.DAL.Abstract;
using DoughnutDreamsBrewedBeans.DAL.Concrete;
using DoughnutDreamsBrewedBeans.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using NuGet.Protocol;

namespace DoughnutDreamsBrewedBeans_Tests;

public class ItemRepository_Tests
{

    private Mock<DDBBDbContext> _mockContext;
    private Mock<DbSet<Item>> _mockItemDbSet;
    private List<Item> _items;
    private List<Station> _stations;

    [SetUp]
    public void Setup()
    {
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

        _stations = new List<Station>
        {
            new Station {Id = 1, Name = "Espresso Station"},
            new Station {Id = 2, Name = "Pastry Station"},
            new Station {Id = 3, Name = "Donut Station"}
        };

        _items.ForEach(i => i.Station = _stations.Where(s => s.Id == i.StationId).FirstOrDefault());

        _mockContext = new Mock<DDBBDbContext>();
        _mockItemDbSet = MockDbHelper.GetMockDbSet(_items.AsQueryable());
        _mockContext.Setup(ctx => ctx.Items).Returns(_mockItemDbSet.Object);
        _mockContext.Setup(ctx => ctx.Set<Item>()).Returns(_mockItemDbSet.Object);
    }

    [Test]
    public void GetAllItems_ReturnsAllItems()
    {
        // Arrange
        IItemRepository repo = new ItemRepository(_mockContext.Object);

        // Act
        var allItems = repo.GetAllItems();

        // Assert
        Assert.That(allItems, Is.EqualTo(_mockItemDbSet.Object));
    }

    [Test]
    public void GetAllItems_ReturnsCorrectItemCount()
    {
        // Arrange
        IItemRepository repo = new ItemRepository(_mockContext.Object);

        // Act
        var allItems = repo.GetAllItems();

        // Assert
        Assert.That(allItems.Count(), Is.EqualTo(14));
    }

    [Test]
    public void GetItemsByStationId_ReturnsCorrectItems()
    {
        // Arrange
        IItemRepository repo = new ItemRepository(_mockContext.Object);

        // Act
        var result = repo.GetItemsByStationId(1);

        // Assert
        Assert.That(result.Count(), Is.EqualTo(8));
    }

    [Test]
    public void GetItemsByStationId_ReturnsNullForInvalidStationId()
    {
        // Arrange
        IItemRepository repo = new ItemRepository(_mockContext.Object);

        // Act
        var result = repo.GetItemsByStationId(4);

        // Assert
        Assert.That(result, Is.EqualTo(null));
    }

    [Test]
    public void GetItemById_ReturnsCorrectItem()
    {
        // Arrange
        IItemRepository repo = new ItemRepository(_mockContext.Object);

        // Act
        var item = repo.GetItemById(1);

        // Assert
        Assert.That(item, Is.EqualTo(_items[3]));
    }

    [Test]
    public void GetItemById_ReturnsNullForInvalidItemId()
    {
        // Arrange
        IItemRepository repo = new ItemRepository(_mockContext.Object);

        // Act
        var item = repo.GetItemById(41);

        // Assert
        Assert.That(item, Is.EqualTo(null));
    }
}