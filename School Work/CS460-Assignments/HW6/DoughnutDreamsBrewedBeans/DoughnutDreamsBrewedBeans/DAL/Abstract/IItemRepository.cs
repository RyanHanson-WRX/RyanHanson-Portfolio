using DoughnutDreamsBrewedBeans.Models;

namespace DoughnutDreamsBrewedBeans.DAL.Abstract
{
    public interface IItemRepository : IRepository<Item>
    {
        IQueryable<Item> GetAllItems();
        IQueryable<Item> GetItemsByStationId(int stationId);
        Item GetItemById(int id);
    }
}