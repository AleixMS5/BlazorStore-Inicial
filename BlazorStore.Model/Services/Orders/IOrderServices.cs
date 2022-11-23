using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorStore.Shared.Dto;

namespace BlazorStore.Model.Services.Orders
{
    public interface IOrderServices
    {
        Task<int> SubmitOrderAsync(NewOrderDto dto, int userId);
        Task<IReadOnlyList<ExistingOrderDto>> GetOrdersAsync(int userId);
        Task<IReadOnlyList<ExistingOrderDto>> GetOrderPageAsync(int year, int month, int page, int pageSize,
            string sortField, bool sortAscending);
        Task<int> CountOrdersAsync(int year, int month);
        Task SetOrderStatusAsync(int orderId, int statusId);
    }
}