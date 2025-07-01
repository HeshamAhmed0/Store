using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Shared;

namespace Services.Sepcification
{
    public class ProductWithBrandsAndTypesSpecification :BaseSpesification<Product,int>
    {
        public ProductWithBrandsAndTypesSpecification(int Id):base(P=>P.Id==Id)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
        }
        public ProductWithBrandsAndTypesSpecification(ProductSpecificationParameters SpecParams) 
            :base( 
                 p=>
                    (string.IsNullOrEmpty(SpecParams.Search)||p.Name.ToLower().Contains(SpecParams.Search.ToLower()))&&
                    (!SpecParams.BrandId.HasValue || p.BrandId==SpecParams.BrandId)&&
                    (!SpecParams.TypeId.HasValue || p.TypeId == SpecParams.TypeId)
                 
                  )
        {
            
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
            ApplySorting(SpecParams.Sort);
            ApplyPagination(SpecParams.PageSize,SpecParams.PageIndex);
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
