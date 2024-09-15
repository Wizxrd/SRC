using Newtonsoft.Json.Linq;
using SRCClient.Models;
using System.Windows.Forms;
using System.Windows;
using System.Windows.Input;
using System.IO;
using SRCClient.Views;

namespace SRCClient
{
    public partial class MainWindow : Window
    {
        private string VERSION = "0.0.2";
        public JObject? config;
        public MainWindow()
        {
            LoadFile.DEBUG = true;
            InitializeComponent();
            Logger.Wipe();
            Logger.Debug("SRCClient", $"Loading v{VERSION}");
            InitializeConfig();
            InitializeCursor();
            InitializeEvents();
        }

        // Initializers
        private void InitializeConfig()
        {
            try
            {
                config = JObject.Parse(File.ReadAllText(LoadFile.Load("Config", "Config.json")));
                this.Left = (int?)config["Window"]?["Location"]?["Left"] ?? 0;
                this.Top = (int?)config["Window"]?["Location"]?["Top"] ?? 0;
                this.Width = (int?)config["Window"]?["Size"]?["Width"] ?? 1280;
                this.Height = (int?)config["Window"]?["Size"]?["Height"] ?? 720;
            }
            catch (Exception ex)
            {
                Logger.Error("MainWindow.LoadConfig", ex.ToString());
            }
        }

        private void InitializeCursor()
        {
            string cursor = config["Cursor"]?.ToString() ?? string.Empty;
            if (cursor != string.Empty)
            {
                this.Cursor = new System.Windows.Input.Cursor(System.Windows.Application.GetResourceStream(new Uri($"pack://application:,,,/Cursors/{cursor}", UriKind.Absolute)).Stream);
            }
            else
            {
                Logger.Error("MainWindow.SetCursor", "Config.Cursor is null!");
            }
        }

        private void InitializeEvents()
        {
            this.KeyDown += MainWindowKeyDown;
        }

        // Events
        private void MainWindowKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.LeftShift))
            {
                if (e.Key == Key.S)
                {
                    OpenSaveProfileAsWindow();
                }
            }
            else if (Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                if (e.Key == Key.L)
                {
                    OpenLoadProfileWindow();
                }
            }
        }

        // Button Handlers
        private void MenuButtonClick(object sender, RoutedEventArgs e)
        {
            MenuPopup.IsOpen = !MenuPopup.IsOpen;
        }

        private void BorderMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private void MinimizeButtonClick(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void MaximizeButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.WindowState == WindowState.Maximized)
                {
                    MainWindowGrid.Margin = new Thickness(0);
                    MaximizeButton.Tag = "pack://application:,,,/Images/Maximize.png";
                    this.WindowState = WindowState.Normal;
                }
                else
                {
                    MainWindowGrid.Margin = new Thickness(5);
                    MaximizeButton.Tag = "pack://application:,,,/Images/MaximizeMinimize.png";
                    this.WindowState = WindowState.Maximized;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("MainWindow.MaximizeButtonClick", ex.ToString());
            }
        }

        private void CloseButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveProfileButtonClick(object sender, RoutedEventArgs e)
        {

        }
        private void SaveProfileAsButtonClick(object sender, RoutedEventArgs e)
        {
            MenuPopup.IsOpen = !MenuPopup.IsOpen;
            OpenSaveProfileAsWindow();
        }

        private void LoadProfileButtonClick(Object sender, RoutedEventArgs e)
        {
            MenuPopup.IsOpen = !MenuPopup.IsOpen;
            OpenLoadProfileWindow();
        }

        // Core Functions
        private void OpenSaveProfileAsWindow()
        {
            SaveProfileAsWindow saveProfileAsWindow = new SaveProfileAsWindow(this);
            //await Task.Delay(1);
            saveProfileAsWindow.Loaded += (s, ev) =>
            {
                saveProfileAsWindow.Top = this.Top + (this.ActualHeight - saveProfileAsWindow.ActualHeight) / 2;
                saveProfileAsWindow.Left = this.Left + (this.ActualWidth - saveProfileAsWindow.ActualWidth) / 2;
            };
            saveProfileAsWindow.ShowDialog();
        }

        private void OpenLoadProfileWindow()
        {
            LoadProfileWindow loadProfileWindow = new LoadProfileWindow();
            loadProfileWindow.Loaded += (s, ev) =>
            {
                loadProfileWindow.Top = this.Top + (this.ActualHeight - loadProfileWindow.ActualHeight) / 2;
                loadProfileWindow.Left = this.Left + (this.ActualWidth - loadProfileWindow.ActualWidth) / 2;
            };
            loadProfileWindow.ShowDialog();
        }
    }
}
