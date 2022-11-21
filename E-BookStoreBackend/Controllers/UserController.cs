using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Drawing;
using System.Security.Claims;

namespace E_BookStoreBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL;

        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }

        [HttpPost("Register")]
        public IActionResult Registration(RegistrationModel registrationModel)
        {
            try
            {
                var result = userBL.UserRegistration(registrationModel);
                if (!result.Equals(null))
                {
                    return this.Ok(new
                    {
                        success = true,
                        message = "User Registration Successfull",
                        data = result
                    });
                }
                else
                {
                    return this.BadRequest(new
                    {
                        success = false,
                        message = "User Registration UnSuccessfull"
                    });
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [HttpPost("Login")]
        public IActionResult UserLogin(LoginModel loginModel)
        {
            try
            {
                var result = userBL.UserLogin(loginModel);
                if (result == null)
                {
                    return this.Unauthorized(new
                    {
                        Success = false,
                        message = "Invalid Email or Password",
                    });
                }
                else
                {
                    return this.Ok(new
                    {
                        success = true,
                        message = "Login Successfull",
                        tokan = result
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost("ForgetPassword")]
        public IActionResult ForgetPassword(string emailId)
        {
            try
            {
                var result = userBL.ForgetPassword(emailId);
                if (!result.Equals(null))
                {
                    return this.Ok(new
                    {
                        success = true,
                        message = "Email Send Successfully",
                    });
                }
                else
                {
                    return this.BadRequest(new
                    {
                        Success = false,
                        message = "EMail has not send",
                    });
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [Authorize]
        [HttpPost("ResetPassword")]
        public IActionResult ResetPassword(string resetPassword, string confirmPassword)
        {
            try
            {
                var EmailId = User.FindFirst(ClaimTypes.Email).Value.ToString();
                var result = userBL.ResetPassword(resetPassword,confirmPassword, EmailId);

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

