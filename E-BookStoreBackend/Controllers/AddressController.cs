using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

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
    }
}
