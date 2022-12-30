using System;
using System.Collections.Generic;

#nullable disable

namespace Ecommerce.Models.DbModel
{
    public partial class CategoryLevel2
    {
        public CategoryLevel2()
        {
            CategoryLevel3s = new HashSet<CategoryLevel3>();
        }

        public int Id { get; set; }
        public string CategoryL2 { get; set; }
        public int CategoryL1Id { get; set; }

        public virtual CategoryLevel1 CategoryL1 { get; set; }
        public virtual ICollection<CategoryLevel3> CategoryLevel3s { get; set; }
    }
}
