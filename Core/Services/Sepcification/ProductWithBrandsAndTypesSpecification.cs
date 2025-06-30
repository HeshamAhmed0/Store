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
        public ProductWithBrandsAndTypesSpecification(int? BrandId, int? TypeId, string? Sort, int PSize, int PIndex) 
            :base( 
                 p=>(!BrandId.HasValue || p.BrandId==BrandId)&&
                    (!TypeId.HasValue || p.TypeId == TypeId)
                 
                  )
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
            ApplySorting(Sort);
            ApplyPagination(PSize,PIndex);
        }

        protected void ApplySorting(string? sort)
        {
            if (!string.IsNullOrEmpty(sort))
            {
                switch(sort.ToLower())
                {
                    case "nameasc":
                        AddOrderBy(p => p.Name);
                        break;
                    case "namedesc":
                        AddOrderByDesc(P=>P.Name); 
                        break;
                    case "priceasc":
                        AddOrderBy(P => P.Price);
                         break;
                    case "pricedesc":
                        AddOrderByDesc(P => P.Price);
                        break;
                    default:
                        AddOrderBy(P => P.Name);
                        break;
                }
            }
            else
            {
                AddOrderBy(P => P.Name);
            }
        }
        protected void ApplyPagination(int PSize,int PIndex)
        {
            Ispagination = true;
            Take = PSize;
            Skip= (PIndex-1)*PSize;
        }
    }
}
