using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class FeedbackRL : IFeedbackRL
    {
        private readonly IConfiguration config;

        SqlConnection sqlConnection;
        string ConnString = "Data Source=LAPTOP-2UH1FDRP\\MSSQLSERVER01;Initial Catalog=RoleBased;Integrated Security=True;";
        public FeedbackRL(IConfiguration config)
        {
            this.config = config;
        }


        public FeedbackModel AddFeedback(FeedbackModel Prod)
        {
            try
            {
                using (sqlConnection = new SqlConnection(ConnString))
                {
                    SqlCommand com = new SqlCommand("Sp_AddFeedback1", sqlConnection);
                    com.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();
                    com.Parameters.AddWithValue("@Id", Prod.Id);
                    com.Parameters.AddWithValue("@ProductId", Prod.ProductId);
                    com.Parameters.AddWithValue("@Comment", Prod.Comment);
                    com.Parameters.AddWithValue("@Rating", Prod.Rating);

                    com.ExecuteNonQuery();
                    return Prod;

                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        //Get feedback

        public object RetriveFeedback(long ProductId)
        {
            SqlConnection conn = new SqlConnection(ConnString);
            SqlCommand com = new SqlCommand("spGetFeedbacks", conn);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@ProductId", ProductId);
            conn.Open();
            FeedbackModel ProductModel = new FeedbackModel();
            SqlDataReader rd = com.ExecuteReader();
            if (rd.HasRows)
            {
                while (rd.Read())
                {
                    ProductModel.Id = Convert.ToInt32(rd["Id"]);
                    ProductModel.ProductId = Convert.ToInt32(rd["ProductId"]);
                    ProductModel.Comment = rd["Comment"].ToString();
                    ProductModel.Rating = Convert.ToInt32(rd["Rating"]);
                }
                return ProductModel;
            }
            return null;
        }
    }
}
