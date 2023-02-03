using Ecommerce.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Interface
{
    public interface IOrderRepository
    {
        public bool OrderNow(OrderModel model);
        public bool OrderNowByCart(CartOrderModel models);
    }
}
