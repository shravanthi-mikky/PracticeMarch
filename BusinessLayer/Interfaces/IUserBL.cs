using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IUserBL
    {
        public RegistrationModel Register(RegistrationModel registrationModel);
        public string Login(LoginModel loginModel);
    }
}
