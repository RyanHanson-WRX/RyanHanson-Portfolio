using DoughnutDreamsBrewedBeans.Models;

namespace DoughnutDreamsBrewedBeans.DAL.Abstract
{
    public interface IOrderItemRepository : IRepository<OrderItem>
    {
        IQueryable<OrderItem> GetOrderItemsByOrderId(int orderId);
        OrderItem GetOrderItemById(int id);
        OrderItem UpdateOrderStatus(int id);
    }
}