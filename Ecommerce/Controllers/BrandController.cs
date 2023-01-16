using Ecommerce.Interface;
using Ecommerce.Models.DbModel;
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
    public class BrandController : Controller
    {
        private readonly IBrandRepository _brandRepository;

        public BrandController(IBrandRepository brandRepository)
        {
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

                return Ok(Result);

            }
            catch(Exception ex)
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

                return Ok(Result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
        }


        [HttpGet]
        [Route("ShowAllBrand")]
        [Authorize]
        public IActionResult ShowAllBrand()
        {
            try
            {
                var Result = _brandRepository.ShowAllBrands();

                return Ok(Result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
        }
    }
}
