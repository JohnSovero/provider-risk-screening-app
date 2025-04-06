using backend.Models;

namespace backend.Services{
    public interface IProviderService
    {
        Task<IEnumerable<Provider>> GetAllAsync();
        Task<Provider?> GetByIdAsync(Guid id);
        Task<Provider> CreateAsync(ProviderDto dto);
        Task<Provider?> UpdateAsync(Guid id, ProviderDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}