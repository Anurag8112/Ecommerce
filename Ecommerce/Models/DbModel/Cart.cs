using System;
using System.Collections.Generic;

#nullable disable

namespace Ecommerce.Models.DbModel
{
    public partial class Cart
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int ProdId { get; set; }

        public virtual CartTable CartNavigation { get; set; }
        public virtual ProductDetail IdNavigation { get; set; }
    }
}
