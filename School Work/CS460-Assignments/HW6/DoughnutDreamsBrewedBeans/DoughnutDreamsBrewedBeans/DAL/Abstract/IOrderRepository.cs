using DoughnutDreamsBrewedBeans.Models;
using DoughnutDreamsBrewedBeans.ViewModels;

namespace DoughnutDreamsBrewedBeans.DAL.Abstract
{
    public interface IOrderRepository : IRepository<Order>
    {
        IQueryable<Order> GetCurrentOrders();
        Order GetOrderById(int id);
        IQueryable<Order> GetAllOrders();

        IEnumerable<OrderViewModel> GetOrdersVM();
        string GetOrderTotal(int orderId);
        bool CheckOrderComplete(int orderId);
        IEnumerable<StationOrderViewModel> GetStationOrdersVM(int stationId);
    }
}