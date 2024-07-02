
using AutoMapper;
using Core.Entitties;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductRepository _productRepository;
        private readonly IGenericRepository<Product> _prodRepo;
        private readonly IGenericRepository<ProductBrand> _prodBrandRepo;
        private readonly IGenericRepository<ProductType> _prodTypeRepo;
        private readonly IMapper _mapper;

        // Dependency Injection. So this service must be registered in the Program.cs class.
        public ProductsController(IProductRepository productRepository,
            IGenericRepository<Product> prodGenRepo, IGenericRepository<ProductBrand> prodBrandRepo,
            IGenericRepository<ProductType> prodTypeRepo, IMapper mapper) 
        {
            _productRepository = productRepository;
            _prodRepo = prodGenRepo;
            _prodBrandRepo = prodBrandRepo;
            _prodTypeRepo = prodTypeRepo;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("fetchproducts")]
        public async Task<List<ProductDto>?> GetProducts()
        {
            var spec = new ProductWithTypeAndBrandSpecification();
             var products =  await _prodRepo.GetListWithSpec(spec) as List<Product>;
            if (products.Any())
                return _mapper.Map<List<Product>, List<ProductDto>>(products);
            return new List<ProductDto>();
        }

        [HttpGet("{id}")]
        public async Task<ProductDto?> GetProduct(long id)
        {
            var response = new ProductDto();
            if (id > 0)
            {
                var spec = new ProductWithTypeAndBrandSpecification(id);
                var result =  await _prodRepo.GetEntityWithSpec(spec);
                if (result != null)
                {
                    response = _mapper.Map<Product, ProductDto>(result);
                }
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
            return await _prodBrandRepo.GetAllListAsync();
        }

        [HttpGet("fetchTypes")]
        public async Task<IReadOnlyList<ProductType>> GetAllTypes()
        {
            return await _prodTypeRepo.GetAllListAsync();
        }
    }
}
