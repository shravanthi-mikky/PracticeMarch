using BusinessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace RoleBased.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        IPaymentBL iPaymentBL;

        public PaymentController(IPaymentBL iPaymentBL)
        {
            this.iPaymentBL = iPaymentBL;
        }

        [HttpPost("Payment")]
        public IActionResult Payment(PaymentModel payModel)
        {
            try
            {
                bool Value = iPaymentBL.Payment(payModel);
                if (Value != false)
                {
                    return Ok(new { Success = true, message = "Payment Sucessfull", Data = Value });
                }
                else
                {
                    return BadRequest(new { Success = false, message = "Payment Unsucessfull" });
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
