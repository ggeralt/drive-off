using Newtonsoft.Json;
using RentACarShared;
using System.Net.Http;
using System.Text;
using System.Windows;

namespace RentACarDesktop
{
    /// <summary>
    /// Interaction logic for EditVehiclePanel.xaml
    /// </summary>
    public partial class EditVehiclePanel : Window
    {
        private VehicleViewModel _vehicleToEdit = new();

        public EditVehiclePanel(VehicleViewModel vehicleToEdit)
        {
            InitializeComponent();
            _vehicleToEdit = vehicleToEdit;
            LoadVehicle(_vehicleToEdit);
        }

        private void LoadVehicle(VehicleViewModel vehicleToEdit)
        {
            tbTitle.Text = vehicleToEdit.Title;
            tbCertificate.Text = vehicleToEdit.HasCertificate.ToString();
            tbModel.Text = vehicleToEdit.Model;
            tbBrand.Text = vehicleToEdit.Brand;
            tbType.Text = vehicleToEdit.Type;
            tbDescription.Text = vehicleToEdit.Description;
            tbLocation.Text = vehicleToEdit.Location;
            tbLatitude.Text = vehicleToEdit.Latitude.ToString();
            tbLongitude.Text = vehicleToEdit.Longitude.ToString();
            tbPrice.Text = vehicleToEdit.Price.ToString();
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var client = new HttpClient();
            string token = Application.Current.Properties["token"].ToString();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var response = await client.DeleteAsync($"https://localhost:7218/api/Admin/DeleteVehicle?vehicleId={_vehicleToEdit.Id}");
            this.Close();
        }

        private async void btnAddCertificate_Click(object sender, RoutedEventArgs e)
        {
            _vehicleToEdit.HasCertificate = true;

            var client = new HttpClient();
            string token = Application.Current.Properties["token"].ToString();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var jsonData = JsonConvert.SerializeObject(_vehicleToEdit);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"https://localhost:7218/api/Admin/Confirm?vehicleId={_vehicleToEdit.Id}", content);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                tbCertificate.Text = "True";
                this.Close();
            }
        }
    }
}
