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
        private readonly BlazorStoreContext _ctx;
        private readonly IMapper _mapper;

        public OrderServices(BlazorStoreContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }
        public async Task<int> SubmitOrderAsync(NewOrderDto dto, int userId)
        {
            var order = _mapper.Map<Order>(dto);
            order.UserId = userId;
            order.Amount = order.Lines.Aggregate(0.0, (t, line) => t += (line.Quantity * line.UnitPrice));
            _ctx.Orders.Add(order);
            await _ctx.SaveChangesAsync();
            return order.Id;
        }

        public async Task<IReadOnlyList<ExistingOrderDto>> GetOrdersAsync(int userId)
        {
            var orders = await _ctx.Orders
                .Include(o => o.Lines)
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.Date)
                .ProjectTo<ExistingOrderDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return orders;
        }

        public async Task<IReadOnlyList<ExistingOrderDto>> GetOrderPageAsync(int year, int month, int page, int pageSize, string sortField, bool sortAscending)
        {
            IQueryable<Order> query = _ctx.Orders.Where(o => o.Date.Month == month && o.Date.Year == year);

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

        public async Task<int> CountOrdersAsync(int year, int month)
        {
            var result = await _ctx.Orders
                .CountAsync(o => o.Date.Month == month && o.Date.Year == year);
            return result;
        }

        public async Task SetOrderStatusAsync(int orderId, int statusId)
        {
            var order = await _ctx.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
            if (order != null)
            {
                order.Status = (OrderStatus)statusId;
                if (order.Status == OrderStatus.Delivered)
                {
                    order.DeliveryDate = DateTime.Now;
                }
                await _ctx.SaveChangesAsync();
            }
        }
    }
}
