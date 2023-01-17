using Ecommerce.Interface;
using Ecommerce.Models.DbModel;
using Ecommerce.Models.ViewModel;
using System;
using System.Linq;

namespace Ecommerce.Repository
{
    public class CommentRepository : ICommentRepository
    {
        public bool AddComment(AddCommentModel model)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();

                var comment = new Comment()
                {
                    UserId = model.UserId,
                    ProdId = model.ProdId,
                    Comment1 = model.Comment
                };

                db.Comments.Add(comment);
                db.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteComment(DeleteCommentModel model)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();

                var comment = db.Comments.FirstOrDefault(x => x.Id == model.CommentId);
                var IsValidUser = db.Users.FirstOrDefault(x => x.Id == model.UserId && x.IsActive == true && x.IsVerified == true);
                if (IsValidUser == null)
                {
                    throw new Exception("Invalid User Id");
                }

                if (comment == null)
                {
                    throw new Exception("Invalid Comment Id");
                }

                else
                {
                    if (comment.UserId == model.UserId)
                    {
                        db.Comments.Remove(comment);
                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        throw new Exception("You Are Not allowed To Delete This Comment");
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool EditComment(EditCommentModel model)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                var comment = db.Comments.FirstOrDefault(x => x.Id == model.CommentId);
                var IsValidUser = db.Users.FirstOrDefault(x => x.Id == model.UserId);

                if (IsValidUser == null)
                {
                    throw new Exception("Invalid User Id");
                }

                if (comment == null)
                {
                    throw new Exception("Invalid Comment Id");
                }

                if (comment.UserId == model.UserId)
                {
                    comment.Comment1 = model.Comment;

                    db.Comments.Update(comment);
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    throw new Exception("You are not allowed to edit this comment");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
