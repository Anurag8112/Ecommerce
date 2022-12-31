using System;
using System.Collections.Generic;

#nullable disable

namespace Ecommerce.Models.DbModel
{
    public partial class BrandCategoryMapping
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public int CategoryL1Id { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual CategoryLevel1 CategoryL1 { get; set; }
    }
}
