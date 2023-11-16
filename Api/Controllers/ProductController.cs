using Api.Models;
using Api.Services.ProductServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore;


namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _productServices;

        public ProductController(IProductServices productServices)
        {
            _productServices = productServices;
        }
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAllProduct()
        {
            return await _productServices.GetAllProduct();
        }
        [HttpGet("{id}")]
        public ActionResult<List<Product>> GetSingleProduct(int id)
        { 
            var singleProduct = _productServices.GetSingleProduct(id);
            if (singleProduct == null)
                return NotFound("Product not found.");
            return Ok(singleProduct);
        }
        [HttpPost]
        public async Task<ActionResult<List<Product>>> AddProduct(Product product)
        {
            try
            {
                var singleProduct = await _productServices.AddProductAsync(product);
                if (singleProduct == null)
                    return NotFound("Product not found.");
                return Ok(singleProduct);
            }
            catch (Exception)
            {
               return StatusCode(400,"Bed request");
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<List<Product>>> UpdateProduct(int id, Product product)
        {
            try {
                var singleProduct = await _productServices.UpdateProductAsync(id, product);
                if (singleProduct == null)
                    return NotFound("Product not found.");

                return Ok(singleProduct);
            }
            catch (Exception) 
            {
                return StatusCode(400, "Bed request");
            }

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Product>>> DeleteProduct(int id)
        {
            var result = await _productServices.DeleteProductAsync(id);
            if (result == null)
                return NotFound("Product not found.");

            return Ok(result);
        }
    }
}
