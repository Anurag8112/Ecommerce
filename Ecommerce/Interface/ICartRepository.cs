using Ecommerce.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Interface
{
    public interface ICartRepository
    {
        public bool AddToCart(CartModel model);
        public bool RemoveFromCart(CartModel model);
        public List<ShowProduct> ShowCartItems();
    }
}
