using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IAdminRL
    {
        public AdminRegistrationModel AdminRegistration(AdminRegistrationModel model);
    }
}
