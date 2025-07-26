using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.OrderDtos;

namespace Services_Absractions
{
    public interface IOrderService
    {
        public Task<OrderResultDto> GetOrderByIdAsync(Guid id);
        public Task<IEnumerable<OrderResultDto>> GetOrdersByUserEmailAsync(string Email);
        public Task<OrderResultDto> CreateOrderAsync(OrderReqestDto order, string UserEmail);
        public Task<IEnumerable<DeliveryMethodDto>> GetAllDeliveryMethods();

    }
}
