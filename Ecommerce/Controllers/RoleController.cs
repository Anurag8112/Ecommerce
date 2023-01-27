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
    public class RoleController : Controller
    {
        private readonly IRoleRepository _roleRepository;
        private readonly ILogger<RoleController> _logger;
        public RoleController(IRoleRepository roleRepository,ILogger<RoleController> logger)
        {
            _logger = logger;
            _roleRepository = roleRepository;
        }

        [HttpPost]
        [Route("AddRole")]
        //[Authorize(Roles = "SuperAdmin")]
        public IActionResult AddRole(RoleModel model)
        {
            try
            {
                var Result = _roleRepository.AddRole(model);
                _logger.LogInformation("-------------API Respond Successfully-------------");
                return Ok(Result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
        }


        [HttpDelete]
        [Route("DeleteRole")]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult DeleteRole(DeleteRoleModel model)
        {
            try
            {
                var Result = _roleRepository.DeleteRole(model);
                _logger.LogInformation("-------------API Respond Successfully-------------");
                return Ok(Result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
        }


        [HttpGet]
        [Route("ShowAllRoles")]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult ShowAllRoles()
        {
            try
            {
                var Result = _roleRepository.GetAllRoles();
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
