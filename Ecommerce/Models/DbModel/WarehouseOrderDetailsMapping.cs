using System;
using System.Collections.Generic;

#nullable disable

namespace Ecommerce.Models.DbModel
{
    public partial class WarehouseOrderDetailsMapping
    {
        public int Id { get; set; }
        public int WarehouseId { get; set; }
        public int OrderDetailId { get; set; }

        public virtual OrderDetail OrderDetail { get; set; }
        public virtual Warehouse Warehouse { get; set; }
    }
}
