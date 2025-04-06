
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ProvidersController(IProviderService service) : ControllerBase{
        private readonly IProviderService _service = service;

        // Endpoint to get all providers
        [HttpGet]
        public async Task<IActionResult> GetAllProviders() => Ok(await _service.GetAllProviders());

        // Endpoint to get provider by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProviderById(Guid id){
            var provider = await _service.GetProviderById(id);
            return provider is null ? NotFound() : Ok(provider);
        }

        // Endpoint to create a new provider
        [HttpPost]
        public async Task<IActionResult> CreateProvider([FromBody] ProviderDto dto){
            var created = await _service.CreateProvider(dto);
            return CreatedAtAction(nameof(GetProviderById), new { id = created.Id }, created);
        }

        // Endpoint to update an existing provider
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProvider(Guid id, [FromBody] ProviderDto dto){
            var updated = await _service.UpdateProvider(id, dto);
            return updated is null ? NotFound() : Ok(updated);
        }

        // Endpoint to delete a provider
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProvider(Guid id)
        {
            var deleted = await _service.DeleteProvider(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}