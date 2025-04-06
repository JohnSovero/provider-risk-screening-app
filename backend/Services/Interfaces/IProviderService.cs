using backend.Models.DTOs;

namespace backend.Services{
    public interface IProviderService
    {
        Task<IEnumerable<Provider>> GetAllProviders();
        Task<Provider?> GetProviderById(Guid id);
        Task<Provider> CreateProvider(ProviderDto dto);
        Task<Provider?> UpdateProvider(Guid id, ProviderDto dto);
        Task<bool> DeleteProvider(Guid id);
    }
}