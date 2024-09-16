﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Windows;
using System.Xml.Linq;

namespace SRCClient.Models
{
    internal class Profile
    {
        private static string GenerateUniqueHash()
        {
            return Guid.NewGuid().ToString();
        }

        public static void Load(string name, MainWindow mainWindow)
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
                        if (profileName != string.Empty && profileName == name)
                        {
                            string cursor = profile["Cursor"]?.ToString() ?? string.Empty;
                            if (cursor != string.Empty)
                            {
                                mainWindow.Cursor = new System.Windows.Input.Cursor(System.Windows.Application.GetResourceStream(new Uri($"pack://application:,,,/Cursors/{cursor}", UriKind.Absolute)).Stream);
                            }
                            bool fullscreen = false;
                            var fullscreenToken = profile["Window"]?["Fullscreen"];
                            if (fullscreenToken != null && fullscreenToken.Type == JTokenType.Boolean)
                            {
                                fullscreen = fullscreenToken.ToObject<bool>();
                                if (fullscreen)
                                {
                                    mainWindow.SetMainWindowGridMargin(5);
                                    mainWindow.WindowState = WindowState.Maximized;
                                }
                                else
                                {
                                    mainWindow.SetMainWindowGridMargin(0);
                                    mainWindow.WindowState = WindowState.Normal;
                                }
                            }
                            double width = 1280.0;
                            var widthToken = profile["Window"]?["Size"]?["Width"];
                            if (widthToken != null && widthToken.Type == JTokenType.Float)
                            {
                                width = widthToken.ToObject<double>();
                            }
                            double height = 720.0;
                            var heightToken = profile["Window"]?["Size"]?["Height"];
                            if (heightToken != null && heightToken.Type == JTokenType.Float)
                            {
                                height = heightToken.ToObject<double>();
                            }
                            double left = 50;
                            var leftToken = profile["Window"]?["Location"]?["Left"];
                            if (leftToken != null && leftToken.Type == JTokenType.Float)
                            {
                                left = leftToken.ToObject<double>();
                            }
                            double top = 50;
                            var topToken = profile["Window"]?["Location"]?["Top"];
                            if (topToken != null && topToken.Type == JTokenType.Float)
                            {
                                top = topToken.ToObject<double>();
                            }

                            mainWindow.Width = width;
                            mainWindow.Height = height;
                            mainWindow.Left = left;
                            mainWindow.Top = top;
                            mainWindow.LoadedProfile = name;
                            mainWindow.Focus();
                            Logger.Debug("Profile.Load", $"Profile: \"{name}\" Successfully Loaded");
                            break;
                        }
                    }
                    else
                    {
                        Logger.Error("LoadProfileWindow.LoadProfiles", $"{file} is not a valid config file!");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Profile.Load", ex.ToString());
            }
        }

        public static void Save(string name, MainWindow mainWindow)
        {
            string[] files = Directory.GetFiles(LoadFile.LoadFolder("Profiles"));
            foreach (string file in files)
            {
                if (file.Contains(".json"))
                {
                    JObject profile = JObject.Parse(File.ReadAllText(LoadFile.Load("Profiles", file)));
                    string profileName = profile["Name"]?.ToString() ?? string.Empty;
                    if (profileName != string.Empty && profileName == name)
                    {
                        profile["Name"] = name;
                        profile["Cursor"] = mainWindow.CursorFile;
                        profile["Window"]["Fullscreen"] = mainWindow.WindowState == WindowState.Maximized;
                        profile["Window"]["Size"]["Width"] = mainWindow.Width;
                        profile["Window"]["Size"]["Height"] = mainWindow.Height;
                        profile["Window"]["Location"]["Left"] = mainWindow.Left;
                        profile["Window"]["Location"]["Top"] = mainWindow.Top;
                        string serialized = JsonConvert.SerializeObject(profile, Formatting.Indented);
                        File.WriteAllText(LoadFile.Load("Profiles", $"{Path.GetFileName(file)}"), serialized);
                        Logger.Debug("Profile.Load", $"Profile: \"{name}\" Successfully Saved");
                        break;
                    }
                }
            }
        }

        public static void SaveAs(string name, MainWindow mainWindow)
        {
            Logger.Debug("SaveAs", mainWindow.Cursor.ToString());
            try
            {
                JObject profile = new JObject
                {
                    { "Name", name },
                    {"Cursor", mainWindow.CursorFile },
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
                File.WriteAllText(LoadFile.Load("Profiles", $"{GenerateUniqueHash()}.json"), serialized);
                Logger.Debug("Profile.Load", $"Profile: \"{name}\" Successfully Saved As");
                Load(name, mainWindow);
            }
            catch (Exception ex)
            {
                Logger.Error("Profile.SaveAs", ex.ToString());
            }
        }

        public static void Copy(string name)
        {
            string[] files = Directory.GetFiles(LoadFile.LoadFolder("Profiles"));
            foreach (string file in files)
            {
                if (file.Contains(".json"))
                {
                    JObject profile = JObject.Parse(File.ReadAllText(LoadFile.Load("Profiles", file)));
                    string profileName = profile["Name"]?.ToString() ?? string.Empty;
                    if (profileName != string.Empty && profileName == name)
                    {
                        profile["Name"] = $"{name} Copy";
                        string serialized = JsonConvert.SerializeObject(profile, Formatting.Indented);
                        File.WriteAllText(LoadFile.Load("Profiles", $"{GenerateUniqueHash()}.json"), serialized);
                        Logger.Debug("Profile.Load", $"Profile: \"{name}\" Successfully Copied");
                        break;
                    }
                }
                else
                {
                    Logger.Error("LoadProfileWindow.LoadProfiles", $"{file} is not a valid config file!");
                }
            }
        }

        public static void Rename(string oldName, string newName)
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
                        if (profileName != string.Empty && profileName == oldName)
                        {
                            profile["Name"] = newName;
                            string serialized = JsonConvert.SerializeObject(profile, Formatting.Indented);
                            File.WriteAllText(LoadFile.Load("Profiles", $"{Path.GetFileName(file)}"), serialized);
                            Logger.Debug("Profile.Load", $"Profile: \"{oldName}\" Successfully Renamed To: \"{newName}\"");
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("LoadProfileWindow.RenameProfile", ex.ToString());
            }
        }

        public static void Delete(string name, MainWindow mainWindow)
        {
            string[] files = Directory.GetFiles(LoadFile.LoadFolder("Profiles"));
            foreach (string file in files)
            {
                if (file.Contains(".json"))
                {
                    JObject profile = JObject.Parse(File.ReadAllText(LoadFile.Load("Profiles", file)));
                    string profileName = profile["Name"]?.ToString() ?? string.Empty;
                    if (profileName != string.Empty && profileName == name)
                    {
                        if (name == mainWindow.LoadedProfile)
                        {
                            mainWindow.LoadedProfile = string.Empty;
                        }
                        File.Delete(file);
                        Logger.Debug("Profile.Load", $"Profile: \"{name}\" Successfully Deleted");
                        break;
                    }
                }
            }
        }
    }
}
