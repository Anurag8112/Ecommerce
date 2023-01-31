using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Services.Stripe
{
	public record AddStripeCustomer(
		string Email,
		string Name,
		AddStripeCard CreditCard);
}
