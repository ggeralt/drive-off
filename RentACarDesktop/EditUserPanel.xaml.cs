using RentACarShared;
using System.Net.Http;
using System.Windows;

namespace RentACarDesktop
{
    /// <summary>
    /// Interaction logic for EditUserPanel.xaml
    /// </summary>
    public partial class EditUserPanel : Window
    {
        private UserViewModel _userToEdit = new();

        public EditUserPanel(UserViewModel userToEdit)
        {
            InitializeComponent();
            _userToEdit = userToEdit;
            LoadUser(userToEdit);
        }

        private void LoadUser(UserViewModel userToEdit)
        {
            tbId.Text = userToEdit.Id;
            tbUserName.Text = userToEdit.Username;
            tbEmail.Text = userToEdit.Email;
            tbEmailConfirmed.Text = userToEdit.IsEmailConfirmed.ToString();
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var client = new HttpClient();
            string token = Application.Current.Properties["token"].ToString();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var response = await client.DeleteAsync($"https://localhost:7218/api/Admin/DeleteAccount?id={_userToEdit.Id}");
            this.Close();
        }
    }
}
