using Newtonsoft.Json;
using RentACarShared;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows;

namespace RentACarDesktop
{
    /// <summary>
    /// Interaction logic for Panel.xaml
    /// </summary>
    public partial class Panel : Window
    {
        public Panel()
        {
            InitializeComponent();
            GetVehicles();
        }

        private async void GetVehicles()
        {
            var client = new HttpClient();
            var response = await client.GetAsync("https://localhost:7218/api/Admin/GetAllNonConfirmedVehicles");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                List<VehicleViewModel>? vehicles = JsonConvert.DeserializeObject<List<VehicleViewModel>>(json);
                lbVehicles.Items.Add(vehicles);
            }
        }
    }
}
