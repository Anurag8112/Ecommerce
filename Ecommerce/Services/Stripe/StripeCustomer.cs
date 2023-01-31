using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Services.Stripe
{
	public record StripeCustomer(
		string Name,
		string Email,
		string CustomerId);
}
