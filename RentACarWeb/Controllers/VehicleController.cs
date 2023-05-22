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
        public async Task<IActionResult> Create(VehicleViewModel vehicleViewModel, List<IFormFile> files)
        {
            var client = httpClientFactory.CreateClient();
            var token = HttpContext.Session.GetString("JWTtoken");

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            var userResponse = await client.GetAsync("https://localhost:7218/api/Auth/GetUserId");
            var userId = await userResponse.Content.ReadAsStringAsync();

            vehicleViewModel.PictureViewModels = new List<PictureViewModel>();
            AddPictures(vehicleViewModel, files);

            var jsonData = JsonConvert.SerializeObject(vehicleViewModel);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"https://localhost:7218/api/Vehicle/AddVehicle?userId={userId}", content);
            var respnseBody = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<ManagerResponse>(respnseBody);

            //var response = client.PostAsJsonAsync<VehicleViewModel>("https://localhost:7218/api/Vehicle/AddVehicle?userId=469c085b-f2c5-4ff0-9772-d9d0e830bc72", vehicleViewModel);

            return View();
        }

        private async void AddPictures(VehicleViewModel vehicleViewModel, List<IFormFile> files)
        {
            foreach (var file in files)
            {
                if (file != null || file.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream);

                        if (memoryStream.Length < 2097152)
                        {
                            var picture = new PictureViewModel
                            {
                                ImageData = memoryStream.ToArray()
                            };
                            vehicleViewModel.PictureViewModels.Add(picture);
                        }
                    }
                }
            }
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
