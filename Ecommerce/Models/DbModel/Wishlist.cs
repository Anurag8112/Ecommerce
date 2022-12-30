using System;
using System.Collections.Generic;

#nullable disable

namespace Ecommerce.Models
{
    public partial class Wishlist
    {
        public Wishlist()
        {
            WishlistItems = new HashSet<WishlistItem>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<WishlistItem> WishlistItems { get; set; }
    }
}
