using System;
using System.Collections.Generic;

#nullable disable

namespace Ecommerce.Models.DbModel
{
    public partial class ProductDetail
    {
        public ProductDetail()
        {
            Carts = new HashSet<Cart>();
            InventryItems = new HashSet<InventryItem>();
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
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<InventryItem> InventryItems { get; set; }
        public virtual ICollection<WishlistItem> WishlistItems { get; set; }
    }
}
