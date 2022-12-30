using System;
using System.Collections.Generic;

#nullable disable

namespace Ecommerce.Models
{
    public partial class CategoryLevel1
    {
        public CategoryLevel1()
        {
            Brands = new HashSet<Brand>();
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string CategoryL1 { get; set; }
        public int CategoryL2Id { get; set; }

        public virtual CategoryLevel2 CategoryL2 { get; set; }
        public virtual ICollection<Brand> Brands { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
