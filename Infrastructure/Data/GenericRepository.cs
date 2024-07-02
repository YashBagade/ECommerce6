using Core.Entitties;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _storeContext;

        public GenericRepository(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }
        public async Task<IReadOnlyList<T>> GetAllListAsync()
        {
            return await _storeContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsycn(long id)
        {
            return await _storeContext.Set<T>().FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<T> GetEntityWithSpec(ISpecification<T> specification)
        {
            return await ApplySpecifications(specification).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> GetListWithSpec(ISpecification<T> specification)
        {
            return await ApplySpecifications(specification).ToListAsync();
        }

        private IQueryable<T> ApplySpecifications(ISpecification<T> specification)
        {
            return SpecificationQueryBuilder<T>.BuildQuery(_storeContext.Set<T>().AsQueryable(), specification);
        }
    }
}
