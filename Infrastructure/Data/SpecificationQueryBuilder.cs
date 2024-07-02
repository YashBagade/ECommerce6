using Core.Entitties;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Data
{
    public class SpecificationQueryBuilder<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> BuildQuery(IQueryable<TEntity> inputQuery, 
            ISpecification<TEntity> spec)
        {
            var query = inputQuery;
            if(spec != null && spec.Criteria != null)
                query = query.Where(spec.Criteria);

            if (spec != null && spec.Includes.Count > 0)
            {
                // Aggregate mhanje accumator sarkhe ahe javascrip madhlya. 'query' he seed value aahe tyachi for below aggregate
                query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));
            }
                

            return query;
        }
    }
}
