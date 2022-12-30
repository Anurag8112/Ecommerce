using System;
using System.Collections.Generic;

#nullable disable

namespace Ecommerce.Models
{
    public partial class ProductDetail
    {
        public ProductDetail()
        {
            WishlistItems = new HashSet<WishlistItem>();
        }

        public int Id { get; set; }
        public int Price { get; set; }
        public int SizeId { get; set; }
        public int ColorId { get; set; }
        public int ProdId { get; set; }

        public virtual Color Color { get; set; }
        public virtual Product Prod { get; set; }
        public virtual Size Size { get; set; }
        public virtual Cart Cart { get; set; }
        public virtual InventryItem InventryItem { get; set; }
        public virtual ICollection<WishlistItem> WishlistItems { get; set; }
    }
}
