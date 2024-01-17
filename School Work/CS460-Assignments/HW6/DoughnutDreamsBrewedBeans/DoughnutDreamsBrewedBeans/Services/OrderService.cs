using DoughnutDreamsBrewedBeans.Models;
using System.Text.Json;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using DoughnutDreamsBrewedBeans.DAL.Abstract;

namespace DoughnutDreamsBrewedBeans.Services
{
    class JsonItem
    {
        public int id { get; set; }
        public int qty { get; set; }
    }

    class JsonOrder
    {
        public int store { get; set; }
        public int dlvy { get; set; }
        public string name { get; set; }
        public List<JsonItem> items { get; set; }
    }
    public class OrderService : IOrderService
    {
        private readonly IOrderItemRepository _orderItemRepo;
        private readonly IOrderRepository _orderRepo;
        private readonly IItemRepository _itemRepo;
        private readonly ILogger<OrderService> _logger;

        public OrderService(IOrderRepository orderRepo, IOrderItemRepository orderItemRepo, ILogger<OrderService> logger, IItemRepository itemRepo)
        {
            _orderRepo = orderRepo;
            _orderItemRepo = orderItemRepo;
            _itemRepo = itemRepo;
            _logger = logger;
        }

        public void CreateOrder(JsonElement response)
        {   
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            try
            {
                JsonOrder currOrder = JsonSerializer.Deserialize<JsonOrder>(response, options);
                List<int> itemIds = _itemRepo.GetAllItems().Select(i => i.Id).ToList();
                foreach (JsonItem item in currOrder.items)
                {
                    if (!itemIds.Contains(item.id))
                    {
                        throw new Exception("Invalid Items Entered.");
                    }
                }
                Order order = new Order
                {
                    StoreId = currOrder.store,
                    DeliveryId = currOrder.dlvy,
                    Name = currOrder.name,
                    Complete = "false",
                };
                _orderRepo.AddOrUpdate(order);
                List<OrderItem> orderItems = new List<OrderItem>();
                foreach (JsonItem item in currOrder.items)
                {
                    OrderItem orderItem = new OrderItem{
                        ItemId = item.id,
                        Quantity = item.qty,
                        OrderId = order.Id,
                        Status = "In Progress"
                    };
                    orderItems.Add(orderItem);
                }
                foreach (OrderItem orderItem in orderItems)
                {
                    _orderItemRepo.AddOrUpdate(orderItem);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error deserializing orders");
                throw new Exception("Error deserializing orders");
            }
        }
    }
}