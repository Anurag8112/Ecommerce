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
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepository;

        public CartController(ICartRepository cartRepository)
        {
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
                    return Ok("Product Already Exist in Cart");
                }

                return Ok(Result);
            }
            catch(Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
        }


    }
}
