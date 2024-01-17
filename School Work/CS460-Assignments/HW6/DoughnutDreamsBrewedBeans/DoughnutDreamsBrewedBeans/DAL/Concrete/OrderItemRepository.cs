using System.Linq;
using Microsoft.EntityFrameworkCore;
using DoughnutDreamsBrewedBeans.Models;
using DoughnutDreamsBrewedBeans.DAL.Abstract;

namespace DoughnutDreamsBrewedBeans.DAL.Concrete
{
    public class OrderItemRepository : Repository<OrderItem>, IOrderItemRepository
    {
        private DbSet<OrderItem> _orderItems;
        public OrderItemRepository(DDBBDbContext context) : base(context)
        {
            _orderItems = context.OrderItems;
        }

        public IQueryable<OrderItem> GetOrderItemsByOrderId(int orderId)
        {
            var orderItems = _orderItems.Where(oi => oi.OrderId == orderId)
                                        .Include(oi => oi.Item);
            if (orderItems.Count() == 0)
            {
                return null;
            }
            return orderItems;
        }

        public OrderItem GetOrderItemById(int id)
        {
            var orderItem = _orderItems.FirstOrDefault(oi => oi.Id == id);
            return orderItem;
        }

        public OrderItem UpdateOrderStatus(int id)
        {
            var orderItem = GetOrderItemById(id);
            if (orderItem == null)
            {
                throw new Exception("Order Item doesn't exist");
            }
            else
            {
                try
                {
                    orderItem.Status = "Complete";
                    AddOrUpdate(orderItem);
                    return orderItem;
                }
                catch (DbUpdateException)
                {
                    throw new DbUpdateException("Error Updating DB");
                }
            }
        }
    }
}