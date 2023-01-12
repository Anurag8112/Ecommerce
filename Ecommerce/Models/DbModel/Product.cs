using System;
using System.Collections.Generic;

#nullable disable

namespace Ecommerce.Models.DbModel
{
    public partial class Product
    {
        public Product()
        {
            Comments = new HashSet<Comment>();
            OrderItems = new HashSet<OrderItem>();
            ProductDetails = new HashSet<ProductDetail>();
            ProductImages = new HashSet<ProductImage>();
            UserProductMappings = new HashSet<UserProductMapping>();
        }

        public int ProdId { get; set; }
        public string ProdName { get; set; }
        public string ProdDescription { get; set; }
        public int CategoryL1Id { get; set; }
        public int CategoryL2Id { get; set; }
        public int CategoryL3Id { get; set; }
        public int BrandId { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual CategoryLevel1 Category1 { get; set; }
        public virtual CategoryLevel2 Category2 { get; set; }
        public virtual CategoryLevel3 Category3 { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<ProductDetail> ProductDetails { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<UserProductMapping> UserProductMappings { get; set; }
    }
}
