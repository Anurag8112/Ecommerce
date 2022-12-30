using System;
using System.Collections.Generic;

#nullable disable

namespace Ecommerce.Models
{
    public partial class WishlistItem
    {
        public int Id { get; set; }
        public int WishlistId { get; set; }
        public int ProdDetailId { get; set; }

        public virtual ProductDetail ProdDetail { get; set; }
        public virtual Wishlist Wishlist { get; set; }
    }
}
