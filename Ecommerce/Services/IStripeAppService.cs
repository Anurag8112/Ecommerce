using Ecommerce.Services.Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Services
{
	public interface IStripeAppService
	{
		Task<StripeCustomer> AddStripeCustomerAsync(AddStripeCustomer customer, CancellationToken ct);
		Task<StripePayment> AddStripePaymentAsync(AddStripePayment payment, CancellationToken ct);
	}
}
