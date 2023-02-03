using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models.ViewModel
{
    public class CartOrderModel
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        [MinLength(16)]
        public string TransectionId { get; set; }
        [Required]
        public int AddressId { get; set; }
        [Required]
        public string Currency { get; set; }
    }
}
