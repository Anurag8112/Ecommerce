using System;
using System.Collections.Generic;

#nullable disable

namespace Ecommerce.Models
{
    public partial class Size
    {
        public Size()
        {
            ProductDetails = new HashSet<ProductDetail>();
        }

        public int Id { get; set; }
        public string Size1 { get; set; }

        public virtual ICollection<ProductDetail> ProductDetails { get; set; }
    }
}
