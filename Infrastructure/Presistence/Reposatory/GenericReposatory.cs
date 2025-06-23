using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Reposatory
{
    public class GenericReposatory<TEntity, Tkey> : IGenericReposatory<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        private readonly StoreDbContext storeDbContext;

        public GenericReposatory(StoreDbContext storeDbContext)
        {
            this.storeDbContext = storeDbContext;
        }
        public async Task Add(TEntity entity)
        {
             await storeDbContext.AddAsync(entity);
        }

        public  void Delete(TEntity entity)
        {
            storeDbContext.Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool TrackChanges = false)
        {
           if(TrackChanges)
            {
               return await storeDbContext.Set<TEntity>().ToListAsync();
            }
            else
            {
                return await storeDbContext.Set<TEntity>().AsNoTracking().ToListAsync();
            }
        }

        public async Task GetByID(int id)
        {
           await storeDbContext.Set<TEntity>().FindAsync(id);
        }

        public void Update(TEntity entity)
        {
           storeDbContext.Set<TEntity>().Update(entity);
        }
    }
}
