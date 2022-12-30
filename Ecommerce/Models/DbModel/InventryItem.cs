using System;
using System.Collections.Generic;

#nullable disable

namespace Ecommerce.Models.DbModel
{
    public partial class InventryItem
    {
        public int Id { get; set; }
        public int ProductDetailId { get; set; }
        public int WarehouseId { get; set; }
        public int ProductCount { get; set; }

        public virtual ProductDetail IdNavigation { get; set; }
        public virtual Warehouse Warehouse { get; set; }
    }
}
