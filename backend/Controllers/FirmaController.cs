using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using backend.Services;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FirmaController : ControllerBase
    {
        private readonly FirmaService _firmaService;

        public FirmaController(FirmaService firmaService)
        {
            _firmaService = firmaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFirmas([FromQuery] string nombre)
        {
            var resultados = await _firmaService.ObtenerFirmas(nombre);
            if (!resultados.Any())
                return NotFound(new { message = "No se encontraron firmas con ese nombre" });

            return Ok(new { totalHits = resultados.Count, resultados });
        }
    }
}
