using System.Windows;
using System.Windows.Input;

namespace SRCClient.Views
{
    public partial class NewProfileWindow : Window
    {
        public NewProfileWindow()
        {
            InitializeComponent();
        }
        private void BorderMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private void TextBoxesTextChanged(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null) return;
            if (textBox.Name == "ServerIPPortTextBox")
            {
                if (textBox.Text == string.Empty)
                {
                    CreateProfileButton.IsEnabled = false;
                    return;
                }
            }
            else if (textBox.Name == "PasswordTextBox")
            {
                if (textBox.Text == string.Empty)
                {
                    CreateProfileButton.IsEnabled = false;
                    return;
                }
            }
            else if (textBox.Name == "CallsignTextBox")
            {
                if (textBox.Text == string.Empty)
                {
                    CreateProfileButton.IsEnabled = false;
                    return;
                }
            }
            else if (textBox.Name == "ProfileNameTextBox")
            {
                if (textBox.Text == string.Empty)
                {
                    CreateProfileButton.IsEnabled = false;
                    return;
                }
            }
            System.Windows.MessageBox.Show("HI");
            CreateProfileButton.IsEnabled = true;
        }

        private void CreateProfileButtonClick(object sender, RoutedEventArgs e)
        {

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
