using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class NotFoundBasketException(string id):NotFoundExceptions($"Basket With Id : {id} Not Found")
    {
    }
}
