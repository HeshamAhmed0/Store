using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> GetQuery<TEntity,Tkey>(IQueryable<TEntity> inputQuery,ISpecification<TEntity,Tkey> spec ) 
            where TEntity :BaseEntity<Tkey>
        {
            var query = inputQuery;
            if( spec.Critiacal is not null)
            {
                query = query.Where(spec.Critiacal);
                
            }
            query = spec.IncludeExpression.Aggregate(query, (currentQuery, IncludeQuery) => currentQuery.Include(IncludeQuery));

            return query;
        }
    }
}
