using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BlazorStore.Model.Data;
using BlazorStore.Shared.Dto;
using Microsoft.EntityFrameworkCore;

namespace BlazorStore.Model.Services.Orders
{
    public class OrderServices : IOrderServices
    {
        private readonly DbContextOptions<BlazorStoreContext> dbo;
        private readonly IMapper _mapper;

        public OrderServices(DbContextOptions<BlazorStoreContext> odb, IMapper mapper)
        {
            dbo = odb;
            _mapper = mapper;
        }
        public async Task<int> SubmitOrderAsync(NewOrderDto dto, int userId)
        {
            using (var db = new BlazorStoreContext(dbo))
            {
                var order = _mapper.Map<Order>(dto);
                order.UserId = userId;
                order.Amount = order.Lines.Aggregate(0.0, (t, line) => t += (line.Quantity * line.UnitPrice));
                db.Orders.Add(order);
                await db.SaveChangesAsync();
                return order.Id;
            }
        }

        public async Task<IReadOnlyList<ExistingOrderDto>> GetOrdersAsync(int userId)
        {
            using (var db = new BlazorStoreContext(dbo))
            {
                var orders = await db.Orders
                    .Include(o => o.Lines)
                    .Where(o => o.UserId == userId)
                    .OrderByDescending(o => o.Date)
                    .ProjectTo<ExistingOrderDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();
                return orders;
            }
            }

        public async Task<IReadOnlyList<ExistingOrderDto>> GetOrderPageAsync(int year, int month, int page, int pageSize, string sortField, bool sortAscending)
        {
            using (var db = new BlazorStoreContext(dbo))
            {
                IQueryable<Order> query = db.Orders.Where(o => o.Date.Month == month && o.Date.Year == year);

                Expression<Func<Order, object>> sortExpression;

                switch (sortField?.ToLowerInvariant())
                {
                    case "date":
                        sortExpression = o => o.Date;
                        break;
                    case "name":
                        sortExpression = o => o.Name;
                        break;
                    case "amount":
                        sortExpression = o => o.Amount;
                        break;
                    case "status":
                        sortExpression = o => o.Status;
                        break;
                    default:
                        sortExpression = p => p.Id;
                        break;
                }

                query = sortAscending ? query.OrderBy(sortExpression) : query.OrderByDescending(sortExpression);

                var result = await query
                    .ProjectTo<ExistingOrderDto>(_mapper.ConfigurationProvider)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
                return result;
            }
        }

        public async Task<int> CountOrdersAsync(int year, int month)
        {
            using (var db = new BlazorStoreContext(dbo))
            {
                var result = await db.Orders
                .CountAsync(o => o.Date.Month == month && o.Date.Year == year);
                return result;
            }
        }

        public async Task SetOrderStatusAsync(int orderId, int statusId)
        {
            using (var db = new BlazorStoreContext(dbo))
            {

                var order = await db.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
                if (order != null)
                {
                    order.Status = statusId;
                    if (((OrderStatus)statusId) == OrderStatus.Delivered)
                    {
                        order.DeliveryDate = DateTime.Now;
                    }
                    try
                    {
                        await db.SaveChangesAsync();

                    }
                    catch (Exception ex)
                    {

                        throw;
                    }
                }
            }
        }
    }
}

