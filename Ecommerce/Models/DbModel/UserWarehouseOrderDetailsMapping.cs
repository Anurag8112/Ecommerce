using System;
using System.Collections.Generic;

#nullable disable

namespace Ecommerce.Models.DbModel
{
    public partial class UserWarehouseOrderDetailsMapping
    {
        public UserWarehouseOrderDetailsMapping()
        {
            DeliveryPartners = new HashSet<DeliveryPartner>();
        }

        public int Id { get; set; }
        public int UserRoleMappingId { get; set; }
        public int WarehouseId { get; set; }
        public int OrderDetailId { get; set; }

        public virtual OrderDetail OrderDetail { get; set; }
        public virtual UserRoleMapping UserRoleMapping { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        public virtual ICollection<DeliveryPartner> DeliveryPartners { get; set; }
    }
}
