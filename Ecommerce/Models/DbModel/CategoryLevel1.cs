using System;
using System.Collections.Generic;

#nullable disable

namespace Ecommerce.Models.DbModel
{
    public partial class CategoryLevel1
    {
        public CategoryLevel1()
        {
            BrandCategoryMappings = new HashSet<BrandCategoryMapping>();
            CategoryLevel2s = new HashSet<CategoryLevel2>();
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string CategoryL1 { get; set; }

        public virtual ICollection<BrandCategoryMapping> BrandCategoryMappings { get; set; }
        public virtual ICollection<CategoryLevel2> CategoryLevel2s { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
