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
        private readonly BlazorStoreContext _context;
        private readonly IMapper _mapper;

        public CategoryServices(BlazorStoreContext context, IMapper mapper)
        {
            this._context = context;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<CategoryDto>> GetAllAsync()
        {
            var result = await _context.Categories
                .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return result;
        }
    }
}
