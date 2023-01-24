using Ecommerce.Interface;
using Ecommerce.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class DeliveryBoyController : Controller
    {
        private readonly IDeliveryBoyRepository _deliveryBoyRepository;
        public DeliveryBoyController(IDeliveryBoyRepository deliveryBoyRepository)
        {
            _deliveryBoyRepository = deliveryBoyRepository;
        }

        [HttpPost]
        [Route("AddDeliveryBoy")]
        //[Authorize(Roles = "SuperAdmin,Seller")]
        public IActionResult AddDeliveryBoy(AddDeliveryBoyModel model)
        {
            try
            {
                var Result = _deliveryBoyRepository.AddDeliveryBoy(model);
                return Ok(Result);
            }catch(Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
        }
    }
}
