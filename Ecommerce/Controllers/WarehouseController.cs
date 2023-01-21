using Ecommerce.Interface;
using Ecommerce.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class WarehouseController : Controller
    {
        private readonly IWarehouseRepository _warehouseRepository;
        public WarehouseController(IWarehouseRepository warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }
        [HttpPost]
        [Route("AddWarehouse")]
        // [Authorize(Roles = "WarehouseManager,SuperAdmin")]
        public IActionResult AddWarehouse(WarehouseModel model)
        {
            try
            {
                var Result = _warehouseRepository.AddWarehouse(model);
                return Ok(Result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error Occured : " + ex.Message);
            }
        }
        [HttpPatch]
        [Route("EditWareHouseName")]
        // [Authorize(Roles = "WarehouseManager,SuperAdmin")]
        public IActionResult EditWarehouseName(EditWarehouseModel model)
        {
            try
            {
                var Result = _warehouseRepository.EditWarehouseName(model);
                return Ok(Result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error Occured : " + ex.Message);
            }
        }
        [HttpDelete]
        [Route("DeleteWarehouse")]
        //[Authorize(Roles = "WarehouseManager,SuperAdmin")]
        public IActionResult DeleteWarehouse(DeleteWarehouseModel model)
        {
            try
            {
                var Result = _warehouseRepository.DeleteWarehouse(model);
                return Ok(Result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error Occured : " + ex.Message);
            }
        }
        [HttpGet]
        [Route("GetAllWarehouse")]
        //[Authorize(Roles = "WarehouseManager,SuperAdmin")]
        public IActionResult GetAllWarehouse()
        {
            try
            {
                var Result = _warehouseRepository.GetAllWarehouse();
                return Ok(Result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error Occured : " + ex.Message);
            }
        }
    }
}
