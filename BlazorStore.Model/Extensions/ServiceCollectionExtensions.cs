using System;
using AutoMapper;
using BlazorStore.Model.Data;
using BlazorStore.Model.Mapping;
using BlazorStore.Model.Services.Catalog;
using BlazorStore.Model.Services.Categories;
using BlazorStore.Model.Services.Orders;
using BlazorStore.Model.Services.Products;
using BlazorStore.Model.Services.Users;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBlazorStoreModelServices(this IServiceCollection services)
        {
            services.AddDbContext<BlazorStoreContext>();
            services.AddScoped<ICategoryServices, CategoryServices>();
            services.AddScoped<IProductServices, ProductServices>();
            services.AddScoped<ICatalogServices, CatalogServices>();
            services.AddScoped<IOrderServices, OrderServices>();
            services.AddScoped<IUserServices, UserServices>();
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            return services;
        } 
    }
}
