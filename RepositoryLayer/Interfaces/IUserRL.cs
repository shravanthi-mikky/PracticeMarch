using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRL
    {
        public RegistrationModel Register(RegistrationModel registrationModel);

        public string Login(LoginModel loginModel);
    }
}
