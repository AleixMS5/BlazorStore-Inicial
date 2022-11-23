using BlazorStore.Model.Data;
using BlazorStore.Shared.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorStore.Model.Services.Categories
{
    public interface ICategoryServices
    {
        Task<IReadOnlyList<CategoryDto>> GetAllAsync();
    }
}
