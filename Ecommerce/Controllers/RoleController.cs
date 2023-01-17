using Ecommerce.Interface;
using Ecommerce.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class RoleController : Controller
    {
        private readonly IRoleRepository _roleRepository;

        public RoleController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        [HttpPost]
        [Route("AddRole")]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult AddRole(RoleModel model)
        {
            try
            {
                var Result = _roleRepository.AddRole(model);

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

                return Ok(Result);

            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
        }
    }
}
