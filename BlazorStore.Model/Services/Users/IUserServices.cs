using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorStore.Shared.Dto;

namespace BlazorStore.Model.Services.Users
{
    public interface IUserServices
    {
        Task<UserDto> GetUserByCredentialsAsync(string email, string password);
        Task<UserDto> GetUserByIdAsync(int id);
        Task<bool> EmailExistsAsync(string email);
        Task CreateUserAsync(NewUserDto newUser);
        Task<IReadOnlyList<UserDto>> GetUserPageAsync(int page, int pageSize, string sortField, bool sortAscending);
        Task<int> CountUsersAsync();
        Task<bool> SetRoleAsync(int id, bool isAdmin);
    }
}