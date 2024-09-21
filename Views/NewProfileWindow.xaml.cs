using SRCClient.Models;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace SRCClient.Views
{
    public partial class NewProfileWindow : Window
    {
        private MainWindow? mainWindow;
        public NewProfileWindow(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            InitializeComponent();
        }
        private void BorderMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private void TextBoxChanged(object sender, EventArgs e)
        {
            try
            {
                if (ServerIPPortTextBox.Text == string.Empty || PasswordTextBox.Text == string.Empty || CallsignTextBox.Text == string.Empty && ProfileNameTextBox.Text == string.Empty)
                {
                    CreateProfileButton.IsEnabled = false;
                    return;
                }
                CreateProfileButton.IsEnabled = true;
            }
            catch (Exception ex)
            {
                Logger.Error("NewProfileWindow.TextBoxesTextChanged", ex.ToString());
            }
        }

        private void CreateProfileButtonClick(object sender, RoutedEventArgs e)
        {
            string regex = @"^(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}):(\d{1,5})$";
            Match match = Regex.Match(ServerIPPortTextBox.Text, regex);
            if (match.Success)
            {
                string serverIP = match.Groups[1].Value;
                string port = match.Groups[2].Value;
                if (mainWindow != null)
                {
                    Profile.New(ProfileNameTextBox.Text, serverIP, port, PasswordTextBox.Text, CallsignTextBox.Text, mainWindow);
                }
            }
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CloseButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
