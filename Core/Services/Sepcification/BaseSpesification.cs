using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;

namespace Services.Sepcification
{
    public class BaseSpesification<TEntity, TKey> : ISpecification<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public Expression<Func<TEntity, bool>>? Critiacal { get; set; }
        public List<Expression<Func<TEntity, object>>> IncludeExpression { get; set; } = new List<Expression<Func<TEntity, object>>>();
        public Expression<Func<TEntity, object>>? OrderBy { get ; set ; }
        public Expression<Func<TEntity, object>>? OrderByDesc { get ; set; }
        public int Skip { get; set ; }
        public int Take { get ; set; }
        public bool Ispagination { get ; set; }

        public BaseSpesification(Expression<Func<TEntity, bool>>? expression)
        {
            Critiacal = expression;
        }
        protected void AddInclude(Expression<Func<TEntity, object>> expression)
        {
            IncludeExpression.Add(expression);
        }
        protected void AddOrderBy(Expression<Func<TEntity, object>> expression)
        {
            OrderBy=expression;
        }
        protected void AddOrderByDesc(Expression<Func<TEntity, object>> expression)
        {
            OrderByDesc=expression;
        }

    }
}
