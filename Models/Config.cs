using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Windows;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;

namespace SRCClient.Models
{
    public class Config
    {
        public static void Load(MainWindow mainWindow)
        {
            try
            {
                JObject config = JObject.Parse(File.ReadAllText(LoadFile.Load("Config", "Config.json")));
                string cursor = config["Cursor"]?.ToString() ?? string.Empty;
                if (cursor != string.Empty)
                {
                    mainWindow.CurrentCursor = cursor;
                    mainWindow.Cursor = new System.Windows.Input.Cursor(System.Windows.Application.GetResourceStream(new Uri($"pack://application:,,,/Cursors/{cursor}", UriKind.Absolute)).Stream);
                }
                mainWindow.Left = (int?)config["Window"]?["Location"]?["Left"] ?? 0;
                mainWindow.Top = (int?)config["Window"]?["Location"]?["Top"] ?? 0;
                mainWindow.Width = (int?)config["Window"]?["Size"]?["Width"] ?? 1280;
                mainWindow.Height = (int?)config["Window"]?["Size"]?["Height"] ?? 720;
            }
            catch (Exception ex)
            {
                Logger.Error("Config.Load", ex.ToString());
            }
        }

        public static void Save(MainWindow mainWindow)
        {
            JObject config = new JObject
            {
                {"Cursor", mainWindow.CurrentCursor },
                {"LastUsedProfile", mainWindow.LastUsedProfile },
                { "Window", new JObject
                    {
                        {"Fullscreen", mainWindow.WindowState == WindowState.Maximized},
                        { "Size", new JObject
                            {
                                { "Width", mainWindow.Width },
                                { "Height", mainWindow.Height }
                            }
                        },
                        { "Location", new JObject
                            {
                                { "Left", mainWindow.Left },
                                { "Top", mainWindow.Top }
                            }
                        }
                    }
                }
            };
            string serialized = JsonConvert.SerializeObject(config, Formatting.Indented);
            File.WriteAllText(LoadFile.Load("Config", "Config.json"), serialized);
            Logger.Debug("Config.Save", "Config Saved");
        }
    }
}
