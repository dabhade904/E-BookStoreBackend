using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using BusinessLayer.Services;

namespace E_BookStoreBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackBL feedbackBL;

        public FeedbackController(IFeedbackBL feedbackBL)
        {
            this.feedbackBL = feedbackBL;
        }

        [Authorize(Roles = Role.User)]
        [HttpPost("AddFeedback")]
        public IActionResult AddFeedback(FeedbackModel model,int bookId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var feedback = this.feedbackBL.AddFeedback(model, bookId, userId);
                if (feedback != null)
                {
                    return this.Ok(new { Status = true, Message = " Feedback added successfully ", Response = feedback });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "  Enter Correct BookId!!!!" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
        [HttpGet("GetAll")]
        public IActionResult GetAllFeedbacks(int bookId)
        {
            try
            {
                var res = feedbackBL.GetAllFeedbacks(bookId);
                if (res != null)
                {
                    return Ok(new { success = true, message = "Feedback fetched sucessfully", data = res });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Faild to fetched Feedbacks" });
                }
            }
            catch (System.Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }
    }
}
