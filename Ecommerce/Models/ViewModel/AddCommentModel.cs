using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.ViewModel
{
    public class AddCommentModel
    {
        [Required]
        public int ProdId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Comment { get; set; }
    }
}
