using System;
using System.Collections.Generic;

#nullable disable

namespace Ecommerce.Models.DbModel
{
    public partial class ProductImage
    {
        public int ImgId { get; set; }
        public int ProdId { get; set; }
        public string Image { get; set; }

        public virtual Product Prod { get; set; }
    }
}
