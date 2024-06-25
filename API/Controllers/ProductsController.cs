
using Core.Entitties;
using Infrastructure.DataContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private StoreContext _context;
        public ProductsController(StoreContext context) // Dependency Injection. So this service must be registered in the Program.cs file.
        {
            _context = context;
        }

        [HttpGet]
        [Route("fetchproducts")]
        //url : api/products
        public async Task<List<Product>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        [HttpGet("{id}")]
        //url : api/products/12
        public async Task<Product> GetProduct(long id)
        {
            var response = new Product();
            if (id > 0)
            {
                response = await _context.Products.FindAsync(id);
            }
            return response;
        }
    }
}
