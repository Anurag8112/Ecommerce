using System;
using System.Collections.Generic;

#nullable disable

namespace Ecommerce.Models
{
    public partial class Comment
    {
        public int Id { get; set; }
        public int ProdId { get; set; }
        public int UserId { get; set; }
        public string Comment1 { get; set; }

        public virtual Product Prod { get; set; }
        public virtual User User { get; set; }
    }
}
