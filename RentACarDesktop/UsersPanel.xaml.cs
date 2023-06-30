using Newtonsoft.Json;
using RentACarShared;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows;
using System.Windows.Input;

namespace RentACarDesktop
{
    /// <summary>
    /// Interaction logic for UsersPanel.xaml
    /// </summary>
    public partial class UsersPanel : Window
    {
        public UsersPanel()
        {
            InitializeComponent();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            GetAllUsers();
        }

        private async void GetAllUsers()
        {
            var client = new HttpClient();
            string token = Application.Current.Properties["token"].ToString();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var response = await client.GetAsync("https://localhost:7218/api/Admin/GetAllUsers");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                List<UserViewModel>? users = JsonConvert.DeserializeObject<List<UserViewModel>>(json);
                lbUsers.ItemsSource = users;
            }
        }

        private void MouseDoubleClickToUpdateSelectedUser(object sender, MouseButtonEventArgs e)
        {
            UserViewModel selectedUser = (UserViewModel)lbUsers.SelectedItem;

            if (selectedUser != null)
            {
                EditUserPanel editUserPanel = new(selectedUser);
                editUserPanel.Show();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainPanel mainPanel = new MainPanel();
            mainPanel.Show();
        }
    }
}
