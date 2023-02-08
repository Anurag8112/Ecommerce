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
    public class DeliveryPartnersController : Controller
    {
        private readonly IDeliveryPartnerRepository _deliveryPartnerRepository;
        private readonly ILogger<DeliveryPartnersController> _logger;

        public DeliveryPartnersController(IDeliveryPartnerRepository deliveryPartnerRepository,ILogger<DeliveryPartnersController> logger)
        {
            _logger = logger;
            _deliveryPartnerRepository = deliveryPartnerRepository;
        }

        [HttpPost]
        [Route("AddDeliveryPartners")]
        [Authorize(Roles = "SuperAdmin,Seller")]
        public IActionResult AddDeliveryPartners(DeliveryPartnerModel model)
        {
            try
            {
                var Result = _deliveryPartnerRepository.AddDeliveryPartners(model);
                _logger.LogInformation("-------------API Respond Successfully-------------");
                return Ok(Result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteDeliveryPartners")]
        [Authorize(Roles = "SuperAdmin,Seller")]
        public IActionResult DeleteDeliveryPartners(DeleteDeliveryPartnerModel model)
        {
            try
            {
                var Result = _deliveryPartnerRepository.DeleteDeliveryPartner(model);
                _logger.LogInformation("-------------API Respond Successfully-------------");
                return Ok(Result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
        }

        [HttpPatch]
        [Route("EditDeliveryPartners")]
        [Authorize(Roles = "SuperAdmin,Seller")]
        public IActionResult EditDeliveryPartners(EditDeliveryPartnerModel model)
        {
            try
            {
                var Result = _deliveryPartnerRepository.EditDeliveryPartnerName(model);
                _logger.LogInformation("-------------API Respond Successfully-------------");
                return Ok(Result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("ShowDeliveryPartners")]
        [Authorize(Roles = "SuperAdmin,Seller")]
        public IActionResult ShowDeliveryPartners()
        {
            try
            {
                var Result = _deliveryPartnerRepository.ShowDeliveryPartners();
                _logger.LogInformation("-------------API Respond Successfully-------------");
                return Ok(Result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
        }
    }
}
