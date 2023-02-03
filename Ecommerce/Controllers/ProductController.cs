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
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductController> _logger;
        public ProductController(IProductRepository productRepository, ILogger<ProductController> logger)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        [HttpPost]
        [Route("Add-Product")]
        [Authorize(Roles = "Seller")]
        public IActionResult Addproducts(ProductModel product)
        {
            try
            {
                var Result = _productRepository.AddProduct(product);
                _logger.LogInformation("-------------API Respond Successfully-------------");
                return Ok("Product Added");
            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
        }



        [HttpGet]
        [Route("Show-ALL-Products")]
        public IActionResult ShowAllProducts()
        {
            try
            {
                var Result = _productRepository.ShowAllProducts();
                _logger.LogInformation("-------------API Respond Successfully-------------");
                return Ok(Result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
        }
        [HttpDelete]
        [Route("DeleteMyProduct")]
        [Authorize(Roles = "SuperAdmin,Seller")]
        public IActionResult DeleteProduct(DeleteProductModel model)
        {
            try
            {
                var Result = _productRepository.DeleteProduct(model);
                _logger.LogInformation("-------------API Respond Successfully-------------");
                return Ok(Result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error Occured: " + ex.Message);
            }
        }
        [HttpGet]
        [Route("{id}/Show-My-Products")]
        [Authorize]
        public IActionResult ShowMyProduct(int id)
        {
            try
            {
                var result = _productRepository.ShowMyProducts(id);
                _logger.LogInformation("-------------API Respond Successfully-------------");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("GetProductComment")]
        public IActionResult GetProductComment(CommentModel model)
        {
            try
            {
                var Result = _productRepository.ShowComments(model);
                _logger.LogInformation("-------------API Respond Successfully-------------");
                return Ok(Result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
