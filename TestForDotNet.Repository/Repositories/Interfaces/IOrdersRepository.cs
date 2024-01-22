using TestForDotNet.Core.DTOs;
using TestForDotNet.Repository.FilterModels;

namespace TestForDotNet.Repository.Repositories.Interfaces
{
    public interface IOrdersRepository
    {
        Task<List<OrderDTO>> ReadAsync(OrderFilterModel? filter = null, bool prefetchWindows = false, bool prefetchSubElements = false);
        Task<OrderDTO> ReadSingleAsync(OrderFilterModel? filter = null, bool prefetchWindows = false, bool prefetchSubElements = false);
        Task<List<OrderDTO>> CreateAsync(List<OrderDTO> orders);
        Task DeleteAsync(List<OrderDTO> orders);
        Task UpdateAsync(List<OrderDTO> orders);
    }
}