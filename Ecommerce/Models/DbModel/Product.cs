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
        public byte[] ProdDescription { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual CategoryLevel1 Category { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<ProductDetail> ProductDetails { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<UserProductMapping> UserProductMappings { get; set; }
    }
}
