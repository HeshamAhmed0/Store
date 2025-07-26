using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Shared.OrderDtos
{
    public class OrderReqestDto
    {
        public string BasketID { get; set; }
        public OrderAdressDto ShipToAddress { get; set; }
        public int DeliveryMethodId { get; set; }
    }
}
