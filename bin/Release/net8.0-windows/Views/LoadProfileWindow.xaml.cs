using SRCClient.Models;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace SRCClient.Views
{
    public partial class LoadProfileWindow : Window
    {
        public LoadProfileWindow()
        {
            InitializeComponent();
            InitializeEvents();
        }

        private void InitializeEvents()
        {
            this.Loaded += LoadProfileWindowLoaded;
        }

        private void LoadProfileWindowLoaded(object sender, RoutedEventArgs e)
        {
            LoadProfiles();
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

        private System.Windows.HorizontalAlignment GetHorizontalAlignment()
        {
            return HorizontalAlignment;
        }

        private System.Windows.Controls.Button CreateProfileButton(string toopTip, string imageSource, System.Windows.Controls.Button parent, RoutedEventHandler clickHandler)
        {
            System.Windows.Controls.Button button = new System.Windows.Controls.Button
            {
                Background = System.Windows.Media.Brushes.Transparent,
                Width = 32,
                Height = 32,
                Margin = new Thickness(5, 0, 0, 0),
                VerticalAlignment = VerticalAlignment.Center,
                BorderThickness = new Thickness(1),
                BorderBrush = new SolidColorBrush(System.Windows.Media.Color.FromRgb(56, 182, 255)),
                ToolTip = toopTip,
                Template = (ControlTemplate)this.Resources["ButtonTemplate"],
                Tag = parent
            };

            System.Windows.Controls.Image image = new System.Windows.Controls.Image
            {
                Width = 16,
                Height = 16,
                Source = new BitmapImage(new Uri(imageSource, UriKind.Absolute))
            };
            button.Content = image;
            button.Click += clickHandler;
            return button;
        }

        private void AddProfileButton(string profileName)
        {
            System.Windows.Controls.Button button = new System.Windows.Controls.Button
            {
                Name = profileName,
                Width = 300,
                Height = 32,
                BorderBrush = new SolidColorBrush(System.Windows.Media.Color.FromRgb(50,50,50)),
                BorderThickness = new Thickness(1),
                Background = new SolidColorBrush(Colors.Transparent),
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalContentAlignment = System.Windows.HorizontalAlignment.Stretch,
                Template = (ControlTemplate)this.Resources["ProfileTemplate"]
            };
            Grid grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            TextBlock textBlock = new TextBlock
            {
                Text = profileName,
                Foreground = System.Windows.Media.Brushes.White,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(10, 0, 0, 0)
            };
            Grid.SetColumn(textBlock, 0);
            grid.Children.Add(textBlock);
            StackPanel stackPanel = new StackPanel
            {
                Orientation = System.Windows.Controls.Orientation.Horizontal,
                VerticalAlignment = VerticalAlignment.Center,
            };
            Grid.SetColumn(stackPanel, 1);

            System.Windows.Controls.Button renameButton = CreateProfileButton("Rename Profile", "pack://application:,,,/Images/Rename.png", button, RenameButtonClick);
            System.Windows.Controls.Button copyButton = CreateProfileButton("Copy Profile", "pack://application:,,,/Images/Copy.png", button,CopyButtonClick);
            System.Windows.Controls.Button deleteButton = CreateProfileButton("Delete Profile", "pack://application:,,,/Images/Delete.png", button, DeleteButtonClick);

            stackPanel.Children.Add(renameButton);
            stackPanel.Children.Add(copyButton);
            stackPanel.Children.Add(deleteButton);
            grid.Children.Add(stackPanel);
            button.Content = grid;
            ProfileStack.Children.Add(button);
        }

        private void RenameButtonClick(object sender, RoutedEventArgs e)
        {
            var button = sender as System.Windows.Controls.Button;
            if (button == null) return;
            var parentButton = button.Tag as System.Windows.Controls.Button;
            if (parentButton == null) return;
            var grid = parentButton.Content as Grid;
            if (grid == null) return;
            var textBlock = grid.Children.OfType<TextBlock>().FirstOrDefault();
            var textBox = grid.Children.OfType<System.Windows.Controls.TextBox>().FirstOrDefault();

            if (textBlock != null)
            {
                var newTextBox = new System.Windows.Controls.TextBox
                {
                    Text = textBlock.Text,
                    Background = System.Windows.Media.Brushes.Transparent,
                    Foreground = System.Windows.Media.Brushes.White,
                    Margin = textBlock.Margin,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch
                };
                grid.Children.Remove(textBlock);
                Grid.SetColumn(newTextBox, 0);
                grid.Children.Add(newTextBox);
                newTextBox.Loaded += (s, args) =>
                {
                    newTextBox.Focus();
                    newTextBox.SelectAll();
                };
            }
            else if (textBox != null)
            {
                var newTextBlock = new TextBlock
                {
                    Text = parentButton.Name,
                    Foreground = System.Windows.Media.Brushes.White,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = textBox.Margin
                };
                grid.Children.Remove(textBox);
                Grid.SetColumn(newTextBlock, 0);
                grid.Children.Add(newTextBlock);
            }
        }

        private void CopyButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void LoadProfiles()
        {
            try
            {
                string[] files = Directory.GetFiles(LoadFile.LoadFolder("Profiles"));
                foreach (string file in files)
                {
                    if (file.Contains(".json"))
                    {
                        JObject profile = JObject.Parse(File.ReadAllText(LoadFile.Load("Profiles", file)));
                        string profileName = profile["Name"]?.ToString() ?? string.Empty;
                        if (profileName != string.Empty)
                        {
                            AddProfileButton(profileName);
                        }
                    }
                    else
                    {
                        Logger.Error("LoadProfileWindow.LoadProfiles", $"{file} is not a valid config file!");
                    }
                }
            }
            catch(Exception ex)
            {
                Logger.Error("LoadProfileWindow.LoadProfiles", ex.ToString());
            }
        }
    }
}
