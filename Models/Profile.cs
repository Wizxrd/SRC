using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Windows;

namespace SRCClient.Models
{
    internal class Profile
    {
        public Profile() { }

        public static void Save(string profileName, Window mainWindow)
        {
            try
            {
                JObject profile = new JObject
                {
                    { "Name", profileName },
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
                string serialized = JsonConvert.SerializeObject(profile, Formatting.Indented);
                File.WriteAllText(LoadFile.Load("Profiles", $"{profileName}.json"), serialized);
            }
            catch (Exception ex)
            {
                Logger.Error("Profile.Save", ex.ToString());
            }
        }

    }
}
