using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using Services_Absractions;
using Shared;

namespace Services
{
    public class ProductService : IProductServices
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public ProductService(IMapper mapper,IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync()
        {
            var Brands =await unitOfWork.GenericReposatory<ProductBrand,int>().GetAllAsync();
            var result =mapper.Map<IEnumerable<BrandResultDto>>(Brands);
            return result;
        }

        public async Task<IEnumerable<ProductResultDto>> GetAllProductAsync()
        {
           var product=await unitOfWork.GenericReposatory<Product, int>().GetAllAsync();
           
            var result =mapper.Map<IEnumerable<ProductResultDto>>(product);

            return result;
        }

        public async Task<IEnumerable<TypeResultDto>> GetAllTypesAsync()
        {
            var Types =await unitOfWork.GenericReposatory<ProductType, int>().GetAllAsync();
            if (Types == null) return null;
            var result = mapper.Map<IEnumerable<TypeResultDto>>(Types);
            return result;
        }

        public async Task<ProductResultDto?> GetProductAsync(int id)
        {
            var product = unitOfWork.GenericReposatory<Product,int>().GetByID(id);
            if (product == null) return null;
            var result = mapper.Map<ProductResultDto>(product);
            return result;
        }
    }
}
