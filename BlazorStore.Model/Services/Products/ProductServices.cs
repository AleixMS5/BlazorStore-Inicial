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
        private readonly BlazorStoreContext _context;
        private readonly IMapper _mapper;

        public ProductServices(BlazorStoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var product = await _context.Products
                .Where(p => p.Id == id)
                .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
            return product;
        }

        public async Task<int> GetProductsCountAsync()
        {
            return await _context.Products.CountAsync();
        }

        public async Task<bool> RemoveAsync(int productId)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateAsync(ProductDto productDto)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productDto.Id);
            if (product != null)
            {
                _mapper.Map(productDto, product);
                return await _context.SaveChangesAsync() == 1;
            }
            return false;
        }

        public async Task CreateAsync(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<ProductDto>> GetProductPageAsync(int page, int pageSize, string sortField, bool sortAscending)
        {
            IQueryable<Product> query = _context.Products;

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
