using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class AdminBL : IAdminBL
    {
        private readonly IAdminRL adminRL;
        public AdminBL(IAdminRL adminRL)
        {
            this.adminRL = adminRL;
        }
        public AdminRegistrationModel AdminRegistration(AdminRegistrationModel model)
        {
            try
            {
                return adminRL.AdminRegistration(model);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string AdminLogin(AdminLoginModel model)
        {
            try
            {
                return adminRL.AdminLogin(model);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}