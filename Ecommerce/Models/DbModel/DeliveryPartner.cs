using System;
using System.Collections.Generic;

#nullable disable

namespace Ecommerce.Models.DbModel
{
    public partial class DeliveryPartner
    {
        public DeliveryPartner()
        {
            DpHubs = new HashSet<DpHub>();
        }

        public int Id { get; set; }
        public string DeliveryPartnerName { get; set; }
        public int HubManagerId { get; set; }

        public virtual UserWarehouseOrderDetailsMapping HubManager { get; set; }
        public virtual ICollection<DpHub> DpHubs { get; set; }
    }
}
