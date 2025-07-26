using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.OrderDtos
{
    public class OrderResultDto
    {
        public string UserEmail { get; set; }
        public OrderAdressDto ShippingAddress { get; set; }
        public ICollection<OrderItemDto> OrderItems { get; set; }
        public decimal SubTotal { get; set; }
        public int PaymentIntentId { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public string PaymentStatus { get; set; } 

        public string DeliveryMethod { get; set; }
        public int? DeliveryMethodId { get; set; }
        public decimal Total {  get; set; }
    }
}
