using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.ViewModel
{
    public class CartModel
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int ProductDetailId { get; set; }
    }
    public class ShowCart
    {
        [Required]
        public int UserId { get; set; }
    }
}
