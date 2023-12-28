using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Repository
{
    public static class SpecificationsEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> entryPoint, ISpecifications<T> spec)
        {
            var query = entryPoint;

            if(spec.WhereCriteria is not null) 
                query = query.Where(spec.WhereCriteria);

            if(spec.OrderBy is not null)
                query = query.OrderBy(spec.OrderBy);

            else if (spec.OrderByDesc is not null)
                query = query.OrderBy(spec.OrderByDesc);

            if(spec.IsPaginationEnabled == true)
                query = query.Skip(spec.Skip).Take(spec.Take);

            if (spec.IncludeCriterias.Count != 0)
                query = spec.IncludeCriterias.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));

            return query;
        }
    }
}
