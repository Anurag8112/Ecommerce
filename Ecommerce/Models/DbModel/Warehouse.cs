using System;
using System.Collections.Generic;

#nullable disable

namespace Ecommerce.Models.DbModel
{
    public partial class Warehouse
    {
        public Warehouse()
        {
            InventryItems = new HashSet<InventryItem>();
            UserWarehouseOrderDetailsMappings = new HashSet<UserWarehouseOrderDetailsMapping>();
        }

        public int Id { get; set; }
        public string WarehouseName { get; set; }

        public virtual ICollection<InventryItem> InventryItems { get; set; }
        public virtual ICollection<UserWarehouseOrderDetailsMapping> UserWarehouseOrderDetailsMappings { get; set; }
    }
}
