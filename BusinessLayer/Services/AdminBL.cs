using BusinessLayer.Interfaces;
using CommonLayer.Model;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class AdminBL : IAdminBL
    {
        IAdminRL iAdminRL;
        public AdminBL(IAdminRL iAdminRL)
        {
            this.iAdminRL = iAdminRL;
        }

        public string Login(AdminLoginModel loginModel)
        {
            try
            {
                return iAdminRL.Login(loginModel);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
