using Ecommerce.Interface;
using Ecommerce.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<DpHubController> _logger;
        public DpHubController(IHubRepository hubRepository, ILogger<DpHubController> logger)
        {
            _logger = logger;
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
                _logger.LogInformation("-------------API Respond Successfully-------------");
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
                _logger.LogInformation("-------------API Respond Successfully-------------");
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
                _logger.LogInformation("-------------API Respond Successfully-------------");
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
