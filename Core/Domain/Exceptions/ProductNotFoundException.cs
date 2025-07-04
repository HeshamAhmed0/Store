using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class ProductNotFoundException(int id)
                 :NotFoundExceptions($"Product Wit Id {id} Not Found !!")
    {
    }
}
