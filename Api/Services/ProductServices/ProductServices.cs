using Microsoft.AspNetCore.Mvc;

namespace Api.Services.ProductServices
{
    public class ProductServices : IProductServices
    {

        private readonly TestDatabaseContext _context;

        public ProductServices(TestDatabaseContext context)
        {
            _context = context;
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var singleProduct = await _context.Products.FindAsync(id);
            if (singleProduct == null)
                return false;

            _context.Products.Remove(singleProduct);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Product>> GetAllProduct()
        {
           var products = await _context.Products.ToListAsync();
           return products;
        }

        public async Task<Product?> GetSingleProduct(int id)
        {
            var singleProduct = _context.Products.FirstOrDefault(p => p.ProductId == id);
            if (singleProduct == null)
                return null;
            return singleProduct;
        }

        public async Task<Product?> UpdateProductAsync(int id, Product product)
        {
            var singleProduct = await _context.Products.FindAsync(id);
            if (singleProduct == null)
                return null;

            singleProduct.ProductName = product.ProductName;
            singleProduct.Price = product.Price;

            await _context.SaveChangesAsync();

            return singleProduct;
        }
    }
    
}
