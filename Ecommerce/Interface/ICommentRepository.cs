using Ecommerce.Models.ViewModel;

namespace Ecommerce.Interface
{
    public interface ICommentRepository
    {
        public bool AddComment(AddCommentModel model);
        public bool EditComment(EditCommentModel model);
        public bool DeleteComment(DeleteCommentModel model);
    }
}
