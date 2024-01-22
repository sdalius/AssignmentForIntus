using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TestForDotNet.Core.DTOs;
using TestForDotNet.Repository.Models.Context;
using TestForDotNet.Repository.Models;
using TestForDotNet.Repository.Repositories.Interfaces;
using AutoMapper.QueryableExtensions;

namespace TestForDotNet.Repository.Repositories
{
    public class SubElementRepository : ISubElementRepository
    {
        private readonly OrdersDbContext _context;
        private readonly IMapper _mapper;

        public SubElementRepository(OrdersDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<SubElementDTO>> ReadAsync()
        {
            var list = await _context.Windows.ToListAsync();
            return _mapper.Map<List<SubElementDTO>>(list);
        }

        public async Task CreateAsync(List<SubElementDTO> orders)
        {
            var entities = orders.AsQueryable().ProjectTo<SubElement>(_mapper.ConfigurationProvider).ToList();

            await _context.AddRangeAsync(entities);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(List<SubElementDTO> orders)
        {
            var entities = orders.AsQueryable().ProjectTo<SubElement>(_mapper.ConfigurationProvider).ToList();

            _context.RemoveRange(entities);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(List<SubElementDTO> orders)
        {
            var entities = orders.AsQueryable().ProjectTo<SubElement>(_mapper.ConfigurationProvider).ToList();

            _context.UpdateRange(entities);

            await _context.SaveChangesAsync();
        }
    }
}