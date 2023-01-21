using System;
using System.Collections.Generic;

#nullable disable

namespace Ecommerce.Models.DbModel
{
    public partial class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual OrderDetail Order { get; set; }
    }
}
