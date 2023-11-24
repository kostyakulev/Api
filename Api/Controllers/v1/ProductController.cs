using Api.Models;
using Api.Services.ProductServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore;


namespace Api.Controllers.v2
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _productServices;

        public ProductController(IProductServices productServices)
        {
            _productServices = productServices;
        }
        /// <summary>
        /// Get all products.
        /// </summary>
        /// <returns>Returns a list of all products.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<Product>), 200)] // Specifies the data type for a successful response
        public async Task<ActionResult<List<Product>>> GetAllProduct()
        {
            return await _productServices.GetAllProduct();
        }
        /// <summary>
        /// Get information about a specific product by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the product.</param>
        /// <returns>Returns information about a specific product.</returns>        
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Product), 200)] // Specifies the data type for a successful response
        [ProducesResponseType(404)] // Specifies the response when the product is not found
        public  async Task<ActionResult<Product>> GetSingleProduct(int id)
        {
            var singleProduct = await _productServices.GetSingleProduct(id);
            if (singleProduct == null)
                return NotFound("Product not found.");
            return Ok(singleProduct);
        }
        /// <summary>
        /// Add a new product.
        /// </summary>
        /// <param name="product">Data for the new product.</param>
        /// <returns>Returns information about the added product.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Product), 200)] // Specifies the data type for a successful response
        [ProducesResponseType(404)] // Specifies the response when the product is not found
        [ProducesResponseType(400)] // Specifies the response for a bad request
        public async Task<ActionResult<Product>> AddProduct(Product product)
        {
            try
            {
                var singleProduct = await _productServices.AddProductAsync(product);
                if (singleProduct == null)
                    return NotFound("Product not found.");
                return Ok(singleProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"Bed request {ex.Message}");
            }
        }
        /// <summary>
        /// Update information about a product by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the product to update.</param>
        /// <param name="product">New data for updating the product.</param>
        /// <returns>Returns information about the updated product.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(List<Product>), 200)] // Specifies the data type for a successful response
        [ProducesResponseType(404)] // Specifies the response when the product is not found
        [ProducesResponseType(400)] // Specifies the response for a bad request
        public async Task<ActionResult<Product>> UpdateProduct( int id, Product product)
        {
            try
            {
                var singleProduct = await _productServices.UpdateProductAsync(id, product);
                if (singleProduct == null)
                    return NotFound("Product not found.");

                return Ok(singleProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"Bed request {ex.Message}");
            }

        }
        /// <summary>
        /// Delete a product by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the product to delete.</param>
        /// <returns>Returns information about the deleted product.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(404)] // Specifies the response when the product is not found
        public async Task<ActionResult> DeleteProduct(int id)
        {
            try
            {
                var result = await _productServices.DeleteProductAsync(id);
                if (result == false)
                    return NotFound("Product not found.");

                return Ok();
            } catch(Exception ex) {
                return BadRequest(ex);
            }

        }
    }
}
