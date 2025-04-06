using PuppeteerSharp;
using System.Text.Json;
using backend.Models;

namespace backend.Services
{
    public class FirmaService
    {
        public async Task<List<Firm>> ObtenerFirmas(string nombre)
        {
            try
            {
                await new BrowserFetcher().DownloadAsync();

                await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions
                {
                    Headless = true,
                });

                await using var page = await browser.NewPageAsync();
                await page.GoToAsync("https://projects.worldbank.org/en/projects-operations/procurement/debarred-firms", WaitUntilNavigation.Networkidle2);

                var data = await page.EvaluateFunctionAsync(@"(nombre) => {
                    const rows = document.querySelectorAll('#k-debarred-firms .k-grid-content table tbody tr');
                    return Array.from(rows).map(row => {
                        const cells = row.querySelectorAll('td');
                        return cells.length > 0 ? {
                            firmName: cells[0]?.innerText?.trim(),
                            additionalInfo: cells[1]?.innerText?.trim(),
                            address: cells[2]?.innerText?.trim(),
                            country: cells[3]?.innerText?.trim(),
                            fromDate: cells[4]?.innerText?.trim(),
                            toDate: cells[5]?.innerText?.trim(),
                            grounds: cells[6]?.innerText?.trim()
                        } : null;
                    }).filter(row => row !== null)
                    .filter(firm => firm.firmName && firm.firmName.toLowerCase().includes(nombre.toLowerCase()));
                }", nombre); // Filtrado directamente en la evaluación

                var lista = JsonSerializer.Deserialize<List<Firm>>(data?.ToString() ?? "[]");
                return lista ?? new List<Firm>();
            }
            catch (Exception ex)
            {
                // Maneja excepciones como problemas de conexión, timeout, etc.
                // Loguea el error o maneja según lo necesario
                Console.WriteLine($"Error al obtener firmas: {ex.Message}");
                return new List<Firm>();
            }
        }
    }
}
