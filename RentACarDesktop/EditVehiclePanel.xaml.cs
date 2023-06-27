using RentACarShared;
using System.Windows;

namespace RentACarDesktop
{
    /// <summary>
    /// Interaction logic for EditVehiclePanel.xaml
    /// </summary>
    public partial class EditVehiclePanel : Window
    {
        public EditVehiclePanel(VehicleViewModel vehicleToEdit)
        {
            InitializeComponent();
            LoadVehicle(vehicleToEdit);
        }

        private void LoadVehicle(VehicleViewModel vehicleToEdit)
        {
            tbTitle.Text = vehicleToEdit.Title;
        }
    }
}
