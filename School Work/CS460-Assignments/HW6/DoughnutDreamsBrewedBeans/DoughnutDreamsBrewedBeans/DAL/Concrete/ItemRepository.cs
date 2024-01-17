using System.Linq;
using Microsoft.EntityFrameworkCore;
using DoughnutDreamsBrewedBeans.Models;
using DoughnutDreamsBrewedBeans.DAL.Abstract;

namespace DoughnutDreamsBrewedBeans.DAL.Concrete
{
    public class ItemRepository : Repository<Item>, IItemRepository
    {
        private DbSet<Item> _items;
        public ItemRepository(DDBBDbContext context) : base(context)
        {
            _items = context.Items;
        }

        public IQueryable<Item> GetAllItems()
        {
            var items = _items;
            return items;
        }

        public IQueryable<Item> GetItemsByStationId(int stationId)
        {
            var items = _items.Where(i => i.StationId == stationId);
            if (items.Count() == 0)
            {
                return null;
            }
            return items;
        }

        public Item GetItemById(int id)
        {
            var item = _items.FirstOrDefault(i => i.Id == id);
            return item;
        }
    }
}