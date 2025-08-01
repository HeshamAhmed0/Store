using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using Domain.Models.OrderModels;
using Persistence.Reposatory;
using Services.Sepcification;
using Services_Absractions;
using Shared.OrderDtos;
using StackExchange.Redis;

namespace Services
{
    public class OrderService(IUnitOfWork unitOfWork,IMapper mapper,IBasketRepository basketRepository) : IOrderService
    {
        public async Task<OrderResultDto> CreateOrderAsync(OrderReqestDto orderRequest, string UserEmail)
        {
            // Get Address
            var Address = mapper.Map<OrderAdress>(orderRequest.ShipToAddress);
           
            //Get Items
            var basket = await basketRepository.GetBasketAsync(orderRequest.BasketID);
            if (basket == null) throw new Exception("Basket Not Found");


            var OrderItems =new List<OrderItem>();
            foreach(var item in basket.Items)
            {
                var product = await unitOfWork.GenericReposatory<Product, int>().GetByID(item.Id);
                if (product == null) throw new Exception("Product Not Found");
                var OrderItem =new OrderItem(product.Id, product.Name,product.PictureUrl,item.Quantity,item.Price);
                OrderItems.Add(OrderItem);
            }
            //Get Deivery Methods
            var DelivaryMethod =await unitOfWork.GenericReposatory<DeliveryMethod,int>().GetByID(orderRequest.DeliveryMethodId);
            if (DelivaryMethod == null) throw new Exception("Delivery Method Not Found");

            // Get SubTotal 
            var SubTotal = OrderItems.Sum(s => s.Price * s.Quntity);


            var Order =new Orders(UserEmail,Address,OrderItems,SubTotal,0,DelivaryMethod);
            await unitOfWork.GenericReposatory<Orders,Guid>().Add(Order);
            var count = await unitOfWork.SaveChanges();
            if (count == 0) throw new Exception("Not Correct");
            var result =mapper.Map<OrderResultDto>(Order);
            return result;

        }

        public async Task<IEnumerable<DeliveryMethodDto>> GetAllDeliveryMethods()
        {
           var DeliveryMethod =await unitOfWork.GenericReposatory<DeliveryMethod,int>().GetAllAsync();
            var result = mapper.Map<IEnumerable<DeliveryMethodDto>>(DeliveryMethod);
            return result;
        }

        public async Task<OrderResultDto> GetOrderByIdAsync(Guid id)
        {
            var spec = new OrderSpecification(id);
             var order = await unitOfWork.GenericReposatory<Orders,Guid>().GetAllAsync(spec);
            var result = mapper.Map<OrderResultDto>(order);
            return result;
        }

        public async Task<IEnumerable<OrderResultDto>> GetOrdersByUserEmailAsync(string Email)
        {
            var spec = new OrderSpecification(Email);
            var order = await unitOfWork.GenericReposatory<Orders, Guid>().GetAllAsync(spec);
            var result = mapper.Map<IEnumerable<OrderResultDto>>(order);
            return result;
        }
    }
}
