using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class PaymentRL : IPaymentRL
    {

        private readonly IConfiguration config;

        SqlConnection sqlConnection;
        string ConnString = "Data Source=LAPTOP-2UH1FDRP\\MSSQLSERVER01;Initial Catalog=RoleBased;Integrated Security=True;";
        public PaymentRL(IConfiguration config)
        {
            this.config = config;
        }
        // Payment retrival
        public bool Payment(PaymentModel payModel)
        {
            using (sqlConnection = new SqlConnection(ConnString))
                try
                {
                    string query = "select PaymentId from PayTable where cardHolder='" + payModel.cardHolder + "' and cardNumber='" + payModel.cardNumber + "' and ExpiryDate='" + payModel.ExpiryDate + "' and CVV='" + payModel.CVV + "';";
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                    sqlConnection.Open();


                    var result = sqlCommand.ExecuteScalar();
                    if (result != null)
                    {

                        return true;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);

                }
                finally { sqlConnection.Close(); }

        }

    }
}
