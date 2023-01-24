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
    public class WishlistController : Controller
    {
        private readonly IWishlistRepository _wishlistRepository;
        private readonly ILogger<WishlistController> _logger;

        public WishlistController(IWishlistRepository wishlistRepository, ILogger<WishlistController> logger)
        {
            _logger = logger;
            _wishlistRepository = wishlistRepository;
        }

        [HttpPost]
        [Route("AddToWishList")]
        [Authorize(Roles = "Buyer")]
        public IActionResult AddToWishList(WishlistModel model)
        {
            try
            {
                var Result = _wishlistRepository.AddToWishlist(model);
                if (Result == false)
                {
                    _logger.LogInformation("-------------Product Already Exist in WishList-------------");
                    return Ok("Product Already Exist in Wishlist");
                }
                _logger.LogInformation("-------------API Respond Successfully-------------");
                return Ok(Result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
        }
        [HttpDelete]
        [Route("DeleteFromWishList")]
        [Authorize(Roles = "Buyer")]
        public IActionResult RemoveFromWishlist(WishlistModel model)
        {
            try
            {
                var Result = _wishlistRepository.RemoveFromWishlist(model);
                _logger.LogInformation("-------------API Respond Successfully-------------");
                return Ok(Result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
        }
        [HttpGet]
        [Route("ShowMyWishlist")]
        [Authorize(Roles = "Buyer")]
        public IActionResult ShowMyWishlist(ShowWishlist model)
        {
            try
            {
                var Result = _wishlistRepository.ShowMyWishlist(model);
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
