using BusinessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Volo.Abp.Users;

namespace RoleBased.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        IFeedbackBL iFeedbackBL;
        public FeedbackController(IFeedbackBL iFeedbackBL)
        {
            this.iFeedbackBL = iFeedbackBL;
        }
        [Authorize]
        [HttpPost("AddFeedback")]
        public IActionResult AddFeedback(FeedbackModel Prod)
        {
            try
            {
                var currentUser = HttpContext.User;
                int Id = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "Id").Value);
                var result = iFeedbackBL.AddFeedback(Prod);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Feedback Added Successfull", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Adding of Feedback was Unsuccessfull" });
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Success = false, message = e.Message });
            }
        }


        [HttpGet("GetProductById")]
        public IActionResult RetriveFeedback(long ProductId)
        {
            try
            {
                var reg = this.iFeedbackBL.RetriveFeedback(ProductId);
                if (reg != null)

                {
                    return this.Ok(new { Success = true, message = "Product Details", Response = reg });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Unable to get Product details" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
    }
}
