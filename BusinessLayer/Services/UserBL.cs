using BusinessLayer.Interfaces;
using CommonLayer.Model;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBL : IUserBL
    {
        IUserRL iUserRL;
        public UserBL(IUserRL iUserRL)
        {
            this.iUserRL=iUserRL;
        }

        public RegistrationModel Register(RegistrationModel registrationModel)
        {
            try
            {
                return iUserRL.Register(registrationModel);
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }

        public string Login(LoginModel loginModel)
        {
            try
            {
                return iUserRL.Login(loginModel);
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }
    }
}
