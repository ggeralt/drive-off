using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RentACarShared;
using System.Text;

namespace RentACarWeb.Controllers
{
    public class VehicleController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public VehicleController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        // GET: VehicleController
        public ActionResult Index()
        {
            return View();
        }

        // GET: VehicleController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: VehicleController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VehicleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleViewModel vehicleViewModel)
        {
            var client = httpClientFactory.CreateClient();

            var jsonData = JsonConvert.SerializeObject(vehicleViewModel);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:7218/api/Vehicle/AddVehicle?userId=469c085b-f2c5-4ff0-9772-d9d0e830bc72", content);
            var respnseBody = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<ManagerResponse>(respnseBody);

            return View();
        }

        // GET: VehicleController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: VehicleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: VehicleController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: VehicleController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
