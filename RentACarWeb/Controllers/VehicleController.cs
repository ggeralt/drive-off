using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RentACarShared;
using RentACarWeb.Models;
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
        public async Task<IActionResult> Index()
        {
            var client = httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7218/api/Vehicle/GetAllVehicles");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var vehicleViewModels = JsonConvert.DeserializeObject<List<VehicleViewModel>>(json);

                return View(vehicleViewModels);
            }
            return NotFound();
        }

        public async Task<IActionResult> IndexSearch(string search)
        {
            var client = httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7218/api/Vehicle/GetSearchedVehicles?searchValue={search}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var vehicleViewModels = JsonConvert.DeserializeObject<List<VehicleViewModel>>(json);

                return View("Index", vehicleViewModels);
            }

            return NotFound();
        }

        // GET: VehicleController/Details/5
        public async Task<IActionResult> Details(int vehicleId)
        {
            var client = httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7218/api/Vehicle/{vehicleId}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var vehicleVewModel = JsonConvert.DeserializeObject<VehicleViewModel>(json);

                if (vehicleVewModel != null)
                {
                    return View(vehicleVewModel);
                }
            }
            return NotFound();

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

            vehicleViewModel.PictureViewModels = new List<PictureViewModel>();
            AddPictures(vehicleViewModel, files);

            var jsonData = JsonConvert.SerializeObject(vehicleViewModel);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"https://localhost:7218/api/Vehicle/AddVehicle", content);
            var respnseBody = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<ManagerResponse>(respnseBody);

            return View();
        }

        // GET: VehicleController/Edit/5
        public async Task<IActionResult> Edit(int vehicleId)
        {
            var client = httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7218/api/Vehicle/{vehicleId}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var vehicleVewModel = JsonConvert.DeserializeObject<VehicleViewModel>(json);

                if (vehicleVewModel != null)
                {
                    return View(vehicleVewModel);
                }
            }
            return NotFound();
        }

        // POST: VehicleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(VehicleViewModel vehicleViewModel, List<IFormFile> files)
        {
            var client = httpClientFactory.CreateClient();
            var token = HttpContext.Session.GetString("JWTtoken");

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            vehicleViewModel.PictureViewModels = new List<PictureViewModel>();
            AddPictures(vehicleViewModel, files);

            var jsonData = JsonConvert.SerializeObject(vehicleViewModel);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"https://localhost:7218/api/Vehicle/UpdateVehicle", content);
            var respnseBody = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<ManagerResponse>(respnseBody);

            return RedirectToAction("MyAccount");
        }

        // GET: VehicleController/Delete/5
        public async Task<IActionResult> Delete(int vehicleId)
        {
            var client = httpClientFactory.CreateClient();
            var token = HttpContext.Session.GetString("JWTtoken");

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            var response = await client.DeleteAsync($"https://localhost:7218/api/Vehicle/DeleteVehicle?vehicleId={vehicleId}");

            if (response.IsSuccessStatusCode)
            {
                return Redirect(Request.Headers["Referer"].ToString());
            }
            return NotFound();
        }

        //POST: VehicleController/Delete/5
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

        public async Task<IActionResult> DeletePicture(int pictureId)
        {
            var client = httpClientFactory.CreateClient();
            var token = HttpContext.Session.GetString("JWTtoken");

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            var response = await client.DeleteAsync($"https://localhost:7218/api/File/Delete?pictureId={pictureId}");

            if (response.IsSuccessStatusCode)
            {
                return Redirect(Request.Headers["Referer"].ToString());
            }
            return NotFound();
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
        public ActionResult Booking(int  vehicleId, string message)
        {
            ViewBag.vehicleId = vehicleId;
            ViewBag.message = message;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Booking(ReservationViewModel reservationViewModel)
        {
            var client = httpClientFactory.CreateClient();
            var token = HttpContext.Session.GetString("JWTtoken");

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            var jsonData = JsonConvert.SerializeObject(reservationViewModel);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"https://localhost:7218/api/Reservation1/AddReservation", content);
            var respnseBody = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<ManagerResponse>(respnseBody);

            if (response.ReasonPhrase == "Unauthorized")
            {
                return RedirectToAction("Booking", new { vehicleId = reservationViewModel.VehicleId, message = "Unauthorized user, login and try again" });
            }

            return RedirectToAction("Booking", new { vehicleId = reservationViewModel.VehicleId, message = responseObject.Message });
        }
        public async Task<IActionResult> MyAccount()
        {
            var client = httpClientFactory.CreateClient();
            var token = HttpContext.Session.GetString("JWTtoken");

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            var response = await client.GetAsync("https://localhost:7218/api/Auth/GetVehicleUser");
            var userResponse = await client.GetAsync("https://localhost:7218/api/Auth/GetUser");

            if (response.IsSuccessStatusCode && userResponse.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var vehicleViewModels = JsonConvert.DeserializeObject<List<VehicleViewModel>>(json);

                var userJson = await userResponse.Content.ReadAsStringAsync();
                var userViewModel = JsonConvert.DeserializeObject<UserViewModel>(userJson);

                ViewBag.User = userViewModel;

                return View(vehicleViewModels);
            }
            return NotFound("Account not found, login and try again");
        }
        public async Task<IActionResult> Reservation()
        {
            var client = httpClientFactory.CreateClient();
            var token = HttpContext.Session.GetString("JWTtoken");

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            var response = await client.GetAsync("https://localhost:7218/api/Reservation1/GetAllReservations");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var reservationViewModels = JsonConvert.DeserializeObject<List<ReservationView>>(json);

                return View(reservationViewModels);
            }
            return NotFound();
        }
        public async Task<IActionResult> DeleteReservation(int reservationId)
        {
            var client = httpClientFactory.CreateClient();
            var token = HttpContext.Session.GetString("JWTtoken");

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            var response = await client.DeleteAsync($"https://localhost:7218/api/Reservation1/DeleteReservation?reservationId={reservationId}");

            if (response.IsSuccessStatusCode)
            {
                return Redirect(Request.Headers["Referer"].ToString());
            }
            return NotFound();
        }
        public async Task<IActionResult> Reviews(int vehicleId)
        {
            var client = httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7218/api/Vehicle/GetReviews?vehicleId={vehicleId}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var reviewViewModels = JsonConvert.DeserializeObject<List<ReviewViewModel>>(json);

                return View(reviewViewModels);
            }
            return NotFound();
        }
        public ActionResult CreateReviews(int vehicleId, string message)
        {
            ViewBag.vehicleId = vehicleId;
            ViewBag.message = message;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateReviews(ReviewViewModel reviewViewModel)
        {
            var client = httpClientFactory.CreateClient();

            var token = HttpContext.Session.GetString("JWTtoken");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            var jsonData = JsonConvert.SerializeObject(reviewViewModel);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"https://localhost:7218/api/Vehicle/CreateReview", content);

            var respnseBody = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<ManagerResponse>(respnseBody);

            if (response.ReasonPhrase == "Unauthorized")
            {
                return RedirectToAction("CreateReviews", new { vehicleId = reviewViewModel.VehicleId, message = "Unauthorized user, login and try again" });
            }
            else if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("CreateReviews", new { vehicleId = reviewViewModel.VehicleId, message = responseObject.Message });
            }
            
            return Redirect(Request.Headers["Referer"].ToString());
        }
        
        public async Task<IActionResult> DeleteReview(int reviewId)
        {
            var client = httpClientFactory.CreateClient();

            var token = HttpContext.Session.GetString("JWTtoken");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            var response = await client.DeleteAsync($"https://localhost:7218/api/Vehicle/DeleteReview?reviewId={reviewId}");

            if (response.IsSuccessStatusCode)
            {
                return Redirect(Request.Headers["Referer"].ToString());
            }
            return NotFound();
        }
    }
}