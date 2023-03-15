using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class AdminRL : IAdminRL
    {
        private readonly IConfiguration config;

        SqlConnection sqlConnection;
        string ConnString = "Data Source=LAPTOP-2UH1FDRP\\MSSQLSERVER01;Initial Catalog=RoleBased;Integrated Security=True;";
        public AdminRL(IConfiguration config)
        {
            this.config = config;
        }

        private string GenerateSecurityToken(string Email, int AdminId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
                new Claim(ClaimTypes.Role,"Admin"),
                new Claim(ClaimTypes.Email,Email),
                new Claim("AdminId",AdminId.ToString())
            };
            var token = new JwtSecurityToken(config["Jwt:Key"],
              config["Jwt:Key"],
              claims,
              expires: DateTime.Now.AddMinutes(360),
              signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        //Admin Login

        //Login

        public string Login(AdminLoginModel loginModel)
        {

            using (sqlConnection = new SqlConnection(ConnString))
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("dbo.SP_AdminLogin", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();

                    sqlCommand.Parameters.AddWithValue("@AdminEmail", loginModel.AdminEmail);
                    sqlCommand.Parameters.AddWithValue("@AdminPassword", loginModel.AdminPassword);

                    SqlDataReader rd = sqlCommand.ExecuteReader();
                    rd.Close();
                    if (rd != null)
                    {
                        string query = "SELECT AdminId FROM AdminTabl WHERE AdminEmail = '" + loginModel.AdminEmail + "'";
                        SqlCommand cmd = new SqlCommand(query, sqlConnection);
                        cmd.CommandType = System.Data.CommandType.Text;
                        var ID = cmd.ExecuteScalar();
                        int Id = Convert.ToInt32(ID);
                        var token = this.GenerateSecurityToken(loginModel.AdminEmail, Id);
                        return token;
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);

                }
                finally { sqlConnection.Close(); }
        }
    }
}
