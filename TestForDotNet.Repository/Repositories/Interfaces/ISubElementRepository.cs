using TestForDotNet.Core.DTOs;

namespace TestForDotNet.Repository.Repositories.Interfaces
{
    public interface ISubElementRepository
    {
        Task<List<SubElementDTO>> ReadAsync();
        Task CreateAsync(List<SubElementDTO> orders);
        Task DeleteAsync(List<SubElementDTO> orders);

        Task UpdateAsync(List<SubElementDTO> orders);
    }
}
