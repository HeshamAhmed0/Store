using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Shared;

namespace Services.Sepcification
{
    public class ProductWithCountSpecification : BaseSpesification<Product, int>
    {
        public ProductWithCountSpecification(ProductSpecificationParameters SpecParams) :
            base(p =>
                      (string.IsNullOrEmpty(SpecParams.Search) || p.Name.ToLower().Contains(SpecParams.Search.ToLower())) &&
                      (!SpecParams.BrandId.HasValue || p.BrandId == SpecParams.BrandId) &&
                      (!SpecParams.TypeId.HasValue || p.TypeId == SpecParams.TypeId))
        {
        }
    }
}
