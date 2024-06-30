using Core.Entitties;
using Core.Interfaces;
using Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _storeContext;
        public ProductRepository(StoreContext storeContext) // store context cha instance pan DI karun milato.DBContext class mention kela aahe tithe
        {
            _storeContext = storeContext;
        }

        public async Task<IReadOnlyList<ProductBrand>> GetAllBrands()
        {
            return await _storeContext.ProductBrands.ToListAsync();
        }

        public async Task<IReadOnlyList<ProductType>> GetAllTypes()
        {
            return await _storeContext.ProductTypes.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(long id)
        {
            return await _storeContext.Products
                         .Include(p => p.ProductBrand)
                         .Include(p => p.ProductType)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await _storeContext.Products
                         .Include(p=> p.ProductType)
                         .Include(p => p.ProductBrand)
                         .ToListAsync();
        }

        public async Task<bool> SeedData()
        {
            bool isSuccess = false;
            try
            {
                //1. Seed ProductBrands dummy data
                var txtProductBrands = File.ReadAllText("D:/My Projects/Ecommerce App with .net core and angular/Infrastructure/Data/SeedDataJsons/brands.json");
                if (!string.IsNullOrWhiteSpace(txtProductBrands))
                {
                    var brandsData = JsonSerializer.Deserialize<List<ProductBrand>>(txtProductBrands);
                    if (brandsData.Any())
                    {
                        _storeContext.ProductBrands.AddRange(brandsData);
                    }
                }

                //2.Seed product types dummy data 
                var txtProductTypes = File.ReadAllText("D:/My Projects/Ecommerce App with .net core and angular/Infrastructure/Data/SeedDataJsons/types.json");
                if (!string.IsNullOrWhiteSpace(txtProductTypes))
                {
                    var typesData = JsonSerializer.Deserialize<List<ProductType>>(txtProductTypes);
                    if (typesData.Any())
                    {
                        _storeContext.ProductTypes.AddRange(typesData);
                    }
                }
                //3. Seed products dummy data 
                var txtProducts = File.ReadAllText("D:/My Projects/Ecommerce App with .net core and angular/Infrastructure/Data/SeedDataJsons/products.json");
                if (!string.IsNullOrWhiteSpace(txtProducts))
                {
                    var products = JsonSerializer.Deserialize<List<Product>>(txtProducts);
                    if (products.Any())
                    {
                        _storeContext.Products.AddRange(products);
                    }
                }

                if (_storeContext.ChangeTracker.HasChanges())
                {
                    await _storeContext.SaveChangesAsync();
                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
                throw new Exception(ex.Message);
            }
            return isSuccess;
        }
    }
}
