using AQTTan.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AQTTan.Controllers
{
    public class ItemsController : Controller
    {
        
        public async Task<IActionResult> ItemsByComponent(int componentId)
        {
            List<Item> items = new List<Item>();
            using (var httpClient = new HttpClient())
            {
                // Replace the URL with the correct endpoint to get items by component ID
                using (var response = await httpClient.GetAsync($"https://localhost:7143/api/Items/byComponent/{componentId}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        items = JsonConvert.DeserializeObject<List<Item>>(content);
                    }
                }
            }
            return View(items);
        }

        // دالة لعرض تفاصيل العنصر المحدد
        public async Task<IActionResult> Details(int id)
        {
            Item item = new Item();

            using (var httpClient = new HttpClient())
            {
                // طلب API لجلب تفاصيل العنصر باستخدام معرف العنصر
                using (var response = await httpClient.GetAsync("https://localhost:7143/api/Items/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        item = JsonConvert.DeserializeObject<Item>(content);
                    }
                }
            }

            // إرجاع عرض التفاصيل مع العنصر المسترجع
            return View(item);
        }
    }
}
