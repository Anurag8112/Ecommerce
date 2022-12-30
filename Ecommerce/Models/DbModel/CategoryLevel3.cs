using System;
using System.Collections.Generic;

#nullable disable

namespace Ecommerce.Models
{
    public partial class CategoryLevel3
    {
        public CategoryLevel3()
        {
            CategoryLevel2s = new HashSet<CategoryLevel2>();
        }

        public int Id { get; set; }
        public string CategoryL3 { get; set; }

        public virtual ICollection<CategoryLevel2> CategoryLevel2s { get; set; }
    }
}
