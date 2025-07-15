using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Services_Absractions;

namespace Services
{
    public class ServicesManager(IMapper mapper
                                 ,IUnitOfWork unitOfWork
                                 ,IBasketRepository basketRepository 
                                 ,ICachReposatory cachReposatory
                                 ,UserManager<AppUser> userManager
                                ) : IServiceManager
    {
        public IProductServices ProductServices {  get;} =new ProductService(mapper,unitOfWork);

        public IBasketService BasketService { get; } = new BasketService(basketRepository, mapper);

        public ICachService CachService { get; } = new CashServices(cachReposatory);

        public IAuthService AuthService { get; } = new AuthService(userManager);
    }
}
