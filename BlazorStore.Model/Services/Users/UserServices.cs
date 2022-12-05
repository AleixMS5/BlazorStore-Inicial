using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BlazorStore.Model.Data;
using BlazorStore.Shared.Dto;
using Microsoft.EntityFrameworkCore;

namespace BlazorStore.Model.Services.Users
{
    public class UserServices : IUserServices
    {
        private readonly DbContextOptions<BlazorStoreContext> dbo;
        private readonly IMapper _mapper;

        public UserServices(DbContextOptions<BlazorStoreContext> odb, IMapper mapper)
        {
            dbo = odb;
            _mapper = mapper;
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            using (var db = new BlazorStoreContext(dbo))
            {
                var user = await db.Users
                .Where(u => u.Id == id)
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync()??new UserDto();
                return user;
                //var user = await db.Users.FirstOrDefaultAsync(u => u.Id == id) ?.ToList().Select(u => new UserDto()
                //{
                //    Id = id,
                //    Name = u.Name,
                //   Address= u.Address,
                //   IsAdmin= u.IsAdmin,
                //   City= u.City,

                //});
                //return user;
            }
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            using (var db = new BlazorStoreContext(dbo))
            {
                var result = await db.Users.AnyAsync(u => u.Email == email);
                return result;
            }
        }

        public async Task<UserDto> GetUserByCredentialsAsync(string email, string password)
        {
            using (var db = new BlazorStoreContext(dbo))
            {
                var passwordHash = ComputeSha256Hash(password);
                var user = await db.Users
                    .Where(u => u.Email == email && u.PasswordHash == passwordHash)
                    .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync();
                return user;
            }
        }

        public async Task CreateUserAsync(NewUserDto newUserDto)
        {
            using (var db = new BlazorStoreContext(dbo))
            {
                var newUser = _mapper.Map<User>(newUserDto);
                newUser.CreationDate = DateTime.Now;
                newUser.PasswordHash = ComputeSha256Hash(newUserDto.Password);
                db.Users.Add(newUser);
                await db.SaveChangesAsync();
            }
        }

        public async Task<IReadOnlyList<UserDto>> GetUserPageAsync(int page, int pageSize, string sortField, bool sortAscending)
        {
            using (var db = new BlazorStoreContext(dbo))
            {
                IQueryable<User> query = db.Users;

                Expression<Func<User, object>> sortExpression;

                switch (sortField?.ToLowerInvariant())
                {
                    case "name":
                        sortExpression = p => p.Name;
                        break;
                    case "email":
                        sortExpression = p => p.Email;
                        break;
                    case "profile":
                        sortExpression = p => p.IsAdmin;
                        break;
                    case "date":
                        sortExpression = p => p.CreationDate;
                        break;
                    default:
                        sortExpression = p => p.Id;
                        break;
                }

                query = sortAscending ? query.OrderBy(sortExpression) : query.OrderByDescending(sortExpression);

                var result = await query
                    .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return result;
            }
        }

        public async Task<int> CountUsersAsync()
        {
            using (var db = new BlazorStoreContext(dbo))
            {
                var result = await db.Users.CountAsync();
                return result;
            }
        }

        public async Task<bool> SetRoleAsync(int id, bool isAdmin)
        {
            using (var db = new BlazorStoreContext(dbo))
            {
                var user = await db.Users.FirstOrDefaultAsync(u => u.Id == id);
                if (user != null)
                {
                    user.IsAdmin = isAdmin;
                    return await db.SaveChangesAsync() == 1;
                }

                return false;
            }
        }

        private static string ComputeSha256Hash(string str)
        {
            using (var sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(str));
                var builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}