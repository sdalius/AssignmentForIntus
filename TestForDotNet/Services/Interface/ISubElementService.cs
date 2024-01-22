using TestForDotNet.Core.DTOs;

namespace TestForDotNet.Services.Interface
{
    public interface ISubElementService
    {
        Task CreateAsync(List<SubElementDTO> subElements);
        Task DeleteAsync(List<SubElementDTO> subElements);
        Task UpdateAsync(List<SubElementDTO> subElements);
    }
}
