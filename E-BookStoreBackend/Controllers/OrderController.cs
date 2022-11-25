using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;

namespace E_BookStoreBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderBL orderBL;

        public OrderController(IOrderBL orderBL)
        {
            this.orderBL = orderBL;
        }
        [Authorize(Roles = Role.User)]
        [HttpPost("AddOrders")]
        // [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
       
        public IActionResult AddOrder(OrderModel order)
        {
            try
            {
                string result = this.orderBL.AddOrder(order);
                if (result.Equals("Ordered successfully"))
                {

                    return this.Ok(new { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = result });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }

        [HttpGet("GetOrdersDetails")]
        public IActionResult AlOrderDetails(int UserId)
        {
            try
            {
                var result = this.orderBL.AllOrderDetails(UserId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Order Book data Fetched", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "There is no order for the User" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }
     
        [HttpDelete("Delete")]
        public IActionResult DeleteOrder(int orderId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = orderBL.DeleteOrder(orderId, userId);
                if (result != null)
                {
                    return this.Ok(new { success = true, data = result });

                }
                else
                {
                    return this.BadRequest();
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
