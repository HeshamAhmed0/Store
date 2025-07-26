using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Identity;

namespace Domain.Models.OrderModels
{
    public class Orders:BaseEntity<Guid>
    {
        public Orders()
        {
        }

        public Orders(string userEmail, OrderAdress shippingAddress, ICollection<OrderItem> orderItems, decimal subTotal, int paymentIntentId,   DeliveryMethod deliveryMethod )
        {
            Id =Guid.NewGuid();
            UserEmail = userEmail;
            ShippingAddress = shippingAddress;
            OrderItems = orderItems;
            SubTotal = subTotal;
            PaymentIntentId = paymentIntentId;
            DeliveryMethod = deliveryMethod;
        }

        public string UserEmail { get; set; }
        public OrderAdress ShippingAddress { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public decimal SubTotal { get; set; }
        public int PaymentIntentId { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public PaymentStatus PaymentStatus { get; set; } =PaymentStatus.Pending;

        public DeliveryMethod DeliveryMethod { get; set; }
        public int? DeliveryMethodId { get; set; }
    }


}

