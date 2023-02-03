using Ecommerce.Interface;
using Ecommerce.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class ColorController : Controller
    {
        private readonly IColorRepository _colorRepository;

        public ColorController(IColorRepository colorRepository)
        {
            _colorRepository = colorRepository;
        }

        [HttpPost]
        [Route("AddColor")]
        [Authorize(Roles = "SuperAdmin,Buyer")]
        public IActionResult AddColor(AddColorModel model)
        {
            try
            {
                var Result = _colorRepository.AddColor(model);
                return Ok(Result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
        }

        [HttpPatch]
        [Route("EditColor")]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult EditColor(EditColorModel model)
        {
            try
            {
                var Result = _colorRepository.EditColor(model);
                return Ok(Result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteColor")]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult DeleteColor(DeleteColorModel model)
        {
            try
            {
                var Result = _colorRepository.DeleteColor(model);
                return Ok(Result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("ShowAllColors")]
        public IActionResult ShowAllColors()
        {
            try
            {
                var Result = _colorRepository.ShowAllColor();
                return Ok(Result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
        }
    }
}
