using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DoughnutDreamsBrewedBeans.Models;
using DoughnutDreamsBrewedBeans.DAL.Abstract;
using DoughnutDreamsBrewedBeans.ViewModels;

namespace DoughnutDreamsBrewedBeans.Controllers
{
    [Route("api/DDBB/")]
    [ApiController]
    public class DDBBApiController : ControllerBase
    {
        private readonly IItemRepository _itemRepo;
        private readonly IOrderItemRepository _orderItemRepo;
        private readonly IOrderRepository _orderRepo;
        private readonly IRepository<Store> _storeRepo;
        private readonly IRepository<DeliveryLocation> _dlvyRepo;

        public DDBBApiController(IItemRepository itemRepo, IOrderItemRepository orderItemRepo, IOrderRepository orderRepo, IRepository<Store> storeRepo, IRepository<DeliveryLocation> dlvyRepo)
        {
            _itemRepo = itemRepo;
            _orderItemRepo = orderItemRepo;
            _orderRepo = orderRepo;
            _storeRepo = storeRepo;
            _dlvyRepo = dlvyRepo;
        }

        // GET: api/DDBB/orders
        [HttpGet("orders")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OrderViewModel>))]
        public ActionResult<IEnumerable<OrderViewModel>> GetOrders()
        {
            try
            {
                var orders = _orderRepo.GetOrdersVM();
                if (orders.Count() == 0)
                {
                    return NoContent();
                }
                return Ok(orders);
            }
            catch
            {
                return NotFound();
            }

        }

        // PUT: api/DDBB/orders/5
        [HttpPut("orders/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status201Created), ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult MarkOrderItemComplete(int id)
        {
            try
            {
                var orderItem = _orderItemRepo.UpdateOrderStatus(id);
                _orderRepo.CheckOrderComplete(orderItem.OrderId);
                return StatusCode(201);
            }
            catch
            {
                return NoContent();
            }
        }

        // GET: api/DDBB/orders/station/1
        [HttpGet("orders/station/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<StationOrderViewModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetStationOrders(int id)
        {
            try
            {
                var orders = _orderRepo.GetStationOrdersVM(id);
                if (orders.Count() == 0)
                {
                    return NoContent();
                }
                return Ok(orders);
            }
            catch
            {
                return NotFound();
            }
        }
    }
}