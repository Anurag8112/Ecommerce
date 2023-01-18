using Ecommerce.Interface;
using Ecommerce.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class OrderNowController : Controller
    {
        private readonly IOrderRepository _OrderRepository;

        public OrderNowController(IOrderRepository OrderRepository)
        {
            _OrderRepository = OrderRepository;
        }

        [HttpPost]
        [Route("Order")]
      //  [Authorize(Roles = "SuperAdmin")]
        public IActionResult OrderNow(OrderModel model)
        {
            try
            {
                var Result = _OrderRepository.OrderNow(model);

                return Ok(Result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
