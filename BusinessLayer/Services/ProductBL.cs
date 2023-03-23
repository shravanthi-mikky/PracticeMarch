using BusinessLayer.Interfaces;
using CommonLayer.Model;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class ProductBL : IProductBL
    {
        IProductRL iProductRL;
        public ProductBL(IProductRL iProductRL)
        {
            this.iProductRL = iProductRL;
        }

        public ProductModel AddProduct(ProductModel Prod)
        {
            try
            {
                return iProductRL.AddProduct(Prod);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public ProductModel3 UpdateProduct(ProductModel3 Prod, long ProductId)
        {
            try
            {
                return iProductRL.UpdateProduct(Prod, ProductId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<ProductModel2> GetAllProducts()
        {
            try
            {
                return iProductRL.GetAllProducts();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public object RetriveProductDetails(long ProductId)
        {
            try
            {
                return iProductRL.RetriveProductDetails(ProductId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool DeleteProduct(long ProductId)
        {
            try
            {
                return iProductRL.DeleteProduct(ProductId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
