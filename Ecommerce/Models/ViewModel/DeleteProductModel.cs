using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.ViewModel
{
    public class DeleteProductModel
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int ProdId { get; set; }
    }
}
