using System;
using System.Collections.Generic;

#nullable disable

namespace Ecommerce.Models
{
    public partial class ProductImage
    {
        public int ImgId { get; set; }
        public int ProdId { get; set; }
        public byte[] Image { get; set; }

        public virtual Product Prod { get; set; }
    }
}
