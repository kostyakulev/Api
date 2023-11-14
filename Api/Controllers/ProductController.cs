using Microsoft.AspNetCore.Mvc;


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
    }
}
