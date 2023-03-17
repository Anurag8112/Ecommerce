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
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepository;
        private readonly ILogger<CartController> _logger;

        public CartController(ICartRepository cartRepository, ILogger<CartController> logger)
        {
            _logger = logger;
            _cartRepository = cartRepository;
        }

        [HttpPost]
        [Route("AddToCart")]
        [Authorize(Roles = "Buyer")]
        public IActionResult AddToCart(CartModel model)
        {
            try
            {
                var Result = _cartRepository.AddToCart(model);
                if (Result == false)
                {
                    _logger.LogInformation("------Product Already Exist in Cart------");
                    return Ok("Product Already Exist in Cart");
                }
                _logger.LogInformation("------------------API Respond Successfully-----------------");
                return Ok(Result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
        }

        [HttpDelete]
        [Route("RemoveFromCart")]
        [Authorize(Roles = "Buyer")]
        public IActionResult RemoveFromCart(DeleteCartItem model)
        {
            try
            {
                var Result = _cartRepository.RemoveFromCart(model);
                _logger.LogInformation("------------------API Respond Successfully-----------------");
                return Ok(Result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("ShowCartItems/{id}")]
        [Authorize(Roles = "Buyer")]
        public IActionResult ShowCaetItem(int userId)
        {
            try
            {
                var Result = _cartRepository.ShowCartItems(userId);
                _logger.LogInformation("-----API Responded Successfully------");
                return Ok(Result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
        }
    }
}
