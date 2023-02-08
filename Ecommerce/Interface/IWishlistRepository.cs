using Ecommerce.Models.ViewModel;
using System.Collections.Generic;

namespace Ecommerce.Interface
{
    public interface IWishlistRepository
    {
        public bool AddToWishlist(WishlistModel model);
        public bool RemoveFromWishlist(WishlistModel model);
        public List<ShowProduct> ShowMyWishlist(int userId);
    }
}
