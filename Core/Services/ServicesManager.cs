using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Services_Absractions;
using Shared;

namespace Services
{
    public class ServicesManager(IMapper mapper
                                 ,IUnitOfWork unitOfWork
                                 ,IBasketRepository basketRepository 
                                 ,ICachReposatory cachReposatory
                                 ,UserManager<AppUser> userManager,
                                 IOptions<JwtOptions> options
                                ) : IServiceManager
    {
        public IProductServices ProductServices {  get;} =new ProductService(mapper,unitOfWork);

        public IBasketService BasketService { get; } = new BasketService(basketRepository, mapper);

        public ICachService CachService { get; } = new CashServices(cachReposatory);

        public IAuthService AuthService { get; } = new AuthService(mapper,userManager,options);

        public IOrderService OrderService { get; } = new OrderService(unitOfWork,mapper,basketRepository);
    }
}
