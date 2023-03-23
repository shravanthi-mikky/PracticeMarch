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
    public class UserRL : IUserRL
    {
        private readonly IConfiguration config;

        SqlConnection sqlConnection;
        string ConnString = "Data Source=LAPTOP-2UH1FDRP\\MSSQLSERVER01;Initial Catalog=RoleBased;Integrated Security=True;";
        public UserRL(IConfiguration config)
        {
            this.config = config;
        }

        public RegistrationModel Register(RegistrationModel registrationModel)
        {
            sqlConnection = new SqlConnection(ConnString);

            using (sqlConnection)
                try
                {
                    var password = registrationModel.Password;
                    SqlCommand sqlCommand = new SqlCommand("dbo.SP_Register", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@Fullname", registrationModel.Fullname);
                    sqlCommand.Parameters.AddWithValue("@Email", registrationModel.Email);
                    sqlCommand.Parameters.AddWithValue("@Mobile", registrationModel.Mobile);
                    sqlCommand.Parameters.AddWithValue("@Password", registrationModel.Password);
                    sqlCommand.Parameters.AddWithValue("@Role", registrationModel.Role);

                    int result = sqlCommand.ExecuteNonQuery();
                    if (result > 0)
                        return registrationModel;
                    else
                        return null;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    sqlConnection.Close();
                }
        }

        private string GenerateSecurityToken(string Email, int UserId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
                new Claim(ClaimTypes.Role,"User"),
                new Claim(ClaimTypes.Email,Email),
                new Claim("Id",UserId.ToString())
            };
            var token = new JwtSecurityToken(config["Jwt:Key"],
              config["Jwt:Key"],
              claims,
              expires: DateTime.Now.AddMinutes(360),
              signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //Login

        public string Login(LoginModel loginModel)
        {

            using (sqlConnection = new SqlConnection(ConnString))
            try
            {
                    SqlCommand sqlCommand = new SqlCommand("dbo.SP_Login", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();

                    sqlCommand.Parameters.AddWithValue("@Email", loginModel.Email);
                    sqlCommand.Parameters.AddWithValue("@Password", loginModel.Password);

                    SqlDataReader rd = sqlCommand.ExecuteReader();
                    rd.Close();
                    if (rd != null)
                    {
                        string query = "SELECT Id FROM Users WHERE Email = '" + loginModel.Email + "'";
                        SqlCommand cmd = new SqlCommand(query, sqlConnection);
                        cmd.CommandType = System.Data.CommandType.Text;
                        var ID = cmd.ExecuteScalar();
                        int Id = Convert.ToInt32(ID);
                        var token = this.GenerateSecurityToken(loginModel.Email, Id);
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
