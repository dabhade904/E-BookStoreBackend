using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IUserRL
    {
        public UserRegistrationModel UserRegistration(UserRegistrationModel userRegistration);
        public string UserLogin(UserLoginModel loginModel);
        public string ForgetPassword(string emailId);
        public bool ResetPassword(string resetPassword, string confirmPassword, string EmailId);

    }
}
