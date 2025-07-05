using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services_Absractions
{
    public interface IServiceManager
    {
        public IProductServices ProductServices { get; }
        public IBasketService BasketService { get; }
    }
}
