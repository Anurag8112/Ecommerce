using Ecommerce.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Interface
{
    public interface IWishlistRepository
    {
        public bool AddToWishlist(WishlistModel model);
        public bool RemoveFromWishlist(WishlistModel model);
        public List<ShowProduct> ShowMyWishlist(ShowWishlist model);
    }
}
