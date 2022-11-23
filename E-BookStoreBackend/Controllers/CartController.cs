using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using BusinessLayer.Interface;

namespace E_BookStoreBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartBL cartBL;
        public CartController(ICartBL cartBL)
        {
            this.cartBL = cartBL;
        }

        [Authorize(Roles = Role.User)]
        [HttpPost("AddToCart")]
        //  [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult AddToCart(CartModel cartModel)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var userData = this.cartBL.AddBookToCart(cartModel, userId);
                if (userData != null)
                {
                    return this.Ok(new
                    {
                        Success = true,
                        message = "Book Added to cart Sucessfully",
                        Response = userData
                    });
                }
                return this.Ok(new
                {
                    Success = true,
                    message = "Book Already Exists"
                });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
        [Authorize(Roles = Role.User)]
        [HttpDelete("DeleteCart")]
        public IActionResult DeletCart(int cartId)
        {
            try
            {
                var data = this.cartBL.DeleteCart(cartId);
                if (data != null)
                {
                    return this.Ok(new
                    {
                        Success = true,
                        message = "Book in Cart Deleted Sucessfully",
                    });
                }
                else
                {
                    return this.BadRequest(new
                    {
                        Success = false,
                        message = "Enter Valid CartId"
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
