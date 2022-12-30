using System;
using System.Collections.Generic;

#nullable disable

namespace Ecommerce.Models
{
    public partial class CategoryLevel2
    {
        public CategoryLevel2()
        {
            CategoryLevel1s = new HashSet<CategoryLevel1>();
        }

        public int Id { get; set; }
        public string CategoryL2 { get; set; }
        public int CategoryL3Id { get; set; }

        public virtual CategoryLevel3 CategoryL3 { get; set; }
        public virtual ICollection<CategoryLevel1> CategoryLevel1s { get; set; }
    }
}
