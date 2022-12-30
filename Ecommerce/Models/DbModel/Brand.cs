﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Ecommerce.Models.DbModel
{
    public partial class Brand
    {
        public Brand()
        {
            CategoryLevel1s = new HashSet<CategoryLevel1>();
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string BrandName { get; set; }
        public int CategoryId { get; set; }

        public virtual CategoryLevel1 Category { get; set; }
        public virtual ICollection<CategoryLevel1> CategoryLevel1s { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
