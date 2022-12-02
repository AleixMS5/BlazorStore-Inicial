using BlazorStore.Model.Data;
using BlazorStore.Shared.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace BlazorStore.Model.Services.Categories
{
    public class CategoryServices: ICategoryServices
    {
        private readonly DbContextOptions<BlazorStoreContext> dbo;
        private readonly IMapper _mapper;

        public CategoryServices(DbContextOptions<BlazorStoreContext> odb, IMapper mapper)
        {
            dbo = odb;
            
            _mapper = mapper;
        }

        public DbContextOptions<BlazorStoreContext> Dbo => dbo;

        public async Task<IReadOnlyList<CategoryDto>> GetAllAsync()
        {
            using (var db = new BlazorStoreContext(Dbo))
            {
                var result = await db.Categories
                .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
                return result;
            }
        }
    }
}
