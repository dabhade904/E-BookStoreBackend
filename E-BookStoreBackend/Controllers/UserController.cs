using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

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
    }
}

