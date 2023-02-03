using Ecommerce.Interface;
using Ecommerce.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class SizeController : Controller
    {
        private readonly ISizeRepository _sizeRepository;

        public SizeController(ISizeRepository sizeRepository)
        {
            _sizeRepository = sizeRepository;
        }

        [HttpPost]
        [Route("AddSize")]
        //[Authorize(Roles = "SuperAdmin")]
        public IActionResult AddSize(AddSizeModel model)
        {
            try
            {
                var Result = _sizeRepository.AddSize(model);
                return Ok(Result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteSize")]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult DeleteSize(DeleteSizeModel model)
        {
            try
            {
                var Result = _sizeRepository.DeleteSize(model);
                return Ok(Result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
        }


        [HttpPatch]
        [Route("EditSize")]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult EditSize(EditSizeModel model)
        {
            try
            {
                var Result = _sizeRepository.EditSize(model);
                return Ok(Result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
        }


        [HttpGet]
        [Route("ShowAllSize")]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult ShowAllSize()
        {
            try
            {
                var Result = _sizeRepository.ShowAllSizes();
                return Ok(Result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
        }
    }
}
