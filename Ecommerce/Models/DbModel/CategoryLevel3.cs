using System;
using System.Collections.Generic;

#nullable disable

namespace Ecommerce.Models.DbModel
{
    public partial class CategoryLevel3
    {
        public int Id { get; set; }
        public int CategoryL2Id { get; set; }
        public string CategoryL3 { get; set; }

        public virtual CategoryLevel2 CategoryL2 { get; set; }
    }
}
