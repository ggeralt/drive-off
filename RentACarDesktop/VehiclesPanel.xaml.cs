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
    public partial class VehiclesPanel : Window
    {
        public VehiclesPanel()
        {
            InitializeComponent();
        }

        private void Window_Activated(object sender, System.EventArgs e)
        {
            GetVehicles();
        }

        private async void GetVehicles()
        {
            var client = new HttpClient();
            string token = Application.Current.Properties["token"].ToString();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var response = await client.GetAsync("https://localhost:7218/api/Admin/GetAllNonConfirmedVehicles");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                List<VehicleViewModel>? vehicles = JsonConvert.DeserializeObject<List<VehicleViewModel>>(json);
                lbVehicles.ItemsSource = vehicles;
            }
        }

        private void MouseDoubleClickToUpdateSelectedVehicle(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            VehicleViewModel selectedVehicle = (VehicleViewModel)lbVehicles.SelectedItem;
            
            if (selectedVehicle != null)
            {
                EditVehiclePanel editVehiclePanel = new(selectedVehicle);
                editVehiclePanel.Show();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainPanel mainPanel = new MainPanel();
            mainPanel.Show();
        }
    }
}
