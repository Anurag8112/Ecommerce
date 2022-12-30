using System;
using System.Collections.Generic;

#nullable disable

namespace Ecommerce.Models
{
    public partial class DeliveryBoy
    {
        public int Id { get; set; }
        public int UserRoleMappingId { get; set; }
        public int AssignedHubId { get; set; }

        public virtual DpHub AssignedHub { get; set; }
        public virtual UserRoleMapping UserRoleMapping { get; set; }
    }
}
