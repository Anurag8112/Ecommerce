using Ecommerce.Models.DbModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models.ViewModel
{
    public class AddDpHubModel
    {
        [Required]
        public int DPId { get; set; }
        [Required]
        public string HubName { get; set; }
        [Required]
        public DpHubAddressModel HubAddress { get; set; }
    }
}
