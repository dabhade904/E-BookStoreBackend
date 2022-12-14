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
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public bool UpdateAddress(int addressId, AddressModel addressModel)
        {
            try
            {
                return addressRL.UpdateAddress(addressId, addressModel);

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public bool DeleteAddress(int addressId)
        {
            try
            {
                return addressRL.DeleteAddress(addressId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<AddressModel> GetAllAddresses(int userId)
        {
            try
            {
                return addressRL.GetAllAddresses(userId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
