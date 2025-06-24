using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Services_Absractions;

namespace Services
{
    public class ServicesManager(IMapper mapper,IUnitOfWork unitOfWork) : IServiceManager
    {
        public IProductServices ProductServices {  get;} =new ProductService(mapper,unitOfWork);

    }
}
