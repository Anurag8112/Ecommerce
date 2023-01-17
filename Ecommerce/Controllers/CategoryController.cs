using Ecommerce.Interface;
using Ecommerce.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
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

                return Ok(Result);

            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("ShowCategoryL1")]
        [Authorize]
        public IActionResult ShowCategoryL1()
        {
            try
            {
                var Result = _categoryRepository.ShowCategoryL1();

                return Ok(Result);

            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("ShowCategoryL2")]
        [Authorize]
        public IActionResult ShowCategoryL2()
        {
            try
            {
                var Result = _categoryRepository.ShowCategoryL2();

                return Ok(Result);

            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("ShowCategoryL3")]
        [Authorize]
        public IActionResult ShowCategoryL3()
        {
            try
            {
                var Result = _categoryRepository.ShowCategoryL3();

                return Ok(Result);

            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
        }


    }
}
