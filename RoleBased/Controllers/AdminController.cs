using BusinessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace RoleBased.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        IAdminBL iAdminBL;
        public AdminController(IAdminBL iAdminBL)
        {
            this.iAdminBL = iAdminBL;
        }

        [HttpPost("AdminLogin")]
        public IActionResult Login(AdminLoginModel loginModel)
        {
            try
            {
                var result = iAdminBL.Login(loginModel);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Admin Login Successfull", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Login Unsuccessfull" });
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Success = false, message = e.Message });
            }
        }
    }
}
