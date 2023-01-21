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
    public class DpHubController : Controller
    {
        private readonly IHubRepository _hubRepository;
        public DpHubController(IHubRepository hubRepository)
        {
            _hubRepository = hubRepository;
        }
        [HttpPost]
        [Route("AddDpHub")]
        [Authorize(Roles = "SuperAdmin,HubManager")]
        public IActionResult AddDpHub(AddDpHubModel model)
        {
            try
            {
                var Result = _hubRepository.AddDPHub(model);
                return Ok(Result);
            } 
            catch(Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
        }
        [HttpDelete]
        [Route("DeleteDpHub")]
        [Authorize(Roles = "SuperAdmin,HubManager")]
        public IActionResult DeleteDpHub(DeleteDpHub model)
        {
            try
            {
                var Result = _hubRepository.RemoveDpHub(model);
                return Ok(Result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
        }
        [HttpPatch]
        [Route("EditDpHub")]
        [Authorize(Roles = "SuperAdmin,HubManager")]
        public IActionResult EditDpHub(EditDpHub model)
        {
            try
            {
                var Result = _hubRepository.EditDpHub(model);
                return Ok(Result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
        }
        [HttpGet]
        [Route("ShowDpHub")]
        [Authorize(Roles = "SuperAdmin,HubManager")]
        public IActionResult ShowDpHub()
        {
            try
            {
                var Result = _hubRepository.ShowDpHub();
                return Ok(Result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
        }
    }
}
