using Ecommerce.Models.DbModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models.ViewModel
{
    public class EditDpHub
    {
        [Required]
        public int DeliveryPartnerId { get; set; }
        [Required]
        public int DpHubId { get; set; }
        [Required]
        public string DpHubName { get; set; }
        [Required]
        public DpHubAddressModel HubAddress {get;set;}
    }
}
