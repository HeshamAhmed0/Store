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
            if (typeof(TEntity) == typeof(Product))
            {
                if (TrackChanges)
                {
                    return await storeDbContext.Products.Include(P=>P.ProductType).Include(P=>P.ProductBrand).ToListAsync() as IEnumerable<TEntity>;
                }
                else
                {
                    return await storeDbContext.Products.Include(P => P.ProductType).Include(P => P.ProductBrand).AsNoTracking().ToListAsync() as IEnumerable<TEntity>;
                }

            }

           if(TrackChanges)
            {
               return await storeDbContext.Set<TEntity>().ToListAsync();
            }
            else
            {
                return await storeDbContext.Set<TEntity>().AsNoTracking().ToListAsync();
            }
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity, Tkey> spec, bool TrackChanges = false)
        {
           return await ApplyQuery(spec).ToListAsync();
        }

        public async Task<TEntity> GetByID(int id)
        {
            if (typeof(TEntity) == typeof(Product))
            {
             var result =   await storeDbContext.Products.Include(p=>p.ProductBrand).Include(p=>p.ProductType).FirstOrDefaultAsync(p=>p.Id==id) ;
                return result as TEntity;
            }
            else
            {
              return  await storeDbContext.Set<TEntity>().FindAsync(id);
            }
          
        }

        public async Task<TEntity> GetByID(ISpecification<TEntity, Tkey> spec)
        {
            return await ApplyQuery(spec).FirstOrDefaultAsync();
        }

        public void Update(TEntity entity)
        {
           storeDbContext.Set<TEntity>().Update(entity);
        }

        private IQueryable<TEntity> ApplyQuery(ISpecification<TEntity, Tkey> spec)
        {
            return SpecificationEvaluator.GetQuery<TEntity, Tkey>(storeDbContext.Set<TEntity>(), spec);
        }
    }
}
