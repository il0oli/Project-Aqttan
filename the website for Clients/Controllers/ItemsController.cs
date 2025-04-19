using System.Net.Http;
using System.Text;
using ClientAqttan.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClientAqttan.Controllers
{
    public class ItemsController : Controller
    {
       
        public async Task<IActionResult> Index()
        {
            List<Item> items = new List<Item>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7143/api/Items"))
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
        // New action to display items by component
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


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Item item, IFormFile? Image)
        {
            if (!ModelState.IsValid)
            {
                return View(item);
            }

            if (Image != null && Image.Length > 0)
            {

                using (var memoryStream = new MemoryStream())

                {

                    await Image.CopyToAsync(memoryStream);
                    item.Image = memoryStream.ToArray();
                }
            }

            using (var httpClient = new HttpClient())
            {
                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:7143/api/Items", stringContent))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }

            }

            return View(item);
        }


























        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {

                return BadRequest("Item Not Found!!!");

            }
            Item item = new Item();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7143/api/Items/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        item = JsonConvert.DeserializeObject<Item>(content);


                    }
                }

            }
            return View(item);
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Item item, IFormFile? Image)
        {

            if (!ModelState.IsValid)
            {
                return View(item);
            }

            if (Image != null && Image.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await Image.CopyToAsync(memoryStream);
                    item.Image = memoryStream.ToArray();
                }
            }

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PutAsync("https://localhost:7143/api/Items/" + item.Id, stringContent))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }

            return View(item);
        }








        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            // إعداد HttpClient للتواصل مع الـAPI
            using (var httpClient = new HttpClient())
            {
                // إرسال طلب الحذف إلى API
                var response = await httpClient.DeleteAsync($"https://localhost:7143/api/Items/{id}");

                if (response.IsSuccessStatusCode)
                {
                    // إعادة التوجيه إلى Index بعد الحذف بنجاح
                    return RedirectToAction("Index");
                }
                else
                {
                    // التعامل مع الفشل في الحذف
                    ModelState.AddModelError("", "Failed to delete item.");
                }
            }

            return RedirectToAction("Index");
        }
    }
}
