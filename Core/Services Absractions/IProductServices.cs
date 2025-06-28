using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace Services_Absractions
{
    public interface IProductServices
    {
        public Task<IEnumerable<ProductResultDto>> GetAllProductAsync(int? BrandId,int? TypeId);

        public Task<ProductResultDto?> GetProductAsync(int id);
        public Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync();
        public Task<IEnumerable<TypeResultDto>> GetAllTypesAsync();
    }
}
