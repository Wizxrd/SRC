using SRCClient.Models;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace SRCClient.Views
{
    public partial class LoadProfileWindow : Window
    {
        MainWindow? mainWindow;
        public LoadProfileWindow(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            Logger.Error("F", this.Width.ToString());
            InitializeComponent();
            InitializeEvents();
        }

        private void InitializeEvents()
        {
            this.Loaded += LoadProfileWindowLoaded;
        }

        private void LoadProfileWindowLoaded(object sender, RoutedEventArgs e)
        {
            LoadProfileButtons();
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

        private void ReloadProfileStack()
        {
            ProfileStack.Children.Clear();
            LoadProfileButtons();
        }

        private System.Windows.Controls.Button? FindParentButton(System.Windows.Controls.TextBox textBox)
        {
            var grid = textBox.Parent as Grid;
            if (grid != null)
            {
                var parentButton = grid.Parent as System.Windows.Controls.Button;
                if (parentButton == null) return null;
                return parentButton;
            }
            return null;
        }

        private void LoadProfileButtons()
        {
            try
            {
                List<string> profileNames = new List<string>();
                string[] files = Directory.GetFiles(LoadFile.LoadFolder("Profiles"));

                foreach (string file in files)
                {
                    if (file.Contains(".json"))
                    {
                        JObject profile = JObject.Parse(File.ReadAllText(LoadFile.Load("Profiles", file)));
                        string profileName = profile["Name"]?.ToString() ?? string.Empty;

                        if (profileName != string.Empty)
                        {
                            profileNames.Add(profileName);
                        }
                    }
                    else
                    {
                        Logger.Error("LoadProfileWindow.LoadProfiles", $"{file} is not a valid config file and can't be loaded!");
                    }
                }
                profileNames = profileNames.OrderBy(p => p, StringComparer.OrdinalIgnoreCase).ToList();
                List<System.Windows.Controls.Button> buttons = new List<System.Windows.Controls.Button>();
                double maxWidth = this.MinWidth;
                foreach (string profileName in profileNames)
                {
                    System.Windows.Controls.Button? button = CreateProfileButton(profileName);
                    if (button != null)
                    {
                        buttons.Add(button);
                        if (button.Width > maxWidth)
                        {
                            maxWidth = button.Width;
                        }
                    }
                }
                foreach (System.Windows.Controls.Button button in buttons)
                {
                    button.Width = maxWidth;
                }
                this.Width = maxWidth + 20;
                if (ProfileScrollBar.ComputedVerticalScrollBarVisibility == Visibility.Visible)
                {
                    this.Width += 10;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("LoadProfileWindow.LoadProfiles", ex.ToString());
            }
        }

        private System.Windows.Controls.Button? CreateProfileSubButtons(string toolTip, string imageSource, System.Windows.Controls.Button parent, RoutedEventHandler clickHandler)
        {
            try
            {
                System.Windows.Controls.Button button = new System.Windows.Controls.Button
                {
                    Background = System.Windows.Media.Brushes.Transparent,
                    Width = 32,
                    Height = 32,
                    Margin = new Thickness(0, 0, 0, 0),
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Right,
                    VerticalAlignment = VerticalAlignment.Center,
                    BorderThickness = new Thickness(1),
                    BorderBrush = new SolidColorBrush(System.Windows.Media.Color.FromRgb(56, 182, 255)),
                    ToolTip = toolTip,
                    Template = (ControlTemplate)this.Resources["ProfileSubButtonTemplate"],
                    Tag = parent,
                    Content = new System.Windows.Controls.Image
                    {
                        Width = 16,
                        Height = 16,
                        Source = new BitmapImage(new Uri(imageSource, UriKind.Absolute)),
                    },
                };
                button.Click += clickHandler;
                button.PreviewMouseDoubleClick += (s, e) =>
                {
                    e.Handled = true;
                };

                return button;
            }
            catch(Exception ex)
            {
                Logger.Error("LoadProfileWindow.CreateProfileSubButtons", ex.ToString());
                return null;
            }
        }

        public System.Windows.Controls.Button? CreateProfileButton(string profileName)
        {
            try
            {
                System.Windows.Controls.Button button = new System.Windows.Controls.Button
                {
                    Name = profileName.Replace(" ", "_"),
                    Height = 32,
                    Margin = new Thickness(10, 0, 10, 0),
                    Padding = new Thickness(0, 0, 0, 0),
                    Background = new SolidColorBrush(Colors.Transparent),
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                    Template = (ControlTemplate)this.Resources["ProfileButtonTemplate"]
                };

                // Create the Grid
                Grid grid = new Grid();
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                TextBlock textBlock = new TextBlock
                {
                    Text = profileName,
                    Foreground = System.Windows.Media.Brushes.White,
                    VerticalAlignment = VerticalAlignment.Center,
                    Padding = new Thickness(5, 0, 0, 0),
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                };
                Grid.SetColumn(textBlock, 0);
                grid.Children.Add(textBlock);

                StackPanel stackPanel = new StackPanel
                {
                    Orientation = System.Windows.Controls.Orientation.Horizontal,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(5, 0, 0, 0)
                };

                Grid.SetColumn(stackPanel, 1);

                button.Click += ProfileButtonClick;
                button.MouseDoubleClick += ProfileButtonMouseDoubleClick;

                // Create and add sub-buttons
                System.Windows.Controls.Button? renameButton = CreateProfileSubButtons("Rename Profile", "pack://application:,,,/Images/Rename.png", button, RenameProfileButtonClick);
                System.Windows.Controls.Button? copyButton = CreateProfileSubButtons("Copy Profile", "pack://application:,,,/Images/Copy.png", button, CopyProfileButtonClick);
                System.Windows.Controls.Button? deleteButton = CreateProfileSubButtons("Delete Profile", "pack://application:,,,/Images/Delete.png", button, DeleteProfileButtonClick);

                if (renameButton != null && copyButton != null && deleteButton != null)
                {
                    stackPanel.Children.Add(renameButton);
                    stackPanel.Children.Add(copyButton);
                    stackPanel.Children.Add(deleteButton);
                    grid.Children.Add(stackPanel);

                    button.Content = grid;
                    ProfileStack.Children.Add(button);
                    textBlock.Measure(new System.Windows.Size(double.PositiveInfinity, double.PositiveInfinity));
                    double textWidth = textBlock.DesiredSize.Width;

                    stackPanel.Measure(new System.Windows.Size(double.PositiveInfinity, double.PositiveInfinity));
                    double stackPanelWidth = stackPanel.DesiredSize.Width;
                    double requiredWidth = textWidth + stackPanelWidth;
                    button.Width = requiredWidth;
                    return button;
                }
                else
                {
                    throw new Exception("Sub Menu Button(s) Null");
                }
            }
            catch (Exception ex)
            {
                Logger.Error("LoadProfileWindow.CreateProfileButton", ex.ToString());
            }
            return null;
        }

        private void ProfileButtonClick(object sender, RoutedEventArgs e)
        {
            var button = sender as System.Windows.Controls.Button;
            if (button == null) return;
            foreach (var child in ProfileStack.Children)
            {
                var childButton = child as System.Windows.Controls.Button;
                if (childButton != null)
                {
                    var childBorder = childButton.Template.FindName("ButtonBorder", childButton) as Border;
                    if (childBorder != null && childBorder.BorderThickness == new Thickness(1))
                    {
                        childBorder.BorderThickness = new Thickness(0);
                    }
                }
            }
            var border = button.Template.FindName("ButtonBorder", button) as Border;
            if (border != null)
            {
                border.BorderThickness = new Thickness(1);
            }
        }

        private void ProfileButtonMouseDoubleClick(object sender, EventArgs e)
        {
            try
            {
                var button = sender as System.Windows.Controls.Button;
                if (button == null) return;
                if (mainWindow == null) return;
                Profile.Load(button.Name.Replace("_", " "), mainWindow);
                this.Close();
            }
            catch(Exception ex)
            {
                Logger.Error("LoadProfileWindow.ProfileButtonMouseDoubleClick", ex.ToString());
            }
        }

        private void RenameProfileButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var button = sender as System.Windows.Controls.Button;
                if (button == null) return;
                var parentButton = button.Tag as System.Windows.Controls.Button;
                if (parentButton == null) return;
                var grid = parentButton.Content as Grid;
                if (grid == null) return;
                foreach (var child in ProfileStack.Children)
                {
                    var otherButton = child as System.Windows.Controls.Button;
                    if (otherButton != null && otherButton != parentButton)
                    {
                        var stackGrid = otherButton.Content as Grid;
                        if (stackGrid == null)
                        {
                            Logger.Debug("LoadProfileWindow.RenameButtonClick", "StackPanel does not contain a Grid.");
                            continue;
                        }
                        var otherTextBox = stackGrid.Children.OfType<System.Windows.Controls.TextBox>().FirstOrDefault();
                        if (otherTextBox != null)
                        {
                            var oldTextBlock = new TextBlock
                            {
                                Text = otherTextBox.Text,
                                Foreground = System.Windows.Media.Brushes.White,
                                VerticalAlignment = VerticalAlignment.Center,
                                Margin = otherTextBox.Margin
                            };
                            stackGrid.Children.Remove(otherTextBox);
                            Grid.SetColumn(oldTextBlock, 0);
                            stackGrid.Children.Add(oldTextBlock);
                        }
                    }
                }
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
                    newTextBox.CaretBrush = System.Windows.Media.Brushes.White;
                    newTextBox.KeyDown += TextBoxKeyDown;
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
                        Text = parentButton.Name.Replace("_", " "),
                        Foreground = System.Windows.Media.Brushes.White,
                        VerticalAlignment = VerticalAlignment.Center,
                        Margin = textBox.Margin
                    };
                    textBox.KeyDown -= TextBoxKeyDown;
                    grid.Children.Remove(textBox);
                    Grid.SetColumn(newTextBlock, 0);
                    grid.Children.Add(newTextBlock);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("LoadProfileWindow.RenameButtonClick", ex.ToString());
            }
        }

        private void TextBoxKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    var textBox = sender as System.Windows.Controls.TextBox;
                    if (textBox == null) return;
                    var parentButton = FindParentButton(textBox);
                    if (parentButton == null) return;
                    string oldName = parentButton.Name.Replace("_", " ");
                    string newName = textBox.Text;
                    parentButton.Name = newName.Replace(" ", "_");
                    var grid = parentButton.Content as Grid;
                    if (grid == null) return;
                    var newTextBlock = new TextBlock
                    {
                        Text = textBox.Text,
                        Foreground = System.Windows.Media.Brushes.White,
                        VerticalAlignment = VerticalAlignment.Center,
                        Margin = textBox.Margin
                    };
                    grid.Children.Remove(textBox);
                    Grid.SetColumn(newTextBlock, 0);
                    grid.Children.Add(newTextBlock);
                    Profile.Rename(oldName, newName);
                    ReloadProfileStack();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("LoadProfileWindow.TextBoxKeyDown", ex.ToString());
            }
        }

        private void CopyProfileButtonClick(object sender, RoutedEventArgs e)
        {
            var button = sender as System.Windows.Controls.Button;
            if (button == null) return;
            var parentButton = button.Tag as System.Windows.Controls.Button;
            if (parentButton == null) return;
            Profile.Copy(parentButton.Name.Replace("_", " "));
            ReloadProfileStack();
        }

        private void DeleteProfileButtonClick(object sender, RoutedEventArgs e)
        {
            var button = sender as System.Windows.Controls.Button;
            if (button == null) return;
            var parentButton = button.Tag as System.Windows.Controls.Button;
            if (parentButton == null) return;
            string profileName = parentButton.Name.Replace("_", " ");
            ConfirmDeleteProfileWindow confirmDeleteProfileWindow = new ConfirmDeleteProfileWindow()
            {
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            confirmDeleteProfileWindow.SetMessageTextBlock("Are you sure you want to delete profile:", profileName);
            confirmDeleteProfileWindow.ShowDialog();
            if (confirmDeleteProfileWindow.DeletionConfirmed)
            {
                ProfileStack.Children.Remove(parentButton);
                if (mainWindow != null)
                {
                    Profile.Delete(profileName, mainWindow);
                    ReloadProfileStack();
                }
            }
        }

        private void NewProfileButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void ImportProfileButtonClick(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Title = "Import Profile",
                Filter = "JSON File (*.json)|*.json",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
            }
        }

        private void DeleteAllProfilesButtonClick(object sender, RoutedEventArgs e)
        {
            ConfirmDeleteProfileWindow confirmDeleteProfileWindow = new ConfirmDeleteProfileWindow()
            {
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            confirmDeleteProfileWindow.SetMessageTextBlock("Are you sure you want to delete ALL profiles?", string.Empty);
            confirmDeleteProfileWindow.ShowDialog();
            if (confirmDeleteProfileWindow.DeletionConfirmed)
            {
                if (mainWindow != null)
                {
                    Profile.DeleteAll();
                    ReloadProfileStack();
                }
            }
        }
    }
}
