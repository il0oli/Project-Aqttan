using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using ClientAqttan.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClientAqttan.Controllers
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
      

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Compo compo, IFormFile? Image)
        {
            if (!ModelState.IsValid)
            {
                return View(compo);
            }

            if (Image != null && Image.Length > 0)
            {

                using (var memoryStream = new MemoryStream())

                {

                    await Image.CopyToAsync(memoryStream);
                    compo.Image = memoryStream.ToArray();
                }
            }

            using (var httpClient = new HttpClient())
            {
                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(compo), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:7143/api/Compos", stringContent))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }

            }

            return View(compo);
        }






        // GET: Compos/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {

                return BadRequest("Component Not Found!!!");

            }
            Compo compo = new Compo();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7143/api/Compos/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        compo = JsonConvert.DeserializeObject<Compo>(content);


                    }
                }

            }
            return View(compo);
        }
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Compo compo, IFormFile? Image)
        {

            if (!ModelState.IsValid)
            {
                return View(compo);
            }

            if(Image != null && Image.Length >0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await Image.CopyToAsync(memoryStream);
                    compo.Image = memoryStream.ToArray();
                }
            }
           
            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(compo), Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                using(var response = await httpClient.PutAsync("https://localhost:7143/api/Compos/" + compo.Id, stringContent))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }

            return View(compo);
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
                var response = await httpClient.DeleteAsync($"https://localhost:7143/api/Compos/{id}");

                if (response.IsSuccessStatusCode)
                {
                    // إعادة التوجيه إلى Index بعد الحذف بنجاح
                    return RedirectToAction("Index");
                }
                else
                {
                    // التعامل مع الفشل في الحذف
                    ModelState.AddModelError("", "Failed to delete component.");
                }
            }

            return RedirectToAction("Index");
        }

    }
}
