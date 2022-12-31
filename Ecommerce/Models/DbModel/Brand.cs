using System;
using System.Collections.Generic;

#nullable disable

namespace Ecommerce.Models.DbModel
{
    public partial class Brand
    {
        public Brand()
        {
            BrandCategoryMappings = new HashSet<BrandCategoryMapping>();
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string BrandName { get; set; }

        public virtual ICollection<BrandCategoryMapping> BrandCategoryMappings { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
