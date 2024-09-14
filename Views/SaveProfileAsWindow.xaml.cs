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
        Window? mainWindow;
        public SaveProfileAsWindow(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            InitializeComponent();
            InitializeEvents();
        }

        private void InitializeEvents()
        {
            ProfileNameTextBox.TextChanged += ProfileNameTextBoxTextChanged;
        }

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

        private void SaveProfileAsMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private void CloseSaveProfileAsButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveProfieButtonClick(object sender, RoutedEventArgs e)
        {
            string profileName = ProfileNameTextBox.Text;
            if (mainWindow != null)
            {
                Profile.Save(profileName, mainWindow);
                this.Close();
            }
        }
    }
}
