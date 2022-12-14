using BusinessLayer.Interface;
using BusinessLayer.Services;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MSMQ.Messaging;
using System;
using System.Security.Claims;

namespace E_BookStoreBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminBL adminBL;

        public AdminController(IAdminBL adminBL)
        {
            this.adminBL = adminBL;
        }

        [HttpPost("Register")]
        public IActionResult Registration(AdminRegistrationModel registrationModel)
        {
            try
            {
                var result = adminBL.AdminRegistration(registrationModel);
                if (!result.Equals(null))
                {
                    return this.Ok(new
                    {
                        success = true,
                        message = "Admin  Registration Successfull",
                        data = result
                    });
                }
                else
                {
                    return this.BadRequest(new
                    {
                        success = false,
                        message = "Admin Registration UnSuccessfull"
                    });
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [HttpPost("Login")]
        public IActionResult AdminLogin(AdminLoginModel adminLoginModel)
        {
            try
            {
                var result = adminBL.AdminLogin(adminLoginModel);
                if (!result.Equals(null))
                {
                    return this.Ok(new
                    {
                        success = true,
                        message = "Admin Login Successfull",
                        data = result
                    });
                }
                else
                {
                    return this.BadRequest(new
                    {
                        success = false,
                        message = "Admin Login UnSuccessfull"
                    });
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [HttpPost("ForgetPassword")]
        public IActionResult ForgetPassword(string email)
        {
            try
            {
                var result = adminBL.ForgetPassword(email);
                if (!result.Equals(null))
                {
                    return this.Ok(new
                    {
                        success = true,
                        message = "Eamil sent Successfully",
                    });
                }
                else
                {
                    return this.BadRequest(new
                    {
                        success = false,
                        message = "EMail has not send"
                    });
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [Authorize]
        [HttpPost("ResetPassword")]
        public IActionResult ResetPassword(string resetPassword, string confirmPassword)
        {
            try
            {
                var EmailId = User.FindFirst(ClaimTypes.Email).Value.ToString();
                var result = adminBL.AdminResetPassword(resetPassword, confirmPassword, EmailId);

                if (result != null)
                {
                    return Ok(new { Success = true, Message = " Password reset succcessful" });
                }
                else
                {
                    return BadRequest(new { Success = false, Message = "Password reset unsuccessful" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
