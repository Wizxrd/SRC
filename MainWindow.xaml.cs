using Newtonsoft.Json.Linq;
using SRCClient.Models;
using System.Windows.Forms;
using System.Windows;
using System.Windows.Input;
using System.IO;
using SRCClient.Views;
using System.Runtime.CompilerServices;

namespace SRCClient
{
    public partial class MainWindow : Window
    {
        private string VERSION = "0.0.3";
        public string LastUsedProfile = string.Empty;
        public string CurrentProfile = string.Empty;
        public string CurrentCursor = string.Empty;
        public JObject? config;
        public MainWindow()
        {
            LoadFile.DEBUG = true;
            InitializeComponent();
            Logger.Wipe();
            Logger.Info("SRCClient", $"Loading v{VERSION}");
            Config.Load(this);
            InitializeEvents();
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
                if (e.Key == Key.M)
                {
                    MenuPopup.IsOpen = !MenuPopup.IsOpen;
                }
                else if (e.Key == Key.F12)
                {
                    // Connect
                }
                else if (e.Key == Key.S)
                {
                    OpenConfirmSaveProfileWindow();
                }
                else if (e.Key == Key.L)
                {
                    OpenLoadProfileWindow();
                }
                else if (e.Key == Key.N)
                {
                    OpenNewProfileWindow();
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

        public void SetMainWindowGridMargin(int thickness)
        {
            MainWindowGrid.Margin = new Thickness(thickness);
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

        private void NewProfileButtonClick(object sender, RoutedEventArgs e)
        {
            OpenNewProfileWindow();
        }

        private void SaveProfileButtonClick(object sender, RoutedEventArgs e)
        {
            OpenConfirmSaveProfileWindow();
        }
        private void SaveProfileAsButtonClick(object sender, RoutedEventArgs e)
        {
            OpenSaveProfileAsWindow();
        }

        private void LoadProfileButtonClick(Object sender, RoutedEventArgs e)
        {
            OpenLoadProfileWindow();
        }

        // Core

        private void OpenConfirmSaveProfileWindow()
        {
            if (CurrentProfile != string.Empty)
            {
                ConfirmSaveProfileWindow confirmSaveProfileWindow = new ConfirmSaveProfileWindow()
                {
                    Owner = this,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner
                };
                confirmSaveProfileWindow.SetMessageTextBlock("Save Profile:", CurrentProfile);
                confirmSaveProfileWindow.ShowDialog();
                if (confirmSaveProfileWindow.SaveConfirmed)
                {
                    Profile.Save(CurrentProfile, this);
                }
            }
            else
            {
                Sound.Play("Error");
                Logger.Warning("MainWindow.SaveProfile", "No Profile Loaded!");
            }
        }

        private void OpenNewProfileWindow()
        {
            NewProfileWindow newProfileWindow = new NewProfileWindow(this)
            {
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            MenuPopup.IsOpen = !MenuPopup.IsOpen;
            newProfileWindow.ShowDialog();
        }

        private void OpenSaveProfileAsWindow()
        {
            if (CurrentProfile != string.Empty)
            {
                SaveProfileAsWindow saveProfileAsWindow = new SaveProfileAsWindow(this)
                {
                    Owner = this,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner
                };
                MenuPopup.IsOpen = !MenuPopup.IsOpen;
                saveProfileAsWindow.ShowDialog();
            }
            else
            {
                Sound.Play("Error");
                Logger.Warning("MainWindow.OpenSaveProfileAsWindow", "No Profile Loaded!");
            }
        }

        private void OpenLoadProfileWindow()
        {
            LoadProfileWindow loadProfileWindow = new LoadProfileWindow(this)
            {
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            MenuPopup.IsOpen = !MenuPopup.IsOpen;
            loadProfileWindow.ShowDialog();
        }
    }
}
