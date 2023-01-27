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
    public class GenderController : Controller
    {
        private readonly IGenderRepository _genderRepository;

        public GenderController (IGenderRepository genderRepository)
        {
            _genderRepository = genderRepository;
        }

        [HttpPost]
        [Route("AddGender")]
        //[Authorize(Roles = "SuperAdmin")]
        public IActionResult AddGender(AddGenderModel model)
        {
            try
            {
                var Result = _genderRepository.AddGender(model);
                return Ok(Result);

            }catch(Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
        }

        [HttpDelete]
        [Route("RemoveGender")]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult RemoveGender(DeleteGenderModel model)
        {
            try
            {
                var Result = _genderRepository.DeleteGender(model);
                return Ok(Result);

            }catch(Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
        }
        [HttpPatch]
        [Route("EditGender")]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult EditGender(EditGenderModel model)
        {
            try
            {
                var Result = _genderRepository.EditGender(model);
                return Ok(Result);

            }catch(Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
        }
        [HttpGet]
        [Route("ShowGenders")]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult ShowGenders()
        {
            try
            {
                var Result = _genderRepository.ShowGenders();
                return Ok(Result);

            }catch(Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
        }
    }
}
