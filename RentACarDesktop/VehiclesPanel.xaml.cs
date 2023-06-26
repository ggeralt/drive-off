using Newtonsoft.Json;
using RentACarShared;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows;

namespace RentACarDesktop
{
    /// <summary>
    /// Interaction logic for Panel.xaml
    /// </summary>
    public partial class VehiclesPanel : Window
    {
        public VehiclesPanel()
        {
            InitializeComponent();
            GetVehicles();
        }

        private async void GetVehicles()
        {
            var client = new HttpClient();
            var response = await client.GetAsync("https://localhost:7218/api/Vehicle/GetAllVehicles");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                List<VehicleViewModel>? vehicles = JsonConvert.DeserializeObject<List<VehicleViewModel>>(json);
                lbVehicles.ItemsSource = vehicles;
            }
        }

        /*private async void MouseDoubleClickToUpdateSelectedVehicle(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            VehicleViewModel vehicle = (VehicleViewModel)lbVehicles.SelectedItem;
            vehicle.HasCertificate = true;

            var client = new HttpClient();
            var jsonData = JsonConvert.SerializeObject(vehicle);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:7218/api/Vehicle/UpdateVehicle", content);

            GetVehicles();
        }*/

        private void MouseDoubleClickToUpdateSelectedVehicle(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            VehicleViewModel selectedVehicle = (VehicleViewModel)lbVehicles.SelectedItem;
            EditVehiclePanel editVehiclePanel = new(selectedVehicle);
            this.Hide();
            editVehiclePanel.Show();
        }
    }
}
