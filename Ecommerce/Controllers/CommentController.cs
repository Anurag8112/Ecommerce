using Ecommerce.Interface;
using Ecommerce.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentRepository;
        private readonly ILogger<CommentController> _logger;
        public CommentController(ICommentRepository commentRepository,ILogger<CommentController> logger)
        {
            _logger = logger;
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
                _logger.LogInformation("-------------API Respond Successfully-------------");
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
                _logger.LogInformation("-------------API Respond Successfully-------------");
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
                _logger.LogInformation("-------------API Respond Successfully-------------");
                return Ok(Result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
        }
    }
}
