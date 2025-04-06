using backend.Exceptions;
using backend.Infraestructure;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services{
    public class ProviderService : IProviderService{
        private readonly AppDbContext _context;

        public ProviderService(AppDbContext context){
            _context = context;
        }

        // Method that retrieves all providers from the database
        public async Task<IEnumerable<Provider>> GetAllProviders(){   
            return await _context.Providers.ToListAsync();
        }

        // Method that retrieves a provider by its ID
        public async Task<Provider?> GetProviderById(Guid id){
            var provider = await _context.Providers.FindAsync(id);
            if(provider == null) {
                throw new ResourceNotFoundException("Proveedor no encontrado.");
            }
            return await _context.Providers.FindAsync(id);
        }

        // Method to create a new provider
        public async Task<Provider> CreateProvider(ProviderDto dto){
            var existingProvider = await _context.Providers.FirstOrDefaultAsync(p => p.TaxId == dto.TaxId);

            if (existingProvider != null) 
                throw new ResourceDuplicateException("Ya existe un proveedor con el mismo TaxId.");
            
            existingProvider = await _context.Providers.FirstOrDefaultAsync(p => p.BusinessName == dto.BusinessName);
            if (existingProvider != null)
                throw new ResourceDuplicateException("Ya existe un proveedor con el mismo BusinessName.");
            
            var provider = new Provider{
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

        // Method to update an existing provider
        public async Task<Provider?> UpdateProvider(Guid id, ProviderDto dto){
            var provider = await _context.Providers.FindAsync(id);
            if (provider == null) return null;

            var existingProvider = await _context.Providers
                .FirstOrDefaultAsync(p => (p.TaxId == dto.TaxId || p.BusinessName == dto.BusinessName) && p.Id != id);

            if (existingProvider != null)
                throw new ResourceDuplicateException("Ya existe un proveedor con el mismo TaxId o BusinessName.");

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

        // Method to delete a provider by its ID
        public async Task<bool> DeleteProvider(Guid id)
        {
            var provider = await _context.Providers.FindAsync(id);
            if (provider == null) throw new ResourceNotFoundException("Proveedor no encontrado.");

            _context.Providers.Remove(provider);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
