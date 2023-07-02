using System.Windows;

namespace RentACarDesktop
{
    /// <summary>
    /// Interaction logic for MainPanel.xaml
    /// </summary>
    public partial class MainPanel : Window
    {
        public MainPanel()
        {
            InitializeComponent();
        }

        private void btnVehicles_Click(object sender, RoutedEventArgs e)
        {
            VehiclesPanel vehiclesPanel = new();
            this.Hide();
            vehiclesPanel.Show();
        }

        private void btnUsers_Click(object sender, RoutedEventArgs e)
        {
            UsersPanel usersPanel = new();
            this.Hide();
            usersPanel.Show();
        }
    }
}
