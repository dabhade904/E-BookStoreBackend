using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IUserRL
    {
        public RegistrationModel UserRegistration(RegistrationModel userRegistration);
        public string UserLogin(LoginModel loginModel);
        public string ForgetPassword(string emailId);


    }
}
