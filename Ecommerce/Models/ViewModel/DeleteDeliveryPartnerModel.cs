using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models.ViewModel
{
    public class DeleteDeliveryPartnerModel
    {
        [Required]
        public int DeliveryPartnerId { get; set; }
    }
}
