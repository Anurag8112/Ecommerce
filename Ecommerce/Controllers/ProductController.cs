using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class ProductController : Controller
    {
        [HttpPost]
        [Route("Add-Product")]
        public String  Addproducts()
        {
            return "Product Added";
        }
    }
}
