using System;
using System.Collections.Generic;

#nullable disable

namespace Ecommerce.Models.DbModel
{
    public partial class DpHub
    {
        public int Id { get; set; }
        public string HubName { get; set; }
        public int DpId { get; set; }
        public virtual DeliveryPartner Dp { get; set; }
        public virtual DeliveryBoy DeliveryBoy { get; set; }
        public virtual DpHubAddress DpHubAddress { get; set; }
    }
}
