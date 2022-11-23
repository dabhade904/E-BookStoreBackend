using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class AddressBL : IAddressBL
    {
        private readonly IAddressRL addressRL;
        public AddressBL(IAddressRL addressRL)
        {
            this.addressRL = addressRL;
        }
        public string AddAddress(int userId, AddressModel addressModel)
        {
            try
            {
                return addressRL.AddAddress(userId, addressModel);
            }catch(Exception e)
            {
                throw e;
            }
        }
    }
}
