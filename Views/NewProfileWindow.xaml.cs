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

        private void CloseButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
