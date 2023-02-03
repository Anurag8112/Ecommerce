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
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<CategoryController> _logger;
        public CategoryController(ICategoryRepository categoryRepository, ILogger<CategoryController> logger)
        {
            _logger = logger;
            _categoryRepository = categoryRepository;
        }

        [HttpPost]
        [Route("AddCategoryL1")]
        [Authorize(Roles = "SuperAdmin,Seller")]
        public IActionResult AddCategoryL1(CategoryModelL1 model)
        {
            try
            {
                var Result = _categoryRepository.AddCategoryL1(model);
                _logger.LogInformation("-------------API Respond Successfully-------------");
                return Ok(Result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
        }


        [HttpPost]
        [Route("AddCategoryL2")]
        [Authorize(Roles = "SuperAdmin,Seller")]
        public IActionResult AddCategoryL2(CategoryModelL2 model)
        {
            try
            {
                var Result = _categoryRepository.AddCategoryL2(model);
                _logger.LogInformation("-------------API Respond Successfully-------------");
                return Ok(Result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
        }


        [HttpPost]
        [Route("AddCategoryL3")]
        [Authorize(Roles = "SuperAdmin,Seller")]
        public IActionResult AddCategoryL3(CategoryModelL3 model)
        {
            try
            {
                var Result = _categoryRepository.AddCategoryL3(model);
                _logger.LogInformation("-------------API Respond Successfully-------------");
                return Ok(Result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
        }


        [HttpDelete]
        [Route("DeleteCategoryL1")]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult DeleteCategoryL1(RemoveCategoryModel model)
        {
            try
            {
                var Result = _categoryRepository.RemoveCategoryL1(model);
                _logger.LogInformation("-------------API Respond Successfully-------------");
                return Ok(Result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
        }


        [HttpDelete]
        [Route("DeleteCategoryL2")]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult DeleteCategoryL2(RemoveCategoryModel model)
        {
            try
            {
                var Result = _categoryRepository.RemoveCategoryL2(model);
                _logger.LogInformation("-------------API Respond Successfully-------------");
                return Ok(Result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
        }


        [HttpDelete]
        [Route("DeleteCategoryL3")]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult DeleteCategoryL3(RemoveCategoryModel model)
        {
            try
            {
                var Result = _categoryRepository.RemoveCategoryL3(model);
                _logger.LogInformation("-------------API Respond Successfully-------------");
                return Ok(Result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
        }


        [HttpGet]
        [Route("ShowCategoryL1")]
        public IActionResult ShowCategoryL1()
        {
            try
            {
                var Result = _categoryRepository.ShowCategoryL1();
                _logger.LogInformation("-----API Respond Successfully-----");
                return Ok(Result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest("Error occurred: " + ex.Message);
            }
        }


        [HttpGet]
        [Route("ShowCategoryL2/{id}")]
        public IActionResult ShowCategoryL2(int id)
        {
            try
            {
                var Result = _categoryRepository.ShowCategoryL2(id);
                _logger.LogInformation("-------------API Respond Successfully-------------");
                return Ok(Result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest("Error occurred: " + ex.Message);
            }
        }


        [HttpGet]
        [Route("ShowCategoryL3/{id}")]
        public IActionResult ShowCategoryL3(int id)
        {
            try
            {
                var Result = _categoryRepository.ShowCategoryL3(id);
                _logger.LogInformation("-------------API Respond Successfully-------------");
                return Ok(Result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest("Error occurred: " + ex.Message);
            }
        }
    }
}
