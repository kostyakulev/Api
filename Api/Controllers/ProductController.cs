using Api.Models;
using Api.Services.ProductServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore;


namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public ActionResult<List<Product>> AddProduct(Product product)
        {
            var singleProduct = _productServices.AddProductAsync(product);
            if (singleProduct == null)
                return NotFound("Product not found.");
            return Ok(singleProduct);
        }
        [HttpPut("{id}")]
        public ActionResult<List<Product>> UpdateProduct(int id, Product product)
        {
            var singleProduct = _productServices.UpdateProductAsync(id, product);
            if (singleProduct == null)
                return NotFound("Product not found.");

            return Ok(singleProduct);
        }
        [HttpDelete("{id}")]
        public ActionResult<List<Product>> DeleteProduct(int id)
        {
            var result = _productServices.DeleteProductAsync(id);
            if (result == null)
                return NotFound("Product not found.");

            return Ok(result);
        }
    }
}
