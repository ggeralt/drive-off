﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RentACarShared;
using System.Text;

namespace RentACarWeb.Controllers
{
    public class UserController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;
        public UserController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public IActionResult Register()
        {
            return View();
        }
        public async Task<IActionResult> RegisterDetails(RegisterViewModel model)
        {
            var client = httpClientFactory.CreateClient();

            var jsonData = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:7218/api/Auth/Register", content);
            var respnseBody = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<ManagerResponse>(respnseBody);

            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> LoginUser(LoginViewModel model)
        {
            var client = httpClientFactory.CreateClient();

            var jsonData = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:7218/api/Auth/Login", content);
            var respnseBody = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<ManagerResponse>(respnseBody);

            if (responseObject.IsSuccess)
            {
                HttpContext.Session.SetString("JWTtoken", responseObject.Message);
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Login");
        }

        public IActionResult ForgetPassword()
        {
            return View();
        }
        public async Task<IActionResult> UserForgetPassword(string email)
        {
            var client = httpClientFactory.CreateClient();

            //var jsonData = JsonConvert.SerializeObject(email);
            //var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"https://localhost:7218/api/Auth/ForgetPassword?email={email}", new StringContent(email));
            var respnseBody = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<ManagerResponse>(respnseBody);

            if (!responseObject.IsSuccess)
            {
                return RedirectToAction("ForgetPassword");
            }
            return RedirectToAction("Login");
        }
    }
}
