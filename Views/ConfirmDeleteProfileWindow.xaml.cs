using System.Windows;
using System.Windows.Input;

namespace SRCClient.Views
{
    public partial class ConfirmDeleteProfileWindow : Window
    {

        public bool DeletionConfirmed { get; private set; }

        public ConfirmDeleteProfileWindow(string profileName)
        {
            InitializeComponent();
            MessageTextBlock.Text += $" \"{profileName}\"";
            MessageTextBlock.Measure(new System.Windows.Size(double.PositiveInfinity, double.PositiveInfinity));
            this.Width = MessageTextBlock.DesiredSize.Width + 10;
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

        private void YesButtonClick(object sender, RoutedEventArgs e)
        {
            DeletionConfirmed = true;
            this.Close();
        }
        
        private void NoButtonClick(object sender, RoutedEventArgs e)
        {
            DeletionConfirmed = false;
            this.Close();
        }
    }
}
