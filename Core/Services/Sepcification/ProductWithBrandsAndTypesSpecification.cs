using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Services.Sepcification
{
    public class ProductWithBrandsAndTypesSpecification :BaseSpesification<Product,int>
    {
        public ProductWithBrandsAndTypesSpecification(int Id):base(P=>P.Id==Id)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
        }
        public ProductWithBrandsAndTypesSpecification(int? BrandId, int? TypeId) 
            :base( 
                 p=>(!BrandId.HasValue || p.BrandId==BrandId)&&
                    (!TypeId.HasValue || p.TypeId == TypeId)
                 
                  )
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
        }

    }
}
