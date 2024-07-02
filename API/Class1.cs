
using AutoMapper;
using Core.Entitties;

namespace API.Helpers
{
    public class ProductUrlResolver: IValueResolver<Product,ProductDto,string>
    {
        private readonly IConfiguration configuration;

        public ProductUrlResolver(IConfiguration _configuration)
        {
            this.configuration = _configuration;
        }

        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrWhiteSpace(source.PictureUrl))
            {
                return configuration["ApiUrl"] + source.PictureUrl;
            }
            return null;
        }
    }
}

