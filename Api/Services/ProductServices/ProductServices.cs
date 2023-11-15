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

        public async Task<List<Product>> AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return await _context.Products.ToListAsync();
        }

        public async Task<List<Product>?> DeleteProductAsync(int id)
        {
            var singleProduct = await _context.Products.FindAsync(id);
            if (singleProduct == null)
                return null;

            _context.Products.Remove(singleProduct);
            await _context.SaveChangesAsync();
            return await _context.Products.ToListAsync();
        }

        public async Task<List<Product>> GetAllProduct()
        {
           var products = await _context.Products.ToListAsync();
           return products;
        }

        public async Task<Product?> GetSingleProduct(int id)
        {
            var singleProduct = _context.Products.Find(id);
            if (singleProduct == null)
                return null;
            return singleProduct;
        }

        public async Task<List<Product>?> UpdateProductAsync(int id, Product product)
        {
            var singleProduct = await _context.Products.FindAsync(id);
            if (singleProduct == null)
                return null;

            singleProduct.ProductName = product.ProductName;
            singleProduct.ProductId = product.ProductId;
            singleProduct.Price = product.Price;

            await _context.SaveChangesAsync();

            return await _context.Products.ToListAsync();
        }
    }
    
}
