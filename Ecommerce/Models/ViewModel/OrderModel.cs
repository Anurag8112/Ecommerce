using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models.ViewModel
{
    public class OrderModel
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        [MinLength(16)]
        public string TransectionId { get; set; }
        [Required]
        public int AddressId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public int ProdId { get; set; }
        [Required]
        public int SizeId { get; set; }
        [Required]
        public int ColorId { get; set; }
        [Required]
        public string PaymentStatus { get; set; }

    }
}
