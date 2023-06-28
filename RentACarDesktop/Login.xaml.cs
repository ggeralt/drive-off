using Newtonsoft.Json;
using RentACarShared;
using System.Net.Http;
using System.Text;
using System.Windows;

namespace RentACarDesktop
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private LoginViewModel? InitializeModel()
        {
            LoginViewModel loginViewModel = new LoginViewModel();
            loginViewModel.Email = txtEmail.Text.ToString();
            loginViewModel.Password = txtPassword.Password.ToString();

            return loginViewModel;
        }

        private async void btnLogin_ClickAsync(object sender, RoutedEventArgs e)
        {
            var client = new HttpClient();
            var jsonData = JsonConvert.SerializeObject(InitializeModel());
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:7218/api/Auth/Login", content);
            var respnseBody = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<ManagerResponse>(respnseBody);

            if (responseObject.IsSuccess)
            {
                Application.Current.Properties["token"] = responseObject.Message;
            }

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                MainPanel mainPanel = new();
                this.Hide();
                mainPanel.Show();
            }
            else
            {
                txtStatus.Content = "Login credentials are not valid!";
            }
        }
    }
}
