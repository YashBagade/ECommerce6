
using Core.Entitties;
using System.Linq.Expressions;

namespace Core.Specifications
{
    public class ProductWithTypeAndBrandSpecification: BaseSpecification<Product>
    {
        public ProductWithTypeAndBrandSpecification()
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
        }

        public ProductWithTypeAndBrandSpecification(long id) : base(p=> p.Id == id)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
        }
    }
}
