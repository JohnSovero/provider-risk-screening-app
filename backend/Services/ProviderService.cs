using backend.Infraestructure;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class ProviderService : IProviderService
    {
        private readonly AppDbContext _context;

        public ProviderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Provider>> GetAllAsync()
        {
            return await _context.Providers.OrderByDescending(p => p.LastEdited).ToListAsync();
        }

        public async Task<Provider?> GetByIdAsync(Guid id)
        {
            return await _context.Providers.FindAsync(id);
        }

        public async Task<Provider> CreateAsync(ProviderDto dto)
        {
            var provider = new Provider
            {
                Id = Guid.NewGuid(),
                BusinessName = dto.BusinessName,
                TradeName = dto.TradeName,
                TaxId = dto.TaxId,
                Phone = dto.Phone,
                Email = dto.Email,
                Website = dto.Website,
                Address = dto.Address,
                Country = dto.Country,
                AnnualBilling = dto.AnnualBilling,
                LastEdited = DateTime.UtcNow
            };

            _context.Providers.Add(provider);
            await _context.SaveChangesAsync();
            return provider;
        }

        public async Task<Provider?> UpdateAsync(Guid id, ProviderDto dto)
        {
            var provider = await _context.Providers.FindAsync(id);
            if (provider == null) return null;

            provider.BusinessName = dto.BusinessName;
            provider.TradeName = dto.TradeName;
            provider.TaxId = dto.TaxId;
            provider.Phone = dto.Phone;
            provider.Email = dto.Email;
            provider.Website = dto.Website;
            provider.Address = dto.Address;
            provider.Country = dto.Country;
            provider.AnnualBilling = dto.AnnualBilling;
            provider.LastEdited = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return provider;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var provider = await _context.Providers.FindAsync(id);
            if (provider == null) return false;

            _context.Providers.Remove(provider);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}