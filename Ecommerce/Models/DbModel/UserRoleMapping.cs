using System;
using System.Collections.Generic;

#nullable disable

namespace Ecommerce.Models
{
    public partial class UserRoleMapping
    {
        public UserRoleMapping()
        {
            DeliveryBoys = new HashSet<DeliveryBoy>();
            UserWarehouseOrderDetailsMappings = new HashSet<UserWarehouseOrderDetailsMapping>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public virtual UserRole Role { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<DeliveryBoy> DeliveryBoys { get; set; }
        public virtual ICollection<UserWarehouseOrderDetailsMapping> UserWarehouseOrderDetailsMappings { get; set; }
    }
}
