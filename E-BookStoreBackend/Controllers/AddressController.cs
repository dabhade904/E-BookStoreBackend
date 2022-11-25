using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Linq;

namespace E_BookStoreBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressBL addressBL;
        public AddressController(IAddressBL addressBL)
        {
            this.addressBL = addressBL;
        }

        [HttpPost("addAddress")]
        public IActionResult AddAddress(int UserId, AddressModel addressModel)
        {
            try
            {
                var result = this.addressBL.AddAddress(UserId, addressModel);
                if (result.Equals(" Address Added Successfully"))
                {
                    return this.Ok(new { success = true, message = $"Address Added Successfully " });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = result });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPut("UpdateAddress")]
        public IActionResult UpdateAddress(int AddressId, AddressModel addressModel)
        {
            try
            {
                var result = this.addressBL.UpdateAddress(AddressId, addressModel);
                if (result.Equals(true))
                {
                    return this.Ok(new { success = true, message = $"Address updated Successfully " });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = result });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpDelete("DeleteAddress")]
        public IActionResult DeleteAddress(int AddressId)
        {
            try
            {
                var result = this.addressBL.DeleteAddress(AddressId);
                if (result.Equals(true))
                {
                    return this.Ok(new { success = true, message = $"Address deleted Successfully " });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = result });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize(Roles = Role.User)]
        [HttpGet("GetAllAddress")]
        public IActionResult GetAllAddress()
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = addressBL.GetAllAddresses(userId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Geting AllAddress sucessfully", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Faild to Get All Addresses" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
