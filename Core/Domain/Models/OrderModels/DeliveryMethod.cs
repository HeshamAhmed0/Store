using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.OrderModels
{
    public class DeliveryMethod :BaseEntity<int>
    {
        public DeliveryMethod()
        {
        }

        public DeliveryMethod(string shortName, string description, decimal cost, int deliveryTime)
        {
            ShortName = shortName;
            Description = description;
            Cost = cost;
            DeliveryTime = deliveryTime;
        }

        public string ShortName { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public int DeliveryTime { get; set; }
    }
}
