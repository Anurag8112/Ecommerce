using Ecommerce.Models.ViewModel;
using System.Collections.Generic;

namespace Ecommerce.Interface
{
    public interface ICartRepository
    {
        public bool AddToCart(CartModel model);
        public bool RemoveFromCart(DeleteCartItem model);
        public List<ShowProduct> ShowCartItems(int userId);
    }
}
