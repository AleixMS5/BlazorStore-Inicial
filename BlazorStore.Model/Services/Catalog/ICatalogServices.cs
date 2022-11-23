using BlazorStore.Model.Data;
using BlazorStore.Shared.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorStore.Model.Services.Catalog
{
    public interface ICatalogServices
    {
        Task<IReadOnlyList<ProductDto>> GetHighlightedProductsAsync(int count);
        Task<IReadOnlyList<ProductDto>> SearchAsync(SearchCriteria criteria);
    }
}