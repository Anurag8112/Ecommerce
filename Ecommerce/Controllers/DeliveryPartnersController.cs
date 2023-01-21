using Ecommerce.Interface;
using Ecommerce.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class DeliveryPartnersController : Controller
    {
        private readonly IDeliveryPartnerRepository _deliveryPartnerRepository;
        public DeliveryPartnersController(IDeliveryPartnerRepository deliveryPartnerRepository)
        {
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
                return Ok(Result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
        }
    }
}
