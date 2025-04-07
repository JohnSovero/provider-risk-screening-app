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
                await page.GoToAsync("https://projects.worldbank.org/en/projects-operations/procurement/debarred-firms", new NavigationOptions
                {
                    Timeout = 30000,
                    WaitUntil = new[] { WaitUntilNavigation.Networkidle2 }
                });
                await page.WaitForSelectorAsync("#k-debarred-firms .k-grid-content table tbody tr", new WaitForSelectorOptions
                {
                    Timeout = 30000
                });

                if (page.IsClosed)
                {
                    Console.WriteLine("La página se cerró antes de tiempo.");
                    return new List<Firm>();
                }
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
                }", nombre);

                var lista = JsonSerializer.Deserialize<List<Firm>>(data?.ToString() ?? "[]");
                return lista ?? new List<Firm>();
            }
            catch (PuppeteerException pex)
            {
                Console.WriteLine($"Error de Puppeteer: {pex.Message}");
                return new List<Firm>();
            }
            catch (TimeoutException tex)
            {
                Console.WriteLine($"Timeout: {tex.Message}");
                return new List<Firm>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general: {ex.Message}");
                return new List<Firm>();
            }
        }
    }
}
