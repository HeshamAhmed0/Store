using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services_Absractions;
using Shared.OrderDtos;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController(IServiceManager serviceManager) :ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderReqestDto orderReqestDto)
        {
            var UserEmail =User.FindFirstValue(ClaimTypes.Email);
            var Order =await serviceManager.OrderService.CreateOrderAsync(orderReqestDto, UserEmail);
            return Ok(Order);
        }
        [HttpGet("GetOrders")]
        public async Task<IActionResult> GetOrdersByEmail()
        {
            var UserEmail = User.FindFirstValue(ClaimTypes.Email);
            var Order = await serviceManager.OrderService.GetOrdersByUserEmailAsync(UserEmail);
            return Ok(Order);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrdersById(Guid id)
        {
            var Order = await serviceManager.OrderService.GetOrderByIdAsync(id);
            return Ok(Order);
        }
        [HttpGet("Delivery")]
        public async Task<IActionResult> GetAllDelivery()
        {
            var DeliveryMethods = await serviceManager.OrderService.GetAllDeliveryMethods();
            return Ok(DeliveryMethods);
        }
    }
}
