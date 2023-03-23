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
    public class ProductRL : IProductRL
    {
        private readonly IConfiguration config;

        SqlConnection sqlConnection;
        string ConnString = "Data Source=LAPTOP-2UH1FDRP\\MSSQLSERVER01;Initial Catalog=RoleBased;Integrated Security=True;";
        public ProductRL(IConfiguration config)
        {
            this.config = config;
        }

        public ProductModel AddProduct(ProductModel Prod)
        {
            try
            {
                using (sqlConnection = new SqlConnection(ConnString))
                {
                    SqlCommand com = new SqlCommand("Sp_AddProduct", sqlConnection);
                    com.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();
                    com.Parameters.AddWithValue("@ProductName", Prod.ProductName);
                    com.Parameters.AddWithValue("@Image", Prod.Image);
                    com.Parameters.AddWithValue("@Description", Prod.Description);
                    com.Parameters.AddWithValue("@Rating", Prod.Rating);
                    com.Parameters.AddWithValue("@Price", Prod.Price);
                    com.Parameters.AddWithValue("@AdminId", Prod.AdminId);

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

        //Update Method
        public ProductModel3 UpdateProduct(ProductModel3 Prod, long ProductId)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConnString);
                using (conn)
                {

                    SqlCommand com = new SqlCommand("Sp_UpdateProduct", conn);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@ProductId", ProductId);
                    com.Parameters.AddWithValue("@ProductName", Prod.ProductName);
                    com.Parameters.AddWithValue("@Image", Prod.Image);
                    com.Parameters.AddWithValue("@Description", Prod.Description);
                    com.Parameters.AddWithValue("@Rating", Prod.Rating);
                    com.Parameters.AddWithValue("@Price", Prod.Price);
                    conn.Open();
                    int i = com.ExecuteNonQuery();
                    conn.Close();
                    if (i >= 1)
                    {
                        return Prod;
                    }
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Get All Products

        public List<ProductModel2> GetAllProducts()
        {
            List<ProductModel2> Products = new List<ProductModel2>();
            SqlConnection conn = new SqlConnection(ConnString);
            using (conn)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SpGetAll", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Products.Add(new ProductModel2
                            {
                                ProductId = Convert.ToInt32(reader["ProductId"]),
                                ProductName = reader["ProductName"].ToString(),
                                Description = reader["Description"].ToString(),
                                Image = reader["Image"].ToString(),
                                Rating = reader["Rating"].ToString(),
                                Price = Convert.ToInt32(reader["Price"]),
                                AdminId = Convert.ToInt32(reader["AdminId"])
                            });
                        }
                        return Products;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        // Get one Product

        public object RetriveProductDetails(long ProductId)
        {
            SqlConnection conn = new SqlConnection(ConnString);
            SqlCommand com = new SqlCommand("Retrive_1_Product", conn);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@ProductId", ProductId);
            conn.Open();
            ProductModel2 ProductModel = new ProductModel2();
            SqlDataReader rd = com.ExecuteReader();
            if (rd.HasRows)
            {
                while (rd.Read())
                {
                    ProductModel.ProductId = Convert.ToInt32(rd["ProductId"]);
                    ProductModel.ProductName = rd["ProductName"].ToString();
                    ProductModel.Description = rd["Description"].ToString();
                    ProductModel.Image = rd["Image"].ToString();
                    ProductModel.Rating = rd["Rating"].ToString();
                    ProductModel.Price = Convert.ToInt32(rd["Price"]);
                    ProductModel.AdminId = Convert.ToInt32(rd["AdminId"]);
                }
                return ProductModel;
            }
            return null;
        }

        //Delete

        public bool DeleteProduct(long ProductId)
        {
            SqlConnection conn = new SqlConnection(ConnString);
            SqlCommand com = new SqlCommand("Sp_Delete", conn);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@ProductId", ProductId);

            conn.Open();
            int i = com.ExecuteNonQuery();
            conn.Close();
            if (i >= 1)
            {
                return true;
            }
            return false;
        }

    }
}
