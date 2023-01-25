using Ecommerce.Interface;
using Ecommerce.Models.DbModel;
using Ecommerce.Models.ViewModel;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Ecommerce.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ILogger<CommentRepository> _logger;

        public CommentRepository (ILogger<CommentRepository> logger)
        {
            _logger = logger;
        }

        public bool AddComment(AddCommentModel model)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                _logger.LogInformation("-----------DB Connection Established------------");

                var comment = new Comment()
                {
                    UserId = model.UserId,
                    ProdId = model.ProdId,
                    Comment1 = model.Comment
                };
                db.Comments.Add(comment);
                db.SaveChanges();
                _logger.LogInformation("-------------Comment Added Successfully-------------");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException.ToString());
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteComment(DeleteCommentModel model)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();

                var comment = db.Comments.FirstOrDefault(x => x.Id == model.CommentId);
                var IsValidUser = db.Users.FirstOrDefault(x => x.Id == model.UserId && x.Isactive == true && x.IsVerified == true);
                if (IsValidUser == null)
                {
                    _logger.LogError("-------------Invalid User Id-------------");
                    throw new Exception("Invalid User Id");
                }

                if (comment == null)
                {
                    _logger.LogError("-------------Invalid Comment Id-------------");
                    throw new Exception("Invalid Comment Id");
                }
                else
                {
                    if (comment.UserId == model.UserId)
                    {
                        db.Comments.Remove(comment);
                        db.SaveChanges();
                        _logger.LogInformation("-------------Comment Removed Succesfully-------------");
                        return true;
                    }
                    else
                    {
                        _logger.LogError("------------Unauthorized To Remove Comment-------------");
                        throw new Exception("You Are Not allowed To Delete This Comment");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException.ToString());
                throw new Exception(ex.Message);
            }
        }
        public bool EditComment(EditCommentModel model)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                _logger.LogTrace("-------------DB Connection Established-------------");
                var comment = db.Comments.FirstOrDefault(x => x.Id == model.CommentId);
                var IsValidUser = db.Users.FirstOrDefault(x => x.Id == model.UserId);

                if (IsValidUser == null)
                {
                    _logger.LogError("---------------Invalid UserId-----------");
                    throw new Exception("Invalid User Id");
                }
                if (comment == null)
                {
                    _logger.LogError("---------------Invalid Comment Id-----------");
                    throw new Exception("Invalid Comment Id");
                }
                if (comment.UserId == model.UserId)
                {
                    comment.Comment1 = model.Comment;

                    db.Comments.Update(comment);
                    db.SaveChanges();
                    _logger.LogInformation("-------------Comment Edited Succesfully-------------");
                    return true;
                }
                else
                {
                    _logger.LogError("--------------Unauthorized to edit comment---------------");
                    throw new Exception("You are not allowed to edit this comment");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException.ToString());
                throw new Exception(ex.Message);
            }
        }
    }
}
