using BusinessLayer.Interface;
using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL iuserRL;
        public UserBL(IUserRL iuserRL)
        {
            this.iuserRL = iuserRL;
        }

        public RegistrationModel UserRegistration(RegistrationModel registrationModel)
        {
            try
            {
                return iuserRL.UserRegistration(registrationModel);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
