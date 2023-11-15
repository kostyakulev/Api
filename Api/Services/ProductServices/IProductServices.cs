namespace Api.Services.ProductServices
{
    public interface IProductServices
    {
        Task<List<Product>> GetAllProduct();
        Task<Product?> GetSingleProduct(int id);
        Task<List<Product>> AddProductAsync(Product product);
        Task<List<Product>?> UpdateProductAsync(int id, Product product);
        Task<List<Product>?> DeleteProductAsync(int id);
    }
}
