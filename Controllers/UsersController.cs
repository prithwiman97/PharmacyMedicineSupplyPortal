using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PharmacyMedicineSupplyPortal.Models;

namespace PharmacyMedicineSupplyPortal.Controllers
{
    public class UsersController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:44370/api/Auth");
        HttpClient client;
        public UsersController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Authenticate(User user)
        {
            string data = JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress,content).Result;
            if(response.IsSuccessStatusCode)
            {
                string token = response.Content.ReadAsStringAsync().Result;
                return View("Home", token);
            }
            ViewBag.Message = "Invalid email or password";
            return View("Login");
        }


        public IActionResult Home(string token)
        {
            return View(token);
        }
    }
}
