using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Contracts
{
    public interface ISpecification<TEntity,Tkey> where TEntity : BaseEntity<Tkey>
    {
       public Expression<Func<TEntity,bool>> Critiacal { get; set; }
        public List<Expression<Func<TEntity,object>>> IncludeExpression {  get; set; }

        public Expression<Func<TEntity,object>>? OrderBy { get; set; }
        public Expression<Func<TEntity,object>>? OrderByDesc { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
        public bool Ispagination { get; set; }

    }
}
