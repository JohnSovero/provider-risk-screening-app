using PuppeteerSharp;
using System.Text.Json;
using backend.Models;

namespace backend.Services
{
    public class FirmaService
    {
        public async Task<List<object>> ObtenerFirmas(string nombre, string pagina)
        {
            try
            {   
                if (pagina == "WorldBank"){
                    await new BrowserFetcher().DownloadAsync();

                    await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions{ Headless = true,});

                    await using var page = await browser.NewPageAsync();
                    await page.GoToAsync("https://projects.worldbank.org/en/projects-operations/procurement/debarred-firms", new NavigationOptions
                    {
                        Timeout = 30000,
                        WaitUntil = new[] { WaitUntilNavigation.Networkidle2 }
                    });

                    await page.WaitForSelectorAsync("#category", new WaitForSelectorOptions { Timeout = 30000 });
                    await page.WaitForSelectorAsync("#k-debarred-firms .k-grid-content table tbody tr", new WaitForSelectorOptions { Timeout = 30000 });

                    // Escribe el nombre en el input de búsqueda
                    await page.TypeAsync("#category", nombre);

                    // Esperar a que los resultados se actualicen y se estabilicen
                    await page.EvaluateFunctionAsync(@"() => new Promise(resolve => {
                        let lastCount = -1;
                        let stableCount = 0;

                        const interval = setInterval(() => {
                            const currentCount = document.querySelectorAll('#k-debarred-firms .k-grid-content table tbody tr').length;
                            if (currentCount === lastCount) {
                                stableCount++;
                                if (stableCount >= 3) {
                                    clearInterval(interval);
                                    resolve(true);
                                }
                            } else {
                                stableCount = 0;
                                lastCount = currentCount;
                            }
                        }, 1000);
                    })");

                    // Extraer resultados
                    var data = await page.EvaluateFunctionAsync(@"(nombre) => {
                        const rows = document.querySelectorAll('#k-debarred-firms .k-grid-content table tbody tr');

                        return Array.from(rows).map(row => {
                            const cells = row.querySelectorAll('td');
                            return cells.length > 0 ? {
                                web: 'WorldBank',
                                firmName: cells[0]?.innerText?.trim(),
                                address: cells[2]?.innerText?.trim(),
                                country: cells[3]?.innerText?.trim(),
                                fromDate: cells[4]?.innerText?.trim(),
                                toDate: cells[5]?.innerText?.trim(),
                                grounds: cells[6]?.innerText?.trim()
                            } : null;
                        }).filter(row => row !== null)
                    }", nombre);

                    var lista = JsonSerializer.Deserialize<List<WorldBankResponse>>(data?.ToString() ?? "[]");
                    return lista?.Cast<object>().ToList() ?? new List<object>();
                }
                else if(pagina == "LeaksDatabase"){
                    Console.WriteLine("Leaks Database.");
                    return new List<object>();
                }
                else if(pagina == "OFAC"){
                    await new BrowserFetcher().DownloadAsync();

                    await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true });
                    await using var page = await browser.NewPageAsync();

                    await page.GoToAsync("https://sanctionssearch.ofac.treas.gov/", new NavigationOptions
                    {
                        Timeout = 300000,
                        WaitUntil = new[] { WaitUntilNavigation.Networkidle2 }
                    });

                    await page.TypeAsync("#ctl00_MainContent_txtLastName", nombre);
                    await page.ClickAsync("#ctl00_MainContent_btnSearch");

                    await page.WaitForSelectorAsync("#gvSearchResults", new WaitForSelectorOptions { Timeout = 300000 });
                    await page.EvaluateFunctionAsync(@"() => new Promise((resolve) => {
                        let lastCount = 0;
                        let stableCount = 0;

                        const interval = setInterval(() => {
                            const currentCount = document.querySelectorAll('#gvSearchResults tr').length;
                            if (currentCount === lastCount) {
                                stableCount++;
                                if (stableCount >= 3) {
                                    clearInterval(interval);
                                    resolve(true);
                                }
                            } else {
                                stableCount = 0;
                                lastCount = currentCount;
                            }
                        }, 1000);
                    })");
                    var data = await page.EvaluateFunctionAsync(@"() => {
                        const rows = document.querySelectorAll('#gvSearchResults tr');

                        return Array.from(rows).map(row => {
                            const cells = row.querySelectorAll('td');
                            return cells.length > 0 ? {
                                name: cells[0]?.innerText?.trim(),
                                address: cells[1]?.innerText?.trim(),
                                type: cells[2]?.innerText?.trim(),
                                programs: cells[3]?.innerText?.trim(),
                                list: cells[4]?.innerText?.trim(),
                                score: cells[5]?.innerText?.trim(),
                                web: 'OFAC'
                            } : null;
                        }).filter(f => f !== null);
                    }");

                    var lista = JsonSerializer.Deserialize<List<OfacResponse>>(data?.ToString() ?? "[]");
                    return lista?.Cast<object>().ToList() ?? new List<object>();
                }
                return new List<object>();
            }
            catch (PuppeteerException pex)
            {
                Console.WriteLine($"Error de Puppeteer: {pex.Message}");
                return new List<object>();
            }
            catch (TimeoutException tex)
            {
                Console.WriteLine($"Timeout: {tex.Message}");
                return new List<object>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general: {ex.Message}");
                return new List<object>();
            }
        }
    }
}
