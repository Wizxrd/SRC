using SRCClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SRCClient.Views
{
    public partial class SaveProfileAsWindow : Window
    {
        MainWindow? mainWindow;
        public SaveProfileAsWindow(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            InitializeComponent();
            InitializeEvents();
            ProfileNameTextBox.Focus();
        }

        // Initializers
        private void InitializeEvents()
        {
            ProfileNameTextBox.TextChanged += ProfileNameTextBoxTextChanged;
            ProfileNameTextBox.KeyDown += ProfileNameTextBoxKeyDown;
        }

        // Events
        private void ProfileNameTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            if (ProfileNameTextBox.Text != string.Empty)
            {
                SaveProfileButton.IsEnabled = true;
            }
            else
            {
                SaveProfileButton.IsEnabled = false;
            }
        }

        private void ProfileNameTextBoxKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SaveProfileAs();
            }
        }

        // Button Handlers
        private void BorderMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private void CloseButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveProfileButtonClick(object sender, RoutedEventArgs e)
        {
            SaveProfileAs();
        }

        // Core
        private void SaveProfileAs()
        {
            string profileName = ProfileNameTextBox.Text;
            if (mainWindow != null)
            {
                Profile.SaveAs(profileName, mainWindow);
                this.Close();
            }
        }
    }
}
