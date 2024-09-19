using System.Windows;
using System.Windows.Input;

namespace SRCClient.Views
{
    public partial class ConfirmSaveProfileWindow : Window
    {
        public bool SaveConfirmed { get; private set; }
        public ConfirmSaveProfileWindow()
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

        public void SetMessageTextBlock(string message, string profileName)
        {
            MessageTextBlock.Text = $"{message} \"{profileName}\"?";
            MessageTextBlock.Measure(new System.Windows.Size(double.PositiveInfinity, double.PositiveInfinity));
            if (MessageTextBlock.DesiredSize.Width > this.MinWidth)
            {
                this.Width = MessageTextBlock.DesiredSize.Width;
            }
        }

        private void CloseButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void YesButtonClick(object sender, RoutedEventArgs e)
        {
            SaveConfirmed = true;
            this.Close();
        }

        private void NoButtonClick(object sender, RoutedEventArgs e)
        {
            SaveConfirmed = false;
            this.Close();
        }
    }
}
