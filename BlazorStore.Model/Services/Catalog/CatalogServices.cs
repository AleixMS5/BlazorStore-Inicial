﻿using BlazorStore.Model.Data;
using BlazorStore.Shared.Dto;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace BlazorStore.Model.Services.Catalog
{
    public class CatalogServices : ICatalogServices
    {
        private readonly DbContextOptions<BlazorStoreContext> dbo;
        private readonly IMapper _mapper;

        public CatalogServices(DbContextOptions<BlazorStoreContext> odb, IMapper mapper)
        {
            dbo = odb;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<ProductDto>> SearchAsync(SearchCriteria criteria)
        {

            using (var db = new BlazorStoreContext(dbo))
            {
                IQueryable<Product> query = db.Products;
                if (criteria.CategoryId != null)
                {
                    query = query.Where(p => p.CategoryId == criteria.CategoryId);
                }
                if (criteria.MinPrice > 0)
                {
                    query = query.Where(p => p.Price > criteria.MinPrice);
                }
                if (criteria.MaxPrice < SearchCriteria.MAX_PRICE)
                {
                    query = query.Where(p => p.Price < criteria.MaxPrice);
                }
                if (!string.IsNullOrWhiteSpace(criteria.Term))
                {
                    query = query.Where(p => EF.Functions.Like(p.Name, $"%{criteria.Term}%") || EF.Functions.Like(p.Description, $"%{criteria.Term}%"));
                }

                IQueryable<Product> products;

                switch (criteria.Sort)
                {
                    case CatalogSort.PriceDesc:
                        products = query.OrderByDescending(p => p.Price);
                        break;
                    case CatalogSort.DiscountDesc:
                        products = query.OrderByDescending(p => p.PrevPrice - p.Price);
                        break;
                    default:
                        products = query.OrderBy(p => p.Price);
                        break;
                }

                var result = await products.ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                                                .ToListAsync();
                return result;
            }
        }

        public async Task<IReadOnlyList<ProductDto>> GetHighlightedProductsAsync(int count)
        {
            using (var db = new BlazorStoreContext(dbo))
            {
                var result = await db.Products
                .Where(p => p.IsHighlighted)
                .OrderBy(p => p.Price)
                .Take(count)
                .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
                return result;
            }
        }

    }

}
