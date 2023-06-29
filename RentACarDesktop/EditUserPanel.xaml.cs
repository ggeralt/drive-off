using RentACarShared;
using System;
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
            LoadVehicle(_userToEdit);
        }

        private void LoadVehicle(UserViewModel userToEdit)
        {
            throw new NotImplementedException();
        }
    }
}
