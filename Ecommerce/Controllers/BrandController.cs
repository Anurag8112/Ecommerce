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
    public class BrandController : Controller
    {
        private readonly IBrandRepository _brandRepository;
        private readonly ILogger<BrandController> _logger;

        public BrandController(IBrandRepository brandRepository, ILogger<BrandController> logger)
        {
            _logger = logger;
            _brandRepository = brandRepository;
        }


        [HttpPost]
        [Route("AddBrand")]
        [Authorize(Roles = "SuperAdmin,Seller")]
        public IActionResult AddBrand(BrandModel model)
        {
            try
            {
                var Result = _brandRepository.AddBrands(model);
                _logger.LogInformation("-----------API Respond Successfully----------");
                return Ok(Result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
        }


        [HttpDelete]
        [Route("RemoveBrand")]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult RemoveBrand(RemoveBrandModel model)
        {
            try
            {
                var Result = _brandRepository.RemoveBrand(model);
                _logger.LogInformation("-----------API Respond Successfully----------");
                return Ok(Result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
        }


        [HttpGet]
        [Route("ShowAllBrand")]
        public IActionResult ShowAllBrand()
        {
            try
            {
                var Result = _brandRepository.ShowAllBrands();
                _logger.LogInformation("-----------API Respond Successfully----------");
                return Ok(Result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
        }
    }
}
