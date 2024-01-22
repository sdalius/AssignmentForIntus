using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using TestForDotNet.Core.DTOs;
using TestForDotNet.Repository.FilterModels;
using TestForDotNet.Repository.Models;
using TestForDotNet.Repository.Models.Context;
using TestForDotNet.Repository.Repositories.Interfaces;

namespace TestForDotNet.Repository.Repositories
{
    public class WindowRepository(OrdersDbContext context, IMapper mapper) : IWindowRepository
    {
        private readonly OrdersDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<List<WindowDTO>> ReadAsync()
        {
            var list = await _context.Windows.ProjectTo<WindowDTO>(_mapper.ConfigurationProvider).ToListAsync();
            return list;
        }

        public async Task<WindowDTO> ReadSingleAsync()
        {
            IQueryable<Window> query = _context.Windows;

            var window = await query.FirstAsync();

            var mappedWindow = new WindowDTO();
            _mapper.Map(window, mappedWindow);

            return mappedWindow;
        }

        public async Task<List<WindowDTO>> CreateAsync(List<WindowDTO> windows)
        {
            var entities = windows.AsQueryable().ProjectTo<Window>(_mapper.ConfigurationProvider).ToList();

            await _context.AddRangeAsync(entities);

            await _context.SaveChangesAsync();

            return entities.AsQueryable().ProjectTo<WindowDTO>(_mapper.ConfigurationProvider).ToList();
        }

        public async Task DeleteAsync(List<WindowDTO> orders)
        {
            var entities = orders.AsQueryable().ProjectTo<Window>(_mapper.ConfigurationProvider).ToList();

            _context.RemoveRange(entities);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(List<WindowDTO> orders)
        {
            var entities = orders.AsQueryable().ProjectTo<Window>(_mapper.ConfigurationProvider).ToList();

            _context.UpdateRange(entities);

            await _context.SaveChangesAsync();
        }
    }
}