using TestForDotNet.Core.DTOs;

namespace TestForDotNet.Repository.Repositories.Interfaces
{
    public interface IWindowRepository
    {
        Task<List<WindowDTO>> ReadAsync();
        Task<WindowDTO> ReadSingleAsync();
        Task<List<WindowDTO>> CreateAsync(List<WindowDTO> orders);
        Task DeleteAsync(List<WindowDTO> orders);
        Task UpdateAsync(List<WindowDTO> orders);
    }
}
