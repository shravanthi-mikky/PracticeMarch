using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IProductBL
    {
        public ProductModel AddProduct(ProductModel Prod);
        public ProductModel3 UpdateProduct(ProductModel3 Prod, long ProductId);
        public List<ProductModel2> GetAllProducts();
        public object RetriveProductDetails(long ProductId);

        public bool DeleteProduct(long ProductId);
    }
}
