
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TestForDotNet.Core.DTOs;
using TestForDotNet.Repository.Models;
using TestForDotNet.Repository.Models.Context;
using TestForDotNet.Repository.Repositories.Interfaces;
using TestForDotNet.Repository.FilterModels;
using AutoMapper.QueryableExtensions;

namespace TestForDotNet.Repository.Repositories
{
    public class OrdersRepository(OrdersDbContext context, IMapper mapper) : IOrdersRepository
    {
        private readonly OrdersDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<List<OrderDTO>> ReadAsync(OrderFilterModel? filter = null, bool prefetchWindows = false, bool prefetchSubElements = false)
        {
            IQueryable<Order> query = _context.Orders;

            query = Filter(query, filter);

            if (prefetchWindows)
                query = query.Include(x => x.Windows);

            if (prefetchSubElements)
                query = query.Include(x => x.Windows).ThenInclude(y => y.SubElements);

            var list = await query.ToListAsync();

            return query.ProjectTo<OrderDTO>(_mapper.ConfigurationProvider).ToList();
        }
        public async Task<OrderDTO> ReadSingleAsync(OrderFilterModel? filter = null , bool prefetchWindows = false, bool prefetchSubElements = false)
        {
            var list = await ReadAsync(filter, prefetchWindows, prefetchSubElements);

            return list.FirstOrDefault() ?? new OrderDTO();
        }

        public async Task<List<OrderDTO>> CreateAsync(List<OrderDTO> orders)
        {
            var entities = orders.AsQueryable().ProjectTo<Order>(_mapper.ConfigurationProvider).ToList();

            await _context.AddRangeAsync(entities);

            await _context.SaveChangesAsync();

            return entities.AsQueryable().ProjectTo<OrderDTO>(_mapper.ConfigurationProvider).ToList();
        }

        public async Task DeleteAsync(List<OrderDTO> orders)
        {
            var entities = _mapper.Map<List<Order>>(orders);

            _context.RemoveRange(entities);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(List<OrderDTO> orders) 
        {
            var entities = orders.AsQueryable().ProjectTo<Order>(_mapper.ConfigurationProvider).ToList();

            _context.UpdateRange(entities);

            await _context.SaveChangesAsync();

            _context.ChangeTracker.Clear();
        }

        public IQueryable<Order> Filter(IQueryable<Order> query, OrderFilterModel? filter)
        {
            if (filter == null)
                return query;

            if (filter.Id.HasValue)
            {
                query = query.Where(entity => entity.OrderId == filter.Id);
            }

            return query;
        }
    }
}