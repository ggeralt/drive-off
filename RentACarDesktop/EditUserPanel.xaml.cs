using Newtonsoft.Json;
using RentACarDesktop.Model;
using System.Net.Http;
using System.Text;
using System.Windows;

namespace RentACarDesktop
{
    /// <summary>
    /// Interaction logic for EditUserPanel.xaml
    /// </summary>
    public partial class EditUserPanel : Window
    {
        private UserModel _userToEdit = new();

        public EditUserPanel(UserModel userToEdit)
        {
            InitializeComponent();
            _userToEdit = userToEdit;
            LoadUser(_userToEdit);
        }

        private void LoadUser(UserModel userToEdit)
        {
            tbId.Text = userToEdit.Id;
            tbUserName.Text = userToEdit.Username;
            tbEmail.Text = userToEdit.Email;
            tbEmailConfirmed.Text = userToEdit.EmailConfirmed.ToString();
            tbHasDrivingLicence.Text = userToEdit.HasDrivingLicence.ToString();
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var client = new HttpClient();
            string token = Application.Current.Properties["token"].ToString();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var response = await client.DeleteAsync($"https://localhost:7218/api/Admin/DeleteAccount?id={_userToEdit.Id}");
            this.Close();
        }

        private async void btnConfirmEmail_Click(object sender, RoutedEventArgs e)
        {
            _userToEdit.EmailConfirmed = true;

            var client = new HttpClient();
            string token = Application.Current.Properties["token"].ToString();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var jsonData = JsonConvert.SerializeObject(_userToEdit);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"https://localhost:7218/api/Auth/ConfirmEmail?userId={_userToEdit.Id}" + $"&token={token}", content);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                this.Close();
            }
            else
            {
                tbEmailConfirmed.Text = response.StatusCode.ToString();
            }
        }
    }
}
