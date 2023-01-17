using Ecommerce.Models.ViewModel;
using System.Collections.Generic;

namespace Ecommerce.Interface
{
    public interface ICartRepository
    {
        public bool AddToCart(CartModel model);
        public bool RemoveFromCart(CartModel model);
        public List<ShowProduct> ShowCartItems();
    }
}
