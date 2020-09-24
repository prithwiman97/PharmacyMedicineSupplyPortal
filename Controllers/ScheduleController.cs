using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PharmacyMedicineSupplyPortal.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace PharmacyMedicineSupplyPortal.Controllers
{
    public class ScheduleController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:44334/api/RepSchedule");
        HttpClient client;
        public ScheduleController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }
        public IActionResult GetSchedule(ScheduleFetch data)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",data.Token);
            HttpResponseMessage response = client.GetAsync(client.BaseAddress+"/"+data.FromDate).Result;
            if(response.IsSuccessStatusCode)
            {
                string scheduleData = response.Content.ReadAsStringAsync().Result;
                IEnumerable<RepSchedule> schedule = JsonConvert.DeserializeObject<List<RepSchedule>>(scheduleData);
                return View(schedule);
            }
            return View();
        }
    }
}
