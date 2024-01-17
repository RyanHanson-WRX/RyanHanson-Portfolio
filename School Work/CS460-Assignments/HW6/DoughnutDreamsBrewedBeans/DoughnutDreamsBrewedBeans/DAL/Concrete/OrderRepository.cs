using System.Linq;
using Microsoft.EntityFrameworkCore;
using DoughnutDreamsBrewedBeans.Models;
using DoughnutDreamsBrewedBeans.ViewModels;
using DoughnutDreamsBrewedBeans.DAL.Abstract;
using System.Globalization;

namespace DoughnutDreamsBrewedBeans.DAL.Concrete
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private DbSet<Order> _orders;
        public OrderRepository(DDBBDbContext context) : base(context)
        {
            _orders = context.Orders;
        }

        public IQueryable<Order> GetCurrentOrders()
        {
            var orders = _orders.Where(o => o.Complete == "false")
                                .OrderBy(o => o.Id);
            return orders;
        }

        public Order GetOrderById(int id)
        {
            var order = _orders.Where(o => o.Id == id).FirstOrDefault();
            return order;
        }

        public IQueryable<Order> GetAllOrders()
        {
            var orders = _orders;
            return orders;
        }

        public string GetOrderTotal(int orderId)
        {
            var orderItems = _orders.Where(o => o.Id == orderId)
                                    .Include(o => o.OrderItems)
                                    .SelectMany(o => o.OrderItems)
                                    .Include(oi => oi.Item);
            decimal orderTotal = 0;
            foreach (var orderItem in orderItems)
            {
                orderTotal += orderItem.Item.Price * orderItem.Quantity;
            }
            if (orderTotal == 0)
            {
                return "$0.00";
            }
            else
            {
                return orderTotal.ToString("C2", CultureInfo.CurrentCulture);
            }
        }

        public IEnumerable<OrderViewModel> GetOrdersVM()
        {
            var orders = GetCurrentOrders().ToList();
            if (!orders.Any())
            {
                IEnumerable<OrderViewModel> currOrders = new List<OrderViewModel>();
                return currOrders;
            }
            var orderViewModels = new List<OrderViewModel>();
            foreach (var order in orders)
            {
                var orderItems = order.OrderItems.ToList();
                var orderItemsVM = new List<OrderItemViewModel>();
                foreach (var oi in orderItems)
                {   
                    var orderItemViewModel = new OrderItemViewModel
                    {
                        Id = oi.Id,
                        ItemName = oi.Item.Name,
                        Quantity = oi.Quantity,
                        Status = oi.Status,
                        StationName = oi.Item.Station.Name,
                    };
                    orderItemsVM.Add(orderItemViewModel);
                }
                var orderViewModel = new OrderViewModel
                {
                    Id = order.Id,
                    CustomerName = order.Name,
                    Store = order.Store.Name,
                    DeliveryLocation = order.Delivery.Name,
                    Complete = order.Complete,
                    TotalPrice = GetOrderTotal(order.Id),
                    OrderItems = orderItemsVM
                };
                    orderViewModels.Add(orderViewModel);
                }
                return orderViewModels;
            }

        public IEnumerable<StationOrderViewModel> GetStationOrdersVM(int stationId)
        {
            var orders = GetCurrentOrders().ToList();
            if (!orders.Any())
            {
                IEnumerable<StationOrderViewModel> currOrders = new List<StationOrderViewModel>();
                return currOrders;
            }
            var stationOrdersVM = new List<StationOrderViewModel>();
            foreach (var order in orders)
            {
                var orderItems = order.OrderItems.ToList();
                var stationOrderItemsVM = new List<StationOrderItemViewModel>();
                foreach (var oi in orderItems)
                {
                    if (oi.Item.StationId == stationId)
                    {
                        var orderItemViewModel = new StationOrderItemViewModel
                        {
                            Id = oi.Id,
                            ItemName = oi.Item.Name,
                            Description = oi.Item.Description,
                            Quantity = oi.Quantity,
                            Status = oi.Status,
                        };
                        stationOrderItemsVM.Add(orderItemViewModel);
                    }
                }
                var itemCompleteCount = stationOrderItemsVM.Where(oi => oi.Status == "Complete").Count();
                if (itemCompleteCount == stationOrderItemsVM.Count())
                {
                    CheckOrderComplete(order.Id);
                    continue;
                }
                if (stationOrderItemsVM.Any())
                {
                    var stationOrderViewModel = new StationOrderViewModel
                    {
                        Id = order.Id,
                        CustomerName = order.Name,
                        DeliveryLocation = order.Delivery.Name,
                        Store = order.Store.Name,
                        OrderItems = stationOrderItemsVM
                    };
                    stationOrdersVM.Add(stationOrderViewModel);
                }
            }
            return stationOrdersVM;
        }

        public bool CheckOrderComplete(int orderId)
        {
            var order = _orders.FirstOrDefault(o => o.Id == orderId);
            var orderItems = order.OrderItems.ToList();
            foreach (var item in orderItems)
            {
                if (item.Status == "In Progress")
                {
                    return false;
                }
            }
            try
            {
                order.Complete = "true";
                AddOrUpdate(order);
                return true;
            }
            catch (DbUpdateException)
            {
                throw new DbUpdateException("Error Updating Order");
            }
        }
    }
}