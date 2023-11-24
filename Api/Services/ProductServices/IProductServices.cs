namespace Api.Services.ProductServices
{
    public interface IProductServices
    {
        Task<List<Product>> GetAllProduct();
        Task<Product?> GetSingleProduct(int id);
        Task<Product> AddProductAsync(Product product);
        Task<Product?> UpdateProductAsync(int id, Product product);
        Task<bool> DeleteProductAsync(int id);
    }
}
