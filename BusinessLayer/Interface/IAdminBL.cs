using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IAdminBL
    {
        public AdminRegistrationModel AdminRegistration(AdminRegistrationModel model);
        public string AdminLogin(AdminLoginModel model);
        public string ForgetPassword(string email);
        public bool AdminResetPassword(string resetPassword, string confirmPassword, string EmailId);

    }
}
