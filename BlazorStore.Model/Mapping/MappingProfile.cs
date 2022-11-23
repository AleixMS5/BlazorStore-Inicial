using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using BlazorStore.Model.Data;
using BlazorStore.Shared.Dto;

namespace BlazorStore.Model.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();
            CreateMap<Category, CategoryDto>();
            CreateMap<Order, ExistingOrderDto>()
                .ForMember(o => o.Status, opt => opt.MapFrom(m => m.Status.ToString()));
            CreateMap<NewOrderDto, Order>();
            CreateMap<OrderLineDto, OrderLine>();
            CreateMap<OrderLine, OrderLineDto>();
            CreateMap<User, UserDto>();
            CreateMap<NewUserDto, User>()
                .ForMember(u => u.PasswordHash, opt => opt.Ignore())
                .ForMember(u => u.IsAdmin, opt => opt.Ignore())
                .ForMember(u => u.Id, opt => opt.Ignore());
        }
    }
}
