using Core.Entitties;


namespace Core.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetProductByIdAsync(long id);
        Task<IReadOnlyList<Product>> GetProductsAsync();
        Task<bool> SeedData();
        Task<IReadOnlyList<ProductBrand>> GetAllBrands();
        Task<IReadOnlyList<ProductType>> GetAllTypes();

    }
}
