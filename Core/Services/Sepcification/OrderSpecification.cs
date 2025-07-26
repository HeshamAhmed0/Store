using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.OrderModels;

namespace Services.Sepcification
{
    public class OrderSpecification : BaseSpesification<Orders, Guid>
    {
        public OrderSpecification(Guid id) : base(o => o.Id==id)
        {
            AddInclude(s=>s.DeliveryMethod);
            AddInclude(s=>s.OrderItems);
        }
        public OrderSpecification(string UserEmail) : base(o => o.UserEmail==UserEmail)
        {
            AddInclude(s=>s.DeliveryMethod);
            AddInclude(s=>s.OrderItems);
        }
    }
}
