using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class UserRegistrationModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public long MobileNumber { get; set; }
    }
}
