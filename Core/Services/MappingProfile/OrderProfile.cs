using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models.Identity;
using Domain.Models.OrderModels;
using Shared.OrderDtos;
using StackExchange.Redis;

namespace Services.MappingProfile
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<DeliveryMethod, DeliveryMethodDto>().ReverseMap();
            CreateMap<OrderAdress,OrderAdressDto>().ReverseMap();
            CreateMap<Address, OrderAdressDto>().ReverseMap();
            CreateMap<OrderItem,OrderItemDto>().ReverseMap();
            CreateMap<Orders, OrderResultDto>().
                ForMember(D => D.PaymentStatus, O => O.MapFrom(S => S.PaymentStatus.ToString())).
                ForMember(D => D.DeliveryMethod, o => o.MapFrom(S => S.DeliveryMethod.ShortName)).
                ForMember(D=>D.Total,o=>o.MapFrom(s=>s.SubTotal + s.DeliveryMethod.Cost));
        }
    }
}
