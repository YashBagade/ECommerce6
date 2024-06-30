
using Core.Entitties;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductRepository _productRepository;
        public ProductsController(IProductRepository productRepository) // Dependency Injection. So this service must be registered in the Program.cs file.
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        [Route("fetchproducts")]
        //url : api/products
        public async Task<List<Product>?> GetProducts()
        {
            return await _productRepository.GetProductsAsync() as List<Product>;
        }

        [HttpGet("{id}")]
        //url : api/products/12
        public async Task<Product> GetProduct(long id)
        {
            var response = new Product();
            if (id > 0)
            {
                response = await _productRepository.GetProductByIdAsync(id);
            }
            return response;
        }

        [HttpGet]
        public async Task<bool> SeedData()
        {
           return await _productRepository.SeedData();
        }

        [HttpGet("fetchBrands")]
        public async Task<IReadOnlyList<ProductBrand>> GetAllBrands()
        {
            return await _productRepository.GetAllBrands();
        }

        [HttpGet("fetchTypes")]
        public async Task<IReadOnlyList<ProductType>> GetAllTypes()
        {
            return await _productRepository.GetAllTypes();
        }
    }
}
