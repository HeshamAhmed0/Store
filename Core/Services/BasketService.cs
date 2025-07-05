using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models;
using Services_Absractions;
using Shared;

namespace Services
{
    public class BasketService(IBasketRepository basketRepository,IMapper imapper) : IBasketService
    {
        
        public async Task<CustomerBasketDto?> GetBasketAsync(string id)
        {
            var CustomerBasket= await basketRepository.GetBasketAsync(id);
            if (CustomerBasket == null) throw new NotFoundBasketException(id);
            var result= imapper.Map<CustomerBasketDto>(CustomerBasket);
            return result;
        }

        public async Task<CustomerBasketDto?> UpdateBasketAsync(CustomerBasketDto customerBasketDto)
        {
            var customerbasket= imapper.Map<CustomerBasket>(customerBasketDto);
            var result=await basketRepository.UpdateBasketAsync(customerbasket, TimeSpan.FromDays(30));
            if (result == null) throw new BasketValidationException();
            var customer =imapper.Map<CustomerBasketDto>(result);
            return customer;
        }
        public async Task<bool> DeleteBasketAsync(string id)
        {
          var Flag=  await basketRepository.DeleteBasketAsync(id);
            if (Flag == null) throw new BadRequestDeleteException();
            return Flag;
        }

    }
}
