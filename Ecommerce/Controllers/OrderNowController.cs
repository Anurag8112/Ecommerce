using Ecommerce.Interface;
using Ecommerce.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class OrderNowController : Controller
    {
        private readonly ILogger<OrderNowController> _logger;
        private readonly IOrderRepository _OrderRepository;
        public OrderNowController(IOrderRepository OrderRepository, ILogger<OrderNowController> logger)
        {
            _logger = logger;
            _OrderRepository = OrderRepository;
        }

        [HttpPost]
        [Route("Order")]
        [Authorize(Roles = "Buyer")]
        public IActionResult OrderNow(OrderModel model)
        {
            try
            {
                var Result = _OrderRepository.OrderNow(model);
                _logger.LogInformation("-------------API Respond Successfully-------------");
                return Ok(Result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("OrderFromCart")]
        [Authorize(Roles ="Buyer")]
        public IActionResult OrderFromCart(CartOrderModel model)
        {
            try
            {
                var Result = _OrderRepository.OrderNowByCart(model);
                _logger.LogInformation("-------------API Respond Successfully-------------");
                return Ok(Result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
