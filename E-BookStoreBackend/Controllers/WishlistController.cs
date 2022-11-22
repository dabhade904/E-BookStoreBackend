using BusinessLayer.Interface;
using BusinessLayer.Services;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace E_BookStoreBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistBL wishlistBL;
        public WishlistController(IWishlistBL wishlistBL)
        {
            this.wishlistBL = wishlistBL;
        }
        [HttpPost("CreateWishList")]
        public IActionResult CreateWishList(WishlistModel wishlistModel,int userId)
        {
            try
            {
                var result = wishlistBL.AddBookinWishList(wishlistModel,userId);
                if (!result.Equals(null))
                {
                    return this.Ok(new
                    {
                        success = true,
                        message = "Wishlist is created",
                        data = result
                    });
                }
                else
                {
                    return this.BadRequest(new
                    {
                        success = false,
                        message = "Something went wrong"
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
