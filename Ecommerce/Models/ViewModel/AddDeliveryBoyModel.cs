using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models.ViewModel
{
    public class AddDeliveryBoyModel
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int AssignedHubId { get; set; }
    }
}
