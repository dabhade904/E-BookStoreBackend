using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IAddressRL
    {
        public string AddAddress(int userId, AddressModel addressModel);
        public bool UpdateAddress(int addressId, AddressModel addressModel);

    }
}
