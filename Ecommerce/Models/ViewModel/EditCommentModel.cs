using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.ViewModel
{
    public class EditCommentModel
    {
        [Required]
        public int CommentId { get; set; }
        [Required]
        public string Comment { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
