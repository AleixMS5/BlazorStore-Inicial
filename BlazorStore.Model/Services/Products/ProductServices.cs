using System;
using System.Collections.Generic;
using BlazorStore.Model.Data;
using BlazorStore.Model.Services.Products;
using BlazorStore.Shared.Dto;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace BlazorStore.Model.Services.Products
{
    public class ProductServices : IProductServices
    {
        private readonly DbContextOptions<BlazorStoreContext> dbo;
        private readonly IMapper _mapper;

        public ProductServices(DbContextOptions<BlazorStoreContext> odb, IMapper mapper)
        {
            dbo = odb;
            _mapper = mapper;
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            using (var db = new BlazorStoreContext(dbo))
            {
                var product = await db.Products
                .Where(p => p.Id == id)
                .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
                return product;
            }
        }

        public async Task<int> GetProductsCountAsync()
        {
            using (var db = new BlazorStoreContext(dbo))
            {
                return await db.Products.CountAsync();
            }
        }

        public async Task<bool> RemoveAsync(int productId)
        {
            using (var db = new BlazorStoreContext(dbo))
            {
                var product = await db.Products.FirstOrDefaultAsync(p => p.Id == productId);
                if (product != null)
                {
                    db.Products.Remove(product);
                    await db.SaveChangesAsync();
                    return true;
                }

                return false;
            }
        }

        public async Task<bool> UpdateAsync(ProductDto productDto)
        {
            using (var db = new BlazorStoreContext(dbo))
            {
                var product = await db.Products.FirstOrDefaultAsync(p => p.Id == productDto.Id);
                if (product != null)
                {
                    _mapper.Map(productDto, product);
                    return await db.SaveChangesAsync() == 1;
                }
                return false;
            }
        }

        public async Task CreateAsync(ProductDto productDto)
        {
            using (var db = new BlazorStoreContext(dbo))
            {
                var product = _mapper.Map<Product>(productDto);
                db.Products.Add(product);
                await db.SaveChangesAsync();
            }
        }

        public async Task<IReadOnlyList<ProductDto>> GetProductPageAsync(int page, int pageSize, string sortField, bool sortAscending)
        {
            using (var db = new BlazorStoreContext(dbo))
            {
                IQueryable<Product> query = db.Products;

                Expression<Func<Product, object>> sortExpression;

                switch (sortField?.ToLowerInvariant())
                {
                    case "name":
                        sortExpression = p => p.Name;
                        break;
                    case "lastUpdated":
                        sortExpression = p => p.LastUpdated;
                        break;
                    case "price":
                        sortExpression = p => p.Price;
                        break;
                    default:
                        sortExpression = p => p.Id;
                        break;
                }

                query = sortAscending ? query.OrderBy(sortExpression) : query.OrderByDescending(sortExpression);

                var result = await query
                    .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
                return result;
            }
        }
    }

}
