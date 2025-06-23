using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Contracts
{
    public interface IGenericReposatory<TEntity,Tkey> where TEntity : BaseEntity<Tkey>
    {
        public Task<IEnumerable<TEntity>> GetAllAsync(bool TrackChanges =false);
        public Task GetByID(int id);
        public Task Add(TEntity entity);
        public void Update(TEntity entity);
        public void Delete(TEntity entity);

    }
}
