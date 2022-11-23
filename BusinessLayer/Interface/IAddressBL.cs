using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IAddressBL
    {
        public string AddAddress(int userId, AddressModel addressModel);
        public bool UpdateAddress(int addressId, AddressModel addressModel);
        public bool DeleteAddress(int addressId);
        public List<AddressModel> GetAllAddresses(int userId);


    }
}
