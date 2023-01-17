using Ecommerce.Interface;
using Ecommerce.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentRepository;

        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        [HttpPost]
        [Route("AddComment")]
        [Authorize(Roles = "Buyer")]
        public IActionResult AddComment(AddCommentModel model)
        {
            try
            {
                var Result = _commentRepository.AddComment(model);

                return Ok(Result);

            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
        }


        [HttpPost]
        [Route("DeleteComment")]
        [Authorize(Roles = "Buyer")]
        public IActionResult DeleteComment(DeleteCommentModel model)
        {
            try
            {
                var Result = _commentRepository.DeleteComment(model);

                return Ok(Result);

            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
        }

        [HttpPut]
        [Route("EditComment")]
        [Authorize(Roles = "Buyer")]
        public IActionResult EditComment(EditCommentModel model)
        {
            try
            {
                var Result = _commentRepository.EditComment(model);

                return Ok(Result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }

        }

    }
}
