using AQTTan.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace AQTTan.Controllers
{
    public class ComposController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<Compo> compos = new List<Compo>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7143/api/Compos"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        compos = JsonConvert.DeserializeObject<List<Compo>>(content);

                    }
                }
            }

            return View(compos);
        }
    }
}
