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
        public UserRegistrationModel UserRegistration(UserRegistrationModel registrationModel)
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
        public string UserLogin(UserLoginModel loginModel)
        {
            try
            {
                return iuserRL.UserLogin(loginModel);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string ForgetPassword(string email)
        {
            try
            {
                return iuserRL.ForgetPassword(email);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool ResetPassword(string resetPassword, string confirmPassword, string EmailId)
        {
            try
            {
                return iuserRL.ResetPassword(resetPassword, confirmPassword, EmailId);  
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
