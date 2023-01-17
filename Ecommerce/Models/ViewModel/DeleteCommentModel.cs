using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.ViewModel
{
    public class DeleteCommentModel
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int CommentId { get; set; }
    }
}
