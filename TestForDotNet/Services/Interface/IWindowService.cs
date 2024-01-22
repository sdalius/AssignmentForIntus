using TestForDotNet.Core.DTOs;

namespace TestForDotNet.Services.Interface
{
    public interface IWindowService
    {
        Task<List<WindowDTO>> CreateAsync(List<WindowDTO> windows);
        Task DeleteAsync(List<WindowDTO> windows);
        Task UpdateAsync(List<WindowDTO> windows);
    }
}
