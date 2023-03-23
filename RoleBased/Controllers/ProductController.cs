using BusinessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace RoleBased.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        IProductBL iProductBL;
        public ProductController(IProductBL iProductBL)
        {
            this.iProductBL = iProductBL;
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost]
        [Route("AddProduct")]

        public IActionResult AddProduct(ProductModel Prod)
        {
            try
            {

                var result = iProductBL.AddProduct(Prod);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Product Added Successfull", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Adding of Product was Unsuccessfull" });
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Success = false, message = e.Message });
            }

        }

        [HttpPut("Update")]
        public IActionResult UpdateProduct(ProductModel3 books, long ProductId)
        {
            try
            {
                var reg = this.iProductBL.UpdateProduct(books, ProductId);
                if (reg != null)

                {
                    return this.Ok(new { Success = true, message = "Product Updated Sucessfull", Response = reg });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Product details not updated" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

        [HttpDelete("Delete")]
        public IActionResult DeleteProduct(long ProductId)
        {
            try
            {
                var reg = this.iProductBL.DeleteProduct(ProductId);
                if (reg != null)

                {
                    return this.Ok(new { Success = true, message = "Product Deleted Sucessfull", Response = reg });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Unable to delete" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

        [HttpGet("Get")]
        public IActionResult GetAllProducts()
        {
            try
            {
                var reg = this.iProductBL.GetAllProducts();
                if (reg != null)

                {
                    return this.Ok(new { Success = true, message = "All Product Details", Response = reg });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Unable to get details" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

        [HttpGet("GetProductById")]
        public IActionResult RetriveProductDetails(long ProductId)
        {
            try
            {
                var reg = this.iProductBL.RetriveProductDetails(ProductId);
                if (reg != null)

                {
                    return this.Ok(new { Success = true, message = "Product Details", Response = reg });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Unable to get Product details" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

    }
}
