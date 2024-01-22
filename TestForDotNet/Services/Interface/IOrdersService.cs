using TestForDotNet.Core.DTOs;
using TestForDotNet.Repository.FilterModels;

namespace TestForDotNet.Services.Interface
{
    public interface IOrdersService
    {
        Task<List<OrderDTO>> ReadOrdersAsync(OrderFilterModel? filter = null, bool prefetchWindows = false, bool prefetchsubElements = false);
        Task<OrderDTO> ReadSingleOrderAsync(OrderFilterModel? filter = null, bool prefetchWindows = false, bool prefetchsubElements = false);
        Task<List<OrderDTO>> CreateOrdersAsync(List<OrderDTO> orders);
        Task UpdateOrdersAsync(List<OrderDTO> orders);
        Task DeleteOrdersAsync(List<OrderDTO> orders);
    }
}
