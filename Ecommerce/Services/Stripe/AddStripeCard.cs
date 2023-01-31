using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Services.Stripe
{
		public record AddStripeCard(
		string Name,
		string CardNumber,
		string ExpirationYear,
		string ExpirationMonth,
		string Cvc);
}
