using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly TestDatabaseContext _context;

        public ProductController(TestDatabaseContext context)
        {
            _context = context;
        }




        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProduct()
        {
            return Ok(await _context.Products.ToListAsync());
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<List<Product>>> GetSingleProduct(int id)
        {
            var singleProduct = await _context.Products.FindAsync(id);
            if (singleProduct == null)
                return NotFound("sorry, the product was not found");
            return Ok(singleProduct);
        }
        [HttpPost]
        public async Task<ActionResult<List<Product>>> AddProduct(Product product)
        {
             _context.Products.Add(product);
            return Ok(await _context.Products.ToArrayAsync());
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<List<Product>>> UpdateProduct(int id,Product product)
        {
            var singleProduct = await _context.Products.FindAsync(id);
            if (singleProduct == null)
                return NotFound("sorry, the product was not found");

            singleProduct.ProductName = product.ProductName;
            singleProduct.ProductId = product.ProductId;
            singleProduct.Price = product.Price;

            return Ok(await _context.Products.ToListAsync());
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Product>>> DeleteProduct(int id)
        {
            var singleProduct = await _context.Products.FindAsync(id);
            if (singleProduct == null)
                return NotFound("sorry, the product was not found");

            _context.Products.Remove(singleProduct); 
            return Ok(await _context.Products.ToListAsync());
        }
    }
}
