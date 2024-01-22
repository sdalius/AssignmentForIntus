using TestForDotNet.Core.DTOs;
using TestForDotNet.Repository.FilterModels;
using TestForDotNet.Repository.Repositories.Interfaces;
using TestForDotNet.Services.Interface;

namespace TestForDotNet.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IOrdersRepository _ordersRepository;

        public OrdersService(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public async Task<List<OrderDTO>> ReadOrdersAsync(OrderFilterModel? filter = null, bool prefetchWindows = false, bool prefetchsubElements = false)
        {
            var orders = await _ordersRepository.ReadAsync(filter, prefetchWindows, prefetchsubElements);
            return orders;
        }

        public async Task<OrderDTO> ReadSingleOrderAsync(OrderFilterModel? filter = null, bool prefetchWindows = false, bool prefetchsubElements = false)
        {
            var order = await _ordersRepository.ReadSingleAsync(filter, prefetchWindows, prefetchsubElements);
            return order;
        }

        public async Task<List<OrderDTO>> CreateOrdersAsync(List<OrderDTO> orders)
        {
            if (orders == null || orders.Count == 0)
                return [];

            var returnedOrders = await _ordersRepository.CreateAsync(orders);
            return returnedOrders;
        }

        public async Task UpdateOrdersAsync(List<OrderDTO> orders)
        {
            if (orders == null || orders.Count == 0)
                return;

            await _ordersRepository.UpdateAsync(orders);
        }

        public async Task DeleteOrdersAsync(List<OrderDTO> orders)
        {
            if (orders == null || orders.Count == 0)
                return;

            await _ordersRepository.DeleteAsync(orders);
        }
    }
}