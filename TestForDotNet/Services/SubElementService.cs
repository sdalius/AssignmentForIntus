using TestForDotNet.Core.DTOs;
using TestForDotNet.Repository.Repositories.Interfaces;
using TestForDotNet.Services.Interface;

namespace TestForDotNet.Services
{
    public class SubElementService(ISubElementRepository subElementRepository) : ISubElementService
    {
        private readonly ISubElementRepository _subElementRepository = subElementRepository;

        public async Task CreateAsync(List<SubElementDTO> subElements)
        {
            if (subElements == null || subElements.Count == 0)
                return;

            await _subElementRepository.CreateAsync(subElements);
        }

        public async Task DeleteAsync(List<SubElementDTO> subElements)
        {
            if (subElements == null || subElements.Count == 0)
                return;

            await _subElementRepository.DeleteAsync(subElements);
        }

        public async Task UpdateAsync(List<SubElementDTO> subElements)
        {
            if (subElements == null || subElements.Count == 0)
                return;

            await _subElementRepository.UpdateAsync(subElements);
        }
    }
}
