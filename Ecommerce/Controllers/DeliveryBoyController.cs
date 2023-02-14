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
    public class DeliveryBoyController : Controller
    {
        private readonly IDeliveryBoyRepository _deliveryBoyRepository;
        private readonly ILogger<DeliveryBoyController> _logger;
        public DeliveryBoyController(IDeliveryBoyRepository deliveryBoyRepository, ILogger<DeliveryBoyController> logger)
        {
            _logger = logger;
            _deliveryBoyRepository = deliveryBoyRepository;
        }

        [HttpPost]
        [Route("AddDeliveryBoy")]
        [Authorize(Roles = "SuperAdmin,Seller")]
        public IActionResult AddDeliveryBoy(AddDeliveryBoyModel model)
        {
            try
            {
                var Result = _deliveryBoyRepository.AddDeliveryBoy(model);
                _logger.LogInformation("-------------API Respond Successfully-------------");
                return Ok(Result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
        }

        [HttpPatch]
        [Route("ChangeDeliveryHub")]
        [Authorize(Roles ="SuperAdmin,Seller")]
        public IActionResult ChangeDeliveryHub(ChangeDeliveryHubModel model)
        {
            try
            {
                var Result = _deliveryBoyRepository.ChangeDeliveryHub(model);
                return Ok(Result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
        }

        [HttpDelete]
        [Route("RemoveDeliveryBoy/{UserId}")]
        [Authorize(Roles ="SuperAdmin,Seller")]
        public IActionResult RemoveDeliveryBoy(int UserId)
        {
            try
            {
                var Result = _deliveryBoyRepository.RemoveDeliveryBoy(UserId);
                return Ok(Result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
        }
    }
}
