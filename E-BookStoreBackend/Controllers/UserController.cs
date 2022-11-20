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
    }
}
