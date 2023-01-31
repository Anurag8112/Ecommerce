using Ecommerce.Services;
using Ecommerce.Services.Stripe;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class StripeController : Controller
    {
            private readonly IStripeAppService _stripeService;

            public StripeController(IStripeAppService stripeService)
            {
                _stripeService = stripeService;
            }

            [HttpPost("customer/add")]
            public async Task<ActionResult<StripeCustomer>> AddStripeCustomer([FromBody] AddStripeCustomer customer,CancellationToken ct)
            {
                StripeCustomer createdCustomer = await _stripeService.AddStripeCustomerAsync(customer,ct);
                return StatusCode(StatusCodes.Status200OK, createdCustomer);
            }

            [HttpPost("payment/add")]
            public async Task<ActionResult<StripePayment>> AddStripePayment([FromBody] AddStripePayment payment,CancellationToken ct)
            {
                StripePayment createdPayment = await _stripeService.AddStripePaymentAsync(payment,ct);
                return StatusCode(StatusCodes.Status200OK, createdPayment);
            }
    }
}
