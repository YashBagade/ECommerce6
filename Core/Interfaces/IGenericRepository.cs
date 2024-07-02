using Core.Entitties;
using Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
       Task<T> GetByIdAsycn(long id);
       Task<IReadOnlyList<T>> GetAllListAsync();

        Task<T> GetEntityWithSpec(ISpecification<T> specification);
        Task<IReadOnlyList<T>> GetListWithSpec(ISpecification<T> specification);
    }
}
