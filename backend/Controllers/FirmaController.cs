using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using backend.Services;
using backend.Models;

public enum Paginas
{
    WorldBank,
    LeaksDatabase,
    OFAC
}

namespace backend.Controllers{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class FirmaController : ControllerBase
    {
        private readonly FirmaService _firmaService;

        public FirmaController(FirmaService firmaService)
        {
            _firmaService = firmaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFirmas([FromQuery] string nombre, [FromQuery] List<Paginas> paginas)
        {
            if (paginas == null || paginas.Count < 1 || paginas.Count > 3)
            {
                return BadRequest("You must select between 1 and 3 valid pages.");
            }
            Console.WriteLine($"Buscando en las páginas: {string.Join(", ", paginas)}");
            var resultados = new List<object>();
            foreach (var pagina in paginas)
            {
                //console pagina
                Console.WriteLine($"Buscando en la página: {pagina}");
                var resultado = await _firmaService.ObtenerFirmas(nombre, pagina.ToString());
                resultados.AddRange(resultado);
            }
            return Ok(new { totalHits = resultados.Count, resultados });
        }
    }
}
