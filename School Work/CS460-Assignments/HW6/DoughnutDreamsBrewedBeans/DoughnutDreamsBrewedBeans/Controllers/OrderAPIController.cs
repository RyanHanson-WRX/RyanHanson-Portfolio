using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DoughnutDreamsBrewedBeans.Models;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using DoughnutDreamsBrewedBeans.Services;
using NuGet.Protocol;

namespace DoughnutDreamsBrewedBeans.Controllers
{

    [ApiController]
    [Route("api/")]
    public class OrderAPIController : ControllerBase
    {
        private readonly ILogger<OrderAPIController> _logger;
        private readonly IOrderService _orderService;

        public OrderAPIController(ILogger<OrderAPIController> logger, IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }

        // POST: api/order
        [HttpPost("order")]
        [ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public ActionResult Order(JsonElement response)
        {
            try
            {
                _orderService.CreateOrder(response);
            }
            catch
            {
                return NotFound();
            }
            return Ok();
        }

    }
}