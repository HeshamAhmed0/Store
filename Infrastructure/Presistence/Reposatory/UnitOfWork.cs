using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;

namespace Persistence.Reposatory
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext storeDbContext;
        private readonly Dictionary<string, object> _reposatories;

        public UnitOfWork(StoreDbContext storeDbContext)
        {
            this.storeDbContext = storeDbContext;
            _reposatories = new Dictionary<string, object>();
        }
        public IGenericReposatory<TEntity, Tkey> GenericReposatory<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>
        {
            var type= typeof(TEntity).Name;
            if (!_reposatories.ContainsKey(type))
            {
                var reposatory = new GenericReposatory<TEntity,Tkey>(storeDbContext);
                _reposatories.Add(type, reposatory);
            }
            return (IGenericReposatory<TEntity, Tkey>) _reposatories[type];
        }

        public async Task<int> SaveChanges()
        {
          return await storeDbContext.SaveChangesAsync();
        }
    }
}
