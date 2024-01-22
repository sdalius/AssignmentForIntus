using TestForDotNet.Core.DTOs;
using TestForDotNet.Repository.Repositories.Interfaces;
using TestForDotNet.Services.Interface;

namespace TestForDotNet.Services
{
    public class WindowService(IWindowRepository windowRepository) : IWindowService
    {
        private readonly IWindowRepository _windowRepository = windowRepository;

        public async Task<List<WindowDTO>> CreateAsync(List<WindowDTO> windows)
        {
            if (windows == null || windows.Count == 0)
                return [];

            var returnedWindows = await _windowRepository.CreateAsync(windows);
            return returnedWindows;
        }

        public async Task UpdateAsync(List<WindowDTO> windows)
        {
            if (windows == null || windows.Count == 0)
                return;

            await _windowRepository.UpdateAsync(windows);
        }

        public async Task DeleteAsync(List<WindowDTO> windows)
        {
            if (windows == null || windows.Count == 0)
                return;

            await _windowRepository.DeleteAsync(windows);
        }
    }
}
