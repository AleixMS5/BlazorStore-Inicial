using System.Collections.Generic;
using BlazorStore.Model.Data;
using BlazorStore.Shared.Dto;
using System.Threading.Tasks;

namespace BlazorStore.Model.Services.Products
{
    public interface IProductServices
    {
        Task<ProductDto> GetByIdAsync(int id);
        Task<IReadOnlyList<ProductDto>> GetProductPageAsync(int page, int pageSize, string sortField, bool sortAscending);
        Task<int> GetProductsCountAsync();
        Task<bool> RemoveAsync(int productId);
        Task<bool> UpdateAsync(ProductDto productDto);
        Task CreateAsync(ProductDto productDto);
    }
}