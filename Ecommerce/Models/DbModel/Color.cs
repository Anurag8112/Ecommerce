using System;
using System.Collections.Generic;

#nullable disable

namespace Ecommerce.Models.DbModel
{
    public partial class Color
    {
        public Color()
        {
            ProductDetails = new HashSet<ProductDetail>();
        }

        public int Id { get; set; }
        public string Color1 { get; set; }

        public virtual ICollection<ProductDetail> ProductDetails { get; set; }
    }
}
