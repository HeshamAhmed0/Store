using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models;
using Shared;

namespace Services.MappingProfile
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductResultDto>().
                    ForMember(N => N.BrandName, n => n.MapFrom(x => x.ProductBrand.Name)).
                    ForMember(N => N.TypeName, n => n.MapFrom(x => x.ProductType.Name));

            CreateMap<ProductBrand, BrandResultDto>();
            CreateMap<ProductType, TypeResultDto>();

        }
    }
}
