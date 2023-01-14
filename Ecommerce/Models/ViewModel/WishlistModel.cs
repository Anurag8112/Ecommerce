using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models.ViewModel
{
    public class WishlistModel
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int ProductDetailsId { get; set; }
    }
    public class ShowWishlist
    {
        [Required]
        public int UserId { get; set; }
    }
}
